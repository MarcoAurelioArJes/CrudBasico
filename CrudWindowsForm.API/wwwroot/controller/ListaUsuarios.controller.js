sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/model/json/JSONModel",
    "sap/m/MessageToast",
    "../validator/mensagensDeErro"
], function(Controller, JSONModel, MessageToast, mensagensDeErro) {
    "use strict";

    return Controller.extend("crudBasico.controller.ListaUsuarios", {
        onInit: function() {
            let rotas = this.getOwnerComponent().getRouter();
            rotas.getRoute("ListaUsuarios").attachPatternMatched(this.criaModelo, this);
        },
        aoClicarEmCadastrar: function() {
            let rotas = this.getOwnerComponent().getRouter();
            rotas.navTo("Cadastrar", {}, true);
        },
        aoClicarNoItemDaLista: function(event) {
            let idUsuario = event.getSource().getBindingContext("usuarios").getProperty("id");
            let rotas = this.getOwnerComponent().getRouter();
            
            rotas.navTo("Detalhes", {
              caminhoDaListaDeUsuarios: window.encodeURIComponent(idUsuario)
            });
        },
        criaModelo: async function() {
            try {
                let respostaHttp = await fetch("https://localhost:7150/api/Usuario", {method: "GET"});
                let respostaBody = await respostaHttp.json(); 
                
                if (!respostaHttp.ok) mensagensDeErro.mensagensDeErro(respostaBody.value);
                    
                let modelo = new JSONModel({
                    listaUsuarios: respostaBody
                });
    
                this.getOwnerComponent().setModel(modelo, "usuarios");
            } catch (err) {
                MessageToast.show(err.message);
            }
        },
        pesquisarUsuario: async function(event) {
            try {
                let consulta = event.getParameter("query");
                if (consulta) {
                    let respostaHttp = await fetch(`https://localhost:7150/api/Usuario/pesquisa/?consulta=${consulta}`, 
                    {method: "GET"});
                    let respostaBody = await respostaHttp.json(); 

                    if (respostaBody.length === 0) mensagensDeErro.mensagensDeErro("Usuário não encontrado");

                    if (!respostaHttp.ok) mensagensDeErro.mensagensDeErro(respostaBody.value);
                    
                    let modelo = new JSONModel({
                        listaUsuarios: respostaBody
                    });
    
                    this.getOwnerComponent().setModel(modelo, "usuarios");
                } else {
                    this.criaModelo();
                }
            } catch (err) {
                MessageToast.show(err.message);
            }
        }
    })
});