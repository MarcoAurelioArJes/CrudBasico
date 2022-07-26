sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/core/routing/History",
    "sap/ui/model/json/JSONModel",
    "sap/m/MessageToast",
    "../constantes/constantesDeRotas"
], function(Controller, History, JSONModel, MessageToast, constantesDeRotas) {
    "use strict";

    return Controller.extend("crudBasico.controller.BaseController", {
        constanteRotas: constantesDeRotas,
        
        objetoDeOpcoesMessageToast: {
            animationDuration: 5000,
            closeOnBrowserNavigation: false
        },
        
        aoClicarEmVoltar: function () {
            let historico = History.getInstance();
            let rotaAnterior = historico.getPreviousHash();
    
            if (rotaAnterior !== undefined) {
              window.history.go(-1);
            } else {
              let rotas = this.getOwnerComponent().getRouter();
              rotas.navTo(this.constanteRotas.ROTA_LISTAR, {}, true);
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