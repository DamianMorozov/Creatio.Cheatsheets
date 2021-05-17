// Using logging in business process.
var userConnection = Get<UserConnection>("UserConnection");
var log = new Terrasoft.Configuration.IntegrationLog(userConnection);
log.SetDefColumnValues();
log.Description = $"Script start";
log.DirectionId = new Guid("A3F528A7-8A23-4435-8E3F-B2B84DF4EA3E");
log.ResultId = new Guid("00FC0D2C-6325-4ABC-AB97-90CABFB064E6");
log.Save(false);
return true;
