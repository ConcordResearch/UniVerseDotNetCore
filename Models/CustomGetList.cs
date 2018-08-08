using System.Collections.Generic;
using UniVerseDotNetCore.Domain.CssServiceLayer.Models;

namespace UniVerseDotNetCore.Models
{
    public class CustomGetList
    {
        public CustomGetList()
        {
            Credentials = new CssCredentials();
            Accounts = new List<string>();
        }
        public CssCredentials Credentials { get; set; }
        public IEnumerable<string> Accounts { get; set; }
    }
}
