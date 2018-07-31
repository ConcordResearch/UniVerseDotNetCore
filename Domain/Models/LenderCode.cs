namespace UniVerseDotNetCore.Domain.Models
{
    public class LenderCode : IChangeIt2
    {
        public LenderCode()
        {
            Code = "LenderCode";
        }
        public LenderCode(string code)
        {
            Code = code;
        }
        public string Code { get; set; }
    }
}