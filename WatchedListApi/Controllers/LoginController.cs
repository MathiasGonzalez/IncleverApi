using Parameters;
using PrimaryHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Angular4API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {
        private Hub _primaryHub = null;
        public Hub PrimaryHub
        {
            get
            {
                if (_primaryHub == null)
                {
                    _primaryHub = new Hub();
                }
                return _primaryHub;
            }
        }

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
