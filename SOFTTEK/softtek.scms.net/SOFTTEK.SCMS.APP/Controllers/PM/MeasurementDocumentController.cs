﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SOFTTEK.SCMS.SRA.Controllers.PM
{
    public class MeasurementDocumentController : BaseApiController
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
            var modelFilter = new SOFTTEK.SCMS.Entity.PM.MeasurementDocument();
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

                List<SOFTTEK.SCMS.Entity.PM.MeasurementDocument> listMeasurementDocument = new List<SOFTTEK.SCMS.Entity.PM.MeasurementDocument>();

                SOFTTEK.SCMS.Business.PM.MeasurementDocumentBO pMeasurementDocumentBO = new Business.PM.MeasurementDocumentBO(ctx);
                listMeasurementDocument = pMeasurementDocumentBO.SearchMeasurementDocument(modelFilter);

                if (listMeasurementDocument.Count > 0)
                {
                    result = Json(listMeasurementDocument);
                }
            }
            catch (Exception ex)
            {
                result = Error(ex);
            }

            return Task.FromResult(result);
        }
        #endregion

        //#region Post method
        //[ActionName("DefaultAction")]
        //[SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        //public Task<IHttpActionResult> Post([FromBody]SOFTTEK.SCMS.Entity.PM.MeasurementDocument modelInsert)
        //{
        //    IHttpActionResult result = Conflict();

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

        //        SOFTTEK.SCMS.Business.PM.MeasurementDocumentBO MeasurementDocumentBO = new Business.PM.MeasurementDocumentBO(ctx);
        //        SOFTTEK.SCMS.Entity.PM.MeasurementDocument registeredActivity = MeasurementDocumentBO.RegisterMeasurementDocument(modelInsert);
        //        if (registeredActivity != null)
        //        {
        //            result = Json(registeredActivity);
        //        }
                
        //    }
        //    catch (Exception ex)
        //    {
        //        result = Error(ex);
        //    }

        //    return Task.FromResult(result);
        //}
        //#endregion

        #region Put method
        [ActionName("DefaultAction")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public Task<IHttpActionResult> Put([FromUri]string id, [FromBody]SOFTTEK.SCMS.Entity.PM.MeasurementDocument modelUpdate)
        {

            IHttpActionResult result = NotFound();

            if ((id == null || id == "0") || (modelUpdate == null))
            {
                result = Conflict();
                return Task.FromResult(result);
            }
            var modelFilter = new SOFTTEK.SCMS.Entity.PM.MeasurementDocument();
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

                SOFTTEK.SCMS.Business.PM.MeasurementDocumentBO activitiesRegisterBO = new Business.PM.MeasurementDocumentBO(ctx);
                SOFTTEK.SCMS.Entity.PM.MeasurementDocument updatedActivity = activitiesRegisterBO.UpdateMeasurementDocument(modelFilter, modelUpdate);

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

        #region GET method
        [ActionName("TASK")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public Task<IHttpActionResult> GetExist(long idTask)
        {
            IHttpActionResult result = NotFound();

            if (idTask == 0)
            {
                result = Conflict();
                return Task.FromResult(result);
            }
            var modelFilter = new SOFTTEK.SCMS.Entity.PM.MeasurementDocument();
            modelFilter.Task.Identifier = Convert.ToInt64(idTask);

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

                List<SOFTTEK.SCMS.Entity.PM.MeasurementDocument> listMeasurementDocument = new List<SOFTTEK.SCMS.Entity.PM.MeasurementDocument>();

                SOFTTEK.SCMS.Business.PM.MeasurementDocumentBO pMeasurementDocumentBO = new Business.PM.MeasurementDocumentBO(ctx);
                listMeasurementDocument = pMeasurementDocumentBO.SearchMeasurementDocument(modelFilter);

                if (listMeasurementDocument.Count > 0)
                {
                    result = Json(listMeasurementDocument);
                }
            }
            catch (Exception ex)
            {
                result = Error(ex);
            }

            return Task.FromResult(result);
        }
        #endregion

        #region SubmitMeasurementDocument
        [ActionName("DefaultAction")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public Task<IHttpActionResult> PostMeasurementDocument([FromBody]SOFTTEK.SCMS.Entity.PM.MeasurementDocument modelInsert)
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

                SOFTTEK.SCMS.Business.PM.MaintenanceBO MaintenanceBO = new Business.PM.MaintenanceBO(ctx);
                SOFTTEK.SCMS.Entity.PM.MeasurementDocument registeredActivity = MaintenanceBO.SubmitMeasurementDocument(modelInsert);
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

        #endregion
    }
}
