using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Threading.Tasks;
using System.IO;

namespace SOFTTEK.SCMS.SRA.Controllers
{
    public class EmployeeController : BaseApiController
    {
        [ActionName("DefaultAction")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public Task<IHttpActionResult> GetEmployeeForToken()
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
                SOFTTEK.SCMS.Entity.Shared.Employee employee = employeeBO.GetEmployeeInfoForToken();
                employee.ImageURL = string.Empty;

                if (employee != null)
                {
                    result = Json(employee);
                }
            }
            catch (Exception ex)
            {
                result = Error(ex);
            }

            return Task.FromResult(result);
        }

        [ActionName("DefaultAction")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public Task<IHttpActionResult> GetEmployeeById(string id)
        {
            IHttpActionResult result = NotFound();

            int employeeID = 0;
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

                SOFTTEK.SCMS.Business.SRA.EmployeeBO employeeBO = new Business.SRA.EmployeeBO(ctx);
                SOFTTEK.SCMS.Entity.Shared.Employee employee = employeeBO.GetEmployeeInfoById(employeeID);
                employee.ImageURL = string.Empty;

                if (employee != null)
                {
                    result = Json(employee);
                }
            }
            catch (Exception ex)
            {
                result = Error(ex);
            }

            return Task.FromResult(result);

        }



        [ActionName("Image")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public Task<HttpResponseMessage> GetEmployeeImageForToken()
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.NotFound);
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
                SOFTTEK.SCMS.Entity.Shared.Employee employee = employeeBO.GetEmployeeInfoForToken();

                if (employee == null)
                {
                    return Task.FromResult(result);
                }

                Uri ftpAddress = null;
                if (!Uri.TryCreate(employee.ImageURL, UriKind.Absolute, out ftpAddress))
                {
                    result = new HttpResponseMessage(HttpStatusCode.Conflict);
                    return Task.FromResult(result);
                }

                if (ftpAddress.Scheme != Uri.UriSchemeFtp)
                {
                    result = new HttpResponseMessage(HttpStatusCode.Conflict);
                    return Task.FromResult(result);
                }


                WebClient request = new WebClient();

                byte[] imageData = request.DownloadData(ftpAddress);
                var ms = new System.IO.MemoryStream(imageData);
                var t = new StringContent(Convert.ToBase64String(imageData), System.Text.UTF8Encoding.UTF8, "image/jpg");

                result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = t;
            }
            catch (Exception ex)
            {
                result = new HttpResponseMessage(HttpStatusCode.Conflict);
                result.Content = new StringContent(ex.Message);
            }
            return Task.FromResult(result);
        }

        [ActionName("Image")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public async Task<HttpResponseMessage> GetEmployeeImageByEmployeeId(string id)
        {

            int employeeID = 0;
            if (int.TryParse(id, out employeeID))
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
                SOFTTEK.SCMS.Entity.Shared.Employee employee = employeeBO.GetEmployeeInfoById(employeeID);

                if (employee != null)
                {
                    Uri ftpAddress = null;
                    if (Uri.TryCreate(employee.ImageURL, UriKind.Absolute, out ftpAddress))
                    {
                        if (ftpAddress.Scheme != Uri.UriSchemeFtp)
                        {
                            return new HttpResponseMessage(HttpStatusCode.Conflict);
                        }

                        return await SOFTTEK.SCMS.Foundation.Web.Ftp.GetContent(ftpAddress);
                    }
                    return new HttpResponseMessage(HttpStatusCode.Conflict);
                }
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            return new HttpResponseMessage(HttpStatusCode.Conflict);
        }


        [ActionName("Image")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public async Task<IHttpActionResult> PostEmployeeImageForEmployeeId(string id)
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            int intHelper = -1;
            if (!int.TryParse(id, out intHelper))
            {
                throw new Exception("Invalid request", new ArgumentException("Invalid id for employee."));
            }

            string ftpURL = string.Empty;
            bool contentUploaded = false;

            MultipartMemoryStreamProvider mpmsProvider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(mpmsProvider).ContinueWith((t) =>
            {
                mpmsProvider = t.Result;
                foreach (HttpContent item in mpmsProvider.Contents)
                {

                    string fileName = string.Concat(Guid.NewGuid(), Path.GetExtension(item.Headers.ContentDisposition.FileName.Trim('\"')));
                    Uri ftpAddress = null;

                    ftpURL = System.Configuration.ConfigurationManager.AppSettings["employee_ftp_url"];

                    ftpURL = string.Concat(ftpURL, fileName);

                    if (Uri.TryCreate(ftpURL, UriKind.Absolute, out ftpAddress))
                    {
                        if (ftpAddress.Scheme != Uri.UriSchemeFtp)
                        {
                            throw new HttpResponseException(HttpStatusCode.Conflict);
                        }


                        Task<bool> uploadToFTP = SOFTTEK.SCMS.Foundation.Web.Ftp.UploadHttpContent(item, ftpAddress);
                        contentUploaded = uploadToFTP.Result;
                    }
                }
            });

            if (contentUploaded)
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
                // assigned = employeeBO.SetEmployeeImage(ftpURL, intHelper);

                return Ok();
            }
            else
            {
                return Conflict();
            }

        }




    }
}