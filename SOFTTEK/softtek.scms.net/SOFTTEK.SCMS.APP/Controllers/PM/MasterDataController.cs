using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SOFTTEK.SCMS.SRA.Controllers.PM
{
    public class MasterDataController : BaseApiController
    {
        [ActionName("DefaultAction")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public Task<IHttpActionResult> GetParameter()
        {
            IHttpActionResult result = NotFound();

            try
            {
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

                List<SOFTTEK.SCMS.Entity.Shared.Parameter<string>> masterData = new List<SOFTTEK.SCMS.Entity.Shared.Parameter<string>>();

                SOFTTEK.SCMS.Business.PM.MasterDataBO masterDataBO = new Business.PM.MasterDataBO(ctx);
                masterData = masterDataBO.GetMasterDataForCategory();

                if (masterData.Count > 0)
                {
                    result = Json(masterData);
                }
            }
            catch (Exception ex)
            {
                result = Error(ex);
            }

            return Task.FromResult(result);
        }
    }
}
