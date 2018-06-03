using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SCMS.Data;

namespace SOFTTEK.SCMS.Business.PM
{
    public class MaterialBO : SOFTTEK.SCMS.Foundation.Business.BusinessObject
    {
        #region Fields
        private PMDataContext dataSource;
        #endregion

        #region Connection 
        public MaterialBO(SOFTTEK.SCMS.Foundation.Business.BusinessContext ctx)
            : base(ctx)
        {
        }
        #endregion

        #region SearchMaterial
        /// <summary>
        /// Get all the activities Material.
        /// </summary>
        /// <param name="modelMaterial">Model Material</param>
        /// <returns></returns>
        public List<SOFTTEK.SCMS.Entity.PM.Material> SearchMaterial(SOFTTEK.SCMS.Entity.PM.Material modelMaterial)
        {
            List<SOFTTEK.SCMS.Entity.PM.Material> listMaterials = new List<SOFTTEK.SCMS.Entity.PM.Material>();
            return context.Execute(() =>
            {

                using (dataSource = new PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var results = dataSource.GetMaterials(modelMaterial);
                    listMaterials = results;
                }

                return listMaterials;
            }, "Retrieve the registered Materials in system.");

        }

        #endregion

        #region RegisterMaterial
        /// <summary>
        /// Register Material.
        /// </summary>
        /// <param name="modelMaterial">modelMaterial Insert Model information Material</param>
        /// <returns>Registered Material information.</returns>
        public SOFTTEK.SCMS.Entity.PM.Material RegisterMaterial(SOFTTEK.SCMS.Entity.PM.Material modelMaterial)
        {
            SOFTTEK.SCMS.Entity.PM.Material modelInsert = new SOFTTEK.SCMS.Entity.PM.Material();

            return context.Execute(() =>
            {
                using (dataSource = new PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();
                    var result = dataSource.InsertMaterial(modelMaterial);
                    modelInsert = result;
                }
                return modelInsert;
            }, "Register an Material in system");
        }
        #endregion

        #region UpdateMaterial
        /// <summary>
        /// Update an Material.
        /// </summary>
        /// <param name="modelUpdate">model Update Model information Material</param>
        /// /// <param name="modelSearch">model filter information Material</param>
        /// <returns>Update Material information.</returns>
        public SOFTTEK.SCMS.Entity.PM.Material UpdateMaterial(SOFTTEK.SCMS.Entity.PM.Material modelUpdate, SOFTTEK.SCMS.Entity.PM.Material modelSearch)
        {
            SOFTTEK.SCMS.Entity.PM.Material modelRUpdate = new SOFTTEK.SCMS.Entity.PM.Material();

            return context.Execute(() =>
            {

                using (dataSource = new PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var result = dataSource.UpdateMaterial(modelUpdate, modelSearch);
                    modelRUpdate = result;

                }
                return modelRUpdate;
            }, "Update an Material in system");
        }
        #endregion
    }
}
