// ------------------------------------------------------------------------------------------------------------------------
// InsertQuery
// https://academy.terrasoft.ru/documents/technic-sdk/7-16/dataservice-sozdanie-zapisi-primer
// ------------------------------------------------------------------------------------------------------------------------

getIqProduct: function () {
  try {
    let iq = this.Ext.create("Terrasoft.InsertQuery", { rootSchemaName: "Product" });
    iq.setParameterValue("Name", "Product name", Terrasoft.DataValueType.TEXT);
    return iq;
  } catch (e) {
    window.console.log("Error getIqProduct!");
    window.console.log(e.message);
    throw e;
  }
},

calcProduct: function(callback) {
  if (this.isEmpty(this.$Product))
    return;
  try {
    let iq = this.getIqProduct();
    iq.execute(function(result) {
      window.console.log(result);
      let scope = this;
      if (result.success) {
        //
      } else {
        callback.call(this);
      }
    }, this);
  } catch (e) {
    window.console.log("Error calcProduct!");
    window.console.log(e.message);
    throw e;
  }
},
