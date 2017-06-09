using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntitiesAcces.CommonEntities
{
    public class Tag
    {
        [Key]
        public string tag { get; set; }

        public virtual List<Snippet> snippets { get; set; }
    }
}
