namespace UniVerseDotNetCore.Domain.Models
{
    public class Maint : CSSAccountFile
    {
        private string _fileName;

        public override string FileName
        {
            get => _fileName;

            set => _fileName = this.GetType().Name.ToUpper();
        }

    }
}