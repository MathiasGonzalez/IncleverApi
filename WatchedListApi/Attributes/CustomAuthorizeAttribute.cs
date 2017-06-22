﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Angular4API.Attributes
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute

    {

        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {

            if (AuthorizeRequest(actionContext))

            {

                return;

            }

            HandleUnauthorizedRequest(actionContext);

        }

        protected override void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {

            //Code to handle unauthorized request           
        }

        private bool AuthorizeRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {

            //Write your code here to perform authorization

            return true;

        }

    }
}