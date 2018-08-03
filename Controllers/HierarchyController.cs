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
        
            new Node("0080", "Developer 0080"),
            new Node("0080", "Developer 0080"),
            new Node("0080", "Developer 0080"),
            new Node("0080", "Developer 0080"),
            new Node("0080", "Developer 0080"),
            new Node("0095", "Developer 0095"),
            new Node("0095", "Developer 0095"),
            new Node("0095", "Developer 0095")
            
        };                              
        static Node[] Projects = new Node[]
        {
            new Node("0080", "Project 0080"),
            new Node("0081", "Project 0081"),
            new Node("0082", "Project 0082"),
            new Node("0083", "Project 0083"),
            new Node("0084", "Project 0084"),
            new Node("0095", "Project 0095"),
            new Node("0095", "Project 0095"),
            new Node("0096", "Project 0096")
        };
        static Node[] Lenders = new Node[]
        {
            new Node("100", "Lender 100"),
            new Node("100", "Lender 100"),
            new Node("100", "Lender 100"),
            new Node("100", "Lender 100"),
            new Node("100", "Lender 100"),
            new Node("101", "Lender 100"),
            new Node("201", "Lender 100"),
            new Node("101", "Lender 101")
                      
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
