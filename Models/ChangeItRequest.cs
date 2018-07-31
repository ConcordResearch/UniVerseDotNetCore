using UniVerseDotNetCore.Domain.CssServiceLayer.Models;
using UniVerseDotNetCore.Domain.Models;

namespace UniVerseDotNetCore.Models
{
    public class ChangeItRequest<T> where T : IChangeIt2, new()
    {


        public ChangeItRequest()
        {
            
        }
        public ChangeItRequest(string changeType, AccountList getList, CssCredentialsModel credentials, CssAccountFile file,Note note)
        {
            TypeOfChange = changeType;
            GetListName = getList;
            Credentials = credentials;
            File = file;
            Note = note;
        }

        public T NewCode  { get; set; }
        public string TypeOfChange { get; set; }

        public AccountList GetListName { get; set; }
        public CssCredentialsModel Credentials { get; set; }
        public CssAccountFile File { get; set; }
        public Note Note { get; set; }
    }

  
}
