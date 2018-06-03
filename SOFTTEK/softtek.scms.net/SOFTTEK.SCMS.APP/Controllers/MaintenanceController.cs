using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SOFTTEK.SCMS.SRA.Controllers
{
    public class MaintenanceController : BaseApiController
    {


        [ActionName("Schedule")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public Task<IHttpActionResult> GetDeviceAvailableWorkOrders()
        {

            try
            {
                IHttpActionResult result = Json<string>(string.Empty);

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                IHttpActionResult error = Error(ex);
                return Task.FromResult(error);
            }
        }

        [ActionName("Schedule")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public Task<IHttpActionResult> PostAttendedWorkOrder([FromBody] object workOrder)
        {

            try
            {
                IHttpActionResult result = Json<string>(string.Empty);

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                IHttpActionResult error = Error(ex);
                return Task.FromResult(error);
            }
        }

        [ActionName("Schedule")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public Task<IHttpActionResult> PutAttendedWorkOrder(string id, [FromBody] object workOrder)
        {

            try
            {
                IHttpActionResult result = Json<string>(string.Empty);

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                IHttpActionResult error = Error(ex);
                return Task.FromResult(error);
            }
        }

        [ActionName("Advice")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public Task<IHttpActionResult> GetWorkOrderAdvices(string id)
        {

            try
            {
                IHttpActionResult result = Json<string>(string.Empty);

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                IHttpActionResult error = Error(ex);
                return Task.FromResult(error);
            }
        }

        [ActionName("Advice")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public Task<IHttpActionResult> PostWorkOrderAdvice(string id, [FromBody] object advice)
        {

            try
            {
                IHttpActionResult result = Json<string>(string.Empty);

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                IHttpActionResult error = Error(ex);
                return Task.FromResult(error);
            }
        }

        [ActionName("Advice")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public Task<IHttpActionResult> PutAdvice(string id, [FromBody] object advice)
        {
            try
            {

                IHttpActionResult result = Json<string>(string.Empty);

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                IHttpActionResult error = Error(ex);
                return Task.FromResult(error);
            }
        }

        [ActionName("Meassurement")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public Task<IHttpActionResult> GetWorkOrderMeassurements(string id)
        {
            try
            {

                IHttpActionResult result = Json<string>(string.Empty);

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                IHttpActionResult error = Error(ex);
                return Task.FromResult(error);
            }
        }

        [ActionName("Meassurement")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public Task<IHttpActionResult> PostWorkOrderMeassurement(string id, [FromBody] object meassurement)
        {

            try
            {
                IHttpActionResult result = Json<string>(string.Empty);

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                IHttpActionResult error = Error(ex);
                return Task.FromResult(error);
            }
        }

        [ActionName("Meassurement")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public Task<IHttpActionResult> PutMeassurement(string id, [FromBody] object meassurement)
        {

            try
            {
                IHttpActionResult result = Json<string>(string.Empty);

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                IHttpActionResult error = Error(ex);
                return Task.FromResult(error);
            }
        }

        [ActionName("Replacement")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public Task<IHttpActionResult> GetWorkOrderReplacements(string id)
        {

            try
            {
                IHttpActionResult result = Json<string>(string.Empty);

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                IHttpActionResult error = Error(ex);
                return Task.FromResult(error);
            }
        }

        [ActionName("Replacement")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public Task<IHttpActionResult> PostWorkOrderReplacement(string id, [FromBody] object replacement)
        {

            try
            {
                IHttpActionResult result = Json<string>(string.Empty);

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                IHttpActionResult error = Error(ex);
                return Task.FromResult(error);
            }
        }

        [ActionName("Replacement")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public Task<IHttpActionResult> PutReplacement(string id, [FromBody] object replacement)
        {

            try
            {
                IHttpActionResult result = Json<string>(string.Empty);

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                IHttpActionResult error = Error(ex);
                return Task.FromResult(error);
            }
        }

        [ActionName("Access")]
        public Task<IHttpActionResult> GetDeviceUsers()
        {

            try
            {
                IHttpActionResult result = Json<string>(string.Empty);
                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                IHttpActionResult error = Error(ex);
                return Task.FromResult(error);
            }
        }

    }
}