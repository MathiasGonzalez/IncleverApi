using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Tag
    {

        public string tag { get; set; }

        public virtual List<Snippet> snippets { get; set; }

    }
}
