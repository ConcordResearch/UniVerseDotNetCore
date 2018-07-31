using System.Collections.Generic;

namespace UniVerseDotNetCore.Domain.CssServiceLayer.Models
{
    public class CssFile
    {
        public CssFile()
        {
            Records = new List<CssRecord>();
        }

        public string FileName { get; set; }
        public List<CssRecord> Records { get; set; }
    }
}