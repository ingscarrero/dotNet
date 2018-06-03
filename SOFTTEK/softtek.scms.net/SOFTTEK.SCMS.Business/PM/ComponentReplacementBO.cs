using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SCMS.Data;

namespace SOFTTEK.SCMS.Business.PM
{
    public class ComponentReplacementBO : SOFTTEK.SCMS.Foundation.Business.BusinessObject
    {
        #region Fields
        private PMDataContext dataSource;
        #endregion

        #region Connection 
        public ComponentReplacementBO(SOFTTEK.SCMS.Foundation.Business.BusinessContext ctx)
            : base(ctx)
        {
        }
        #endregion

        #region SearchComponentReplacement
        /// <summary>
        /// Get all the activities reported by an employee in a period.
        /// </summary>
        /// <param name="modelComponentReplacement">Model ComponentReplacement</param>
        /// <returns></returns>
        public List<SOFTTEK.SCMS.Entity.PM.ComponentReplacement> SearchComponentReplacement(SOFTTEK.SCMS.Entity.PM.ComponentReplacement modelComponentReplacement)
        {
            List<SOFTTEK.SCMS.Entity.PM.ComponentReplacement> listComponentReplacement = new List<SOFTTEK.SCMS.Entity.PM.ComponentReplacement>();
            return context.Execute(() =>
            {

                using (dataSource = new PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var results = dataSource.GetComponentReplacements(modelComponentReplacement);
                    listComponentReplacement = results;
                }

                return listComponentReplacement;
            }, "Retrieve the registered ComponentReplacement in system.");

        }

        #endregion

        #region RegisterComponentReplacement
        /// <summary>
        /// Register ComponentReplacement.
        /// </summary>
        /// <param name="modelComponentReplacement">modelComponentReplacement Insert Model information ComponentReplacement</param>
        /// <returns>Registered activity information.</returns>
        public SOFTTEK.SCMS.Entity.PM.ComponentReplacement RegisterComponentReplacement(SOFTTEK.SCMS.Entity.PM.ComponentReplacement modelComponentReplacement)
        {
            SOFTTEK.SCMS.Entity.PM.ComponentReplacement modelInsert = new SOFTTEK.SCMS.Entity.PM.ComponentReplacement();

            return context.Execute(() =>
            {
                using (dataSource = new PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();
                    var result = dataSource.InsertComponentReplacement(modelComponentReplacement);
                    modelInsert = result;
                }
                return modelInsert;
            }, "Register an ComponentReplacement in system");
        }
        #endregion

        #region UpdateComponentReplacement
        /// <summary>
        /// Update an ComponentReplacement.
        /// </summary>
        /// <param name="modelUpdate">model Update Model information ComponentReplacement</param>
        /// /// <param name="modelSearch">model filter information ComponentReplacement</param>
        /// <returns>Update ComponentReplacement information.</returns>
        public SOFTTEK.SCMS.Entity.PM.ComponentReplacement UpdateComponentReplacement(SOFTTEK.SCMS.Entity.PM.ComponentReplacement modelUpdate, SOFTTEK.SCMS.Entity.PM.ComponentReplacement modelSearch)
        {
            SOFTTEK.SCMS.Entity.PM.ComponentReplacement modelRUpdate = new SOFTTEK.SCMS.Entity.PM.ComponentReplacement();

            return context.Execute(() =>
            {

                using (dataSource = new PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var result = dataSource.UpdateComponentReplacement(modelUpdate, modelSearch);
                    modelRUpdate = result;

                }
                return modelRUpdate;
            }, "Update an ComponentReplacement in system");
        }
        #endregion
    }
}
