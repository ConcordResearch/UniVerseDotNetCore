using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UniVerseDotNetCore.Domain.CssServiceLayer;
using UniVerseDotNetCore.Domain.CssServiceLayer.Models;
using UniVerseDotNetCore.Domain.Models;


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

            var listname = $"{username}.{model.FilterCriteria.CssFileName.ToString().ToUpper()}.{CssCaller.GetRandomString()}";
            var name = new AccountList(){AccountListName = listname};
            var data = CssCaller.FilterMaint(model.FilterCriteria, name, model.Credentials);
            return new JsonResult(new { ListName = name.ToString(), FilterResultLog = data });


        }


    }
}
