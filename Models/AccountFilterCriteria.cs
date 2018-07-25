using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace UniVerseDotNetCore.Models
{
    public class AccountFilterCriteria<T> where T : CSSAccountFile, new()
    {

        public string DeveloperCode { get; set; }
        public  T CssFileName { get;  set; }

        public List<Criterion> Criteria { get; set; }


        public AccountFilterCriteria()
        {
            Criteria = new List<Criterion>();
            CssFileName = new T();
        }
        public AccountFilterCriteria(string developerCode) : this()
        {
            DeveloperCode = developerCode;
        }

        public List<Criterion> AddCriterion(Criterion criterion)
        {
            Criteria.Add(criterion);
            return Criteria;
        }
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
            foreach (var filter in Criteria)
            {
                if (query.Length != 0 )
                {
                    query.Append( " AND ");
                }
                query.Append( filter.Attribute + "=\""  + filter.Filter + "\"");
            }

            query.Append($"SELECT {CssFileName} WITH " + query);

            return query.ToString();
        }
    }
}