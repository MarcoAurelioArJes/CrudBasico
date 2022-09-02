sap.ui.define([
	"webapps/walkthrough/model/formatter",
	"sap/ui/model/resource/ResourceModel"
], function (formatter, ResourceModel) {
	"use strict";

	QUnit.module("Formatting functions", {
		beforeEach: function () {
			this._oResourceModel = new ResourceModel({
				bundleUrl: sap.ui.require.toUrl("webapps/walkthrough") + "/i18n/i18n.properties"
			});
		},
		afterEach: function () {
			this._oResourceModel.destroy();
		}
	});

	QUnit.test("Should return the translated texts", function (assert) {

		var oModel = this.stub();

		oModel.withArgs("i18n").returns(this._oResourceModel);
		var oViewStub = {
			getModel: oModel
		};
		
		var oControllerStub = {
			getView: this.stub().returns(oViewStub)
		};

		var statusTextTest = formatter.statusText.bind(oControllerStub);

		assert.strictEqual(statusTextTest("A"), "New", "The long text for status A is correct");

		assert.strictEqual(statusTextTest("B"), "In Progress", "The long text for status B is correct");

		assert.strictEqual(statusTextTest("C"), "Done", "The long text for status C is correct");

		assert.strictEqual(statusTextTest("Foo"), "Foo", "The long text for status Foo is correct");
	});
});