using Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntitiesAcces.CommonEntities
{
    public class Field
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? id { get; set; }

        public string name { get; set; }

        public string type { get; set; }

        public string value { get; set; }

        public bool? isLink { get; set; }

        public Language? code { get; set; }

        public Guid? snipettid { get; set; }

        [ForeignKey("snipettid")]
        public Snippet snipett { get; set; }
    }
}
