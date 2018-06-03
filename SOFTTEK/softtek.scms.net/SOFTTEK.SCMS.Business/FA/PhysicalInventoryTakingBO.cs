using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SCMS.Data;

namespace SOFTTEK.SCMS.Business.FA
{
    public class PhysicalInventoryTakingBO : SOFTTEK.SCMS.Foundation.Business.BusinessObject
    {
        #region Fields
        private FAMDataContext dataSource;
        #endregion

        #region Connection 
        public PhysicalInventoryTakingBO(SOFTTEK.SCMS.Foundation.Business.BusinessContext ctx)
            : base(ctx)
        {
        }
        #endregion

        #region SearchPhysicalInventoryTaking
        /// <summary>
        /// Get all the modelPhysicalInventoryTaking.
        /// </summary>
        /// <param name="modelPhysicalInventoryTaking">modelPhysicalInventoryTaking Identifier</param>
        /// <returns></returns>
        public List<SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking> SearchPhysicalInventoryTakings(SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking modelPhysicalInventoryTaking)
        {
            List<SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking> listPhysicalInventoryTakings = new List<SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking>();
            return context.Execute(() =>
            {

                using (dataSource = new FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var results = dataSource.GetPhysicalInventoryTakings(modelPhysicalInventoryTaking);
                    listPhysicalInventoryTakings = results;
                }

                return listPhysicalInventoryTakings;
            }, "Retrieve the registered PhysicalInventoryTakings in system.");

        }

        #endregion

        #region RegisterPhysicalInventoryTaking
        /// <summary>
        /// Register PhysicalInventoryTaking.
        /// </summary>
        /// <param name="modelPhysicalInventoryTaking">modelPhysicalInventoryTaking Insert Model information PhysicalInventoryTaking</param>
        /// <returns>Registered activity information.</returns>
        public SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking RegisterPhysicalInventoryTaking(SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking modelPhysicalInventoryTaking)
        {
            SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking modelInsert = new SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking();

            return context.Execute(() =>
            {
                using (dataSource = new FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();
                    var result = dataSource.InsertPhysicalInventoryTaking(modelPhysicalInventoryTaking);
                    modelInsert = result;
                }
                return modelInsert;
            }, "Register an PhysicalInventoryTaking in system");
        }
        #endregion

        #region UpdatePhysicalInventoryTaking
        /// <summary>
        /// Update an PhysicalInventoryTaking.
        /// </summary>
        /// <param name="modelUpdate">model Update Model information PhysicalInventoryTaking</param>
        /// /// <param name="modelSearch">model filter information PhysicalInventoryTaking</param>
        /// <returns>Update PhysicalInventoryTaking information.</returns>
        public SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking UpdatePhysicalInventoryTaking(SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking modelUpdate, SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking modelSearch)
        {
            SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking modelRUpdate = new SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking();

            return context.Execute(() =>
            {

                using (dataSource = new FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var result = dataSource.UpdatePhysicalInventoryTaking(modelUpdate, modelSearch);
                    modelRUpdate = result;

                }
                return modelRUpdate;
            }, "Update an PhysicalInventoryTaking in system");
        }
        #endregion
    }
}
