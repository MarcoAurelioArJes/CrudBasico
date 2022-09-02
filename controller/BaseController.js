sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/core/Fragment"
], function (Controller) {
    "use strict";
    
    return Controller.extend("webapps.walkthrough.controller.BaseController", {
        onOpenDialog : function () {
            if (!this.pDialog) {
                this.pDialog = this.loadFragment({
                    name: "webapps.walkthrough.view.HelloDialog"
                });
            } 
            this.pDialog.then(function(oDialog) {
                oDialog.open();
            });
        }
    })
});