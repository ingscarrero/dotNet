using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SOFTTEK.SCMS.SRA.Controllers
{
    public class ActivityController : BaseApiController
    {

        #region DefaultAction
        // GET api/<controller>/5
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
                        ClientID = GetToken().Identifier,
                        AuthorizationTicket = GetToken().Identifier,
                        AppID = new System.Configuration.AppSettingsReader().GetValue("S_SRA_APP_idENTIFIER", typeof(string)).ToString()
                    }
                };

                List<SOFTTEK.SCMS.Business.Entity.SRA.Week<SOFTTEK.SCMS.Business.Entity.SRA.ARSDetailedActivity>> activitiesForWeeks = new List<Business.Entity.SRA.Week<SOFTTEK.SCMS.Business.Entity.SRA.ARSDetailedActivity>>();

                SOFTTEK.SCMS.Business.SRA.ActivityBO activitiesRegisterBO = new Business.SRA.ActivityBO(ctx);
                activitiesForWeeks = activitiesRegisterBO.GetAvailableWeeksInformationForEmployee<SOFTTEK.SCMS.Business.Entity.SRA.ARSDetailedActivity>(employeeID);

                if (activitiesForWeeks.Count > 0)
                {
                    result = Json(activitiesForWeeks);
                }
            }
            catch (Exception ex)
            {
                result = Error(ex);
            }

            return Task.FromResult(result);
        }

        // POST api/<controller>
        [ActionName("DefaultAction")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public Task<IHttpActionResult> Post([FromBody]SOFTTEK.SCMS.Business.Entity.SRA.ARSDetailedActivity activity)
        {

            IHttpActionResult result = Conflict();
            activity.ReportedAt = DateTime.Now;

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

                SOFTTEK.SCMS.Business.SRA.ActivityBO activitiesRegisterBO = new Business.SRA.ActivityBO(ctx);
                SOFTTEK.SCMS.Entity.SRA.DetailedActivity registeredActivity = activitiesRegisterBO.RegisterActivity<SOFTTEK.SCMS.Business.Entity.SRA.ARSDetailedActivity>(activity);
                List<SOFTTEK.SCMS.Business.Entity.SRA.Week<SOFTTEK.SCMS.Business.Entity.SRA.ARSDetailedActivity>> activitiesForWeeks = activitiesRegisterBO.GetAvailableWeeksInformationForEmployee<SOFTTEK.SCMS.Business.Entity.SRA.ARSDetailedActivity>(registeredActivity.Employee.Identifier);
                if (activitiesForWeeks.Count > 0)
                {
                    result = Json(activitiesForWeeks);
                }
            }
            catch (Exception ex)
            {
                result = Error(ex);
            }

            return Task.FromResult(result);
        }

        // PUT api/<controller>/5
        [ActionName("DefaultAction")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public Task<IHttpActionResult> Put(string id, [FromBody]SOFTTEK.SCMS.Business.Entity.SRA.ARSDetailedActivity activity)
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

                SOFTTEK.SCMS.Business.SRA.ActivityBO activitiesRegisterBO = new Business.SRA.ActivityBO(ctx);
                SOFTTEK.SCMS.Entity.SRA.DetailedActivity updatedActivity = activitiesRegisterBO.ChangeActivity<SOFTTEK.SCMS.Business.Entity.SRA.ARSDetailedActivity>(activityID, activity);
                List<SOFTTEK.SCMS.Business.Entity.SRA.Week<SOFTTEK.SCMS.Business.Entity.SRA.ARSDetailedActivity>> activitiesForWeeks = activitiesRegisterBO.GetAvailableWeeksInformationForEmployee<SOFTTEK.SCMS.Business.Entity.SRA.ARSDetailedActivity>(updatedActivity.Employee.Identifier);

                if (activitiesForWeeks.Count > 0)
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

        #region Approval
        
        // GET api/<controller>/5
        [ActionName("Approval")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public Task<IHttpActionResult> GetActivitiesToApproveForProject(string id)
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


                SOFTTEK.SCMS.Business.SRA.EmployeeBO employeeBO = new Business.SRA.EmployeeBO(ctx);
                SOFTTEK.SCMS.Business.SRA.ActivityBO activitiesRegisterBO = new Business.SRA.ActivityBO(ctx);

                SOFTTEK.SCMS.Entity.Shared.Employee approver = employeeBO.GetEmployeeInfoForToken();
                List<SOFTTEK.SCMS.Business.Entity.SRA.ARSDetailedActivity> activitiesToApprove = activitiesRegisterBO.GetActivitiesToApprove<SOFTTEK.SCMS.Business.Entity.SRA.ARSDetailedActivity>(approver.Identifier, id);
                
                if (activitiesToApprove.Count > 0)
                {
                    result = Json(activitiesToApprove);
                }
            }
            catch (Exception ex)
            {
                result = Error(ex);
            }

            return Task.FromResult(result);
        }

        // PUT api/<controller>/5
        [ActionName("Approval")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public Task<IHttpActionResult> PutActivityApproval(string id, [FromBody]string comments)
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

                SOFTTEK.SCMS.Business.SRA.EmployeeBO employeeBO = new Business.SRA.EmployeeBO(ctx);
                SOFTTEK.SCMS.Business.SRA.ActivityBO activitiesRegisterBO = new Business.SRA.ActivityBO(ctx);
                

                SOFTTEK.SCMS.Entity.Shared.Employee approver = employeeBO.GetEmployeeInfoForToken();
                SOFTTEK.SCMS.Entity.SRA.Activity updatedActivity = activitiesRegisterBO.ApproveActivity<SOFTTEK.SCMS.Business.Entity.SRA.ARSDetailedActivity>(activityID, approver, comments);

                List<SOFTTEK.SCMS.Business.Entity.SRA.ARSDetailedActivity> activitiesToApprove = activitiesRegisterBO.GetActivitiesToApprove<SOFTTEK.SCMS.Business.Entity.SRA.ARSDetailedActivity>(approver.Identifier, updatedActivity.Project);

                if (activitiesToApprove.Count > 0)
                {
                    result = Json(activitiesToApprove);
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