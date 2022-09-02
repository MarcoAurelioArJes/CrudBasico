sap.ui.define([
	"sap/ui/core/util/MockServer",
	"sap/base/util/UriParameters"
], function (MockServer, UriParameters) {
	"use strict";

	return {
		init: function () {
			var oMockServer = new MockServer({
				rootUri: "https://services.odata.org/V2/Northwind/Northwind.svc/"
			});

			var oUriParameters = new UriParameters(window.location.href);

			MockServer.config({
				autoRespond: true,
				autoRespondAfter: oUriParameters.get("serverDelay") || 500
			});

			var sPath = sap.ui.require.toUrl("webapps/walkthrough/localService");
			oMockServer.simulate(sPath + "/metadata.xml", sPath + "/mockdata");

			oMockServer.start();
		}
	};
});
