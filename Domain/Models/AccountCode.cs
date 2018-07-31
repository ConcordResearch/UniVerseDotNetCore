namespace UniVerseDotNetCore.Domain.Models
{
    public class AccountCode : IChangeIt2
    {
        public AccountCode()
        {
            
        }
        public AccountCode(string code)
        {
            Code = code;
        }
        public string Code { get; set; }
    }
}