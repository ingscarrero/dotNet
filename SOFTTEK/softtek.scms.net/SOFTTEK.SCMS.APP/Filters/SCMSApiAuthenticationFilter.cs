using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOFTTEK.SCMS.SRA.Filters
{
    public class SCMSApiAuthenticationFilter : GenericAuthenticationFilter
    {
        public SCMSApiAuthenticationFilter(bool isActive)
            : base(isActive)
        {
        }

        /// <summary>
        /// Virtual method.Can be overriden with the custom Authorization.
        /// </summary>
        /// <param name="deviceIdentifier"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        protected override bool OnAuthorizeUser(string deviceIdentifier, string user, string password, System.Web.Http.Controllers.HttpActionContext filterContext)
        {
            if (!base.OnAuthorizeUser(deviceIdentifier, user, password, filterContext))
            {
                return false;
            }

            SOFTTEK.SCMS.Entity.Security.User userCredentials = new SOFTTEK.SCMS.Entity.Security.User
            {
                DeviceIdentifier = deviceIdentifier,
                NetworkAccount = user,
                Password = password
            };

            SOFTTEK.SCMS.Foundation.Business.BusinessContext ctx = new SOFTTEK.SCMS.Foundation.Business.BusinessContext
            {
                SecurityContext = new Foundation.Security.SecurityContext
                {
                    DeviceID = userCredentials.DeviceIdentifier,
                    ClientID = userCredentials.NetworkAccount,
                    AuthorizationTicket = userCredentials.NetworkAccount,
                    AppID = new System.Configuration.AppSettingsReader().GetValue("S_SRA_APP_idENTIFIER", typeof(string)).ToString()
                }
            };
            
            SOFTTEK.SCMS.Business.Security.SecurityBO securityProvider = new Business.Security.SecurityBO(ctx);
            
            if (securityProvider.Authorize(userCredentials) != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Virtual method.Can be overriden with the custom Authorization.
        /// </summary>
        /// <param name="deviceIdentifier"></param>
        /// <param name="token"></param>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        protected override bool OnAuthorizeUser(string deviceIdentifier, string token, System.Web.Http.Controllers.HttpActionContext filterContext)
        {
            if (!base.OnAuthorizeUser(deviceIdentifier, token, filterContext))
            {
                return false;
            }


            SOFTTEK.SCMS.Foundation.Business.BusinessContext ctx = new SOFTTEK.SCMS.Foundation.Business.BusinessContext
            {
                SecurityContext = new Foundation.Security.SecurityContext
                {
                    DeviceID = deviceIdentifier,
                    ClientID = deviceIdentifier,
                    AuthorizationTicket = token,
                    AppID = new System.Configuration.AppSettingsReader().GetValue("S_SRA_APP_idENTIFIER", typeof(string)).ToString()
                }
            };

            SOFTTEK.SCMS.Business.Security.SecurityBO securityProvider = new SCMS.Business.Security.SecurityBO(ctx);
            
            if (securityProvider.GetToken() != null)
            {
                return true;
            }

            return false;
        }

    }
}