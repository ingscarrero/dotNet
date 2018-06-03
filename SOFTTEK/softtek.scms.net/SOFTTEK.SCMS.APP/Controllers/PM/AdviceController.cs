using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SOFTTEK.SCMS.SRA.Controllers.PM
{
    public class AdviceController : BaseApiController
    {
        #region Action

        #region GET method
        [ActionName("DefaultAction")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public Task<IHttpActionResult> Get(string id)
        {
            //[FromBody]SOFTTEK.SCMS.Entity.PM.Advice modelFilter
            IHttpActionResult result = NotFound();

            if (id == null || id == "0")
            {
                result = Conflict();
                return Task.FromResult(result);
            }
            var modelFilter = new SOFTTEK.SCMS.Entity.PM.Advice();
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

                List<SOFTTEK.SCMS.Entity.PM.Advice> listAdvice = new List<SOFTTEK.SCMS.Entity.PM.Advice>();

                SOFTTEK.SCMS.Business.PM.AdviceBO pAdviceBO = new Business.PM.AdviceBO(ctx);
                listAdvice = pAdviceBO.SearchAdvice(modelFilter);

                if (listAdvice.Count > 0)
                {
                    result = Json(listAdvice);
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
        //[ActionName("DefaultAction")]
        //[SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        //public Task<IHttpActionResult> Post([FromBody]SOFTTEK.SCMS.Entity.PM.Advice modelInsert)
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

        //        SOFTTEK.SCMS.Business.PM.AdviceBO AdviceBO = new Business.PM.AdviceBO(ctx);
        //        SOFTTEK.SCMS.Entity.PM.Advice registeredActivity = AdviceBO.RegisterAdvice(modelInsert);
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

        #region Put method

        [ActionName("DefaultAction")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public Task<IHttpActionResult> Post([FromUri]int id, [FromBody]SOFTTEK.SCMS.Entity.PM.Advice modelUpdate)
        {
            //[FromBody]SOFTTEK.SCMS.Entity.PM.Advice modelUpdate,[FromBody]SOFTTEK.SCMS.Entity.PM.Advice modelFilter
            IHttpActionResult result = Conflict();
            //if ((id == null || id == "0") || (modelUpdate == null))
            //{
            //    return Task.FromResult(result);
            //}
            var modelFilter = new SOFTTEK.SCMS.Entity.PM.Advice();
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

                SOFTTEK.SCMS.Business.PM.AdviceBO activitiesRegisterBO = new Business.PM.AdviceBO(ctx);
                SOFTTEK.SCMS.Entity.PM.Advice updatedActivity = activitiesRegisterBO.UpdateAdvice(modelFilter, modelUpdate);

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


        #region SubmitAdvice
        [ActionName("DefaultAction")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public Task<IHttpActionResult> PostAdvice([FromBody]SOFTTEK.SCMS.Entity.PM.Advice modelInsert)
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

                SOFTTEK.SCMS.Business.PM.MaintenanceBO MaintenanceBO = new Business.PM.MaintenanceBO(ctx);
                SOFTTEK.SCMS.Entity.PM.Advice registeredActivity = MaintenanceBO.SubmitAdvise(modelInsert);
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

        #endregion
    }
}
