using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Auth
{
    public class Session
    {      
        public Guid? userid { get; set; }

        public DateTime? fechasession { get; set; }

        public string token { get; set; }

        public string platform { get; set; }

        public string ip { get; set; }

        public User user { get; set; }

    }
}
