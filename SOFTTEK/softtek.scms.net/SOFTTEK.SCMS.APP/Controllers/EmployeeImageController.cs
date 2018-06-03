using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
namespace SOFTTEK.SCMS.SRA.Controllers
{
    [Obsolete("Use EmployeeController instead.")]
    public class EmployeeImageController : BaseApiController
    {
        [Obsolete("Use EmployeeController instead with GetEmployeeImageForToken method.")]
        [ActionName("DefaultAction")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public HttpResponseMessage Get()
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

            if (employee != null)
            {
                Uri ftpAddress = null;
                if (Uri.TryCreate(employee.ImageURL, UriKind.Absolute, out ftpAddress))
                {
                    if (ftpAddress.Scheme != Uri.UriSchemeFtp)
                    {
                        return new HttpResponseMessage(HttpStatusCode.Conflict);
                    }
                    WebClient request = new WebClient();


                    byte[] imageData = request.DownloadData(ftpAddress);
                    var ms = new System.IO.MemoryStream(imageData);
                    var t = new StringContent(Convert.ToBase64String(imageData), System.Text.UTF8Encoding.UTF8, "image/jpg");

                    var result = new HttpResponseMessage(HttpStatusCode.OK);
                    result.Content = t;
                    return result;
                }
                return new HttpResponseMessage(HttpStatusCode.Conflict);
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound);

        }

        [Obsolete("Use EmployeeController instead with GetEmployeeImageByEmployeeId method.")]
        [ActionName("DefaultAction")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public async Task<HttpResponseMessage> Get(string id)
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

        [Obsolete("Use EmployeeController instead with PostEmployeeImageForEmployeeId method.")]
        [ActionName("DefaultAction")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public async Task<IHttpActionResult> Post(string id)
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
            await Request.Content.ReadAsMultipartAsync(mpmsProvider).ContinueWith((t) => {
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
                //bool assigned = employeeBO.SetEmployeeImage(ftpURL, intHelper);

                return Ok();
            }
            else
            {
                return Conflict();
            }
            
        }

        
        


    }
}
