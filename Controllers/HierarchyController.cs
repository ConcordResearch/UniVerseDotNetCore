using System;
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
        static readonly Node[] Developers = new Node[]
        {
        
            //new Node("0080", "Client A (0080)"),
            new Node("0095", "Client A (0095)")
            
        };                              
        static readonly Node[] Projects = new Node[]
        {
            new Node("0080", "Project A (0080)"),
            new Node("0081", "Project B (0081)"),
            new Node("0082", "Project C (0082)"),
            new Node("0083", "Project D (0083)"),
            new Node("0084", "Project E (0084)"),
            new Node("0095", "Project F (0095)"),
            new Node("0096", "Project G (0096)")
        };
        static readonly Node[] Lenders = new Node[]
        {
            
            new Node("101", "Lender ABC (101)"),
            new Node("201", "Lender XZY (201)"),
            new Node("400", "Lender DEF (400)")
        };            

        /// <summary>
        /// Retrieves the set of Developers that the user has access to
        /// </summary>
        /// <returns>Json array of the Developer codes and the text names of the Developers.</returns>
 
        [HttpGet("developers")]
        public string GetDevelopers()
        {
            Console.WriteLine("Received request for developers");

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
