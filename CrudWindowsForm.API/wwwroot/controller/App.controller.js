sap.ui.define([
    "sap/ui/core/mvc/Controller",
], function(Controller) {

    return Controller.extend("crudBasico.controller.App", {
        onInit: function () {
			this.getView().addStyleClass(this.getOwnerComponent().getContentDensityClass());
		}
    });
});