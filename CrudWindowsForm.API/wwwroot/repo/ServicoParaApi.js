sap.ui.define([], 
    function () {
        let baseUrl = "https://localhost:7150/api/Usuario";

        return {
            requisicao: async function ({parametroUrl = "", verboHttp, body = {}}) {
                let corpoRequisicao = {
                    method: verboHttp,
                    headers: {
                        "Content-Type": "application/json"
                    }
                }

                if (Object.keys(body).length > 0) corpoRequisicao.body = JSON.stringify(body);
                
                let respostaHttp = await fetch(`${baseUrl}/${parametroUrl}`, corpoRequisicao);
                return respostaHttp;
            },
            resposta: async function (requisicao) {
                return requisicao.headers.get("content-type") !== null ? await requisicao.json() : null;
            }
        }
});