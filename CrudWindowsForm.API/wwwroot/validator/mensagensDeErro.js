sap.ui.define([
    "sap/ui/model/ValidateException",
],
function(ValidateException) {
    return {
        mensagensDeErro: function ({input, mensagem}) {
            let i18n = this.getView().getModel("i18n").getResourceBundle();
            
            input.setValueState("Error");
            input.setValueStateText(mensagem);
            throw new ValidateException(i18n.getText("AvisoSobreCamposObrigatorios"));
          }
    }
})