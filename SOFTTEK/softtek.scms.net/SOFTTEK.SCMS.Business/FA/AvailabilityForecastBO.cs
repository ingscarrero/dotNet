using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SCMS.Data;

namespace SOFTTEK.SCMS.Business.FA  
{
    public class AvailabilityForecastBO : SOFTTEK.SCMS.Foundation.Business.BusinessObject
    {
        #region Fields
        private FAMDataContext dataSource;
        #endregion

        #region Connection 
        public AvailabilityForecastBO(SOFTTEK.SCMS.Foundation.Business.BusinessContext ctx)
            : base(ctx)
        {
        }
        #endregion

        #region SearchAvailabilityForecast
        /// <summary>
        /// Get all the modelAvailabilityForecast.
        /// </summary>
        /// <param name="modelAvailabilityForecast">modelAvailabilityForecast Identifier</param>
        /// <returns></returns>
        public List<SOFTTEK.SCMS.Entity.FA.AvailabilityForecast> SearchAvailabilityForecasts(SOFTTEK.SCMS.Entity.FA.AvailabilityForecast modelAvailabilityForecast)
        {
            List<SOFTTEK.SCMS.Entity.FA.AvailabilityForecast> listAvailabilityForecasts = new List<SOFTTEK.SCMS.Entity.FA.AvailabilityForecast>();
            return context.Execute(() =>
            {

                using (dataSource = new FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var results = dataSource.GetAvailabilityForecasts(modelAvailabilityForecast);
                    listAvailabilityForecasts = results;
                }

                return listAvailabilityForecasts;
            }, "Retrieve the registered AvailabilityForecasts in system.");

        }

        #endregion

        #region RegisterAvailabilityForecast
        /// <summary>
        /// Register AvailabilityForecast.
        /// </summary>
        /// <param name="modelAvailabilityForecast">modelAvailabilityForecast Insert Model information AvailabilityForecast</param>
        /// <returns>Registered activity information.</returns>
        public SOFTTEK.SCMS.Entity.FA.AvailabilityForecast RegisterAvailabilityForecast(SOFTTEK.SCMS.Entity.FA.AvailabilityForecast modelAvailabilityForecast)
        {
            SOFTTEK.SCMS.Entity.FA.AvailabilityForecast modelInsert = new SOFTTEK.SCMS.Entity.FA.AvailabilityForecast();

            return context.Execute(() =>
            {
                using (dataSource = new FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();
                    var result = dataSource.InsertAvailabilityForecast(modelAvailabilityForecast);
                    modelInsert = result;
                }
                return modelInsert;
            }, "Register an AvailabilityForecast in system");
        }
        #endregion

        #region UpdateAvailabilityForecast
        /// <summary>
        /// Update an AvailabilityForecast.
        /// </summary>
        /// <param name="modelUpdate">model Update Model information AvailabilityForecast</param>
        /// /// <param name="modelSearch">model filter information AvailabilityForecast</param>
        /// <returns>Update AvailabilityForecast information.</returns>
        public SOFTTEK.SCMS.Entity.FA.AvailabilityForecast UpdateAvailabilityForecast(SOFTTEK.SCMS.Entity.FA.AvailabilityForecast modelUpdate, SOFTTEK.SCMS.Entity.FA.AvailabilityForecast modelSearch)
        {
            SOFTTEK.SCMS.Entity.FA.AvailabilityForecast modelRUpdate = new SOFTTEK.SCMS.Entity.FA.AvailabilityForecast();

            return context.Execute(() =>
            {

                using (dataSource = new FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var result = dataSource.UpdateAvailabilityForecast(modelUpdate, modelSearch);
                    modelRUpdate = result;

                }
                return modelRUpdate;
            }, "Update an AvailabilityForecast in system");
        }
        #endregion
    }
}
