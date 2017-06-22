
using PrimaryHub;
using PrimaryHub.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Angular4API.Controllers
{
  
    public class LoginController : BaseController
    {
   
        [HttpPost]
        public LogInOut LogIn([FromBody]LogInIn input)
        {
            return PrimaryHub.LogIn(input);
        }


        [HttpPost]
        public SignUpOut SignUp([FromBody]SignUpIn input)
        {
            return PrimaryHub.SignUp(input);
        }
    }
}
