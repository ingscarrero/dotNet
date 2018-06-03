using Microsoft.VisualStudio.TestTools.UnitTesting;
using SAP.Middleware.Connector;
using SOFTTEK.SCMS.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace SOFTTEK.SCMS.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CryptographyTest()
        {
            byte[] salt_bytes = Convert.FromBase64String(SCMS.Test.Properties.Settings.Default.SCMS_SALTBytes);
            SOFTTEK.SCMS.Foundation.Security.SymmetricCipherProvider c = new Foundation.Security.SymmetricCipherProvider(salt_bytes);
            c.SymmmetricAlgorithmName = "AES";
            c.Key = SCMS.Test.Properties.Settings.Default.SCMS_Password;

            string s = c.EncryptData("sergio.carrero");
            string t = c.DecryptData(s);
        }

        [TestMethod]
        public void SRAWeekTest()
        {
            SOFTTEK.SCMS.Business.Entity.SRA.ARSDetailedActivity act = new Business.Entity.SRA.ARSDetailedActivity();

            act.Details = "DevelopmentActivity:TestDA|Stage:TestStage|TestDetail";
            act.DevelopmentActivity = "TestDA2";
            act.Stage = "TestStage2";

            Assert.IsNotNull(act.Details);
            Assert.IsNotNull(act.DevelopmentActivity);
            Assert.IsNotNull(act.Stage);
        }

        [TestMethod]
        public void TestMasterDataRFC()
        {
            PMDataContext dc = new PMDataContext(null);
            var result = dc.GetMasterData();
            var categories = result.GroupBy(p => p.Description).Select(g => g.ToList()).ToList();
        }

        [TestMethod]
        public void TestNotification()
        {
            PMDataContext dc = new PMDataContext(null);
            TimeSpan t = new TimeSpan(1, 5, 56);
            Entity.PM.Notifications n = new Entity.PM.Notifications
            {
                ActualWork = string.Format("{0:00}:{1:00}:{2:00}", t.Hours, t.Minutes, t.Seconds),
                ExecutionStartAt = DateTime.Now.ToString("yyyyMMdd HH:mm:ss"),
                Priority = "2"
            };
            var result = dc.SubmitWorkOrder(null);
        }

        [TestMethod]
        public void TestWorkOrder()
        {
            PMDataContext dc = new PMDataContext(null);
            var wo = dc.GetWorkOrdersInPeriod();
            var result = dc.WorkOrderNotifications(wo.First().ExternalIdentifier);
        }

        [TestMethod]
        public void TestWorkOrders()
        {
            PMDataContext dc = new PMDataContext(null);
            dc.GetWorkOrdersInPeriod();
        }

        [TestMethod]
        public void TestGetTechnicalObjects()
        {
            PMDataContext dc = new PMDataContext(null);
            var s = dc.GetTechnicalObjects();
        }

        [TestMethod]
        public void TestCreateAdvise()
        {
            PMDataContext dc = new PMDataContext(null);
            Entity.PM.Advice a = new Entity.PM.Advice
            {
                Priority = "2",
                TechnicalObject = new Entity.PM.TechnicalObject
                {
                    ExternalIdentifier = "000000000010013816",
                    TechnicalLocation = new Entity.PM.TechnicalObject
                    {
                        ExternalIdentifier = "?0100000000000005587",
                        TOType = Entity.PM.TechnicalObjectTypes.TechnicalObjectTypeTechnicalLocation
                    },
                    TOType = Entity.PM.TechnicalObjectTypes.TechnicalObjectTypeEquipment
                },
                Comments = "Test",
                Type = "",
                ExecutionStartAt = DateTime.Now.AddHours(-3),
                ExecutionFinishedAt = DateTime.Now.AddHours(-2),
            };

            var s = dc.CreateAdvice(a);
        }

        [TestMethod]
        public void TestSAPConnection()
        {
            SOFTTEK.SAP.Integration.SAPConnection sapConnector = new SAP.Integration.SAPConnection();
            Assert.IsNotNull(sapConnector);
            //string connectionString = sapConnector.SAPConnectionString();
            var testDestination = sapConnector.LoadDestination();
            testDestination.Ping();

            var testDestinationRepository = testDestination.Repository;

            var testRFC = testDestinationRepository.CreateFunction("ZRFC_PM_TOMAR_MEDIDA");

            var rfcMetadata = testRFC.Metadata;

            Dictionary<string, object> input = new Dictionary<string, object>();
            Dictionary<string, object> output = new Dictionary<string, object>();

            testRFC.SetValue("I_PUNTO", "9003");
            testRFC.SetValue("I_MEDIDA", "1000");

            testRFC.Invoke(testDestination);

            for (int i = 0; i < rfcMetadata.ParameterCount; i++)
            {
                if (rfcMetadata[i].Direction.Equals(global::SAP.Middleware.Connector.RfcDirection.IMPORT))
                {
                    Type inputType = SAP.Integration.SAPData.GetDataType(rfcMetadata[i].DataType);
                    input[rfcMetadata[i].Name] = testRFC.GetValue(rfcMetadata[i].Name);//inputType.IsValueType ? Activator.CreateInstance(inputType) : null;
                }
                else if (rfcMetadata[i].Direction.Equals(global::SAP.Middleware.Connector.RfcDirection.EXPORT))
                {
                    Type outputType = SAP.Integration.SAPData.GetDataType(rfcMetadata[i].DataType);
                    output[rfcMetadata[i].Name] = testRFC.GetValue(rfcMetadata[i].Name); //outputType.IsValueType ? Activator.CreateInstance(outputType) : null;
                }
            }

            Assert.IsNotNull(output);
        }

        [TestMethod]
        public void InsertSapAdvice()
        {
            SCMS.Entity.PM.Advice returnList = new SCMS.Entity.PM.Advice();
            var sapUser = "JEGONZALEZ";
            SOFTTEK.SCMS.Entity.PM.Advice modelInsert = new Entity.PM.Advice();
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

            //if(stateConnection)
            //initializerRFC.Invoke(connectDestination);

            //if (initializerRFC.GetString("EV_INDICADOR") == "0")
            //{
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
            //}
            //else
            //{
            //    throw new Exception(String.Format("No ha sido posible cosultar las ordenes de trabajo. Causa: {0}", initializerRFC.GetString("EV_TEXT")));
            //}

            TimeSpan ts = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            TIME time = new TIME(Convert.ToUInt32(DateTime.Now.Hour), Convert.ToUInt32(DateTime.Now.Minute), Convert.ToUInt32(DateTime.Now.Second));

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("EQUIPMENT", "");
            dic.Add("FUNCT_LOC", "");
            dic.Add("PRIORITY", "Alta");
            dic.Add("NOTIF_DATE", DateTime.Now);
            dic.Add("SHORT_TEXT", "");
            dic.Add("BREAKDOWN", "");
            dic.Add("STRMLFNDATE", DateTime.Now);
            dic.Add("STRMLFNTIME", time.ToString());
            dic.Add("ENDMLFNDATE", DateTime.Now);
            dic.Add("ENDMLFNTIME", time.ToString());
            IRfcStructure dictonaryMetadataTable;
            try
            {
                dictonaryMetadataTable = SOFTTEK.SAP.Integration.SAPData.CreateRFCStructure(dic, connectDestination, "ZPMS_AVISOS_MANTENIMIENTO");
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Ha ocurrido un error, causa : {0}", ex));
            }
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

        [TestMethod]
        public void RFCAnalysis()
        {
            SOFTTEK.SAP.Integration.SAPConnection sapConnector = new SAP.Integration.SAPConnection();
            Assert.IsNotNull(sapConnector);
            //string connectionString = sapConnector.SAPConnectionString();
            var testDestination = sapConnector.LoadDestination();
            testDestination.Ping();

            var testDestinationRepository = testDestination.Repository;

            string[] rfcs = {
                "ZRFC_PM_TOMAR_MEDIDA",
                "ZFM_RFC_OBJETO_TECNICO",
                "ZFM_RFC_ORDENES_MANTENIMIENTO",
                "ZFM_RFC_NOTIFICACION_ORDEN",
                "ZFM_RFC_CREACION_AVISOS",
                "ZFM_RFC_UPDATE_NOTIFICACION",
            };

            Dictionary<string, object> rfcsMetadata = new Dictionary<string, object>();
            rfcs.ToList().ForEach(s =>
            {
                var testRFC = testDestinationRepository.CreateFunction(s);
                var rfcMetadata = testRFC.Metadata;
                rfcsMetadata[s] = rfcMetadata;
            });
        }
    }
}