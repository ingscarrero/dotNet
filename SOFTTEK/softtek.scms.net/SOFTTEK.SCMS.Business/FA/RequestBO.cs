using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SCMS.Data;

namespace SOFTTEK.SCMS.Business.FA
{
    public class RequestBO : SOFTTEK.SCMS.Foundation.Business.BusinessObject
    {
        #region Fields
        private FAMDataContext dataSource;
        #endregion

        #region Connection 
        public RequestBO(SOFTTEK.SCMS.Foundation.Business.BusinessContext ctx)
            : base(ctx)
        {
        }
        #endregion

        #region SearchRequest
        /// <summary>
        /// Get all the modelRequest.
        /// </summary>
        /// <param name="modelRequest">modelRequest Identifier</param>
        /// <returns></returns>
        public List<SOFTTEK.SCMS.Entity.FA.Request> SearchRequests(SOFTTEK.SCMS.Entity.FA.Request modelRequest)
        {
            List<SOFTTEK.SCMS.Entity.FA.Request> listRequests = new List<SOFTTEK.SCMS.Entity.FA.Request>();
            return context.Execute(() =>
            {

                using (dataSource = new FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var results = dataSource.GetRequests(modelRequest);
                    listRequests = results;
                }

                return listRequests;
            }, "Retrieve the registered Requests in system.");

        }

        #endregion

        #region RegisterRequest
        /// <summary>
        /// Register Request.
        /// </summary>
        /// <param name="modelRequest">modelRequest Insert Model information Request</param>
        /// <returns>Registered activity information.</returns>
        public SOFTTEK.SCMS.Entity.FA.Request RegisterRequest(SOFTTEK.SCMS.Entity.FA.Request modelRequest)
        {
            SOFTTEK.SCMS.Entity.FA.Request modelInsert = new SOFTTEK.SCMS.Entity.FA.Request();

            return context.Execute(() =>
            {
                using (dataSource = new FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();
                    var result = dataSource.InsertRequest(modelRequest);
                    modelInsert = result;
                }
                return modelInsert;
            }, "Register an Request in system");
        }
        #endregion

        #region UpdateRequest
        /// <summary>
        /// Update an Request.
        /// </summary>
        /// <param name="modelUpdate">model Update Model information Request</param>
        /// /// <param name="modelSearch">model filter information Request</param>
        /// <returns>Update Request information.</returns>
        public SOFTTEK.SCMS.Entity.FA.Request UpdateRequest(SOFTTEK.SCMS.Entity.FA.Request modelUpdate, SOFTTEK.SCMS.Entity.FA.Request modelSearch)
        {
            SOFTTEK.SCMS.Entity.FA.Request modelRUpdate = new SOFTTEK.SCMS.Entity.FA.Request();

            return context.Execute(() =>
            {

                using (dataSource = new FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var result = dataSource.UpdateRequest(modelUpdate, modelSearch);
                    modelRUpdate = result;

                }
                return modelRUpdate;
            }, "Update an Request in system");
        }
        #endregion
    }
}
