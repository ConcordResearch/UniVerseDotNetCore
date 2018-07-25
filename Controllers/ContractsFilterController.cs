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
    public class ContractsFilterController : ControllerBase
    {
      
        [HttpGet]
        public string GetContractsFileModel()
        {
            var filtrCriteria = new AccountFilterCriteria<Contracts>();
            filtrCriteria.AddCriterion(new Criterion() { Attribute = "DEV.CODE", Filter = "1075" });
            var credentials = new CssCredentialsModel();
            var model = new ContractsFilterCallModel(){Credentials = credentials,FilterCriteria = filtrCriteria};
            return JsonConvert.SerializeObject(model);
        }
    
        [HttpPost] 
        public ActionResult<string> ReturnContractsFile([FromBody] ContractsFilterCallModel model)
        {
            var name = new AccountList(){AccountListName = CssCaller.GetRandomString()};
            var data = CssCaller.FilterContracts(model.FilterCriteria, name, model.Credentials);

            return JsonConvert.SerializeObject(new {ListName = name.ToString(), FilterLog = data});
           

        }

       
    }
}
