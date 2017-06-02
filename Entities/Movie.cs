using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Movie
    {
        public Movie()
        {

        }

        public string uuid { get; set; }

        public Guid? movieGuid { get; set; }

        public int? id { get; set; }

        //public string VisualItemTypeId { get; set; }

        //public string title { get; set; }

        //public bool? adult { get; set; }

        //public string original_language { get; set; }

        //public string original_title { get; set; }

        //public string overview { get; set; }

        //public long? popularity { get; set; }

        //public string poster_path { get; set; }

        //public string backdrop_path { get; set; }

        //public bool video{get;set;}

        public long? vote_average { get; set; }

        public long? vote_count { get; set; }  

        public string release_date { get; set; }

        public DateTime? release { get; set; }

        public bool? seen { get; set; }

        public int? rating { get; set; }

        public string director { get; set; }

       
    }
}
