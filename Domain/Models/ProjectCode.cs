namespace UniVerseDotNetCore.Domain.Models
{
    public class ProjectCode : IChangeIt2
    {
        public ProjectCode()
        {
            
        }
        public ProjectCode(string code)
        {
            Code = code;
        }
        public string Code { get; set; }
    }
}
