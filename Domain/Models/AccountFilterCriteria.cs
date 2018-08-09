using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace UniVerseDotNetCore.Domain.Models
{
    public class AccountFilterCriteria<T> where T : CssAccountFile, new()
    {
        public string DeveloperCode { get; set; }
        public  T CssFileName { get;  set; }

        public List<Criterion> Criteria { get; set; }
        
        public AccountFilterCriteria()
        {
            Criteria = new List<Criterion>();
            CssFileName = new T();
        }

        // ReSharper disable once UnusedMember.Global
        public AccountFilterCriteria(string developerCode) : this()
        {
            DeveloperCode = developerCode;
        }

        // ReSharper disable once UnusedMethodReturnValue.Global
        public List<Criterion> AddCriterion(Criterion criterion)
        {
            Criteria.Add(criterion);
            return Criteria;
        }

        // ReSharper disable once UnusedMember.Global
        public List<Criterion> AddCriteria(IEnumerable<Criterion> criteria)
        {
            Criteria.ToList().AddRange(criteria);
            return Criteria;
        }

        public override string ToString()
        {
            //SELECT MAINT WITH PROJ.CODE ="1075" AND LEND.CODE="100" AND AC="N"
            if (Criteria.Count == 0)
            {
                return "empty";
            }
            if (Criteria.Count == 1)
            {
                return $"SELECT {CssFileName} WITH {Criteria[0].Attribute}=\"{Criteria[0].Filter}\"" ;
            }

            var query = new StringBuilder();
            query.Append($"SELECT {CssFileName} WITH ");

            foreach (var filter in Criteria)
            {
                if(string.IsNullOrWhiteSpace(filter.Filter)) continue;
                
                if (!query.ToString().EndsWith("WITH "))
                {
                    query.Append( " AND ");
                }
                query.Append( filter.Attribute + "=\""  + filter.Filter + "\"");
            }

            
            return query.ToString();
        }
    }
}