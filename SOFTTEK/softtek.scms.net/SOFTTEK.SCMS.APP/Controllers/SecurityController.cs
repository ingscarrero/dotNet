using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;


namespace SOFTTEK.SCMS.SRA.Controllers
{
    public class SecurityController : BaseApiController
    {

        [ActionName("DefaultAction")]
        // POST api/security
        public IHttpActionResult Post([FromBody]SOFTTEK.SCMS.Entity.Security.User user)
        {
            IHttpActionResult result = Conflict();


            SOFTTEK.SCMS.Foundation.Business.BusinessContext ctx = new SOFTTEK.SCMS.Foundation.Business.BusinessContext
            {
                SecurityContext = new Foundation.Security.SecurityContext
                {
                    DeviceID = user.DeviceIdentifier,
                    ClientID = user.NetworkAccount,
                    AuthorizationTicket = user.NetworkAccount,
                    AppID = new System.Configuration.AppSettingsReader().GetValue("S_SRA_APP_idENTIFIER", typeof(string)).ToString()
                }
            };

            SOFTTEK.SCMS.Business.Security.SecurityBO secManagerBO = new Business.Security.SecurityBO(ctx);
            SOFTTEK.SCMS.Entity.Security.Token authorizationToken = secManagerBO.Authorize(user);

            if (authorizationToken != null)
            {
                result = Json(authorizationToken);
            }

            return result;
        }
        
        [ActionName("User")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public IHttpActionResult CreateUser([FromBody]SOFTTEK.SCMS.Entity.Security.User user)
        {
            IHttpActionResult result = Conflict();

            SOFTTEK.SCMS.Foundation.Business.BusinessContext ctx = new SOFTTEK.SCMS.Foundation.Business.BusinessContext
            {
                SecurityContext = new Foundation.Security.SecurityContext
                {
                    DeviceID = user.DeviceIdentifier,
                    ClientID = user.NetworkAccount,
                    AuthorizationTicket = user.NetworkAccount,
                    AppID = new System.Configuration.AppSettingsReader().GetValue("S_SRA_APP_idENTIFIER", typeof(string)).ToString()
                }
            };

            SOFTTEK.SCMS.Business.Security.SecurityBO secManagerBO = new Business.Security.SecurityBO(ctx);
            SOFTTEK.SCMS.Entity.Security.Token authorizationToken = null;

            authorizationToken = secManagerBO.Register(user);

            if (authorizationToken != null)
            {
                result = Json(authorizationToken);
            }

            return result;
        }
    }
}
