namespace UniVerseDotNetCore.Models
{
    public class CSSAccountFile
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