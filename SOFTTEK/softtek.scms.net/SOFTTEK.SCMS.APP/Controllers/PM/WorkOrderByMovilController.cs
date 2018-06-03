using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SOFTTEK.SCMS.SRA.Controllers.PM
{
    public class WorkOrderByMovilController : BaseApiController
    {
        // GET: api/WorkOrderByMovil
        #region GetWorkOrders
        [ActionName("DefaultAction")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public Task<IHttpActionResult> Get()
        {
            IHttpActionResult result = NotFound();

            var modelFilter = new SOFTTEK.SCMS.Entity.PM.TechnicalObject();
            modelFilter.ExternalIdentifier = "000000000010013815";

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

                SOFTTEK.SCMS.Business.PM.TechnicalObjectBO pTechnicalObjectBO = new Business.PM.TechnicalObjectBO(ctx);
                List<SOFTTEK.SCMS.Entity.PM.WorkOrder> listWorkOrder = new List<SOFTTEK.SCMS.Entity.PM.WorkOrder>();
                listWorkOrder = pTechnicalObjectBO.SearchWorkOrdesWithTechnicalObject(modelFilter);

                if (listWorkOrder.Count > 0)
                {
                     result = Json(listWorkOrder);
                }
                
            }
            catch (Exception ex)
            {
                result = Error(ex);
            }

            return Task.FromResult(result);
        }
        #endregion


    }
}
