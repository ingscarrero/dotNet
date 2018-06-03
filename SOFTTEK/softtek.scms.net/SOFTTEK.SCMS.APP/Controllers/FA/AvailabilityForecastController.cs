using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SOFTTEK.SCMS.SRA.Controllers.FA
{
    public class AvailabilityForecastController : BaseApiController
    {
        #region Action

        #region GET method
        [ActionName("DefaultAction")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public Task<IHttpActionResult> Get([FromBody]SOFTTEK.SCMS.Entity.FA.AvailabilityForecast modelFilter)
        {
            IHttpActionResult result = NotFound();

            if (modelFilter == null)
            {
                result = Conflict();
                return Task.FromResult(result);
            }

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

                List<SOFTTEK.SCMS.Entity.FA.AvailabilityForecast> listAvailabilityForecast = new List<SOFTTEK.SCMS.Entity.FA.AvailabilityForecast>();

                SOFTTEK.SCMS.Business.FA.AvailabilityForecastBO pAvailabilityForecastBO = new Business.FA.AvailabilityForecastBO(ctx);
                listAvailabilityForecast = pAvailabilityForecastBO.SearchAvailabilityForecasts(modelFilter);

                if (listAvailabilityForecast.Count > 0)
                {
                    result = Json(listAvailabilityForecast);
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
        public Task<IHttpActionResult> Post([FromBody]SOFTTEK.SCMS.Entity.FA.AvailabilityForecast modelInsert)
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

                SOFTTEK.SCMS.Business.FA.AvailabilityForecastBO AvailabilityForecastBO = new Business.FA.AvailabilityForecastBO(ctx);
                SOFTTEK.SCMS.Entity.FA.AvailabilityForecast registeredActivity = AvailabilityForecastBO.RegisterAvailabilityForecast(modelInsert);
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
        public Task<IHttpActionResult> Put([FromBody]SOFTTEK.SCMS.Entity.FA.AvailabilityForecast modelFilter, [FromBody]SOFTTEK.SCMS.Entity.FA.AvailabilityForecast modelUpdate)
        {

            IHttpActionResult result = NotFound();

            if ((modelFilter == null) || (modelUpdate == null))
            {
                result = Conflict();
                return Task.FromResult(result);
            }

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

                SOFTTEK.SCMS.Business.FA.AvailabilityForecastBO activitiesRegisterBO = new Business.FA.AvailabilityForecastBO(ctx);
                SOFTTEK.SCMS.Entity.FA.AvailabilityForecast updatedActivity = activitiesRegisterBO.UpdateAvailabilityForecast(modelFilter, modelUpdate);

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

        #endregion
    }
}
