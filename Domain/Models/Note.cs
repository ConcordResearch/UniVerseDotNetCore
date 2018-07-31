using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniVerseDotNetCore.Domain.Models
{
    public class Note
    {
        public Note(string message)
        {
            Message = message;
        }
        public string Message { get; set; }
    }
}
