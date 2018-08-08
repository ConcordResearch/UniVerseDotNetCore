using UniVerseDotNetCore.Domain.CssServiceLayer.Models;
using UniVerseDotNetCore.Domain.Models;

namespace UniVerseDotNetCore.Models
{
    public class MultiNoteModel
    {
        public MultiNoteModel()
        {
            GetList = new AccountList();
            Credentials = new CssCredentials();
            Note = new Note("This is the note");
        }
        public AccountList GetList { get; set; }
        public CssCredentials Credentials { get; set; }
        public Note Note { get; set; }
    }
}