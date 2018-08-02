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
    public class AccountlistController : ControllerBase
    {

        [HttpGet]
        public JsonResult GetReturnAccountListModel()
        {
           var file = new Contracts();
            return new JsonResult( new CssCallModel(file));
        }
    
        [HttpPost]
        public JsonResult ReturnFile([FromBody] CssCallModel model)
        {
            if (CssAppConfig.RunInTestMode)
            
            { var commandResults = new CssCommandResult()
                {
                    Results = new List<CommandResponse>()
                    {
                        new CommandResponse("sample css action","sample css response"),
                        new CommandResponse("sample css action","sample css response")
                    }
                };
                var result = new CssFileResult(){ CssResponses = commandResults, FileContents = new CssFile() };
                return new JsonResult(result);
            }
            var data = Utils.GetData(model.CssDataFile,model.File, model.Credentials);
 
            return new JsonResult(data);
        }

       
    }
}
