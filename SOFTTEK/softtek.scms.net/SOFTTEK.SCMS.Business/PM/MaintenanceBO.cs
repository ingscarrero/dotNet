using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFTTEK.SCMS.Business.PM
{
    /// <summary>
    /// Plant Maintenance Business Abstraction
    /// </summary>
    public class MaintenanceBO : Foundation.Business.BusinessObject
    {
        private const string kPMCommitteStatus = "Commited";
        private const string kPMScheduledStatus = "Scheduled";
        private SOFTTEK.SCMS.Data.PMDataContext dataSource;

        /// <summary>
        /// Default Constructor for a Plant Maintenance Business Object instance
        /// </summary>
        /// <param name="ctx"></param>
        public MaintenanceBO(Foundation.Business.BusinessContext ctx)
            : base(ctx)
        {
        }

        /// <summary>
        /// Retrive all SAP pendig Work Orders for the correspondieng SAP User for the Given Security Context´s Device
        /// </summary>
        /// <returns></returns>
        public List<SCMS.Entity.PM.WorkOrder> RetrievePendingWorkOrdersForDevice()
        {
            return context.Execute(() =>
            {
                //string sapUser = GetDeviceSAPUser();
                string sapUser = "JEGONZALEZ";

                SCMS.Entity.PM.WorkOrder filter = new SCMS.Entity.PM.WorkOrder();
                filter.Performer = sapUser;
                filter.State = kPMScheduledStatus;

                DateTime from = DateTime.Now.Date;
                DateTime to = from.AddDays(1);

                List<SCMS.Entity.PM.WorkOrder> workOrders;

                workOrders = GetSAPWorkOrdersInPeriod(sapUser, from, to);

                if (workOrders == null || workOrders.Count == 0)
                {
                    throw new Exception("No PM Work Orders were found in SAP for the given criteria.");
                }

                using (dataSource = new Data.PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    //workOrders = dataSource.GetWorkOrders(filter).Where(wo => wo.ScheduledTo.Date.CompareTo(from.Date) > 0 && wo.ScheduledTo.Date.CompareTo(to.Date) < 1).ToList();
                    for (int i = 0; i < workOrders.Count(); i++)
                    {
                        var modelTechnicalObject = new SCMS.Entity.PM.TechnicalObject { Identifier = workOrders[i].TechnicalObject.Identifier };
                        var selectTechnicalObject = dataSource.GetTechnicalObjects(modelTechnicalObject);
                        workOrders[i].TechnicalObject = (from x in selectTechnicalObject select x).FirstOrDefault();
                    }
                }
                return workOrders;
            }, "Retrieve Pending Work Orders for a given device´s SAP User");
        }

        /// <summary>
        /// Retrieve the Technical Object in SAP corresponding to the SAP.
        /// </summary>
        /// <param name="sapUser"></param>
        /// <returns>SAP Work Orders Information</returns>
        private SCMS.Entity.PM.Material InsertSapMaterial(string sapUser, SOFTTEK.SCMS.Entity.PM.Material modelInsert)
        {
            SCMS.Entity.PM.Material returnList = new SCMS.Entity.PM.Material();

            return context.Execute(() =>
            {
                SOFTTEK.SAP.Integration.SAPConnection sapConnector = new SAP.Integration.SAPConnection();

                var connectDestination = sapConnector.LoadDestination();
                var destinationRepository = connectDestination.Repository;

                var initializerRFC = destinationRepository.CreateFunction(new System.Configuration.AppSettingsReader().GetValue("SAP_RFC_CREATE_ADVICE", typeof(string)).ToString());
                var rfcMetadata = initializerRFC.Metadata;

                bool stateConnection = Ping(connectDestination);

                Dictionary<string, object> input = new Dictionary<string, object>();
                Dictionary<string, object> output = new Dictionary<string, object>();
                Dictionary<object, object> tables = new Dictionary<object, object>();
                Dictionary<object, object> estructuras = new Dictionary<object, object>();

                TimeSpan ts = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                TIME time = new TIME(Convert.ToUInt32(DateTime.Now.Hour), Convert.ToUInt32(DateTime.Now.Minute), Convert.ToUInt32(DateTime.Now.Second));

                Dictionary<string, object> dic = new Dictionary<string, object>();
                //dic.Add("EQUIPMENT", "000000000010013816");
                //dic.Add("FUNCT_LOC", "?0100000000000005587");
                //dic.Add("PRIORITY", modelInsert.Priority);
                //dic.Add("NOTIF_DATE", DateTime.Now);
                //dic.Add("SHORT_TEXT", modelInsert.Comments);
                //dic.Add("BREAKDOWN", modelInsert.Type);
                //dic.Add("STRMLFNDATE", DateTime.Now);
                //dic.Add("STRMLFNTIME", time.ToString());
                //dic.Add("ENDMLFNDATE", DateTime.Now);
                //dic.Add("ENDMLFNTIME", time.ToString());
                IRfcStructure structureAdviceInsert = SOFTTEK.SAP.Integration.SAPData.CreateRFCStructure(dic, connectDestination, "ZPMS_AVISOS_MANTENIMIENTO");

                initializerRFC.SetValue("IM_NOTIFHEADER", structureAdviceInsert);
                //testRFC.SetValue("I_PUNTO", document.Measures.FirstOrDefault().ExternalIdentifier.ToString());
                initializerRFC.SetValue("IM_NOTIF_TYPE", "M1");

                if (stateConnection)
                    initializerRFC.Invoke(connectDestination);

                if (initializerRFC.GetString("EV_INDICADOR") == "0")
                {
                    for (int i = 0; i < rfcMetadata.ParameterCount; i++)
                    {
                        if (rfcMetadata[i].Direction.Equals(global::SAP.Middleware.Connector.RfcDirection.IMPORT))
                        {
                            Type inputType = SAP.Integration.SAPData.GetDataType(rfcMetadata[i].DataType);
                            input[rfcMetadata[i].Name] = initializerRFC.GetValue(rfcMetadata[i].Name);//inputType.IsValueType ? Activator.CreateInstance(inputType) : null;
                        }
                        else if (rfcMetadata[i].Direction.Equals(global::SAP.Middleware.Connector.RfcDirection.EXPORT))
                        {
                            Type outputType = SAP.Integration.SAPData.GetDataType(rfcMetadata[i].DataType);
                            output[rfcMetadata[i].Name] = initializerRFC.GetValue(rfcMetadata[i].Name); //outputType.IsValueType ? Activator.CreateInstance(outputType) : null;
                        }
                        else if (rfcMetadata[i].Direction.Equals(global::SAP.Middleware.Connector.RfcDirection.TABLES))
                        {
                            Type tablesType = SAP.Integration.SAPData.GetDataType(rfcMetadata[i].DataType);
                            tables[rfcMetadata[i].Name] = initializerRFC.GetValue(rfcMetadata[i].Name); //outputType.IsValueType ? Activator.CreateInstance(outputType) : null;
                        }
                        else if (rfcMetadata[i].Direction.Equals(global::SAP.Middleware.Connector.RfcDataType.STRUCTURE))
                        {
                            Type estructurasType = SAP.Integration.SAPData.GetDataType(rfcMetadata[i].DataType);
                            estructuras[rfcMetadata[i].Name] = initializerRFC.GetValue(rfcMetadata[i].Name);
                        }
                    }
                }
                else
                {
                    throw new Exception(String.Format("No ha sido posible cosultar las ordenes de trabajo. Causa: {0}", initializerRFC.GetString("EV_TEXT")));
                }

                List<string> cnt = new List<string>();
                List<IRfcStructure> irc = new List<IRfcStructure>();
                IRfcTable detail = initializerRFC["GT_OBJETO_TECNICO"].GetTable();

                List<SOFTTEK.SCMS.Entity.PM.Material> listMaterial = new List<SCMS.Entity.PM.Material>();
                List<Dictionary<string, object>> dictonaryMetadataTable = SOFTTEK.SAP.Integration.SAPData.GetListDictionaryForMetadataExport(initializerRFC, "GT_OBJETO_TECNICO");

                foreach (Dictionary<string, object> list in dictonaryMetadataTable)
                {
                    SOFTTEK.SCMS.Entity.PM.Material insert_Material = new SCMS.Entity.PM.Material();
                }

                return returnList;
            }, string.Format("Submit PM to SAP available Material for {0}", sapUser));
        }

        /// <summary>
        /// Insert Advice in SAP corresponding to the SAP.
        /// </summary>
        /// <param name="sapUser"></param>
        /// <returns>SAP insert advice</returns>
        private SCMS.Entity.PM.Advice InsertSapAdvice(string sapUser, SOFTTEK.SCMS.Entity.PM.Advice modelInsert)
        {
            return context.Execute(() =>
            {
                List<SCMS.Entity.PM.TechnicalObject> sapTechnicalObjects = new List<SCMS.Entity.PM.TechnicalObject>();

                SOFTTEK.SAP.Integration.SAPConnection sapConnector = new SAP.Integration.SAPConnection();

                var connectDestination = sapConnector.LoadDestination();
                var destinationRepository = connectDestination.Repository;

                var initializerRFC = destinationRepository.CreateFunction(new System.Configuration.AppSettingsReader().GetValue("SAP_RFC_CREATE_ADVICE", typeof(string)).ToString());
                var rfcMetadata = initializerRFC.Metadata;

                bool stateConnection = Ping(connectDestination);

                Dictionary<string, object> input = new Dictionary<string, object>();
                Dictionary<string, object> output = new Dictionary<string, object>();
                Dictionary<object, object> tables = new Dictionary<object, object>();
                Dictionary<object, object> estructuras = new Dictionary<object, object>();

                TimeSpan ts = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                TIME time = new TIME(Convert.ToUInt32(DateTime.Now.Hour), Convert.ToUInt32(DateTime.Now.Minute), Convert.ToUInt32(DateTime.Now.Second));
                var dateStart = Convert.ToDateTime(modelInsert.ExecutionStartAt);
                var dateEnd = Convert.ToDateTime(modelInsert.ExecutionFinishedAt);

                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("EQUIPMENT", "000000000010013816");
                dic.Add("FUNCT_LOC", "?0100000000000005587");
                dic.Add("PRIORITY", modelInsert.Priority);
                dic.Add("NOTIF_DATE", DateTime.Now);
                dic.Add("SHORT_TEXT", modelInsert.Comments);
                dic.Add("BREAKDOWN", modelInsert.Type);
                dic.Add("STRMLFNDATE", dateStart);
                dic.Add("STRMLFNTIME", time.ToString());
                dic.Add("ENDMLFNDATE", dateEnd);
                dic.Add("ENDMLFNTIME", time.ToString());
                IRfcStructure structureAdviceInsert = SOFTTEK.SAP.Integration.SAPData.CreateRFCStructure(dic, connectDestination, "ZPMS_AVISOS_MANTENIMIENTO");

                initializerRFC.SetValue("IM_NOTIFHEADER", structureAdviceInsert);
                //testRFC.SetValue("I_PUNTO", document.Measures.FirstOrDefault().ExternalIdentifier.ToString());
                initializerRFC.SetValue("IM_NOTIF_TYPE", "M1");

                if (stateConnection)
                    initializerRFC.Invoke(connectDestination);

                if (initializerRFC.GetString("EV_INDICADOR") == "0")
                {
                    for (int i = 0; i < rfcMetadata.ParameterCount; i++)
                    {
                        if (rfcMetadata[i].Direction.Equals(global::SAP.Middleware.Connector.RfcDirection.IMPORT))
                        {
                            Type inputType = SAP.Integration.SAPData.GetDataType(rfcMetadata[i].DataType);
                            input[rfcMetadata[i].Name] = initializerRFC.GetValue(rfcMetadata[i].Name);//inputType.IsValueType ? Activator.CreateInstance(inputType) : null;
                        }
                        else if (rfcMetadata[i].Direction.Equals(global::SAP.Middleware.Connector.RfcDirection.EXPORT))
                        {
                            Type outputType = SAP.Integration.SAPData.GetDataType(rfcMetadata[i].DataType);
                            output[rfcMetadata[i].Name] = initializerRFC.GetValue(rfcMetadata[i].Name); //outputType.IsValueType ? Activator.CreateInstance(outputType) : null;
                        }
                        else if (rfcMetadata[i].Direction.Equals(global::SAP.Middleware.Connector.RfcDirection.TABLES))
                        {
                            Type tablesType = SAP.Integration.SAPData.GetDataType(rfcMetadata[i].DataType);
                            tables[rfcMetadata[i].Name] = initializerRFC.GetValue(rfcMetadata[i].Name); //outputType.IsValueType ? Activator.CreateInstance(outputType) : null;
                        }
                        else if (rfcMetadata[i].Direction.Equals(global::SAP.Middleware.Connector.RfcDataType.STRUCTURE))
                        {
                            Type estructurasType = SAP.Integration.SAPData.GetDataType(rfcMetadata[i].DataType);
                            estructuras[rfcMetadata[i].Name] = initializerRFC.GetValue(rfcMetadata[i].Name);
                        }
                    }
                }
                else
                {
                    throw new Exception(String.Format("No ha sido posible cosultar las ordenes de trabajo. Causa: {0}", initializerRFC.GetString("EV_TEXT")));
                }

                List<string> cnt = new List<string>();
                List<IRfcStructure> irc = new List<IRfcStructure>();

                List<SOFTTEK.SCMS.Entity.PM.Advice> listAdvice = new List<SCMS.Entity.PM.Advice>();
                List<Dictionary<string, object>> dictonaryMetadataTable = SOFTTEK.SAP.Integration.SAPData.GetListDictionaryForMetadataExport(initializerRFC, "ZPMT_NOTIF_CREATE");

                SOFTTEK.SCMS.Entity.PM.Advice insert_Advice = new SCMS.Entity.PM.Advice();
                foreach (Dictionary<string, object> list in dictonaryMetadataTable)
                {
                    var value_Not = list["NOTIF_NO"].ToString() != null ? list["NOTIF_NO"].ToString() : string.Empty;
                    insert_Advice.ExternalIdentifier = value_Not;
                    insert_Advice.ExecutionStartAt = modelInsert.ExecutionStartAt;
                    insert_Advice.ExecutionFinishedAt = modelInsert.ExecutionFinishedAt;
                    insert_Advice.ExecutionHourStartAt = modelInsert.ExecutionHourStartAt;
                    insert_Advice.ExecutionHourFinishedAt = modelInsert.ExecutionHourFinishedAt;
                    insert_Advice.Comments = modelInsert.Comments;
                    insert_Advice.DeviceType = "";
                    insert_Advice.Priority = "";
                    insert_Advice.ScheduledTo = DateTime.Now;
                    insert_Advice.Type = "";
                    insert_Advice.Task = new SCMS.Entity.PM.Task { Identifier = modelInsert.Task.Identifier };
                    insert_Advice.TechnicalObject = new SCMS.Entity.PM.TechnicalObject { Identifier = modelInsert.TechnicalObject.Identifier };
                }

                var insertModel = RegisterAdvice(insert_Advice);

                return insertModel;
            }, string.Format("Retrieve PM SAP available Advice for {0}", sapUser));
        }

        /// <summary>
        /// Insert notifications in SAP corresponding to the SAP.
        /// </summary>
        /// <param name="sapUser"></param>
        /// <returns>SAP insert notifications</returns>
        private SCMS.Entity.PM.Notifications InsertSapEndNotifications(SCMS.Entity.PM.Notifications modelNotifications)
        {
            //

            SOFTTEK.SAP.Integration.SAPConnection sapConnector = new SAP.Integration.SAPConnection();

            var connectDestination = sapConnector.LoadDestination();
            var destinationRepository = connectDestination.Repository;

            //var testRFC = testDestinationRepository.CreateFunction("ZRFC_PM_TOMAR_MEDIDA");
            var initializerRFC = destinationRepository.CreateFunction(new System.Configuration.AppSettingsReader().GetValue("SAP_RFC_UPDATE_NOTIFICATION", typeof(string)).ToString());
            var rfcMetadata = initializerRFC.Metadata;

            bool stateConnection = Ping(connectDestination);

            Dictionary<string, object> input = new Dictionary<string, object>();
            Dictionary<string, object> output = new Dictionary<string, object>();

            initializerRFC.SetValue("LTXA1", modelNotifications.Comments);
            //testRFC.SetValue("I_PUNTO", document.Measures.FirstOrDefault().ExternalIdentifier.ToString());
            initializerRFC.SetValue("ISMNW", modelNotifications.ActualWork);
            initializerRFC.SetValue("BUDAT", modelNotifications.ExecutionStartAt);

            if (stateConnection)
                initializerRFC.Invoke(connectDestination);

            if (initializerRFC.GetString("EV_INDICADOR") == "0")
            {
                for (int i = 0; i < rfcMetadata.ParameterCount; i++)
                {
                    if (rfcMetadata[i].Direction.Equals(global::SAP.Middleware.Connector.RfcDirection.IMPORT))
                    {
                        Type inputType = SAP.Integration.SAPData.GetDataType(rfcMetadata[i].DataType);
                        input[rfcMetadata[i].Name] = initializerRFC.GetValue(rfcMetadata[i].Name);//inputType.IsValueType ? Activator.CreateInstance(inputType) : null;
                    }
                    else if (rfcMetadata[i].Direction.Equals(global::SAP.Middleware.Connector.RfcDirection.EXPORT))
                    {
                        Type outputType = SAP.Integration.SAPData.GetDataType(rfcMetadata[i].DataType);
                        output[rfcMetadata[i].Name] = initializerRFC.GetValue(rfcMetadata[i].Name); //outputType.IsValueType ? Activator.CreateInstance(outputType) : null;
                    }
                }
                // TO DO:
                //var name = output["EV_DOCUMENTO"].ToString();
                //document.ExternalIdentifier = name;
                return modelNotifications;
            }
            else
            {
                throw new Exception(String.Format("No ha sido posible insertar el documento de medida. Causa: {0}", initializerRFC.GetString("EV_TEXT")));
            }
        }

        /// <summary>
        /// Insert notifications in SAP corresponding to the SAP.
        /// </summary>
        /// <param name="sapUser"></param>
        /// <returns>SAP insert notifications</returns>
        private SCMS.Entity.PM.Advice InsertSapNotifications(string sapUser, SOFTTEK.SCMS.Entity.PM.Advice modelInsert)
        {
            return context.Execute(() =>
            {
                List<SCMS.Entity.PM.TechnicalObject> sapTechnicalObjects = new List<SCMS.Entity.PM.TechnicalObject>();

                SOFTTEK.SAP.Integration.SAPConnection sapConnector = new SAP.Integration.SAPConnection();

                var connectDestination = sapConnector.LoadDestination();
                var destinationRepository = connectDestination.Repository;

                var initializerRFC = destinationRepository.CreateFunction(new System.Configuration.AppSettingsReader().GetValue("SAP_RFC_CREATE_ADVICE", typeof(string)).ToString());
                var rfcMetadata = initializerRFC.Metadata;

                bool stateConnection = Ping(connectDestination);

                Dictionary<string, object> input = new Dictionary<string, object>();
                Dictionary<string, object> output = new Dictionary<string, object>();
                Dictionary<object, object> tables = new Dictionary<object, object>();
                Dictionary<object, object> estructuras = new Dictionary<object, object>();

                TimeSpan ts = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                TIME time = new TIME(Convert.ToUInt32(DateTime.Now.Hour), Convert.ToUInt32(DateTime.Now.Minute), Convert.ToUInt32(DateTime.Now.Second));
                var dateStart = Convert.ToDateTime(modelInsert.ExecutionStartAt);
                var dateEnd = Convert.ToDateTime(modelInsert.ExecutionFinishedAt);

                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("EQUIPMENT", "000000000010013816");
                dic.Add("FUNCT_LOC", "?0100000000000005587");
                dic.Add("PRIORITY", modelInsert.Priority);
                dic.Add("NOTIF_DATE", DateTime.Now);
                dic.Add("SHORT_TEXT", modelInsert.Comments);
                dic.Add("BREAKDOWN", modelInsert.Type);
                dic.Add("STRMLFNDATE", dateStart);
                dic.Add("STRMLFNTIME", time.ToString());
                dic.Add("ENDMLFNDATE", dateEnd);
                dic.Add("ENDMLFNTIME", time.ToString());
                IRfcStructure structureAdviceInsert = SOFTTEK.SAP.Integration.SAPData.CreateRFCStructure(dic, connectDestination, "ZPMS_AVISOS_MANTENIMIENTO");

                initializerRFC.SetValue("IM_NOTIFHEADER", structureAdviceInsert);
                //testRFC.SetValue("I_PUNTO", document.Measures.FirstOrDefault().ExternalIdentifier.ToString());
                initializerRFC.SetValue("IM_NOTIF_TYPE", "M1");

                if (stateConnection)
                    initializerRFC.Invoke(connectDestination);

                if (initializerRFC.GetString("EV_INDICADOR") == "0")
                {
                    for (int i = 0; i < rfcMetadata.ParameterCount; i++)
                    {
                        if (rfcMetadata[i].Direction.Equals(global::SAP.Middleware.Connector.RfcDirection.IMPORT))
                        {
                            Type inputType = SAP.Integration.SAPData.GetDataType(rfcMetadata[i].DataType);
                            input[rfcMetadata[i].Name] = initializerRFC.GetValue(rfcMetadata[i].Name);//inputType.IsValueType ? Activator.CreateInstance(inputType) : null;
                        }
                        else if (rfcMetadata[i].Direction.Equals(global::SAP.Middleware.Connector.RfcDirection.EXPORT))
                        {
                            Type outputType = SAP.Integration.SAPData.GetDataType(rfcMetadata[i].DataType);
                            output[rfcMetadata[i].Name] = initializerRFC.GetValue(rfcMetadata[i].Name); //outputType.IsValueType ? Activator.CreateInstance(outputType) : null;
                        }
                        else if (rfcMetadata[i].Direction.Equals(global::SAP.Middleware.Connector.RfcDirection.TABLES))
                        {
                            Type tablesType = SAP.Integration.SAPData.GetDataType(rfcMetadata[i].DataType);
                            tables[rfcMetadata[i].Name] = initializerRFC.GetValue(rfcMetadata[i].Name); //outputType.IsValueType ? Activator.CreateInstance(outputType) : null;
                        }
                        else if (rfcMetadata[i].Direction.Equals(global::SAP.Middleware.Connector.RfcDataType.STRUCTURE))
                        {
                            Type estructurasType = SAP.Integration.SAPData.GetDataType(rfcMetadata[i].DataType);
                            estructuras[rfcMetadata[i].Name] = initializerRFC.GetValue(rfcMetadata[i].Name);
                        }
                    }
                }
                else
                {
                    throw new Exception(String.Format("No ha sido posible cosultar las ordenes de trabajo. Causa: {0}", initializerRFC.GetString("EV_TEXT")));
                }

                List<string> cnt = new List<string>();
                List<IRfcStructure> irc = new List<IRfcStructure>();

                List<SOFTTEK.SCMS.Entity.PM.Advice> listAdvice = new List<SCMS.Entity.PM.Advice>();
                List<Dictionary<string, object>> dictonaryMetadataTable = SOFTTEK.SAP.Integration.SAPData.GetListDictionaryForMetadataExport(initializerRFC, "ZPMT_NOTIF_CREATE");

                SOFTTEK.SCMS.Entity.PM.Advice insert_Advice = new SCMS.Entity.PM.Advice();
                foreach (Dictionary<string, object> list in dictonaryMetadataTable)
                {
                    var value_Not = list["NOTIF_NO"].ToString() != null ? list["NOTIF_NO"].ToString() : string.Empty;
                    insert_Advice.ExternalIdentifier = value_Not;
                    insert_Advice.ExecutionStartAt = modelInsert.ExecutionStartAt;
                    insert_Advice.ExecutionFinishedAt = modelInsert.ExecutionFinishedAt;
                    insert_Advice.ExecutionHourStartAt = modelInsert.ExecutionHourStartAt;
                    insert_Advice.ExecutionHourFinishedAt = modelInsert.ExecutionHourFinishedAt;
                    insert_Advice.Comments = modelInsert.Comments;
                    insert_Advice.DeviceType = "";
                    insert_Advice.Priority = "";
                    insert_Advice.ScheduledTo = DateTime.Now;
                    insert_Advice.Type = "";
                    insert_Advice.Task = new SCMS.Entity.PM.Task { Identifier = modelInsert.Task.Identifier };
                    insert_Advice.TechnicalObject = new SCMS.Entity.PM.TechnicalObject { Identifier = modelInsert.TechnicalObject.Identifier };
                }

                var insertModel = RegisterAdvice(insert_Advice);

                return insertModel;
            }, string.Format("Retrieve PM SAP available Advice for {0}", sapUser));
        }

        /// <summary>
        /// Retrieve PM Technical Object and its corresponding Measures
        /// </summary>
        /// <returns>Retrieved PM Technical Object on SAP</returns>
        public List<SCMS.Entity.PM.TechnicalObject> RetrievePMTechnicalObjects()
        {
            return context.Execute(() =>
            {
                //string sapUser = GetDeviceSAPUser();
                string sapUser = "JEGONZALEZ";
                List<SCMS.Entity.PM.TechnicalObject> technicalLocations = GetSapTechnicalObjects(sapUser);
                return technicalLocations;
            }, "Retrieve PM Technical Object for a given device´s SAP User");
        }

        /// <summary>
        /// Retrieve the Technical Object in SAP corresponding to the SAP.
        /// </summary>
        /// <param name="sapUser"></param>
        /// <returns>SAP Work Orders Information</returns>
        private List<SCMS.Entity.PM.TechnicalObject> GetSapTechnicalObjects(string sapUser)
        {
            List<SCMS.Entity.PM.TechnicalObject> returnList = new List<SCMS.Entity.PM.TechnicalObject>();

            return context.Execute(() =>
            {
                List<SCMS.Entity.PM.TechnicalObject> sapTechnicalObjects;
                sapTechnicalObjects = new List<SCMS.Entity.PM.TechnicalObject>();

                SOFTTEK.SAP.Integration.SAPConnection sapConnector = new SAP.Integration.SAPConnection();
                var connectDestination = sapConnector.LoadDestination();
                var destinationRepository = connectDestination.Repository;

                var initializerRFC = destinationRepository.CreateFunction(new System.Configuration.AppSettingsReader().GetValue("SAP_RFC_PM_TECHNICAL_OBJECT", typeof(string)).ToString());
                var rfcMetadata = initializerRFC.Metadata;
                bool stateConnection = Ping(connectDestination);

                Dictionary<string, object> input = new Dictionary<string, object>();
                Dictionary<string, object> output = new Dictionary<string, object>();
                Dictionary<object, object> tables = new Dictionary<object, object>();

                if (stateConnection)
                    initializerRFC.Invoke(connectDestination);

                if (initializerRFC.GetString("EV_INDICADOR") == "0")
                {
                    for (int i = 0; i < rfcMetadata.ParameterCount; i++)
                    {
                        if (rfcMetadata[i].Direction.Equals(global::SAP.Middleware.Connector.RfcDirection.IMPORT))
                        {
                            Type inputType = SAP.Integration.SAPData.GetDataType(rfcMetadata[i].DataType);
                            input[rfcMetadata[i].Name] = initializerRFC.GetValue(rfcMetadata[i].Name);//inputType.IsValueType ? Activator.CreateInstance(inputType) : null;
                        }
                        else if (rfcMetadata[i].Direction.Equals(global::SAP.Middleware.Connector.RfcDirection.EXPORT))
                        {
                            Type outputType = SAP.Integration.SAPData.GetDataType(rfcMetadata[i].DataType);
                            output[rfcMetadata[i].Name] = initializerRFC.GetValue(rfcMetadata[i].Name); //outputType.IsValueType ? Activator.CreateInstance(outputType) : null;
                        }
                        else if (rfcMetadata[i].Direction.Equals(global::SAP.Middleware.Connector.RfcDirection.TABLES))
                        {
                            Type tablesType = SAP.Integration.SAPData.GetDataType(rfcMetadata[i].DataType);
                            tables[rfcMetadata[i].Name] = initializerRFC.GetValue(rfcMetadata[i].Name); //outputType.IsValueType ? Activator.CreateInstance(outputType) : null;
                        }
                    }
                }
                else
                {
                    throw new Exception(String.Format("No ha sido posible cosultar las ordenes de trabajo. Causa: {0}", initializerRFC.GetString("EV_TEXT")));
                }

                List<string> cnt = new List<string>();
                List<IRfcStructure> irc = new List<IRfcStructure>();
                IRfcTable detail = initializerRFC["GT_OBJETO_TECNICO"].GetTable();
                List<SOFTTEK.SCMS.Entity.PM.TechnicalObject> listTechnicalObjects = new List<SCMS.Entity.PM.TechnicalObject>();
                List<Dictionary<string, object>> dictonaryMetadataTable = SOFTTEK.SAP.Integration.SAPData.GetListDictionaryForMetadataExport(initializerRFC, "GT_OBJETO_TECNICO");

                foreach (Dictionary<string, object> list in dictonaryMetadataTable)
                {
                    SOFTTEK.SCMS.Entity.PM.TechnicalObject insert_TechnicalObject = new SCMS.Entity.PM.TechnicalObject();

                    //Código de equipo.
                    var value_EQUNR = list["SERIAL"].ToString() != null ? list["SERIAL"].ToString() : string.Empty;
                    //Denominación equipo.
                    var value_EQUKTX = list["EQKTX"].ToString() != null ? list["EQKTX"].ToString() : string.Empty;
                    //Centro Emplazamiento del equipo.
                    //var value_SWERK = list["SWERK"].ToString() != null ? list["SWERK"].ToString() : string.Empty;
                    //Emplazamiento del equipo.
                    //var value_STORT = list["STORT"].ToString() != null ? list["STORT"].ToString() : string.Empty;
                    //Área de empresa.
                    //var value_BEBER = list["BEBER"].ToString() != null ? list["BEBER"].ToString() : string.Empty;

                    //Se debe mostrar en la pantalla el valor del campo, pero el código de lectura debe ser el valor inconvertible.
                    bool flag_TPLNR = list.Any(x => x.Key == "TPLNR");
                    var value_TPLNR = "";
                    if (flag_TPLNR)
                    {
                        value_TPLNR = list["TPLNR"].ToString() != null ? list["TPLNR"].ToString() : string.Empty;
                    }

                    //Descripción ubicación técnica.
                    bool flag_PLTXT = list.Any(x => x.Key == "PLTXT");
                    var value_PLTXT = "";
                    if (flag_PLTXT)
                    {
                        value_PLTXT = list["PLTXT"].ToString() != null ? list["PLTXT"].ToString() : string.Empty;
                    }

                    //Centro Emplazamiento de la ubicación.
                    var value_SWERK = list["SWERK"].ToString() != null ? list["SWERK"].ToString() : string.Empty;
                    //Emplazamiento del equipo.
                    var value_STORT = list["STORT"].ToString() != null ? list["STORT"].ToString() : string.Empty;
                    //Área de empresa.
                    var value_BEBER = list["BEBER"].ToString() != null ? list["BEBER"].ToString() : string.Empty;
                    //Denominación de la clase de actividad de mantenimiento	CHAR	30
                    //var value_ILATX = list["ILATX"].ToString() != null ? list["ILATX"].ToString() : string.Empty;

                    insert_TechnicalObject.ExternalIdentifier = value_EQUNR != "" ? value_EQUNR : value_TPLNR;
                    insert_TechnicalObject.Description = value_EQUKTX != "" ? value_EQUKTX : value_PLTXT;
                    insert_TechnicalObject.TOType = value_EQUNR != "" ? SCMS.Entity.PM.TechnicalObjectTypes.TechnicalObjectTypeEquipment : SCMS.Entity.PM.TechnicalObjectTypes.TechnicalObjectTypeTechnicalLocation;
                    insert_TechnicalObject.Name = value_EQUKTX != "" ? value_EQUKTX : value_PLTXT;
                    insert_TechnicalObject.Placement = value_STORT;
                    insert_TechnicalObject.PlannificationCenter = string.Empty;
                    insert_TechnicalObject.Area = value_BEBER;
                    insert_TechnicalObject.CostCenter = string.Empty;
                    listTechnicalObjects.Add(insert_TechnicalObject);
                }

                returnList = listTechnicalObjects;
                for (int i = 0; i < returnList.Count(); i++)
                {
                    SOFTTEK.SCMS.Entity.PM.TechnicalObject insertModelTechnicalObject = new SCMS.Entity.PM.TechnicalObject();
                    SOFTTEK.SCMS.Entity.PM.TechnicalObject existTechnicalObject = SelectTechnicalObject(new SCMS.Entity.PM.TechnicalObject { ExternalIdentifier = returnList[i].ExternalIdentifier });
                    if (existTechnicalObject == null)
                    {
                        if (returnList[i].ExternalIdentifier != "")
                        {
                            insertModelTechnicalObject = RegisterTechnicalObject(returnList[i]);
                        }
                        else
                        {
                            insertModelTechnicalObject = existTechnicalObject;
                        }
                    }
                    else if (returnList[i].ExternalIdentifier != "")
                    {
                        insertModelTechnicalObject = UpdateTechnicalObject(returnList[i]);
                    }
                }

                List<SCMS.Entity.PM.TechnicalObject> registeredTechnicalObjects = GetSapTechnicalObjects(sapUser);
                return registeredTechnicalObjects;
            }, string.Format("Retrieve PM SAP available Technical Object for {0}", sapUser));
        }

        /// <summary>
        /// Retrieve the Work Orders in SAP corresponding to the SAP user in a period.
        /// </summary>
        /// <param name="sapUser"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>SAP Work Orders Information</returns>
        private List<SCMS.Entity.PM.WorkOrder> GetSAPWorkOrdersInPeriod(string sapUser, DateTime from, DateTime to)
        {
            return context.Execute(() =>
            {
                List<SCMS.Entity.PM.WorkOrder> sapWorkOrders;
                sapWorkOrders = new List<SCMS.Entity.PM.WorkOrder>();

                SOFTTEK.SAP.Integration.SAPConnection sapConnector = new SAP.Integration.SAPConnection();

                var connectDestination = sapConnector.LoadDestination();
                var destinationRepository = connectDestination.Repository;

                //var testRFC = testDestinationRepository.CreateFunction("ZRFC_PM_TOMAR_MEDIDA");
                var initializerRFC = destinationRepository.CreateFunction(new System.Configuration.AppSettingsReader().GetValue("SAP_RFC_PM_WORK_ORDERS", typeof(string)).ToString());
                var rfcMetadata = initializerRFC.Metadata;

                bool stateConnection = Ping(connectDestination);

                Dictionary<string, object> input = new Dictionary<string, object>();
                Dictionary<string, object> output = new Dictionary<string, object>();
                Dictionary<object, object> tables = new Dictionary<object, object>();

                if (stateConnection)
                    initializerRFC.Invoke(connectDestination);

                if (initializerRFC.GetString("EV_INDICADOR") == "0")
                {
                    for (int i = 0; i < rfcMetadata.ParameterCount; i++)
                    {
                        if (rfcMetadata[i].Direction.Equals(global::SAP.Middleware.Connector.RfcDirection.IMPORT))
                        {
                            Type inputType = SAP.Integration.SAPData.GetDataType(rfcMetadata[i].DataType);
                            input[rfcMetadata[i].Name] = initializerRFC.GetValue(rfcMetadata[i].Name);//inputType.IsValueType ? Activator.CreateInstance(inputType) : null;
                        }
                        else if (rfcMetadata[i].Direction.Equals(global::SAP.Middleware.Connector.RfcDirection.EXPORT))
                        {
                            Type outputType = SAP.Integration.SAPData.GetDataType(rfcMetadata[i].DataType);
                            output[rfcMetadata[i].Name] = initializerRFC.GetValue(rfcMetadata[i].Name); //outputType.IsValueType ? Activator.CreateInstance(outputType) : null;
                        }
                        else if (rfcMetadata[i].Direction.Equals(global::SAP.Middleware.Connector.RfcDirection.TABLES))
                        {
                            Type tablesType = SAP.Integration.SAPData.GetDataType(rfcMetadata[i].DataType);
                            tables[rfcMetadata[i].Name] = initializerRFC.GetValue(rfcMetadata[i].Name); //outputType.IsValueType ? Activator.CreateInstance(outputType) : null;
                        }
                    }
                }
                else
                {
                    throw new Exception(String.Format("No ha sido posible cosultar las ordenes de trabajo. Causa: {0}", initializerRFC.GetString("EV_TEXT")));
                }

                List<string> cnt = new List<string>();
                List<IRfcStructure> irc = new List<IRfcStructure>();
                IRfcTable detail = initializerRFC["ZPMT_ORDEN_MANT"].GetTable();
                List<SOFTTEK.SCMS.Entity.PM.WorkOrder> listWorkOrders = new List<SCMS.Entity.PM.WorkOrder>();
                List<SOFTTEK.SCMS.Entity.PM.TechnicalObject> listTechnicalObjects = new List<SCMS.Entity.PM.TechnicalObject>();
                List<SOFTTEK.SCMS.Entity.PM.MaintenancePlan> listMaintenancePlan = new List<SCMS.Entity.PM.MaintenancePlan>();
                List<SOFTTEK.SCMS.Entity.PM.Task> listTask = new List<SCMS.Entity.PM.Task>();
                List<SOFTTEK.SCMS.Entity.PM.Material> listMaterial = new List<SCMS.Entity.PM.Material>();

                List<Dictionary<string, object>> dictonaryMetadataTable = SOFTTEK.SAP.Integration.SAPData.GetListDictionaryForMetadataExport(initializerRFC, "ZPMT_ORDEN_MANT");

                foreach (Dictionary<string, object> list in dictonaryMetadataTable)
                {
                    SOFTTEK.SCMS.Entity.PM.WorkOrder insert_workOrder = new SCMS.Entity.PM.WorkOrder();
                    SOFTTEK.SCMS.Entity.PM.TechnicalObject insert_TechnicalObject = new SCMS.Entity.PM.TechnicalObject();
                    SOFTTEK.SCMS.Entity.PM.Task insert_Task = new SCMS.Entity.PM.Task();
                    SOFTTEK.SCMS.Entity.PM.MaintenancePlan insert_MaintenancePlan = new SCMS.Entity.PM.MaintenancePlan();
                    SOFTTEK.SCMS.Entity.PM.Material insert_Material = new SCMS.Entity.PM.Material();

                    //Número de orden	CHAR	12
                    var value_AUFNR = list["AUFNR"].ToString() != null ? list["AUFNR"].ToString() : string.Empty;
                    //Clase de orden	CHAR	4
                    var value_AUART = list["AUART"].ToString() != null ? list["AUART"].ToString() : string.Empty;
                    //Prioridad	CHAR	1
                    var value_PRIOK = list["PRIOK"].ToString() != null ? list["PRIOK"].ToString() : string.Empty;
                    //Fecha liberación	DATS	8
                    var value_IDAT1 = list["IDAT1"].ToString() != null ? list["IDAT1"].ToString() : string.Empty;
                    //Fecha de inicio extrema	DATS	8
                    var value_GSTRP = list["GSTRP"].ToString() != null ? list["GSTRP"].ToString() : string.Empty;
                    //Fecha fin extrema	DATS	8
                    var value_GLTRP = list["GLTRP"].ToString() != null ? list["GLTRP"].ToString() : string.Empty;
                    //Número de equipo	CHAR	18
                    var value_EQUNR = list["EQUNR"].ToString() != null ? list["EQUNR"].ToString() : string.Empty;
                    //Denominación de equipo	CHAR	40
                    var value_EQKTX = list["EQKTX"].ToString() != null ? list["EQKTX"].ToString() : string.Empty;
                    //emplazaminto	CHAR	40
                    var value_KTEXT = list["KTEXT"].ToString() != null ? list["KTEXT"].ToString() : string.Empty;
                    //Número de ut
                    var value_TPLNR = list["TPLNR"].ToString() != null ? list["TPLNR"].ToString() : string.Empty;
                    //Denominación de la ubicación técnica
                    var value_PLTXT = list["PLTXT"].ToString() != null ? list["PLTXT"].ToString() : string.Empty;
                    //Centro de planificación del mantenimiento
                    var value_IWERK = list["IWERK"].ToString() != null ? list["IWERK"].ToString() : string.Empty;
                    //Nombre del grupo de planificación de mantenimiento	CHAR	18
                    var value_INNAM = list["INNAM"].ToString() != null ? list["INNAM"].ToString() : string.Empty;
                    //Puesto de trabajo responsable	NUMC	8
                    var value_GEWRK = list["GEWRK"].ToString() != null ? list["GEWRK"].ToString() : string.Empty;
                    //Clase de actividad de mantenimiento	CHAR	3
                    var value_ILART = list["ILART"].ToString() != null ? list["ILART"].ToString() : string.Empty;
                    //Denominación de la clase de actividad de mantenimiento	CHAR	30
                    var value_ILATX = list["ILATX"].ToString() != null ? list["ILATX"].ToString() : string.Empty;
                    //Nº hoja ruta de operaciones en orden	CHAR	8
                    var value_AUFPL = list["AUFPL"].ToString() != null ? list["AUFPL"].ToString() : string.Empty;
                    //Id Operacion
                    var value_APLZL = list["APLZL"].ToString() != null ? list["APLZL"].ToString() : string.Empty;
                    //Puesto de trabajo	CHAR	8
                    var value_ARBPL = list["ARBPL"].ToString() != null ? list["ARBPL"].ToString() : string.Empty;
                    //Descripción operación: 2. línea de texto	CHAR	40
                    var value_LTXA1 = list["LTXA1"].ToString() != null ? list["LTXA1"].ToString() : string.Empty;
                    //Cantidad de capacidad necesaria	INT1	3
                    var value_ANZZL = list["ANZZL"].ToString() != null ? list["ANZZL"].ToString() : string.Empty;
                    //Duración de operación normal	QUAN	5
                    var value_DAUNO = list["DAUNO"].ToString() != null ? list["DAUNO"].ToString() : string.Empty;
                    //Número de material	CHAR	18
                    var value_MATNR = list["MATNR"].ToString() != null ? list["MATNR"].ToString() : string.Empty;
                    //Texto breve de material	CHAR	40
                    var value_MAKTX = list["MAKTX"].ToString() != null ? list["MAKTX"].ToString() : string.Empty;
                    //Cantidad necesaria	QUAN	13
                    var value_BDMNG = list["BDMNG"].ToString() != null ? list["BDMNG"].ToString() : string.Empty;
                    //Reserva	NUMC	10
                    var value_RSNUM = list["RSNUM"].ToString() != null ? list["RSNUM"].ToString() : string.Empty;
                    //
                    //var value_RSPOS = list["RSPOS"].ToString() != null ? list["RSPOS"].ToString() : string.Empty;
                    //
                    var value_VORNR = list["VORNR"].ToString() != null ? list["VORNR"].ToString() : string.Empty;

                    insert_TechnicalObject.ExternalIdentifier = value_EQUNR != "" ? value_EQUNR : value_TPLNR;
                    insert_TechnicalObject.Description = value_EQKTX != "" ? value_EQKTX : value_PLTXT;
                    insert_TechnicalObject.TOType = value_EQUNR != "" ? SCMS.Entity.PM.TechnicalObjectTypes.TechnicalObjectTypeTechnicalLocation : SCMS.Entity.PM.TechnicalObjectTypes.TechnicalObjectTypeTechnicalLocation;
                    insert_TechnicalObject.Name = value_EQKTX != "" ? value_EQKTX : value_PLTXT;
                    insert_TechnicalObject.Placement = value_KTEXT;
                    insert_TechnicalObject.PlannificationCenter = value_IWERK;
                    insert_TechnicalObject.Area = string.Empty;
                    insert_TechnicalObject.CostCenter = string.Empty;
                    listTechnicalObjects.Add(insert_TechnicalObject);

                    SOFTTEK.SCMS.Entity.PM.TechnicalObject insertModelTechnicalObject = new SCMS.Entity.PM.TechnicalObject();
                    SOFTTEK.SCMS.Entity.PM.TechnicalObject existTechnicalObject = SelectTechnicalObject(new SCMS.Entity.PM.TechnicalObject { ExternalIdentifier = insert_TechnicalObject.ExternalIdentifier });
                    //SOFTTEK.SCMS.Entity.PM.TechnicalObject existTechnicalObject = SelectTechnicalObject(insert_TechnicalObject);
                    if (existTechnicalObject == null)
                    {
                        if (insert_TechnicalObject.ExternalIdentifier != "")
                        {
                            insertModelTechnicalObject = RegisterTechnicalObject(insert_TechnicalObject);
                        }
                        else
                        {
                            insertModelTechnicalObject = existTechnicalObject;
                        }
                    }
                    else if (insert_TechnicalObject.ExternalIdentifier != "")
                    {
                        insertModelTechnicalObject = UpdateTechnicalObject(existTechnicalObject);
                    }

                    insert_workOrder.ExternalIdentifier = value_AUFNR;
                    insert_workOrder.Type = value_AUART;
                    insert_workOrder.Company = string.Empty;
                    insert_workOrder.Priority = value_PRIOK;
                    insert_workOrder.Performer = string.Empty;
                    insert_workOrder.State = string.Empty;
                    insert_workOrder.TechnicalObject = new SCMS.Entity.PM.TechnicalObject { Identifier = insertModelTechnicalObject.Identifier };
                    insert_workOrder.ScheduledTo = DateTime.Now;
                    insert_workOrder.ExecutionStartAt = Convert.ToDateTime(value_GSTRP);
                    insert_workOrder.ExecutionFinishedAt = Convert.ToDateTime(value_GLTRP);
                    insert_workOrder.Activity = value_ILATX;
                    if (value_IDAT1 != "0000-00-00")
                    {
                        insert_workOrder.ReleaseDate = Convert.ToDateTime(value_IDAT1);
                    }
                    else
                    {
                        insert_workOrder.ReleaseDate = DateTime.Now;
                    }

                    insert_workOrder.PlanningGroup = value_INNAM;
                    insert_workOrder.Workstation = value_GEWRK;
                    listWorkOrders.Add(insert_workOrder);

                    SOFTTEK.SCMS.Entity.PM.WorkOrder insertModelWorkOrder = new SCMS.Entity.PM.WorkOrder();
                    SOFTTEK.SCMS.Entity.PM.WorkOrder existWorkOrder = SelectWorkOrder(new SCMS.Entity.PM.WorkOrder { ExternalIdentifier = insert_workOrder.ExternalIdentifier });
                    //SOFTTEK.SCMS.Entity.PM.WorkOrder existWorkOrder = SelectWorkOrder(insert_workOrder);
                    if (existWorkOrder == null)
                    {
                        if (insert_workOrder.ExternalIdentifier != "")
                        {
                            insertModelWorkOrder = RegisterWorkOrder(insert_workOrder);
                        }
                        else
                        {
                            insertModelWorkOrder = existWorkOrder;
                        }
                    }
                    else if (insert_workOrder.ExternalIdentifier != "")
                    {
                        insertModelWorkOrder = UpdateWorkOrder(existWorkOrder);
                    }

                    insert_MaintenancePlan.Description = value_INNAM;
                    insert_MaintenancePlan.ExternalIdentifier = value_IWERK;
                    insert_MaintenancePlan.WorkOrder = insertModelWorkOrder.Identifier;
                    insert_MaintenancePlan.Comments = value_ILATX;
                    insert_MaintenancePlan.DeviceType = string.Empty;
                    //insert_MaintenancePlan.Activities = string.Empty;
                    listMaintenancePlan.Add(insert_MaintenancePlan);

                    SOFTTEK.SCMS.Entity.PM.MaintenancePlan insertModelMaintenancePlan = new SCMS.Entity.PM.MaintenancePlan();
                    //SOFTTEK.SCMS.Entity.PM.MaintenancePlan existMaintenancePlan = SelectMaintenancePlan(new SCMS.Entity.PM.MaintenancePlan { ExternalIdentifier = insert_MaintenancePlan.ExternalIdentifier });
                    SOFTTEK.SCMS.Entity.PM.MaintenancePlan existMaintenancePlan = SelectMaintenancePlan(insert_MaintenancePlan);
                    if (existMaintenancePlan == null)
                    {
                        if (insert_MaintenancePlan.ExternalIdentifier != "")
                        {
                            insertModelMaintenancePlan = RegisterMaintenancePlan(insert_MaintenancePlan);
                        }
                        else
                        {
                            insertModelMaintenancePlan = existMaintenancePlan;
                        }
                    }
                    else if (insert_MaintenancePlan.ExternalIdentifier != "")
                    {
                        //insertModelMaintenancePlan = UpdateMaintenancePlan(existMaintenancePlan);
                        insertModelMaintenancePlan = existMaintenancePlan;
                    }

                    insert_Task.ExternalIdentifier = string.Format("{0}-{1}", value_AUFPL, value_APLZL);
                    insert_Task.Name = value_LTXA1;
                    insert_Task.Description = value_LTXA1;
                    insert_Task.Performer = string.Empty;
                    insert_Task.StartedAt = Convert.ToDateTime(value_GSTRP);
                    insert_Task.FinishedAt = Convert.ToDateTime(value_GLTRP);
                    insert_Task.Comments = string.Empty;
                    insert_Task.DurationOperation = value_DAUNO;
                    insert_Task.QuantityCapacity = value_ANZZL;
                    insert_Task.Plan = new SCMS.Entity.PM.MaintenancePlan { Identifier = insertModelMaintenancePlan.Identifier };
                    insert_Task.WorkOrder = new SCMS.Entity.PM.WorkOrder { Identifier = insertModelWorkOrder.Identifier };
                    insert_Task.Status = string.Empty;
                    listTask.Add(insert_Task);

                    SOFTTEK.SCMS.Entity.PM.Task insertModelTask = new SCMS.Entity.PM.Task();
                    SOFTTEK.SCMS.Entity.PM.Task existTask = SelectTask(new SCMS.Entity.PM.Task { ExternalIdentifier = insert_Task.ExternalIdentifier });
                    //SOFTTEK.SCMS.Entity.PM.Task existTask = SelectTask(insert_Task);
                    if (existTask == null)
                    {
                        if (insert_Task.ExternalIdentifier != "")
                        {
                            insertModelTask = RegisterTask(insert_Task);
                        }
                        else
                        {
                            insertModelTask = existTask;
                        }
                    }
                    else if (insert_Task.ExternalIdentifier != "")
                    {
                        insertModelTask = UpdateTask(existTask);
                    }

                    insert_Material.ExternalIdentifier = value_MATNR;
                    insert_Material.Name = value_MAKTX;
                    insert_Material.Description = value_MAKTX;
                    insert_Material.Class = string.Empty;
                    insert_Material.MaterialParameter = 0;
                    insert_Material.Stock = value_BDMNG != null || value_BDMNG != "" ? Convert.ToDouble(value_BDMNG) : 0;
                    insert_Material.Task = new SCMS.Entity.PM.Task { Identifier = insertModelTask.Identifier };
                    listMaterial.Add(insert_Material);

                    SOFTTEK.SCMS.Entity.PM.Material insertModelMaterial = new SCMS.Entity.PM.Material();
                    SOFTTEK.SCMS.Entity.PM.Material existMaterial = SelectMaterial(new SCMS.Entity.PM.Material { ExternalIdentifier = insert_Material.ExternalIdentifier, Task = insert_Material.Task });
                    //SOFTTEK.SCMS.Entity.PM.Material existMaterial = SelectMaterial(insert_Material);
                    if (existMaterial == null)
                    {
                        if (insert_Material.ExternalIdentifier != "")
                        {
                            insertModelMaterial = RegisterMaterial(insert_Material);
                        }
                        else
                        {
                            insertModelMaterial = existMaterial;
                        }
                    }
                    else if (insert_Material.ExternalIdentifier != "")
                    {
                        insertModelMaterial = UpdateMaterial(existMaterial);
                    }
                }

                #region Old Implementation

                //foreach (IRfcStructure column in detail)
                //{
                //    SOFTTEK.SCMS.Entity.PM.WorkOrder insert_workOrder = new SCMS.Entity.PM.WorkOrder();
                //    SOFTTEK.SCMS.Entity.PM.TechnicalObject insert_TechnicalObject = new SCMS.Entity.PM.TechnicalObject();
                //    SOFTTEK.SCMS.Entity.PM.Task insert_Task = new SCMS.Entity.PM.Task();
                //    SOFTTEK.SCMS.Entity.PM.MaintenancePlan insert_MaintenancePlan = new SCMS.Entity.PM.MaintenancePlan();
                //    SOFTTEK.SCMS.Entity.PM.Material insert_Material = new SCMS.Entity.PM.Material();

                //    //Número de orden	CHAR	12
                //    var value_AUFNR = column.GetString("AUFNR") != null ? column.GetString("AUFNR") : string.Empty;
                //    //Clase de orden	CHAR	4
                //    var value_AUART = column.GetString("AUART") != null ? column.GetString("AUART") : string.Empty;
                //    //Prioridad	CHAR	1
                //    var value_PRIOK = column.GetString("PRIOK") != null ? column.GetString("PRIOK") : string.Empty;
                //    //Fecha liberación	DATS	8
                //    var value_IDAT1 = column.GetString("IDAT1") != null ? column.GetString("IDAT1") : string.Empty;
                //    //Fecha de inicio extrema	DATS	8
                //    var value_GSTRP = column.GetString("GSTRP") != null ? column.GetString("GSTRP") : string.Empty;
                //    //Fecha fin extrema	DATS	8
                //    var value_GLTRP = column.GetString("GLTRP") != null ? column.GetString("GLTRP") : string.Empty;
                //    //Número de equipo	CHAR	18
                //    var value_EQUNR = column.GetString("EQUNR") != null ? column.GetString("EQUNR") : string.Empty;
                //    //Denominación de equipo	CHAR	40
                //    var value_EQKTX = column.GetString("EQKTX") != null ? column.GetString("EQKTX") : string.Empty;
                //    //emplazaminto	CHAR	40
                //    var value_KTEXT = column.GetString("KTEXT") != null ? column.GetString("KTEXT") : string.Empty;
                //    //Número de ut
                //    var value_TPLNR = column.GetString("TPLNR") != null ? column.GetString("TPLNR") : string.Empty;
                //    //Denominación de la ubicación técnica
                //    var value_PLTXT = column.GetString("PLTXT") != null ? column.GetString("PLTXT") : string.Empty;
                //    //Centro de planificación del mantenimiento
                //    var value_IWERK = column.GetString("IWERK") != null ? column.GetString("IWERK") : string.Empty;
                //    //Nombre del grupo de planificación de mantenimiento	CHAR	18
                //    var value_INNAM = column.GetString("INNAM") != null ? column.GetString("INNAM") : string.Empty;
                //    //Puesto de trabajo responsable	NUMC	8
                //    var value_GEWRK = column.GetString("GEWRK") != null ? column.GetString("GEWRK") : string.Empty;
                //    //Clase de actividad de mantenimiento	CHAR	3
                //    var value_ILART = column.GetString("ILART") != null ? column.GetString("ILART") : string.Empty;
                //    //Denominación de la clase de actividad de mantenimiento	CHAR	30
                //    var value_ILATX = column.GetString("ILATX") != null ? column.GetString("ILATX") : string.Empty;
                //    //Puesto de trabajo	CHAR	8
                //    var value_AUFPL = column.GetString("AUFPL") != null ? column.GetString("AUFPL") : string.Empty;
                //    //Id Operacion
                //    var value_APLZL = column.GetString("APLZL") != null ? column.GetString("APLZL") : string.Empty;
                //    //Puesto de trabajo	CHAR	8
                //    var value_ARBPL = column.GetString("ARBPL") != null ? column.GetString("ARBPL") : string.Empty;
                //    //Descripción operación: 2. línea de texto	CHAR	40
                //    var value_LTXA1 = column.GetString("LTXA1") != null ? column.GetString("LTXA1") : string.Empty;
                //    //Cantidad de capacidad necesaria	INT1	3
                //    var value_ANZZL = column.GetString("ANZZL") != null ? column.GetString("ANZZL") : string.Empty;
                //    //Duración de operación normal	QUAN	5
                //    var value_DAUNO = column.GetString("DAUNO") != null ? column.GetString("DAUNO") : string.Empty;
                //    //Número de material	CHAR	18
                //    var value_MATNR = column.GetString("MATNR") != null ? column.GetString("MATNR") : string.Empty;
                //    //Texto breve de material	CHAR	40
                //    var value_MAKTX = column.GetString("MAKTX") != null ? column.GetString("MAKTX") : string.Empty;
                //    //Cantidad necesaria	QUAN	13
                //    var value_BDMNG = column.GetString("BDMNG") != null ? column.GetString("BDMNG") : string.Empty;
                //    //Reserva	NUMC	10
                //    var value_RSNUM = column.GetString("RSNUM") != null ? column.GetString("RSNUM") : string.Empty;
                //    //
                //    //var value_RSPOS = column.GetString("RSPOS") != null ? column.GetString("RSPOS") : string.Empty;
                //    //
                //    var value_VORNR = column.GetString("VORNR") != null ? column.GetString("VORNR") : string.Empty;

                //    insert_TechnicalObject.ExternalIdentifier = value_EQUNR != "" ? value_EQUNR : value_TPLNR;
                //    insert_TechnicalObject.Description = value_EQKTX != "" ? value_EQKTX : value_PLTXT;
                //    insert_TechnicalObject.Activity = value_EQUNR != "" ? "Obj_Tec" : "Ub_Tec";
                //    insert_TechnicalObject.Name = value_EQKTX != "" ? value_EQKTX : value_PLTXT;
                //    insert_TechnicalObject.Placement = string.Empty;
                //    insert_TechnicalObject.PlannificationCenter = string.Empty;
                //    insert_TechnicalObject.Area = string.Empty;
                //    insert_TechnicalObject.CostCenter = string.Empty;
                //    listTechnicalObjects.Add(insert_TechnicalObject);

                //    SOFTTEK.SCMS.Entity.PM.TechnicalObject insertModelTechnicalObject = new SCMS.Entity.PM.TechnicalObject();
                //    SOFTTEK.SCMS.Entity.PM.TechnicalObject existTechnicalObject = SelectTechnicalObject(insert_TechnicalObject);
                //    if (existTechnicalObject == null)
                //    {
                //        if (insert_TechnicalObject.ExternalIdentifier != "")
                //        {
                //            insertModelTechnicalObject = RegisterTechnicalObject(insert_TechnicalObject);
                //        }
                //        else {
                //            insertModelTechnicalObject = existTechnicalObject;
                //        }
                //    }
                //    else if (insert_TechnicalObject.ExternalIdentifier != "")
                //    {
                //        insertModelTechnicalObject = existTechnicalObject;
                //    }

                //    insert_workOrder.ExternalIdentifier = value_AUFNR;
                //    insert_workOrder.Type = value_AUART;
                //    insert_workOrder.Company = string.Empty;
                //    insert_workOrder.Priority = value_PRIOK;
                //    insert_workOrder.Performer = string.Empty;
                //    insert_workOrder.State = string.Empty;
                //    insert_workOrder.TechnicalObject = new SCMS.Entity.PM.TechnicalObject { Identifier = insertModelTechnicalObject.Identifier };
                //    insert_workOrder.ScheduledTo = DateTime.Now;
                //    insert_workOrder.ExecutionStartAt = Convert.ToDateTime(value_GSTRP);
                //    insert_workOrder.ExecutionFinishedAt = Convert.ToDateTime(value_GLTRP);
                //    listWorkOrders.Add(insert_workOrder);

                //    SOFTTEK.SCMS.Entity.PM.WorkOrder insertModelWorkOrder = new SCMS.Entity.PM.WorkOrder();
                //    SOFTTEK.SCMS.Entity.PM.WorkOrder existWorkOrder = SelectWorkOrder(insert_workOrder);
                //    if (existWorkOrder == null)
                //    {
                //        if (insert_workOrder.ExternalIdentifier != "")
                //        {
                //            insertModelWorkOrder = RegisterWorkOrder(insert_workOrder);
                //        }
                //        else
                //        {
                //            insertModelWorkOrder = existWorkOrder;
                //        }
                //    }
                //    else if (insert_workOrder.ExternalIdentifier != "")
                //    {
                //        insertModelWorkOrder = existWorkOrder;
                //    }

                //    insert_MaintenancePlan.Description = value_INNAM;
                //    insert_MaintenancePlan.ExternalIdentifier = value_IWERK;
                //    insert_MaintenancePlan.WorkOrder = insertModelWorkOrder.Identifier;
                //    insert_MaintenancePlan.Comments = value_ILATX;
                //    insert_MaintenancePlan.DeviceType = string.Empty;
                //    //insert_MaintenancePlan.Activities = string.Empty;
                //    listMaintenancePlan.Add(insert_MaintenancePlan);

                //    SOFTTEK.SCMS.Entity.PM.MaintenancePlan insertModelMaintenancePlan = new SCMS.Entity.PM.MaintenancePlan();
                //    SOFTTEK.SCMS.Entity.PM.MaintenancePlan existMaintenancePlan = SelectMaintenancePlan(insert_MaintenancePlan);
                //    if (existMaintenancePlan == null)
                //    {
                //        if (insert_MaintenancePlan.ExternalIdentifier != "")
                //        {
                //            insertModelMaintenancePlan = RegisterMaintenancePlan(insert_MaintenancePlan);
                //        }
                //        else
                //        {
                //            insertModelMaintenancePlan = existMaintenancePlan;
                //        }
                //    }
                //    else if (insert_MaintenancePlan.ExternalIdentifier != "")
                //    {
                //        insertModelMaintenancePlan = existMaintenancePlan;
                //    }

                //    insert_Task.ExternalIdentifier = value_ILART;
                //    insert_Task.Name = value_LTXA1;
                //    insert_Task.Description = value_LTXA1;
                //    insert_Task.Performer = string.Empty;
                //    insert_Task.StartedAt = Convert.ToDateTime(value_GSTRP);
                //    insert_Task.FinishedAt = Convert.ToDateTime(value_GLTRP);
                //    insert_Task.Comments = string.Empty;
                //    insert_Task.Plan = new SCMS.Entity.PM.MaintenancePlan { Identifier = insertModelMaintenancePlan.Identifier };
                //    insert_Task.WorkOrder = new SCMS.Entity.PM.WorkOrder { Identifier = insertModelWorkOrder.Identifier };
                //    insert_Task.Status = string.Empty;
                //    listTask.Add(insert_Task);

                //    SOFTTEK.SCMS.Entity.PM.Task insertModelTask = new SCMS.Entity.PM.Task();
                //    SOFTTEK.SCMS.Entity.PM.Task existTask = SelectTask(insert_Task);
                //    if (existTask == null)
                //    {
                //        if (insert_Task.ExternalIdentifier != "")
                //        {
                //            insertModelTask = RegisterTask(insert_Task);
                //        }
                //        else
                //        {
                //            insertModelTask = existTask;
                //        }
                //    }
                //    else if (insert_Task.ExternalIdentifier != "")
                //    {
                //        insertModelTask = existTask;
                //    }

                //    insert_Material.ExternalIdentifier = value_MATNR;
                //    insert_Material.Name = value_MAKTX;
                //    insert_Material.Description = value_MAKTX;
                //    insert_Material.Class = string.Empty;
                //    insert_Material.Stock = value_BDMNG != null || value_BDMNG != "" ? Convert.ToDouble(value_BDMNG) : 0;
                //    listMaterial.Add(insert_Material);

                //    SOFTTEK.SCMS.Entity.PM.Material insertModelMaterial = new SCMS.Entity.PM.Material();
                //    SOFTTEK.SCMS.Entity.PM.Material existMaterial = SelectMaterial(insert_Material);
                //    if (existMaterial == null)
                //    {
                //        if (insert_Material.ExternalIdentifier != "")
                //        {
                //            insertModelMaterial = RegisterMaterial(insert_Material);
                //        }
                //        else
                //        {
                //            insertModelMaterial = existMaterial;
                //        }
                //    }
                //    else if (insert_Material.ExternalIdentifier != "")
                //    {
                //        insertModelMaterial = existMaterial;
                //    }

                //}

                #endregion Old Implementation

                //GetSapTechnicalObjects(sapUser);
                //InsertSapAdvice(sapUser, new SOFTTEK.SCMS.Entity.PM.Advice());

                //List<SCMS.Entity.PM.WorkOrder> registeredWorkOrders = sapWorkOrders.Select(wo => RegisterWorkOrder(wo)).ToList();
                List<SCMS.Entity.PM.WorkOrder> registeredWorkOrders = GetListWorkOrder(new SCMS.Entity.PM.WorkOrder { });
                return registeredWorkOrders;
            }, string.Format("Retrieve PM SAP available Work Orders for {0} between {1:dd/MM/yyyy} and {2:dd/MM/yyyy}.", sapUser, from, to));
        }

        public bool Ping(RfcDestination connectDestination)
        {
            try
            {
                connectDestination.Ping();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("No ha sido posible realizar la conexión con SAP. Causa: {0}", ex));
            }
        }

        #region Methods CRUDS Register WorkOrders

        #region Methods Gets

        /// <summary>
        /// get a Retrieved from SAP WorkOrder
        /// </summary>
        /// <param name="sapWorkOrder"></param>
        /// <returns>Select Work Order</returns>
        private List<SCMS.Entity.PM.WorkOrder> GetListWorkOrder(SCMS.Entity.PM.WorkOrder sapWorkOrder)
        {
            return context.Execute(() =>
            {
                List<SCMS.Entity.PM.WorkOrder> registeredWorkOrder = null;

                using (dataSource = new Data.PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    registeredWorkOrder = dataSource.GetWorkOrders(sapWorkOrder);
                }

                return registeredWorkOrder;
            }, string.Format("Get PM SAP Work Orders {0}.", sapWorkOrder.ExternalIdentifier));
        }

        /// <summary>
        /// get a Retrieved from SAP WorkOrder
        /// </summary>
        /// <param name="sapWorkOrder"></param>
        /// <returns>Select Work Order</returns>
        private SCMS.Entity.PM.WorkOrder SelectWorkOrder(SCMS.Entity.PM.WorkOrder sapWorkOrder)
        {
            return context.Execute(() =>
            {
                SCMS.Entity.PM.WorkOrder registeredWorkOrder = null;

                using (dataSource = new Data.PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    registeredWorkOrder = dataSource.GetWorkOrders(sapWorkOrder).FirstOrDefault();
                }

                return registeredWorkOrder;
            }, string.Format("Get PM SAP Work Orders {0}.", sapWorkOrder.ExternalIdentifier));
        }

        /// <summary>
        /// get a Retrieved from SAP TechnicalObject
        /// </summary>
        /// <param name="sapWorkOrder"></param>
        /// <returns>Select TechnicalObject</returns>
        private SCMS.Entity.PM.TechnicalObject SelectTechnicalObject(SCMS.Entity.PM.TechnicalObject sapTechnicalObject)
        {
            return context.Execute(() =>
            {
                SCMS.Entity.PM.TechnicalObject registeredTechnicalObject = null;

                using (dataSource = new Data.PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    registeredTechnicalObject = dataSource.GetTechnicalObjects(sapTechnicalObject).FirstOrDefault();
                }

                return registeredTechnicalObject;
            }, string.Format("Get PM SAP TechnicalObject {0}.", sapTechnicalObject.ExternalIdentifier));
        }

        /// <summary>
        /// get a Retrieved from SAP MaintenancePlan
        /// </summary>
        /// <param name="sapMaintenancePlan"></param>
        /// <returns>Select MaintenancePlan</returns>
        private SCMS.Entity.PM.MaintenancePlan SelectMaintenancePlan(SCMS.Entity.PM.MaintenancePlan sapMaintenancePlan)
        {
            return context.Execute(() =>
            {
                SCMS.Entity.PM.MaintenancePlan registeredMaintenancePlan = null;

                using (dataSource = new Data.PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    registeredMaintenancePlan = dataSource.GetMaintenancePlans(sapMaintenancePlan).FirstOrDefault();
                }

                return registeredMaintenancePlan;
            }, string.Format("Get PM SAP MaintenancePlan {0}.", sapMaintenancePlan.ExternalIdentifier));
        }

        /// <summary>
        /// get a Retrieved from SAP Task
        /// </summary>
        /// <param name="sapTask"></param>
        /// <returns>Select Task</returns>
        private SCMS.Entity.PM.Task SelectTask(SCMS.Entity.PM.Task sapTask)
        {
            return context.Execute(() =>
            {
                SCMS.Entity.PM.Task registeredTask = null;

                using (dataSource = new Data.PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    registeredTask = dataSource.GetTasks(sapTask).FirstOrDefault();
                }

                return registeredTask;
            }, string.Format("Get PM SAP Task {0}.", sapTask.ExternalIdentifier));
        }

        /// <summary>
        /// get a Retrieved from SAP Material
        /// </summary>
        /// <param name="sapMaterial"></param>
        /// <returns>Select Material</returns>
        private SCMS.Entity.PM.Material SelectMaterial(SCMS.Entity.PM.Material sapMaterial)
        {
            return context.Execute(() =>
            {
                SCMS.Entity.PM.Material registeredMaterial = null;

                using (dataSource = new Data.PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    registeredMaterial = dataSource.GetMaterials(sapMaterial).FirstOrDefault();
                }

                return registeredMaterial;
            }, string.Format("Get PM SAP Material {0}.", sapMaterial.ExternalIdentifier));
        }

        #endregion Methods Gets

        #region Methods Post

        /// <summary>
        /// Submits a Retrieved from SAP WorkOrder
        /// </summary>
        /// <param name="sapWorkOrder"></param>
        /// <returns>Registered Work Order</returns>
        private SCMS.Entity.PM.WorkOrder RegisterWorkOrder(SCMS.Entity.PM.WorkOrder sapWorkOrder)
        {
            return context.Execute(() =>
            {
                SCMS.Entity.PM.WorkOrder registeredWorkOrder;

                using (dataSource = new Data.PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    registeredWorkOrder = dataSource.InsertWorkOrder(sapWorkOrder);
                }

                return registeredWorkOrder;
            }, string.Format("Register PM SAP Work Orders {0}.", sapWorkOrder.ExternalIdentifier));
        }

        /// <summary>
        /// Submits a Retrieved from SAP TechnicalObject
        /// </summary>
        /// <param name="sapTechnicalObject"></param>
        /// <returns>Registered TechnicalObject</returns>
        private SCMS.Entity.PM.TechnicalObject RegisterTechnicalObject(SCMS.Entity.PM.TechnicalObject sapTechnicalObject)
        {
            return context.Execute(() =>
            {
                SCMS.Entity.PM.TechnicalObject registeredTechnicalObject;

                using (dataSource = new Data.PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    registeredTechnicalObject = dataSource.InsertTechnicalObject(sapTechnicalObject);
                }

                return registeredTechnicalObject;
            }, string.Format("Register PM SAP Work Orders {0}.", sapTechnicalObject.ExternalIdentifier));
        }

        /// <summary>
        /// Submits a Retrieved from SAP MaintenancePlan
        /// </summary>
        /// <param name="sapMaintenancePlan"></param>
        /// <returns>Registered MaintenancePlan</returns>
        private SCMS.Entity.PM.MaintenancePlan RegisterMaintenancePlan(SCMS.Entity.PM.MaintenancePlan sapMaintenancePlan)
        {
            return context.Execute(() =>
            {
                SCMS.Entity.PM.MaintenancePlan registeredMaintenancePlan;

                using (dataSource = new Data.PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    registeredMaintenancePlan = dataSource.InsertMaintenancePlan(sapMaintenancePlan);
                }

                return registeredMaintenancePlan;
            }, string.Format("Register PM SAP MaintenancePlan {0}.", sapMaintenancePlan.ExternalIdentifier));
        }

        /// <summary>
        /// Submits a Retrieved from SAP Task
        /// </summary>
        /// <param name="sapTask"></param>
        /// <returns>Registered Task</returns>
        private SCMS.Entity.PM.Task RegisterTask(SCMS.Entity.PM.Task sapTask)
        {
            return context.Execute(() =>
            {
                SCMS.Entity.PM.Task registeredTask;

                using (dataSource = new Data.PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    registeredTask = dataSource.InsertTask(sapTask);
                }

                return registeredTask;
            }, string.Format("Register PM SAP Task {0}.", sapTask.ExternalIdentifier));
        }

        /// <summary>
        /// Submits a Retrieved from SAP Task
        /// </summary>
        /// <param name="sapAdvice"></param>
        /// <returns>Registered Task</returns>
        private SCMS.Entity.PM.Advice RegisterAdvice(SCMS.Entity.PM.Advice sapAdvice)
        {
            return context.Execute(() =>
            {
                SCMS.Entity.PM.Advice registeredAdvice;

                using (dataSource = new Data.PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    registeredAdvice = dataSource.InsertAdvice(sapAdvice);
                }

                return registeredAdvice;
            }, string.Format("Register PM SAP Advice {0}.", sapAdvice.ExternalIdentifier));
        }

        /// <summary>
        /// Submits a Retrieved from SAP Material
        /// </summary>
        /// <param name="sapMaterial"></param>
        /// <returns>Registered Material</returns>
        private SCMS.Entity.PM.Material RegisterMaterial(SCMS.Entity.PM.Material sapMaterial)
        {
            return context.Execute(() =>
            {
                SCMS.Entity.PM.Material registeredMaterial;

                using (dataSource = new Data.PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    registeredMaterial = dataSource.InsertMaterial(sapMaterial);
                }

                return registeredMaterial;
            }, string.Format("Register PM SAP Material {0}.", sapMaterial.ExternalIdentifier));
        }

        #endregion Methods Post

        #region Methods Put

        /// <summary>
        /// Update a Retrieved from SAP WorkOrder
        /// </summary>
        /// <param name="sapWorkOrder"></param>
        /// <returns>Update Work Order</returns>
        private SCMS.Entity.PM.WorkOrder UpdateWorkOrder(SCMS.Entity.PM.WorkOrder sapWorkOrder)
        {
            return context.Execute(() =>
            {
                SCMS.Entity.PM.WorkOrder updateWorkOrder;

                using (dataSource = new Data.PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    SCMS.Entity.PM.WorkOrder modelFilter = new SCMS.Entity.PM.WorkOrder();
                    modelFilter.Identifier = sapWorkOrder.Identifier;
                    updateWorkOrder = dataSource.UpdateWorkOrder(sapWorkOrder, modelFilter);
                }

                return updateWorkOrder;
            }, string.Format("Update PM SAP Work Orders {0}.", sapWorkOrder.ExternalIdentifier));
        }

        /// <summary>
        /// Submits a Update from SAP TechnicalObject
        /// </summary>
        /// <param name="sapTechnicalObject"></param>
        /// <returns>Update TechnicalObject</returns>
        private SCMS.Entity.PM.TechnicalObject UpdateTechnicalObject(SCMS.Entity.PM.TechnicalObject sapTechnicalObject)
        {
            return context.Execute(() =>
            {
                SCMS.Entity.PM.TechnicalObject updateTechnicalObject;

                using (dataSource = new Data.PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    SCMS.Entity.PM.TechnicalObject modelFilter = new SCMS.Entity.PM.TechnicalObject();
                    modelFilter.ExternalIdentifier = sapTechnicalObject.ExternalIdentifier;
                    updateTechnicalObject = dataSource.UpdateTechnicalObject(sapTechnicalObject, modelFilter);
                }

                return updateTechnicalObject;
            }, string.Format("Update PM SAP Work Orders {0}.", sapTechnicalObject.ExternalIdentifier));
        }

        /// <summary>
        /// Submits a Update from SAP MaintenancePlan
        /// </summary>
        /// <param name="sapMaintenancePlan"></param>
        /// <returns>Update MaintenancePlan</returns>
        private SCMS.Entity.PM.MaintenancePlan UpdateMaintenancePlan(SCMS.Entity.PM.MaintenancePlan sapMaintenancePlan)
        {
            return context.Execute(() =>
            {
                SCMS.Entity.PM.MaintenancePlan updateMaintenancePlan;

                using (dataSource = new Data.PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    SCMS.Entity.PM.MaintenancePlan modelFilter = new SCMS.Entity.PM.MaintenancePlan();
                    modelFilter.ExternalIdentifier = sapMaintenancePlan.ExternalIdentifier;
                    updateMaintenancePlan = dataSource.UpdateMaintenancePlan(sapMaintenancePlan, modelFilter);
                }

                return updateMaintenancePlan;
            }, string.Format("Update PM SAP MaintenancePlan {0}.", sapMaintenancePlan.ExternalIdentifier));
        }

        /// <summary>
        /// Submits a Update from SAP Task
        /// </summary>
        /// <param name="sapTask"></param>
        /// <returns>Update Task</returns>
        private SCMS.Entity.PM.Task UpdateTask(SCMS.Entity.PM.Task sapTask)
        {
            return context.Execute(() =>
            {
                SCMS.Entity.PM.Task updateTask;

                using (dataSource = new Data.PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    SCMS.Entity.PM.Task modelFilter = new SCMS.Entity.PM.Task();
                    modelFilter.ExternalIdentifier = sapTask.ExternalIdentifier;
                    updateTask = dataSource.UpdateTask(sapTask, modelFilter);
                }

                return updateTask;
            }, string.Format("Update PM SAP Task {0}.", sapTask.ExternalIdentifier));
        }

        /// <summary>
        /// Submits a Update from SAP Material
        /// </summary>
        /// <param name="sapMaterial"></param>
        /// <returns>Update Material</returns>
        private SCMS.Entity.PM.Material UpdateMaterial(SCMS.Entity.PM.Material sapMaterial)
        {
            return context.Execute(() =>
            {
                SCMS.Entity.PM.Material updateMaterial;

                using (dataSource = new Data.PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    SCMS.Entity.PM.Material modelFilter = new SCMS.Entity.PM.Material();
                    modelFilter.ExternalIdentifier = sapMaterial.ExternalIdentifier;
                    updateMaterial = dataSource.UpdateMaterial(sapMaterial, modelFilter);
                }

                return updateMaterial;
            }, string.Format("Update PM SAP Material {0}.", sapMaterial.ExternalIdentifier));
        }

        #endregion Methods Put

        #endregion Methods CRUDS Register WorkOrders

        /// <summary>
        /// Retrieve Designated SAP User for the Given Security Context´s Device
        /// </summary>
        /// <returns>SAP User Login</returns>
        private string GetDeviceSAPUser()
        {
            return context.Execute(() =>
            {
                string deviceIdentifier = context.SecurityContext.DeviceID;
                string sapUser;
                //TODO: Get SAP User for Device Identifier.
                using (dataSource = new Data.PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    sapUser = dataSource.GetSAPUser(deviceIdentifier);
                }
                return sapUser;
            }, string.Format("Get Device {0} SAP User", context.SecurityContext.DeviceID));
        }

        /// <summary>
        /// Submit a Predictive/Scheduled Maintenance Work Order and its tasks
        /// </summary>
        /// <param name="workOrder">Commited Work Order Information</param>
        /// <returns>Updated Work Order Information on SAP</returns>
        public SCMS.Entity.PM.WorkOrder SubmitExecutedMaintenanceWorkOrder(SCMS.Entity.PM.WorkOrder workOrder)
        {
            return context.Execute(() =>
            {
                string sapUser = GetDeviceSAPUser();
                workOrder.Activities.ForEach(a =>
                    SubmitTask(a)
                );
                workOrder.State = kPMCommitteStatus;
                workOrder.ExecutionStartAt = workOrder.Activities.OrderBy(a => a.StartedAt).FirstOrDefault().StartedAt;
                workOrder.ExecutionFinishedAt = workOrder.Activities.OrderBy(a => a.FinishedAt).LastOrDefault().FinishedAt;

                SCMS.Entity.PM.WorkOrder updatedWorkOrder = UpdateSAPWorkOrder(workOrder);
                //TODO: Persist Work Order to SAP.
                using (dataSource = new Data.PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    updatedWorkOrder = dataSource.UpdateWorkOrder(workOrder, workOrder);
                }
                return updatedWorkOrder;
            }, string.Format("Submmit Executed Maintenance Work Order {0}", workOrder.ExternalIdentifier));
        }

        private SCMS.Entity.PM.WorkOrder UpdateSAPWorkOrder(SCMS.Entity.PM.WorkOrder workOrder)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Submit a Predictive/Scheduled Maintenance Work Order Task Information to SAP
        /// </summary>
        /// <param name="task">Task Information</param>
        /// <returns>Registered Task on SAP</returns>
        private SCMS.Entity.PM.Task SubmitTask(SCMS.Entity.PM.Task task)
        {
            return context.Execute(() =>
            {
                string sapUser = GetDeviceSAPUser();

                task.Status = kPMCommitteStatus;
                task.Performer = context.SecurityContext.ClientID;

                SCMS.Entity.PM.Task submittedTask = UpdateSAPTask(task);
                //TODO: Persiste task on SAP.
                using (dataSource = new Data.PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    submittedTask = dataSource.UpdateTask(task, task);
                }
                return submittedTask;
            }, string.Format("Submmit Task {0} from SAP Work Order {1}", task.Identifier, task.WorkOrder.ExternalIdentifier));
        }

        private SCMS.Entity.PM.Task UpdateSAPTask(SCMS.Entity.PM.Task task)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Submit a Scheduled Maintenance Component Replacement to SAP
        /// </summary>
        /// <param name="replacement">Replacement Information</param>
        /// <returns>Registered Measurement Document on SAP</returns>
        public SCMS.Entity.PM.ComponentReplacement SubmitComponentReplacement(SCMS.Entity.PM.ComponentReplacement replacement)
        {
            return context.Execute(() =>
            {
                string sapUser = GetDeviceSAPUser();

                SCMS.Entity.PM.ComponentReplacement submittedComponentReplacement = RegisterSAPComponentReplacement(replacement);
                //TODO: Persiste Component Replacement on SAP.
                using (dataSource = new Data.PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    submittedComponentReplacement = dataSource.InsertComponentReplacement(submittedComponentReplacement);
                }
                return submittedComponentReplacement;
            }, string.Format("Submit Component Replacement for task {0}", replacement.Task.Identifier));
        }

        private SCMS.Entity.PM.ComponentReplacement RegisterSAPComponentReplacement(SCMS.Entity.PM.ComponentReplacement replacement)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Submit a Predictive/Corrective Maintenance Measurement Document to SAP
        /// </summary>
        /// <param name="document">Measurement Document Information</param>
        /// <returns>Registered Measurement Document on SAP</returns>
        public SCMS.Entity.PM.MeasurementDocument SubmitMeasurementDocument(SCMS.Entity.PM.MeasurementDocument document)
        {
            return context.Execute(() =>
            {
                //string sapUser = GetDeviceSAPUser();

                //Send Model MeasuremntDocument to SAP
                SCMS.Entity.PM.MeasurementDocument submittedMeasurementDocument = RegisterSAPMeasureDocument(document);

                //TODO: Persist Component Replacement on SAP.
                using (dataSource = new Data.PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    //Insert MeasurementDocument
                    submittedMeasurementDocument = dataSource.InsertMeasurementDocument(submittedMeasurementDocument);
                }
                SCMS.Entity.PM.MeasurementDocument measureDocumentSap = new SCMS.Entity.PM.MeasurementDocument();
                measureDocumentSap.Identifier = submittedMeasurementDocument.Identifier;
                measureDocumentSap.ExternalIdentifier = submittedMeasurementDocument.ExternalIdentifier;
                //Insert
                for (int i = 0; i < document.Measures.Count; i++)
                {
                    document.Measures[i].Document = measureDocumentSap;
                }
                submittedMeasurementDocument.Measures = document.Measures.Select(m => SubmitMeasure(m)).ToList();
                return submittedMeasurementDocument;
            }, string.Format("Submit Measurement Document for task {0}", document.Task.Identifier));
        }

        private SCMS.Entity.PM.MeasurementDocument RegisterSAPMeasureDocument(SCMS.Entity.PM.MeasurementDocument document)
        {
            //

            SOFTTEK.SAP.Integration.SAPConnection sapConnector = new SAP.Integration.SAPConnection();

            var connectDestination = sapConnector.LoadDestination();
            var destinationRepository = connectDestination.Repository;

            //var testRFC = testDestinationRepository.CreateFunction("ZRFC_PM_TOMAR_MEDIDA");
            var initializerRFC = destinationRepository.CreateFunction(new System.Configuration.AppSettingsReader().GetValue("SAP_RFC_PM_MEASUREMENT_DOCUMENT", typeof(string)).ToString());
            var rfcMetadata = initializerRFC.Metadata;

            bool stateConnection = Ping(connectDestination);

            Dictionary<string, object> input = new Dictionary<string, object>();
            Dictionary<string, object> output = new Dictionary<string, object>();

            initializerRFC.SetValue("I_PUNTO", "9003");
            //testRFC.SetValue("I_PUNTO", document.Measures.FirstOrDefault().ExternalIdentifier.ToString());
            initializerRFC.SetValue("I_MEDIDA", document.Measures.FirstOrDefault().Value.ToString());
            initializerRFC.SetValue("I_USER", "JEGONZALEZ");

            if (stateConnection)
                initializerRFC.Invoke(connectDestination);

            if (initializerRFC.GetString("EV_INDICADOR") == "0")
            {
                for (int i = 0; i < rfcMetadata.ParameterCount; i++)
                {
                    if (rfcMetadata[i].Direction.Equals(global::SAP.Middleware.Connector.RfcDirection.IMPORT))
                    {
                        Type inputType = SAP.Integration.SAPData.GetDataType(rfcMetadata[i].DataType);
                        input[rfcMetadata[i].Name] = initializerRFC.GetValue(rfcMetadata[i].Name);//inputType.IsValueType ? Activator.CreateInstance(inputType) : null;
                    }
                    else if (rfcMetadata[i].Direction.Equals(global::SAP.Middleware.Connector.RfcDirection.EXPORT))
                    {
                        Type outputType = SAP.Integration.SAPData.GetDataType(rfcMetadata[i].DataType);
                        output[rfcMetadata[i].Name] = initializerRFC.GetValue(rfcMetadata[i].Name); //outputType.IsValueType ? Activator.CreateInstance(outputType) : null;
                    }
                }
                // TO DO:
                var name = output["EV_DOCUMENTO"].ToString();
                document.ExternalIdentifier = name;
                return document;
            }
            else
            {
                throw new Exception(String.Format("No ha sido posible insertar el documento de medida. Causa: {0}", initializerRFC.GetString("EV_TEXT")));
            }
        }

        /// <summary>
        /// Submit a Mesure Entry for a registered Measument Document
        /// </summary>
        /// <param name="measure">Measure information</param>
        /// <returns>Registered Measure on SAP</returns>
        private SCMS.Entity.PM.Measure SubmitMeasure(SCMS.Entity.PM.Measure measure)
        {
            return context.Execute(() =>
            {
                SCMS.Entity.PM.Measure submittedMeasure;
                using (dataSource = new Data.PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    submittedMeasure = dataSource.InsertMeasure(measure);
                }
                return submittedMeasure;
            }, string.Format("Submit Measure for Measument Document {0}", measure.Document.Identifier));
        }

        /// <summary>
        /// Submit a Mesure Entry for a registered Measument Document
        /// </summary>
        /// <param name="measure">Measure information</param>
        /// <returns>Registered Measure on SAP</returns>
        private SCMS.Entity.PM.Notifications SubmitMeasure(SCMS.Entity.PM.Notifications notifications)
        {
            return context.Execute(() =>
            {
                SCMS.Entity.PM.Notifications submittedNotifications = RegisterSAPNotifications(notifications);
                using (dataSource = new Data.PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                }
                return submittedNotifications;
            }, string.Format("Submit Notifications in SAP"));
        }

        /// <summary>
        /// Submit a Scheduled/Corrective Maintenance Advice to SAP
        /// </summary>
        /// <param name="advice">Advice information</param>
        /// <returns>Registered Advice on SAP</returns>
        public SCMS.Entity.PM.Advice SubmitAdvise(SCMS.Entity.PM.Advice advice)
        {
            if (!advice.Task.Equals(default(Task)))
            {
                return context.Execute(() =>
                    {
                        SCMS.Entity.PM.Advice submittedAdvice = RegisterSAPAdvice(advice);
                        using (dataSource = new Data.PMDataContext(context.SecurityContext))
                        {
                            dataSource.ConnectionString = "SRA";
                            dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                            dataSource.Initialize();
                            submittedAdvice = dataSource.InsertAdvice(submittedAdvice);
                        }
                        return submittedAdvice;
                    }, string.Format("Submit Scheduled Advice with task {0} for Asset with SAP identifier {1}", advice.Task.Identifier, advice.TechnicalObject.ExternalIdentifier));
            }
            else
            {
                return context.Execute(() =>
                {
                    SCMS.Entity.PM.Advice submittedAdvice = RegisterSAPCorrectiveAdvice(advice);
                    using (dataSource = new Data.PMDataContext(context.SecurityContext))
                    {
                        dataSource.ConnectionString = "SRA";
                        dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                        dataSource.Initialize();
                        submittedAdvice = dataSource.InsertAdvice(submittedAdvice);
                    }
                    return submittedAdvice;
                }, string.Format("Submit Corrective Maintenance Advice for Asset with SAP identifier {0}", advice.TechnicalObject.ExternalIdentifier));
            }
        }

        private SCMS.Entity.PM.Advice RegisterSAPCorrectiveAdvice(SCMS.Entity.PM.Advice advice)
        {
            throw new NotImplementedException();
        }

        private SCMS.Entity.PM.Advice RegisterSAPAdvice(SCMS.Entity.PM.Advice advice)
        {
            return context.Execute(() =>
            {
                //string sapUser = GetDeviceSAPUser();
                string sapUser = "JEGONZALEZ";

                SCMS.Entity.PM.WorkOrder filter = new SCMS.Entity.PM.WorkOrder();
                filter.Performer = sapUser;
                filter.State = kPMScheduledStatus;

                DateTime from = DateTime.Now.Date;
                DateTime to = from.AddDays(1);

                SCMS.Entity.PM.Advice modelAdvice;

                modelAdvice = InsertSapAdvice(sapUser, advice);

                if (modelAdvice == null)
                {
                    throw new Exception("Error insert the advice in SAP.");
                }

                using (dataSource = new Data.PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();

                    var insertAdvice = dataSource.InsertAdvice(modelAdvice);
                    modelAdvice = insertAdvice;
                }
                return modelAdvice;
            }, "Retrieve Pending Work Orders for a given device´s SAP User");
        }

        private SCMS.Entity.PM.Notifications RegisterSAPNotifications(SCMS.Entity.PM.Notifications notifications)
        {
            return context.Execute(() =>
            {
                //string sapUser = GetDeviceSAPUser();
                string sapUser = "JEGONZALEZ";

                SCMS.Entity.PM.Notifications modelNotifications;

                modelNotifications = InsertSapEndNotifications(notifications);

                if (modelNotifications == null)
                {
                    throw new Exception("Error insert the advice in SAP.");
                }

                using (dataSource = new Data.PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();

                    //var insertAdvice = dataSource.InsertAdvice(modelAdvice);
                    //modelAdvice = insertAdvice;
                }
                return modelNotifications;
            }, "Insert Notification for a given device´s SAP User");
        }

        #region Initial Setup

        /// <summary>
        /// Retrieve PM Equipments Information and its corresponding Measures
        /// </summary>
        /// <returns>Retrieved PM Equipments on SAP</returns>
        public List<SCMS.Entity.PM.TechnicalObject> RetrievePMEquipmentAssets()
        {
            return context.Execute(() =>
            {
                //string sapUser = GetDeviceSAPUser();
                string sapUser = "JEGONZALEZ";
                List<SCMS.Entity.PM.TechnicalObject> equipments = GetSapTechnicalObjects(sapUser);
                return equipments;
            }, "Retrieve PM Equipments Assets for a given device´s SAP User");
        }

        private List<SCMS.Entity.PM.TechnicalObject> RetrievePMSAPEquipments()
        {
            return context.Execute(() =>
            {
                List<SCMS.Entity.PM.TechnicalObject> sapPMEquipment = new List<SCMS.Entity.PM.TechnicalObject>();

                List<SCMS.Entity.PM.TechnicalObject> registeredEquipments = sapPMEquipment.Select(e => RegisterEquipment(e)).ToList();
                return registeredEquipments;
            }, "Retrieve and store PM Equipment from SAP");
        }

        private SCMS.Entity.PM.TechnicalObject RegisterEquipment(SCMS.Entity.PM.TechnicalObject sapEquipment)
        {
            return context.Execute(() =>
            {
                SCMS.Entity.PM.TechnicalObject submittedEquipment;
                using (dataSource = new Data.PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    submittedEquipment = dataSource.InsertTechnicalObject(sapEquipment);
                }
                return submittedEquipment;
            }, string.Format("Register SAP Technical Location {0}", sapEquipment.ExternalIdentifier));
        }

        /// <summary>
        /// Retrieve PM Technical Locations and its corresponding Measures
        /// </summary>
        /// <returns>Retrieved PM Technical Locations on SAP</returns>
        public List<SCMS.Entity.PM.TechnicalObject> RetrievePMTechnicalLocationsAssets()
        {
            return context.Execute(() =>
            {
                string sapUser = GetDeviceSAPUser();
                List<SCMS.Entity.PM.TechnicalObject> technicalLocations = RetrievePMSAPTechnicalLocations();
                return technicalLocations;
            }, "Retrieve PM Technical Locations Assets for a given device´s SAP User");
        }

        private List<SCMS.Entity.PM.TechnicalObject> RetrievePMSAPTechnicalLocations()
        {
            return context.Execute(() =>
            {
                List<SCMS.Entity.PM.TechnicalObject> sapPMTechnicalLocations = new List<SCMS.Entity.PM.TechnicalObject>();

                List<SCMS.Entity.PM.TechnicalObject> registeredTechnicalLocations = sapPMTechnicalLocations.Select(t => RegisterTechnicalLocation(t)).ToList();
                return registeredTechnicalLocations;
            }, "Retrieve and store PM Technical Location from SAP");
        }

        private SCMS.Entity.PM.TechnicalObject RegisterTechnicalLocation(SCMS.Entity.PM.TechnicalObject sapTechnicalLocation)
        {
            return context.Execute(() =>
            {
                SCMS.Entity.PM.TechnicalObject submittedTechnicalLocation;
                using (dataSource = new Data.PMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    submittedTechnicalLocation = dataSource.InsertTechnicalObject(sapTechnicalLocation);
                }
                return submittedTechnicalLocation;
            }, string.Format("Register SAP Technical Location {0}", sapTechnicalLocation.ExternalIdentifier));
        }

        /// <summary>
        /// Retrieve PM Materials to be used on Components Replacement Functionality
        /// </summary>
        /// <returns>Retrieved PM Materials on SAP</returns>
        public List<SCMS.Entity.PM.Material> RetrievePMMaterials()
        {
            return context.Execute(() =>
            {
                string sapUser = GetDeviceSAPUser();
                List<SCMS.Entity.PM.Material> materials = RetrievePMSAPMaterials();
                return materials;
            }, "Retrieve PM Materials for a given device´s SAP User");
        }

        private List<SCMS.Entity.PM.Material> RetrievePMSAPMaterials()
        {
            return context.Execute(() =>
            {
                List<SCMS.Entity.PM.Material> sapPMMaterials = new List<SCMS.Entity.PM.Material>();

                List<SCMS.Entity.PM.Material> registeredMaterials = sapPMMaterials.Select(m => RegisterMaterial(m)).ToList();
                return registeredMaterials;
            }, "Retrieve and store PM Master Entities from SAP");
        }

        //private SCMS.Entity.PM.Material RegisterMaterial(SCMS.Entity.PM.Material sapMaterial)
        //{
        //    return context.Execute(() =>
        //    {
        //        SCMS.Entity.PM.Material submittedMaterial; ;
        //        using (dataSource = new Data.PMDataContext(context.SecurityContext))
        //        {
        //            dataSource.ConnectionString = "SRA";
        //            dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
        //            dataSource.Initialize();
        //            submittedMaterial = dataSource.InsertMaterial(sapMaterial);
        //        }
        //        return submittedMaterial;
        //    }, string.Format("Register SAP Material {0}", sapMaterial.Identifier));
        //}

        #endregion Initial Setup
    }
}