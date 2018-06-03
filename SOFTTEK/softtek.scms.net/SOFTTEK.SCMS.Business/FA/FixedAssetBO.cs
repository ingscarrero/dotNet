using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SCMS.Data;

namespace SOFTTEK.SCMS.Business.FA
{
    public class FixedAssetBO : SOFTTEK.SCMS.Foundation.Business.BusinessObject
    {
        #region Fields
        private FAMDataContext dataSource;
        #endregion

        #region Connection 
        public FixedAssetBO(SOFTTEK.SCMS.Foundation.Business.BusinessContext ctx)
            : base(ctx)
        {
        }
        #endregion

        #region SearchFixedAsset
        /// <summary>
        /// Get all the modelFixedAsset.
        /// </summary>
        /// <param name="modelFixedAsset">modelFixedAsset Identifier</param>
        /// <returns></returns>
        public List<SOFTTEK.SCMS.Entity.FA.FixedAsset> SearchFixedAssets(SOFTTEK.SCMS.Entity.FA.FixedAsset modelFixedAsset)
        {
            List<SOFTTEK.SCMS.Entity.FA.FixedAsset> listFixedAssets = new List<SOFTTEK.SCMS.Entity.FA.FixedAsset>();
            return context.Execute(() =>
            {

                using (dataSource = new FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var results = dataSource.GetFixedAssets(modelFixedAsset);
                    listFixedAssets = results;
                }

                return listFixedAssets;
            }, "Retrieve the registered FixedAssets in system.");

        }

        #endregion

        #region RegisterFixedAsset
        /// <summary>
        /// Register FixedAsset.
        /// </summary>
        /// <param name="modelFixedAsset">modelFixedAsset Insert Model information FixedAsset</param>
        /// <returns>Registered activity information.</returns>
        public SOFTTEK.SCMS.Entity.FA.FixedAsset RegisterFixedAsset(SOFTTEK.SCMS.Entity.FA.FixedAsset modelFixedAsset)
        {
            SOFTTEK.SCMS.Entity.FA.FixedAsset modelInsert = new SOFTTEK.SCMS.Entity.FA.FixedAsset();

            return context.Execute(() =>
            {
                using (dataSource = new FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();
                    var result = dataSource.InsertFixedAsset(modelFixedAsset);
                    modelInsert = result;
                }
                return modelInsert;
            }, "Register an FixedAsset in system");
        }
        #endregion

        #region UpdateFixedAsset
        /// <summary>
        /// Update an FixedAsset.
        /// </summary>
        /// <param name="modelUpdate">model Update Model information FixedAsset</param>
        /// /// <param name="modelSearch">model filter information FixedAsset</param>
        /// <returns>Update FixedAsset information.</returns>
        public SOFTTEK.SCMS.Entity.FA.FixedAsset UpdateFixedAsset(SOFTTEK.SCMS.Entity.FA.FixedAsset modelUpdate, SOFTTEK.SCMS.Entity.FA.FixedAsset modelSearch)
        {
            SOFTTEK.SCMS.Entity.FA.FixedAsset modelRUpdate = new SOFTTEK.SCMS.Entity.FA.FixedAsset();

            return context.Execute(() =>
            {

                using (dataSource = new FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var result = dataSource.UpdateFixedAsset(modelUpdate, modelSearch);
                    modelRUpdate = result;

                }
                return modelRUpdate;
            }, "Update an FixedAsset in system");
        }
        #endregion
    }
}
