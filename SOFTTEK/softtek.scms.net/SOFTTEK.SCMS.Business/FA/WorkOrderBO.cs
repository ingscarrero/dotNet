using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SCMS.Data;

namespace SOFTTEK.SCMS.Business.FA
{
    public class WorkOrderBO : SOFTTEK.SCMS.Foundation.Business.BusinessObject
    {
        #region Fields
        private FAMDataContext dataSource;
        #endregion

        #region Connection 
        public WorkOrderBO(SOFTTEK.SCMS.Foundation.Business.BusinessContext ctx)
            : base(ctx)
        {
        }
        #endregion

        #region SearchWorkOrder
        /// <summary>
        /// Get all the modelWorkOrder.
        /// </summary>
        /// <param name="modelWorkOrder">modelWorkOrder Identifier</param>
        /// <returns></returns>
        public List<SOFTTEK.SCMS.Entity.FA.WorkOrder> SearchWorkOrders(SOFTTEK.SCMS.Entity.FA.WorkOrder modelWorkOrder)
        {
            List<SOFTTEK.SCMS.Entity.FA.WorkOrder> listWorkOrders = new List<SOFTTEK.SCMS.Entity.FA.WorkOrder>();
            return context.Execute(() =>
            {

                using (dataSource = new FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var results = dataSource.GetWorkOrders(modelWorkOrder);
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
        /// <returns>Registered activity information.</returns>
        public SOFTTEK.SCMS.Entity.FA.WorkOrder RegisterWorkOrder(SOFTTEK.SCMS.Entity.FA.WorkOrder modelWorkOrder)
        {
            SOFTTEK.SCMS.Entity.FA.WorkOrder modelInsert = new SOFTTEK.SCMS.Entity.FA.WorkOrder();

            return context.Execute(() =>
            {
                using (dataSource = new FAMDataContext(context.SecurityContext))
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
        public SOFTTEK.SCMS.Entity.FA.WorkOrder UpdateWorkOrder(SOFTTEK.SCMS.Entity.FA.WorkOrder modelUpdate, SOFTTEK.SCMS.Entity.FA.WorkOrder modelSearch)
        {
            SOFTTEK.SCMS.Entity.FA.WorkOrder modelRUpdate = new SOFTTEK.SCMS.Entity.FA.WorkOrder();

            return context.Execute(() =>
            {

                using (dataSource = new FAMDataContext(context.SecurityContext))
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
