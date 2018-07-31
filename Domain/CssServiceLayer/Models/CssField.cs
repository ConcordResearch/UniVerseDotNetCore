namespace UniVerseDotNetCore.Domain.CssServiceLayer.Models
{
    public class CssField
    {
        public CssField(string id, string data)
        {
            FieldData = data;
            FieldId = id;

        }
        public string FieldId { get; set; }
        public string FieldData  { get; set; }
    }
}