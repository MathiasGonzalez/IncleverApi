 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntitiesAcces.CommonEntities
{
    [Table("Movies")]
    public class Movie
    {
        public Movie()
        {

        }

        public string uuid { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid? MovieGuid { get; set; }

        public int? id{ get; set; }


        //[MaxLength(350)]
        //public string title { get; set; }

        //public bool? adult { get; set; }

        //public string original_language { get; set; }

        //public string original_title { get; set; }

        //public string overview { get; set; }

        //public int? popularity { get; set; }

        //public string poster_path { get; set; }

        //public string backdrop_path { get; set; }

        //public bool video { get; set; }

        public int? vote_average { get; set; }

        public int? vote_count { get; set; }        

        public string release_date { get; set; }

        public DateTime? release { get; set; }

        public bool? seen { get; set; }

        public int? rating { get; set; }

        public string director { get; set; }    
        
      
    }
}
