sap.ui.define([
    "sap/ui/model/ValidateException",
    "./mensagensDeErro",
    "sap/ui/core/format/DateFormat",
], function (ValidateException, mensagensDeErro, DateFormat) {
    "use strict";
    return {
      retornaDataValida: function (data) {
        let i18n = this.getView().getModel("i18n").getResourceBundle();

        let instanciaData = new Date(data.getValue());

        let dataMinima = data.setMinDate(new Date(1929, 1, 1));
        let dataMaxima = data.setMaxDate(new Date());
        
        let formatador = DateFormat.getDateInstance({pattern: "dd/MM/yyyy"});
        
        if (!data.isValidValue(data) && (instanciaData < data.getMinDate() 
            || instanciaData > data.getMaxDate())) {
            let mensagem = i18n.getText("AvisoCampoDataNascimentoDataMaiorOuMenor", 
                            [formatador.format(new Date(dataMinima.getMinDate())), 
                            formatador.format(new Date(dataMaxima.getMaxDate()))]);
          mensagensDeErro.mensagensDeErroParaOsCampos.bind(this)({input: data, mensagem: mensagem});

        } else if (!data.isValidValue(data)) {
          mensagensDeErro.mensagensDeErroParaOsCampos.bind(this)({input: data, mensagem: i18n.getText("AvisoCampoDataNascimento")});
        }
        data.setValueState("None");

        return data.getValue();  
      },
      retornaEmailValido: function (email) {
        let valorDoInput = email.getValue();

        let i18n = this.getView().getModel("i18n").getResourceBundle();

        let regexEmail = /^\w+[\w-+\.]*\@\w+([-\.]\w+)*\.[a-zA-Z]{2,}$/;
        if (!valorDoInput.match(regexEmail)) {
          mensagensDeErro.mensagensDeErroParaOsCampos.bind(this)({input: email, mensagem: `${valorDoInput} ${i18n.getText("AvisoCampoEmail")}`});
        }
        email.setValueState("None");

        return valorDoInput;
      },
      retornaValorCampoGenerico: function(input) {
        let valorDoInput = input.getValue();
        let nomeInput = input.getName();

        let i18n = this.getView().getModel("i18n").getResourceBundle();
        if (valorDoInput.length === 0 || valorDoInput === undefined) {
          mensagensDeErro.mensagensDeErroParaOsCampos.bind(this)({input, mensagem: `${nomeInput} ${i18n.getText("AvisoCampoGenerico")}`});
        }

        input.setValueState("None");
        return valorDoInput;
      },
      defineCampoDeErroDaApi: function({nomePropriedade, mensagem}) {
        let campo = this.byId(`campo${nomePropriedade}`);
        mensagensDeErro.mensagensDeErroParaOsCampos.bind(this)({input : campo,
                                                   mensagem: mensagem});

        campo.setValueState("None");
      }
    }
});