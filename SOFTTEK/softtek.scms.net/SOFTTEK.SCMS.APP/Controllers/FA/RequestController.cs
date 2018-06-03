using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SOFTTEK.SCMS.SRA.Controllers.FA
{
    public class RequestController : BaseApiController
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
            var modelFilter = new SOFTTEK.SCMS.Entity.FA.Request();
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

                List<SOFTTEK.SCMS.Entity.FA.Request> listRequest = new List<SOFTTEK.SCMS.Entity.FA.Request>();

                SOFTTEK.SCMS.Business.FA.RequestBO pRequestBO = new Business.FA.RequestBO(ctx);
                listRequest = pRequestBO.SearchRequests(modelFilter);

                if (listRequest.Count > 0)
                {
                    result = Json(listRequest);
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
        public Task<IHttpActionResult> Post([FromBody]SOFTTEK.SCMS.Entity.FA.Request modelInsert)
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

                SOFTTEK.SCMS.Business.FA.RequestBO RequestBO = new Business.FA.RequestBO(ctx);
                SOFTTEK.SCMS.Entity.FA.Request registeredActivity = RequestBO.RegisterRequest(modelInsert);
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
        public Task<IHttpActionResult> Put([FromUri]string id, [FromBody]SOFTTEK.SCMS.Entity.FA.Request modelUpdate)
        {

            IHttpActionResult result = NotFound();

            if ((id == null || id == "0") || (modelUpdate == null))
            {
                result = Conflict();
                return Task.FromResult(result);
            }
            var modelFilter = new SOFTTEK.SCMS.Entity.FA.Request();
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

                SOFTTEK.SCMS.Business.FA.RequestBO activitiesRegisterBO = new Business.FA.RequestBO(ctx);
                SOFTTEK.SCMS.Entity.FA.Request updatedActivity = activitiesRegisterBO.UpdateRequest(modelFilter, modelUpdate);

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
