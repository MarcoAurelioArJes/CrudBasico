sap.ui.define([
    "./BaseController",
    "sap/m/MessageToast",
    "../repo/UsuarioRepositorio"
], function(BaseController, MessageToast, UsuarioRepositorio) {
    "use strict";

    let nomeModelo = "usuarios";
    return BaseController.extend("crudBasico.controller.ListaUsuarios", {
        onInit: function() {
            let rotas = this.getOwnerComponent().getRouter();
            
            rotas.getRoute(this.constanteRotas.ROTA_LISTAR).attachPatternMatched(this.listarTodos, this);
        },
        aoClicarEmCadastrar: function() {
            let rotas = this.getOwnerComponent().getRouter();
            rotas.navTo(this.constanteRotas.ROTA_CADASTRAR, {}, true);
        },
        aoClicarNoItemDaLista: function(event) {
            let idUsuario = event.getSource().getBindingContext(nomeModelo).getProperty("id");
            let rotas = this.getOwnerComponent().getRouter();
            
            rotas.navTo(this.constanteRotas.ROTA_DETALHES, {
              id: window.encodeURIComponent(idUsuario)
            });
        },
        listarTodos: async function() {
            try {
                this.criaModelo(await UsuarioRepositorio.listar(), nomeModelo);
            } catch (err) {
                MessageToast.show(err.message, this.objetoDeOpcoesMessageToast);
            }
        },
        pesquisarUsuario: async function(event) {
            try {
                let consulta = event.getParameter("query");

                if (consulta) {
                    this.criaModelo(await UsuarioRepositorio.pesquisar.bind(this)(consulta), nomeModelo);
                } else {
                    this.listarTodos();
                }
            } catch (err) {
                MessageToast.show(err.message, this.objetoDeOpcoesMessageToast);
            }
        }
    })
});