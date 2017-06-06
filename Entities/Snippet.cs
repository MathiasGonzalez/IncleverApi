using System;
using System.Collections.Generic;


namespace Entities
{
    public class Snippet
    {
        public Guid? snipetid { get; set; }

        public int? id { get; set; }

        public int? groupid { get; set; }

        public string title { get; set; }

        public string description { get; set; }

        public List<string> tags { get; set; }

        public DateTime? date { get; set; }

        public List<Field> fields { get; set; }

        public Group group { get; set; }
    }
}
