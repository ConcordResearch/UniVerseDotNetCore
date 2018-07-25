using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UniVerseDotNetCore.CSSLayer;
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
            var name = new AccountList(){AccountListName = CssCaller.GetRandomString()};
            var data = CssCaller.FilterMaint(model.FilterCriteria, name, model.Credentials);

            return JsonConvert.SerializeObject(new {ListName = name.ToString(), FilterLog = data});
           

        }

       
    }
}
