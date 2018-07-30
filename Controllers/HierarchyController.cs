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
        // Initialize the data for the demo. These should be removed if we can get the Developers, Projects and Lenders from the Universe DB
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
        static Dictionary<string, string> Projects = new Dictionary<string, string>()
        {
            { "010", "Project 010" },
            { "020", "Project 020" },
            { "030", "Project 030" },
            { "040", "Project 040" },
            { "050", "Project 050" },
            { "060", "Project 060" },
            { "070", "Project 070" }
        };
        static Dictionary<string, string> Lenders = new Dictionary<string, string>()
        {
            { "100", "Lender 100" },
            { "200", "Lender 200" },
            { "300", "Lender 300" },
            { "400", "Lender 400" }
        };

        /// <summary>
        /// Retrieves the set of Developers that the user has access to
        /// </summary>
        /// <returns>Json array of the Developer codes and the text names of the Developers.</returns>
        [HttpGet]
        public string GetDevelopers()
        {
            return JsonConvert.SerializeObject(Developers);
        }

        /// <summary>
        /// Retrieves the set of Projects that the user has access to
        /// </summary>
        /// <returns>Json array of the Lender codes and the text names of the Projects.</returns>
        [HttpGet]
        public string GetProjects()
        {
            return JsonConvert.SerializeObject(Projects);
        }
         
        /// <summary>
        /// Retrieves the set of Lenders that the user has access to
        /// </summary>
        /// <returns>Json array of the Lender codes and the text names of the Lenders.</returns>
        [HttpGet]
        public string GetLenders()
        {
            return JsonConvert.SerializeObject(Lenders);
        }

    }
}
