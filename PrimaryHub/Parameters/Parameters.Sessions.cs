using Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimaryHub.Parameters
{
    public class NewSessionIn : BaseParameter
    {
        public Session session { get; set; }
    }

    public class NewSessionOut : BaseParameter
    {
        public Session session { get; set; }
    }
}
