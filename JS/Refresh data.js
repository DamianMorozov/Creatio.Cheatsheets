// ------------------------------------------------------------------------------------------------------------------------
// Refresh data.
// ------------------------------------------------------------------------------------------------------------------------
this.refreshGridData();
this.reloadEntity();
// Обновить реестр.
this.reloadGridData();
this.updateSection();
// Перезагрузить деталь.
this.updateDetail({detail: "SupplyPayment"});
// Обновлить все детали.
this.updateDetail({realoadAll:true});
// Обновить карточку сущности.
this.reloadEntity();
this.loadLookupDisplayValue("Type", constants.ContractType.Standard);
------------------------------------------------------------------------------------------------------------------------
// Деталь.
this.reloadEntity();
// Перезагрузка детали.
this.updateDetail({ detail: "SupplyPayment" });
this.updateDetail({ detail: "ProductUnitDetail", reloadAll: true });
this.updateDetail({ primaryColumnValue: "SomeRecordId" });
------------------------------------------------------------------------------------------------------------------------
// Обновление всех деталей.
// all-combined.js:41 Detail not found: undefined
this.updateDetail({ realoadAll:true });
------------------------------------------------------------------------------------------------------------------------
