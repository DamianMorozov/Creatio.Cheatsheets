// ------------------------------------------------------------------------------------------------------------------------
// showConfirmationDialog
// https://community.terrasoft.ru/questions/showconfirmationdialog-localizablestring
// ------------------------------------------------------------------------------------------------------------------------

define("LeadPageV2", ["LeadPageV2Resources"], function(resources) {
  
var ButtonDoItCfg= {
  "className": "Terrasoft.Button",
  "returnCode": "ButtonDoIt",
  "style": "green",
  "caption": { "bindTo": "Resources.Strings.ButtonDoItCaption" }
};
var cfg = {
  style: Terrasoft.MessageBoxStyles.BLUE
};
this.showConfirmationDialog( cs1 + " " + cs2,
function getSelectedButton(returnCode) {
  if (returnCode === Terrasoft.MessageBoxButtons.YES.returnCode) {
    var args = {
      sysProcessName: "ButtonDoItProcessCode",
      parameters: {
      BPcs1: csID1,
      BPcs2: csID2
    }
  };
  ProcessModuleUtilities.executeProcess(args);
}
}, ["yes", "no", ButtonDoItCfg], cfg);
