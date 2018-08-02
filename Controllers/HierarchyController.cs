using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
 

namespace UniVerseDotNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HierarchyController : ControllerBase
    {
        public class Node
        {
            public Node(string id, string name)
            {
                Id = id;
                Name = name;
            }

            public string Id { get; set; }
            public string Name { get; set; }
        }

        // Initialize the data for the demo. These should be removed if we can get the Developers, Projects and Lenders from the Universe DB
        static Node[] Developers = new Node[]
        {
            new Node("0001", "Developer 0001"),
            new Node("0002", "Developer 0002"),
            new Node("0003", "Developer 0003"),
            new Node("0004", "Developer 0004"),
            new Node("0005", "Developer 0005"),
            new Node("0006", "Developer 0006"),
            new Node("0007", "Developer 0007"),
            new Node("0008", "Developer 0008"),
            new Node("0009", "Developer 0009")
        };
        static Node[] Projects = new Node[]
        {
            new Node("010", "Project 010"),
            new Node("020", "Project 020"),
            new Node("030", "Project 030"),
            new Node("040", "Project 040"),
            new Node("050", "Project 050"),
            new Node("060", "Project 060"),
            new Node("070", "Project 070")
        };
        static Node[] Lenders = new Node[]
        {
            new Node("100", "Lender 100"),
            new Node("200", "Lender 200"),
            new Node("300", "Lender 300"),
            new Node("400", "Lender 400")
        };

        /// <summary>
        /// Retrieves the set of Developers that the user has access to
        /// </summary>
        /// <returns>Json array of the Developer codes and the text names of the Developers.</returns>
 
        [HttpGet("developers")]
        public string GetDevelopers()
        {
            Console.WriteLine($"Received request for developers");

            return JsonConvert.SerializeObject(Developers);
        }

        /// <summary>
        /// Retrieves the set of Projects that the user has access to
        /// </summary>
        /// <returns>Json array of the Lender codes and the text names of the Projects.</returns>

        [HttpGet("projects")]
        public string GetProjects([FromQuery(Name = "developers")] string developers)
        {
            var jsonDevelopers = JsonConvert.SerializeObject(developers);
            Console.WriteLine($"Received request for projects given the developers: {jsonDevelopers}");

            return JsonConvert.SerializeObject(Projects);
        }

        /// <summary>
        /// Retrieves the set of Lenders that the user has access to
        /// </summary>
        /// <returns>Json array of the Lender codes and the text names of the Lenders.</returns>

        [HttpGet("lenders")]
        public string GetLenders([FromQuery(Name = "developers")] string developers)
        {
            var jsonDevelopers = JsonConvert.SerializeObject(developers);
            Console.WriteLine($"Received request for projects given the developers: {jsonDevelopers}");

            return JsonConvert.SerializeObject(Lenders);
        }

    }
}
