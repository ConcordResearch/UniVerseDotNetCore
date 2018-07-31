using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
                Credentials = new CssCredentialsModel(),
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
            var response = CssCaller.ChangeLenderCode(request.File, request.GetListName,request.NewCode,request.Note, request.Credentials);
            return new JsonResult(response);
        }
     
    }
}