using System;

namespace Entities
{
    public class GroupPermission
    {
       
        public int? groupid { get; set; }
   
        public Guid? userid { get; set; }

        public bool? favorite { get; set; }

        public bool? isdefault { get; set; }

        public bool? isprivate { get; set; }

        public bool? sticky { get; set; }

        public User user { get; set; }

        public Group group { get; set; }
    }
}
