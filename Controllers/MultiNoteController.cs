using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniVerseDotNetCore.Domain.CssServiceLayer;
using UniVerseDotNetCore.Domain.Models;
using UniVerseDotNetCore.Models;

namespace UniVerseDotNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MultiNoteController : ControllerBase
    {
        // GET: api/MultiNote
        [HttpGet]
        public JsonResult  Get()
        {
            var note = new MultiNoteModel();
            return new JsonResult(note);
        }
 
        // POST: api/MultiNote
        [HttpPost]
        public JsonResult Post([FromBody] MultiNoteModel model)
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
            CssCommandResult data = MultiNoteCapability.MakeNote(model.GetList,model.Note,model.Credentials);
            return new JsonResult( data);

        }


    }
}
