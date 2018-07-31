using System.Collections.Generic;
using UniVerseDotNetCore.Domain.CssServiceLayer.Models;

namespace UniVerseDotNetCore.Models
{
    public class FileResult
    {
        public FileResult()
        {
            Results = new List<CommandResponse>();
            FileContents = new CssFile();
        }
      
        public List<CommandResponse> Results { get; set; }
        public CssFile FileContents { get; set; }
    }
}