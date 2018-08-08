using UniVerseDotNetCore.Domain.CssServiceLayer.Models;

namespace UniVerseDotNetCore.Models
{
    public class CssFileResult
    {
        public CssFileResult()
        {
            CssResponses = new CssCommandResult();
            FileContents = new CssFile();
        }
      
        public CssCommandResult CssResponses { get; set; }
        public CssFile FileContents { get; set; }
    }
}