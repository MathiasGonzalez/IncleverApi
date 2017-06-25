using Enums;
using System;

namespace Entities
{
    public class User
    {
        public Guid? userid { get; set; }

        public string UID { get; set; }

        public string email { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string userName { get; set; }

        public string password { get; set; }

        public UserStatus status { get; set; }
        
    }
}
