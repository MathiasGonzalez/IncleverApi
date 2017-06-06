using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Field
    {
        public int? id { get; set; }

        public string name { get; set; }

        public string type { get; set; }

        public string value { get; set; }

        public bool? isLink { get; set; }

        public Language? code { get; set; }

        public Guid? snipettid { get; set; }

        public Snippet snipett { get; set; }
    }
}
