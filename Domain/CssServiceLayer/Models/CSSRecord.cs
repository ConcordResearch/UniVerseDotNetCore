using System.Collections.Generic;

namespace UniVerseDotNetCore.Domain.CssServiceLayer.Models
{
    public class CssRecord
    {
        public CssRecord(string id, string data)
        {
            RecordId = id;
            RecordData = data;
            Fields = new List<CssField>();
        }
        public string RecordId { get; set; }

        public string RecordData { get; set; }
        public List<CssField> Fields { get; set; }
    }
}