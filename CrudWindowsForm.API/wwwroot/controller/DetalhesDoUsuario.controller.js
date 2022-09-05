sap.ui.define([
    "./BaseController",
    "sap/ui/core/syncStyleClass",
    "sap/m/MessageToast",
    "../validator/mensagensDeErro",
    "../repo/UsuarioRepositorio",
    "../model/formataData"
], function(BaseController, syncStyleClass, MessageToast, mensagensDeErro, UsuarioRepositorio, formataData) {
    "use strict";

    let rotas;
    return BaseController.extend("crudBasico.controller.DetalhesDoUsuario", {
      onInit: function () {
        rotas = this.getOwnerComponent().getRouter();
        rotas.getRoute("Detalhes").attachPatternMatched(this.pegaUsuarioPorId, this);
      },
      aoClicarEmEditar: function (event) {
        let idUsuario = event.getSource().getModel("usuario").getData().id;

        rotas.navTo("Editar", {
          caminhoDaListaDeUsuarios: window.encodeURIComponent(idUsuario),
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
          let idUsuario = event.getSource().getModel("usuario").getData().id;
          
          let respostaHttp = await UsuarioRepositorio.deletar(idUsuario);
          
          respostaHttp.ok ? rotas.navTo("ListaUsuarios", {}, true) :  mensagensDeErro.mensagensDeErro(err)
        } catch(err) {
          MessageToast.show(err);
        }
      },
      fecharDialog: function() {
        this.byId("dialogParaConfirmacao").close();
      },
      pegaUsuarioPorId: async function (event) {
        try {
          let idUsuario = event.getParameters().arguments.caminhoDaListaDeUsuarios;

          let usuario = await UsuarioRepositorio.obterPorId(idUsuario);

          usuario.dataCriacao = new Date(usuario.dataCriacao);
          if (usuario.dataNascimento) usuario.dataNascimento = new Date(usuario.dataNascimento);

          this.criaModelo(usuario, "usuario");
        } catch (err) {
          MessageToast.show(err.message);
        }
      },
    });
});