using System;
using System.Collections.Generic;


namespace Entities
{
    public class Group
    {
        
        public int? groupid { get; set; }            

        public string title { get; set; }

        public string description { get; set; }

        public string tags { get; set; }

        public DateTime? date { get; set; }

        public bool? isPrivate { get; set; }

        public int? categoryid { get; set; }

        public Category category { get; set; }

        public List<Field> fields { get; set; }
    }
}
