QUnit.config.autostart = false;
sap.ui.getCore().attachInit(function () {
	"use strict";

	sap.ui.require([
		"webapps/walkthrough/test/integration/NavigationJourney"
	], function () {
		QUnit.start();
	});
});