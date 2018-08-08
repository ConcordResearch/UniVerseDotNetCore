using UniVerseDotNetCore.Domain.CssServiceLayer.Models;
using UniVerseDotNetCore.Domain.Models;

namespace UniVerseDotNetCore.Models
{
    public class MaintFilterCallModel
    {
        public MaintFilterCallModel()
        {
            Credentials = new CssCredentials();
            FilterCriteria = new AccountFilterCriteria<Maint>();
        }


        public CssCredentials Credentials { get; set; }
        public AccountFilterCriteria<Maint> FilterCriteria { get; set; }

    }
}