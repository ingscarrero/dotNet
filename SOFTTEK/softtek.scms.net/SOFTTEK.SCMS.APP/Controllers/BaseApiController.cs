using SOFTTEK.SCMS.Entity.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;

namespace SOFTTEK.SCMS.SRA.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        protected SOFTTEK.SCMS.Entity.Security.Token GetToken( ) 
        {
            string authHeaderValue = null;
            var authRequest = ActionContext.Request.Headers.Authorization;
            if (authRequest != null && !String.IsNullOrEmpty(authRequest.Scheme) && authRequest.Scheme == "Basic")
                authHeaderValue = authRequest.Parameter;
            if (string.IsNullOrEmpty(authHeaderValue))
                return null;

            authHeaderValue = Encoding.Default.GetString(Convert.FromBase64String(authHeaderValue));
            var credentials = authHeaderValue.Split(':');

            if (credentials.Length == 2)
            {
                SOFTTEK.SCMS.Entity.Security.Token token = new SOFTTEK.SCMS.Entity.Security.Token
                {
                    Identifier = credentials[1]
                };
                return token;
            }

            return null;
        }

        protected string GetDeviceIdentifier()
        {
            string authHeaderValue = null;
            var authRequest = ActionContext.Request.Headers.Authorization;
            if (authRequest != null && !String.IsNullOrEmpty(authRequest.Scheme) && authRequest.Scheme == "Basic")
                authHeaderValue = authRequest.Parameter;
            if (string.IsNullOrEmpty(authHeaderValue))
                return null;

            authHeaderValue = Encoding.Default.GetString(Convert.FromBase64String(authHeaderValue));
            var credentials = authHeaderValue.Split(':');

            if (credentials.Length == 2)
            {
                string deviceIdentifier = credentials[0];
                return deviceIdentifier;
            }

            return null;
        }

        public IHttpActionResult Error(Exception ex)
        {
            ApiError apiError = new ApiError();
            int errorCode = 0;
            apiError.Message = "Ops! There was an error at the API. Please try again by validating the API provided information. If the problem persist, please contact us with the provided error information.";
            //apiError.Description = ex.Data["Description"].ToString();
            //apiError.Reason = ex.Data["Reason"].ToString();
            apiError.SupportUrl = System.Configuration.ConfigurationManager.AppSettings.Get("SCMS_Support_URL");
            //apiError.Code = int.TryParse(ex.Data["Code"].ToString(), out errorCode) ? errorCode : -1;

            IHttpActionResult result = Content(HttpStatusCode.Conflict, apiError);
            return result;
        }
    }
}
