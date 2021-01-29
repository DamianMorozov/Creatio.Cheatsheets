// ------------------------------------------------------------------------------------------------------------------------
// Messages
// https://www.youtube.com/watch?v=ZvCQsO_MQQE&t=29s
// ------------------------------------------------------------------------------------------------------------------------
// Publish
// ---------
messages: {
  "msgRefreshData": { mode: Terrasoft.MessageMode.PTP, direction: Terrasoft.MessageDirectionType.PUBLISH },
},
init: function() {
  this.callParent(arguments);
  // Variant 1.
  var pageInfo = this.sandbox.publish("msgRefreshData", "getThis", [this.sandbox.id]);
  pageInfo.refreshData();
  // Variant 2.
  // this.sandbox.publish("msgRefreshData", "refreshData", [this.sandbox.id]);
},
// ------------------------------------------------------------------------------------------------------------------------
// Subscribe
// ---------
messages: {
  "msgRefreshData": { mode: Terrasoft.MessageMode.PTP, direction: Terrasoft.MessageDirectionType.SUBSCRIBE },
},
init: function() {
  this.callParent(arguments);
  this.sandbox.subscribe("msgRefreshData", function(item) { 
    if (item === "getThis")
      return this;
    if (item === "refreshData")
      return this.refreshData();
    return null;
  }, this, ["SectionModuleV2_ContactSectionV2"]);
},
refreshData: function () {
  this.reloadEntity();
},
// ------------------------------------------------------------------------------------------------------------------------
