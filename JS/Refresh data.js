// ------------------------------------------------------------------------------------------------------------------------
// Refresh data
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
this.updateDetail({detail: "SupplyPayment"});
this.updateDetail({ detail: "ProductUnitDetail", reloadAll: true });
// Обновление всех деталей.
this.updateDetail({realoadAll:true});
------------------------------------------------------------------------------------------------------------------------
