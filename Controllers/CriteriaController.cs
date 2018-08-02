using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UniVerseDotNetCore.Domain.Models;


namespace UniVerseDotNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CriteriaController : ControllerBase
    {

        [HttpGet]
        public ActionResult<List<Criterion>> GetMaintFileModel()
        {
            //Developer – multiple developer codes
            //Project – multiple developer codes
            //Lender – multiple developer codes
            //Active / Inactive / All
            //Account code
            //Days delinquent
            //Credit score

            var criterion = new List<Criterion>
            {
                new Criterion() {Attribute = "DEV.CODE", Filter = "1075"},
                new Criterion() {Attribute = "PROJ.CODE", Filter = "1076"},
                new Criterion() {Attribute = "AC", Filter = "N"},
                new Criterion() {Attribute = "DAYS.DELINQ", Filter = "43"},
                new Criterion() {Attribute = "CREDIT.SCORE", Filter = "700"}
            };

            return criterion;
        }

    }
}
