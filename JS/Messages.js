// ------------------------------------------------------------------------------------------------------------------------
// Messages
// ------------------------------------------------------------------------------------------------------------------------
// Publish
// ---------
messages: {
  "refreshProductData": { mode: Terrasoft.MessageMode.PTP, direction: Terrasoft.MessageDirectionType.PUBLISH },
},
// method 
  scope.sandbox.publish("refreshProductData");
// ------------------------------------------------------------------------------------------------------------------------
// Subscribe
// ---------
messages: {
  "refreshProductData": { mode: Terrasoft.MessageMode.PTP, direction: Terrasoft.MessageDirectionType.SUBSCRIBE },
},
onEntityInitialized: function() {
  this.callParent(arguments);
  // Subscribers.
  this.sandbox.subscribe("refreshProductData", this.refreshProductData, this);
},
refreshProductData: function () {
  this.reloadEntity();
},
// ------------------------------------------------------------------------------------------------------------------------
