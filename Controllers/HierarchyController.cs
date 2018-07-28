using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UniVerseDotNetCore.CSSLayer;
using UniVerseDotNetCore.Models;

namespace UniVerseDotNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HierarchyController : ControllerBase
    {
        // Initialize the data for the demo. This should be removed if we can get the Developers, Projects and Lenders from the Universe DB
        static Dictionary<string, string> Developers = new Dictionary<string, string>()
        {
            { "0001", "Developer 0001" },
            { "0002", "Developer 0002" },
            { "0003", "Developer 0003" },
            { "0004", "Developer 0004" },
            { "0005", "Developer 0005" },
            { "0006", "Developer 0006" },
            { "0007", "Developer 0007" },
            { "0008", "Developer 0008" },
            { "0009", "Developer 0009" }
        };

        // Table containing data for dpl
        // {"0000", "010", "100"

        /// <summary>
        /// Retrieves the set of Developers that the user has access to
        /// </summary>
        /// <returns>Json array of the Developer codes and the text names of the Developers.</returns>
        [HttpGet]
        public string GetDevelopers()
        {
            return JsonConvert.SerializeObject(Developers);
        }
    }
}
