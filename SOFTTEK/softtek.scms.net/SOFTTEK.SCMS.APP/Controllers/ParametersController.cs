using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Threading.Tasks;

namespace SOFTTEK.SCMS.SRA.Controllers
{
    public class ParameterController : BaseApiController
    {
        [ActionName("DefaultAction")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public IHttpActionResult Get(string id)
        {
            IHttpActionResult result = NotFound();

            SOFTTEK.SCMS.Foundation.Business.BusinessContext ctx = new SOFTTEK.SCMS.Foundation.Business.BusinessContext
            {
                SecurityContext = new Foundation.Security.SecurityContext
                {
                    DeviceID = GetDeviceIdentifier(),
                    ClientID = GetToken().Identifier,
                    AuthorizationTicket = GetToken().Identifier,
                    AppID = new System.Configuration.AppSettingsReader().GetValue("S_SRA_APP_idENTIFIER", typeof(string)).ToString()
                }
            };

            SOFTTEK.SCMS.Business.Common.ConfigurationBO mobileParametersBO = new SCMS.Business.Common.ConfigurationBO(ctx);
            
            List<SOFTTEK.SCMS.Entity.Shared.Parameter<string>> items = mobileParametersBO.GetParametersForCategory(id);

            if (items != null && items.Count > 0)
            {
                result = Json(items);
            }

            return result;
        }

    }
}