using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SCMS.Data;

namespace SOFTTEK.SCMS.Business.FA
{
    public class AvailabilityForecastItemBO : SOFTTEK.SCMS.Foundation.Business.BusinessObject
    {
        #region Fields
        private FAMDataContext dataSource;
        #endregion

        #region Connection 
        public AvailabilityForecastItemBO(SOFTTEK.SCMS.Foundation.Business.BusinessContext ctx)
            : base(ctx)
        {
        }
        #endregion

        #region SearchAvailabilityForecastItem
        /// <summary>
        /// Get all the AvailabilityForecastItem.
        /// </summary>
        /// <param name="modelAvailabilityForecastItem">AvailabilityForecastItem Identifier</param>
        /// <returns></returns>
        public List<SOFTTEK.SCMS.Entity.FA.AvailabilityForecastItem> SearchAvailabilityForecastItems(SOFTTEK.SCMS.Entity.FA.AvailabilityForecastItem modelAvailabilityForecastItem)
        {
            List<SOFTTEK.SCMS.Entity.FA.AvailabilityForecastItem> listAvailabilityForecastItems = new List<SOFTTEK.SCMS.Entity.FA.AvailabilityForecastItem>();
            return context.Execute(() =>
            {

                using (dataSource = new FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var results = dataSource.GetAvailabilityForecastItems(modelAvailabilityForecastItem);
                    listAvailabilityForecastItems = results;
                }

                return listAvailabilityForecastItems;
            }, "Retrieve the registered AvailabilityForecastItem in system.");

        }

        #endregion

        #region RegisterAvailabilityForecastItem
        /// <summary>
        /// Register AvailabilityForecastItem.
        /// </summary>
        /// <param name="modelAvailabilityForecastItem">modelAvailabilityForecastItem Insert Model information AvailabilityForecastItem</param>
        /// <returns>Registered activity information.</returns>
        public SOFTTEK.SCMS.Entity.FA.AvailabilityForecastItem RegisterAvailabilityForecastItem(SOFTTEK.SCMS.Entity.FA.AvailabilityForecastItem modelAvailabilityForecastItem)
        {
            SOFTTEK.SCMS.Entity.FA.AvailabilityForecastItem modelInsert = new SOFTTEK.SCMS.Entity.FA.AvailabilityForecastItem();

            return context.Execute(() =>
            {
                using (dataSource = new FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();
                    var result = dataSource.InsertAvailabilityForecastItem(modelAvailabilityForecastItem);
                    modelInsert = result;
                }
                return modelInsert;
            }, "Register an AvailabilityForecastItem in system");
        }
        #endregion

        #region UpdateAvailabilityForecastItem
        /// <summary>
        /// Update an AvailabilityForecastItem.
        /// </summary>
        /// <param name="modelUpdate">model Update Model information AvailabilityForecastItem</param>
        /// /// <param name="modelSearch">model filter information AvailabilityForecastItem</param>
        /// <returns>Update AvailabilityForecastItem information.</returns>
        public SOFTTEK.SCMS.Entity.FA.AvailabilityForecastItem UpdateAvailabilityForecastItem(SOFTTEK.SCMS.Entity.FA.AvailabilityForecastItem modelUpdate, SOFTTEK.SCMS.Entity.FA.AvailabilityForecastItem modelSearch)
        {
            SOFTTEK.SCMS.Entity.FA.AvailabilityForecastItem modelRUpdate = new SOFTTEK.SCMS.Entity.FA.AvailabilityForecastItem();

            return context.Execute(() =>
            {

                using (dataSource = new FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var result = dataSource.UpdateAvailabilityForecastItem(modelUpdate, modelSearch);
                    modelRUpdate = result;

                }
                return modelRUpdate;
            }, "Update an AvailabilityForecastItem in system");
        }
        #endregion
    }
}
