using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SCMS.Data;

namespace SOFTTEK.SCMS.Business.FA
{
    public class PhysicalInventoryTakingItemBO : SOFTTEK.SCMS.Foundation.Business.BusinessObject
    {
        #region Fields
        private FAMDataContext dataSource;
        #endregion

        #region Connection 
        public PhysicalInventoryTakingItemBO(SOFTTEK.SCMS.Foundation.Business.BusinessContext ctx)
            : base(ctx)
        {
        }
        #endregion

        #region SearchPhysicalInventoryTakingItem
        /// <summary>
        /// Get all the modelPhysicalInventoryTakingItem.
        /// </summary>
        /// <param name="modelPhysicalInventoryTakingItem">modelPhysicalInventoryTakingItem Identifier</param>
        /// <returns></returns>
        public List<SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTakingItem> SearchPhysicalInventoryTakingItems(SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTakingItem modelPhysicalInventoryTakingItem)
        {
            List<SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTakingItem> listPhysicalInventoryTakingItems = new List<SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTakingItem>();
            return context.Execute(() =>
            {

                using (dataSource = new FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var results = dataSource.GetPhysicalInventoryTakingItems(modelPhysicalInventoryTakingItem);
                    listPhysicalInventoryTakingItems = results;
                }

                return listPhysicalInventoryTakingItems;
            }, "Retrieve the registered PhysicalInventoryTakingItems in system.");

        }

        #endregion

        #region RegisterPhysicalInventoryTakingItem
        /// <summary>
        /// Register PhysicalInventoryTakingItem.
        /// </summary>
        /// <param name="modelPhysicalInventoryTakingItem">modelPhysicalInventoryTakingItem Insert Model information PhysicalInventoryTakingItem</param>
        /// <returns>Registered activity information.</returns>
        public SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTakingItem RegisterPhysicalInventoryTakingItem(SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTakingItem modelPhysicalInventoryTakingItem)
        {
            SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTakingItem modelInsert = new SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTakingItem();

            return context.Execute(() =>
            {
                using (dataSource = new FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();
                    var result = dataSource.InsertPhysicalInventoryTakingItem(modelPhysicalInventoryTakingItem);
                    modelInsert = result;
                }
                return modelInsert;
            }, "Register an PhysicalInventoryTakingItem in system");
        }
        #endregion

        #region UpdatePhysicalInventoryTakingItem
        /// <summary>
        /// Update an PhysicalInventoryTakingItem.
        /// </summary>
        /// <param name="modelUpdate">model Update Model information PhysicalInventoryTakingItem</param>
        /// /// <param name="modelSearch">model filter information PhysicalInventoryTakingItem</param>
        /// <returns>Update PhysicalInventoryTakingItem information.</returns>
        public SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTakingItem UpdatePhysicalInventoryTakingItem(SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTakingItem modelUpdate, SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTakingItem modelSearch)
        {
            SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTakingItem modelRUpdate = new SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTakingItem();

            return context.Execute(() =>
            {

                using (dataSource = new FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var result = dataSource.UpdatePhysicalInventoryTakingItem(modelUpdate, modelSearch);
                    modelRUpdate = result;

                }
                return modelRUpdate;
            }, "Update an PhysicalInventoryTakingItem in system");
        }
        #endregion
    }
}
