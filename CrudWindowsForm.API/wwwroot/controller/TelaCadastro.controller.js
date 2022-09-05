sap.ui.define([
    "./BaseController",
    "sap/m/MessageToast",
    "../model/formataData",
    "../validator/validaCampos",
    "../repo/UsuarioRepositorio"
], function(BaseController, MessageToast, formataData, validaCampos, UsuarioRepositorio) {
    "use strict";
    let rotas;

    return BaseController.extend("crudBasico.controller.TelaCadastro", {
      onInit: function () {
        rotas = this.getOwnerComponent().getRouter();
        
        rotas
          .getRoute("Cadastrar")
          .attachPatternMatched(this._alteracoesParaRotaDeCadastrar, this);
        rotas
          .getRoute("Editar")
          .attachPatternMatched(this.criaModeloParaRotaEditar, this);
      },
      retornaArrayDeCampos: function() {
        return [
          this.byId("campoNome"),
          this.byId("campoEmail"),
          this.byId("campoSenha"),
          this.byId("campoDataNascimento")
        ];
      },
      limpaErrosDosCampos: function() {
        this.retornaArrayDeCampos().forEach(input => {
          input.setValueState("None");
        });
      },
      _alteracoesParaRotaDeCadastrar: function () {
        let i18n = this.getView().getModel("i18n").getResourceBundle();

        this.byId("btnCadastrar").setVisible(true);

        this.byId("btnEditar").setVisible(false);
        
        this.byId("campoDataNascimento").setPlaceholder(i18n.getText("PlaceholderDataNascimentoCadastrar"));
        
        this.byId("paginaCadastro").setTitle(i18n.getText("TituloTelaCadastrar"));
        
        this.byId("campoEmail").setEnabled(true);

        this.byId("campoSenha").setVisible(true);

        this.limpaErrosDosCampos();
      
        this.criaModelo({}, "usuario");
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
      criaModeloParaRotaEditar: async function (event) {
        let i18n = this.getView().getModel("i18n").getResourceBundle();
        try {
          let idUsuario = event.getParameters().arguments.caminhoDaListaDeUsuarios;
          let dadosRetornados = await UsuarioRepositorio.obterPorId(idUsuario);

          if (dadosRetornados.dataNascimento !== null) {
            dadosRetornados.dataNascimento = formataData.retornaDataFormatada(dadosRetornados.dataNascimento);
          } else {
            this.byId("campoDataNascimento")
            .setPlaceholder(i18n.getText("PlaceholderAvisoDataNascimentoEditar"));
          }

          this.criaModelo(dadosRetornados, "usuario")

          this._alteracoesParaRotaDeEditar();
        } catch (err) {
          MessageToast.show(i18n.getText("Mensagem.OcorreuUmErro"));
        }
      },
      botaoCadastrar: function() {
        this.servicoParaCadastrarEAtualizar();
      },
      botaoEditar: function(event) {
        let idUsuario = event.getSource().getModel("usuario").getData().id
        this.servicoParaCadastrarEAtualizar(idUsuario);
      },
      aoClicarEmCancelar: function() {
        this.aoClicarEmVoltar();
      },
      servicoParaCadastrarEAtualizar: async function(idUsuario) {
        try {
          let objetoUsuario = {
            nome: validaCampos.retornaValorCampoGenerico.bind(this)(this.byId("campoNome")),
            email: validaCampos.retornaEmailValido.bind(this)(this.byId("campoEmail")),
            senha: validaCampos.retornaValorCampoGenerico.bind(this)(this.byId("campoSenha")),
            dataNascimento: validaCampos.retornaDataValida.bind(this)(this.byId("campoDataNascimento")) || null
          };
          
          if (idUsuario !== undefined) {
            objetoUsuario.id = idUsuario
            await UsuarioRepositorio.atualizar.bind(this)(objetoUsuario);
          } else {
            await UsuarioRepositorio.criar.bind(this)(objetoUsuario);
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