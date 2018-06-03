using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SOFTTEK.SCMS.SRA.Controllers
{
    [Obsolete("Use EmployeeController instead.")]
    public class IdentityController : BaseApiController
    {
        [Obsolete("Use EmployeeController instead with GetEmployeeForToken method.")]
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

            SOFTTEK.SCMS.Business.SRA.EmployeeBO employeeBO = new Business.SRA.EmployeeBO(ctx);
            SOFTTEK.SCMS.Entity.Shared.Employee employee = employeeBO.GetEmployeeInfoForToken();
            employee.ImageURL = string.Empty;

            if (employee != null)
            {
                result = Json(employee);
            }

            return result;
        }

    }
}
