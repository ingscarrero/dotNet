using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SCMS.Data;

namespace SOFTTEK.SCMS.Business.FA
{
    public class TechnicalEvaluationRequestBO : SOFTTEK.SCMS.Foundation.Business.BusinessObject
    {
        #region Fields
        private FAMDataContext dataSource;
        #endregion

        #region Connection 
        public TechnicalEvaluationRequestBO(SOFTTEK.SCMS.Foundation.Business.BusinessContext ctx)
            : base(ctx)
        {
        }
        #endregion

        #region SearchTechnicalEvaluationRequest
        /// <summary>
        /// Get all the modelTechnicalEvaluationRequest.
        /// </summary>
        /// <param name="modelTechnicalEvaluationRequest">modelTechnicalEvaluationRequest Identifier</param>
        /// <returns></returns>
        public List<SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest> SearchTechnicalEvaluationRequests(SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest modelTechnicalEvaluationRequest)
        {
            List<SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest> listTechnicalEvaluationRequests = new List<SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest>();
            return context.Execute(() =>
            {

                using (dataSource = new FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var results = dataSource.GetTechnicalEvaluationRequests(modelTechnicalEvaluationRequest);
                    listTechnicalEvaluationRequests = results;
                }

                return listTechnicalEvaluationRequests;
            }, "Retrieve the registered TechnicalEvaluationRequests in system.");

        }

        #endregion

        #region RegisterTechnicalEvaluationRequest
        /// <summary>
        /// Register TechnicalEvaluationRequest.
        /// </summary>
        /// <param name="modelTechnicalEvaluationRequest">modelTechnicalEvaluationRequest Insert Model information TechnicalEvaluationRequest</param>
        /// <returns>Registered activity information.</returns>
        public SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest RegisterTechnicalEvaluationRequest(SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest modelTechnicalEvaluationRequest)
        {
            SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest modelInsert = new SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest();

            return context.Execute(() =>
            {
                using (dataSource = new FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();
                    var result = dataSource.InsertTechnicalEvaluationRequest(modelTechnicalEvaluationRequest);
                    modelInsert = result;
                }
                return modelInsert;
            }, "Register an TechnicalEvaluationRequest in system");
        }
        #endregion

        #region UpdateTechnicalEvaluationRequest
        /// <summary>
        /// Update an TechnicalEvaluationRequest.
        /// </summary>
        /// <param name="modelUpdate">model Update Model information TechnicalEvaluationRequest</param>
        /// /// <param name="modelSearch">model filter information TechnicalEvaluationRequest</param>
        /// <returns>Update TechnicalEvaluationRequest information.</returns>
        public SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest UpdateTechnicalEvaluationRequest(SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest modelUpdate, SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest modelSearch)
        {
            SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest modelRUpdate = new SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest();

            return context.Execute(() =>
            {

                using (dataSource = new FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var result = dataSource.UpdateTechnicalEvaluationRequest(modelUpdate, modelSearch);
                    modelRUpdate = result;

                }
                return modelRUpdate;
            }, "Update an TechnicalEvaluationRequest in system");
        }
        #endregion
    }
}
