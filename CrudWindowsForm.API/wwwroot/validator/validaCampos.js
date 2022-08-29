sap.ui.define([
    "sap/ui/model/ValidateException",
    "./mensagensDeErro",
    "sap/ui/core/format/DateFormat",
], function (ValidateException, mensagensDeErro, DateFormat) {
    "use strict";
    return {
      validaData: function (data) {
        let i18n = this.getView().getModel("i18n").getResourceBundle();

        let instanciaData = new Date(data.getValue());

        let dataMinima = data.setMinDate(new Date(1929, 1, 1));
        let dataMaxima = data.setMaxDate(new Date());
        
        let formatador = DateFormat.getDateInstance({pattern: "dd/MM/yyyy"});
        
        if ((instanciaData < data.getMinDate() || instanciaData > data.getMaxDate())) {
          let mensagem = i18n.getText("AvisoCampoDataNascimentoDataMaiorOuMenor", 
                          [formatador.format(new Date(dataMinima.getMinDate())), 
                          formatador.format(new Date(dataMaxima.getMaxDate()))]);

          throw new ValidateException(mensagem);
        } else if (!data.isValidValue(data)) {
          mensagensDeErro.mensagensDeErro.bind(this)({input: data, mensagem: i18n.getText("AvisoCampoDataNascimento")});
        }
        data.setValueState("None");

        return data.getValue();  
      },
      validaEmail: function (email) {
        let valorDoInput = email.getValue();

        let i18n = this.getView().getModel("i18n").getResourceBundle();

        let regexEmail = /^\w+[\w-+\.]*\@\w+([-\.]\w+)*\.[a-zA-Z]{2,}$/;
        if (!valorDoInput.match(regexEmail)) {
          mensagensDeErro.mensagensDeErro.bind(this)({input: email, mensagem: `${valorDoInput} ${i18n.getText("AvisoCampoEmail")}`});
        }
        email.setValueState("None");

        return valorDoInput;
      },
      validaCampoGenerico: function(input) {
        let valorDoInput = input.getValue();
        let nomeInput = input.getName();
        console.log("teste")
        let i18n = this.getView().getModel("i18n").getResourceBundle();
        if (valorDoInput.length === 0 || valorDoInput === undefined) {
          mensagensDeErro.mensagensDeErro.bind(this)({input, mensagem: `${nomeInput} ${i18n.getText("AvisoCampoGenerico")}`});
        }

        input.setValueState("None");
        return valorDoInput;
      }
    }
});