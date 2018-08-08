using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.AspNetCore.Mvc;
using UniVerseDotNetCore.Domain.CssServiceLayer;
using UniVerseDotNetCore.Domain.Models;
using UniVerseDotNetCore.Models;

namespace UniVerseDotNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomGetListController : ControllerBase
    {
        // GET: api/CustomGetList
        [HttpGet]
        public CustomGetList Get()
        {
            var acts = new List<AccountModel>() {new AccountModel("12345678911"), new AccountModel("212345678901")};
            var accounts = new CustomGetList {Accounts = acts};
            return accounts;
        }
 

        // POST: api/CustomGetList
        [HttpPost]
        public ActionResult Post([FromBody] CustomGetList model)
        {
            var usernameSplit = model.Credentials.User.Split("\\");
            var username = usernameSplit.Length == 2 ? usernameSplit[1].ToUpper() : "unknown";

            var listname = $"{Utils.GetRandomString()}";
            var name = new AccountList() { AccountListName = listname };

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
                return new JsonResult(new { ListName = name.ToString(), FilterResultLog = results });
            }

            CssCommandResult data = FilterCapability.MakeCustomGetList(model.Accounts, name, model.Credentials);
            return new JsonResult(new { ListName = name.ToString(), FilterResultLog = data });

        }


    }
}
