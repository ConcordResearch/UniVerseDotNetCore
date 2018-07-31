using Newtonsoft.Json;
using UniVerseDotNetCore.Domain.Models;

namespace UniVerseDotNetCore.Domain.CssServiceLayer.Models
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
            Credentials = new CssCredentialsModel();
            CssDataFile = new AccountList() {AccountListName = "empty"};
            
        }
       
        public CssCallModel(CssCredentialsModel credentials, AccountList accountList, CssAccountFile file)
        {
            Credentials = credentials;
            CssDataFile = accountList;
            File = file;
        }

        public CssCredentialsModel Credentials { get; set; }
        public AccountList CssDataFile { get; set; }
        public CssAccountFile File { get; set; }

    }
}