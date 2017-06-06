using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntitiesAcces.CommonEntities
{
    public class Account
    {
        [Key]
        [ForeignKey("user")]
        public Guid? userid { get; set; }

        public long? logins { get; set; }

        public bool? seePublic { get; set; }

        public bool? onlyPrivate { get; set; }

        public User user { get; set; }
    }
}
