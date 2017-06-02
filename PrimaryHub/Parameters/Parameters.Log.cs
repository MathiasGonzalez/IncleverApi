using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parameters
{
    public class SignUpIn
    {
        public User User { get; set; }
    }
    public class SignUpOut
    {
        public User User { get; set; }
        public string result { get; set; }
    }

    public class LogInIn
    {
        public bool FireBaseForce { get; set; }
        public User User { get; set; }
    }
    public class LogInOut
    {
        public User User { get; set; }
        public string result { get; set; }
    }
}
