namespace UniVerseDotNetCore.Domain.Models
{
    public class AccountList
    {
        public AccountList()
        {
            
        }
        public AccountList(string accountListName)
        {
            AccountListName = accountListName;
        }
        public string AccountListName { get; set; }
        public override string ToString()
        {
            return AccountListName;
        }
    }
}