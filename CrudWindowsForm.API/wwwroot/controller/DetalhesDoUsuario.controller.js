sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/core/routing/History",
    "sap/ui/model/json/JSONModel",
    "sap/ui/core/syncStyleClass",
    "sap/ui/core/format/DateFormat",
    "sap/m/MessageToast"
], function(Controller, History, JSONModel, syncStyleClass, DateFormat, MessageToast) {
    "use strict";

    return Controller.extend("crudBasico.controller.DetalhesDoUsuario", {
      onInit: function () {
        let rotas = this.getOwnerComponent().getRouter();
        rotas.getRoute("Detalhes").attachPatternMatched(this.pegaUsuarioPorId, this);
      },
      aoClicarEmVoltar: function () {
        var historico = History.getInstance();
        var rotaAnterior = historico.getPreviousHash();

        if (rotaAnterior !== undefined) {
          window.history.go(-1);
        } else {
          let rotas = this.getOwnerComponent().getRouter();
          rotas.navTo("ListaUsuarios", {}, true);
        }
      },
      aoClicarEmEditar: function (event) {
        let idUsuario = event.getSource().getModel("usuario").getData().id;
        let rotas = this.getOwnerComponent().getRouter();

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
      confirmarDialog: function(event) {
        let idUsuario = event.getSource().getModel("usuario").getData().id;

        fetch(`https://localhost:7150/api/Usuario/${idUsuario}`, {
          method: "DELETE",
        }).then(() => {
          let rotas = this.getOwnerComponent().getRouter();
          rotas.navTo("ListaUsuarios", {}, true);
        }).catch(err => console.log(err))
      },
      fecharDialog: function() {
        this.byId("dialogParaConfirmacao").close();
      },
      pegaUsuarioPorId: async function (event) {
        try {
          let idUsuario = event.getParameters().arguments.caminhoDaListaDeUsuarios;

          let respostaHttp = await fetch(`https://localhost:7150/api/Usuario/${idUsuario}`, {method: "GET"});
          let respostaBody = await respostaHttp.json();

          respostaBody.dataCriacao = DateFormat.getDateInstance().format(new Date(respostaBody.dataCriacao));
          if (respostaBody.dataNascimento) respostaBody.dataNascimento = new Date(respostaBody.dataNascimento);

          let usuario = new JSONModel(respostaBody);
          this.getView().setModel(usuario, "usuario");
        } catch (err) {
          MessageToast.show(err.message);
        }
      },
    });
});