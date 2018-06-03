using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SCMS.Data;

namespace SOFTTEK.SCMS.Business.FA
{
    public class TransferRequestBO : SOFTTEK.SCMS.Foundation.Business.BusinessObject
    {
        #region Fields
        private FAMDataContext dataSource;
        #endregion

        #region Connection 
        public TransferRequestBO(SOFTTEK.SCMS.Foundation.Business.BusinessContext ctx)
            : base(ctx)
        {
        }
        #endregion

        #region SearchTransferRequest
        /// <summary>
        /// Get all the modelTransferRequest.
        /// </summary>
        /// <param name="modelTransferRequest">modelTransferRequest Identifier</param>
        /// <returns></returns>
        public List<SOFTTEK.SCMS.Entity.FA.TransferRequest> SearchTransferRequests(SOFTTEK.SCMS.Entity.FA.TransferRequest modelTransferRequest)
        {
            List<SOFTTEK.SCMS.Entity.FA.TransferRequest> listTransferRequests = new List<SOFTTEK.SCMS.Entity.FA.TransferRequest>();
            return context.Execute(() =>
            {

                using (dataSource = new FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var results = dataSource.GetTransferRequests(modelTransferRequest);
                    listTransferRequests = results;
                }

                return listTransferRequests;
            }, "Retrieve the registered TransferRequests in system.");

        }

        #endregion

        #region RegisterTransferRequest
        /// <summary>
        /// Register TransferRequest.
        /// </summary>
        /// <param name="modelTransferRequest">modelTransferRequest Insert Model information TransferRequest</param>
        /// <returns>Registered activity information.</returns>
        public SOFTTEK.SCMS.Entity.FA.TransferRequest RegisterTransferRequest(SOFTTEK.SCMS.Entity.FA.TransferRequest modelTransferRequest)
        {
            SOFTTEK.SCMS.Entity.FA.TransferRequest modelInsert = new SOFTTEK.SCMS.Entity.FA.TransferRequest();

            return context.Execute(() =>
            {
                using (dataSource = new FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();
                    var result = dataSource.InsertTransferRequest(modelTransferRequest);
                    modelInsert = result;
                }
                return modelInsert;
            }, "Register an TransferRequest in system");
        }
        #endregion

        #region UpdateTransferRequest
        /// <summary>
        /// Update an TransferRequest.
        /// </summary>
        /// <param name="modelUpdate">model Update Model information TransferRequest</param>
        /// /// <param name="modelSearch">model filter information TransferRequest</param>
        /// <returns>Update TransferRequest information.</returns>
        public SOFTTEK.SCMS.Entity.FA.TransferRequest UpdateTransferRequest(SOFTTEK.SCMS.Entity.FA.TransferRequest modelUpdate, SOFTTEK.SCMS.Entity.FA.TransferRequest modelSearch)
        {
            SOFTTEK.SCMS.Entity.FA.TransferRequest modelRUpdate = new SOFTTEK.SCMS.Entity.FA.TransferRequest();

            return context.Execute(() =>
            {

                using (dataSource = new FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var result = dataSource.UpdateTransferRequest(modelUpdate, modelSearch);
                    modelRUpdate = result;

                }
                return modelRUpdate;
            }, "Update an TransferRequest in system");
        }
        #endregion
    }
}
