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
    public class ChangeAccountCodeController : ControllerBase
    {
        [HttpGet]
        public JsonResult GetAccountCodeModel()
        {
            var sample = new ChangeItRequest<AccountCode>
            {
                Note = new Note("This is a note"),
                Credentials = new CssCredentialsModel(),
                File = new Contracts(),
                GetListName = new AccountList("NameOfAccountList"),
                TypeOfChange = "AccountCode",
                NewCode = new AccountCode("A")

            };

            return new JsonResult(sample);
        }
      
        [HttpPost]
        public JsonResult ChangeAccountCodeModel([FromBody] ChangeItRequest<AccountCode> request)
        {
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
                return new JsonResult(results);
            }
            CssCommandResult response = ChangeItCapability.ChangeAccountCode(request.File, request.GetListName, request.NewCode, request.Note, request.Credentials);
            return new JsonResult(response);
        }
    }
}