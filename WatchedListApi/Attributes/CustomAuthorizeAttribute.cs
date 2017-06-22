using PrimaryHub.Parameters;
using System;
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
            actionContext.Response =
                new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
        }

        private bool AuthorizeRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {

            //Write your code here to perform authorization
            string Header = "token";

            var model = actionContext.ActionArguments.First().Value as BaseParameter;

            if (model == null || model.user == null)
            {
                return false;
            }

            if (!ValidSession(model, GetClientIp(actionContext.Request)))
            {
                return false;
            }

            #region Check token
            var headerToken = actionContext.Request.Headers.Where(h => h.Key.ToUpper() == Header).FirstOrDefault();

            if (headerToken.Key == null || headerToken.Value == null || headerToken.Value.Count() < 1)
            {
                return false;
            }
            if ("valorDelTokenDesdeBD" == headerToken.Value.First())
            {
                return true;
            }
            #endregion

            return false;

        }

        private bool ValidSession(BaseParameter input, String ip)
        {
            #region Check Session
            var Hub = new PrimaryHub.Hub();
            var lastSession = Hub.GetLastSession(new GetLastSessionIn { user = input.user });
            if (lastSession.session.ip == ip)//LA ip COINCIDE
            {
                return true;
            }

            #endregion
            return false;
        }


        private string GetClientIp(System.Net.Http.HttpRequestMessage request = null)
        {


            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }
            //else if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
            //{
            //    RemoteEndpointMessageProperty prop = (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name];
            //    return prop.Address;
            //}
            else if (HttpContext.Current != null)
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
            else
            {
                return null;
            }
        }

    }
}