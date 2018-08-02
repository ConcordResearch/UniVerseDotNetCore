using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UniVerseDotNetCore.Domain.CssServiceLayer;
using UniVerseDotNetCore.Domain.CssServiceLayer.Models;
using UniVerseDotNetCore.Domain.Models;
using UniVerseDotNetCore.Models;


namespace UniVerseDotNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintFilterController : ControllerBase
    {
       
        [HttpGet]
        public ActionResult<MaintFilterCallModel> GetMaintFileModel()
        {
            var filtrCriteria = new AccountFilterCriteria<Maint>();
            filtrCriteria.AddCriterion(new Criterion() { Attribute = "DEV.CODE", Filter = "1075" });
            var credentials = new CssCredentialsModel();
            var model = new MaintFilterCallModel(){Credentials = credentials,FilterCriteria = filtrCriteria};
            return model;
        }
    
        [HttpPost] 
        public ActionResult<string> ReturnMaintFile([FromBody] MaintFilterCallModel model)
        {
            var usernameSplit = model.Credentials.User.Split("\\");
            var username = usernameSplit.Length == 2 ? usernameSplit[1].ToUpper() : "unknown";

            var listname = $"{username}.{model.FilterCriteria.CssFileName.ToString().ToUpper()}.{Utils.GetRandomString()}";
            var name = new AccountList(){AccountListName = listname};

            if (CssAppConfig.RunInTestMode)
            {
                var results = new CssCommandResult()
                {
                    Results = new List<CommandResponse>()
                    {
                        new CommandResponse("sample css action","sample css response"),
                        new CommandResponse("sample css action","sample css response")
                    }
                };
                return new JsonResult(new { ListName = name.ToString(), FilterResultLog = results });
            }

            CssCommandResult data = FilterCapability.FilterMaint(model.FilterCriteria, name, model.Credentials);
            return new JsonResult(new { ListName = name.ToString(), FilterResultLog = data });


        }


    }
}
