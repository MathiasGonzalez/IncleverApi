using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class VisualSection
    {
        public int? section_key { get; set; }

        public string title { get; set; }

        public string content { get; set; }

        public Guid? VisualItemGuid { get; set; }
    }
}
