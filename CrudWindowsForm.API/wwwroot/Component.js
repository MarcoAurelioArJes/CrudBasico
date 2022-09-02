sap.ui.define([
    "sap/ui/core/UIComponent",
    "sap/ui/Device",
    "sap/ui/model/json/JSONModel"
], function (UIComponent, Device, JSONModel) {
    "use strict";

    return UIComponent.extend("crudBasico.Component", {
        metadata: {
            interfaces: ["sap.ui.core.IAsyncContentCreation"],
            manifest: "json"
        },
        init : function () {
            UIComponent.prototype.init.apply(this, arguments);
            
            let modeloDoDispositivo = new JSONModel(Device);
            modeloDoDispositivo.setDefaultBindingMode("OneWay");
            this.setModel(modeloDoDispositivo, "device");
            
            this.getRouter().initialize();
        },
        getContentDensityClass : function () {
			if (!this._sContentDensityClass) {
				if (!Device.support.touch) {
					this._sContentDensityClass = "sapUiSizeCompact";
				} else {
					this._sContentDensityClass = "sapUiSizeCozy";
				}
			}
			return this._sContentDensityClass;
		}
    })
});