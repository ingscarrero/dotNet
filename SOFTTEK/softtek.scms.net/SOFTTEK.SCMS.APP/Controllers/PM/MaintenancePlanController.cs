using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SOFTTEK.SCMS.SRA.Controllers.PM
{
    public class MaintenancePlanController : BaseApiController
    {
        #region Action

        #region GET method
        [ActionName("DefaultAction")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public Task<IHttpActionResult> Get(string id)
        {
            IHttpActionResult result = NotFound();

            if (id == null || id == "0")
            {
                result = Conflict();
                return Task.FromResult(result);
            }
            var modelFilter = new SOFTTEK.SCMS.Entity.PM.MaintenancePlan();
            modelFilter.Identifier = Convert.ToInt64(id);

            try
            {
                SOFTTEK.SCMS.Foundation.Business.BusinessContext ctx = new SOFTTEK.SCMS.Foundation.Business.BusinessContext
                {
                    SecurityContext = new Foundation.Security.SecurityContext
                    {
                        DeviceID = GetDeviceIdentifier(),
                        ClientID = GetToken().UserIS,
                        AuthorizationTicket = GetToken().Identifier,
                        AppID = new System.Configuration.AppSettingsReader().GetValue("S_SRA_APP_idENTIFIER", typeof(string)).ToString()
                    }
                };

                List<SOFTTEK.SCMS.Entity.PM.MaintenancePlan> listMaintenancePlan = new List<SOFTTEK.SCMS.Entity.PM.MaintenancePlan>();

                SOFTTEK.SCMS.Business.PM.MaintenancePlanBO pMaintenancePlanBO = new Business.PM.MaintenancePlanBO(ctx);
                listMaintenancePlan = pMaintenancePlanBO.SearchMaintenancePlan(modelFilter);

                if (listMaintenancePlan.Count > 0)
                {
                    result = Json(listMaintenancePlan);
                }
            }
            catch (Exception ex)
            {
                result = Error(ex);
            }

            return Task.FromResult(result);
        }
        #endregion

        #region Post method
        [ActionName("DefaultAction")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public Task<IHttpActionResult> Post([FromBody]SOFTTEK.SCMS.Entity.PM.MaintenancePlan modelInsert)
        {
            IHttpActionResult result = Conflict();

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

                SOFTTEK.SCMS.Business.PM.MaintenancePlanBO MaintenancePlanBO = new Business.PM.MaintenancePlanBO(ctx);
                SOFTTEK.SCMS.Entity.PM.MaintenancePlan registeredActivity = MaintenancePlanBO.RegisterMaintenancePlan(modelInsert);
                if (registeredActivity != null)
                {
                    result = Json(registeredActivity);
                }
            }
            catch (Exception ex)
            {
                result = Error(ex);
            }

            return Task.FromResult(result);
        }
        #endregion

        #region Put method
        [ActionName("DefaultAction")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public Task<IHttpActionResult> Put([FromUri]string id, [FromBody]SOFTTEK.SCMS.Entity.PM.MaintenancePlan modelUpdate)
        {

            IHttpActionResult result = NotFound();

            if ((id == null || id == "0") || (modelUpdate == null))
            {
                result = Conflict();
                return Task.FromResult(result);
            }
            var modelFilter = new SOFTTEK.SCMS.Entity.PM.MaintenancePlan();
            modelFilter.Identifier = Convert.ToInt64(id);

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

                SOFTTEK.SCMS.Business.PM.MaintenancePlanBO activitiesRegisterBO = new Business.PM.MaintenancePlanBO(ctx);
                SOFTTEK.SCMS.Entity.PM.MaintenancePlan updatedActivity = activitiesRegisterBO.UpdateMaintenancePlan(modelFilter, modelUpdate);

                if (updatedActivity != null)
                {
                    result = Json(updatedActivity);
                }
            }
            catch (Exception ex)
            {
                result = Error(ex);
            }

            return Task.FromResult(result);
        }
        #endregion

        #region GET method
        [ActionName("WorkOrder")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public Task<IHttpActionResult> Get(long id)
        {
            IHttpActionResult result = NotFound();

            if (id == 0)
            {
                result = Conflict();
                return Task.FromResult(result);
            }
            var modelFilter = new SOFTTEK.SCMS.Entity.PM.MaintenancePlan();
            modelFilter.WorkOrder = Convert.ToInt64(id);

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

                List<SOFTTEK.SCMS.Entity.PM.MaintenancePlan> listMaintenancePlan = new List<SOFTTEK.SCMS.Entity.PM.MaintenancePlan>();

                SOFTTEK.SCMS.Business.PM.MaintenancePlanBO pMaintenancePlanBO = new Business.PM.MaintenancePlanBO(ctx);
                listMaintenancePlan = pMaintenancePlanBO.SearchMaintenancePlan(modelFilter);

                if (listMaintenancePlan.Count > 0)
                {
                    result = Json(listMaintenancePlan);
                }
            }
            catch (Exception ex)
            {
                result = Error(ex);
            }

            return Task.FromResult(result);
        }
        #endregion
        #endregion

    }
}
