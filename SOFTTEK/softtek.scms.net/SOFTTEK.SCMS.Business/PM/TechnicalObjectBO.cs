using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SCMS.Data;

namespace SOFTTEK.SCMS.Business.PM
{
    public class TechnicalObjectBO : SOFTTEK.SCMS.Foundation.Business.BusinessObject
    {
        #region Fields
        private PMDataContext dataSource;
        #endregion

        #region Connection 
        public TechnicalObjectBO(SOFTTEK.SCMS.Foundation.Business.BusinessContext ctx)
            : base(ctx)
        {
        }
        #endregion

        #region SearchTechnicalObject
        /// <summary>
        /// Get all the activities TechnicalObject.
        /// </summary>
        /// <param name="modelTechnicalObject">Model TechnicalObject</param>
        /// <returns></returns>
        public List<SOFTTEK.SCMS.Entity.PM.TechnicalObject> SearchTechnicalObject(SOFTTEK.SCMS.Entity.PM.TechnicalObject modelTechnicalObject)
        {
            List<SOFTTEK.SCMS.Entity.PM.TechnicalObject> listTechnicalObjects = new List<SOFTTEK.SCMS.Entity.PM.TechnicalObject>();
            return context.Execute(() =>
            {

                using (dataSource = new PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var results = dataSource.GetTechnicalObjects(modelTechnicalObject);
                    listTechnicalObjects = results;
                }

                return listTechnicalObjects;
            }, "Retrieve the registered TechnicalObjects in system.");

        }

        #endregion

        #region SearchWorkOrdesWithTechnicalObject
        /// <summary>
        /// Get all the activities TechnicalObject.
        /// </summary>
        /// <param name="modelTechnicalObject">Model TechnicalObject</param>
        /// <returns></returns>
        public List<SOFTTEK.SCMS.Entity.PM.WorkOrder> SearchWorkOrdesWithTechnicalObject(SOFTTEK.SCMS.Entity.PM.TechnicalObject modelTechnicalObject)
        {
            List<SOFTTEK.SCMS.Entity.PM.TechnicalObject> listTechnicalObjects = new List<SOFTTEK.SCMS.Entity.PM.TechnicalObject>();
            List<SOFTTEK.SCMS.Entity.PM.WorkOrder> listWorkOrders = new List<SOFTTEK.SCMS.Entity.PM.WorkOrder>();
            return context.Execute(() =>
            {

                using (dataSource = new PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var modelWorkOrders = new SOFTTEK.SCMS.Entity.PM.WorkOrder();
                    //modelWorkOrders.TechnicalObject.Identifier = (resultsTechnical[0]).Identifier;
                    var resultsWorkOrders = dataSource.GetWorkOrders(modelWorkOrders);

                    var resultsTechnical = dataSource.GetTechnicalObjects(modelTechnicalObject);
                    if(resultsTechnical == null)
                    {
                        resultsWorkOrders = (from x in resultsWorkOrders select x).ToList();
                        listWorkOrders = resultsWorkOrders;
                    }

                }

                return listWorkOrders;
            }, "Retrieve the registered TechnicalObjects in system.");

        }

        #endregion

        #region RegisterTechnicalObject
        /// <summary>
        /// Register TechnicalObject.
        /// </summary>
        /// <param name="modelTechnicalObject">modelTechnicalObject Insert Model information TechnicalObject</param>
        /// <returns>Registered activity information.</returns>
        public SOFTTEK.SCMS.Entity.PM.TechnicalObject RegisterTechnicalObject(SOFTTEK.SCMS.Entity.PM.TechnicalObject modelTechnicalObject)
        {
            SOFTTEK.SCMS.Entity.PM.TechnicalObject modelInsert = new SOFTTEK.SCMS.Entity.PM.TechnicalObject();

            return context.Execute(() =>
            {
                using (dataSource = new PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();
                    var result = dataSource.InsertTechnicalObject(modelTechnicalObject);
                    modelInsert = result;
                }
                return modelInsert;
            }, "Register an TechnicalObjects in system");
        }
        #endregion

        #region UpdateTechnicalObject
        /// <summary>
        /// Update an TechnicalObject.
        /// </summary>
        /// <param name="modelUpdate">model Update Model information TechnicalObject</param>
        /// /// <param name="modelSearch">model filter information TechnicalObject</param>
        /// <returns>Update TechnicalObject information.</returns>
        public SOFTTEK.SCMS.Entity.PM.TechnicalObject UpdateTechnicalObject(SOFTTEK.SCMS.Entity.PM.TechnicalObject modelUpdate, SOFTTEK.SCMS.Entity.PM.TechnicalObject modelSearch)
        {
            SOFTTEK.SCMS.Entity.PM.TechnicalObject modelRUpdate = new SOFTTEK.SCMS.Entity.PM.TechnicalObject();

            return context.Execute(() =>
            {

                using (dataSource = new PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var result = dataSource.UpdateTechnicalObject(modelUpdate, modelSearch);
                    modelRUpdate = result;

                }
                return modelRUpdate;
            }, "Update an TechnicalObject in system");
        }
        #endregion
    }
}
