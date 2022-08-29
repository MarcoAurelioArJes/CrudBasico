sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/model/json/JSONModel",
    "sap/m/MessageToast",
], function(Controller, JSONModel, MessageToast) {
    "use strict";

    return Controller.extend("crudBasico.controller.ListaUsuarios", {
        onInit: function() {
            
            let rotas = this.getOwnerComponent().getRouter();
            rotas.getRoute("ListaUsuarios").attachPatternMatched(this.criaModelo, this);
        },
        aoClicarEmCadastrar: function() {
            let rotas = this.getOwnerComponent().getRouter();
            rotas.navTo("Cadastrar");
        },
        aoClicarNoItemDaLista: function(event) {
            let idUsuario = event.getSource().getBindingContext("usuarios").getProperty("id");
            let rotas = this.getOwnerComponent().getRouter();
            
            rotas.navTo("Detalhes", {
              caminhoDaListaDeUsuarios: window.encodeURIComponent(idUsuario)
            });
        },
        criaModelo: function() {
            fetch("https://localhost:7150/api/Usuario", {
                method: "GET"
            })
            .then(async res => {
                let respostaHttp = res;
                let respostaBody = await res.json();
                if (respostaHttp.ok) {
                    let modelo = new JSONModel({
                        listaUsuarios: respostaBody
                    })
                    this.getOwnerComponent().setModel(modelo, "usuarios")
                    
                } else {
                    MessageToast.show(resultado.value);
                }
            })
        }
    })
});