using Microsoft.AspNetCore.Mvc;
using UniVerseDotNetCore.Domain.CssServiceLayer;
using UniVerseDotNetCore.Domain.CssServiceLayer.Models;
using UniVerseDotNetCore.Domain.Models;


namespace UniVerseDotNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractsFilterController : ControllerBase
    {
      
        [HttpGet]
        public JsonResult GetContractsFileModel()
        {
            var filtrCriteria = new AccountFilterCriteria<Contracts>();
            filtrCriteria.AddCriterion(new Criterion() { Attribute = "DEV.CODE", Filter = "1075" });
            var credentials = new CssCredentialsModel();
            var model = new ContractsFilterCallModel(){Credentials = credentials,FilterCriteria = filtrCriteria};
            return new JsonResult(model);
        }
    
        [HttpPost] 
        public JsonResult ReturnContractsFile([FromBody] ContractsFilterCallModel model)
        {
            var usernameSplit = model.Credentials.User.Split("\\");
            var username = usernameSplit.Length == 2 ? usernameSplit[1].ToUpper() : "unknown";

            var listname = $"{username}.{model.FilterCriteria.CssFileName.ToString().ToUpper()}.{Utils.GetRandomString()}";
            var name = new AccountList(){AccountListName = listname};
            var data = FilterCapability.FilterContracts(model.FilterCriteria, name, model.Credentials);

            return new JsonResult(new {ListName = name.ToString(), FilterResultLog = data});
           

        }

       
    }
}
