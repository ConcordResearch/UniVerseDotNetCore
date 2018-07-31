namespace UniVerseDotNetCore.Domain.Models
{
    public class Contracts : CSSAccountFile
    {
        
        public override string FileName => this.GetType().Name.ToUpper();
    }
}