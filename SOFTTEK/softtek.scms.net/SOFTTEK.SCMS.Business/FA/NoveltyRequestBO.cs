using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SCMS.Data;

namespace SOFTTEK.SCMS.Business.FA
{
    public class NoveltyRequestBO : SOFTTEK.SCMS.Foundation.Business.BusinessObject
    {
        #region Fields
        private FAMDataContext dataSource;
        #endregion

        #region Connection 
        public NoveltyRequestBO(SOFTTEK.SCMS.Foundation.Business.BusinessContext ctx)
            : base(ctx)
        {
        }
        #endregion

        #region SearchNoveltyRequest
        /// <summary>
        /// Get all the modelNoveltyRequest.
        /// </summary>
        /// <param name="modelNoveltyRequest">modelNoveltyRequest Identifier</param>
        /// <returns></returns>
        public List<SOFTTEK.SCMS.Entity.FA.NoveltyRequest> SearchNoveltyRequests(SOFTTEK.SCMS.Entity.FA.NoveltyRequest modelNoveltyRequest)
        {
            List<SOFTTEK.SCMS.Entity.FA.NoveltyRequest> listNoveltyRequests = new List<SOFTTEK.SCMS.Entity.FA.NoveltyRequest>();
            return context.Execute(() =>
            {

                using (dataSource = new FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var results = dataSource.GetNoveltyRequests(modelNoveltyRequest);
                    listNoveltyRequests = results;
                }

                return listNoveltyRequests;
            }, "Retrieve the registered NoveltyRequests in system.");

        }

        #endregion

        #region RegisterNoveltyRequest
        /// <summary>
        /// Register NoveltyRequest.
        /// </summary>
        /// <param name="modelNoveltyRequest">modelNoveltyRequest Insert Model information NoveltyRequest</param>
        /// <returns>Registered activity information.</returns>
        public SOFTTEK.SCMS.Entity.FA.NoveltyRequest RegisterNoveltyRequest(SOFTTEK.SCMS.Entity.FA.NoveltyRequest modelNoveltyRequest)
        {
            SOFTTEK.SCMS.Entity.FA.NoveltyRequest modelInsert = new SOFTTEK.SCMS.Entity.FA.NoveltyRequest();

            return context.Execute(() =>
            {
                using (dataSource = new FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();
                    var result = dataSource.InsertNoveltyRequest(modelNoveltyRequest);
                    modelInsert = result;
                }
                return modelInsert;
            }, "Register an NoveltyRequest in system");
        }
        #endregion

        #region UpdateNoveltyRequest
        /// <summary>
        /// Update an NoveltyRequest.
        /// </summary>
        /// <param name="modelUpdate">model Update Model information NoveltyRequest</param>
        /// /// <param name="modelSearch">model filter information NoveltyRequest</param>
        /// <returns>Update NoveltyRequest information.</returns>
        public SOFTTEK.SCMS.Entity.FA.NoveltyRequest UpdateNoveltyRequest(SOFTTEK.SCMS.Entity.FA.NoveltyRequest modelUpdate, SOFTTEK.SCMS.Entity.FA.NoveltyRequest modelSearch)
        {
            SOFTTEK.SCMS.Entity.FA.NoveltyRequest modelRUpdate = new SOFTTEK.SCMS.Entity.FA.NoveltyRequest();

            return context.Execute(() =>
            {

                using (dataSource = new FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var result = dataSource.UpdateNoveltyRequest(modelUpdate, modelSearch);
                    modelRUpdate = result;

                }
                return modelRUpdate;
            }, "Update an NoveltyRequest in system");
        }
        #endregion
    }
}
