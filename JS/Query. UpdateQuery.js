// ------------------------------------------------------------------------------------------------------------------------
// UpdateQuery
// ------------------------------------------------------------------------------------------------------------------------

getEsqProductUnit: function () {
  try {
    let esq = this.Ext.create("Terrasoft.EntitySchemaQuery", { rootSchemaName: "ProductUnit" });
    esq.addColumn("Id");
    esq.addColumn("Product");
    esq.addColumn("Unit");
    esq.addColumn("IsBase");
    esq.addColumn("NumberOfBaseUnits");
    esq.filters.add("filterProduct", this.Terrasoft.createColumnFilterWithParameter(
      this.Terrasoft.ComparisonType.EQUAL, "Product.Id", this.$Product.value));
    esq.filters.add("filterUnit", this.Terrasoft.createColumnFilterWithParameter(
      this.Terrasoft.ComparisonType.EQUAL, "Unit.Id", this.$Unit.value));
    esq.filters.add("filterIsBase", this.Terrasoft.createColumnFilterWithParameter(
      this.Terrasoft.ComparisonType.EQUAL, "IsBase", 1));
    return esq;
  } catch (e) {
    window.console.log("Error getEsqProductUnit!");
    window.console.log(e.message);
    throw e;
  }
},
getUqProduct: function (item) {
  try {
    let uq = Ext.create("Terrasoft.UpdateQuery", { rootSchemaName: "Product" });
    uq.enablePrimaryColumnFilter(item.$Product.value);
    uq.setParameterValue("Unit", item.$Unit.value, Terrasoft.DataValueType.GUID);
    return uq;
  } catch (e) {
    window.console.log("Error getUqProduct!");
    window.console.log(e.message);
    throw e;
  }
},
calcProductBaseUnit: function(callback) {
  if (this.isEmpty(this.$Product))
    return;
  try {
    let esq = this.getEsqProductUnit();
    esq.getEntityCollection(function (result) {
      let scope = this;
      if (result.success) {
        result.collection.each(function (item) {
          let uq = scope.getUqProduct(item);
          uq.execute(function (result) {
            if (result.success) {
              //
            }
          }, this);
        });
      } else {
        callback.call(this);
      }
    }, this);
  } catch (e) {
    window.console.log("Error calcProductBaseUnit!");
    window.console.log(e.message);
    throw e;
  }
},
