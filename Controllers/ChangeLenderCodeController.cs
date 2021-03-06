﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UniVerseDotNetCore.Models;
using UniVerseDotNetCore.Domain.CssServiceLayer;
using UniVerseDotNetCore.Domain.CssServiceLayer.Models;
using UniVerseDotNetCore.Domain.Models;


namespace UniVerseDotNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChangeLenderCodeController : ControllerBase
    {
        [HttpGet]
        public JsonResult GetChangeLenderModel()
        {
            var sample = new ChangeItRequest<LenderCode>
            {
                Note = new Note("This is a note"),
                Credentials = new CssCredentials(),
                File = new Contracts(),
                GetListName = new AccountList("NameOfAccountList"),
                TypeOfChange = "LenderCode",
                NewCode = new LenderCode("100")

            };

            return new JsonResult(sample);
        }
        [HttpPost]
        public JsonResult ChangeLenderModel([FromBody] ChangeItRequest<LenderCode> request)
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
            CssCommandResult response = ChangeItCapability.ChangeLenderCode(request.File, request.GetListName,request.NewCode,request.Note, request.Credentials);
            return new JsonResult(response);
        }
     
    }
}