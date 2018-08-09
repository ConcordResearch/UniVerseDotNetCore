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

            new Node("0098", "Project F (0098)"),
            new Node("0099", "Project G (0099)")
        };
        static readonly Node[] Lenders = new Node[]
        {
			new Node("201", "House (201)"),
            new Node("600", "Lender ABC (600)")
            
        };            

        /// <summary>
        /// Retrieves the set of Developers that the user has access to
        /// </summary>
        /// <returns>Json array of the Developer codes and the text names of the Developers.</returns>
 
        [HttpGet("developers")]
        public JsonResult GetDevelopers()
        {
            Console.WriteLine("Received request for developers");

            return new JsonResult(Developers);
        }

        /// <summary>
        /// Retrieves the set of Projects that the user has access to
        /// </summary>
        /// <returns>Json array of the Lender codes and the text names of the Projects.</returns>

        [HttpGet("projects")]
        public JsonResult GetProjects([FromQuery(Name = "developers")] string developers)
        {
            var jsonDevelopers = JsonConvert.SerializeObject(developers);
            Console.WriteLine($"Received request for projects given the developers: {jsonDevelopers}");

            return new JsonResult(Projects);
        }

        /// <summary>
        /// Retrieves the set of Lenders that the user has access to
        /// </summary>
        /// <returns>Json array of the Lender codes and the text names of the Lenders.</returns>

        [HttpGet("lenders")]
        public JsonResult GetLenders([FromQuery(Name = "developers")] string developers)
        {
            var jsonDevelopers = JsonConvert.SerializeObject(developers);
            Console.WriteLine($"Received request for projects given the developers: {jsonDevelopers}");

            return new JsonResult(Lenders);
        }

    }
}
