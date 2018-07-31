using System.Collections.Generic;
using UniVerseDotNetCore.Domain.CssServiceLayer.Models;

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