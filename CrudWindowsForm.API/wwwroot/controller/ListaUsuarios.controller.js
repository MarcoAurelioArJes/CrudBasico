sap.ui.define([
    "./BaseController",
    "sap/ui/model/json/JSONModel",
    "sap/m/MessageToast",
    "../repo/UsuarioRepositorio"
], function(BaseController, JSONModel, MessageToast, UsuarioRepositorio) {
    "use strict";

    return BaseController.extend("crudBasico.controller.ListaUsuarios", {
        onInit: function() {
            let rotas = this.getOwnerComponent().getRouter();
            
            rotas.getRoute("ListaUsuarios").attachPatternMatched(this.listarTodos, this);
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
        listarTodos: async function() {
            try {
                this.criaModelo(await UsuarioRepositorio.listar(), "usuarios");
            } catch (err) {
                MessageToast.show(err.message);
            }
        },
        pesquisarUsuario: async function(event) {
            try {
                let consulta = event.getParameter("query");

                if (consulta) {
                    this.criaModelo(await UsuarioRepositorio.pesquisar.bind(this)(consulta), "usuarios");
                } else {
                    this.listarTodos();
                }
            } catch (err) {
                MessageToast.show(err.message);
            }
        }
    })
});