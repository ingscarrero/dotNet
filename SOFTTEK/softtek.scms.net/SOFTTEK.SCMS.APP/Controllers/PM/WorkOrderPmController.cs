using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SOFTTEK.SCMS.SRA.Controllers.PM
{
    public class WorkOrderPmController : BaseApiController
    {
        #region Action

        #region GET method
        [ActionName("DefaultAction")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public Task<IHttpActionResult> Get()
        {
            IHttpActionResult result = NotFound();

            var modelFilter = new SOFTTEK.SCMS.Entity.PM.WorkOrder();

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

                List<SOFTTEK.SCMS.Entity.PM.WorkOrder> listWorkOrder = new List<SOFTTEK.SCMS.Entity.PM.WorkOrder>();

                SOFTTEK.SCMS.Business.PM.WorkOrderBO pWorkOrderBO = new Business.PM.WorkOrderBO(ctx);
                listWorkOrder = pWorkOrderBO.SearchWorkOrder(modelFilter);

                if (listWorkOrder.Count > 0)
                {
                    result = Json(listWorkOrder);
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
        //[ActionName("DefaultAction")]
        //[SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        //public Task<IHttpActionResult> Post([FromBody]SOFTTEK.SCMS.Entity.PM.WorkOrder modelInsert)
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

        //        SOFTTEK.SCMS.Business.PM.WorkOrderBO WorkOrderBO = new Business.PM.WorkOrderBO(ctx);
        //        SOFTTEK.SCMS.Entity.PM.WorkOrder registeredActivity = WorkOrderBO.RegisterWorkOrder(modelInsert);
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
        #endregion

        #region Put method
        [ActionName("DefaultAction")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public Task<IHttpActionResult> Put([FromUri]string id, [FromBody]SOFTTEK.SCMS.Entity.PM.WorkOrder modelUpdate)
        {

            IHttpActionResult result = NotFound();

            if ((id == null || id == "0") || (modelUpdate == null))
            {
                result = Conflict();
                return Task.FromResult(result);
            }
            var modelFilter = new SOFTTEK.SCMS.Entity.PM.WorkOrder();
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

                SOFTTEK.SCMS.Business.PM.WorkOrderBO activitiesRegisterBO = new Business.PM.WorkOrderBO(ctx);
                SOFTTEK.SCMS.Entity.PM.WorkOrder updatedActivity = activitiesRegisterBO.UpdateWorkOrder(modelFilter, modelUpdate);

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

        #region PostWorkOrder
        [ActionName("DefaultAction")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public Task<IHttpActionResult> PostWorkOrder([FromBody]SOFTTEK.SCMS.Entity.PM.WorkOrder modelInsert)
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
                SOFTTEK.SCMS.Entity.PM.WorkOrder registeredActivity = MaintenanceBO.SubmitExecutedMaintenanceWorkOrder(modelInsert);
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


        #region GetWorkOrders
        [ActionName("WorkOrder")]
        [SCMS.SRA.Filters.SCMSApiAuthenticationFilter(true)]
        public Task<IHttpActionResult> Get(long id)
        {
            IHttpActionResult result = NotFound();
            try
            {
                var modelFilter = new SOFTTEK.SCMS.Entity.PM.WorkOrder();
                modelFilter.ExternalIdentifier = id.ToString();
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
                List<SOFTTEK.SCMS.Entity.PM.WorkOrder> listWorkOrder = new List<SOFTTEK.SCMS.Entity.PM.WorkOrder>();
                if (id == 1)
                {
                    List<SOFTTEK.SCMS.Entity.PM.TechnicalObject> listTechnicalObject = new List<Entity.PM.TechnicalObject>();
                    SOFTTEK.SCMS.Business.PM.MaintenanceBO MaintenanceBO = new Business.PM.MaintenanceBO(ctx);
                    listTechnicalObject = MaintenanceBO.RetrievePMTechnicalObjects();
                    listWorkOrder = MaintenanceBO.RetrievePendingWorkOrdersForDevice();
                }
                else
                {
                    SOFTTEK.SCMS.Business.PM.WorkOrderBO pWorkOrderBO = new Business.PM.WorkOrderBO(ctx);
                    listWorkOrder = pWorkOrderBO.SearchWorkOrder(modelFilter);
                }

                if (listWorkOrder.Count > 0)
                {
                    result = Json(listWorkOrder);
                }
            }
            catch (Exception ex)
            {
                result = Error(ex);
            }

            return Task.FromResult(result);
        }
        //BarcodeWorkOrders
        #endregion
        
        #endregion
    }
}
