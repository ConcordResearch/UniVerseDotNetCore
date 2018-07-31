using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniVerseDotNetCore.Domain.Models
{
    public class ProjectCode : IChangeIt2
    {
        public ProjectCode()
        {
            
        }
        public ProjectCode(string code)
        {
            Code = code;
        }
        public string Code { get; set; }
    }
}
