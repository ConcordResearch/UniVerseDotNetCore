using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UniVerseDotNetCore.CSSLayer;
using UniVerseDotNetCore.Models;

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

            var criterion = new List<Criterion>();
            criterion.Add(new Criterion() { Attribute = "DEV.CODE", Filter = "1075" });
            criterion.Add(new Criterion() { Attribute = "PROJ.CODE", Filter = "1076" });
            criterion.Add(new Criterion() { Attribute = "AC", Filter = "N" });
            criterion.Add(new Criterion() { Attribute = "DAYS.DELINQ", Filter = "43" });
           
            return criterion;
        }

    }
}
