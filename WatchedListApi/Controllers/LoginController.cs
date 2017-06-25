
using PrimaryHub;
using PrimaryHub.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Angular4API.Controllers
{

    public class LoginController : BaseController
    {

        [HttpPost]
        public LogInOut LogIn([FromBody]LogInIn input)
        {
            var IP = string.Empty;

            if (ActionContext.Request.Properties.ContainsKey("MS_HttpContext"))
            {
                IP = ((HttpContextWrapper)ActionContext.Request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }
            else
            {
                IP = HttpContext.Current.Request.UserHostAddress;
            }
            return PrimaryHub.LogIn(input,IP);
        }


        [HttpPost]
        public SignUpOut SignUp([FromBody]SignUpIn input)
        {
            return PrimaryHub.SignUp(input);
        }
    }
}
