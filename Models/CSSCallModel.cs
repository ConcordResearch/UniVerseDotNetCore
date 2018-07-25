namespace UniVerseDotNetCore.Models
{
    public class CssCallModel
    {
        public CssCallModel()
        {
            Credentials = new CssCredentialsModel();
            CssDataFile = new AccountList() {AccountListName = "empty"};
        }
        public CssCallModel(CssCredentialsModel credentials, AccountList accountList)
        {
            Credentials = credentials;
            CssDataFile = accountList;
        }

        public CssCredentialsModel Credentials { get; }
        public AccountList CssDataFile { get; }

    }
}