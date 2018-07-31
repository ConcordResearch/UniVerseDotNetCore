using Microsoft.AspNetCore.Mvc;
using UniVerseDotNetCore.Domain.CssServiceLayer;
using UniVerseDotNetCore.Domain.CssServiceLayer.Models;
using UniVerseDotNetCore.Domain.Models;


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
            var data = CssCaller.GetData(model.CssDataFile,model.File, model.Credentials);
 
            return new JsonResult(data);
        }

       
    }
}
