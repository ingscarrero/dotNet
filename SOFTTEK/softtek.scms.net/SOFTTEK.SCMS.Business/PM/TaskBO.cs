using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SCMS.Data;

namespace SOFTTEK.SCMS.Business.PM
{
    public class TaskBO : SOFTTEK.SCMS.Foundation.Business.BusinessObject
    {
        #region Fields
        private PMDataContext dataSource;
        #endregion

        #region Connection 
        public TaskBO(SOFTTEK.SCMS.Foundation.Business.BusinessContext ctx)
            : base(ctx)
        {
        }
        #endregion

        #region SearchTask
        /// <summary>
        /// Get all the Tasks.
        /// </summary>
        /// <param name="modelTask">Model Task</param>
        /// <returns></returns>
        public List<SOFTTEK.SCMS.Entity.PM.Task> SearchTask(SOFTTEK.SCMS.Entity.PM.Task modelTask)
        {
            List<SOFTTEK.SCMS.Entity.PM.Task> listTasks = new List<SOFTTEK.SCMS.Entity.PM.Task>();
            return context.Execute(() =>
            {

                using (dataSource = new PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var results = dataSource.GetTasks(modelTask);
                    listTasks = results;
                }

                return listTasks;
            }, "Retrieve the registered Tasks in system.");

        }

        #endregion

        #region RegisterTask
        /// <summary>
        /// Register Task.
        /// </summary>
        /// <param name="modelTask">modelTask Insert Model information Task</param>
        /// <returns>Registered Task information.</returns>
        public SOFTTEK.SCMS.Entity.PM.Task RegisterTask(SOFTTEK.SCMS.Entity.PM.Task modelTask)
        {
            SOFTTEK.SCMS.Entity.PM.Task modelInsert = new SOFTTEK.SCMS.Entity.PM.Task();

            return context.Execute(() =>
            {
                using (dataSource = new PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();
                    var result = dataSource.InsertTask(modelTask);
                    modelInsert = result;
                }
                return modelInsert;
            }, "Register an Task in system");
        }
        #endregion

        #region UpdateTask
        /// <summary>
        /// Update an Task.
        /// </summary>
        /// <param name="modelUpdate">model Update Model information Task</param>
        /// /// <param name="modelSearch">model filter information Task</param>
        /// <returns>Update Task information.</returns>
        public SOFTTEK.SCMS.Entity.PM.Task UpdateTask(SOFTTEK.SCMS.Entity.PM.Task modelUpdate, SOFTTEK.SCMS.Entity.PM.Task modelSearch)
        {
            SOFTTEK.SCMS.Entity.PM.Task modelRUpdate = new SOFTTEK.SCMS.Entity.PM.Task();

            return context.Execute(() =>
            {

                using (dataSource = new PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var result = dataSource.UpdateTask(modelUpdate, modelSearch);
                    modelRUpdate = result;
                }
                return modelRUpdate;
            }, "Update an Task in system");
        }
        #endregion
    }
}
