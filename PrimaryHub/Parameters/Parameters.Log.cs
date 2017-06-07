using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimaryHub.Parameters
{
    public class SignUpIn : BaseParameter
    {
        public User User { get; set; }
    }
    public class SignUpOut : BaseParameter
    {
        public User User { get; set; }      
    }

    public class LogInIn : BaseParameter
    {
        public bool FireBaseForce { get; set; }
        public User User { get; set; }
    }
    public class LogInOut : BaseParameter
    {
        public User User { get; set; }
       
    }
}
