using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SCMS.Data;

namespace SOFTTEK.SCMS.Business.PM
{
    public class MeasurementDocumentBO : SOFTTEK.SCMS.Foundation.Business.BusinessObject
    {
        #region Fields
        private PMDataContext dataSource;
        #endregion

        #region Connection 
        public MeasurementDocumentBO(SOFTTEK.SCMS.Foundation.Business.BusinessContext ctx)
            : base(ctx)
        {
        }
        #endregion

        #region SearchMeasurementDocument
        /// <summary>
        /// Get all the activities MeasurementDocument.
        /// </summary>
        /// <param name="modelMeasurementDocument">Model MeasurementDocument</param>
        /// <returns></returns>
        public List<SOFTTEK.SCMS.Entity.PM.MeasurementDocument> SearchMeasurementDocument(SOFTTEK.SCMS.Entity.PM.MeasurementDocument modelMeasurementDocument)
        {
            List<SOFTTEK.SCMS.Entity.PM.MeasurementDocument> listMeasurementDocument = new List<SOFTTEK.SCMS.Entity.PM.MeasurementDocument>();
            return context.Execute(() =>
            {

                using (dataSource = new PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var results = dataSource.GetMeasurementDocuments(modelMeasurementDocument);
                    listMeasurementDocument = results;
                }

                return listMeasurementDocument;
            }, "Retrieve the registered MeasurementDocument in system.");

        }

        #endregion

        #region RegisterMeasurementDocument
        /// <summary>
        /// Register MeasurementDocument.
        /// </summary>
        /// <param name="modelMeasurementDocument">modelMeasurementDocument Insert Model information MeasurementDocument</param>
        /// <returns>Registered MeasurementDocument information.</returns>
        public SOFTTEK.SCMS.Entity.PM.MeasurementDocument RegisterMeasurementDocument(SOFTTEK.SCMS.Entity.PM.MeasurementDocument modelMeasurementDocument)
        {
            SOFTTEK.SCMS.Entity.PM.MeasurementDocument modelInsert = new SOFTTEK.SCMS.Entity.PM.MeasurementDocument();

            return context.Execute(() =>
            {
                using (dataSource = new PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();
                    var result = dataSource.InsertMeasurementDocument(modelMeasurementDocument);
                    modelInsert = result;

                    for (int i = 0; i < modelMeasurementDocument.Measures.Count(); i++)
                    {
                        var modelMeasure = new SCMS.Entity.PM.Measure();
                        modelMeasure = modelMeasurementDocument.Measures[i];
                        modelMeasure.Document = new SCMS.Entity.PM.MeasurementDocument { Identifier = modelInsert.Identifier };
                        var insertMeasure = dataSource.InsertMeasure(modelMeasure);
                        var listMeasures = new List<SCMS.Entity.PM.Measure>();
                        listMeasures.Add(insertMeasure);
                        modelInsert.Measures = listMeasures; 
                    }
                }

                return modelInsert;
            }, "Register an MeasurementDocument in system");
        }
        #endregion

        #region UpdateMeasurementDocument
        /// <summary>
        /// Update an MeasurementDocument.
        /// </summary>
        /// <param name="modelUpdate">model Update Model information MeasurementDocument</param>
        /// /// <param name="modelSearch">model filter information MeasurementDocument</param>
        /// <returns>Update MeasurementDocument information.</returns>
        public SOFTTEK.SCMS.Entity.PM.MeasurementDocument UpdateMeasurementDocument(SOFTTEK.SCMS.Entity.PM.MeasurementDocument modelUpdate, SOFTTEK.SCMS.Entity.PM.MeasurementDocument modelSearch)
        {
            SOFTTEK.SCMS.Entity.PM.MeasurementDocument modelRUpdate = new SOFTTEK.SCMS.Entity.PM.MeasurementDocument();

            return context.Execute(() =>
            {

                using (dataSource = new PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var result = dataSource.UpdateMeasurementDocument(modelUpdate, modelSearch);
                    modelRUpdate = result;

                }
                return modelRUpdate;
            }, "Update an MeasurementDocument in system");
        }
        #endregion
    }
}
