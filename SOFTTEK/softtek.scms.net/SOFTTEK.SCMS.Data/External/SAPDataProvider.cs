using SAP.Middleware.Connector;
using SOFTTEK.SAP.Integration;
using SOFTTEK.SCMS.Foundation.Data;
using SOFTTEK.SCMS.Foundation.Data.Loggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFTTEK.SCMS.Data.External
{
    public class SAPDataProvider
    {
        private const string kRFCNameMasterData = "ZFM_PM_RFC_DATOS_MAESTROS";
        private const string kRFCNameMeasurementDocument = "ZRFC_PM_TOMAR_MEDIDA";
        private const string kRFCNameTechnicalObject = "ZFM_RFC_OBJETO_TECNICO";
        private const string kRFCNameWorkOrders = "ZFM_RFC_ORDENES_MANTENIMIENTO";
        private const string kRFCNameCreateAdvice = "ZFM_RFC_CREACION_AVISOS";
        private const string kRFCNameWorkOrderNotification = "ZFM_RFC_NOTIFICACION_ORDEN";
        private const string kRFCNameWorkOrderNotificationUpdate = "ZFM_RFC_UPDATE_NOTIFICACION";

        private const string kParameterInputDirection = "INPUT";
        private const string kParameterOutputDirection = "OUTPUT";

        public const string kRFCResponseIndicatorFieldName = "EV_INDICADOR";
        public const string kRFCResponseDetailFieldName = "EV_TEXT";
        public const string kRFCResponseUserFieldName = "EV_USER";
        public const string kDefaultResponseIndicator = "0";

        public DataContext Context { get; set; }
        public Logger Logger { get; set; }

        public SAPDataProvider(DataContext ctx)
        {
            Logger = new DefaultLogger();
            Context = ctx;
        }

        /// <summary>
        /// Delegate method responsible of building up the RFC request and map the response into a list
        /// of elements by using the provided mopping function.
        /// </summary>
        /// <typeparam name="K">Expected Type of elements according to the provided mapper parameter</typeparam>
        /// <param name="rfcName">Name of the RFC</param>
        /// <param name="parameters">Dictionary with the required parameters to invoke the RFC</param>
        /// <param name="mapper">Function delegate responsible of mapping table structures into list of elements of type T.</param>
        /// <returns>Dictionary that stores</returns>
        private Dictionary<string, object> RetrieveResponseData<K>(string rfcName, Dictionary<string, object> parameters = null, Func<string, Func<Dictionary<string, object>, K>> mapper = null)
        {
            SOFTTEK.SAP.Integration.SAPConnection sapConnector = new SAP.Integration.SAPConnection();

            var sapDestination = sapConnector.LoadDestination();
            var sapDestinationRepository = sapDestination.Repository;
            var sapRFC = sapDestinationRepository.CreateFunction(rfcName);

            /// Add Input Parameters
            if (null != parameters && parameters.Keys != null)
            {
                parameters.Keys.ToList().ForEach(k => sapRFC.SetValue(k, parameters[k]));
            }

            /// Invoke RFC for data retrieval
            sapRFC.Invoke(sapDestination);

            var rfcMetadata = sapRFC.Metadata;

            Dictionary<string, object> rfcParameters = new Dictionary<string, object>();

            // Retrieve data from response
            for (int i = 0; i < rfcMetadata.ParameterCount; i++)
            {
                if (global::SAP.Middleware.Connector.RfcDirection.IMPORT.Equals(rfcMetadata[i].Direction))
                {
                    rfcParameters[rfcMetadata[i].Name] = new
                    {
                        Direction = kParameterInputDirection,
                        Type = SAP.Integration.SAPData.GetDataType(rfcMetadata[i].DataType),
                        Value = sapRFC.GetValue(rfcMetadata[i].Name)
                    };
                }
                else if (global::SAP.Middleware.Connector.RfcDirection.EXPORT.Equals(rfcMetadata[i].Direction))
                {
                    rfcParameters[rfcMetadata[i].Name] = new
                    {
                        Direction = kParameterOutputDirection,
                        Type = SAP.Integration.SAPData.GetDataType(rfcMetadata[i].DataType),
                        Value = sapRFC.GetValue(rfcMetadata[i].Name)
                    };
                }
                else if (global::SAP.Middleware.Connector.RfcDirection.TABLES.Equals(rfcMetadata[i].Direction))
                {
                    IRfcTable rfcTable = (IRfcTable)sapRFC.GetValue(rfcMetadata[i].Name);

                    if (rfcTable != default(IRfcTable))
                    {
                        if (mapper != null && mapper(rfcMetadata[i].Name) != null)
                        {
                            rfcParameters[rfcMetadata[i].Name] = new
                            {
                                Direction = kParameterOutputDirection,
                                Type = typeof(List<K>),
                                Value = SAP.Integration.SAPData.RFCTableToGenericList(rfcTable, mapper(rfcMetadata[i].Name))
                            };
                        }
                        else
                        {
                            rfcParameters[rfcMetadata[i].Name] = new
                            {
                                Direction = kParameterOutputDirection,
                                Type = typeof(Dictionary<string, object>),
                                Value = rfcTable.ToListOfDictionaries()
                            };
                        }
                    }
                }
            }

            return rfcParameters;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <param name="mapper"></param>
        /// <param name="parameters">Dictionary with th value for the required parameters of the RFC:
        /// <para/>- I_PUNTO:      External identifier of the measurement point
        /// <para/>- I_MEDIDA:     Value
        /// <para/>- I_USER:       User that submit the measurement
        /// <returns>Diction</returns>
        public Dictionary<string, object> RegisterMeasurementPoint(Dictionary<string, object> parameters)
        {
            try
            {
                Dictionary<string, object> rfcParameters = RetrieveResponseData<object>(kRFCNameMeasurementDocument, parameters);

                // Validate RFC's response
                if (rfcParameters[kRFCResponseIndicatorFieldName] != null &&
                    !kDefaultResponseIndicator.Equals(((dynamic)rfcParameters[kRFCResponseIndicatorFieldName]).Value))
                {
                    throw new InvalidOperationException(
                        string.Format("The SAP RFC {0} has completed the request with code {1}. {2}",
                            kRFCNameMeasurementDocument,
                            ((dynamic)rfcParameters[kRFCResponseIndicatorFieldName]).Value,
                            ((dynamic)rfcParameters[kRFCResponseDetailFieldName]).Value)
                    );
                }

                Logger.Log(new Foundation.Entity.LogEntry
                {
                    Type = LOG_TYPE.LOG_TYPE_MESSAGE,
                    Description = string.Format("The SAP RFC {0} has completed the request with code {1}. {2}",
                            kRFCNameMasterData,
                            ((dynamic)rfcParameters[kRFCResponseIndicatorFieldName]).Value,
                            ((dynamic)rfcParameters[kRFCResponseDetailFieldName]).Value),
                    App = Context.SecurityContext.AppID,
                    Device = Context.SecurityContext.DeviceID,
                    Module = "SAPDataProvider.RegisterMeasurementPoint",
                    User = Context.SecurityContext.ClientID
                });

                return rfcParameters;
            }
            catch (Exception ex)
            {
                Logger.Log(new Foundation.Entity.LogEntry
                {
                    Type = LOG_TYPE.LOG_TYPE_ERROR,
                    Description = ex.Message,
                    App = Context.SecurityContext.AppID,
                    Device = Context.SecurityContext.DeviceID,
                    Module = "SAPDataProvider.RegisterMeasurementPoint",
                    User = Context.SecurityContext.ClientID
                });

                throw ex;
            }
        }

        public Dictionary<string, object> GetWorkOrderDetails<K>(Dictionary<string, object> parameters, Func<string, Func<Dictionary<string, object>, K>> mapper)
        {
            try
            {
                //STRUCTURE ZPMS_UPDATE_NOTIF
                Dictionary<string, object> rfcParameters = RetrieveResponseData(kRFCNameWorkOrderNotification, parameters, mapper);

                // Validate RFC's response
                if (rfcParameters[kRFCResponseIndicatorFieldName] != null &&
                    !kDefaultResponseIndicator.Equals(((dynamic)rfcParameters[kRFCResponseIndicatorFieldName]).Value))
                {
                    throw new InvalidOperationException(
                        string.Format("The SAP RFC {0} has completed the request with code {1}. {2}",
                            kRFCNameMeasurementDocument,
                            ((dynamic)rfcParameters[kRFCResponseIndicatorFieldName]).Value,
                            ((dynamic)rfcParameters[kRFCResponseDetailFieldName]).Value)
                    );
                }

                Logger.Log(new Foundation.Entity.LogEntry
                {
                    Type = LOG_TYPE.LOG_TYPE_MESSAGE,
                    Description = string.Format("The SAP RFC {0} has completed the request with code {1}. {2}",
                            kRFCNameMasterData,
                            ((dynamic)rfcParameters[kRFCResponseIndicatorFieldName]).Value,
                            ((dynamic)rfcParameters[kRFCResponseDetailFieldName]).Value),
                    App = Context.SecurityContext.AppID,
                    Device = Context.SecurityContext.DeviceID,
                    Module = "SAPDataProvider.SubmitWorkOrderActivity",
                    User = Context.SecurityContext.ClientID
                });

                return rfcParameters;
            }
            catch (Exception ex)
            {
                Logger.Log(new Foundation.Entity.LogEntry
                {
                    Type = LOG_TYPE.LOG_TYPE_ERROR,
                    Description = ex.Message,
                    App = Context.SecurityContext.AppID,
                    Device = Context.SecurityContext.DeviceID,
                    Module = "SAPDataProvider.SubmitWorkOrderActivity",
                    User = Context.SecurityContext.ClientID
                });

                throw ex;
            }
        }

        public Dictionary<string, object> SubmitWorkOrderActivity(Dictionary<string, object> parameters)
        {
            try
            {
                //STRUCTURE ZPMS_UPDATE_NOTIF
                SOFTTEK.SAP.Integration.SAPConnection sapConnector = new SAP.Integration.SAPConnection();
                var sapDestination = sapConnector.LoadDestination();

                Dictionary<string, object> rfcInputParameters = new Dictionary<string, object>();
                rfcInputParameters["ET_TIMETICKETS"] = SAPData.CreateRFCStructure(
                    parameters,
                    sapDestination,
                    "ZPMS_UPDATE_NOTIF"
                );

                Dictionary<string, object> rfcParameters = RetrieveResponseData<object>(kRFCNameWorkOrderNotificationUpdate, rfcInputParameters);

                // Validate RFC's response
                if (rfcParameters[kRFCResponseIndicatorFieldName] != null &&
                    !kDefaultResponseIndicator.Equals(((dynamic)rfcParameters[kRFCResponseIndicatorFieldName]).Value))
                {
                    throw new InvalidOperationException(
                        string.Format("The SAP RFC {0} has completed the request with code {1}. {2}",
                            kRFCNameMeasurementDocument,
                            ((dynamic)rfcParameters[kRFCResponseIndicatorFieldName]).Value,
                            ((dynamic)rfcParameters[kRFCResponseDetailFieldName]).Value)
                    );
                }

                Logger.Log(new Foundation.Entity.LogEntry
                {
                    Type = LOG_TYPE.LOG_TYPE_MESSAGE,
                    Description = string.Format("The SAP RFC {0} has completed the request with code {1}. {2}",
                            kRFCNameMasterData,
                            ((dynamic)rfcParameters[kRFCResponseIndicatorFieldName]).Value,
                            ((dynamic)rfcParameters[kRFCResponseDetailFieldName]).Value),
                    App = Context.SecurityContext.AppID,
                    Device = Context.SecurityContext.DeviceID,
                    Module = "SAPDataProvider.SubmitWorkOrderActivity",
                    User = Context.SecurityContext.ClientID
                });

                return rfcParameters;
            }
            catch (Exception ex)
            {
                Logger.Log(new Foundation.Entity.LogEntry
                {
                    Type = LOG_TYPE.LOG_TYPE_ERROR,
                    Description = ex.Message,
                    App = Context.SecurityContext.AppID,
                    Device = Context.SecurityContext.DeviceID,
                    Module = "SAPDataProvider.SubmitWorkOrderActivity",
                    User = Context.SecurityContext.ClientID
                });

                throw ex;
            }
        }

        public Dictionary<string, object> CreateAdvice(Dictionary<string, object> parameters)
        {
            try
            {
                SOFTTEK.SAP.Integration.SAPConnection sapConnector = new SAP.Integration.SAPConnection();
                var sapDestination = sapConnector.LoadDestination();

                Dictionary<string, object> rfcInputParameters = new Dictionary<string, object>();
                rfcInputParameters["IM_NOTIFHEADER"] = SAPData.CreateRFCStructure(
                    parameters,
                    sapDestination,
                    "ZPMS_AVISOS_MANTENIMIENTO"
                );
                rfcInputParameters["IM_NOTIF_TYPE"] = "M1";

                Dictionary<string, object> rfcParameters = RetrieveResponseData<object>(kRFCNameCreateAdvice, rfcInputParameters);

                // Validate RFC's response
                if (rfcParameters[kRFCResponseIndicatorFieldName] != null &&
                    !kDefaultResponseIndicator.Equals(((dynamic)rfcParameters[kRFCResponseIndicatorFieldName]).Value))
                {
                    throw new InvalidOperationException(
                        string.Format("The SAP RFC {0} has completed the request with code {1}. {2}",
                            kRFCNameMeasurementDocument,
                            ((dynamic)rfcParameters[kRFCResponseIndicatorFieldName]).Value,
                            ((dynamic)rfcParameters[kRFCResponseDetailFieldName]).Value)
                    );
                }

                Logger.Log(new Foundation.Entity.LogEntry
                {
                    Type = LOG_TYPE.LOG_TYPE_MESSAGE,
                    Description = string.Format("The SAP RFC {0} has completed the request with code {1}. {2}",
                            kRFCNameMasterData,
                            ((dynamic)rfcParameters[kRFCResponseIndicatorFieldName]).Value,
                            ((dynamic)rfcParameters[kRFCResponseDetailFieldName]).Value),
                    App = Context.SecurityContext.AppID,
                    Device = Context.SecurityContext.DeviceID,
                    Module = "SAPDataProvider.CreateAdvice",
                    User = Context.SecurityContext.ClientID
                });

                return rfcParameters;
            }
            catch (Exception ex)
            {
                Logger.Log(new Foundation.Entity.LogEntry
                {
                    Type = LOG_TYPE.LOG_TYPE_ERROR,
                    Description = ex.Message,
                    App = Context.SecurityContext.AppID,
                    Device = Context.SecurityContext.DeviceID,
                    Module = "SAPDataProvider.CreateAdvice",
                    User = Context.SecurityContext.ClientID
                });

                throw ex;
            }
        }

        /// <summary>
        /// Invokes the SAP RFC and performs the mapping of the response to elements of type K.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <param name="mapper"> delegated function  responsible for mapping the output elements into
        /// instances of type K with the expected values of:
        /// <para/>- LLAVE
        /// <para/>- CATEGORIA
        /// <para/>- DESCRIPCION
        /// </param>
        /// <returns></returns>
        public Dictionary<string, object> GetMasterDataFromRFC<K>(Func<string, Func<Dictionary<string, object>, K>> mapper)
        {
            try
            {
                Dictionary<string, object> rfcParameters = RetrieveResponseData(kRFCNameMasterData, null, mapper);

                // Validate RFC's response
                if (rfcParameters[kRFCResponseIndicatorFieldName] != null &&
                    !kDefaultResponseIndicator.Equals(((dynamic)rfcParameters[kRFCResponseIndicatorFieldName]).Value))
                {
                    throw new InvalidOperationException(
                        string.Format("The SAP RFC {0} has completed the request with code {1}. {2}",
                            kRFCNameMasterData,
                            ((dynamic)rfcParameters[kRFCResponseIndicatorFieldName]).Value,
                            ((dynamic)rfcParameters[kRFCResponseDetailFieldName]).Value)
                    );
                }

                Logger.Log(new Foundation.Entity.LogEntry
                {
                    Type = LOG_TYPE.LOG_TYPE_MESSAGE,
                    Description = string.Format("The SAP RFC {0} has completed the request with code {1}. {2}",
                            kRFCNameMasterData,
                            ((dynamic)rfcParameters[kRFCResponseIndicatorFieldName]).Value,
                            ((dynamic)rfcParameters[kRFCResponseDetailFieldName]).Value),
                    App = Context.SecurityContext.AppID,
                    Device = Context.SecurityContext.DeviceID,
                    Module = "SAPDataProvider.GetMasterDataFromRFC",
                    User = Context.SecurityContext.ClientID
                });

                return rfcParameters;
            }
            catch (Exception ex)
            {
                Logger.Log(new Foundation.Entity.LogEntry
                {
                    Type = LOG_TYPE.LOG_TYPE_ERROR,
                    Description = ex.Message,
                    App = Context.SecurityContext.AppID,
                    Device = Context.SecurityContext.DeviceID,
                    Module = "SAPDataProvider.GetMasterDataFromRFC",
                    User = Context.SecurityContext.ClientID
                });

                throw ex;
            }
        }

        public Dictionary<string, object> GetWorkOrdersInPeriod<K>(Dictionary<string, object> parameters, Func<string, Func<Dictionary<string, object>, K>> mapper)
        {
            try
            {
                Dictionary<string, object> rfcParameters = RetrieveResponseData(kRFCNameWorkOrders, null, mapper);

                // Validate RFC's response
                if (rfcParameters[kRFCResponseIndicatorFieldName] != null &&
                    !kDefaultResponseIndicator.Equals(((dynamic)rfcParameters[kRFCResponseIndicatorFieldName]).Value))
                {
                    throw new InvalidOperationException(
                        string.Format("The SAP RFC {0} has completed the request with code {1}. {2}",
                            kRFCNameMasterData,
                            ((dynamic)rfcParameters[kRFCResponseIndicatorFieldName]).Value,
                            ((dynamic)rfcParameters[kRFCResponseDetailFieldName]).Value)
                    );
                }

                Logger.Log(new Foundation.Entity.LogEntry
                {
                    Type = LOG_TYPE.LOG_TYPE_MESSAGE,
                    Description = string.Format("The SAP RFC {0} has completed the request with code {1}. {2}",
                            kRFCNameMasterData,
                            ((dynamic)rfcParameters[kRFCResponseIndicatorFieldName]).Value,
                            ((dynamic)rfcParameters[kRFCResponseDetailFieldName]).Value),
                    App = Context.SecurityContext.AppID,
                    Device = Context.SecurityContext.DeviceID,
                    Module = "SAPDataProvider.GetWorkOrdersInPeriod",
                    User = Context.SecurityContext.ClientID
                });

                return rfcParameters;
            }
            catch (Exception ex)
            {
                Logger.Log(new Foundation.Entity.LogEntry
                {
                    Type = LOG_TYPE.LOG_TYPE_ERROR,
                    Description = ex.Message,
                    App = Context.SecurityContext.AppID,
                    Device = Context.SecurityContext.DeviceID,
                    Module = "SAPDataProvider.GetWorkOrdersInPeriod",
                    User = Context.SecurityContext.ClientID
                });

                throw ex;
            }
        }

        public Dictionary<string, object> GetTecnicalObjectsRFC<K>(Func<string, Func<Dictionary<string, object>, K>> mapper)
        {
            try
            {
                Dictionary<string, object> rfcParameters = RetrieveResponseData(kRFCNameTechnicalObject, null, mapper);

                // Validate RFC's response
                if (rfcParameters[kRFCResponseIndicatorFieldName] != null &&
                    !kDefaultResponseIndicator.Equals(((dynamic)rfcParameters[kRFCResponseIndicatorFieldName]).Value))
                {
                    throw new InvalidOperationException(
                        string.Format("The SAP RFC {0} has completed the request with code {1}. {2}",
                            kRFCNameMasterData,
                            ((dynamic)rfcParameters[kRFCResponseIndicatorFieldName]).Value,
                            ((dynamic)rfcParameters[kRFCResponseDetailFieldName]).Value)
                    );
                }

                Logger.Log(new Foundation.Entity.LogEntry
                {
                    Type = LOG_TYPE.LOG_TYPE_MESSAGE,
                    Description = string.Format("The SAP RFC {0} has completed the request with code {1}. {2}",
                            kRFCNameMasterData,
                            ((dynamic)rfcParameters[kRFCResponseIndicatorFieldName]).Value,
                            ((dynamic)rfcParameters[kRFCResponseDetailFieldName]).Value),
                    App = Context.SecurityContext.AppID,
                    Device = Context.SecurityContext.DeviceID,
                    Module = "SAPDataProvider.GetTecnicalObjectsRFC",
                    User = Context.SecurityContext.ClientID
                });

                return rfcParameters;
            }
            catch (Exception ex)
            {
                Logger.Log(new Foundation.Entity.LogEntry
                {
                    Type = LOG_TYPE.LOG_TYPE_ERROR,
                    Description = ex.Message,
                    App = Context.SecurityContext.AppID,
                    Device = Context.SecurityContext.DeviceID,
                    Module = "SAPDataProvider.GetTecnicalObjectsRFC",
                    User = Context.SecurityContext.ClientID
                });

                throw ex;
            }
        }
    }
}