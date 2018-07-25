using Newtonsoft.Json;

namespace UniVerseDotNetCore.Models
{
    public class ContractsFilterCallModel 
    {
        public ContractsFilterCallModel()
        {
            Credentials = new CssCredentialsModel();
            FilterCriteria = new AccountFilterCriteria<Contracts>();
        }


        public CssCredentialsModel Credentials { get; set; }
        public AccountFilterCriteria<Contracts> FilterCriteria { get; set; }
        
    }
    public class MaintFilterCallModel
    {
        public MaintFilterCallModel()
        {
            Credentials = new CssCredentialsModel();
            FilterCriteria = new AccountFilterCriteria<Maint>();
        }


        public CssCredentialsModel Credentials { get; set; }
        public AccountFilterCriteria<Maint> FilterCriteria { get; set; }

    }
}