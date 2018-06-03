using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SOFTTEK.SCMS.SRA.Controllers.PM
{
    public class ComponentReplacementController : BaseApiController
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
            var modelFilter = new SOFTTEK.SCMS.Entity.PM.ComponentReplacement();
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

                List<SOFTTEK.SCMS.Entity.PM.ComponentReplacement> listComponentReplacement = new List<SOFTTEK.SCMS.Entity.PM.ComponentReplacement>();

                SOFTTEK.SCMS.Business.PM.ComponentReplacementBO pComponentReplacementBO = new Business.PM.ComponentReplacementBO(ctx);
                listComponentReplacement = pComponentReplacementBO.SearchComponentReplacement(modelFilter);

                if (listComponentReplacement.Count > 0)
                {
                    result = Json(listComponentReplacement);
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
        public Task<IHttpActionResult> Post([FromBody]SOFTTEK.SCMS.Entity.PM.ComponentReplacement modelInsert)
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

                SOFTTEK.SCMS.Business.PM.ComponentReplacementBO ComponentReplacementBO = new Business.PM.ComponentReplacementBO(ctx);
                SOFTTEK.SCMS.Entity.PM.ComponentReplacement registeredActivity = ComponentReplacementBO.RegisterComponentReplacement(modelInsert);
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
        public Task<IHttpActionResult> Put([FromUri]string id, [FromBody]SOFTTEK.SCMS.Entity.PM.ComponentReplacement modelUpdate)
        {

            IHttpActionResult result = NotFound();

            if ((id == null || id == "0") || (modelUpdate == null))
            {
                result = Conflict();
                return Task.FromResult(result);
            }
            var modelFilter = new SOFTTEK.SCMS.Entity.PM.ComponentReplacement();
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

                SOFTTEK.SCMS.Business.PM.ComponentReplacementBO activitiesRegisterBO = new Business.PM.ComponentReplacementBO(ctx);
                SOFTTEK.SCMS.Entity.PM.ComponentReplacement updatedActivity = activitiesRegisterBO.UpdateComponentReplacement(modelFilter, modelUpdate);

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

        #region SubmitComponentReplacement
        //[ActionName("DefaultAction")]
        //[SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        //public Task<IHttpActionResult> PostComponentReplacement([FromBody]SOFTTEK.SCMS.Entity.PM.ComponentReplacement modelInsert)
        //{
        //    IHttpActionResult result = Conflict();

        //    try
        //    {
        //        SOFTTEK.SCMS.Foundation.Business.BusinessContext ctx = new SOFTTEK.SCMS.Foundation.Business.BusinessContext
        //        {
        //            SecurityContext = new Foundation.Security.SecurityContext
        //            {
        //                DeviceID = GetDeviceIdentifier(),
        //                ClientID = GetToken().Identifier,
        //                AuthorizationTicket = GetToken().Identifier,
        //                AppID = new System.Configuration.AppSettingsReader().GetValue("S_SRA_APP_idENTIFIER", typeof(string)).ToString()
        //            }
        //        };

        //        SOFTTEK.SCMS.Business.PM.MaintenanceBO MaintenanceBO = new Business.PM.MaintenanceBO(ctx);
        //        SOFTTEK.SCMS.Entity.PM.ComponentReplacement registeredActivity = MaintenanceBO.SubmitComponentReplacement(modelInsert);
        //        if (registeredActivity != null)
        //        {
        //            result = Json(registeredActivity);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result = Error(ex);
        //    }

        //    return Task.FromResult(result);
        //}
        #endregion

        #endregion
    }
}
