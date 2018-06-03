using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SOFTTEK.SCMS.SRA.Controllers.PM
{
    public class MaterialController : BaseApiController
    {
        #region Action

        #region GET method
        [ActionName("WorkOrder")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public Task<IHttpActionResult> Get(int id)
        { 
            IHttpActionResult result = NotFound();
            if (id == null || id == 0)
            {
                result = Conflict();
                return Task.FromResult(result);
            }
            var modelTask = new SOFTTEK.SCMS.Entity.PM.Task();
            modelTask.Identifier = Convert.ToInt32(id);
            var modelFilter = new SOFTTEK.SCMS.Entity.PM.Material();
            modelFilter.Task = modelTask;
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

                List<SOFTTEK.SCMS.Entity.PM.Material> listMaterial = new List<SOFTTEK.SCMS.Entity.PM.Material>();

                SOFTTEK.SCMS.Business.PM.MaterialBO pMaterialBO = new Business.PM.MaterialBO(ctx);
                listMaterial = pMaterialBO.SearchMaterial(modelFilter);

                if (listMaterial.Count > 0)
                {
                    result = Json(listMaterial);
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
        public Task<IHttpActionResult> Post([FromBody]SOFTTEK.SCMS.Entity.PM.Material modelInsert)
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

                SOFTTEK.SCMS.Business.PM.MaterialBO MaterialBO = new Business.PM.MaterialBO(ctx);
                SOFTTEK.SCMS.Entity.PM.Material registeredActivity = MaterialBO.RegisterMaterial(modelInsert);
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
        public Task<IHttpActionResult> Put([FromUri]string id, [FromBody]SOFTTEK.SCMS.Entity.PM.Material modelUpdate)
        {

            IHttpActionResult result = NotFound();

            if ((id == null || id == "0") || (modelUpdate == null))
            {
                result = Conflict();
                return Task.FromResult(result);
            }
            var modelFilter = new SOFTTEK.SCMS.Entity.PM.Material();
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

                SOFTTEK.SCMS.Business.PM.MaterialBO activitiesRegisterBO = new Business.PM.MaterialBO(ctx);
                SOFTTEK.SCMS.Entity.PM.Material updatedActivity = activitiesRegisterBO.UpdateMaterial(modelFilter, modelUpdate);

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

        #region Retrive
        //[ActionName("Retrive")]
        //[SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        //public Task<IHttpActionResult> Retrive()
        //{
        //    IHttpActionResult result = NotFound();

        //    try
        //    {
        //        SOFTTEK.SCMS.Foundation.Business.BusinessContext ctx = new SOFTTEK.SCMS.Foundation.Business.BusinessContext
        //        {
        //            SecurityContext = new Foundation.Security.SecurityContext
        //            {
        //                DeviceID = GetDeviceIdentifier(),
        //                ClientID = GetToken().Identifier,
        //                AuthorizationTicket = GetToken().Identifier,
        //                AppID = new System.Configuration.AppSettingsReader().GetValue("S_SRA_APP_idENTIFIER", typeof(string)).ToString()
        //            }
        //        };


        //        List<SOFTTEK.SCMS.Entity.PM.Material> listMaterial = new List<SOFTTEK.SCMS.Entity.PM.Material>();

        //        SOFTTEK.SCMS.Business.PM.MaintenanceBO MaintenanceBO = new Business.PM.MaintenanceBO(ctx);
        //        listMaterial = MaintenanceBO.RetrievePMMaterials();

        //        if (listMaterial.Count > 0)
        //        {
        //            result = Json(listMaterial);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result = Error(ex);
        //    }

        //    return Task.FromResult(result);
        //}
        #endregion


        #endregion
    }
}
