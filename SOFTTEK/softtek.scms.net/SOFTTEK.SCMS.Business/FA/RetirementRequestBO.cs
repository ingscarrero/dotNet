using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SCMS.Data;

namespace SOFTTEK.SCMS.Business.FA
{
    public class RetirementRequestBO : SOFTTEK.SCMS.Foundation.Business.BusinessObject
    {
        #region Fields
        private FAMDataContext dataSource;
        #endregion

        #region Connection 
        public RetirementRequestBO(SOFTTEK.SCMS.Foundation.Business.BusinessContext ctx)
            : base(ctx)
        {
        }
        #endregion

        #region SearchRetirementRequest
        /// <summary>
        /// Get all the modelRetirementRequest.
        /// </summary>
        /// <param name="modelRetirementRequest">modelRetirementRequest Identifier</param>
        /// <returns></returns>
        public List<SOFTTEK.SCMS.Entity.FA.RetirementRequest> SearchRetirementRequests(SOFTTEK.SCMS.Entity.FA.RetirementRequest modelRetirementRequest)
        {
            List<SOFTTEK.SCMS.Entity.FA.RetirementRequest> listRetirementRequests = new List<SOFTTEK.SCMS.Entity.FA.RetirementRequest>();
            return context.Execute(() =>
            {

                using (dataSource = new FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var results = dataSource.GetRetirementRequests(modelRetirementRequest);
                    listRetirementRequests = results;
                }

                return listRetirementRequests;
            }, "Retrieve the registered RetirementRequests in system.");

        }

        #endregion

        #region RegisterRetirementRequest
        /// <summary>
        /// Register RetirementRequest.
        /// </summary>
        /// <param name="modelRetirementRequest">modelRetirementRequest Insert Model information RetirementRequest</param>
        /// <returns>Registered activity information.</returns>
        public SOFTTEK.SCMS.Entity.FA.RetirementRequest RegisterRetirementRequest(SOFTTEK.SCMS.Entity.FA.RetirementRequest modelRetirementRequest)
        {
            SOFTTEK.SCMS.Entity.FA.RetirementRequest modelInsert = new SOFTTEK.SCMS.Entity.FA.RetirementRequest();

            return context.Execute(() =>
            {
                using (dataSource = new FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();
                    var result = dataSource.InsertRetirementRequest(modelRetirementRequest);
                    modelInsert = result;
                }
                return modelInsert;
            }, "Register an RetirementRequest in system");
        }
        #endregion

        #region UpdateRetirementRequest
        /// <summary>
        /// Update an RetirementRequest.
        /// </summary>
        /// <param name="modelUpdate">model Update Model information RetirementRequest</param>
        /// /// <param name="modelSearch">model filter information RetirementRequest</param>
        /// <returns>Update RetirementRequest information.</returns>
        public SOFTTEK.SCMS.Entity.FA.RetirementRequest UpdateRetirementRequest(SOFTTEK.SCMS.Entity.FA.RetirementRequest modelUpdate, SOFTTEK.SCMS.Entity.FA.RetirementRequest modelSearch)
        {
            SOFTTEK.SCMS.Entity.FA.RetirementRequest modelRUpdate = new SOFTTEK.SCMS.Entity.FA.RetirementRequest();

            return context.Execute(() =>
            {

                using (dataSource = new FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var result = dataSource.UpdateRetirementRequest(modelUpdate, modelSearch);
                    modelRUpdate = result;

                }
                return modelRUpdate;
            }, "Update an RetirementRequest in system");
        }
        #endregion
    }
}
