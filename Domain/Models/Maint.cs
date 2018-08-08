namespace UniVerseDotNetCore.Domain.Models
{
    public class Maint : CssAccountFile
    {
        private string _fileName;

        public override string FileName
        {
            get => _fileName;

            // ReSharper disable once ValueParameterNotUsed
            set => _fileName = GetType().Name.ToUpper();
        }

    }
}