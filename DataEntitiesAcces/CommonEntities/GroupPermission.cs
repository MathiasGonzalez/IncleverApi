using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntitiesAcces.CommonEntities
{
    public class GroupPermission
    {
        [Column(Order = 0), Key, ForeignKey("group")]
        public int? groupid { get; set; }

        [Column(Order = 1), Key, ForeignKey("user")]
        public Guid? userid { get; set; }

        public bool? favorite { get; set; }

        public bool? isdefault { get; set; }

        public bool? isprivate { get; set; }

        public bool? sticky { get; set; }

        public User user { get; set; }

        public Group group { get; set; }
    }
}
