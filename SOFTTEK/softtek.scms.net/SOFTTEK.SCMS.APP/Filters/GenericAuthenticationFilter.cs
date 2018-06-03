using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Filters;
using System.Threading;

namespace SOFTTEK.SCMS.SRA.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class GenericAuthenticationFilter : AuthorizationFilterAttribute
    {
        private readonly bool _isActive = true;

        /// <summary>
        /// parameter isActive explicitly enables/disables this filetr.
        /// </summary>
        /// <param name="isActive"></param>
        public GenericAuthenticationFilter(bool isActive)
        {
            _isActive = isActive;
        }

        /// <summary>
        /// Checks basic authentication request
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (!_isActive)
            {
                return;
            }
            var identity = FetchAuthHeader(actionContext);
            if (identity == null)
            {
                ChallengeAuthRequest(actionContext);
                return;
            }
            var genericPrincipal = new System.Security.Principal.GenericPrincipal(identity, null);
            Thread.CurrentPrincipal = genericPrincipal;

            string deviceId = identity.DeviceIdentifier;
            string token = identity.Token != null ? identity.Token.Identifier : null;
            string user = identity.User != null ? identity.User.NetworkAccount : null;
            string password = identity.User != null ? identity.User.Password : null;

            if (!OnAuthorizeUser(deviceId, user, password, actionContext) && !OnAuthorizeUser(deviceId, token, actionContext))
            {
                ChallengeAuthRequest(actionContext);
                return;
            }
            base.OnAuthorization(actionContext);
        }

        /// <summary>
        /// Virtual method.Can be overriden with the custom Authorization.
        /// </summary>
        /// <param name="deviceIdentifier"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        protected virtual bool OnAuthorizeUser(string deviceIdentifier, string user, string password, System.Web.Http.Controllers.HttpActionContext filterContext)
        {
            if (string.IsNullOrEmpty(deviceIdentifier) || string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Virtual method.Can be overriden with the custom Authorization.
        /// </summary>
        /// <param name="deviceIdentifier"></param>
        /// <param name="token"></param>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        protected virtual bool OnAuthorizeUser(string deviceIdentifier, string token, System.Web.Http.Controllers.HttpActionContext filterContext)
        {
            if (string.IsNullOrEmpty(deviceIdentifier) || string.IsNullOrEmpty(token))
            {
                return false;
            }
            return true;
        }

        protected virtual SOFTTEK.SCMS.SRA.Models.BasicAuthenticationIdentity FetchAuthHeader(System.Web.Http.Controllers.HttpActionContext filterContext)
        {
            string authHeaderValue = null;
            var authRequest = filterContext.Request.Headers.Authorization;
            if (authRequest != null && !String.IsNullOrEmpty(authRequest.Scheme) && authRequest.Scheme == "Basic")
                authHeaderValue = authRequest.Parameter;
            if (string.IsNullOrEmpty(authHeaderValue))
                return null;
            authHeaderValue = Encoding.Default.GetString(Convert.FromBase64String(authHeaderValue));
            var credentials = authHeaderValue.Split(':');

            if (credentials.Length == 2)
            {
                SOFTTEK.SCMS.Entity.Security.Token token = new Entity.Security.Token
                {
                    Identifier = credentials[1]
                };
                SOFTTEK.SCMS.SRA.Models.BasicAuthenticationIdentity identity = new SOFTTEK.SCMS.SRA.Models.BasicAuthenticationIdentity(
                    token,
                    credentials[0]
                );
                return identity;
            }
            else if(credentials.Length == 3){
                SOFTTEK.SCMS.SRA.Models.BasicAuthenticationIdentity identity = new SOFTTEK.SCMS.SRA.Models.BasicAuthenticationIdentity( 
                    credentials[1], 
                    credentials[2],
                    credentials[0]
                );
                return identity;
            }
            return null;
        }

        /// <summary>
        /// Send the Authentication Challenge request
        /// </summary>
        /// <param name="filterContext"></param>
        private static void ChallengeAuthRequest(System.Web.Http.Controllers.HttpActionContext filterContext)
        {
            var dnsHost = filterContext.Request.RequestUri.DnsSafeHost;
            filterContext.Response = filterContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized);
            filterContext.Response.Headers.Add("WWW-Authenticate", string.Format("Basic realm=\"{0}\"", dnsHost));
        }

    }
}