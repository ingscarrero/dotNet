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
    public class MenuController : BaseApiController
    {
        [ActionName("DefaultAction")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public IHttpActionResult Get()
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

            SOFTTEK.SCMS.Business.Common.ContentBO mobileContentBO = new Business.Common.ContentBO(ctx);
            List<SOFTTEK.SCMS.Entity.SRA.Mobile.HomeItem> items = mobileContentBO.GetMenuItems();

            if (items != null && items.Count > 0)
            {
                result = Json(items);
            }

            return result;
        }

        
    }
}