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
        /// <summary>
        /// Establece si se debe chequear consistencia de la ip de la request
        /// </summary>
        private const bool VALIDAR_IP = false;

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

            const string Header = "Token";
            const string UserIDToken = "Uid";

            #region Validar session



            var userToken = actionContext.Request.Headers.Where(h => h.Key == UserIDToken).FirstOrDefault();

            if (userToken.Key == null || userToken.Value == null || userToken.Value.Count() < 1)
            {
                return false;
            }

            if (VALIDAR_IP && !ValidSession(Guid.Parse(userToken.Value.First()), GetClientIp(actionContext.Request)))
            {
                return false;
            }
            #endregion

            #region Validar token
            var headerToken = actionContext.Request.Headers.Where(h => h.Key == Header).FirstOrDefault();

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
            #region Validar Session
            var Hub = new PrimaryHub.Hub();
            var lastSession = Hub.GetLastSession(new GetLastSessionIn { user = input.user });
            if (lastSession.session.ip == ip)//LA ip COINCIDE
            {
                return true;
            }

            #endregion
            return false;
        }

        private bool ValidSession(Guid userid, String ip)
        {
            #region Validar Session
            var Hub = new PrimaryHub.Hub();
            var lastSession = Hub.GetLastSession(new GetLastSessionIn { user = new Entities.User { userid = userid } });
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