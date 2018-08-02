using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UniVerseDotNetCore.Domain.CssServiceLayer;
using UniVerseDotNetCore.Domain.CssServiceLayer.Models;
using UniVerseDotNetCore.Domain.Models;
using UniVerseDotNetCore.Models;

namespace UniVerseDotNetCore.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class ContractsFilterController : ControllerBase {

        [HttpGet]
        public JsonResult GetContractsFileModel () {
            var filtrCriteria = new AccountFilterCriteria<Contracts> ();
            filtrCriteria.AddCriterion (new Criterion () { Attribute = "DEV.CODE", Filter = "6600" });
            filtrCriteria.AddCriterion (new Criterion () { Attribute = "PROJ.CODE", Filter = "6606" });
            filtrCriteria.AddCriterion (new Criterion () { Attribute = "LEND.CODE", Filter = "100" });
            filtrCriteria.AddCriterion (new Criterion () { Attribute = "AC", Filter = "N" });

            var credentials = new CssCredentialsModel ();
            var model = new ContractsFilterCallModel () { Credentials = credentials, FilterCriteria = filtrCriteria };
            return new JsonResult (model);
        }

        [HttpPost]
        public JsonResult ReturnContractsFile ([FromBody] ContractsFilterCallModel model) {

            var usernameSplit = model.Credentials.User.Split ("\\");
            var username = usernameSplit.Length == 2 ? usernameSplit[1].ToUpper () : "unknown";

            var listname = $"{username}.{model.FilterCriteria.CssFileName.ToString().ToUpper()}.{Utils.GetRandomString()}";
            var name = new AccountList () { AccountListName = listname };

            if (CssAppConfig.RunInTestMode) {
                var results = new CssCommandResult () {
                    Results = new List<CommandResponse> () {
                    new CommandResponse ("sample css action", "sample css response"),
                    new CommandResponse ("sample css action", "sample css response")
                    }
                };
                return new JsonResult (new { ListName = name.ToString (), FilterResultLog = results });
            }

            CssCommandResult data = FilterCapability.FilterContracts (model.FilterCriteria, name, model.Credentials);

            return new JsonResult (new { ListName = name.ToString (), FilterResultLog = data });

        }

    }
}