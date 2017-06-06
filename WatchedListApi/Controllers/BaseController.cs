using PrimaryHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Angular4API.Controllers
{
    public class BaseController : ApiController
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
    }
}
