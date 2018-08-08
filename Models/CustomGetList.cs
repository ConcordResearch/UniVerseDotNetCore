using System.Collections.Generic;
using UniVerseDotNetCore.Domain.CssServiceLayer.Models;

namespace UniVerseDotNetCore.Models
{
    public class CustomGetList
    {
        public CustomGetList()
        {
            Credentials = new CssCredentials();
            Accounts = new List<AccountModel>();
        }

        public CssCredentials Credentials { get; set; }
        public IEnumerable<AccountModel> Accounts { get; set; }
    }
}
