using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class User
    {
        public Guid? Guid { get; set; }
        public string UID;
        public string email;
        public string firstName;
        public string lastName;
        public string userName;
        public string password;
    }
}
