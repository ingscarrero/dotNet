using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SCMS.Data;

namespace SOFTTEK.SCMS.Business.FA
{
    public class ProviderBO : SOFTTEK.SCMS.Foundation.Business.BusinessObject
    {
        #region Fields
        private FAMDataContext dataSource;
        #endregion

        #region Connection 
        public ProviderBO(SOFTTEK.SCMS.Foundation.Business.BusinessContext ctx)
            : base(ctx)
        {
        }
        #endregion

        #region SearchProvider
        /// <summary>
        /// Get all the modelProvider.
        /// </summary>
        /// <param name="modelProvider">modelProvider Identifier</param>
        /// <returns></returns>
        public List<SOFTTEK.SCMS.Entity.FA.Provider> SearchProviders(SOFTTEK.SCMS.Entity.FA.Provider modelProvider)
        {
            List<SOFTTEK.SCMS.Entity.FA.Provider> listProviders = new List<SOFTTEK.SCMS.Entity.FA.Provider>();
            return context.Execute(() =>
            {

                using (dataSource = new FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var results = dataSource.GetProviders(modelProvider);
                    listProviders = results;
                }

                return listProviders;
            }, "Retrieve the registered Providers in system.");

        }

        #endregion

        #region RegisterProvider
        /// <summary>
        /// Register Provider.
        /// </summary>
        /// <param name="modelProvider">modelProvider Insert Model information Provider</param>
        /// <returns>Registered activity information.</returns>
        public SOFTTEK.SCMS.Entity.FA.Provider RegisterProvider(SOFTTEK.SCMS.Entity.FA.Provider modelProvider)
        {
            SOFTTEK.SCMS.Entity.FA.Provider modelInsert = new SOFTTEK.SCMS.Entity.FA.Provider();

            return context.Execute(() =>
            {
                using (dataSource = new FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();
                    var result = dataSource.InsertProvider(modelProvider);
                    modelInsert = result;
                }
                return modelInsert;
            }, "Register an Provider in system");
        }
        #endregion

        #region UpdateProvider
        /// <summary>
        /// Update an Provider.
        /// </summary>
        /// <param name="modelUpdate">model Update Model information Provider</param>
        /// /// <param name="modelSearch">model filter information Provider</param>
        /// <returns>Update Provider information.</returns>
        public SOFTTEK.SCMS.Entity.FA.Provider UpdateProvider(SOFTTEK.SCMS.Entity.FA.Provider modelUpdate, SOFTTEK.SCMS.Entity.FA.Provider modelSearch)
        {
            SOFTTEK.SCMS.Entity.FA.Provider modelRUpdate = new SOFTTEK.SCMS.Entity.FA.Provider();

            return context.Execute(() =>
            {

                using (dataSource = new FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var result = dataSource.UpdateProvider(modelUpdate, modelSearch);
                    modelRUpdate = result;

                }
                return modelRUpdate;
            }, "Update an Provider in system");
        }
        #endregion
    }
}
