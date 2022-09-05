sap.ui.define([
    "./BaseController",
], function(Controller) {

    return Controller.extend("crudBasico.controller.App", {
        onInit: function () {
			this.getView().addStyleClass(this.getOwnerComponent().getContentDensityClass());
		}
    });
});