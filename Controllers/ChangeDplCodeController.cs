using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UniVerseDotNetCore.Models;
using UniVerseDotNetCore.Domain.CssServiceLayer;
using UniVerseDotNetCore.Domain.CssServiceLayer.Models;
using UniVerseDotNetCore.Domain.Models;


namespace UniVerseDotNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChangeDplCodeController : ControllerBase
    {
        [HttpGet]
        public JsonResult GetChangeDplModel()
        {
            var sample = new ChangeItRequest<DplCode>
            {
                Note = new Note("This is a note"),
                Credentials = new CssCredentials(),
                File = new Contracts(),
                GetListName = new AccountList("NameOfAccountList"),
                TypeOfChange = "DplCode",
                NewCode = new DplCode("10751075100")

            };

            return new JsonResult(sample);
        }
        [HttpPost]
        public JsonResult ChangeDplModel([FromBody] ChangeItRequest<DplCode> request)
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
            Console.WriteLine($"TESTING: {request.NewCode.GetDeveloperCode()}, {request.NewCode.GetProjectCode()}, {request.NewCode.GetLenderCode()}");
            var response = 
            ChangeItCapability.ChangeDplCode(request.File,
                                    request.GetListName,
                                    request.NewCode,
                                    request.Note,
                                    request.Credentials);

            Console.WriteLine(JsonConvert.SerializeObject(response));
            return new JsonResult(response);
        }
     
    }
}