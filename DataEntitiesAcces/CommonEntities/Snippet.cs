using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntitiesAcces.CommonEntities
{
    public class Snippet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid? snipetid { get; set; }

        public int? id { get; set; }

        public int? groupid { get; set; }

        public string title { get; set; }

        public string description { get; set; }

        public string tags { get; set; }

        public DateTime? date { get; set; }

        public List<Field> fields { get; set; }

        [ForeignKey("groupid")]
        public Group group { get; set; }
    }
}
