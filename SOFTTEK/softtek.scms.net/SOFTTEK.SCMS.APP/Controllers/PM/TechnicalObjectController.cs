using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SOFTTEK.SCMS.SRA.Controllers.PM
{
    public class TechnicalObjectController : BaseApiController
    {
        #region Action

        #region GET method
        [ActionName("Barcode")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(false)]
        public Task<IHttpActionResult> GetBarcode(string id)
        {
            IHttpActionResult result = NotFound();

            if (id == null || id == "")
            {
                result = Conflict();
                return Task.FromResult(result);
            }
            var modelFilter = new SOFTTEK.SCMS.Entity.PM.TechnicalObject();
            modelFilter.ExternalIdentifier = id;

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

                List<SOFTTEK.SCMS.Entity.PM.TechnicalObject> listTechnicalObject = new List<SOFTTEK.SCMS.Entity.PM.TechnicalObject>();

                SOFTTEK.SCMS.Business.PM.TechnicalObjectBO pTechnicalObjectBO = new Business.PM.TechnicalObjectBO(ctx);
                listTechnicalObject = pTechnicalObjectBO.SearchTechnicalObject(modelFilter);

                if (listTechnicalObject.Count > 0)
                {
                    result = Json(listTechnicalObject);
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
        public Task<IHttpActionResult> Post([FromBody]SOFTTEK.SCMS.Entity.PM.TechnicalObject modelInsert)
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

                SOFTTEK.SCMS.Business.PM.TechnicalObjectBO TechnicalObjectBO = new Business.PM.TechnicalObjectBO(ctx);
                SOFTTEK.SCMS.Entity.PM.TechnicalObject registeredActivity = TechnicalObjectBO.RegisterTechnicalObject(modelInsert);
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
        public Task<IHttpActionResult> Put([FromUri]string id, [FromBody]SOFTTEK.SCMS.Entity.PM.TechnicalObject modelUpdate)
        {

            IHttpActionResult result = NotFound();

            if ((id == null || id == "0") || (modelUpdate == null))
            {
                result = Conflict();
                return Task.FromResult(result);
            }
            var modelFilter = new SOFTTEK.SCMS.Entity.PM.TechnicalObject();
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

                SOFTTEK.SCMS.Business.PM.TechnicalObjectBO activitiesRegisterBO = new Business.PM.TechnicalObjectBO(ctx);
                SOFTTEK.SCMS.Entity.PM.TechnicalObject updatedActivity = activitiesRegisterBO.UpdateTechnicalObject(modelFilter,modelUpdate);

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

        #region RetrievePMEquipmentAssets
        [ActionName("DefaultAction")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public Task<IHttpActionResult> Get(string barcode)
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

                List<SOFTTEK.SCMS.Entity.PM.TechnicalObject> listTechnicalObject = new List<SOFTTEK.SCMS.Entity.PM.TechnicalObject>();

                SOFTTEK.SCMS.Business.PM.MaintenanceBO MaintenanceBO = new Business.PM.MaintenanceBO(ctx);
                listTechnicalObject = MaintenanceBO.RetrievePMEquipmentAssets();

                if (listTechnicalObject.Count > 0)
                {
                    result = Json(listTechnicalObject);
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
