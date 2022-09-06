sap.ui.define([
    "./BaseController",
    "sap/ui/core/syncStyleClass",
    "sap/m/MessageToast",
    "../validator/mensagensDeErro",
    "../repo/UsuarioRepositorio"
], function(BaseController, syncStyleClass, MessageToast, mensagensDeErro, UsuarioRepositorio) {
    "use strict";

    let rotas;
    let nomeModelo = "usuario";
    return BaseController.extend("crudBasico.controller.DetalhesDoUsuario", {
      onInit: function () {
        rotas = this.getOwnerComponent().getRouter();
        rotas.getRoute(this.constanteRotas.ROTA_DETALHES).attachPatternMatched(this.pegaUsuarioPorId, this);
      },
      aoClicarEmEditar: function (event) {
        let idUsuario = event.getSource().getModel(nomeModelo).getData().id;

        rotas.navTo(this.constanteRotas.ROTA_EDITAR, {
          id: window.encodeURIComponent(idUsuario),
        });
      },
      aoClicarEmDeletar: function() {
          if (!this.pDialog) {
            this.pDialog = this.loadFragment({
              name: "crudBasico.view.DialogParaConfirmacao"
            })
            .then(function (dialog) {
              syncStyleClass(this.getOwnerComponent().getContentDensityClass(), this.getView(), dialog);
              return dialog;
            }.bind(this));
          }

          this.pDialog.then(function (dialog) {
            dialog.open();
          })
      },
      confirmarDialog: async function(event) {
        try {
          let i18n = this.getView().getModel("i18n").getResourceBundle();

          let idUsuario = event.getSource().getModel(nomeModelo).getData().id;
          
          let respostaHttp = await UsuarioRepositorio.deletar(idUsuario);
          
          if (respostaHttp.ok) {
            MessageToast.show(i18n.getText("Mensagem.UsuarioRemovidoComSucesso"), this.objetoDeOpcoesMessageToast);
            this.fecharDialog();
            rotas.navTo(this.constanteRotas.ROTA_LISTAR, {}, true);
          } else {
            mensagensDeErro.mensagensDeErro(err)
          }

        } catch(err) {
          MessageToast.show(err, this.objetoDeOpcoesMessageToast);
        }
      },
      fecharDialog: function() {
        this.byId("dialogParaConfirmacao").close();
      },
      pegaUsuarioPorId: async function (event) {
        try {
          let idUsuario = event.getParameters().arguments.id;

          let usuario = await UsuarioRepositorio.obterPorId(idUsuario);

          usuario.dataCriacao = new Date(usuario.dataCriacao);
          if (usuario.dataNascimento) usuario.dataNascimento = new Date(usuario.dataNascimento);

          this.criaModelo(usuario, nomeModelo);
        } catch (err) {
          MessageToast.show(err.message, this.objetoDeOpcoesMessageToast);
        }
      },
    });
});