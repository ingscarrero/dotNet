using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SCMS.Data;

namespace SOFTTEK.SCMS.Business.PM
{
    public class WorkOrderBO : SOFTTEK.SCMS.Foundation.Business.BusinessObject
    {
        #region Fields
        private PMDataContext dataSource;
        #endregion

        #region Connection 
        public WorkOrderBO(SOFTTEK.SCMS.Foundation.Business.BusinessContext ctx)
            : base(ctx)
        {
        }
        #endregion

        #region SearchWorkOrder
        /// <summary>
        /// Get all the activities WorkOrder.
        /// </summary>
        /// <param name="modelWorkOrder">Model WorkOrder</param>
        /// <returns></returns>
        public List<SOFTTEK.SCMS.Entity.PM.WorkOrder> SearchWorkOrder(SOFTTEK.SCMS.Entity.PM.WorkOrder modelWorkOrder)
        {
            List<SOFTTEK.SCMS.Entity.PM.WorkOrder> listWorkOrders = new List<SOFTTEK.SCMS.Entity.PM.WorkOrder>();
            return context.Execute(() =>
            {

                using (dataSource = new PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var results = dataSource.GetWorkOrders(modelWorkOrder);
                    for(int i = 0; i < results.Count(); i++)
                    {
                        var modelTechnicalObject = new SCMS.Entity.PM.TechnicalObject { Identifier = results[i].TechnicalObject.Identifier };
                        var selectTechnicalObject = dataSource.GetTechnicalObjects(modelTechnicalObject);
                        results[i].TechnicalObject = (from x in selectTechnicalObject select x).FirstOrDefault();
                    }

                    listWorkOrders = results;
                }

                return listWorkOrders;
            }, "Retrieve the registered WorkOrders in system.");

        }

        #endregion

        #region RegisterWorkOrder
        /// <summary>
        /// Register WorkOrder.
        /// </summary>
        /// <param name="modelWorkOrder">modelWorkOrder Insert Model information WorkOrder</param>
        /// <returns>Registered WorkOrder information.</returns>
        public SOFTTEK.SCMS.Entity.PM.WorkOrder RegisterWorkOrder(SOFTTEK.SCMS.Entity.PM.WorkOrder modelWorkOrder)
        {
            SOFTTEK.SCMS.Entity.PM.WorkOrder modelInsert = new SOFTTEK.SCMS.Entity.PM.WorkOrder();

            return context.Execute(() =>
            {
                using (dataSource = new PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();
                    var result = dataSource.InsertWorkOrder(modelWorkOrder);
                    modelInsert = result;
                }
                return modelInsert;
            }, "Register an WorkOrder in system");
        }
        #endregion

        #region UpdateWorkOrder
        /// <summary>
        /// Update an WorkOrder.
        /// </summary>
        /// <param name="modelUpdate">model Update Model information WorkOrder</param>
        /// /// <param name="modelSearch">model filter information WorkOrder</param>
        /// <returns>Update WorkOrder information.</returns>
        public SOFTTEK.SCMS.Entity.PM.WorkOrder UpdateWorkOrder(SOFTTEK.SCMS.Entity.PM.WorkOrder modelUpdate, SOFTTEK.SCMS.Entity.PM.WorkOrder modelSearch)
        {
            SOFTTEK.SCMS.Entity.PM.WorkOrder modelRUpdate = new SOFTTEK.SCMS.Entity.PM.WorkOrder();

            return context.Execute(() =>
            {

                using (dataSource = new PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var result = dataSource.UpdateWorkOrder(modelUpdate, modelSearch);
                    modelRUpdate = result;

                }
                return modelRUpdate;
            }, "Update an WorkOrder in system");
        }
        #endregion
    }
}
