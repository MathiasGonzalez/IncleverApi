using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace DataEntitiesAcces.CommonEntities
{
    [Table("VisualSections")]
    public class VisualSection
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? section_key { get; set; }

        public string title { get; set; }

        public string content { get; set; }

        public Guid? VisualItemGuid { get; set; }

        [ForeignKey("VisualItemGuid")]
        public Movie ParentItem { get; set; }

    }
}
