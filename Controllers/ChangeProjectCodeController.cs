using Microsoft.AspNetCore.Mvc;
using UniVerseDotNetCore.Domain.CssServiceLayer;
using UniVerseDotNetCore.Domain.CssServiceLayer.Models;
using UniVerseDotNetCore.Domain.Models;
using UniVerseDotNetCore.Models;

namespace UniVerseDotNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChangeProjectCodeController : ControllerBase
    {
        [HttpGet]
        public JsonResult GetProjectCodeModel()
        {
            var sample = new ChangeItRequest<ProjectCode>
            {
                Note = new Note("This is a note"),
                Credentials = new CssCredentialsModel(),
                File = new Contracts(),
                GetListName = new AccountList("NameOfAccountList"),
                TypeOfChange = "ProjectCode",
                NewCode = new ProjectCode("1075")

            };

            return new JsonResult(sample);
        }
      
        [HttpPost]
        public JsonResult ChangeProjectCodeModel([FromBody] ChangeItRequest<ProjectCode> request)
        {
                var response = ChangeItCapability.ChangeProjectCode(request.File, request.GetListName, request.NewCode, request.Note, request.Credentials);
            return new JsonResult(response);
        }
        
    }
}