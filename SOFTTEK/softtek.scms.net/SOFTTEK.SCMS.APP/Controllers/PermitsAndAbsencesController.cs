using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SOFTTEK.SCMS.SRA.Controllers
{
    public class PermitsAndAbsencesController : BaseApiController
    {
        #region Action

        #region GET method
        [ActionName("DefaultAction")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public Task<IHttpActionResult> Get(string id)
        {
            int employeeID;

            IHttpActionResult result = NotFound();

            if (!int.TryParse(id, out employeeID))
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

                List<SOFTTEK.SCMS.Entity.SRA.PermitsAndAbsences> listPermitsAndAbsences = new List<SOFTTEK.SCMS.Entity.SRA.PermitsAndAbsences>();

                SOFTTEK.SCMS.Business.SRA.PermitsAndAbsencesBO pPermitsAndAbsencesBO = new Business.SRA.PermitsAndAbsencesBO(ctx);
                listPermitsAndAbsences = pPermitsAndAbsencesBO.GetPermitsAndAbsencesForEmployeeId(employeeID);

                if (listPermitsAndAbsences.Count > 0)
                {
                    result = Json(listPermitsAndAbsences);
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
        public Task<IHttpActionResult> Post([FromBody]SOFTTEK.SCMS.Entity.SRA.PermitsAndAbsences activityPerAndAbs)
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

                SOFTTEK.SCMS.Business.SRA.PermitsAndAbsencesBO permitsAndAbsencesBO = new Business.SRA.PermitsAndAbsencesBO(ctx);
                List<SOFTTEK.SCMS.Entity.SRA.PermitsAndAbsences> registeredActivity = permitsAndAbsencesBO.RegisterPermitsAnsAbsences(activityPerAndAbs);
                if (registeredActivity.Count > 0)
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
        public Task<IHttpActionResult> Put(string id, [FromBody]SOFTTEK.SCMS.Entity.SRA.PermitsAndAbsences activityPerAndAbs)
        {
            int activityID;

            IHttpActionResult result = NotFound();

            if (!int.TryParse(id, out activityID))
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

                SOFTTEK.SCMS.Business.SRA.PermitsAndAbsencesBO activitiesRegisterBO = new Business.SRA.PermitsAndAbsencesBO(ctx);
                List<SOFTTEK.SCMS.Entity.SRA.PermitsAndAbsences> updatedActivity = activitiesRegisterBO.UpdatePermitsAnsAbsences(activityID, activityPerAndAbs);

                if (updatedActivity.Count > 0)
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
