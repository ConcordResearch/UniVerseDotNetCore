using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UniVerseDotNetCore.CSSLayer;
using UniVerseDotNetCore.Models;

namespace UniVerseDotNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountlistController : ControllerBase
    {

        [HttpGet]
        public string GetReturnAccountListModel()
        {
            return JsonConvert.SerializeObject(new CssCallModel());
        }
    
        [HttpPost]
        public ActionResult<string> ReturnFile(CssCallModel model)
        {
            var data = CssCaller.GetFileData(model.CssDataFile, model.Credentials);
            Console.WriteLine(data);
            return data;
        }

       
    }
}
