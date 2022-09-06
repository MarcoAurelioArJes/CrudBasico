sap.ui.define([
    "./ServicoParaApi",
    "../validator/validaCampos",
    "../validator/mensagensDeErro"
], function UsuarioRepositorio(ServicoParaApi, validaCampos, mensagensDeErro) {    
    return {
        async criar(objetoUsuario) {
            let respostaHttp = await ServicoParaApi.requisicao({verboHttp: "POST", body: objetoUsuario})

            let resultado = await ServicoParaApi.resposta(respostaHttp);

            if (!respostaHttp.ok) {
            validaCampos.defineCampoDeErroDaApi.bind(this)({nomePropriedade: resultado.value.nomePropriedade, 
                mensagem: resultado.value.mensagemErro});
            }
        },
        async listar() {
            let respostaHttp = await ServicoParaApi.requisicao({verboHttp: "GET"})
            
            let respostaBody = await ServicoParaApi.resposta(respostaHttp);
            
            if (!respostaHttp.ok) mensagensDeErro.mensagensDeErro(respostaBody.value);

            return respostaBody;
        },
        async pesquisar(consulta) {
            let i18n = this.getOwnerComponent().getModel("i18n").getResourceBundle();

            let respostaHttp = await ServicoParaApi.requisicao({parametroUrl: `pesquisa/?consulta=${consulta}`, verboHttp: "GET"});
            let respostaBody = await ServicoParaApi.resposta(respostaHttp);

            if (respostaBody.length === 0) mensagensDeErro.mensagensDeErro(i18n.getText("Mensagem.UsuarioNaoEncontrado"));

            if (!respostaHttp.ok) mensagensDeErro.mensagensDeErro(respostaBody.value);
            
            return respostaBody;
        },
        async obterPorId(id) {
            let respostaHttp = await ServicoParaApi.requisicao({parametroUrl: id, verboHttp: "GET"});
            
            if (!respostaHttp.ok) mensagensDeErro.mensagensDeErro(dadosRetornados.value);

            let resultado = await ServicoParaApi.resposta(respostaHttp);      
            
            return resultado;
        },
        async atualizar(objetoUsuario) {
            let respostaHttp = await ServicoParaApi.requisicao({verboHttp: "PUT", body: objetoUsuario})

            let resultado = await ServicoParaApi.resposta(respostaHttp);
            
            if (!respostaHttp.ok) {
                validaCampos.defineCampoDeErroDaApi.bind(this)({nomePropriedade: resultado.value.nomePropriedade, 
                    mensagem: resultado.value.mensagemErro});
            }
        },
        async deletar(id) {
            return await ServicoParaApi.requisicao({parametroUrl: id, verboHttp: "DELETE"});
        }
    }
})