namespace UniVerseDotNetCore.Domain.Models
{
    public class Contracts : CssAccountFile
    {
        
        public override string FileName => GetType().Name.ToUpper();
    }
}