namespace UniVerseDotNetCore.Domain.Models
{
    public class CssAccountFile
    {
        public virtual string FileName
        {
            get;
            set;
        }
        public override string ToString()
        {
            return FileName;
        }
    }
}