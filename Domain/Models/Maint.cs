namespace UniVerseDotNetCore.Domain.Models
{
    public class Maint : CssAccountFile
    {
        private string _fileName;

        public override string FileName
        {
            get => _fileName;

            set => _fileName = GetType().Name.ToUpper();
        }

    }
}