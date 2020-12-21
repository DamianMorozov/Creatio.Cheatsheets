// ------------------------------------------------------------------------------------------------------------------------
// EntitySchemaQuery. Create
// ------------------------------------------------------------------------------------------------------------------------
var UserConnection = Get<UserConnection>("UserConnection");
var divisionId = Get<Guid>("ScDivision");
var parentProcessId = Get<Guid>("ParentProcessId");
var jobGroup = "ScDistributionSalesMonthPlanningProcess";

var esq = new EntitySchemaQuery(UserConnection.EntitySchemaManager, "ScRegion");
esq.AddAllSchemaColumns();
esq.Filters.Add(esq.CreateIsNotNullFilter("Contact"));
esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal, "Division", divisionId));
var scRegions = esq.GetEntityCollection(UserConnection);

var tag = new Terrasoft.Configuration.ActivityTag(UserConnection);
tag.SetDefColumnValues();
tag.Name = "Заполнение плана продаж дистрибиьюции. " + Get<string>("PlanningMonth") + ". "+ Get<string>("DivisionName");
tag.Save(false);
Set("TagId", tag.Id);
Set("ScRegionCount", scRegions.Count);

foreach(var scRegion in scRegions)
{
	var parameters = new Dictionary<string, object>();
    parameters["ScRegion"] = scRegion.PrimaryColumnValue;
    parameters["ActivityTagId"] = tag.Id;
    parameters["ScRegionCount"] = scRegions.Count;
    parameters["ParentProcessId"] = parentProcessId;
    Terrasoft.Core.Scheduler.AppScheduler.ScheduleImmediateProcessJob(jobGroup + Guid.NewGuid().ToString(), jobGroup,
    	"ScDistributionSalesPlanningRMProcess", UserConnection.Workspace.Name, UserConnection.CurrentUser.Name, parameters);
}
return true;
