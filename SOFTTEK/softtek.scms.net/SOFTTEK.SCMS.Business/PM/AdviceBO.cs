using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SCMS.Data;

namespace SOFTTEK.SCMS.Business.PM
{
    public class AdviceBO : SOFTTEK.SCMS.Foundation.Business.BusinessObject
    {
        #region Fields
        private PMDataContext dataSource;
        #endregion

        #region Connection 
        public AdviceBO(SOFTTEK.SCMS.Foundation.Business.BusinessContext ctx)
            : base(ctx)
        {
        }
        #endregion

        #region SearchAdvice
        /// <summary>
        /// Get all the activities Advice.
        /// </summary>
        /// <param name="modelAdvice">Model Advice</param>
        /// <returns></returns>
        public List<SOFTTEK.SCMS.Entity.PM.Advice> SearchAdvice(SOFTTEK.SCMS.Entity.PM.Advice modelAdvice)
        {
            List<SOFTTEK.SCMS.Entity.PM.Advice> listAdvices = new List<SOFTTEK.SCMS.Entity.PM.Advice>();
            return context.Execute(() =>
            {

                using (dataSource = new PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var results = dataSource.GetAdvices(modelAdvice);
                    listAdvices = results;
                }

                return listAdvices;
            }, "Retrieve the registered Advices in system.");

        }

        #endregion

        #region RegisterAdvice
        /// <summary>
        /// Register advice.
        /// </summary>
        /// <param name="modelAdvice">modelAdvice Insert Model information Advice</param>
        /// <returns>Registered activity information.</returns>
        public SOFTTEK.SCMS.Entity.PM.Advice RegisterAdvice(SOFTTEK.SCMS.Entity.PM.Advice modelAdvice)
        {
            //List<SOFTTEK.SCMS.Entity.PM.Advice> listReturn = new List<SOFTTEK.SCMS.Entity.PM.Advice>();
            SOFTTEK.SCMS.Entity.PM.Advice modelInsert = new SOFTTEK.SCMS.Entity.PM.Advice();
            
            return context.Execute(() =>
            {
                using (dataSource = new PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();
                    var result = dataSource.InsertAdvice(modelAdvice);
                    modelInsert = result;
                    //listReturn = result;
                }
                return modelInsert;
            }, "Register an Advices in system");
        }
        #endregion

        #region UpdateAdvice
        /// <summary>
        /// Update an Advice.
        /// </summary>
        /// <param name="modelUpdate">model Update Model information Advice</param>
        /// /// <param name="modelSearch">model filter information Advice</param>
        /// <returns>Update advice information.</returns>
        public SOFTTEK.SCMS.Entity.PM.Advice UpdateAdvice(SOFTTEK.SCMS.Entity.PM.Advice modelUpdate, SOFTTEK.SCMS.Entity.PM.Advice modelSearch)
        {
            SOFTTEK.SCMS.Entity.PM.Advice modelRUpdate = new SOFTTEK.SCMS.Entity.PM.Advice();

            return context.Execute(() =>
            {

                using (dataSource = new PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var result = dataSource.UpdateAdvice(modelUpdate, modelSearch);
                    modelRUpdate = result;

                }
                return modelRUpdate;
            }, "Update an advice in system");
        }
        #endregion
    }
}
