using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SCMS.Data;

namespace SOFTTEK.SCMS.Business.PM
{
    public class MaintenancePlanBO : SOFTTEK.SCMS.Foundation.Business.BusinessObject
    {
        #region Fields
        private PMDataContext dataSource;
        #endregion

        #region Connection 
        public MaintenancePlanBO(SOFTTEK.SCMS.Foundation.Business.BusinessContext ctx)
            : base(ctx)
        {
        }
        #endregion

        #region SearchMaintenancePlan
        /// <summary>
        /// Get all the activities MaintenancePlan.
        /// </summary>
        /// <param name="modelMaintenancePlan">Model MaintenancePlan</param>
        /// <returns></returns>
        public List<SOFTTEK.SCMS.Entity.PM.MaintenancePlan> SearchMaintenancePlan(SOFTTEK.SCMS.Entity.PM.MaintenancePlan modelMaintenancePlan)
        {
            List<SOFTTEK.SCMS.Entity.PM.MaintenancePlan> listMaintenancePlans = new List<SOFTTEK.SCMS.Entity.PM.MaintenancePlan>();
            return context.Execute(() =>
            {

                using (dataSource = new PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var results = dataSource.GetMaintenancePlans(modelMaintenancePlan);
                    for (int i = 0; i < results.Count(); i++)
                    {
                        //results[i].WorkOrder
                        var modelTask = new SCMS.Entity.PM.Task { WorkOrder = new SCMS.Entity.PM.WorkOrder { Identifier = results[i].WorkOrder },
                                                                  Plan = new SCMS.Entity.PM.MaintenancePlan { Identifier = results[i].Identifier }
                        };
                        var selectActivity = dataSource.GetTasks(modelTask);
                        results[i].Activities = selectActivity;
                    }
                    listMaintenancePlans = results;
                }

                return listMaintenancePlans;
            }, "Retrieve the registered MaintenancePlans in system.");

        }

        #endregion

        #region RegisterMaintenancePlan
        /// <summary>
        /// Register MaintenancePlan.
        /// </summary>
        /// <param name="modelMaintenancePlan">modelMaintenancePlan Insert Model information MaintenancePlan</param>
        /// <returns>Registered activity information.</returns>
        public SOFTTEK.SCMS.Entity.PM.MaintenancePlan RegisterMaintenancePlan(SOFTTEK.SCMS.Entity.PM.MaintenancePlan modelMaintenancePlan)
        {
            SOFTTEK.SCMS.Entity.PM.MaintenancePlan modelInsert = new SOFTTEK.SCMS.Entity.PM.MaintenancePlan();

            return context.Execute(() =>
            {
                using (dataSource = new PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();
                    var result = dataSource.InsertMaintenancePlan(modelMaintenancePlan);
                    modelInsert = result;
                }
                return modelInsert;
            }, "Register an MaintenancePlan in system");
        }
        #endregion

        #region UpdateMaintenancePlan
        /// <summary>
        /// Update an MaintenancePlan.
        /// </summary>
        /// <param name="modelUpdate">model Update Model information MaintenancePlan</param>
        /// /// <param name="modelSearch">model filter information MaintenancePlan</param>
        /// <returns>Update MaintenancePlan information.</returns>
        public SOFTTEK.SCMS.Entity.PM.MaintenancePlan UpdateMaintenancePlan(SOFTTEK.SCMS.Entity.PM.MaintenancePlan modelUpdate, SOFTTEK.SCMS.Entity.PM.MaintenancePlan modelSearch)
        {
            SOFTTEK.SCMS.Entity.PM.MaintenancePlan modelRUpdate = new SOFTTEK.SCMS.Entity.PM.MaintenancePlan();

            return context.Execute(() =>
            {

                using (dataSource = new PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var result = dataSource.UpdateMaintenancePlan(modelUpdate, modelSearch);
                    modelRUpdate = result;

                }
                return modelRUpdate;
            }, "Update an MaintenancePlan in system");
        }
        #endregion
    }
}
