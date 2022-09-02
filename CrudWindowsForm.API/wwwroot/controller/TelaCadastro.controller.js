sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/core/routing/History",
    "sap/ui/model/json/JSONModel",
    "sap/m/MessageToast",
    "sap/ui/core/format/DateFormat",
    "../validator/validaCampos",
    "../validator/mensagensDeErro"
], function(Controller, History, JSONModel, MessageToast, DateFormat, validaCampos, mensagensDeErro) {
    "use strict";
    let rotas;

    return Controller.extend("crudBasico.controller.TelaCadastro", {
      onInit: function () {
        rotas = this.getOwnerComponent().getRouter();
        
        rotas
          .getRoute("Cadastrar")
          .attachPatternMatched(this.limpaValoresDosCampos, this);
        rotas
          .getRoute("Editar")
          .attachPatternMatched(this.criaModeloUsuario, this);
      },
      aoClicarEmVoltar: function () {
        let historico = History.getInstance();
        let rotaAnterior = historico.getPreviousHash();

        if (rotaAnterior !== undefined) {
          window.history.go(-1);
        } else {
          rotas.navTo("ListaUsuarios", {}, true);
        }
      },
      criaModeloUsuario: async function (event) {
        let idUsuario = event.getParameters().arguments.caminhoDaListaDeUsuarios;

        try {
          let respostaHTTP = await fetch(`https://localhost:7150/api/Usuario/${idUsuario}`, {method: "GET"});

          let dadosRetornados = await respostaHTTP.json();

          if (!respostaHTTP.ok) mensagensDeErro.mensagensDeErro(dadosRetornados.value);

          if (dadosRetornados.dataNascimento !== null) {
            dadosRetornados.dataNascimento = DateFormat.getDateInstance({pattern: "yyyy-MM-dd"})
            .format(new Date(dadosRetornados.dataNascimento));
          } else {
            let i18n = this.getView().getModel("i18n").getResourceBundle();
            this.byId("campoDataNascimento")
            .setPlaceholder(i18n.getText("PlaceholderAvisoDataNascimentoEditar"));
          }

          let usuario = new JSONModel(dadosRetornados);
                      
          this.getView().setModel(usuario, "usuario");
          
          this._alteracoesParaRotaDeEditar();
        } catch (err) {
          MessageToast.show("Ocorreu um erro" + err.message);
        }
      },
      retornaArrayDeCampos: function() {
        return [
          this.byId("campoNome"),
          this.byId("campoEmail"),
          this.byId("campoSenha"),
          this.byId("campoDataNascimento")
        ];
      },
      limpaValoresDosCampos: function () {
        this.retornaArrayDeCampos().forEach(input => {
          input.setValue("");
        });
        
        this._alteracoesParaRotaDeCadastrar();
      },
      limpaErrosDosCampos: function() {
        this.retornaArrayDeCampos().forEach(input => {
          input.setValueState("None");
        });
      },
      _alteracoesParaRotaDeCadastrar: function () {
        this.byId("btnCadastrar").setVisible(true);

        this.byId("btnEditar").setVisible(false);
        
        let i18n = this.getView().getModel("i18n").getResourceBundle();
        
        this.byId("campoDataNascimento").setPlaceholder(i18n.getText("PlaceholderDataNascimentoCadastrar"));
        
        this.byId("paginaCadastro").setTitle(i18n.getText("TituloTelaCadastrar"));
        
        this.byId("campoEmail").setEnabled(true);

        this.byId("campoSenha").setVisible(true);

        this.limpaErrosDosCampos();
      },
      _alteracoesParaRotaDeEditar: function () {
        let i18n = this.getView().getModel("i18n").getResourceBundle();

        this.byId("paginaCadastro").setTitle(i18n.getText("TituloTelaEditar"));

        this.byId("btnEditar").setVisible(true);
        
        this.byId("btnCadastrar").setVisible(false);
        
        this.byId("campoEmail").setEnabled(false);

        this.byId("campoSenha").setVisible(false);

        this.limpaErrosDosCampos();
      },
      botaoCadastrar: function() {
        this.servicoParaCadastrarEAtualizar({verboHTTP: "POST"});
      },
      botaoEditar: function(event) {
        let idUsuario = event.getSource().getModel("usuario").getData().id
        this.servicoParaCadastrarEAtualizar({verboHTTP: "PUT", idUsuario});
      },
      botaoCancelar: function() {
        this.aoClicarEmVoltar();
      },
      servicoParaCadastrarEAtualizar: async function({verboHTTP, idUsuario}) {
        try {
          let objetoUsuario = {
            nome: validaCampos.retornaValorCampoGenerico.bind(this)(this.byId("campoNome")),
            email: validaCampos.retornaEmailValido.bind(this)(this.byId("campoEmail")),
            senha: validaCampos.retornaValorCampoGenerico.bind(this)(this.byId("campoSenha")),
            dataNascimento: validaCampos.retornaDataValida.bind(this)(this.byId("campoDataNascimento")) || null
          };

          if (idUsuario != undefined) objetoUsuario.id = idUsuario;
          
          let respostaHttp = await fetch(`https://localhost:7150/api/Usuario`, {
            method: `${verboHTTP}`,
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(objetoUsuario)
          });

          let resultado = respostaHttp.headers.get("content-type") !== null ? await respostaHttp.json() : null;

          if (!respostaHttp.ok) {
            validaCampos.defineCampoDeErroDaApi.bind(this)({nomePropriedade: resultado.value.nomePropriedade, 
              mensagem: resultado.value.mensagemErro});
          }
          
          let i18n = this.getView().getModel("i18n").getResourceBundle();
          MessageToast.show(i18n.getText("MensagemDeSucessoAoCadastrar"));
                
          rotas.navTo("ListaUsuarios", {}, true);
        } catch (err) {
          MessageToast.show(err.message)
        }
      }
    });
});