using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntitiesAcces.CommonEntities.Auth
{
    public class Session
    {
        [Key]
        [ForeignKey("user")]
        public Guid? userid { get; set; }

        public DateTime? fechasession { get; set; }

        public string token { get; set; }

        public string platform { get; set; }

        public string ip { get; set; }

        public virtual User user { get; set; }

    }
}
