using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SCMS.Data;

namespace SOFTTEK.SCMS.Business.PM
{
    public class ActivityBO : SOFTTEK.SCMS.Foundation.Business.BusinessObject
    {
        #region Fields
        private PMDataContext dataSource;
        #endregion

        #region Connection 
        public ActivityBO(SOFTTEK.SCMS.Foundation.Business.BusinessContext ctx)
            : base(ctx)
        {
        }
        #endregion

        #region SearchActivity
        /// <summary>
        /// Get all the activities Activity.
        /// </summary>
        /// <param name="modelActivity">Model Activity</param>
        /// <returns></returns>
        public List<SOFTTEK.SCMS.Entity.PM.Activity> SearchActivity(SOFTTEK.SCMS.Entity.PM.Activity modelActivity)
        {
            List<SOFTTEK.SCMS.Entity.PM.Activity> listActivitys = new List<SOFTTEK.SCMS.Entity.PM.Activity>();
            return context.Execute(() =>
            {

                using (dataSource = new PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var results = dataSource.GetActivitys(modelActivity);
                    listActivitys = results;
                }

                return listActivitys;
            }, "Retrieve the registered Activitys in system.");

        }

        #endregion

        #region RegisterActivity
        /// <summary>
        /// Register Activity.
        /// </summary>
        /// <param name="modelActivity">modelActivity Insert Model information Activity</param>
        /// <returns>Registered activity information.</returns>
        public SOFTTEK.SCMS.Entity.PM.Activity RegisterActivity(SOFTTEK.SCMS.Entity.PM.Activity modelActivity)
        {
            SOFTTEK.SCMS.Entity.PM.Activity modelInsert = new SOFTTEK.SCMS.Entity.PM.Activity();

            return context.Execute(() =>
            {
                using (dataSource = new PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();
                    var result = dataSource.InsertActivity(modelActivity);
                    modelInsert = result;
                }
                return modelInsert;
            }, "Register an Activitys in system");
        }
        #endregion

        #region UpdateActivity
        /// <summary>
        /// Update an Activity.
        /// </summary>
        /// <param name="modelUpdate">model Update Model information Activity</param>
        /// /// <param name="modelSearch">model filter information Activity</param>
        /// <returns>Update Activity information.</returns>
        public SOFTTEK.SCMS.Entity.PM.Activity UpdateActivity(SOFTTEK.SCMS.Entity.PM.Activity modelUpdate, SOFTTEK.SCMS.Entity.PM.Activity modelSearch)
        {
            SOFTTEK.SCMS.Entity.PM.Activity modelRUpdate = new SOFTTEK.SCMS.Entity.PM.Activity();

            return context.Execute(() =>
            {

                using (dataSource = new PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var result = dataSource.UpdateActivity(modelUpdate, modelSearch);
                    modelRUpdate = result;

                }
                return modelRUpdate;
            }, "Update an Activity in system");
        }
        #endregion
    }
}
