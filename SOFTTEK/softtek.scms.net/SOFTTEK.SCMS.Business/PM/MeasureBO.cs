using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SCMS.Data;

namespace SOFTTEK.SCMS.Business.PM
{
    public class MeasureBO : SOFTTEK.SCMS.Foundation.Business.BusinessObject
    {
        #region Fields
        private PMDataContext dataSource;
        #endregion

        #region Connection 
        public MeasureBO(SOFTTEK.SCMS.Foundation.Business.BusinessContext ctx)
            : base(ctx)
        {
        }
        #endregion

        #region SearchMeasure
        /// <summary>
        /// Get all the activities Measure.
        /// </summary>
        /// <param name="modelMeasure">Model Measure</param>
        /// <returns></returns>
        public List<SOFTTEK.SCMS.Entity.PM.Measure> SearchMeasure(SOFTTEK.SCMS.Entity.PM.Measure modelMeasure)
        {
            List<SOFTTEK.SCMS.Entity.PM.Measure> listMeasures = new List<SOFTTEK.SCMS.Entity.PM.Measure>();
            return context.Execute(() =>
            {

                using (dataSource = new PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var results = dataSource.GetMeasures(modelMeasure);
                    listMeasures = results;
                }

                return listMeasures;
            }, "Retrieve the registered Measure in system.");

        }

        #endregion

        #region RegisterMeasure
        /// <summary>
        /// Register Measure.
        /// </summary>
        /// <param name="modelMeasure">modelMeasure Insert Model information Measure</param>
        /// <returns>Registered Measure information.</returns>
        public SOFTTEK.SCMS.Entity.PM.Measure RegisterMeasure(SOFTTEK.SCMS.Entity.PM.Measure modelMeasure)
        {
            SOFTTEK.SCMS.Entity.PM.Measure modelInsert = new SOFTTEK.SCMS.Entity.PM.Measure();

            return context.Execute(() =>
            {
                using (dataSource = new PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();
                    var result = dataSource.InsertMeasure(modelMeasure);
                    modelInsert = result;
                }
                return modelInsert;
            }, "Register an Measure in system");
        }
        #endregion

        #region UpdateMeasure
        /// <summary>
        /// Update an Measure.
        /// </summary>
        /// <param name="modelUpdate">model Update Model information Measure</param>
        /// /// <param name="modelSearch">model filter information Measure</param>
        /// <returns>Update Measure information.</returns>
        public SOFTTEK.SCMS.Entity.PM.Measure UpdateMeasure(SOFTTEK.SCMS.Entity.PM.Measure modelUpdate, SOFTTEK.SCMS.Entity.PM.Measure modelSearch)
        {
            SOFTTEK.SCMS.Entity.PM.Measure modelRUpdate = new SOFTTEK.SCMS.Entity.PM.Measure();

            return context.Execute(() =>
            {

                using (dataSource = new PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var result = dataSource.UpdateMeasure(modelUpdate, modelSearch);
                    modelRUpdate = result;

                }
                return modelRUpdate;
            }, "Update an Measure in system");
        }
        #endregion
    }
}
