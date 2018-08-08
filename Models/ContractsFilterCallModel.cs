using UniVerseDotNetCore.Domain.CssServiceLayer.Models;
using UniVerseDotNetCore.Domain.Models;

namespace UniVerseDotNetCore.Models
{
    public class ContractsFilterCallModel 
    {
        public ContractsFilterCallModel()
        {
            Credentials = new CssCredentials();
            FilterCriteria = new AccountFilterCriteria<Contracts>();
        }


        public CssCredentials Credentials { get; set; }
        public AccountFilterCriteria<Contracts> FilterCriteria { get; set; }
        
    }
}