using System;

namespace Entities
{
    public class Account
    {      
        public Guid? userid { get; set; }
        public long? logins { get; set; }
        public bool? seePublic { get; set; }
        public bool? onlyPrivate { get; set; }
        public User user { get; set; }
    }
}
