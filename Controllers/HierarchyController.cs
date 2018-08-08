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
            new Node("0099", "Client A (0099)")           
        };                              
        static readonly Node[] Projects = new Node[]
        {
            new Node("0090", "Project A (0090)"),
            new Node("0092", "Project B (0092)"),
            new Node("0094", "Project C (0094)"),
            new Node("0095", "Project D (0095)"),
            new Node("0096", "Project E (0096)"),
            new Node("0098", "Project F (0098)"),
            new Node("0099", "Project G (0099)")
        };
        static readonly Node[] Lenders = new Node[]
        {
				
			new Node("100", "Lender ABC (100)"),
			new Node("201", "Lender ABC (201)"),
            new Node("400", "Lender ABC (400)"),
            new Node("600", "Lender XYZ (600)")
            
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
