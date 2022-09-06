sap.ui.define([
    "sap/ui/core/format/DateFormat",
],
function(DateFormat) {
    return {
          retornaDataFormatada: function (data) {
            return DateFormat.getDateInstance({pattern: "yyyy-MM-dd"})
            .format(new Date(data));
          }
    }
})