using System.Collections.Generic;

namespace UniVerseDotNetCore.Models
{
    public class CssCommandResult
    {
        public CssCommandResult()
        {
            Results = new List<CommandResponse>();
        }
        public List<CommandResponse> Results { get; set; }
    }
}
