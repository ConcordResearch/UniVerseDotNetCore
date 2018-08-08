namespace UniVerseDotNetCore.Models
{
    public class AccountModel
    {
        public AccountModel()
        {
        }

        public AccountModel(string accountNumber)
        {
            Account = accountNumber;
        }
        public string Account { get; set; }
    }
}