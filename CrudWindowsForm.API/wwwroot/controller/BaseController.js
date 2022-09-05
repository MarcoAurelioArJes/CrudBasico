sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/core/routing/History",
    "sap/ui/model/json/JSONModel",
    "sap/m/MessageToast",
], function(Controller, History, JSONModel, MessageToast) {
    "use strict";

    return Controller.extend("crudBasico.controller.BaseController", {
        onInit: function() {
          
        },
        aoClicarEmVoltar: function () {
            let historico = History.getInstance();
            let rotaAnterior = historico.getPreviousHash();
    
            if (rotaAnterior !== undefined) {
              window.history.go(-1);
            } else {
              let rotas = this.getOwnerComponent().getRouter();
              rotas.navTo("ListaUsuarios", {}, true);
            }
        },
        criaModelo: async function(modelo, nome) {
            try {
                let modeloCriado = new JSONModel(modelo);

                this.getView().setModel(modeloCriado, nome);
            } catch (err) {
                MessageToast.show(err.message);
            }
        }
    });
});