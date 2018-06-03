using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SCMS.Data;

namespace SOFTTEK.SCMS.Business.FA
{
    public class PurchaseRequestBO : SOFTTEK.SCMS.Foundation.Business.BusinessObject
    {
        #region Fields
        private FAMDataContext dataSource;
        #endregion

        #region Connection 
        public PurchaseRequestBO(SOFTTEK.SCMS.Foundation.Business.BusinessContext ctx)
            : base(ctx)
        {
        }
        #endregion

        #region SearchPurchaseRequest
        /// <summary>
        /// Get all the modelPurchaseRequest.
        /// </summary>
        /// <param name="modelPurchaseRequest">modelPurchaseRequest Identifier</param>
        /// <returns></returns>
        public List<SOFTTEK.SCMS.Entity.FA.PurchaseRequest> SearchPurchaseRequests(SOFTTEK.SCMS.Entity.FA.PurchaseRequest modelPurchaseRequest)
        {
            List<SOFTTEK.SCMS.Entity.FA.PurchaseRequest> listPurchaseRequests = new List<SOFTTEK.SCMS.Entity.FA.PurchaseRequest>();
            return context.Execute(() =>
            {

                using (dataSource = new FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var results = dataSource.GetPurchaseRequests(modelPurchaseRequest);
                    listPurchaseRequests = results;
                }

                return listPurchaseRequests;
            }, "Retrieve the registered PurchaseRequests in system.");

        }

        #endregion

        #region RegisterPurchaseRequest
        /// <summary>
        /// Register PurchaseRequest.
        /// </summary>
        /// <param name="modelPurchaseRequest">modelPurchaseRequest Insert Model information PurchaseRequest</param>
        /// <returns>Registered activity information.</returns>
        public SOFTTEK.SCMS.Entity.FA.PurchaseRequest RegisterPurchaseRequest(SOFTTEK.SCMS.Entity.FA.PurchaseRequest modelPurchaseRequest)
        {
            SOFTTEK.SCMS.Entity.FA.PurchaseRequest modelInsert = new SOFTTEK.SCMS.Entity.FA.PurchaseRequest();

            return context.Execute(() =>
            {
                using (dataSource = new FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();
                    var result = dataSource.InsertPurchaseRequest(modelPurchaseRequest);
                    modelInsert = result;
                }
                return modelInsert;
            }, "Register an PurchaseRequest in system");
        }
        #endregion

        #region UpdatePurchaseRequest
        /// <summary>
        /// Update an PurchaseRequest.
        /// </summary>
        /// <param name="modelUpdate">model Update Model information PurchaseRequest</param>
        /// /// <param name="modelSearch">model filter information PurchaseRequest</param>
        /// <returns>Update PurchaseRequest information.</returns>
        public SOFTTEK.SCMS.Entity.FA.PurchaseRequest UpdatePurchaseRequest(SOFTTEK.SCMS.Entity.FA.PurchaseRequest modelUpdate, SOFTTEK.SCMS.Entity.FA.PurchaseRequest modelSearch)
        {
            SOFTTEK.SCMS.Entity.FA.PurchaseRequest modelRUpdate = new SOFTTEK.SCMS.Entity.FA.PurchaseRequest();

            return context.Execute(() =>
            {

                using (dataSource = new FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var result = dataSource.UpdatePurchaseRequest(modelUpdate, modelSearch);
                    modelRUpdate = result;

                }
                return modelRUpdate;
            }, "Update an PurchaseRequest in system");
        }
        #endregion
    }
}
