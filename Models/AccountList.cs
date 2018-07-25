namespace UniVerseDotNetCore.Models
{
    public class AccountList
    {
        public string AccountListName { get; set; }
        public override string ToString()
        {
            return AccountListName;
        }
    }
}