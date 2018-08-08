using Newtonsoft.Json;
using UniVerseDotNetCore.Domain.CssServiceLayer.Models;
using UniVerseDotNetCore.Domain.Models;

namespace UniVerseDotNetCore.Models
{
    public class CssCallModel
    {
        [JsonConstructor]
        public CssCallModel()
        {
            
        }
        public CssCallModel(CssAccountFile file)
        {
            File = file;
            Credentials = new CssCredentials();
            CssDataFile = new AccountList() {AccountListName = "empty"};
            
        }
       
        public CssCallModel(CssCredentials credentials, AccountList accountList, CssAccountFile file)
        {
            Credentials = credentials;
            CssDataFile = accountList;
            File = file;
        }

        public CssCredentials Credentials { get; set; }
        public AccountList CssDataFile { get; set; }
        public CssAccountFile File { get; set; }

    }
}