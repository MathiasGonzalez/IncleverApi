using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntitiesAcces.CommonEntities
{
    public class Group
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? groupid { get; set; }             

        public string title { get; set; }

        public string description { get; set; }

        public string tags { get; set; }

        public DateTime? date { get; set; }

        public bool? isPrivate { get; set; }

        public int? categoryid { get; set; }

        [ForeignKey("categoryid")]
        public Category category { get; set; }

        public virtual List<Snippet> snippets { get; set; }
    }
}
