using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SCMS.Foundation.Security;
using SOFTTEK.SCMS.Foundation.Data;
using SOFTTEK.SCMS.Entity;

namespace SOFTTEK.SCMS.Data
{
    
    public class FAMDataContext : DataContext, IDisposable
    {
        #region Fields
        /// <summary>
        /// Get or Set the instance of the Data Context Data Provider
        /// </summary>
        private SOFTTEK.SCMS.Foundation.Data.DataProvider dataProvider;
        #endregion

        #region Propierties
        /// <summary>
        /// Get or Set the string that represents the connection parameters for the Data Context Instance
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Get or Set the Provider Identifier of the Data Provider to instance at the initialization of the Data Context
        /// </summary>
        public string ProviderName { get; private set; }

        /// <summary>
        /// Get or Set The Default DB user for the Data Provider
        /// </summary>
        public string DefaultUser { private get; set; }
        #endregion

        #region Configuration
        /// <summary>
        /// Default Constructor for the instance of the Data Context
        /// <param name="securityContext">Security Context instance to be used by the Data Context instance</param>
        /// </summary>
        public FAMDataContext(SecurityContext securityContext)
            : base(securityContext)
        {

        }

        /// <summary>
        /// Perform the initialization routine for the Data Context Instance, creating an instance for the Data Provider.
        /// </summary>
        public void Initialize()
        {
            if (string.IsNullOrEmpty(ConnectionString))
            {
                throw new Exception("ConnectionString is required.", new ArgumentException("ConnectionString cannot be null."));
            }
            ProviderName = System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionString].ProviderName;
            switch (ProviderName)
            {
                case "System.Data.SqlClient":
                    dataProvider = new SOFTTEK.SCMS.Foundation.Data.DataProviders.MSSQLDataProvider();
                    dataProvider.ConnectionString = ConnectionString;
                    dataProvider.Context = this;
                    break;
                case "System.Data.OracleClient":
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Release the instance´s resources
        /// </summary>
        public void Dispose()
        {
            dataProvider.Dispose();
            Dispose(false);
        }

        /// <summary>
        /// Implementation of the Dispose Pattern for the disposable instance of the Data Context by releasing all its disposable resources.
        /// <param name="disposing">Flag that indicates the current disposing status for the Data Context instance</param>
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Dispose();
            }
            dataProvider = null;
        }
        #endregion

        #region Accessors

        #region AvailabilityForecast

        /// <summary>
        /// Retrieves a list of AvailabilityForecast instances that matches a given filter criteria by the AvailabilityForecast object instance 
        /// </summary>
        /// <param name="filter">Object with the information required to filter the set of results</param>
        /// <returns>List of AvailabilityForecast instances that matches the filter.</returns>
        public List<SOFTTEK.SCMS.Entity.FA.AvailabilityForecast> GetAvailabilityForecasts(SOFTTEK.SCMS.Entity.FA.AvailabilityForecast filter)
        {
            Func<SOFTTEK.SCMS.Entity.FA.AvailabilityForecast, List<System.Data.IDataParameter>> filterDelegate = (f) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
                //Token
                System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p_tok.Direction = System.Data.ParameterDirection.Input;
                p_tok.DbType = System.Data.DbType.String;
                parameters.Add(p_tok);
                //Identifier
                System.Data.IDataParameter p_filter_avlfrc_id_Pk = dataProvider.GetParameter("@filter_avlfrc_id_Pk",
                    f.Identifier == default(System.Int64) ?
                        (object)System.DBNull.Value :
                        (object)f.Identifier);
                p_filter_avlfrc_id_Pk.Direction = System.Data.ParameterDirection.Input;
                p_filter_avlfrc_id_Pk.DbType = DbType.Int64;
                parameters.Add(p_filter_avlfrc_id_Pk);
                //Request
                System.Data.IDataParameter p_filter_avlfrc_request_Fk = dataProvider.GetParameter("@filter_avlfrc_request_Fk",
                    f.Request == default(SOFTTEK.SCMS.Entity.FA.Request) ?
                        (object)System.DBNull.Value :
                        (object)f.Request.Identifier);
                p_filter_avlfrc_request_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_avlfrc_request_Fk.DbType = DbType.Int64;
                parameters.Add(p_filter_avlfrc_request_Fk);
                //ValidatedBy
                System.Data.IDataParameter p_filter_avlfrc_validated_by_Fk = dataProvider.GetParameter("@filter_avlfrc_validated_by_Fk",
                    f.ValidatedBy == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.ValidatedBy.Identifier);
                p_filter_avlfrc_validated_by_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_avlfrc_validated_by_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_avlfrc_validated_by_Fk);
                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.FA.AvailabilityForecast> mapper = (reader) =>
            {
                long longHelper = default(long);
                int intHelper = default(int);
                DateTime dateHelper = default(DateTime);

                SOFTTEK.SCMS.Entity.FA.AvailabilityForecast value = new SOFTTEK.SCMS.Entity.FA.AvailabilityForecast
                {
                    Identifier = reader["avlfrc_id_Pk"] != null ?
                        (long.TryParse(reader["avlfrc_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    Request = reader["avlfrc_request_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.FA.Request { Identifier = long.TryParse(reader["avlfrc_request_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null,
                    GeneratedAt = reader["avlfrc_generated_at"] != null ?
                        (DateTime.TryParse(reader["avlfrc_generated_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                    ValidUntil = reader["avlfrc_valid_until"] != null ?
                        (DateTime.TryParse(reader["avlfrc_valid_until"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                    ValidatedBy = reader["avlfrc_validated_by_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["avlfrc_validated_by_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                };
                return value;
            };

            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            List<SOFTTEK.SCMS.Entity.FA.AvailabilityForecast> result = dataProvider.ExecuteReaderWithFilter(SCMS.FA_SP_Select_AvailabilityForecast, filterDelegate, filter, mapper);
            return result;
        }

        /// <summary>
        /// Inserts the instance of the provided AvailabilityForecast at the Data Context datasource  
        /// </summary>
        /// <param name="instance">Instance of AvailabilityForecast with the entity´s information to store</param>
        /// <returns>Instance of AvailabilityForecast with the aditional information provided by the Data Context datasource</returns>
        public SOFTTEK.SCMS.Entity.FA.AvailabilityForecast InsertAvailabilityForecast(SOFTTEK.SCMS.Entity.FA.AvailabilityForecast instance)
        {
            Func<SOFTTEK.SCMS.Entity.FA.AvailabilityForecast, List<System.Data.IDataParameter>> input = (i) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();

                //Token
                System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p_tok.Direction = System.Data.ParameterDirection.Input;
                p_tok.DbType = System.Data.DbType.String;
                parameters.Add(p_tok);

                // Insert
                //Request
                System.Data.IDataParameter p_new_avlfrc_request_Fk = dataProvider.GetParameter("@new_avlfrc_request_Fk",
                    i.Request == default(SOFTTEK.SCMS.Entity.FA.Request) ?
                        (object)System.DBNull.Value :
                        (object)i.Request.Identifier);
                p_new_avlfrc_request_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_avlfrc_request_Fk.DbType = DbType.Int64;
                parameters.Add(p_new_avlfrc_request_Fk);
                //ValidUntil
                System.Data.IDataParameter p_new_avlfrc_valid_until = dataProvider.GetParameter("@new_avlfrc_valid_until",
                    i.ValidUntil == default(System.DateTime) ?
                        (object)System.DBNull.Value :
                        (object)i.ValidUntil);
                p_new_avlfrc_valid_until.Direction = System.Data.ParameterDirection.Input;
                p_new_avlfrc_valid_until.DbType = DbType.DateTime;
                parameters.Add(p_new_avlfrc_valid_until);
                //ValidatedBy
                System.Data.IDataParameter p_new_avlfrc_validated_by_Fk = dataProvider.GetParameter("@new_avlfrc_validated_by_Fk",
                    i.ValidatedBy == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)i.ValidatedBy.Identifier);
                p_new_avlfrc_validated_by_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_avlfrc_validated_by_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_avlfrc_validated_by_Fk);

                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.FA.AvailabilityForecast> mapper = (reader) =>
            {
                long longHelper = default(long);
                int intHelper = default(int);
                DateTime dateHelper = default(DateTime);

                SOFTTEK.SCMS.Entity.FA.AvailabilityForecast value = new SOFTTEK.SCMS.Entity.FA.AvailabilityForecast
                {
                    Identifier = reader["v_avlfrc_id_Pk"] != null ?
                        (long.TryParse(reader["v_avlfrc_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    Request = reader["v_avlfrc_request_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.FA.Request { Identifier = long.TryParse(reader["v_avlfrc_request_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null,
                    GeneratedAt = reader["v_avlfrc_generated_at"] != null ?
                        (DateTime.TryParse(reader["v_avlfrc_generated_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                    ValidUntil = reader["v_avlfrc_valid_until"] != null ?
                        (DateTime.TryParse(reader["v_avlfrc_valid_until"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                    ValidatedBy = reader["v_avlfrc_validated_by_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_avlfrc_validated_by_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                };
                return value;
            };


            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            SOFTTEK.SCMS.Entity.FA.AvailabilityForecast result = dataProvider.ExecuteForEntityWithFilter(SCMS.FA_SP_Insert_AvailabilityForecast, input, instance, mapper);
            return result;
        }

        /// <summary>
        /// Update the entity's stored information with that provided by the instance of AvailabilityForecast at the Data Context datasource  
        /// </summary>
        /// <param name="instance">Instance of AvailabilityForecast with the entity´s information to update</param>
        /// <param name="filter">Object with the information required to filter the target stored entity´s information</param>
        /// <returns>Instance of AvailabilityForecast with the updated information provided by the Data Context datasource</returns>
        public SOFTTEK.SCMS.Entity.FA.AvailabilityForecast UpdateAvailabilityForecast(SOFTTEK.SCMS.Entity.FA.AvailabilityForecast instance, SOFTTEK.SCMS.Entity.FA.AvailabilityForecast filter)
        {
            Func<SOFTTEK.SCMS.Entity.FA.AvailabilityForecast, List<System.Data.IDataParameter>> filterDelegate = (f) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();

                //Token
                System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p_tok.Direction = System.Data.ParameterDirection.Input;
                p_tok.DbType = System.Data.DbType.String;
                parameters.Add(p_tok);

                // Filter
                //Identifier
                System.Data.IDataParameter p_filter_avlfrc_id_Pk = dataProvider.GetParameter("@filter_avlfrc_id_Pk",
                    f.Identifier == default(System.Int64) ?
                        (object)System.DBNull.Value :
                        (object)f.Identifier);
                p_filter_avlfrc_id_Pk.Direction = System.Data.ParameterDirection.Input;
                p_filter_avlfrc_id_Pk.DbType = DbType.Int64;
                parameters.Add(p_filter_avlfrc_id_Pk);
                //Request
                System.Data.IDataParameter p_filter_avlfrc_request_Fk = dataProvider.GetParameter("@filter_avlfrc_request_Fk",
                    f.Request == default(SOFTTEK.SCMS.Entity.FA.Request) ?
                        (object)System.DBNull.Value :
                        (object)f.Request.Identifier);
                p_filter_avlfrc_request_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_avlfrc_request_Fk.DbType = DbType.Int64;
                parameters.Add(p_filter_avlfrc_request_Fk);
                //ValidatedBy
                System.Data.IDataParameter p_filter_avlfrc_validated_by_Fk = dataProvider.GetParameter("@filter_avlfrc_validated_by_Fk",
                    f.ValidatedBy == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.ValidatedBy.Identifier);
                p_filter_avlfrc_validated_by_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_avlfrc_validated_by_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_avlfrc_validated_by_Fk);
                // Update
                //Request
                System.Data.IDataParameter p_new_avlfrc_request_Fk = dataProvider.GetParameter("@new_avlfrc_request_Fk",
                    instance.Request == default(SOFTTEK.SCMS.Entity.FA.Request) ?
                        (object)System.DBNull.Value :
                        (object)instance.Request.Identifier);
                p_new_avlfrc_request_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_avlfrc_request_Fk.DbType = DbType.Int64;
                parameters.Add(p_new_avlfrc_request_Fk);
                //ValidUntil
                System.Data.IDataParameter p_new_avlfrc_valid_until = dataProvider.GetParameter("@new_avlfrc_valid_until",
                    f.ValidUntil == default(System.DateTime) ?
                        (object)System.DBNull.Value :
                        (object)instance.ValidUntil);
                p_new_avlfrc_valid_until.Direction = System.Data.ParameterDirection.Input;
                p_new_avlfrc_valid_until.DbType = DbType.DateTime;
                parameters.Add(p_new_avlfrc_valid_until);
                //ValidatedBy
                System.Data.IDataParameter p_new_avlfrc_validated_by_Fk = dataProvider.GetParameter("@new_avlfrc_validated_by_Fk",
                    instance.ValidatedBy == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)instance.ValidatedBy.Identifier);
                p_new_avlfrc_validated_by_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_avlfrc_validated_by_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_avlfrc_validated_by_Fk);

                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.FA.AvailabilityForecast> mapper = (reader) =>
            {
                long longHelper = default(long);
                int intHelper = default(int);
                DateTime dateHelper = default(DateTime);

                SOFTTEK.SCMS.Entity.FA.AvailabilityForecast value = new SOFTTEK.SCMS.Entity.FA.AvailabilityForecast
                {
                    Identifier = reader["v_avlfrc_id_Pk"] != null ?
                        (long.TryParse(reader["v_avlfrc_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    Request = reader["v_avlfrc_request_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.FA.Request { Identifier = long.TryParse(reader["v_avlfrc_request_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null,
                    GeneratedAt = reader["v_avlfrc_generated_at"] != null ?
                        (DateTime.TryParse(reader["v_avlfrc_generated_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                    ValidUntil = reader["v_avlfrc_valid_until"] != null ?
                        (DateTime.TryParse(reader["v_avlfrc_valid_until"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                    ValidatedBy = reader["v_avlfrc_validated_by_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_avlfrc_validated_by_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                };
                return value;
            };


            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            SOFTTEK.SCMS.Entity.FA.AvailabilityForecast result = dataProvider.ExecuteForEntityWithFilter(SCMS.FA_SP_Update_AvailabilityForecast, filterDelegate, filter, mapper);
            return result;
        }
        #endregion
        #region AvailabilityForecastItem

        /// <summary>
        /// Retrieves a list of AvailabilityForecastItem instances that matches a given filter criteria by the AvailabilityForecastItem object instance 
        /// </summary>
        /// <param name="filter">Object with the information required to filter the set of results</param>
        /// <returns>List of AvailabilityForecastItem instances that matches the filter.</returns>
        public List<SOFTTEK.SCMS.Entity.FA.AvailabilityForecastItem> GetAvailabilityForecastItems(SOFTTEK.SCMS.Entity.FA.AvailabilityForecastItem filter)
        {
            Func<SOFTTEK.SCMS.Entity.FA.AvailabilityForecastItem, List<System.Data.IDataParameter>> filterDelegate = (f) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
                //Token
                System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p_tok.Direction = System.Data.ParameterDirection.Input;
                p_tok.DbType = System.Data.DbType.String;
                parameters.Add(p_tok);
                //Identifier
                System.Data.IDataParameter p_filter_avlfrcitm_id_Pk = dataProvider.GetParameter("@filter_avlfrcitm_id_Pk",
                    f.Identifier == default(System.Int64) ?
                        (object)System.DBNull.Value :
                        (object)f.Identifier);
                p_filter_avlfrcitm_id_Pk.Direction = System.Data.ParameterDirection.Input;
                p_filter_avlfrcitm_id_Pk.DbType = DbType.Int64;
                parameters.Add(p_filter_avlfrcitm_id_Pk);
                //Status
                System.Data.IDataParameter p_filter_avlfrcitm_status = dataProvider.GetParameter("@filter_avlfrcitm_status",
                    f.Status == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Status);
                p_filter_avlfrcitm_status.Direction = System.Data.ParameterDirection.Input;
                p_filter_avlfrcitm_status.DbType = DbType.String;
                parameters.Add(p_filter_avlfrcitm_status);
                //AvailabilityForecast
                System.Data.IDataParameter p_filter_avlfrcitm_availability_forecast_Fk = dataProvider.GetParameter("@filter_avlfrcitm_availability_forecast_Fk",
                    f.AvailabilityForecast == default(SOFTTEK.SCMS.Entity.FA.AvailabilityForecast) ?
                        (object)System.DBNull.Value :
                        (object)f.AvailabilityForecast.Identifier);
                p_filter_avlfrcitm_availability_forecast_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_avlfrcitm_availability_forecast_Fk.DbType = DbType.Int64;
                parameters.Add(p_filter_avlfrcitm_availability_forecast_Fk);
                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.FA.AvailabilityForecastItem> mapper = (reader) =>
            {
                long longHelper = default(long);
                int intHelper = default(int);
                DateTime dateHelper = default(DateTime);

                SOFTTEK.SCMS.Entity.FA.AvailabilityForecastItem value = new SOFTTEK.SCMS.Entity.FA.AvailabilityForecastItem
                {
                    Identifier = reader["avlfrcitm_id_Pk"] != null ?
                        (long.TryParse(reader["avlfrcitm_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    FixedAsset = reader["avlfrcitm_fixed_asset_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.FA.FixedAsset { Identifier = long.TryParse(reader["avlfrcitm_fixed_asset_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null,
                    Stock = reader["avlfrcitm_stock"] != null ?
                        (int.TryParse(reader["avlfrcitm_stock"].ToString(), out intHelper) ? intHelper : -1) : -1,
                    Status = reader["avlfrcitm_status"] != null ?
                        reader["avlfrcitm_status"].ToString() : string.Empty,
                    From = reader["avlfrcitm_from"] != null ?
                        (DateTime.TryParse(reader["avlfrcitm_from"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                    To = reader["avlfrcitm_to"] != null ?
                        (DateTime.TryParse(reader["avlfrcitm_to"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                    AvailabilityForecast = reader["avlfrcitm_availability_forecast_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.FA.AvailabilityForecast { Identifier = long.TryParse(reader["avlfrcitm_availability_forecast_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null,
                };
                return value;
            };

            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            List<SOFTTEK.SCMS.Entity.FA.AvailabilityForecastItem> result = dataProvider.ExecuteReaderWithFilter(SCMS.FA_SP_Select_AvailabilityForecastItem, filterDelegate, filter, mapper);
            return result;
        }

        /// <summary>
        /// Inserts the instance of the provided AvailabilityForecastItem at the Data Context datasource  
        /// </summary>
        /// <param name="instance">Instance of AvailabilityForecastItem with the entity´s information to store</param>
        /// <returns>Instance of AvailabilityForecastItem with the aditional information provided by the Data Context datasource</returns>
        public SOFTTEK.SCMS.Entity.FA.AvailabilityForecastItem InsertAvailabilityForecastItem(SOFTTEK.SCMS.Entity.FA.AvailabilityForecastItem instance)
        {
            Func<SOFTTEK.SCMS.Entity.FA.AvailabilityForecastItem, List<System.Data.IDataParameter>> input = (i) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();

                //Token
                System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p_tok.Direction = System.Data.ParameterDirection.Input;
                p_tok.DbType = System.Data.DbType.String;
                parameters.Add(p_tok);

                // Insert
                //FixedAsset
                System.Data.IDataParameter p_new_avlfrcitm_fixed_asset_Fk = dataProvider.GetParameter("@new_avlfrcitm_fixed_asset_Fk",
                    i.FixedAsset == default(SOFTTEK.SCMS.Entity.FA.FixedAsset) ?
                        (object)System.DBNull.Value :
                        (object)i.FixedAsset.Identifier);
                p_new_avlfrcitm_fixed_asset_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_avlfrcitm_fixed_asset_Fk.DbType = DbType.Int64;
                parameters.Add(p_new_avlfrcitm_fixed_asset_Fk);
                //Stock
                System.Data.IDataParameter p_new_avlfrcitm_stock = dataProvider.GetParameter("@new_avlfrcitm_stock",
                    i.Stock == default(System.Int32) ?
                        (object)System.DBNull.Value :
                        (object)i.Stock);
                p_new_avlfrcitm_stock.Direction = System.Data.ParameterDirection.Input;
                p_new_avlfrcitm_stock.DbType = DbType.Int32;
                parameters.Add(p_new_avlfrcitm_stock);
                //Status
                System.Data.IDataParameter p_new_avlfrcitm_status = dataProvider.GetParameter("@new_avlfrcitm_status",
                    i.Status == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Status);
                p_new_avlfrcitm_status.Direction = System.Data.ParameterDirection.Input;
                p_new_avlfrcitm_status.DbType = DbType.String;
                parameters.Add(p_new_avlfrcitm_status);
                //From
                System.Data.IDataParameter p_new_avlfrcitm_from = dataProvider.GetParameter("@new_avlfrcitm_from",
                    i.From == default(System.DateTime) ?
                        (object)System.DBNull.Value :
                        (object)i.From);
                p_new_avlfrcitm_from.Direction = System.Data.ParameterDirection.Input;
                p_new_avlfrcitm_from.DbType = DbType.DateTime;
                parameters.Add(p_new_avlfrcitm_from);
                //To
                System.Data.IDataParameter p_new_avlfrcitm_to = dataProvider.GetParameter("@new_avlfrcitm_to",
                    i.To == default(System.DateTime) ?
                        (object)System.DBNull.Value :
                        (object)i.To);
                p_new_avlfrcitm_to.Direction = System.Data.ParameterDirection.Input;
                p_new_avlfrcitm_to.DbType = DbType.DateTime;
                parameters.Add(p_new_avlfrcitm_to);
                //AvailabilityForecast
                System.Data.IDataParameter p_new_avlfrcitm_availability_forecast_Fk = dataProvider.GetParameter("@new_avlfrcitm_availability_forecast_Fk",
                    i.AvailabilityForecast == default(SOFTTEK.SCMS.Entity.FA.AvailabilityForecast) ?
                        (object)System.DBNull.Value :
                        (object)i.AvailabilityForecast.Identifier);
                p_new_avlfrcitm_availability_forecast_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_avlfrcitm_availability_forecast_Fk.DbType = DbType.Int64;
                parameters.Add(p_new_avlfrcitm_availability_forecast_Fk);

                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.FA.AvailabilityForecastItem> mapper = (reader) =>
            {
                long longHelper = default(long);
                int intHelper = default(int);
                DateTime dateHelper = default(DateTime);

                SOFTTEK.SCMS.Entity.FA.AvailabilityForecastItem value = new SOFTTEK.SCMS.Entity.FA.AvailabilityForecastItem
                {
                    Identifier = reader["v_avlfrcitm_id_Pk"] != null ?
                        (long.TryParse(reader["v_avlfrcitm_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    FixedAsset = reader["v_avlfrcitm_fixed_asset_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.FA.FixedAsset { Identifier = long.TryParse(reader["v_avlfrcitm_fixed_asset_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null,
                    Stock = reader["v_avlfrcitm_stock"] != null ?
                        (int.TryParse(reader["v_avlfrcitm_stock"].ToString(), out intHelper) ? intHelper : -1) : -1,
                    Status = reader["v_avlfrcitm_status"] != null ?
                        reader["v_avlfrcitm_status"].ToString() : string.Empty,
                    From = reader["v_avlfrcitm_from"] != null ?
                        (DateTime.TryParse(reader["v_avlfrcitm_from"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                    To = reader["v_avlfrcitm_to"] != null ?
                        (DateTime.TryParse(reader["v_avlfrcitm_to"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                    AvailabilityForecast = reader["v_avlfrcitm_availability_forecast_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.FA.AvailabilityForecast { Identifier = long.TryParse(reader["v_avlfrcitm_availability_forecast_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null,
                };
                return value;
            };


            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            SOFTTEK.SCMS.Entity.FA.AvailabilityForecastItem result = dataProvider.ExecuteForEntityWithFilter(SCMS.FA_SP_Insert_AvailabilityForecastItem, input, instance, mapper);
            return result;
        }

        /// <summary>
        /// Update the entity's stored information with that provided by the instance of AvailabilityForecastItem at the Data Context datasource  
        /// </summary>
        /// <param name="instance">Instance of AvailabilityForecastItem with the entity´s information to update</param>
        /// <param name="filter">Object with the information required to filter the target stored entity´s information</param>
        /// <returns>Instance of AvailabilityForecastItem with the updated information provided by the Data Context datasource</returns>
        public SOFTTEK.SCMS.Entity.FA.AvailabilityForecastItem UpdateAvailabilityForecastItem(SOFTTEK.SCMS.Entity.FA.AvailabilityForecastItem instance, SOFTTEK.SCMS.Entity.FA.AvailabilityForecastItem filter)
        {
            Func<SOFTTEK.SCMS.Entity.FA.AvailabilityForecastItem, List<System.Data.IDataParameter>> filterDelegate = (f) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();

                //Token
                System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p_tok.Direction = System.Data.ParameterDirection.Input;
                p_tok.DbType = System.Data.DbType.String;
                parameters.Add(p_tok);

                // Filter
                //Identifier
                System.Data.IDataParameter p_filter_avlfrcitm_id_Pk = dataProvider.GetParameter("@filter_avlfrcitm_id_Pk",
                    f.Identifier == default(System.Int64) ?
                        (object)System.DBNull.Value :
                        (object)f.Identifier);
                p_filter_avlfrcitm_id_Pk.Direction = System.Data.ParameterDirection.Input;
                p_filter_avlfrcitm_id_Pk.DbType = DbType.Int64;
                parameters.Add(p_filter_avlfrcitm_id_Pk);
                //Status
                System.Data.IDataParameter p_filter_avlfrcitm_status = dataProvider.GetParameter("@filter_avlfrcitm_status",
                    f.Status == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Status);
                p_filter_avlfrcitm_status.Direction = System.Data.ParameterDirection.Input;
                p_filter_avlfrcitm_status.DbType = DbType.String;
                parameters.Add(p_filter_avlfrcitm_status);
                //AvailabilityForecast
                System.Data.IDataParameter p_filter_avlfrcitm_availability_forecast_Fk = dataProvider.GetParameter("@filter_avlfrcitm_availability_forecast_Fk",
                    f.AvailabilityForecast == default(SOFTTEK.SCMS.Entity.FA.AvailabilityForecast) ?
                        (object)System.DBNull.Value :
                        (object)f.AvailabilityForecast.Identifier);
                p_filter_avlfrcitm_availability_forecast_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_avlfrcitm_availability_forecast_Fk.DbType = DbType.Int64;
                parameters.Add(p_filter_avlfrcitm_availability_forecast_Fk);
                // Update
                //FixedAsset
                System.Data.IDataParameter p_new_avlfrcitm_fixed_asset_Fk = dataProvider.GetParameter("@new_avlfrcitm_fixed_asset_Fk",
                    instance.FixedAsset == default(SOFTTEK.SCMS.Entity.FA.FixedAsset) ?
                        (object)System.DBNull.Value :
                        (object)instance.FixedAsset.Identifier);
                p_new_avlfrcitm_fixed_asset_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_avlfrcitm_fixed_asset_Fk.DbType = DbType.Int64;
                parameters.Add(p_new_avlfrcitm_fixed_asset_Fk);
                //Stock
                System.Data.IDataParameter p_new_avlfrcitm_stock = dataProvider.GetParameter("@new_avlfrcitm_stock",
                    f.Stock == default(System.Int32) ?
                        (object)System.DBNull.Value :
                        (object)instance.Stock);
                p_new_avlfrcitm_stock.Direction = System.Data.ParameterDirection.Input;
                p_new_avlfrcitm_stock.DbType = DbType.Int32;
                parameters.Add(p_new_avlfrcitm_stock);
                //Status
                System.Data.IDataParameter p_new_avlfrcitm_status = dataProvider.GetParameter("@new_avlfrcitm_status",
                    f.Status == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Status);
                p_new_avlfrcitm_status.Direction = System.Data.ParameterDirection.Input;
                p_new_avlfrcitm_status.DbType = DbType.String;
                parameters.Add(p_new_avlfrcitm_status);
                //From
                System.Data.IDataParameter p_new_avlfrcitm_from = dataProvider.GetParameter("@new_avlfrcitm_from",
                    f.From == default(System.DateTime) ?
                        (object)System.DBNull.Value :
                        (object)instance.From);
                p_new_avlfrcitm_from.Direction = System.Data.ParameterDirection.Input;
                p_new_avlfrcitm_from.DbType = DbType.DateTime;
                parameters.Add(p_new_avlfrcitm_from);
                //To
                System.Data.IDataParameter p_new_avlfrcitm_to = dataProvider.GetParameter("@new_avlfrcitm_to",
                    f.To == default(System.DateTime) ?
                        (object)System.DBNull.Value :
                        (object)instance.To);
                p_new_avlfrcitm_to.Direction = System.Data.ParameterDirection.Input;
                p_new_avlfrcitm_to.DbType = DbType.DateTime;
                parameters.Add(p_new_avlfrcitm_to);
                //AvailabilityForecast
                System.Data.IDataParameter p_new_avlfrcitm_availability_forecast_Fk = dataProvider.GetParameter("@new_avlfrcitm_availability_forecast_Fk",
                    instance.AvailabilityForecast == default(SOFTTEK.SCMS.Entity.FA.AvailabilityForecast) ?
                        (object)System.DBNull.Value :
                        (object)instance.AvailabilityForecast.Identifier);
                p_new_avlfrcitm_availability_forecast_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_avlfrcitm_availability_forecast_Fk.DbType = DbType.Int64;
                parameters.Add(p_new_avlfrcitm_availability_forecast_Fk);

                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.FA.AvailabilityForecastItem> mapper = (reader) =>
            {
                long longHelper = default(long);
                int intHelper = default(int);
                DateTime dateHelper = default(DateTime);

                SOFTTEK.SCMS.Entity.FA.AvailabilityForecastItem value = new SOFTTEK.SCMS.Entity.FA.AvailabilityForecastItem
                {
                    Identifier = reader["v_avlfrcitm_id_Pk"] != null ?
                        (long.TryParse(reader["v_avlfrcitm_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    FixedAsset = reader["v_avlfrcitm_fixed_asset_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.FA.FixedAsset { Identifier = long.TryParse(reader["v_avlfrcitm_fixed_asset_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null,
                    Stock = reader["v_avlfrcitm_stock"] != null ?
                        (int.TryParse(reader["v_avlfrcitm_stock"].ToString(), out intHelper) ? intHelper : -1) : -1,
                    Status = reader["v_avlfrcitm_status"] != null ?
                        reader["v_avlfrcitm_status"].ToString() : string.Empty,
                    From = reader["v_avlfrcitm_from"] != null ?
                        (DateTime.TryParse(reader["v_avlfrcitm_from"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                    To = reader["v_avlfrcitm_to"] != null ?
                        (DateTime.TryParse(reader["v_avlfrcitm_to"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                    AvailabilityForecast = reader["v_avlfrcitm_availability_forecast_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.FA.AvailabilityForecast { Identifier = long.TryParse(reader["v_avlfrcitm_availability_forecast_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null,
                };
                return value;
            };


            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            SOFTTEK.SCMS.Entity.FA.AvailabilityForecastItem result = dataProvider.ExecuteForEntityWithFilter(SCMS.FA_SP_Update_AvailabilityForecastItem, filterDelegate, filter, mapper);
            return result;
        }
        #endregion
        #region FixedAsset

        /// <summary>
        /// Retrieves a list of FixedAsset instances that matches a given filter criteria by the FixedAsset object instance 
        /// </summary>
        /// <param name="filter">Object with the information required to filter the set of results</param>
        /// <returns>List of FixedAsset instances that matches the filter.</returns>
        public List<SOFTTEK.SCMS.Entity.FA.FixedAsset> GetFixedAssets(SOFTTEK.SCMS.Entity.FA.FixedAsset filter)
        {
            Func<SOFTTEK.SCMS.Entity.FA.FixedAsset, List<System.Data.IDataParameter>> filterDelegate = (f) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
                //Token
                System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p_tok.Direction = System.Data.ParameterDirection.Input;
                p_tok.DbType = System.Data.DbType.String;
                parameters.Add(p_tok);
                //SerialNumber
                System.Data.IDataParameter p_filter_fxdast_serial_number = dataProvider.GetParameter("@filter_fxdast_serial_number",
                    f.SerialNumber == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.SerialNumber);
                p_filter_fxdast_serial_number.Direction = System.Data.ParameterDirection.Input;
                p_filter_fxdast_serial_number.DbType = DbType.String;
                parameters.Add(p_filter_fxdast_serial_number);
                //Identifier
                System.Data.IDataParameter p_filter_fxdast_id_Pk = dataProvider.GetParameter("@filter_fxdast_id_Pk",
                    f.Identifier == default(System.Int64) ?
                        (object)System.DBNull.Value :
                        (object)f.Identifier);
                p_filter_fxdast_id_Pk.Direction = System.Data.ParameterDirection.Input;
                p_filter_fxdast_id_Pk.DbType = DbType.Int64;
                parameters.Add(p_filter_fxdast_id_Pk);
                //ExternalIdentifier
                System.Data.IDataParameter p_filter_fxdast_external_identifier = dataProvider.GetParameter("@filter_fxdast_external_identifier",
                    f.ExternalIdentifier == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.ExternalIdentifier);
                p_filter_fxdast_external_identifier.Direction = System.Data.ParameterDirection.Input;
                p_filter_fxdast_external_identifier.DbType = DbType.String;
                parameters.Add(p_filter_fxdast_external_identifier);
                //Description
                System.Data.IDataParameter p_filter_fxdast_description = dataProvider.GetParameter("@filter_fxdast_description",
                    f.Description == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Description);
                p_filter_fxdast_description.Direction = System.Data.ParameterDirection.Input;
                p_filter_fxdast_description.DbType = DbType.String;
                parameters.Add(p_filter_fxdast_description);
                //Placement
                System.Data.IDataParameter p_filter_fxdast_placement = dataProvider.GetParameter("@filter_fxdast_placement",
                    f.Placement == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Placement);
                p_filter_fxdast_placement.Direction = System.Data.ParameterDirection.Input;
                p_filter_fxdast_placement.DbType = DbType.String;
                parameters.Add(p_filter_fxdast_placement);
                //PlannificationCenter
                System.Data.IDataParameter p_filter_fxdast_plannification_center = dataProvider.GetParameter("@filter_fxdast_plannification_center",
                    f.PlannificationCenter == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.PlannificationCenter);
                p_filter_fxdast_plannification_center.Direction = System.Data.ParameterDirection.Input;
                p_filter_fxdast_plannification_center.DbType = DbType.String;
                parameters.Add(p_filter_fxdast_plannification_center);
                //Area
                System.Data.IDataParameter p_filter_fxdast_area = dataProvider.GetParameter("@filter_fxdast_area",
                    f.Area == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Area);
                p_filter_fxdast_area.Direction = System.Data.ParameterDirection.Input;
                p_filter_fxdast_area.DbType = DbType.String;
                parameters.Add(p_filter_fxdast_area);
                //CostCenter
                System.Data.IDataParameter p_filter_fxdast_cost_center = dataProvider.GetParameter("@filter_fxdast_cost_center",
                    f.CostCenter == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.CostCenter);
                p_filter_fxdast_cost_center.Direction = System.Data.ParameterDirection.Input;
                p_filter_fxdast_cost_center.DbType = DbType.String;
                parameters.Add(p_filter_fxdast_cost_center);
                
                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.FA.FixedAsset> mapper = (reader) =>
            {
                long longHelper = default(long);

                SOFTTEK.SCMS.Entity.FA.FixedAsset value = new SOFTTEK.SCMS.Entity.FA.FixedAsset
                {
                    ImageUrl = reader["fxdast_image_url"] != null ?
                        reader["fxdast_image_url"].ToString() : string.Empty,
                    SerialNumber = reader["fxdast_serial_number"] != null ?
                        reader["fxdast_serial_number"].ToString() : string.Empty,
                    Identifier = reader["fxdast_id_Pk"] != null ?
                        (long.TryParse(reader["fxdast_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    ExternalIdentifier = reader["fxdast_external_identifier"] != null ?
                        reader["fxdast_external_identifier"].ToString() : string.Empty,
                    Description = reader["fxdast_description"] != null ?
                        reader["fxdast_description"].ToString() : string.Empty,
                    Placement = reader["fxdast_placement"] != null ?
                        reader["fxdast_placement"].ToString() : string.Empty,
                    PlannificationCenter = reader["fxdast_plannification_center"] != null ?
                        reader["fxdast_plannification_center"].ToString() : string.Empty,
                    Area = reader["fxdast_area"] != null ?
                        reader["fxdast_area"].ToString() : string.Empty,
                    CostCenter = reader["fxdast_cost_center"] != null ?
                        reader["fxdast_cost_center"].ToString() : string.Empty,
                   
                };
                return value;
            };

            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            List<SOFTTEK.SCMS.Entity.FA.FixedAsset> result = dataProvider.ExecuteReaderWithFilter(SCMS.FA_SP_Select_FixedAsset, filterDelegate, filter, mapper);
            return result;
        }

        /// <summary>
        /// Inserts the instance of the provided FixedAsset at the Data Context datasource  
        /// </summary>
        /// <param name="instance">Instance of FixedAsset with the entity´s information to store</param>
        /// <returns>Instance of FixedAsset with the aditional information provided by the Data Context datasource</returns>
        public SOFTTEK.SCMS.Entity.FA.FixedAsset InsertFixedAsset(SOFTTEK.SCMS.Entity.FA.FixedAsset instance)
        {
            Func<SOFTTEK.SCMS.Entity.FA.FixedAsset, List<System.Data.IDataParameter>> input = (i) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();

                //Token
                System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p_tok.Direction = System.Data.ParameterDirection.Input;
                p_tok.DbType = System.Data.DbType.String;
                parameters.Add(p_tok);

                // Insert
                //ImageUrl
                System.Data.IDataParameter p_new_fxdast_image_url = dataProvider.GetParameter("@new_fxdast_image_url",
                    i.ImageUrl == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.ImageUrl);
                p_new_fxdast_image_url.Direction = System.Data.ParameterDirection.Input;
                p_new_fxdast_image_url.DbType = DbType.String;
                parameters.Add(p_new_fxdast_image_url);
                //SerialNumber
                System.Data.IDataParameter p_new_fxdast_serial_number = dataProvider.GetParameter("@new_fxdast_serial_number",
                    i.SerialNumber == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.SerialNumber);
                p_new_fxdast_serial_number.Direction = System.Data.ParameterDirection.Input;
                p_new_fxdast_serial_number.DbType = DbType.String;
                parameters.Add(p_new_fxdast_serial_number);
                //ExternalIdentifier
                System.Data.IDataParameter p_new_fxdast_external_identifier = dataProvider.GetParameter("@new_fxdast_external_identifier",
                    i.ExternalIdentifier == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.ExternalIdentifier);
                p_new_fxdast_external_identifier.Direction = System.Data.ParameterDirection.Input;
                p_new_fxdast_external_identifier.DbType = DbType.String;
                parameters.Add(p_new_fxdast_external_identifier);
                //Description
                System.Data.IDataParameter p_new_fxdast_description = dataProvider.GetParameter("@new_fxdast_description",
                    i.Description == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Description);
                p_new_fxdast_description.Direction = System.Data.ParameterDirection.Input;
                p_new_fxdast_description.DbType = DbType.String;
                parameters.Add(p_new_fxdast_description);
                //Placement
                System.Data.IDataParameter p_new_fxdast_placement = dataProvider.GetParameter("@new_fxdast_placement",
                    i.Placement == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Placement);
                p_new_fxdast_placement.Direction = System.Data.ParameterDirection.Input;
                p_new_fxdast_placement.DbType = DbType.String;
                parameters.Add(p_new_fxdast_placement);
                //PlannificationCenter
                System.Data.IDataParameter p_new_fxdast_plannification_center = dataProvider.GetParameter("@new_fxdast_plannification_center",
                    i.PlannificationCenter == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.PlannificationCenter);
                p_new_fxdast_plannification_center.Direction = System.Data.ParameterDirection.Input;
                p_new_fxdast_plannification_center.DbType = DbType.String;
                parameters.Add(p_new_fxdast_plannification_center);
                //Area
                System.Data.IDataParameter p_new_fxdast_area = dataProvider.GetParameter("@new_fxdast_area",
                    i.Area == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Area);
                p_new_fxdast_area.Direction = System.Data.ParameterDirection.Input;
                p_new_fxdast_area.DbType = DbType.String;
                parameters.Add(p_new_fxdast_area);
                //CostCenter
                System.Data.IDataParameter p_new_fxdast_cost_center = dataProvider.GetParameter("@new_fxdast_cost_center",
                    i.CostCenter == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.CostCenter);
                p_new_fxdast_cost_center.Direction = System.Data.ParameterDirection.Input;
                p_new_fxdast_cost_center.DbType = DbType.String;
                parameters.Add(p_new_fxdast_cost_center);
                
                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.FA.FixedAsset> mapper = (reader) =>
            {
                long longHelper = default(long);

                SOFTTEK.SCMS.Entity.FA.FixedAsset value = new SOFTTEK.SCMS.Entity.FA.FixedAsset
                {
                    ImageUrl = reader["v_fxdast_image_url"] != null ?
                        reader["v_fxdast_image_url"].ToString() : string.Empty,
                    SerialNumber = reader["v_fxdast_serial_number"] != null ?
                        reader["v_fxdast_serial_number"].ToString() : string.Empty,
                    Identifier = reader["v_fxdast_id_Pk"] != null ?
                        (long.TryParse(reader["v_fxdast_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    ExternalIdentifier = reader["v_fxdast_external_identifier"] != null ?
                        reader["v_fxdast_external_identifier"].ToString() : string.Empty,
                    Description = reader["v_fxdast_description"] != null ?
                        reader["v_fxdast_description"].ToString() : string.Empty,
                    Placement = reader["v_fxdast_placement"] != null ?
                        reader["v_fxdast_placement"].ToString() : string.Empty,
                    PlannificationCenter = reader["v_fxdast_plannification_center"] != null ?
                        reader["v_fxdast_plannification_center"].ToString() : string.Empty,
                    Area = reader["v_fxdast_area"] != null ?
                        reader["v_fxdast_area"].ToString() : string.Empty,
                    CostCenter = reader["v_fxdast_cost_center"] != null ?
                        reader["v_fxdast_cost_center"].ToString() : string.Empty,
                    
                };
                return value;
            };


            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            SOFTTEK.SCMS.Entity.FA.FixedAsset result = dataProvider.ExecuteForEntityWithFilter(SCMS.FA_SP_Insert_FixedAsset, input, instance, mapper);
            return result;
        }

        /// <summary>
        /// Update the entity's stored information with that provided by the instance of FixedAsset at the Data Context datasource  
        /// </summary>
        /// <param name="instance">Instance of FixedAsset with the entity´s information to update</param>
        /// <param name="filter">Object with the information required to filter the target stored entity´s information</param>
        /// <returns>Instance of FixedAsset with the updated information provided by the Data Context datasource</returns>
        public SOFTTEK.SCMS.Entity.FA.FixedAsset UpdateFixedAsset(SOFTTEK.SCMS.Entity.FA.FixedAsset instance, SOFTTEK.SCMS.Entity.FA.FixedAsset filter)
        {
            Func<SOFTTEK.SCMS.Entity.FA.FixedAsset, List<System.Data.IDataParameter>> filterDelegate = (f) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();

                //Token
                System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p_tok.Direction = System.Data.ParameterDirection.Input;
                p_tok.DbType = System.Data.DbType.String;
                parameters.Add(p_tok);

                // Filter
                //SerialNumber
                System.Data.IDataParameter p_filter_fxdast_serial_number = dataProvider.GetParameter("@filter_fxdast_serial_number",
                    f.SerialNumber == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.SerialNumber);
                p_filter_fxdast_serial_number.Direction = System.Data.ParameterDirection.Input;
                p_filter_fxdast_serial_number.DbType = DbType.String;
                parameters.Add(p_filter_fxdast_serial_number);
                //Identifier
                System.Data.IDataParameter p_filter_fxdast_id_Pk = dataProvider.GetParameter("@filter_fxdast_id_Pk",
                    f.Identifier == default(System.Int64) ?
                        (object)System.DBNull.Value :
                        (object)f.Identifier);
                p_filter_fxdast_id_Pk.Direction = System.Data.ParameterDirection.Input;
                p_filter_fxdast_id_Pk.DbType = DbType.Int64;
                parameters.Add(p_filter_fxdast_id_Pk);
                //ExternalIdentifier
                System.Data.IDataParameter p_filter_fxdast_external_identifier = dataProvider.GetParameter("@filter_fxdast_external_identifier",
                    f.ExternalIdentifier == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.ExternalIdentifier);
                p_filter_fxdast_external_identifier.Direction = System.Data.ParameterDirection.Input;
                p_filter_fxdast_external_identifier.DbType = DbType.String;
                parameters.Add(p_filter_fxdast_external_identifier);
                //Description
                System.Data.IDataParameter p_filter_fxdast_description = dataProvider.GetParameter("@filter_fxdast_description",
                    f.Description == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Description);
                p_filter_fxdast_description.Direction = System.Data.ParameterDirection.Input;
                p_filter_fxdast_description.DbType = DbType.String;
                parameters.Add(p_filter_fxdast_description);
                //Placement
                System.Data.IDataParameter p_filter_fxdast_placement = dataProvider.GetParameter("@filter_fxdast_placement",
                    f.Placement == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Placement);
                p_filter_fxdast_placement.Direction = System.Data.ParameterDirection.Input;
                p_filter_fxdast_placement.DbType = DbType.String;
                parameters.Add(p_filter_fxdast_placement);
                //PlannificationCenter
                System.Data.IDataParameter p_filter_fxdast_plannification_center = dataProvider.GetParameter("@filter_fxdast_plannification_center",
                    f.PlannificationCenter == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.PlannificationCenter);
                p_filter_fxdast_plannification_center.Direction = System.Data.ParameterDirection.Input;
                p_filter_fxdast_plannification_center.DbType = DbType.String;
                parameters.Add(p_filter_fxdast_plannification_center);
                //Area
                System.Data.IDataParameter p_filter_fxdast_area = dataProvider.GetParameter("@filter_fxdast_area",
                    f.Area == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Area);
                p_filter_fxdast_area.Direction = System.Data.ParameterDirection.Input;
                p_filter_fxdast_area.DbType = DbType.String;
                parameters.Add(p_filter_fxdast_area);
                //CostCenter
                System.Data.IDataParameter p_filter_fxdast_cost_center = dataProvider.GetParameter("@filter_fxdast_cost_center",
                    f.CostCenter == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.CostCenter);
                p_filter_fxdast_cost_center.Direction = System.Data.ParameterDirection.Input;
                p_filter_fxdast_cost_center.DbType = DbType.String;
                parameters.Add(p_filter_fxdast_cost_center);
                
                // Update
                //ImageUrl
                System.Data.IDataParameter p_new_fxdast_image_url = dataProvider.GetParameter("@new_fxdast_image_url",
                    f.ImageUrl == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.ImageUrl);
                p_new_fxdast_image_url.Direction = System.Data.ParameterDirection.Input;
                p_new_fxdast_image_url.DbType = DbType.String;
                parameters.Add(p_new_fxdast_image_url);
                //SerialNumber
                System.Data.IDataParameter p_new_fxdast_serial_number = dataProvider.GetParameter("@new_fxdast_serial_number",
                    f.SerialNumber == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.SerialNumber);
                p_new_fxdast_serial_number.Direction = System.Data.ParameterDirection.Input;
                p_new_fxdast_serial_number.DbType = DbType.String;
                parameters.Add(p_new_fxdast_serial_number);
                //ExternalIdentifier
                System.Data.IDataParameter p_new_fxdast_external_identifier = dataProvider.GetParameter("@new_fxdast_external_identifier",
                    f.ExternalIdentifier == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.ExternalIdentifier);
                p_new_fxdast_external_identifier.Direction = System.Data.ParameterDirection.Input;
                p_new_fxdast_external_identifier.DbType = DbType.String;
                parameters.Add(p_new_fxdast_external_identifier);
                //Description
                System.Data.IDataParameter p_new_fxdast_description = dataProvider.GetParameter("@new_fxdast_description",
                    f.Description == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Description);
                p_new_fxdast_description.Direction = System.Data.ParameterDirection.Input;
                p_new_fxdast_description.DbType = DbType.String;
                parameters.Add(p_new_fxdast_description);
                //Placement
                System.Data.IDataParameter p_new_fxdast_placement = dataProvider.GetParameter("@new_fxdast_placement",
                    f.Placement == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Placement);
                p_new_fxdast_placement.Direction = System.Data.ParameterDirection.Input;
                p_new_fxdast_placement.DbType = DbType.String;
                parameters.Add(p_new_fxdast_placement);
                //PlannificationCenter
                System.Data.IDataParameter p_new_fxdast_plannification_center = dataProvider.GetParameter("@new_fxdast_plannification_center",
                    f.PlannificationCenter == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.PlannificationCenter);
                p_new_fxdast_plannification_center.Direction = System.Data.ParameterDirection.Input;
                p_new_fxdast_plannification_center.DbType = DbType.String;
                parameters.Add(p_new_fxdast_plannification_center);
                //Area
                System.Data.IDataParameter p_new_fxdast_area = dataProvider.GetParameter("@new_fxdast_area",
                    f.Area == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Area);
                p_new_fxdast_area.Direction = System.Data.ParameterDirection.Input;
                p_new_fxdast_area.DbType = DbType.String;
                parameters.Add(p_new_fxdast_area);
                //CostCenter
                System.Data.IDataParameter p_new_fxdast_cost_center = dataProvider.GetParameter("@new_fxdast_cost_center",
                    f.CostCenter == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.CostCenter);
                p_new_fxdast_cost_center.Direction = System.Data.ParameterDirection.Input;
                p_new_fxdast_cost_center.DbType = DbType.String;
                parameters.Add(p_new_fxdast_cost_center);
               

                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.FA.FixedAsset> mapper = (reader) =>
            {
                long longHelper = default(long);

                SOFTTEK.SCMS.Entity.FA.FixedAsset value = new SOFTTEK.SCMS.Entity.FA.FixedAsset
                {
                    ImageUrl = reader["v_fxdast_image_url"] != null ?
                        reader["v_fxdast_image_url"].ToString() : string.Empty,
                    SerialNumber = reader["v_fxdast_serial_number"] != null ?
                        reader["v_fxdast_serial_number"].ToString() : string.Empty,
                    Identifier = reader["v_fxdast_id_Pk"] != null ?
                        (long.TryParse(reader["v_fxdast_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    ExternalIdentifier = reader["v_fxdast_external_identifier"] != null ?
                        reader["v_fxdast_external_identifier"].ToString() : string.Empty,
                    Description = reader["v_fxdast_description"] != null ?
                        reader["v_fxdast_description"].ToString() : string.Empty,
                    Placement = reader["v_fxdast_placement"] != null ?
                        reader["v_fxdast_placement"].ToString() : string.Empty,
                    PlannificationCenter = reader["v_fxdast_plannification_center"] != null ?
                        reader["v_fxdast_plannification_center"].ToString() : string.Empty,
                    Area = reader["v_fxdast_area"] != null ?
                        reader["v_fxdast_area"].ToString() : string.Empty,
                    CostCenter = reader["v_fxdast_cost_center"] != null ?
                        reader["v_fxdast_cost_center"].ToString() : string.Empty,
                    
                };
                return value;
            };


            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            SOFTTEK.SCMS.Entity.FA.FixedAsset result = dataProvider.ExecuteForEntityWithFilter(SCMS.FA_SP_Update_FixedAsset, filterDelegate, filter, mapper);
            return result;
        }
        #endregion
        #region Request

        /// <summary>
        /// Retrieves a list of Request instances that matches a given filter criteria by the Request object instance 
        /// </summary>
        /// <param name="filter">Object with the information required to filter the set of results</param>
        /// <returns>List of Request instances that matches the filter.</returns>
        public List<SOFTTEK.SCMS.Entity.FA.Request> GetRequests(SOFTTEK.SCMS.Entity.FA.Request filter)
        {
            Func<SOFTTEK.SCMS.Entity.FA.Request, List<System.Data.IDataParameter>> filterDelegate = (f) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
                //Token
                System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p_tok.Direction = System.Data.ParameterDirection.Input;
                p_tok.DbType = System.Data.DbType.String;
                parameters.Add(p_tok);
                //Identifier
                System.Data.IDataParameter p_filter_rqs_id_Pk = dataProvider.GetParameter("@filter_rqs_id_Pk",
                    f.Identifier == default(System.Int64) ?
                        (object)System.DBNull.Value :
                        (object)f.Identifier);
                p_filter_rqs_id_Pk.Direction = System.Data.ParameterDirection.Input;
                p_filter_rqs_id_Pk.DbType = DbType.Int64;
                parameters.Add(p_filter_rqs_id_Pk);
                //Number
                System.Data.IDataParameter p_filter_rqs_number = dataProvider.GetParameter("@filter_rqs_number",
                    f.Number == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Number);
                p_filter_rqs_number.Direction = System.Data.ParameterDirection.Input;
                p_filter_rqs_number.DbType = DbType.String;
                parameters.Add(p_filter_rqs_number);
                //Type
                System.Data.IDataParameter p_filter_rqs_type_Fk = dataProvider.GetParameter("@filter_rqs_type_Fk",
                    f.Type == default(SOFTTEK.SCMS.Entity.Shared.Parameter<System.String>) ?
                        (object)System.DBNull.Value :
                        (object)f.Type.Identifier);
                p_filter_rqs_type_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_rqs_type_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_rqs_type_Fk);
                //Informed
                System.Data.IDataParameter p_filter_rqs_informed_Fk = dataProvider.GetParameter("@filter_rqs_informed_Fk",
                    f.Informed == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Informed.Identifier);
                p_filter_rqs_informed_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_rqs_informed_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_rqs_informed_Fk);
                //Responsible
                System.Data.IDataParameter p_filter_rqs_responsible_Fk = dataProvider.GetParameter("@filter_rqs_responsible_Fk",
                    f.Responsible == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Responsible.Identifier);
                p_filter_rqs_responsible_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_rqs_responsible_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_rqs_responsible_Fk);
                //Accountable
                System.Data.IDataParameter p_filter_rqs_accountable_Fk = dataProvider.GetParameter("@filter_rqs_accountable_Fk",
                    f.Accountable == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Accountable.Identifier);
                p_filter_rqs_accountable_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_rqs_accountable_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_rqs_accountable_Fk);
                //Details
                System.Data.IDataParameter p_filter_rqs_details = dataProvider.GetParameter("@filter_rqs_details",
                    f.Details == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Details);
                p_filter_rqs_details.Direction = System.Data.ParameterDirection.Input;
                p_filter_rqs_details.DbType = DbType.String;
                parameters.Add(p_filter_rqs_details);
                //Comments
                System.Data.IDataParameter p_filter_rqs_comments = dataProvider.GetParameter("@filter_rqs_comments",
                    f.Comments == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Comments);
                p_filter_rqs_comments.Direction = System.Data.ParameterDirection.Input;
                p_filter_rqs_comments.DbType = DbType.String;
                parameters.Add(p_filter_rqs_comments);
                //Topic
                System.Data.IDataParameter p_filter_rqs_topic = dataProvider.GetParameter("@filter_rqs_topic",
                    f.Topic == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Topic);
                p_filter_rqs_topic.Direction = System.Data.ParameterDirection.Input;
                p_filter_rqs_topic.DbType = DbType.String;
                parameters.Add(p_filter_rqs_topic);
                //Status
                System.Data.IDataParameter p_filter_rqs_status = dataProvider.GetParameter("@filter_rqs_status",
                    f.Status == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Status);
                p_filter_rqs_status.Direction = System.Data.ParameterDirection.Input;
                p_filter_rqs_status.DbType = DbType.String;
                parameters.Add(p_filter_rqs_status);
                //UpdatedAt
                System.Data.IDataParameter p_filter_rqs_updated_at = dataProvider.GetParameter("@filter_rqs_updated_at",
                    f.UpdatedAt == default(System.DateTime) ?
                        (object)System.DBNull.Value :
                        (object)f.UpdatedAt);
                p_filter_rqs_updated_at.Direction = System.Data.ParameterDirection.Input;
                p_filter_rqs_updated_at.DbType = DbType.DateTime;
                parameters.Add(p_filter_rqs_updated_at);
                //RegisteredAt
                System.Data.IDataParameter p_filter_rqs_registered_at = dataProvider.GetParameter("@filter_rqs_registered_at",
                    f.RegisteredAt == default(System.DateTime) ?
                        (object)System.DBNull.Value :
                        (object)f.RegisteredAt);
                p_filter_rqs_registered_at.Direction = System.Data.ParameterDirection.Input;
                p_filter_rqs_registered_at.DbType = DbType.DateTime;
                parameters.Add(p_filter_rqs_registered_at);
                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.FA.Request> mapper = (reader) =>
            {
                long longHelper = default(long);
                int intHelper = default(int);
                DateTime dateHelper = default(DateTime);

                SOFTTEK.SCMS.Entity.FA.Request value = new SOFTTEK.SCMS.Entity.FA.Request
                {
                    Identifier = reader["rqs_id_Pk"] != null ?
                        (long.TryParse(reader["rqs_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    Number = reader["rqs_number"] != null ?
                        reader["rqs_number"].ToString() : string.Empty,
                    Type = reader["rqs_type_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Parameter<System.String> { Identifier = int.TryParse(reader["rqs_type_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Informed = reader["rqs_informed_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["rqs_informed_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Responsible = reader["rqs_responsible_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["rqs_responsible_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Accountable = reader["rqs_accountable_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["rqs_accountable_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Details = reader["rqs_details"] != null ?
                        reader["rqs_details"].ToString() : string.Empty,
                    Comments = reader["rqs_comments"] != null ?
                        reader["rqs_comments"].ToString() : string.Empty,
                    Topic = reader["rqs_topic"] != null ?
                        reader["rqs_topic"].ToString() : string.Empty,
                    Status = reader["rqs_status"] != null ?
                        reader["rqs_status"].ToString() : string.Empty,
                    UpdatedAt = reader["rqs_updated_at"] != null ?
                        (DateTime.TryParse(reader["rqs_updated_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                    RegisteredAt = reader["rqs_registered_at"] != null ?
                        (DateTime.TryParse(reader["rqs_registered_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                };
                return value;
            };

            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            List<SOFTTEK.SCMS.Entity.FA.Request> result = dataProvider.ExecuteReaderWithFilter(SCMS.FA_SP_Select_Request, filterDelegate, filter, mapper);
            return result;
        }

        /// <summary>
        /// Inserts the instance of the provided Request at the Data Context datasource  
        /// </summary>
        /// <param name="instance">Instance of Request with the entity´s information to store</param>
        /// <returns>Instance of Request with the aditional information provided by the Data Context datasource</returns>
        public SOFTTEK.SCMS.Entity.FA.Request InsertRequest(SOFTTEK.SCMS.Entity.FA.Request instance)
        {
            Func<SOFTTEK.SCMS.Entity.FA.Request, List<System.Data.IDataParameter>> input = (i) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();

                //Token
                System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p_tok.Direction = System.Data.ParameterDirection.Input;
                p_tok.DbType = System.Data.DbType.String;
                parameters.Add(p_tok);

                // Insert
                //Number
                System.Data.IDataParameter p_new_rqs_number = dataProvider.GetParameter("@new_rqs_number",
                    i.Number == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Number);
                p_new_rqs_number.Direction = System.Data.ParameterDirection.Input;
                p_new_rqs_number.DbType = DbType.String;
                parameters.Add(p_new_rqs_number);
                //Type
                System.Data.IDataParameter p_new_rqs_type_Fk = dataProvider.GetParameter("@new_rqs_type_Fk",
                    i.Type == default(SOFTTEK.SCMS.Entity.Shared.Parameter<System.String>) ?
                        (object)System.DBNull.Value :
                        (object)i.Type.Identifier);
                p_new_rqs_type_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_rqs_type_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_rqs_type_Fk);
                //Informed
                System.Data.IDataParameter p_new_rqs_informed_Fk = dataProvider.GetParameter("@new_rqs_informed_Fk",
                    i.Informed == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)i.Informed.Identifier);
                p_new_rqs_informed_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_rqs_informed_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_rqs_informed_Fk);
                //Responsible
                System.Data.IDataParameter p_new_rqs_responsible_Fk = dataProvider.GetParameter("@new_rqs_responsible_Fk",
                    i.Responsible == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)i.Responsible.Identifier);
                p_new_rqs_responsible_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_rqs_responsible_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_rqs_responsible_Fk);
                //Accountable
                System.Data.IDataParameter p_new_rqs_accountable_Fk = dataProvider.GetParameter("@new_rqs_accountable_Fk",
                    i.Accountable == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)i.Accountable.Identifier);
                p_new_rqs_accountable_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_rqs_accountable_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_rqs_accountable_Fk);
                //Details
                System.Data.IDataParameter p_new_rqs_details = dataProvider.GetParameter("@new_rqs_details",
                    i.Details == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Details);
                p_new_rqs_details.Direction = System.Data.ParameterDirection.Input;
                p_new_rqs_details.DbType = DbType.String;
                parameters.Add(p_new_rqs_details);
                //Comments
                System.Data.IDataParameter p_new_rqs_comments = dataProvider.GetParameter("@new_rqs_comments",
                    i.Comments == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Comments);
                p_new_rqs_comments.Direction = System.Data.ParameterDirection.Input;
                p_new_rqs_comments.DbType = DbType.String;
                parameters.Add(p_new_rqs_comments);
                //Topic
                System.Data.IDataParameter p_new_rqs_topic = dataProvider.GetParameter("@new_rqs_topic",
                    i.Topic == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Topic);
                p_new_rqs_topic.Direction = System.Data.ParameterDirection.Input;
                p_new_rqs_topic.DbType = DbType.String;
                parameters.Add(p_new_rqs_topic);
                //Status
                System.Data.IDataParameter p_new_rqs_status = dataProvider.GetParameter("@new_rqs_status",
                    i.Status == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Status);
                p_new_rqs_status.Direction = System.Data.ParameterDirection.Input;
                p_new_rqs_status.DbType = DbType.String;
                parameters.Add(p_new_rqs_status);

                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.FA.Request> mapper = (reader) =>
            {
                long longHelper = default(long);
                int intHelper = default(int);
                DateTime dateHelper = default(DateTime);

                SOFTTEK.SCMS.Entity.FA.Request value = new SOFTTEK.SCMS.Entity.FA.Request
                {
                    Identifier = reader["v_rqs_id_Pk"] != null ?
                        (long.TryParse(reader["v_rqs_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    Number = reader["v_rqs_number"] != null ?
                        reader["v_rqs_number"].ToString() : string.Empty,
                    Type = reader["v_rqs_type_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Parameter<System.String> { Identifier = int.TryParse(reader["v_rqs_type_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Informed = reader["v_rqs_informed_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_rqs_informed_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Responsible = reader["v_rqs_responsible_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_rqs_responsible_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Accountable = reader["v_rqs_accountable_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_rqs_accountable_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Details = reader["v_rqs_details"] != null ?
                        reader["v_rqs_details"].ToString() : string.Empty,
                    Comments = reader["v_rqs_comments"] != null ?
                        reader["v_rqs_comments"].ToString() : string.Empty,
                    Topic = reader["v_rqs_topic"] != null ?
                        reader["v_rqs_topic"].ToString() : string.Empty,
                    Status = reader["v_rqs_status"] != null ?
                        reader["v_rqs_status"].ToString() : string.Empty,
                    UpdatedAt = reader["v_rqs_updated_at"] != null ?
                        (DateTime.TryParse(reader["v_rqs_updated_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                    RegisteredAt = reader["v_rqs_registered_at"] != null ?
                        (DateTime.TryParse(reader["v_rqs_registered_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                };
                return value;
            };


            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            SOFTTEK.SCMS.Entity.FA.Request result = dataProvider.ExecuteForEntityWithFilter(SCMS.FA_SP_Insert_Request, input, instance, mapper);
            return result;
        }

        /// <summary>
        /// Update the entity's stored information with that provided by the instance of Request at the Data Context datasource  
        /// </summary>
        /// <param name="instance">Instance of Request with the entity´s information to update</param>
        /// <param name="filter">Object with the information required to filter the target stored entity´s information</param>
        /// <returns>Instance of Request with the updated information provided by the Data Context datasource</returns>
        public SOFTTEK.SCMS.Entity.FA.Request UpdateRequest(SOFTTEK.SCMS.Entity.FA.Request instance, SOFTTEK.SCMS.Entity.FA.Request filter)
        {
            Func<SOFTTEK.SCMS.Entity.FA.Request, List<System.Data.IDataParameter>> filterDelegate = (f) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();

                //Token
                System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p_tok.Direction = System.Data.ParameterDirection.Input;
                p_tok.DbType = System.Data.DbType.String;
                parameters.Add(p_tok);

                // Filter
                //Identifier
                System.Data.IDataParameter p_filter_rqs_id_Pk = dataProvider.GetParameter("@filter_rqs_id_Pk",
                    f.Identifier == default(System.Int64) ?
                        (object)System.DBNull.Value :
                        (object)f.Identifier);
                p_filter_rqs_id_Pk.Direction = System.Data.ParameterDirection.Input;
                p_filter_rqs_id_Pk.DbType = DbType.Int64;
                parameters.Add(p_filter_rqs_id_Pk);
                //Number
                System.Data.IDataParameter p_filter_rqs_number = dataProvider.GetParameter("@filter_rqs_number",
                    f.Number == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Number);
                p_filter_rqs_number.Direction = System.Data.ParameterDirection.Input;
                p_filter_rqs_number.DbType = DbType.String;
                parameters.Add(p_filter_rqs_number);
                //Type
                System.Data.IDataParameter p_filter_rqs_type_Fk = dataProvider.GetParameter("@filter_rqs_type_Fk",
                    f.Type == default(SOFTTEK.SCMS.Entity.Shared.Parameter<System.String>) ?
                        (object)System.DBNull.Value :
                        (object)f.Type.Identifier);
                p_filter_rqs_type_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_rqs_type_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_rqs_type_Fk);
                //Informed
                System.Data.IDataParameter p_filter_rqs_informed_Fk = dataProvider.GetParameter("@filter_rqs_informed_Fk",
                    f.Informed == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Informed.Identifier);
                p_filter_rqs_informed_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_rqs_informed_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_rqs_informed_Fk);
                //Responsible
                System.Data.IDataParameter p_filter_rqs_responsible_Fk = dataProvider.GetParameter("@filter_rqs_responsible_Fk",
                    f.Responsible == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Responsible.Identifier);
                p_filter_rqs_responsible_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_rqs_responsible_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_rqs_responsible_Fk);
                //Accountable
                System.Data.IDataParameter p_filter_rqs_accountable_Fk = dataProvider.GetParameter("@filter_rqs_accountable_Fk",
                    f.Accountable == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Accountable.Identifier);
                p_filter_rqs_accountable_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_rqs_accountable_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_rqs_accountable_Fk);
                //Details
                System.Data.IDataParameter p_filter_rqs_details = dataProvider.GetParameter("@filter_rqs_details",
                    f.Details == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Details);
                p_filter_rqs_details.Direction = System.Data.ParameterDirection.Input;
                p_filter_rqs_details.DbType = DbType.String;
                parameters.Add(p_filter_rqs_details);
                //Comments
                System.Data.IDataParameter p_filter_rqs_comments = dataProvider.GetParameter("@filter_rqs_comments",
                    f.Comments == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Comments);
                p_filter_rqs_comments.Direction = System.Data.ParameterDirection.Input;
                p_filter_rqs_comments.DbType = DbType.String;
                parameters.Add(p_filter_rqs_comments);
                //Topic
                System.Data.IDataParameter p_filter_rqs_topic = dataProvider.GetParameter("@filter_rqs_topic",
                    f.Topic == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Topic);
                p_filter_rqs_topic.Direction = System.Data.ParameterDirection.Input;
                p_filter_rqs_topic.DbType = DbType.String;
                parameters.Add(p_filter_rqs_topic);
                //Status
                System.Data.IDataParameter p_filter_rqs_status = dataProvider.GetParameter("@filter_rqs_status",
                    f.Status == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Status);
                p_filter_rqs_status.Direction = System.Data.ParameterDirection.Input;
                p_filter_rqs_status.DbType = DbType.String;
                parameters.Add(p_filter_rqs_status);
                //UpdatedAt
                System.Data.IDataParameter p_filter_rqs_updated_at = dataProvider.GetParameter("@filter_rqs_updated_at",
                    f.UpdatedAt == default(System.DateTime) ?
                        (object)System.DBNull.Value :
                        (object)f.UpdatedAt);
                p_filter_rqs_updated_at.Direction = System.Data.ParameterDirection.Input;
                p_filter_rqs_updated_at.DbType = DbType.DateTime;
                parameters.Add(p_filter_rqs_updated_at);
                //RegisteredAt
                System.Data.IDataParameter p_filter_rqs_registered_at = dataProvider.GetParameter("@filter_rqs_registered_at",
                    f.RegisteredAt == default(System.DateTime) ?
                        (object)System.DBNull.Value :
                        (object)f.RegisteredAt);
                p_filter_rqs_registered_at.Direction = System.Data.ParameterDirection.Input;
                p_filter_rqs_registered_at.DbType = DbType.DateTime;
                parameters.Add(p_filter_rqs_registered_at);
                // Update
                //Number
                System.Data.IDataParameter p_new_rqs_number = dataProvider.GetParameter("@new_rqs_number",
                    f.Number == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Number);
                p_new_rqs_number.Direction = System.Data.ParameterDirection.Input;
                p_new_rqs_number.DbType = DbType.String;
                parameters.Add(p_new_rqs_number);
                //Type
                System.Data.IDataParameter p_new_rqs_type_Fk = dataProvider.GetParameter("@new_rqs_type_Fk",
                    instance.Type == default(SOFTTEK.SCMS.Entity.Shared.Parameter<System.String>) ?
                        (object)System.DBNull.Value :
                        (object)instance.Type.Identifier);
                p_new_rqs_type_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_rqs_type_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_rqs_type_Fk);
                //Informed
                System.Data.IDataParameter p_new_rqs_informed_Fk = dataProvider.GetParameter("@new_rqs_informed_Fk",
                    instance.Informed == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)instance.Informed.Identifier);
                p_new_rqs_informed_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_rqs_informed_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_rqs_informed_Fk);
                //Responsible
                System.Data.IDataParameter p_new_rqs_responsible_Fk = dataProvider.GetParameter("@new_rqs_responsible_Fk",
                    instance.Responsible == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)instance.Responsible.Identifier);
                p_new_rqs_responsible_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_rqs_responsible_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_rqs_responsible_Fk);
                //Accountable
                System.Data.IDataParameter p_new_rqs_accountable_Fk = dataProvider.GetParameter("@new_rqs_accountable_Fk",
                    instance.Accountable == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)instance.Accountable.Identifier);
                p_new_rqs_accountable_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_rqs_accountable_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_rqs_accountable_Fk);
                //Details
                System.Data.IDataParameter p_new_rqs_details = dataProvider.GetParameter("@new_rqs_details",
                    f.Details == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Details);
                p_new_rqs_details.Direction = System.Data.ParameterDirection.Input;
                p_new_rqs_details.DbType = DbType.String;
                parameters.Add(p_new_rqs_details);
                //Comments
                System.Data.IDataParameter p_new_rqs_comments = dataProvider.GetParameter("@new_rqs_comments",
                    f.Comments == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Comments);
                p_new_rqs_comments.Direction = System.Data.ParameterDirection.Input;
                p_new_rqs_comments.DbType = DbType.String;
                parameters.Add(p_new_rqs_comments);
                //Topic
                System.Data.IDataParameter p_new_rqs_topic = dataProvider.GetParameter("@new_rqs_topic",
                    f.Topic == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Topic);
                p_new_rqs_topic.Direction = System.Data.ParameterDirection.Input;
                p_new_rqs_topic.DbType = DbType.String;
                parameters.Add(p_new_rqs_topic);
                //Status
                System.Data.IDataParameter p_new_rqs_status = dataProvider.GetParameter("@new_rqs_status",
                    f.Status == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Status);
                p_new_rqs_status.Direction = System.Data.ParameterDirection.Input;
                p_new_rqs_status.DbType = DbType.String;
                parameters.Add(p_new_rqs_status);

                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.FA.Request> mapper = (reader) =>
            {
                long longHelper = default(long);
                int intHelper = default(int);
                DateTime dateHelper = default(DateTime);

                SOFTTEK.SCMS.Entity.FA.Request value = new SOFTTEK.SCMS.Entity.FA.Request
                {
                    Identifier = reader["v_rqs_id_Pk"] != null ?
                        (long.TryParse(reader["v_rqs_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    Number = reader["v_rqs_number"] != null ?
                        reader["v_rqs_number"].ToString() : string.Empty,
                    Type = reader["v_rqs_type_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Parameter<System.String> { Identifier = int.TryParse(reader["v_rqs_type_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Informed = reader["v_rqs_informed_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_rqs_informed_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Responsible = reader["v_rqs_responsible_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_rqs_responsible_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Accountable = reader["v_rqs_accountable_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_rqs_accountable_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Details = reader["v_rqs_details"] != null ?
                        reader["v_rqs_details"].ToString() : string.Empty,
                    Comments = reader["v_rqs_comments"] != null ?
                        reader["v_rqs_comments"].ToString() : string.Empty,
                    Topic = reader["v_rqs_topic"] != null ?
                        reader["v_rqs_topic"].ToString() : string.Empty,
                    Status = reader["v_rqs_status"] != null ?
                        reader["v_rqs_status"].ToString() : string.Empty,
                    UpdatedAt = reader["v_rqs_updated_at"] != null ?
                        (DateTime.TryParse(reader["v_rqs_updated_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                    RegisteredAt = reader["v_rqs_registered_at"] != null ?
                        (DateTime.TryParse(reader["v_rqs_registered_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                };
                return value;
            };


            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            SOFTTEK.SCMS.Entity.FA.Request result = dataProvider.ExecuteForEntityWithFilter(SCMS.FA_SP_Update_Request, filterDelegate, filter, mapper);
            return result;
        }
        #endregion
        #region NoveltyRequest

        /// <summary>
        /// Retrieves a list of NoveltyRequest instances that matches a given filter criteria by the NoveltyRequest object instance 
        /// </summary>
        /// <param name="filter">Object with the information required to filter the set of results</param>
        /// <returns>List of NoveltyRequest instances that matches the filter.</returns>
        public List<SOFTTEK.SCMS.Entity.FA.NoveltyRequest> GetNoveltyRequests(SOFTTEK.SCMS.Entity.FA.NoveltyRequest filter)
        {
            Func<SOFTTEK.SCMS.Entity.FA.NoveltyRequest, List<System.Data.IDataParameter>> filterDelegate = (f) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
                //Token
                System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p_tok.Direction = System.Data.ParameterDirection.Input;
                p_tok.DbType = System.Data.DbType.String;
                parameters.Add(p_tok);
                //Request
                System.Data.IDataParameter p_filter_nvlrqs_request_Fk = dataProvider.GetParameter("@filter_nvlrqs_request_Fk",
                    f.Request == default(SOFTTEK.SCMS.Entity.FA.Request) ?
                        (object)System.DBNull.Value :
                        (object)f.Request.Identifier);
                p_filter_nvlrqs_request_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_nvlrqs_request_Fk.DbType = DbType.Int64;
                parameters.Add(p_filter_nvlrqs_request_Fk);
                //ExternalIdentifier
                System.Data.IDataParameter p_filter_nvlrqs_external_identifier = dataProvider.GetParameter("@filter_nvlrqs_external_identifier",
                    f.ExternalIdentifier == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.ExternalIdentifier);
                p_filter_nvlrqs_external_identifier.Direction = System.Data.ParameterDirection.Input;
                p_filter_nvlrqs_external_identifier.DbType = DbType.String;
                parameters.Add(p_filter_nvlrqs_external_identifier);
                //Identifier
                System.Data.IDataParameter p_filter_nvlrqs_id_Pk = dataProvider.GetParameter("@filter_nvlrqs_id_Pk",
                    f.Identifier == default(System.Int64) ?
                        (object)System.DBNull.Value :
                        (object)f.Identifier);
                p_filter_nvlrqs_id_Pk.Direction = System.Data.ParameterDirection.Input;
                p_filter_nvlrqs_id_Pk.DbType = DbType.Int64;
                parameters.Add(p_filter_nvlrqs_id_Pk);
                //Number
                System.Data.IDataParameter p_filter_nvlrqs_number = dataProvider.GetParameter("@filter_nvlrqs_number",
                    f.Number == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Number);
                p_filter_nvlrqs_number.Direction = System.Data.ParameterDirection.Input;
                p_filter_nvlrqs_number.DbType = DbType.String;
                parameters.Add(p_filter_nvlrqs_number);
                //Type
                System.Data.IDataParameter p_filter_nvlrqs_type_Fk = dataProvider.GetParameter("@filter_nvlrqs_type_Fk",
                    f.Type == default(SOFTTEK.SCMS.Entity.Shared.Parameter<System.String>) ?
                        (object)System.DBNull.Value :
                        (object)f.Type.Identifier);
                p_filter_nvlrqs_type_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_nvlrqs_type_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_nvlrqs_type_Fk);
                //Informed
                System.Data.IDataParameter p_filter_nvlrqs_informed_Fk = dataProvider.GetParameter("@filter_nvlrqs_informed_Fk",
                    f.Informed == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Informed.Identifier);
                p_filter_nvlrqs_informed_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_nvlrqs_informed_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_nvlrqs_informed_Fk);
                //Responsible
                System.Data.IDataParameter p_filter_nvlrqs_responsible_Fk = dataProvider.GetParameter("@filter_nvlrqs_responsible_Fk",
                    f.Responsible == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Responsible.Identifier);
                p_filter_nvlrqs_responsible_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_nvlrqs_responsible_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_nvlrqs_responsible_Fk);
                //Accountable
                System.Data.IDataParameter p_filter_nvlrqs_accountable_Fk = dataProvider.GetParameter("@filter_nvlrqs_accountable_Fk",
                    f.Accountable == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Accountable.Identifier);
                p_filter_nvlrqs_accountable_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_nvlrqs_accountable_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_nvlrqs_accountable_Fk);
                //Details
                System.Data.IDataParameter p_filter_nvlrqs_details = dataProvider.GetParameter("@filter_nvlrqs_details",
                    f.Details == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Details);
                p_filter_nvlrqs_details.Direction = System.Data.ParameterDirection.Input;
                p_filter_nvlrqs_details.DbType = DbType.String;
                parameters.Add(p_filter_nvlrqs_details);
                //Comments
                System.Data.IDataParameter p_filter_nvlrqs_comments = dataProvider.GetParameter("@filter_nvlrqs_comments",
                    f.Comments == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Comments);
                p_filter_nvlrqs_comments.Direction = System.Data.ParameterDirection.Input;
                p_filter_nvlrqs_comments.DbType = DbType.String;
                parameters.Add(p_filter_nvlrqs_comments);
                //Topic
                System.Data.IDataParameter p_filter_nvlrqs_topic = dataProvider.GetParameter("@filter_nvlrqs_topic",
                    f.Topic == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Topic);
                p_filter_nvlrqs_topic.Direction = System.Data.ParameterDirection.Input;
                p_filter_nvlrqs_topic.DbType = DbType.String;
                parameters.Add(p_filter_nvlrqs_topic);
                //Status
                System.Data.IDataParameter p_filter_nvlrqs_status = dataProvider.GetParameter("@filter_nvlrqs_status",
                    f.Status == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Status);
                p_filter_nvlrqs_status.Direction = System.Data.ParameterDirection.Input;
                p_filter_nvlrqs_status.DbType = DbType.String;
                parameters.Add(p_filter_nvlrqs_status);
                //UpdatedAt
                System.Data.IDataParameter p_filter_nvlrqs_updated_at = dataProvider.GetParameter("@filter_nvlrqs_updated_at",
                    f.UpdatedAt == default(System.DateTime) ?
                        (object)System.DBNull.Value :
                        (object)f.UpdatedAt);
                p_filter_nvlrqs_updated_at.Direction = System.Data.ParameterDirection.Input;
                p_filter_nvlrqs_updated_at.DbType = DbType.DateTime;
                parameters.Add(p_filter_nvlrqs_updated_at);
                //RegisteredAt
                System.Data.IDataParameter p_filter_nvlrqs_registered_at = dataProvider.GetParameter("@filter_nvlrqs_registered_at",
                    f.RegisteredAt == default(System.DateTime) ?
                        (object)System.DBNull.Value :
                        (object)f.RegisteredAt);
                p_filter_nvlrqs_registered_at.Direction = System.Data.ParameterDirection.Input;
                p_filter_nvlrqs_registered_at.DbType = DbType.DateTime;
                parameters.Add(p_filter_nvlrqs_registered_at);
                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.FA.NoveltyRequest> mapper = (reader) =>
            {
                long longHelper = default(long);
                int intHelper = default(int);
                DateTime dateHelper = default(DateTime);

                SOFTTEK.SCMS.Entity.FA.NoveltyRequest value = new SOFTTEK.SCMS.Entity.FA.NoveltyRequest
                {
                    Request = reader["nvlrqs_request_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.FA.Request { Identifier = long.TryParse(reader["nvlrqs_request_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null,
                    ExternalIdentifier = reader["nvlrqs_external_identifier"] != null ?
                        reader["nvlrqs_external_identifier"].ToString() : string.Empty,
                    Identifier = reader["nvlrqs_id_Pk"] != null ?
                        (long.TryParse(reader["nvlrqs_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    Number = reader["nvlrqs_number"] != null ?
                        reader["nvlrqs_number"].ToString() : string.Empty,
                    Type = reader["nvlrqs_type_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Parameter<System.String> { Identifier = int.TryParse(reader["nvlrqs_type_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Informed = reader["nvlrqs_informed_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["nvlrqs_informed_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Responsible = reader["nvlrqs_responsible_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["nvlrqs_responsible_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Accountable = reader["nvlrqs_accountable_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["nvlrqs_accountable_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Details = reader["nvlrqs_details"] != null ?
                        reader["nvlrqs_details"].ToString() : string.Empty,
                    Comments = reader["nvlrqs_comments"] != null ?
                        reader["nvlrqs_comments"].ToString() : string.Empty,
                    Topic = reader["nvlrqs_topic"] != null ?
                        reader["nvlrqs_topic"].ToString() : string.Empty,
                    Status = reader["nvlrqs_status"] != null ?
                        reader["nvlrqs_status"].ToString() : string.Empty,
                    UpdatedAt = reader["nvlrqs_updated_at"] != null ?
                        (DateTime.TryParse(reader["nvlrqs_updated_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                    RegisteredAt = reader["nvlrqs_registered_at"] != null ?
                        (DateTime.TryParse(reader["nvlrqs_registered_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                };
                return value;
            };

            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            List<SOFTTEK.SCMS.Entity.FA.NoveltyRequest> result = dataProvider.ExecuteReaderWithFilter(SCMS.FA_SP_Select_NoveltyRequest, filterDelegate, filter, mapper);
            return result;
        }

        /// <summary>
        /// Inserts the instance of the provided NoveltyRequest at the Data Context datasource  
        /// </summary>
        /// <param name="instance">Instance of NoveltyRequest with the entity´s information to store</param>
        /// <returns>Instance of NoveltyRequest with the aditional information provided by the Data Context datasource</returns>
        public SOFTTEK.SCMS.Entity.FA.NoveltyRequest InsertNoveltyRequest(SOFTTEK.SCMS.Entity.FA.NoveltyRequest instance)
        {
            Func<SOFTTEK.SCMS.Entity.FA.NoveltyRequest, List<System.Data.IDataParameter>> input = (i) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();

                //Token
                System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p_tok.Direction = System.Data.ParameterDirection.Input;
                p_tok.DbType = System.Data.DbType.String;
                parameters.Add(p_tok);

                // Insert
                //Request
                System.Data.IDataParameter p_new_nvlrqs_request_Fk = dataProvider.GetParameter("@new_nvlrqs_request_Fk",
                    i.Request == default(SOFTTEK.SCMS.Entity.FA.Request) ?
                        (object)System.DBNull.Value :
                        (object)i.Request.Identifier);
                p_new_nvlrqs_request_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_nvlrqs_request_Fk.DbType = DbType.Int64;
                parameters.Add(p_new_nvlrqs_request_Fk);
                //ExternalIdentifier
                System.Data.IDataParameter p_new_nvlrqs_external_identifier = dataProvider.GetParameter("@new_nvlrqs_external_identifier",
                    i.ExternalIdentifier == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.ExternalIdentifier);
                p_new_nvlrqs_external_identifier.Direction = System.Data.ParameterDirection.Input;
                p_new_nvlrqs_external_identifier.DbType = DbType.String;
                parameters.Add(p_new_nvlrqs_external_identifier);
                //Number
                System.Data.IDataParameter p_new_nvlrqs_number = dataProvider.GetParameter("@new_nvlrqs_number",
                    i.Number == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Number);
                p_new_nvlrqs_number.Direction = System.Data.ParameterDirection.Input;
                p_new_nvlrqs_number.DbType = DbType.String;
                parameters.Add(p_new_nvlrqs_number);
                //Type
                System.Data.IDataParameter p_new_nvlrqs_type_Fk = dataProvider.GetParameter("@new_nvlrqs_type_Fk",
                    i.Type == default(SOFTTEK.SCMS.Entity.Shared.Parameter<System.String>) ?
                        (object)System.DBNull.Value :
                        (object)i.Type.Identifier);
                p_new_nvlrqs_type_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_nvlrqs_type_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_nvlrqs_type_Fk);
                //Informed
                System.Data.IDataParameter p_new_nvlrqs_informed_Fk = dataProvider.GetParameter("@new_nvlrqs_informed_Fk",
                    i.Informed == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)i.Informed.Identifier);
                p_new_nvlrqs_informed_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_nvlrqs_informed_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_nvlrqs_informed_Fk);
                //Responsible
                System.Data.IDataParameter p_new_nvlrqs_responsible_Fk = dataProvider.GetParameter("@new_nvlrqs_responsible_Fk",
                    i.Responsible == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)i.Responsible.Identifier);
                p_new_nvlrqs_responsible_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_nvlrqs_responsible_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_nvlrqs_responsible_Fk);
                //Accountable
                System.Data.IDataParameter p_new_nvlrqs_accountable_Fk = dataProvider.GetParameter("@new_nvlrqs_accountable_Fk",
                    i.Accountable == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)i.Accountable.Identifier);
                p_new_nvlrqs_accountable_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_nvlrqs_accountable_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_nvlrqs_accountable_Fk);
                //Details
                System.Data.IDataParameter p_new_nvlrqs_details = dataProvider.GetParameter("@new_nvlrqs_details",
                    i.Details == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Details);
                p_new_nvlrqs_details.Direction = System.Data.ParameterDirection.Input;
                p_new_nvlrqs_details.DbType = DbType.String;
                parameters.Add(p_new_nvlrqs_details);
                //Comments
                System.Data.IDataParameter p_new_nvlrqs_comments = dataProvider.GetParameter("@new_nvlrqs_comments",
                    i.Comments == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Comments);
                p_new_nvlrqs_comments.Direction = System.Data.ParameterDirection.Input;
                p_new_nvlrqs_comments.DbType = DbType.String;
                parameters.Add(p_new_nvlrqs_comments);
                //Topic
                System.Data.IDataParameter p_new_nvlrqs_topic = dataProvider.GetParameter("@new_nvlrqs_topic",
                    i.Topic == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Topic);
                p_new_nvlrqs_topic.Direction = System.Data.ParameterDirection.Input;
                p_new_nvlrqs_topic.DbType = DbType.String;
                parameters.Add(p_new_nvlrqs_topic);
                //Status
                System.Data.IDataParameter p_new_nvlrqs_status = dataProvider.GetParameter("@new_nvlrqs_status",
                    i.Status == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Status);
                p_new_nvlrqs_status.Direction = System.Data.ParameterDirection.Input;
                p_new_nvlrqs_status.DbType = DbType.String;
                parameters.Add(p_new_nvlrqs_status);

                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.FA.NoveltyRequest> mapper = (reader) =>
            {
                long longHelper = default(long);
                int intHelper = default(int);
                DateTime dateHelper = default(DateTime);

                SOFTTEK.SCMS.Entity.FA.NoveltyRequest value = new SOFTTEK.SCMS.Entity.FA.NoveltyRequest
                {
                    Request = reader["v_nvlrqs_request_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.FA.Request { Identifier = long.TryParse(reader["v_nvlrqs_request_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null,
                    ExternalIdentifier = reader["v_nvlrqs_external_identifier"] != null ?
                        reader["v_nvlrqs_external_identifier"].ToString() : string.Empty,
                    Identifier = reader["v_nvlrqs_id_Pk"] != null ?
                        (long.TryParse(reader["v_nvlrqs_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    Number = reader["v_nvlrqs_number"] != null ?
                        reader["v_nvlrqs_number"].ToString() : string.Empty,
                    Type = reader["v_nvlrqs_type_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Parameter<System.String> { Identifier = int.TryParse(reader["v_nvlrqs_type_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Informed = reader["v_nvlrqs_informed_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_nvlrqs_informed_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Responsible = reader["v_nvlrqs_responsible_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_nvlrqs_responsible_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Accountable = reader["v_nvlrqs_accountable_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_nvlrqs_accountable_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Details = reader["v_nvlrqs_details"] != null ?
                        reader["v_nvlrqs_details"].ToString() : string.Empty,
                    Comments = reader["v_nvlrqs_comments"] != null ?
                        reader["v_nvlrqs_comments"].ToString() : string.Empty,
                    Topic = reader["v_nvlrqs_topic"] != null ?
                        reader["v_nvlrqs_topic"].ToString() : string.Empty,
                    Status = reader["v_nvlrqs_status"] != null ?
                        reader["v_nvlrqs_status"].ToString() : string.Empty,
                    UpdatedAt = reader["v_nvlrqs_updated_at"] != null ?
                        (DateTime.TryParse(reader["v_nvlrqs_updated_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                    RegisteredAt = reader["v_nvlrqs_registered_at"] != null ?
                        (DateTime.TryParse(reader["v_nvlrqs_registered_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                };
                return value;
            };


            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            SOFTTEK.SCMS.Entity.FA.NoveltyRequest result = dataProvider.ExecuteForEntityWithFilter(SCMS.FA_SP_Insert_NoveltyRequest, input, instance, mapper);
            return result;
        }

        /// <summary>
        /// Update the entity's stored information with that provided by the instance of NoveltyRequest at the Data Context datasource  
        /// </summary>
        /// <param name="instance">Instance of NoveltyRequest with the entity´s information to update</param>
        /// <param name="filter">Object with the information required to filter the target stored entity´s information</param>
        /// <returns>Instance of NoveltyRequest with the updated information provided by the Data Context datasource</returns>
        public SOFTTEK.SCMS.Entity.FA.NoveltyRequest UpdateNoveltyRequest(SOFTTEK.SCMS.Entity.FA.NoveltyRequest instance, SOFTTEK.SCMS.Entity.FA.NoveltyRequest filter)
        {
            Func<SOFTTEK.SCMS.Entity.FA.NoveltyRequest, List<System.Data.IDataParameter>> filterDelegate = (f) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();

                //Token
                System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p_tok.Direction = System.Data.ParameterDirection.Input;
                p_tok.DbType = System.Data.DbType.String;
                parameters.Add(p_tok);

                // Filter
                //Request
                System.Data.IDataParameter p_filter_nvlrqs_request_Fk = dataProvider.GetParameter("@filter_nvlrqs_request_Fk",
                    f.Request == default(SOFTTEK.SCMS.Entity.FA.Request) ?
                        (object)System.DBNull.Value :
                        (object)f.Request.Identifier);
                p_filter_nvlrqs_request_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_nvlrqs_request_Fk.DbType = DbType.Int64;
                parameters.Add(p_filter_nvlrqs_request_Fk);
                //ExternalIdentifier
                System.Data.IDataParameter p_filter_nvlrqs_external_identifier = dataProvider.GetParameter("@filter_nvlrqs_external_identifier",
                    f.ExternalIdentifier == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.ExternalIdentifier);
                p_filter_nvlrqs_external_identifier.Direction = System.Data.ParameterDirection.Input;
                p_filter_nvlrqs_external_identifier.DbType = DbType.String;
                parameters.Add(p_filter_nvlrqs_external_identifier);
                //Identifier
                System.Data.IDataParameter p_filter_nvlrqs_id_Pk = dataProvider.GetParameter("@filter_nvlrqs_id_Pk",
                    f.Identifier == default(System.Int64) ?
                        (object)System.DBNull.Value :
                        (object)f.Identifier);
                p_filter_nvlrqs_id_Pk.Direction = System.Data.ParameterDirection.Input;
                p_filter_nvlrqs_id_Pk.DbType = DbType.Int64;
                parameters.Add(p_filter_nvlrqs_id_Pk);
                //Number
                System.Data.IDataParameter p_filter_nvlrqs_number = dataProvider.GetParameter("@filter_nvlrqs_number",
                    f.Number == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Number);
                p_filter_nvlrqs_number.Direction = System.Data.ParameterDirection.Input;
                p_filter_nvlrqs_number.DbType = DbType.String;
                parameters.Add(p_filter_nvlrqs_number);
                //Type
                System.Data.IDataParameter p_filter_nvlrqs_type_Fk = dataProvider.GetParameter("@filter_nvlrqs_type_Fk",
                    f.Type == default(SOFTTEK.SCMS.Entity.Shared.Parameter<System.String>) ?
                        (object)System.DBNull.Value :
                        (object)f.Type.Identifier);
                p_filter_nvlrqs_type_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_nvlrqs_type_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_nvlrqs_type_Fk);
                //Informed
                System.Data.IDataParameter p_filter_nvlrqs_informed_Fk = dataProvider.GetParameter("@filter_nvlrqs_informed_Fk",
                    f.Informed == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Informed.Identifier);
                p_filter_nvlrqs_informed_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_nvlrqs_informed_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_nvlrqs_informed_Fk);
                //Responsible
                System.Data.IDataParameter p_filter_nvlrqs_responsible_Fk = dataProvider.GetParameter("@filter_nvlrqs_responsible_Fk",
                    f.Responsible == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Responsible.Identifier);
                p_filter_nvlrqs_responsible_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_nvlrqs_responsible_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_nvlrqs_responsible_Fk);
                //Accountable
                System.Data.IDataParameter p_filter_nvlrqs_accountable_Fk = dataProvider.GetParameter("@filter_nvlrqs_accountable_Fk",
                    f.Accountable == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Accountable.Identifier);
                p_filter_nvlrqs_accountable_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_nvlrqs_accountable_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_nvlrqs_accountable_Fk);
                //Details
                System.Data.IDataParameter p_filter_nvlrqs_details = dataProvider.GetParameter("@filter_nvlrqs_details",
                    f.Details == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Details);
                p_filter_nvlrqs_details.Direction = System.Data.ParameterDirection.Input;
                p_filter_nvlrqs_details.DbType = DbType.String;
                parameters.Add(p_filter_nvlrqs_details);
                //Comments
                System.Data.IDataParameter p_filter_nvlrqs_comments = dataProvider.GetParameter("@filter_nvlrqs_comments",
                    f.Comments == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Comments);
                p_filter_nvlrqs_comments.Direction = System.Data.ParameterDirection.Input;
                p_filter_nvlrqs_comments.DbType = DbType.String;
                parameters.Add(p_filter_nvlrqs_comments);
                //Topic
                System.Data.IDataParameter p_filter_nvlrqs_topic = dataProvider.GetParameter("@filter_nvlrqs_topic",
                    f.Topic == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Topic);
                p_filter_nvlrqs_topic.Direction = System.Data.ParameterDirection.Input;
                p_filter_nvlrqs_topic.DbType = DbType.String;
                parameters.Add(p_filter_nvlrqs_topic);
                //Status
                System.Data.IDataParameter p_filter_nvlrqs_status = dataProvider.GetParameter("@filter_nvlrqs_status",
                    f.Status == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Status);
                p_filter_nvlrqs_status.Direction = System.Data.ParameterDirection.Input;
                p_filter_nvlrqs_status.DbType = DbType.String;
                parameters.Add(p_filter_nvlrqs_status);
                //UpdatedAt
                System.Data.IDataParameter p_filter_nvlrqs_updated_at = dataProvider.GetParameter("@filter_nvlrqs_updated_at",
                    f.UpdatedAt == default(System.DateTime) ?
                        (object)System.DBNull.Value :
                        (object)f.UpdatedAt);
                p_filter_nvlrqs_updated_at.Direction = System.Data.ParameterDirection.Input;
                p_filter_nvlrqs_updated_at.DbType = DbType.DateTime;
                parameters.Add(p_filter_nvlrqs_updated_at);
                //RegisteredAt
                System.Data.IDataParameter p_filter_nvlrqs_registered_at = dataProvider.GetParameter("@filter_nvlrqs_registered_at",
                    f.RegisteredAt == default(System.DateTime) ?
                        (object)System.DBNull.Value :
                        (object)f.RegisteredAt);
                p_filter_nvlrqs_registered_at.Direction = System.Data.ParameterDirection.Input;
                p_filter_nvlrqs_registered_at.DbType = DbType.DateTime;
                parameters.Add(p_filter_nvlrqs_registered_at);
                // Update
                //Request
                System.Data.IDataParameter p_new_nvlrqs_request_Fk = dataProvider.GetParameter("@new_nvlrqs_request_Fk",
                    instance.Request == default(SOFTTEK.SCMS.Entity.FA.Request) ?
                        (object)System.DBNull.Value :
                        (object)instance.Request.Identifier);
                p_new_nvlrqs_request_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_nvlrqs_request_Fk.DbType = DbType.Int64;
                parameters.Add(p_new_nvlrqs_request_Fk);
                //ExternalIdentifier
                System.Data.IDataParameter p_new_nvlrqs_external_identifier = dataProvider.GetParameter("@new_nvlrqs_external_identifier",
                    f.ExternalIdentifier == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.ExternalIdentifier);
                p_new_nvlrqs_external_identifier.Direction = System.Data.ParameterDirection.Input;
                p_new_nvlrqs_external_identifier.DbType = DbType.String;
                parameters.Add(p_new_nvlrqs_external_identifier);
                //Number
                System.Data.IDataParameter p_new_nvlrqs_number = dataProvider.GetParameter("@new_nvlrqs_number",
                    f.Number == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Number);
                p_new_nvlrqs_number.Direction = System.Data.ParameterDirection.Input;
                p_new_nvlrqs_number.DbType = DbType.String;
                parameters.Add(p_new_nvlrqs_number);
                //Type
                System.Data.IDataParameter p_new_nvlrqs_type_Fk = dataProvider.GetParameter("@new_nvlrqs_type_Fk",
                    instance.Type == default(SOFTTEK.SCMS.Entity.Shared.Parameter<System.String>) ?
                        (object)System.DBNull.Value :
                        (object)instance.Type.Identifier);
                p_new_nvlrqs_type_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_nvlrqs_type_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_nvlrqs_type_Fk);
                //Informed
                System.Data.IDataParameter p_new_nvlrqs_informed_Fk = dataProvider.GetParameter("@new_nvlrqs_informed_Fk",
                    instance.Informed == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)instance.Informed.Identifier);
                p_new_nvlrqs_informed_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_nvlrqs_informed_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_nvlrqs_informed_Fk);
                //Responsible
                System.Data.IDataParameter p_new_nvlrqs_responsible_Fk = dataProvider.GetParameter("@new_nvlrqs_responsible_Fk",
                    instance.Responsible == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)instance.Responsible.Identifier);
                p_new_nvlrqs_responsible_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_nvlrqs_responsible_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_nvlrqs_responsible_Fk);
                //Accountable
                System.Data.IDataParameter p_new_nvlrqs_accountable_Fk = dataProvider.GetParameter("@new_nvlrqs_accountable_Fk",
                    instance.Accountable == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)instance.Accountable.Identifier);
                p_new_nvlrqs_accountable_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_nvlrqs_accountable_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_nvlrqs_accountable_Fk);
                //Details
                System.Data.IDataParameter p_new_nvlrqs_details = dataProvider.GetParameter("@new_nvlrqs_details",
                    f.Details == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Details);
                p_new_nvlrqs_details.Direction = System.Data.ParameterDirection.Input;
                p_new_nvlrqs_details.DbType = DbType.String;
                parameters.Add(p_new_nvlrqs_details);
                //Comments
                System.Data.IDataParameter p_new_nvlrqs_comments = dataProvider.GetParameter("@new_nvlrqs_comments",
                    f.Comments == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Comments);
                p_new_nvlrqs_comments.Direction = System.Data.ParameterDirection.Input;
                p_new_nvlrqs_comments.DbType = DbType.String;
                parameters.Add(p_new_nvlrqs_comments);
                //Topic
                System.Data.IDataParameter p_new_nvlrqs_topic = dataProvider.GetParameter("@new_nvlrqs_topic",
                    f.Topic == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Topic);
                p_new_nvlrqs_topic.Direction = System.Data.ParameterDirection.Input;
                p_new_nvlrqs_topic.DbType = DbType.String;
                parameters.Add(p_new_nvlrqs_topic);
                //Status
                System.Data.IDataParameter p_new_nvlrqs_status = dataProvider.GetParameter("@new_nvlrqs_status",
                    f.Status == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Status);
                p_new_nvlrqs_status.Direction = System.Data.ParameterDirection.Input;
                p_new_nvlrqs_status.DbType = DbType.String;
                parameters.Add(p_new_nvlrqs_status);

                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.FA.NoveltyRequest> mapper = (reader) =>
            {
                long longHelper = default(long);
                int intHelper = default(int);
                DateTime dateHelper = default(DateTime);

                SOFTTEK.SCMS.Entity.FA.NoveltyRequest value = new SOFTTEK.SCMS.Entity.FA.NoveltyRequest
                {
                    Request = reader["v_nvlrqs_request_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.FA.Request { Identifier = long.TryParse(reader["v_nvlrqs_request_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null,
                    ExternalIdentifier = reader["v_nvlrqs_external_identifier"] != null ?
                        reader["v_nvlrqs_external_identifier"].ToString() : string.Empty,
                    Identifier = reader["v_nvlrqs_id_Pk"] != null ?
                        (long.TryParse(reader["v_nvlrqs_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    Number = reader["v_nvlrqs_number"] != null ?
                        reader["v_nvlrqs_number"].ToString() : string.Empty,
                    Type = reader["v_nvlrqs_type_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Parameter<System.String> { Identifier = int.TryParse(reader["v_nvlrqs_type_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Informed = reader["v_nvlrqs_informed_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_nvlrqs_informed_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Responsible = reader["v_nvlrqs_responsible_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_nvlrqs_responsible_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Accountable = reader["v_nvlrqs_accountable_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_nvlrqs_accountable_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Details = reader["v_nvlrqs_details"] != null ?
                        reader["v_nvlrqs_details"].ToString() : string.Empty,
                    Comments = reader["v_nvlrqs_comments"] != null ?
                        reader["v_nvlrqs_comments"].ToString() : string.Empty,
                    Topic = reader["v_nvlrqs_topic"] != null ?
                        reader["v_nvlrqs_topic"].ToString() : string.Empty,
                    Status = reader["v_nvlrqs_status"] != null ?
                        reader["v_nvlrqs_status"].ToString() : string.Empty,
                    UpdatedAt = reader["v_nvlrqs_updated_at"] != null ?
                        (DateTime.TryParse(reader["v_nvlrqs_updated_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                    RegisteredAt = reader["v_nvlrqs_registered_at"] != null ?
                        (DateTime.TryParse(reader["v_nvlrqs_registered_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                };
                return value;
            };


            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            SOFTTEK.SCMS.Entity.FA.NoveltyRequest result = dataProvider.ExecuteForEntityWithFilter(SCMS.FA_SP_Update_NoveltyRequest, filterDelegate, filter, mapper);
            return result;
        }
        #endregion
        #region PurchaseRequest

        /// <summary>
        /// Retrieves a list of PurchaseRequest instances that matches a given filter criteria by the PurchaseRequest object instance 
        /// </summary>
        /// <param name="filter">Object with the information required to filter the set of results</param>
        /// <returns>List of PurchaseRequest instances that matches the filter.</returns>
        public List<SOFTTEK.SCMS.Entity.FA.PurchaseRequest> GetPurchaseRequests(SOFTTEK.SCMS.Entity.FA.PurchaseRequest filter)
        {
            Func<SOFTTEK.SCMS.Entity.FA.PurchaseRequest, List<System.Data.IDataParameter>> filterDelegate = (f) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
                //Token
                System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p_tok.Direction = System.Data.ParameterDirection.Input;
                p_tok.DbType = System.Data.DbType.String;
                parameters.Add(p_tok);
                //ExternalIdentifier
                System.Data.IDataParameter p_filter_prcrqs_external_identifier = dataProvider.GetParameter("@filter_prcrqs_external_identifier",
                    f.ExternalIdentifier == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.ExternalIdentifier);
                p_filter_prcrqs_external_identifier.Direction = System.Data.ParameterDirection.Input;
                p_filter_prcrqs_external_identifier.DbType = DbType.String;
                parameters.Add(p_filter_prcrqs_external_identifier);
                //FixedAsset
                System.Data.IDataParameter p_filter_prcrqs_fixed_asset_Fk = dataProvider.GetParameter("@filter_prcrqs_fixed_asset_Fk",
                    f.FixedAsset == default(SOFTTEK.SCMS.Entity.FA.FixedAsset) ?
                        (object)System.DBNull.Value :
                        (object)f.FixedAsset.Identifier);
                p_filter_prcrqs_fixed_asset_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_prcrqs_fixed_asset_Fk.DbType = DbType.Int64;
                parameters.Add(p_filter_prcrqs_fixed_asset_Fk);
                //Novelty
                System.Data.IDataParameter p_filter_prcrqs_novelty_Fk = dataProvider.GetParameter("@filter_prcrqs_novelty_Fk",
                    f.Novelty == default(SOFTTEK.SCMS.Entity.FA.NoveltyRequest) ?
                        (object)System.DBNull.Value :
                        (object)f.Novelty.Identifier);
                p_filter_prcrqs_novelty_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_prcrqs_novelty_Fk.DbType = DbType.Int64;
                parameters.Add(p_filter_prcrqs_novelty_Fk);
                //Identifier
                System.Data.IDataParameter p_filter_prcrqs_id_Pk = dataProvider.GetParameter("@filter_prcrqs_id_Pk",
                    f.Identifier == default(System.Int64) ?
                        (object)System.DBNull.Value :
                        (object)f.Identifier);
                p_filter_prcrqs_id_Pk.Direction = System.Data.ParameterDirection.Input;
                p_filter_prcrqs_id_Pk.DbType = DbType.Int64;
                parameters.Add(p_filter_prcrqs_id_Pk);
                //Number
                System.Data.IDataParameter p_filter_prcrqs_number = dataProvider.GetParameter("@filter_prcrqs_number",
                    f.Number == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Number);
                p_filter_prcrqs_number.Direction = System.Data.ParameterDirection.Input;
                p_filter_prcrqs_number.DbType = DbType.String;
                parameters.Add(p_filter_prcrqs_number);
                //Type
                System.Data.IDataParameter p_filter_prcrqs_type_Fk = dataProvider.GetParameter("@filter_prcrqs_type_Fk",
                    f.Type == default(SOFTTEK.SCMS.Entity.Shared.Parameter<System.String>) ?
                        (object)System.DBNull.Value :
                        (object)f.Type.Identifier);
                p_filter_prcrqs_type_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_prcrqs_type_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_prcrqs_type_Fk);
                //Informed
                System.Data.IDataParameter p_filter_prcrqs_informed_Fk = dataProvider.GetParameter("@filter_prcrqs_informed_Fk",
                    f.Informed == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Informed.Identifier);
                p_filter_prcrqs_informed_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_prcrqs_informed_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_prcrqs_informed_Fk);
                //Responsible
                System.Data.IDataParameter p_filter_prcrqs_responsible_Fk = dataProvider.GetParameter("@filter_prcrqs_responsible_Fk",
                    f.Responsible == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Responsible.Identifier);
                p_filter_prcrqs_responsible_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_prcrqs_responsible_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_prcrqs_responsible_Fk);
                //Accountable
                System.Data.IDataParameter p_filter_prcrqs_accountable_Fk = dataProvider.GetParameter("@filter_prcrqs_accountable_Fk",
                    f.Accountable == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Accountable.Identifier);
                p_filter_prcrqs_accountable_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_prcrqs_accountable_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_prcrqs_accountable_Fk);
                //Details
                System.Data.IDataParameter p_filter_prcrqs_details = dataProvider.GetParameter("@filter_prcrqs_details",
                    f.Details == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Details);
                p_filter_prcrqs_details.Direction = System.Data.ParameterDirection.Input;
                p_filter_prcrqs_details.DbType = DbType.String;
                parameters.Add(p_filter_prcrqs_details);
                //Comments
                System.Data.IDataParameter p_filter_prcrqs_comments = dataProvider.GetParameter("@filter_prcrqs_comments",
                    f.Comments == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Comments);
                p_filter_prcrqs_comments.Direction = System.Data.ParameterDirection.Input;
                p_filter_prcrqs_comments.DbType = DbType.String;
                parameters.Add(p_filter_prcrqs_comments);
                //Topic
                System.Data.IDataParameter p_filter_prcrqs_topic = dataProvider.GetParameter("@filter_prcrqs_topic",
                    f.Topic == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Topic);
                p_filter_prcrqs_topic.Direction = System.Data.ParameterDirection.Input;
                p_filter_prcrqs_topic.DbType = DbType.String;
                parameters.Add(p_filter_prcrqs_topic);
                //Status
                System.Data.IDataParameter p_filter_prcrqs_status = dataProvider.GetParameter("@filter_prcrqs_status",
                    f.Status == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Status);
                p_filter_prcrqs_status.Direction = System.Data.ParameterDirection.Input;
                p_filter_prcrqs_status.DbType = DbType.String;
                parameters.Add(p_filter_prcrqs_status);
                //UpdatedAt
                System.Data.IDataParameter p_filter_prcrqs_updated_at = dataProvider.GetParameter("@filter_prcrqs_updated_at",
                    f.UpdatedAt == default(System.DateTime) ?
                        (object)System.DBNull.Value :
                        (object)f.UpdatedAt);
                p_filter_prcrqs_updated_at.Direction = System.Data.ParameterDirection.Input;
                p_filter_prcrqs_updated_at.DbType = DbType.DateTime;
                parameters.Add(p_filter_prcrqs_updated_at);
                //RegisteredAt
                System.Data.IDataParameter p_filter_prcrqs_registered_at = dataProvider.GetParameter("@filter_prcrqs_registered_at",
                    f.RegisteredAt == default(System.DateTime) ?
                        (object)System.DBNull.Value :
                        (object)f.RegisteredAt);
                p_filter_prcrqs_registered_at.Direction = System.Data.ParameterDirection.Input;
                p_filter_prcrqs_registered_at.DbType = DbType.DateTime;
                parameters.Add(p_filter_prcrqs_registered_at);
                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.FA.PurchaseRequest> mapper = (reader) =>
            {
                long longHelper = default(long);
                int intHelper = default(int);
                DateTime dateHelper = default(DateTime);

                SOFTTEK.SCMS.Entity.FA.PurchaseRequest value = new SOFTTEK.SCMS.Entity.FA.PurchaseRequest
                {
                    ExternalIdentifier = reader["prcrqs_external_identifier"] != null ?
                        reader["prcrqs_external_identifier"].ToString() : string.Empty,
                    FixedAsset = reader["prcrqs_fixed_asset_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.FA.FixedAsset { Identifier = long.TryParse(reader["prcrqs_fixed_asset_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null,
                    Novelty = reader["prcrqs_novelty_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.FA.NoveltyRequest { Identifier = long.TryParse(reader["prcrqs_novelty_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null,
                    Identifier = reader["prcrqs_id_Pk"] != null ?
                        (long.TryParse(reader["prcrqs_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    Number = reader["prcrqs_number"] != null ?
                        reader["prcrqs_number"].ToString() : string.Empty,
                    Type = reader["prcrqs_type_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Parameter<System.String> { Identifier = int.TryParse(reader["prcrqs_type_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Informed = reader["prcrqs_informed_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["prcrqs_informed_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Responsible = reader["prcrqs_responsible_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["prcrqs_responsible_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Accountable = reader["prcrqs_accountable_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["prcrqs_accountable_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Details = reader["prcrqs_details"] != null ?
                        reader["prcrqs_details"].ToString() : string.Empty,
                    Comments = reader["prcrqs_comments"] != null ?
                        reader["prcrqs_comments"].ToString() : string.Empty,
                    Topic = reader["prcrqs_topic"] != null ?
                        reader["prcrqs_topic"].ToString() : string.Empty,
                    Status = reader["prcrqs_status"] != null ?
                        reader["prcrqs_status"].ToString() : string.Empty,
                    UpdatedAt = reader["prcrqs_updated_at"] != null ?
                        (DateTime.TryParse(reader["prcrqs_updated_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                    RegisteredAt = reader["prcrqs_registered_at"] != null ?
                        (DateTime.TryParse(reader["prcrqs_registered_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                };
                return value;
            };

            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            List<SOFTTEK.SCMS.Entity.FA.PurchaseRequest> result = dataProvider.ExecuteReaderWithFilter(SCMS.FA_SP_Select_PurchaseRequest, filterDelegate, filter, mapper);
            return result;
        }

        /// <summary>
        /// Inserts the instance of the provided PurchaseRequest at the Data Context datasource  
        /// </summary>
        /// <param name="instance">Instance of PurchaseRequest with the entity´s information to store</param>
        /// <returns>Instance of PurchaseRequest with the aditional information provided by the Data Context datasource</returns>
        public SOFTTEK.SCMS.Entity.FA.PurchaseRequest InsertPurchaseRequest(SOFTTEK.SCMS.Entity.FA.PurchaseRequest instance)
        {
            Func<SOFTTEK.SCMS.Entity.FA.PurchaseRequest, List<System.Data.IDataParameter>> input = (i) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();

                //Token
                System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p_tok.Direction = System.Data.ParameterDirection.Input;
                p_tok.DbType = System.Data.DbType.String;
                parameters.Add(p_tok);

                // Insert
                //ExternalIdentifier
                System.Data.IDataParameter p_new_prcrqs_external_identifier = dataProvider.GetParameter("@new_prcrqs_external_identifier",
                    i.ExternalIdentifier == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.ExternalIdentifier);
                p_new_prcrqs_external_identifier.Direction = System.Data.ParameterDirection.Input;
                p_new_prcrqs_external_identifier.DbType = DbType.String;
                parameters.Add(p_new_prcrqs_external_identifier);
                //FixedAsset
                System.Data.IDataParameter p_new_prcrqs_fixed_asset_Fk = dataProvider.GetParameter("@new_prcrqs_fixed_asset_Fk",
                    i.FixedAsset == default(SOFTTEK.SCMS.Entity.FA.FixedAsset) ?
                        (object)System.DBNull.Value :
                        (object)i.FixedAsset.Identifier);
                p_new_prcrqs_fixed_asset_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_prcrqs_fixed_asset_Fk.DbType = DbType.Int64;
                parameters.Add(p_new_prcrqs_fixed_asset_Fk);
                //Novelty
                System.Data.IDataParameter p_new_prcrqs_novelty_Fk = dataProvider.GetParameter("@new_prcrqs_novelty_Fk",
                    i.Novelty == default(SOFTTEK.SCMS.Entity.FA.NoveltyRequest) ?
                        (object)System.DBNull.Value :
                        (object)i.Novelty.Identifier);
                p_new_prcrqs_novelty_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_prcrqs_novelty_Fk.DbType = DbType.Int64;
                parameters.Add(p_new_prcrqs_novelty_Fk);
                //Number
                System.Data.IDataParameter p_new_prcrqs_number = dataProvider.GetParameter("@new_prcrqs_number",
                    i.Number == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Number);
                p_new_prcrqs_number.Direction = System.Data.ParameterDirection.Input;
                p_new_prcrqs_number.DbType = DbType.String;
                parameters.Add(p_new_prcrqs_number);
                //Type
                System.Data.IDataParameter p_new_prcrqs_type_Fk = dataProvider.GetParameter("@new_prcrqs_type_Fk",
                    i.Type == default(SOFTTEK.SCMS.Entity.Shared.Parameter<System.String>) ?
                        (object)System.DBNull.Value :
                        (object)i.Type.Identifier);
                p_new_prcrqs_type_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_prcrqs_type_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_prcrqs_type_Fk);
                //Informed
                System.Data.IDataParameter p_new_prcrqs_informed_Fk = dataProvider.GetParameter("@new_prcrqs_informed_Fk",
                    i.Informed == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)i.Informed.Identifier);
                p_new_prcrqs_informed_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_prcrqs_informed_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_prcrqs_informed_Fk);
                //Responsible
                System.Data.IDataParameter p_new_prcrqs_responsible_Fk = dataProvider.GetParameter("@new_prcrqs_responsible_Fk",
                    i.Responsible == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)i.Responsible.Identifier);
                p_new_prcrqs_responsible_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_prcrqs_responsible_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_prcrqs_responsible_Fk);
                //Accountable
                System.Data.IDataParameter p_new_prcrqs_accountable_Fk = dataProvider.GetParameter("@new_prcrqs_accountable_Fk",
                    i.Accountable == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)i.Accountable.Identifier);
                p_new_prcrqs_accountable_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_prcrqs_accountable_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_prcrqs_accountable_Fk);
                //Details
                System.Data.IDataParameter p_new_prcrqs_details = dataProvider.GetParameter("@new_prcrqs_details",
                    i.Details == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Details);
                p_new_prcrqs_details.Direction = System.Data.ParameterDirection.Input;
                p_new_prcrqs_details.DbType = DbType.String;
                parameters.Add(p_new_prcrqs_details);
                //Comments
                System.Data.IDataParameter p_new_prcrqs_comments = dataProvider.GetParameter("@new_prcrqs_comments",
                    i.Comments == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Comments);
                p_new_prcrqs_comments.Direction = System.Data.ParameterDirection.Input;
                p_new_prcrqs_comments.DbType = DbType.String;
                parameters.Add(p_new_prcrqs_comments);
                //Topic
                System.Data.IDataParameter p_new_prcrqs_topic = dataProvider.GetParameter("@new_prcrqs_topic",
                    i.Topic == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Topic);
                p_new_prcrqs_topic.Direction = System.Data.ParameterDirection.Input;
                p_new_prcrqs_topic.DbType = DbType.String;
                parameters.Add(p_new_prcrqs_topic);
                //Status
                System.Data.IDataParameter p_new_prcrqs_status = dataProvider.GetParameter("@new_prcrqs_status",
                    i.Status == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Status);
                p_new_prcrqs_status.Direction = System.Data.ParameterDirection.Input;
                p_new_prcrqs_status.DbType = DbType.String;
                parameters.Add(p_new_prcrqs_status);

                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.FA.PurchaseRequest> mapper = (reader) =>
            {
                long longHelper = default(long);
                int intHelper = default(int);
                DateTime dateHelper = default(DateTime);

                SOFTTEK.SCMS.Entity.FA.PurchaseRequest value = new SOFTTEK.SCMS.Entity.FA.PurchaseRequest
                {
                    ExternalIdentifier = reader["v_prcrqs_external_identifier"] != null ?
                        reader["v_prcrqs_external_identifier"].ToString() : string.Empty,
                    FixedAsset = reader["v_prcrqs_fixed_asset_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.FA.FixedAsset { Identifier = long.TryParse(reader["v_prcrqs_fixed_asset_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null,
                    Novelty = reader["v_prcrqs_novelty_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.FA.NoveltyRequest { Identifier = long.TryParse(reader["v_prcrqs_novelty_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null,
                    Identifier = reader["v_prcrqs_id_Pk"] != null ?
                        (long.TryParse(reader["v_prcrqs_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    Number = reader["v_prcrqs_number"] != null ?
                        reader["v_prcrqs_number"].ToString() : string.Empty,
                    Type = reader["v_prcrqs_type_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Parameter<System.String> { Identifier = int.TryParse(reader["v_prcrqs_type_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Informed = reader["v_prcrqs_informed_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_prcrqs_informed_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Responsible = reader["v_prcrqs_responsible_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_prcrqs_responsible_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Accountable = reader["v_prcrqs_accountable_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_prcrqs_accountable_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Details = reader["v_prcrqs_details"] != null ?
                        reader["v_prcrqs_details"].ToString() : string.Empty,
                    Comments = reader["v_prcrqs_comments"] != null ?
                        reader["v_prcrqs_comments"].ToString() : string.Empty,
                    Topic = reader["v_prcrqs_topic"] != null ?
                        reader["v_prcrqs_topic"].ToString() : string.Empty,
                    Status = reader["v_prcrqs_status"] != null ?
                        reader["v_prcrqs_status"].ToString() : string.Empty,
                    UpdatedAt = reader["v_prcrqs_updated_at"] != null ?
                        (DateTime.TryParse(reader["v_prcrqs_updated_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                    RegisteredAt = reader["v_prcrqs_registered_at"] != null ?
                        (DateTime.TryParse(reader["v_prcrqs_registered_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                };
                return value;
            };


            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            SOFTTEK.SCMS.Entity.FA.PurchaseRequest result = dataProvider.ExecuteForEntityWithFilter(SCMS.FA_SP_Insert_PurchaseRequest, input, instance, mapper);
            return result;
        }

        /// <summary>
        /// Update the entity's stored information with that provided by the instance of PurchaseRequest at the Data Context datasource  
        /// </summary>
        /// <param name="instance">Instance of PurchaseRequest with the entity´s information to update</param>
        /// <param name="filter">Object with the information required to filter the target stored entity´s information</param>
        /// <returns>Instance of PurchaseRequest with the updated information provided by the Data Context datasource</returns>
        public SOFTTEK.SCMS.Entity.FA.PurchaseRequest UpdatePurchaseRequest(SOFTTEK.SCMS.Entity.FA.PurchaseRequest instance, SOFTTEK.SCMS.Entity.FA.PurchaseRequest filter)
        {
            Func<SOFTTEK.SCMS.Entity.FA.PurchaseRequest, List<System.Data.IDataParameter>> filterDelegate = (f) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();

                //Token
                System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p_tok.Direction = System.Data.ParameterDirection.Input;
                p_tok.DbType = System.Data.DbType.String;
                parameters.Add(p_tok);

                // Filter
                //ExternalIdentifier
                System.Data.IDataParameter p_filter_prcrqs_external_identifier = dataProvider.GetParameter("@filter_prcrqs_external_identifier",
                    f.ExternalIdentifier == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.ExternalIdentifier);
                p_filter_prcrqs_external_identifier.Direction = System.Data.ParameterDirection.Input;
                p_filter_prcrqs_external_identifier.DbType = DbType.String;
                parameters.Add(p_filter_prcrqs_external_identifier);
                //FixedAsset
                System.Data.IDataParameter p_filter_prcrqs_fixed_asset_Fk = dataProvider.GetParameter("@filter_prcrqs_fixed_asset_Fk",
                    f.FixedAsset == default(SOFTTEK.SCMS.Entity.FA.FixedAsset) ?
                        (object)System.DBNull.Value :
                        (object)f.FixedAsset.Identifier);
                p_filter_prcrqs_fixed_asset_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_prcrqs_fixed_asset_Fk.DbType = DbType.Int64;
                parameters.Add(p_filter_prcrqs_fixed_asset_Fk);
                //Novelty
                System.Data.IDataParameter p_filter_prcrqs_novelty_Fk = dataProvider.GetParameter("@filter_prcrqs_novelty_Fk",
                    f.Novelty == default(SOFTTEK.SCMS.Entity.FA.NoveltyRequest) ?
                        (object)System.DBNull.Value :
                        (object)f.Novelty.Identifier);
                p_filter_prcrqs_novelty_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_prcrqs_novelty_Fk.DbType = DbType.Int64;
                parameters.Add(p_filter_prcrqs_novelty_Fk);
                //Identifier
                System.Data.IDataParameter p_filter_prcrqs_id_Pk = dataProvider.GetParameter("@filter_prcrqs_id_Pk",
                    f.Identifier == default(System.Int64) ?
                        (object)System.DBNull.Value :
                        (object)f.Identifier);
                p_filter_prcrqs_id_Pk.Direction = System.Data.ParameterDirection.Input;
                p_filter_prcrqs_id_Pk.DbType = DbType.Int64;
                parameters.Add(p_filter_prcrqs_id_Pk);
                //Number
                System.Data.IDataParameter p_filter_prcrqs_number = dataProvider.GetParameter("@filter_prcrqs_number",
                    f.Number == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Number);
                p_filter_prcrqs_number.Direction = System.Data.ParameterDirection.Input;
                p_filter_prcrqs_number.DbType = DbType.String;
                parameters.Add(p_filter_prcrqs_number);
                //Type
                System.Data.IDataParameter p_filter_prcrqs_type_Fk = dataProvider.GetParameter("@filter_prcrqs_type_Fk",
                    f.Type == default(SOFTTEK.SCMS.Entity.Shared.Parameter<System.String>) ?
                        (object)System.DBNull.Value :
                        (object)f.Type.Identifier);
                p_filter_prcrqs_type_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_prcrqs_type_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_prcrqs_type_Fk);
                //Informed
                System.Data.IDataParameter p_filter_prcrqs_informed_Fk = dataProvider.GetParameter("@filter_prcrqs_informed_Fk",
                    f.Informed == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Informed.Identifier);
                p_filter_prcrqs_informed_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_prcrqs_informed_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_prcrqs_informed_Fk);
                //Responsible
                System.Data.IDataParameter p_filter_prcrqs_responsible_Fk = dataProvider.GetParameter("@filter_prcrqs_responsible_Fk",
                    f.Responsible == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Responsible.Identifier);
                p_filter_prcrqs_responsible_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_prcrqs_responsible_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_prcrqs_responsible_Fk);
                //Accountable
                System.Data.IDataParameter p_filter_prcrqs_accountable_Fk = dataProvider.GetParameter("@filter_prcrqs_accountable_Fk",
                    f.Accountable == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Accountable.Identifier);
                p_filter_prcrqs_accountable_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_prcrqs_accountable_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_prcrqs_accountable_Fk);
                //Details
                System.Data.IDataParameter p_filter_prcrqs_details = dataProvider.GetParameter("@filter_prcrqs_details",
                    f.Details == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Details);
                p_filter_prcrqs_details.Direction = System.Data.ParameterDirection.Input;
                p_filter_prcrqs_details.DbType = DbType.String;
                parameters.Add(p_filter_prcrqs_details);
                //Comments
                System.Data.IDataParameter p_filter_prcrqs_comments = dataProvider.GetParameter("@filter_prcrqs_comments",
                    f.Comments == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Comments);
                p_filter_prcrqs_comments.Direction = System.Data.ParameterDirection.Input;
                p_filter_prcrqs_comments.DbType = DbType.String;
                parameters.Add(p_filter_prcrqs_comments);
                //Topic
                System.Data.IDataParameter p_filter_prcrqs_topic = dataProvider.GetParameter("@filter_prcrqs_topic",
                    f.Topic == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Topic);
                p_filter_prcrqs_topic.Direction = System.Data.ParameterDirection.Input;
                p_filter_prcrqs_topic.DbType = DbType.String;
                parameters.Add(p_filter_prcrqs_topic);
                //Status
                System.Data.IDataParameter p_filter_prcrqs_status = dataProvider.GetParameter("@filter_prcrqs_status",
                    f.Status == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Status);
                p_filter_prcrqs_status.Direction = System.Data.ParameterDirection.Input;
                p_filter_prcrqs_status.DbType = DbType.String;
                parameters.Add(p_filter_prcrqs_status);
                //UpdatedAt
                System.Data.IDataParameter p_filter_prcrqs_updated_at = dataProvider.GetParameter("@filter_prcrqs_updated_at",
                    f.UpdatedAt == default(System.DateTime) ?
                        (object)System.DBNull.Value :
                        (object)f.UpdatedAt);
                p_filter_prcrqs_updated_at.Direction = System.Data.ParameterDirection.Input;
                p_filter_prcrqs_updated_at.DbType = DbType.DateTime;
                parameters.Add(p_filter_prcrqs_updated_at);
                //RegisteredAt
                System.Data.IDataParameter p_filter_prcrqs_registered_at = dataProvider.GetParameter("@filter_prcrqs_registered_at",
                    f.RegisteredAt == default(System.DateTime) ?
                        (object)System.DBNull.Value :
                        (object)f.RegisteredAt);
                p_filter_prcrqs_registered_at.Direction = System.Data.ParameterDirection.Input;
                p_filter_prcrqs_registered_at.DbType = DbType.DateTime;
                parameters.Add(p_filter_prcrqs_registered_at);
                // Update
                //ExternalIdentifier
                System.Data.IDataParameter p_new_prcrqs_external_identifier = dataProvider.GetParameter("@new_prcrqs_external_identifier",
                    f.ExternalIdentifier == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.ExternalIdentifier);
                p_new_prcrqs_external_identifier.Direction = System.Data.ParameterDirection.Input;
                p_new_prcrqs_external_identifier.DbType = DbType.String;
                parameters.Add(p_new_prcrqs_external_identifier);
                //FixedAsset
                System.Data.IDataParameter p_new_prcrqs_fixed_asset_Fk = dataProvider.GetParameter("@new_prcrqs_fixed_asset_Fk",
                    instance.FixedAsset == default(SOFTTEK.SCMS.Entity.FA.FixedAsset) ?
                        (object)System.DBNull.Value :
                        (object)instance.FixedAsset.Identifier);
                p_new_prcrqs_fixed_asset_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_prcrqs_fixed_asset_Fk.DbType = DbType.Int64;
                parameters.Add(p_new_prcrqs_fixed_asset_Fk);
                //Novelty
                System.Data.IDataParameter p_new_prcrqs_novelty_Fk = dataProvider.GetParameter("@new_prcrqs_novelty_Fk",
                    instance.Novelty == default(SOFTTEK.SCMS.Entity.FA.NoveltyRequest) ?
                        (object)System.DBNull.Value :
                        (object)instance.Novelty.Identifier);
                p_new_prcrqs_novelty_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_prcrqs_novelty_Fk.DbType = DbType.Int64;
                parameters.Add(p_new_prcrqs_novelty_Fk);
                //Number
                System.Data.IDataParameter p_new_prcrqs_number = dataProvider.GetParameter("@new_prcrqs_number",
                    f.Number == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Number);
                p_new_prcrqs_number.Direction = System.Data.ParameterDirection.Input;
                p_new_prcrqs_number.DbType = DbType.String;
                parameters.Add(p_new_prcrqs_number);
                //Type
                System.Data.IDataParameter p_new_prcrqs_type_Fk = dataProvider.GetParameter("@new_prcrqs_type_Fk",
                    instance.Type == default(SOFTTEK.SCMS.Entity.Shared.Parameter<System.String>) ?
                        (object)System.DBNull.Value :
                        (object)instance.Type.Identifier);
                p_new_prcrqs_type_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_prcrqs_type_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_prcrqs_type_Fk);
                //Informed
                System.Data.IDataParameter p_new_prcrqs_informed_Fk = dataProvider.GetParameter("@new_prcrqs_informed_Fk",
                    instance.Informed == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)instance.Informed.Identifier);
                p_new_prcrqs_informed_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_prcrqs_informed_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_prcrqs_informed_Fk);
                //Responsible
                System.Data.IDataParameter p_new_prcrqs_responsible_Fk = dataProvider.GetParameter("@new_prcrqs_responsible_Fk",
                    instance.Responsible == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)instance.Responsible.Identifier);
                p_new_prcrqs_responsible_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_prcrqs_responsible_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_prcrqs_responsible_Fk);
                //Accountable
                System.Data.IDataParameter p_new_prcrqs_accountable_Fk = dataProvider.GetParameter("@new_prcrqs_accountable_Fk",
                    instance.Accountable == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)instance.Accountable.Identifier);
                p_new_prcrqs_accountable_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_prcrqs_accountable_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_prcrqs_accountable_Fk);
                //Details
                System.Data.IDataParameter p_new_prcrqs_details = dataProvider.GetParameter("@new_prcrqs_details",
                    f.Details == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Details);
                p_new_prcrqs_details.Direction = System.Data.ParameterDirection.Input;
                p_new_prcrqs_details.DbType = DbType.String;
                parameters.Add(p_new_prcrqs_details);
                //Comments
                System.Data.IDataParameter p_new_prcrqs_comments = dataProvider.GetParameter("@new_prcrqs_comments",
                    f.Comments == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Comments);
                p_new_prcrqs_comments.Direction = System.Data.ParameterDirection.Input;
                p_new_prcrqs_comments.DbType = DbType.String;
                parameters.Add(p_new_prcrqs_comments);
                //Topic
                System.Data.IDataParameter p_new_prcrqs_topic = dataProvider.GetParameter("@new_prcrqs_topic",
                    f.Topic == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Topic);
                p_new_prcrqs_topic.Direction = System.Data.ParameterDirection.Input;
                p_new_prcrqs_topic.DbType = DbType.String;
                parameters.Add(p_new_prcrqs_topic);
                //Status
                System.Data.IDataParameter p_new_prcrqs_status = dataProvider.GetParameter("@new_prcrqs_status",
                    f.Status == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Status);
                p_new_prcrqs_status.Direction = System.Data.ParameterDirection.Input;
                p_new_prcrqs_status.DbType = DbType.String;
                parameters.Add(p_new_prcrqs_status);

                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.FA.PurchaseRequest> mapper = (reader) =>
            {
                long longHelper = default(long);
                int intHelper = default(int);
                DateTime dateHelper = default(DateTime);

                SOFTTEK.SCMS.Entity.FA.PurchaseRequest value = new SOFTTEK.SCMS.Entity.FA.PurchaseRequest
                {
                    ExternalIdentifier = reader["v_prcrqs_external_identifier"] != null ?
                        reader["v_prcrqs_external_identifier"].ToString() : string.Empty,
                    FixedAsset = reader["v_prcrqs_fixed_asset_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.FA.FixedAsset { Identifier = long.TryParse(reader["v_prcrqs_fixed_asset_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null,
                    Novelty = reader["v_prcrqs_novelty_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.FA.NoveltyRequest { Identifier = long.TryParse(reader["v_prcrqs_novelty_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null,
                    Identifier = reader["v_prcrqs_id_Pk"] != null ?
                        (long.TryParse(reader["v_prcrqs_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    Number = reader["v_prcrqs_number"] != null ?
                        reader["v_prcrqs_number"].ToString() : string.Empty,
                    Type = reader["v_prcrqs_type_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Parameter<System.String> { Identifier = int.TryParse(reader["v_prcrqs_type_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Informed = reader["v_prcrqs_informed_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_prcrqs_informed_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Responsible = reader["v_prcrqs_responsible_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_prcrqs_responsible_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Accountable = reader["v_prcrqs_accountable_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_prcrqs_accountable_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Details = reader["v_prcrqs_details"] != null ?
                        reader["v_prcrqs_details"].ToString() : string.Empty,
                    Comments = reader["v_prcrqs_comments"] != null ?
                        reader["v_prcrqs_comments"].ToString() : string.Empty,
                    Topic = reader["v_prcrqs_topic"] != null ?
                        reader["v_prcrqs_topic"].ToString() : string.Empty,
                    Status = reader["v_prcrqs_status"] != null ?
                        reader["v_prcrqs_status"].ToString() : string.Empty,
                    UpdatedAt = reader["v_prcrqs_updated_at"] != null ?
                        (DateTime.TryParse(reader["v_prcrqs_updated_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                    RegisteredAt = reader["v_prcrqs_registered_at"] != null ?
                        (DateTime.TryParse(reader["v_prcrqs_registered_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                };
                return value;
            };


            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            SOFTTEK.SCMS.Entity.FA.PurchaseRequest result = dataProvider.ExecuteForEntityWithFilter(SCMS.FA_SP_Update_PurchaseRequest, filterDelegate, filter, mapper);
            return result;
        }
        #endregion
        #region PhysicalInventoryTakingItem

        /// <summary>
        /// Retrieves a list of PhysicalInventoryTakingItem instances that matches a given filter criteria by the PhysicalInventoryTakingItem object instance 
        /// </summary>
        /// <param name="filter">Object with the information required to filter the set of results</param>
        /// <returns>List of PhysicalInventoryTakingItem instances that matches the filter.</returns>
        public List<SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTakingItem> GetPhysicalInventoryTakingItems(SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTakingItem filter)
        {
            Func<SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTakingItem, List<System.Data.IDataParameter>> filterDelegate = (f) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
                //Token
                System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p_tok.Direction = System.Data.ParameterDirection.Input;
                p_tok.DbType = System.Data.DbType.String;
                parameters.Add(p_tok);
                //Identifier
                System.Data.IDataParameter p_filter_phyinvtknitm_id_Pk = dataProvider.GetParameter("@filter_phyinvtknitm_id_Pk",
                    f.Identifier == default(System.Int64) ?
                        (object)System.DBNull.Value :
                        (object)f.Identifier);
                p_filter_phyinvtknitm_id_Pk.Direction = System.Data.ParameterDirection.Input;
                p_filter_phyinvtknitm_id_Pk.DbType = DbType.Int64;
                parameters.Add(p_filter_phyinvtknitm_id_Pk);
                //FixedAsset
                System.Data.IDataParameter p_filter_phyinvtknitm_fixed_asset_Fk = dataProvider.GetParameter("@filter_phyinvtknitm_fixed_asset_Fk",
                    f.FixedAsset == default(SOFTTEK.SCMS.Entity.FA.FixedAsset) ?
                        (object)System.DBNull.Value :
                        (object)f.FixedAsset.Identifier);
                p_filter_phyinvtknitm_fixed_asset_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_phyinvtknitm_fixed_asset_Fk.DbType = DbType.Int64;
                parameters.Add(p_filter_phyinvtknitm_fixed_asset_Fk);
                //FixedAssetState
                System.Data.IDataParameter p_filter_phyinvtknitm_fixed_asset_state = dataProvider.GetParameter("@filter_phyinvtknitm_fixed_asset_state",
                    f.FixedAssetState == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.FixedAssetState);
                p_filter_phyinvtknitm_fixed_asset_state.Direction = System.Data.ParameterDirection.Input;
                p_filter_phyinvtknitm_fixed_asset_state.DbType = DbType.String;
                parameters.Add(p_filter_phyinvtknitm_fixed_asset_state);
                //Comments
                System.Data.IDataParameter p_filter_phyinvtknitm_comments = dataProvider.GetParameter("@filter_phyinvtknitm_comments",
                    f.Comments == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Comments);
                p_filter_phyinvtknitm_comments.Direction = System.Data.ParameterDirection.Input;
                p_filter_phyinvtknitm_comments.DbType = DbType.String;
                parameters.Add(p_filter_phyinvtknitm_comments);
                //Responsible
                System.Data.IDataParameter p_filter_phyinvtknitm_responsible_Fk = dataProvider.GetParameter("@filter_phyinvtknitm_responsible_Fk",
                    f.Responsible == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Responsible.Identifier);
                p_filter_phyinvtknitm_responsible_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_phyinvtknitm_responsible_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_phyinvtknitm_responsible_Fk);
                //PhysicalInventoryTaking
                System.Data.IDataParameter p_filter_phyinvtknitm_physical_inventory_taking_Fk = dataProvider.GetParameter("@filter_phyinvtknitm_physical_inventory_taking_Fk",
                    f.PhysicalInventoryTaking == default(SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking) ?
                        (object)System.DBNull.Value :
                        (object)f.PhysicalInventoryTaking.Identifier);
                p_filter_phyinvtknitm_physical_inventory_taking_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_phyinvtknitm_physical_inventory_taking_Fk.DbType = DbType.Int64;
                parameters.Add(p_filter_phyinvtknitm_physical_inventory_taking_Fk);
                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTakingItem> mapper = (reader) =>
            {
                long longHelper = default(long);
                int intHelper = default(int);
                DateTime dateHelper = default(DateTime);

                SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTakingItem value = new SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTakingItem
                {
                    Identifier = reader["phyinvtknitm_id_Pk"] != null ?
                        (long.TryParse(reader["phyinvtknitm_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    FixedAsset = reader["phyinvtknitm_fixed_asset_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.FA.FixedAsset { Identifier = long.TryParse(reader["phyinvtknitm_fixed_asset_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null,
                    VerifiedAt = reader["phyinvtknitm_verified_at"] != null ?
                        (DateTime.TryParse(reader["phyinvtknitm_verified_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                    FixedAssetState = reader["phyinvtknitm_fixed_asset_state"] != null ?
                        reader["phyinvtknitm_fixed_asset_state"].ToString() : string.Empty,
                    Comments = reader["phyinvtknitm_comments"] != null ?
                        reader["phyinvtknitm_comments"].ToString() : string.Empty,
                    Responsible = reader["phyinvtknitm_responsible_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["phyinvtknitm_responsible_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    PhysicalInventoryTaking = reader["phyinvtknitm_physical_inventory_taking_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking { Identifier = long.TryParse(reader["phyinvtknitm_physical_inventory_taking_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null,
                };
                return value;
            };

            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            List<SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTakingItem> result = dataProvider.ExecuteReaderWithFilter(SCMS.FA_SP_Select_PhysicalInventoryTakingItem, filterDelegate, filter, mapper);
            return result;
        }

        /// <summary>
        /// Inserts the instance of the provided PhysicalInventoryTakingItem at the Data Context datasource  
        /// </summary>
        /// <param name="instance">Instance of PhysicalInventoryTakingItem with the entity´s information to store</param>
        /// <returns>Instance of PhysicalInventoryTakingItem with the aditional information provided by the Data Context datasource</returns>
        public SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTakingItem InsertPhysicalInventoryTakingItem(SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTakingItem instance)
        {
            Func<SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTakingItem, List<System.Data.IDataParameter>> input = (i) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();

                //Token
                System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p_tok.Direction = System.Data.ParameterDirection.Input;
                p_tok.DbType = System.Data.DbType.String;
                parameters.Add(p_tok);

                // Insert
                //FixedAsset
                System.Data.IDataParameter p_new_phyinvtknitm_fixed_asset_Fk = dataProvider.GetParameter("@new_phyinvtknitm_fixed_asset_Fk",
                    i.FixedAsset == default(SOFTTEK.SCMS.Entity.FA.FixedAsset) ?
                        (object)System.DBNull.Value :
                        (object)i.FixedAsset.Identifier);
                p_new_phyinvtknitm_fixed_asset_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_phyinvtknitm_fixed_asset_Fk.DbType = DbType.Int64;
                parameters.Add(p_new_phyinvtknitm_fixed_asset_Fk);
                //FixedAssetState
                System.Data.IDataParameter p_new_phyinvtknitm_fixed_asset_state = dataProvider.GetParameter("@new_phyinvtknitm_fixed_asset_state",
                    i.FixedAssetState == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.FixedAssetState);
                p_new_phyinvtknitm_fixed_asset_state.Direction = System.Data.ParameterDirection.Input;
                p_new_phyinvtknitm_fixed_asset_state.DbType = DbType.String;
                parameters.Add(p_new_phyinvtknitm_fixed_asset_state);
                //Comments
                System.Data.IDataParameter p_new_phyinvtknitm_comments = dataProvider.GetParameter("@new_phyinvtknitm_comments",
                    i.Comments == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Comments);
                p_new_phyinvtknitm_comments.Direction = System.Data.ParameterDirection.Input;
                p_new_phyinvtknitm_comments.DbType = DbType.String;
                parameters.Add(p_new_phyinvtknitm_comments);
                //Responsible
                System.Data.IDataParameter p_new_phyinvtknitm_responsible_Fk = dataProvider.GetParameter("@new_phyinvtknitm_responsible_Fk",
                    i.Responsible == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)i.Responsible.Identifier);
                p_new_phyinvtknitm_responsible_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_phyinvtknitm_responsible_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_phyinvtknitm_responsible_Fk);
                //PhysicalInventoryTaking
                System.Data.IDataParameter p_new_phyinvtknitm_physical_inventory_taking_Fk = dataProvider.GetParameter("@new_phyinvtknitm_physical_inventory_taking_Fk",
                    i.PhysicalInventoryTaking == default(SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking) ?
                        (object)System.DBNull.Value :
                        (object)i.PhysicalInventoryTaking.Identifier);
                p_new_phyinvtknitm_physical_inventory_taking_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_phyinvtknitm_physical_inventory_taking_Fk.DbType = DbType.Int64;
                parameters.Add(p_new_phyinvtknitm_physical_inventory_taking_Fk);

                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTakingItem> mapper = (reader) =>
            {
                long longHelper = default(long);
                int intHelper = default(int);
                DateTime dateHelper = default(DateTime);

                SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTakingItem value = new SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTakingItem
                {
                    Identifier = reader["v_phyinvtknitm_id_Pk"] != null ?
                        (long.TryParse(reader["v_phyinvtknitm_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    FixedAsset = reader["v_phyinvtknitm_fixed_asset_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.FA.FixedAsset { Identifier = long.TryParse(reader["v_phyinvtknitm_fixed_asset_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null,
                    VerifiedAt = reader["v_phyinvtknitm_verified_at"] != null ?
                        (DateTime.TryParse(reader["v_phyinvtknitm_verified_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                    FixedAssetState = reader["v_phyinvtknitm_fixed_asset_state"] != null ?
                        reader["v_phyinvtknitm_fixed_asset_state"].ToString() : string.Empty,
                    Comments = reader["v_phyinvtknitm_comments"] != null ?
                        reader["v_phyinvtknitm_comments"].ToString() : string.Empty,
                    Responsible = reader["v_phyinvtknitm_responsible_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_phyinvtknitm_responsible_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    PhysicalInventoryTaking = reader["v_phyinvtknitm_physical_inventory_taking_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking { Identifier = long.TryParse(reader["v_phyinvtknitm_physical_inventory_taking_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null,
                };
                return value;
            };


            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTakingItem result = dataProvider.ExecuteForEntityWithFilter(SCMS.FA_SP_Insert_PhysicalInventoryTakingItem, input, instance, mapper);
            return result;
        }

        /// <summary>
        /// Update the entity's stored information with that provided by the instance of PhysicalInventoryTakingItem at the Data Context datasource  
        /// </summary>
        /// <param name="instance">Instance of PhysicalInventoryTakingItem with the entity´s information to update</param>
        /// <param name="filter">Object with the information required to filter the target stored entity´s information</param>
        /// <returns>Instance of PhysicalInventoryTakingItem with the updated information provided by the Data Context datasource</returns>
        public SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTakingItem UpdatePhysicalInventoryTakingItem(SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTakingItem instance, SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTakingItem filter)
        {
            Func<SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTakingItem, List<System.Data.IDataParameter>> filterDelegate = (f) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();

                //Token
                System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p_tok.Direction = System.Data.ParameterDirection.Input;
                p_tok.DbType = System.Data.DbType.String;
                parameters.Add(p_tok);

                // Filter
                //Identifier
                System.Data.IDataParameter p_filter_phyinvtknitm_id_Pk = dataProvider.GetParameter("@filter_phyinvtknitm_id_Pk",
                    f.Identifier == default(System.Int64) ?
                        (object)System.DBNull.Value :
                        (object)f.Identifier);
                p_filter_phyinvtknitm_id_Pk.Direction = System.Data.ParameterDirection.Input;
                p_filter_phyinvtknitm_id_Pk.DbType = DbType.Int64;
                parameters.Add(p_filter_phyinvtknitm_id_Pk);
                //FixedAsset
                System.Data.IDataParameter p_filter_phyinvtknitm_fixed_asset_Fk = dataProvider.GetParameter("@filter_phyinvtknitm_fixed_asset_Fk",
                    f.FixedAsset == default(SOFTTEK.SCMS.Entity.FA.FixedAsset) ?
                        (object)System.DBNull.Value :
                        (object)f.FixedAsset.Identifier);
                p_filter_phyinvtknitm_fixed_asset_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_phyinvtknitm_fixed_asset_Fk.DbType = DbType.Int64;
                parameters.Add(p_filter_phyinvtknitm_fixed_asset_Fk);
                //FixedAssetState
                System.Data.IDataParameter p_filter_phyinvtknitm_fixed_asset_state = dataProvider.GetParameter("@filter_phyinvtknitm_fixed_asset_state",
                    f.FixedAssetState == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.FixedAssetState);
                p_filter_phyinvtknitm_fixed_asset_state.Direction = System.Data.ParameterDirection.Input;
                p_filter_phyinvtknitm_fixed_asset_state.DbType = DbType.String;
                parameters.Add(p_filter_phyinvtknitm_fixed_asset_state);
                //Comments
                System.Data.IDataParameter p_filter_phyinvtknitm_comments = dataProvider.GetParameter("@filter_phyinvtknitm_comments",
                    f.Comments == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Comments);
                p_filter_phyinvtknitm_comments.Direction = System.Data.ParameterDirection.Input;
                p_filter_phyinvtknitm_comments.DbType = DbType.String;
                parameters.Add(p_filter_phyinvtknitm_comments);
                //Responsible
                System.Data.IDataParameter p_filter_phyinvtknitm_responsible_Fk = dataProvider.GetParameter("@filter_phyinvtknitm_responsible_Fk",
                    f.Responsible == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Responsible.Identifier);
                p_filter_phyinvtknitm_responsible_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_phyinvtknitm_responsible_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_phyinvtknitm_responsible_Fk);
                //PhysicalInventoryTaking
                System.Data.IDataParameter p_filter_phyinvtknitm_physical_inventory_taking_Fk = dataProvider.GetParameter("@filter_phyinvtknitm_physical_inventory_taking_Fk",
                    f.PhysicalInventoryTaking == default(SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking) ?
                        (object)System.DBNull.Value :
                        (object)f.PhysicalInventoryTaking.Identifier);
                p_filter_phyinvtknitm_physical_inventory_taking_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_phyinvtknitm_physical_inventory_taking_Fk.DbType = DbType.Int64;
                parameters.Add(p_filter_phyinvtknitm_physical_inventory_taking_Fk);
                // Update
                //FixedAsset
                System.Data.IDataParameter p_new_phyinvtknitm_fixed_asset_Fk = dataProvider.GetParameter("@new_phyinvtknitm_fixed_asset_Fk",
                    instance.FixedAsset == default(SOFTTEK.SCMS.Entity.FA.FixedAsset) ?
                        (object)System.DBNull.Value :
                        (object)instance.FixedAsset.Identifier);
                p_new_phyinvtknitm_fixed_asset_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_phyinvtknitm_fixed_asset_Fk.DbType = DbType.Int64;
                parameters.Add(p_new_phyinvtknitm_fixed_asset_Fk);
                //FixedAssetState
                System.Data.IDataParameter p_new_phyinvtknitm_fixed_asset_state = dataProvider.GetParameter("@new_phyinvtknitm_fixed_asset_state",
                    f.FixedAssetState == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.FixedAssetState);
                p_new_phyinvtknitm_fixed_asset_state.Direction = System.Data.ParameterDirection.Input;
                p_new_phyinvtknitm_fixed_asset_state.DbType = DbType.String;
                parameters.Add(p_new_phyinvtknitm_fixed_asset_state);
                //Comments
                System.Data.IDataParameter p_new_phyinvtknitm_comments = dataProvider.GetParameter("@new_phyinvtknitm_comments",
                    f.Comments == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Comments);
                p_new_phyinvtknitm_comments.Direction = System.Data.ParameterDirection.Input;
                p_new_phyinvtknitm_comments.DbType = DbType.String;
                parameters.Add(p_new_phyinvtknitm_comments);
                //Responsible
                System.Data.IDataParameter p_new_phyinvtknitm_responsible_Fk = dataProvider.GetParameter("@new_phyinvtknitm_responsible_Fk",
                    instance.Responsible == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)instance.Responsible.Identifier);
                p_new_phyinvtknitm_responsible_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_phyinvtknitm_responsible_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_phyinvtknitm_responsible_Fk);
                //PhysicalInventoryTaking
                System.Data.IDataParameter p_new_phyinvtknitm_physical_inventory_taking_Fk = dataProvider.GetParameter("@new_phyinvtknitm_physical_inventory_taking_Fk",
                    instance.PhysicalInventoryTaking == default(SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking) ?
                        (object)System.DBNull.Value :
                        (object)instance.PhysicalInventoryTaking.Identifier);
                p_new_phyinvtknitm_physical_inventory_taking_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_phyinvtknitm_physical_inventory_taking_Fk.DbType = DbType.Int64;
                parameters.Add(p_new_phyinvtknitm_physical_inventory_taking_Fk);

                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTakingItem> mapper = (reader) =>
            {
                long longHelper = default(long);
                int intHelper = default(int);
                DateTime dateHelper = default(DateTime);

                SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTakingItem value = new SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTakingItem
                {
                    Identifier = reader["v_phyinvtknitm_id_Pk"] != null ?
                        (long.TryParse(reader["v_phyinvtknitm_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    FixedAsset = reader["v_phyinvtknitm_fixed_asset_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.FA.FixedAsset { Identifier = long.TryParse(reader["v_phyinvtknitm_fixed_asset_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null,
                    VerifiedAt = reader["v_phyinvtknitm_verified_at"] != null ?
                        (DateTime.TryParse(reader["v_phyinvtknitm_verified_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                    FixedAssetState = reader["v_phyinvtknitm_fixed_asset_state"] != null ?
                        reader["v_phyinvtknitm_fixed_asset_state"].ToString() : string.Empty,
                    Comments = reader["v_phyinvtknitm_comments"] != null ?
                        reader["v_phyinvtknitm_comments"].ToString() : string.Empty,
                    Responsible = reader["v_phyinvtknitm_responsible_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_phyinvtknitm_responsible_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    PhysicalInventoryTaking = reader["v_phyinvtknitm_physical_inventory_taking_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking { Identifier = long.TryParse(reader["v_phyinvtknitm_physical_inventory_taking_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null,
                };
                return value;
            };


            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTakingItem result = dataProvider.ExecuteForEntityWithFilter(SCMS.FA_SP_Update_PhysicalInventoryTakingItem, filterDelegate, filter, mapper);
            return result;
        }
        #endregion
        #region PhysicalInventoryTaking

        /// <summary>
        /// Retrieves a list of PhysicalInventoryTaking instances that matches a given filter criteria by the PhysicalInventoryTaking object instance 
        /// </summary>
        /// <param name="filter">Object with the information required to filter the set of results</param>
        /// <returns>List of PhysicalInventoryTaking instances that matches the filter.</returns>
        public List<SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking> GetPhysicalInventoryTakings(SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking filter)
        {
            Func<SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking, List<System.Data.IDataParameter>> filterDelegate = (f) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
                //Token
                System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p_tok.Direction = System.Data.ParameterDirection.Input;
                p_tok.DbType = System.Data.DbType.String;
                parameters.Add(p_tok);
                //Identifier
                System.Data.IDataParameter p_filter_phyinvtkn_id_Pk = dataProvider.GetParameter("@filter_phyinvtkn_id_Pk",
                    f.Identifier == default(System.Int64) ?
                        (object)System.DBNull.Value :
                        (object)f.Identifier);
                p_filter_phyinvtkn_id_Pk.Direction = System.Data.ParameterDirection.Input;
                p_filter_phyinvtkn_id_Pk.DbType = DbType.Int64;
                parameters.Add(p_filter_phyinvtkn_id_Pk);
                //Accountable
                System.Data.IDataParameter p_filter_phyinvtkn_accountable_Fk = dataProvider.GetParameter("@filter_phyinvtkn_accountable_Fk",
                    f.Accountable == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Accountable.Identifier);
                p_filter_phyinvtkn_accountable_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_phyinvtkn_accountable_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_phyinvtkn_accountable_Fk);
                //Responsible
                System.Data.IDataParameter p_filter_phyinvtkn_responsible_Fk = dataProvider.GetParameter("@filter_phyinvtkn_responsible_Fk",
                    f.Responsible == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Responsible.Identifier);
                p_filter_phyinvtkn_responsible_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_phyinvtkn_responsible_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_phyinvtkn_responsible_Fk);
                //Informed
                System.Data.IDataParameter p_filter_phyinvtkn_informed_Fk = dataProvider.GetParameter("@filter_phyinvtkn_informed_Fk",
                    f.Informed == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Informed.Identifier);
                p_filter_phyinvtkn_informed_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_phyinvtkn_informed_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_phyinvtkn_informed_Fk);
                //WorkOrder
                System.Data.IDataParameter p_filter_phyinvtkn_work_order_Fk = dataProvider.GetParameter("@filter_phyinvtkn_work_order_Fk",
                    f.WorkOrder == default(SOFTTEK.SCMS.Entity.FA.WorkOrder) ?
                        (object)System.DBNull.Value :
                        (object)f.WorkOrder.Identifier);
                p_filter_phyinvtkn_work_order_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_phyinvtkn_work_order_Fk.DbType = DbType.Int64;
                parameters.Add(p_filter_phyinvtkn_work_order_Fk);
                //Location
                System.Data.IDataParameter p_filter_phyinvtkn_location = dataProvider.GetParameter("@filter_phyinvtkn_location",
                    f.Location == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Location);
                p_filter_phyinvtkn_location.Direction = System.Data.ParameterDirection.Input;
                p_filter_phyinvtkn_location.DbType = DbType.String;
                parameters.Add(p_filter_phyinvtkn_location);
                //Status
                System.Data.IDataParameter p_filter_phyinvtkn_status = dataProvider.GetParameter("@filter_phyinvtkn_status",
                    f.Status == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Status);
                p_filter_phyinvtkn_status.Direction = System.Data.ParameterDirection.Input;
                p_filter_phyinvtkn_status.DbType = DbType.String;
                parameters.Add(p_filter_phyinvtkn_status);
                //Comments
                System.Data.IDataParameter p_filter_phyinvtkn_comments = dataProvider.GetParameter("@filter_phyinvtkn_comments",
                    f.Comments == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Comments);
                p_filter_phyinvtkn_comments.Direction = System.Data.ParameterDirection.Input;
                p_filter_phyinvtkn_comments.DbType = DbType.String;
                parameters.Add(p_filter_phyinvtkn_comments);
                //UpdatedAt
                System.Data.IDataParameter p_filter_phyinvtkn_updated_at = dataProvider.GetParameter("@filter_phyinvtkn_updated_at",
                    f.UpdatedAt == default(System.DateTime) ?
                        (object)System.DBNull.Value :
                        (object)f.UpdatedAt);
                p_filter_phyinvtkn_updated_at.Direction = System.Data.ParameterDirection.Input;
                p_filter_phyinvtkn_updated_at.DbType = DbType.DateTime;
                parameters.Add(p_filter_phyinvtkn_updated_at);
                //RegisteredAt
                System.Data.IDataParameter p_filter_phyinvtkn_registered_at = dataProvider.GetParameter("@filter_phyinvtkn_registered_at",
                    f.RegisteredAt == default(System.DateTime) ?
                        (object)System.DBNull.Value :
                        (object)f.RegisteredAt);
                p_filter_phyinvtkn_registered_at.Direction = System.Data.ParameterDirection.Input;
                p_filter_phyinvtkn_registered_at.DbType = DbType.DateTime;
                parameters.Add(p_filter_phyinvtkn_registered_at);
                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking> mapper = (reader) =>
            {
                long longHelper = default(long);
                int intHelper = default(int);
                DateTime dateHelper = default(DateTime);

                SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking value = new SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking
                {
                    Identifier = reader["phyinvtkn_id_Pk"] != null ?
                        (long.TryParse(reader["phyinvtkn_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    Accountable = reader["phyinvtkn_accountable_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["phyinvtkn_accountable_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Responsible = reader["phyinvtkn_responsible_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["phyinvtkn_responsible_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Informed = reader["phyinvtkn_informed_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["phyinvtkn_informed_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    WorkOrder = reader["phyinvtkn_work_order_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.FA.WorkOrder { Identifier = long.TryParse(reader["phyinvtkn_work_order_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null,
                    Location = reader["phyinvtkn_location"] != null ?
                        reader["phyinvtkn_location"].ToString() : string.Empty,
                    Status = reader["phyinvtkn_status"] != null ?
                        reader["phyinvtkn_status"].ToString() : string.Empty,
                    Comments = reader["phyinvtkn_comments"] != null ?
                        reader["phyinvtkn_comments"].ToString() : string.Empty,
                    UpdatedAt = reader["phyinvtkn_updated_at"] != null ?
                        (DateTime.TryParse(reader["phyinvtkn_updated_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                    RegisteredAt = reader["phyinvtkn_registered_at"] != null ?
                        (DateTime.TryParse(reader["phyinvtkn_registered_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                };
                return value;
            };

            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            List<SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking> result = dataProvider.ExecuteReaderWithFilter(SCMS.FA_SP_Select_PhysicalInventoryTaking, filterDelegate, filter, mapper);
            return result;
        }

        /// <summary>
        /// Inserts the instance of the provided PhysicalInventoryTaking at the Data Context datasource  
        /// </summary>
        /// <param name="instance">Instance of PhysicalInventoryTaking with the entity´s information to store</param>
        /// <returns>Instance of PhysicalInventoryTaking with the aditional information provided by the Data Context datasource</returns>
        public SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking InsertPhysicalInventoryTaking(SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking instance)
        {
            Func<SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking, List<System.Data.IDataParameter>> input = (i) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();

                //Token
                System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p_tok.Direction = System.Data.ParameterDirection.Input;
                p_tok.DbType = System.Data.DbType.String;
                parameters.Add(p_tok);

                // Insert
                //Accountable
                System.Data.IDataParameter p_new_phyinvtkn_accountable_Fk = dataProvider.GetParameter("@new_phyinvtkn_accountable_Fk",
                    i.Accountable == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)i.Accountable.Identifier);
                p_new_phyinvtkn_accountable_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_phyinvtkn_accountable_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_phyinvtkn_accountable_Fk);
                //Responsible
                System.Data.IDataParameter p_new_phyinvtkn_responsible_Fk = dataProvider.GetParameter("@new_phyinvtkn_responsible_Fk",
                    i.Responsible == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)i.Responsible.Identifier);
                p_new_phyinvtkn_responsible_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_phyinvtkn_responsible_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_phyinvtkn_responsible_Fk);
                //Informed
                System.Data.IDataParameter p_new_phyinvtkn_informed_Fk = dataProvider.GetParameter("@new_phyinvtkn_informed_Fk",
                    i.Informed == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)i.Informed.Identifier);
                p_new_phyinvtkn_informed_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_phyinvtkn_informed_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_phyinvtkn_informed_Fk);
                //WorkOrder
                System.Data.IDataParameter p_new_phyinvtkn_work_order_Fk = dataProvider.GetParameter("@new_phyinvtkn_work_order_Fk",
                    i.WorkOrder == default(SOFTTEK.SCMS.Entity.FA.WorkOrder) ?
                        (object)System.DBNull.Value :
                        (object)i.WorkOrder.Identifier);
                p_new_phyinvtkn_work_order_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_phyinvtkn_work_order_Fk.DbType = DbType.Int64;
                parameters.Add(p_new_phyinvtkn_work_order_Fk);
                //Location
                System.Data.IDataParameter p_new_phyinvtkn_location = dataProvider.GetParameter("@new_phyinvtkn_location",
                    i.Location == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Location);
                p_new_phyinvtkn_location.Direction = System.Data.ParameterDirection.Input;
                p_new_phyinvtkn_location.DbType = DbType.String;
                parameters.Add(p_new_phyinvtkn_location);
                //Status
                System.Data.IDataParameter p_new_phyinvtkn_status = dataProvider.GetParameter("@new_phyinvtkn_status",
                    i.Status == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Status);
                p_new_phyinvtkn_status.Direction = System.Data.ParameterDirection.Input;
                p_new_phyinvtkn_status.DbType = DbType.String;
                parameters.Add(p_new_phyinvtkn_status);
                //Comments
                System.Data.IDataParameter p_new_phyinvtkn_comments = dataProvider.GetParameter("@new_phyinvtkn_comments",
                    i.Comments == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Comments);
                p_new_phyinvtkn_comments.Direction = System.Data.ParameterDirection.Input;
                p_new_phyinvtkn_comments.DbType = DbType.String;
                parameters.Add(p_new_phyinvtkn_comments);

                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking> mapper = (reader) =>
            {
                long longHelper = default(long);
                int intHelper = default(int);
                DateTime dateHelper = default(DateTime);

                SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking value = new SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking
                {
                    Identifier = reader["v_phyinvtkn_id_Pk"] != null ?
                        (long.TryParse(reader["v_phyinvtkn_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    Accountable = reader["v_phyinvtkn_accountable_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_phyinvtkn_accountable_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Responsible = reader["v_phyinvtkn_responsible_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_phyinvtkn_responsible_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Informed = reader["v_phyinvtkn_informed_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_phyinvtkn_informed_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    WorkOrder = reader["v_phyinvtkn_work_order_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.FA.WorkOrder { Identifier = long.TryParse(reader["v_phyinvtkn_work_order_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null,
                    Location = reader["v_phyinvtkn_location"] != null ?
                        reader["v_phyinvtkn_location"].ToString() : string.Empty,
                    Status = reader["v_phyinvtkn_status"] != null ?
                        reader["v_phyinvtkn_status"].ToString() : string.Empty,
                    Comments = reader["v_phyinvtkn_comments"] != null ?
                        reader["v_phyinvtkn_comments"].ToString() : string.Empty,
                    UpdatedAt = reader["v_phyinvtkn_updated_at"] != null ?
                        (DateTime.TryParse(reader["v_phyinvtkn_updated_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                    RegisteredAt = reader["v_phyinvtkn_registered_at"] != null ?
                        (DateTime.TryParse(reader["v_phyinvtkn_registered_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                };
                return value;
            };


            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking result = dataProvider.ExecuteForEntityWithFilter(SCMS.FA_SP_Insert_PhysicalInventoryTaking, input, instance, mapper);
            return result;
        }

        /// <summary>
        /// Update the entity's stored information with that provided by the instance of PhysicalInventoryTaking at the Data Context datasource  
        /// </summary>
        /// <param name="instance">Instance of PhysicalInventoryTaking with the entity´s information to update</param>
        /// <param name="filter">Object with the information required to filter the target stored entity´s information</param>
        /// <returns>Instance of PhysicalInventoryTaking with the updated information provided by the Data Context datasource</returns>
        public SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking UpdatePhysicalInventoryTaking(SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking instance, SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking filter)
        {
            Func<SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking, List<System.Data.IDataParameter>> filterDelegate = (f) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();

                //Token
                System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p_tok.Direction = System.Data.ParameterDirection.Input;
                p_tok.DbType = System.Data.DbType.String;
                parameters.Add(p_tok);

                // Filter
                //Identifier
                System.Data.IDataParameter p_filter_phyinvtkn_id_Pk = dataProvider.GetParameter("@filter_phyinvtkn_id_Pk",
                    f.Identifier == default(System.Int64) ?
                        (object)System.DBNull.Value :
                        (object)f.Identifier);
                p_filter_phyinvtkn_id_Pk.Direction = System.Data.ParameterDirection.Input;
                p_filter_phyinvtkn_id_Pk.DbType = DbType.Int64;
                parameters.Add(p_filter_phyinvtkn_id_Pk);
                //Accountable
                System.Data.IDataParameter p_filter_phyinvtkn_accountable_Fk = dataProvider.GetParameter("@filter_phyinvtkn_accountable_Fk",
                    f.Accountable == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Accountable.Identifier);
                p_filter_phyinvtkn_accountable_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_phyinvtkn_accountable_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_phyinvtkn_accountable_Fk);
                //Responsible
                System.Data.IDataParameter p_filter_phyinvtkn_responsible_Fk = dataProvider.GetParameter("@filter_phyinvtkn_responsible_Fk",
                    f.Responsible == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Responsible.Identifier);
                p_filter_phyinvtkn_responsible_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_phyinvtkn_responsible_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_phyinvtkn_responsible_Fk);
                //Informed
                System.Data.IDataParameter p_filter_phyinvtkn_informed_Fk = dataProvider.GetParameter("@filter_phyinvtkn_informed_Fk",
                    f.Informed == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Informed.Identifier);
                p_filter_phyinvtkn_informed_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_phyinvtkn_informed_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_phyinvtkn_informed_Fk);
                //WorkOrder
                System.Data.IDataParameter p_filter_phyinvtkn_work_order_Fk = dataProvider.GetParameter("@filter_phyinvtkn_work_order_Fk",
                    f.WorkOrder == default(SOFTTEK.SCMS.Entity.FA.WorkOrder) ?
                        (object)System.DBNull.Value :
                        (object)f.WorkOrder.Identifier);
                p_filter_phyinvtkn_work_order_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_phyinvtkn_work_order_Fk.DbType = DbType.Int64;
                parameters.Add(p_filter_phyinvtkn_work_order_Fk);
                //Location
                System.Data.IDataParameter p_filter_phyinvtkn_location = dataProvider.GetParameter("@filter_phyinvtkn_location",
                    f.Location == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Location);
                p_filter_phyinvtkn_location.Direction = System.Data.ParameterDirection.Input;
                p_filter_phyinvtkn_location.DbType = DbType.String;
                parameters.Add(p_filter_phyinvtkn_location);
                //Status
                System.Data.IDataParameter p_filter_phyinvtkn_status = dataProvider.GetParameter("@filter_phyinvtkn_status",
                    f.Status == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Status);
                p_filter_phyinvtkn_status.Direction = System.Data.ParameterDirection.Input;
                p_filter_phyinvtkn_status.DbType = DbType.String;
                parameters.Add(p_filter_phyinvtkn_status);
                //Comments
                System.Data.IDataParameter p_filter_phyinvtkn_comments = dataProvider.GetParameter("@filter_phyinvtkn_comments",
                    f.Comments == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Comments);
                p_filter_phyinvtkn_comments.Direction = System.Data.ParameterDirection.Input;
                p_filter_phyinvtkn_comments.DbType = DbType.String;
                parameters.Add(p_filter_phyinvtkn_comments);
                //UpdatedAt
                System.Data.IDataParameter p_filter_phyinvtkn_updated_at = dataProvider.GetParameter("@filter_phyinvtkn_updated_at",
                    f.UpdatedAt == default(System.DateTime) ?
                        (object)System.DBNull.Value :
                        (object)f.UpdatedAt);
                p_filter_phyinvtkn_updated_at.Direction = System.Data.ParameterDirection.Input;
                p_filter_phyinvtkn_updated_at.DbType = DbType.DateTime;
                parameters.Add(p_filter_phyinvtkn_updated_at);
                //RegisteredAt
                System.Data.IDataParameter p_filter_phyinvtkn_registered_at = dataProvider.GetParameter("@filter_phyinvtkn_registered_at",
                    f.RegisteredAt == default(System.DateTime) ?
                        (object)System.DBNull.Value :
                        (object)f.RegisteredAt);
                p_filter_phyinvtkn_registered_at.Direction = System.Data.ParameterDirection.Input;
                p_filter_phyinvtkn_registered_at.DbType = DbType.DateTime;
                parameters.Add(p_filter_phyinvtkn_registered_at);
                // Update
                //Accountable
                System.Data.IDataParameter p_new_phyinvtkn_accountable_Fk = dataProvider.GetParameter("@new_phyinvtkn_accountable_Fk",
                    instance.Accountable == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)instance.Accountable.Identifier);
                p_new_phyinvtkn_accountable_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_phyinvtkn_accountable_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_phyinvtkn_accountable_Fk);
                //Responsible
                System.Data.IDataParameter p_new_phyinvtkn_responsible_Fk = dataProvider.GetParameter("@new_phyinvtkn_responsible_Fk",
                    instance.Responsible == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)instance.Responsible.Identifier);
                p_new_phyinvtkn_responsible_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_phyinvtkn_responsible_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_phyinvtkn_responsible_Fk);
                //Informed
                System.Data.IDataParameter p_new_phyinvtkn_informed_Fk = dataProvider.GetParameter("@new_phyinvtkn_informed_Fk",
                    instance.Informed == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)instance.Informed.Identifier);
                p_new_phyinvtkn_informed_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_phyinvtkn_informed_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_phyinvtkn_informed_Fk);
                //WorkOrder
                System.Data.IDataParameter p_new_phyinvtkn_work_order_Fk = dataProvider.GetParameter("@new_phyinvtkn_work_order_Fk",
                    instance.WorkOrder == default(SOFTTEK.SCMS.Entity.FA.WorkOrder) ?
                        (object)System.DBNull.Value :
                        (object)instance.WorkOrder.Identifier);
                p_new_phyinvtkn_work_order_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_phyinvtkn_work_order_Fk.DbType = DbType.Int64;
                parameters.Add(p_new_phyinvtkn_work_order_Fk);
                //Location
                System.Data.IDataParameter p_new_phyinvtkn_location = dataProvider.GetParameter("@new_phyinvtkn_location",
                    f.Location == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Location);
                p_new_phyinvtkn_location.Direction = System.Data.ParameterDirection.Input;
                p_new_phyinvtkn_location.DbType = DbType.String;
                parameters.Add(p_new_phyinvtkn_location);
                //Status
                System.Data.IDataParameter p_new_phyinvtkn_status = dataProvider.GetParameter("@new_phyinvtkn_status",
                    f.Status == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Status);
                p_new_phyinvtkn_status.Direction = System.Data.ParameterDirection.Input;
                p_new_phyinvtkn_status.DbType = DbType.String;
                parameters.Add(p_new_phyinvtkn_status);
                //Comments
                System.Data.IDataParameter p_new_phyinvtkn_comments = dataProvider.GetParameter("@new_phyinvtkn_comments",
                    f.Comments == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Comments);
                p_new_phyinvtkn_comments.Direction = System.Data.ParameterDirection.Input;
                p_new_phyinvtkn_comments.DbType = DbType.String;
                parameters.Add(p_new_phyinvtkn_comments);

                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking> mapper = (reader) =>
            {
                long longHelper = default(long);
                int intHelper = default(int);
                DateTime dateHelper = default(DateTime);

                SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking value = new SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking
                {
                    Identifier = reader["v_phyinvtkn_id_Pk"] != null ?
                        (long.TryParse(reader["v_phyinvtkn_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    Accountable = reader["v_phyinvtkn_accountable_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_phyinvtkn_accountable_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Responsible = reader["v_phyinvtkn_responsible_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_phyinvtkn_responsible_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Informed = reader["v_phyinvtkn_informed_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_phyinvtkn_informed_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    WorkOrder = reader["v_phyinvtkn_work_order_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.FA.WorkOrder { Identifier = long.TryParse(reader["v_phyinvtkn_work_order_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null,
                    Location = reader["v_phyinvtkn_location"] != null ?
                        reader["v_phyinvtkn_location"].ToString() : string.Empty,
                    Status = reader["v_phyinvtkn_status"] != null ?
                        reader["v_phyinvtkn_status"].ToString() : string.Empty,
                    Comments = reader["v_phyinvtkn_comments"] != null ?
                        reader["v_phyinvtkn_comments"].ToString() : string.Empty,
                    UpdatedAt = reader["v_phyinvtkn_updated_at"] != null ?
                        (DateTime.TryParse(reader["v_phyinvtkn_updated_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                    RegisteredAt = reader["v_phyinvtkn_registered_at"] != null ?
                        (DateTime.TryParse(reader["v_phyinvtkn_registered_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                };
                return value;
            };


            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking result = dataProvider.ExecuteForEntityWithFilter(SCMS.FA_SP_Update_PhysicalInventoryTaking, filterDelegate, filter, mapper);
            return result;
        }
        #endregion
        #region Provider

        /// <summary>
        /// Retrieves a list of Provider instances that matches a given filter criteria by the Provider object instance 
        /// </summary>
        /// <param name="filter">Object with the information required to filter the set of results</param>
        /// <returns>List of Provider instances that matches the filter.</returns>
        public List<SOFTTEK.SCMS.Entity.FA.Provider> GetProviders(SOFTTEK.SCMS.Entity.FA.Provider filter)
        {
            Func<SOFTTEK.SCMS.Entity.FA.Provider, List<System.Data.IDataParameter>> filterDelegate = (f) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
                //Token
                System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p_tok.Direction = System.Data.ParameterDirection.Input;
                p_tok.DbType = System.Data.DbType.String;
                parameters.Add(p_tok);
                //Identifier
                System.Data.IDataParameter p_filter_prv_id_Pk = dataProvider.GetParameter("@filter_prv_id_Pk",
                    f.Identifier == default(System.Int64) ?
                        (object)System.DBNull.Value :
                        (object)f.Identifier);
                p_filter_prv_id_Pk.Direction = System.Data.ParameterDirection.Input;
                p_filter_prv_id_Pk.DbType = DbType.Int64;
                parameters.Add(p_filter_prv_id_Pk);
                //ExternalIdentifier
                System.Data.IDataParameter p_filter_prv_external_identifier = dataProvider.GetParameter("@filter_prv_external_identifier",
                    f.ExternalIdentifier == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.ExternalIdentifier);
                p_filter_prv_external_identifier.Direction = System.Data.ParameterDirection.Input;
                p_filter_prv_external_identifier.DbType = DbType.String;
                parameters.Add(p_filter_prv_external_identifier);
                //Name
                System.Data.IDataParameter p_filter_prv_name = dataProvider.GetParameter("@filter_prv_name",
                    f.Name == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Name);
                p_filter_prv_name.Direction = System.Data.ParameterDirection.Input;
                p_filter_prv_name.DbType = DbType.String;
                parameters.Add(p_filter_prv_name);
                //Contract
                System.Data.IDataParameter p_filter_prv_contract = dataProvider.GetParameter("@filter_prv_contract",
                    f.Contract == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Contract);
                p_filter_prv_contract.Direction = System.Data.ParameterDirection.Input;
                p_filter_prv_contract.DbType = DbType.String;
                parameters.Add(p_filter_prv_contract);
                //Document
                System.Data.IDataParameter p_filter_prv_document = dataProvider.GetParameter("@filter_prv_document",
                    f.Document == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Document);
                p_filter_prv_document.Direction = System.Data.ParameterDirection.Input;
                p_filter_prv_document.DbType = DbType.String;
                parameters.Add(p_filter_prv_document);
                //State
                System.Data.IDataParameter p_filter_prv_state = dataProvider.GetParameter("@filter_prv_state",
                    f.State == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.State);
                p_filter_prv_state.Direction = System.Data.ParameterDirection.Input;
                p_filter_prv_state.DbType = DbType.String;
                parameters.Add(p_filter_prv_state);
                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.FA.Provider> mapper = (reader) =>
            {
                long longHelper = default(long);

                SOFTTEK.SCMS.Entity.FA.Provider value = new SOFTTEK.SCMS.Entity.FA.Provider
                {
                    Identifier = reader["prv_id_Pk"] != null ?
                        (long.TryParse(reader["prv_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    ExternalIdentifier = reader["prv_external_identifier"] != null ?
                        reader["prv_external_identifier"].ToString() : string.Empty,
                    Name = reader["prv_name"] != null ?
                        reader["prv_name"].ToString() : string.Empty,
                    Contract = reader["prv_contract"] != null ?
                        reader["prv_contract"].ToString() : string.Empty,
                    Document = reader["prv_document"] != null ?
                        reader["prv_document"].ToString() : string.Empty,
                    State = reader["prv_state"] != null ?
                        reader["prv_state"].ToString() : string.Empty,
                };
                return value;
            };

            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            List<SOFTTEK.SCMS.Entity.FA.Provider> result = dataProvider.ExecuteReaderWithFilter(SCMS.FA_SP_Select_Provider, filterDelegate, filter, mapper);
            return result;
        }

        /// <summary>
        /// Inserts the instance of the provided Provider at the Data Context datasource  
        /// </summary>
        /// <param name="instance">Instance of Provider with the entity´s information to store</param>
        /// <returns>Instance of Provider with the aditional information provided by the Data Context datasource</returns>
        public SOFTTEK.SCMS.Entity.FA.Provider InsertProvider(SOFTTEK.SCMS.Entity.FA.Provider instance)
        {
            Func<SOFTTEK.SCMS.Entity.FA.Provider, List<System.Data.IDataParameter>> input = (i) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();

                //Token
                System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p_tok.Direction = System.Data.ParameterDirection.Input;
                p_tok.DbType = System.Data.DbType.String;
                parameters.Add(p_tok);

                // Insert
                //ExternalIdentifier
                System.Data.IDataParameter p_new_prv_external_identifier = dataProvider.GetParameter("@new_prv_external_identifier",
                    i.ExternalIdentifier == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.ExternalIdentifier);
                p_new_prv_external_identifier.Direction = System.Data.ParameterDirection.Input;
                p_new_prv_external_identifier.DbType = DbType.String;
                parameters.Add(p_new_prv_external_identifier);
                //Name
                System.Data.IDataParameter p_new_prv_name = dataProvider.GetParameter("@new_prv_name",
                    i.Name == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Name);
                p_new_prv_name.Direction = System.Data.ParameterDirection.Input;
                p_new_prv_name.DbType = DbType.String;
                parameters.Add(p_new_prv_name);
                //Contract
                System.Data.IDataParameter p_new_prv_contract = dataProvider.GetParameter("@new_prv_contract",
                    i.Contract == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Contract);
                p_new_prv_contract.Direction = System.Data.ParameterDirection.Input;
                p_new_prv_contract.DbType = DbType.String;
                parameters.Add(p_new_prv_contract);
                //Document
                System.Data.IDataParameter p_new_prv_document = dataProvider.GetParameter("@new_prv_document",
                    i.Document == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Document);
                p_new_prv_document.Direction = System.Data.ParameterDirection.Input;
                p_new_prv_document.DbType = DbType.String;
                parameters.Add(p_new_prv_document);
                //State
                System.Data.IDataParameter p_new_prv_state = dataProvider.GetParameter("@new_prv_state",
                    i.State == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.State);
                p_new_prv_state.Direction = System.Data.ParameterDirection.Input;
                p_new_prv_state.DbType = DbType.String;
                parameters.Add(p_new_prv_state);

                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.FA.Provider> mapper = (reader) =>
            {
                long longHelper = default(long);

                SOFTTEK.SCMS.Entity.FA.Provider value = new SOFTTEK.SCMS.Entity.FA.Provider
                {
                    Identifier = reader["v_prv_id_Pk"] != null ?
                        (long.TryParse(reader["v_prv_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    ExternalIdentifier = reader["v_prv_external_identifier"] != null ?
                        reader["v_prv_external_identifier"].ToString() : string.Empty,
                    Name = reader["v_prv_name"] != null ?
                        reader["v_prv_name"].ToString() : string.Empty,
                    Contract = reader["v_prv_contract"] != null ?
                        reader["v_prv_contract"].ToString() : string.Empty,
                    Document = reader["v_prv_document"] != null ?
                        reader["v_prv_document"].ToString() : string.Empty,
                    State = reader["v_prv_state"] != null ?
                        reader["v_prv_state"].ToString() : string.Empty,
                };
                return value;
            };


            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            SOFTTEK.SCMS.Entity.FA.Provider result = dataProvider.ExecuteForEntityWithFilter(SCMS.FA_SP_Insert_Provider, input, instance, mapper);
            return result;
        }

        /// <summary>
        /// Update the entity's stored information with that provided by the instance of Provider at the Data Context datasource  
        /// </summary>
        /// <param name="instance">Instance of Provider with the entity´s information to update</param>
        /// <param name="filter">Object with the information required to filter the target stored entity´s information</param>
        /// <returns>Instance of Provider with the updated information provided by the Data Context datasource</returns>
        public SOFTTEK.SCMS.Entity.FA.Provider UpdateProvider(SOFTTEK.SCMS.Entity.FA.Provider instance, SOFTTEK.SCMS.Entity.FA.Provider filter)
        {
            Func<SOFTTEK.SCMS.Entity.FA.Provider, List<System.Data.IDataParameter>> filterDelegate = (f) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();

                //Token
                System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p_tok.Direction = System.Data.ParameterDirection.Input;
                p_tok.DbType = System.Data.DbType.String;
                parameters.Add(p_tok);

                // Filter
                //Identifier
                System.Data.IDataParameter p_filter_prv_id_Pk = dataProvider.GetParameter("@filter_prv_id_Pk",
                    f.Identifier == default(System.Int64) ?
                        (object)System.DBNull.Value :
                        (object)f.Identifier);
                p_filter_prv_id_Pk.Direction = System.Data.ParameterDirection.Input;
                p_filter_prv_id_Pk.DbType = DbType.Int64;
                parameters.Add(p_filter_prv_id_Pk);
                //ExternalIdentifier
                System.Data.IDataParameter p_filter_prv_external_identifier = dataProvider.GetParameter("@filter_prv_external_identifier",
                    f.ExternalIdentifier == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.ExternalIdentifier);
                p_filter_prv_external_identifier.Direction = System.Data.ParameterDirection.Input;
                p_filter_prv_external_identifier.DbType = DbType.String;
                parameters.Add(p_filter_prv_external_identifier);
                //Name
                System.Data.IDataParameter p_filter_prv_name = dataProvider.GetParameter("@filter_prv_name",
                    f.Name == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Name);
                p_filter_prv_name.Direction = System.Data.ParameterDirection.Input;
                p_filter_prv_name.DbType = DbType.String;
                parameters.Add(p_filter_prv_name);
                //Contract
                System.Data.IDataParameter p_filter_prv_contract = dataProvider.GetParameter("@filter_prv_contract",
                    f.Contract == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Contract);
                p_filter_prv_contract.Direction = System.Data.ParameterDirection.Input;
                p_filter_prv_contract.DbType = DbType.String;
                parameters.Add(p_filter_prv_contract);
                //Document
                System.Data.IDataParameter p_filter_prv_document = dataProvider.GetParameter("@filter_prv_document",
                    f.Document == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Document);
                p_filter_prv_document.Direction = System.Data.ParameterDirection.Input;
                p_filter_prv_document.DbType = DbType.String;
                parameters.Add(p_filter_prv_document);
                //State
                System.Data.IDataParameter p_filter_prv_state = dataProvider.GetParameter("@filter_prv_state",
                    f.State == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.State);
                p_filter_prv_state.Direction = System.Data.ParameterDirection.Input;
                p_filter_prv_state.DbType = DbType.String;
                parameters.Add(p_filter_prv_state);
                // Update
                //ExternalIdentifier
                System.Data.IDataParameter p_new_prv_external_identifier = dataProvider.GetParameter("@new_prv_external_identifier",
                    f.ExternalIdentifier == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.ExternalIdentifier);
                p_new_prv_external_identifier.Direction = System.Data.ParameterDirection.Input;
                p_new_prv_external_identifier.DbType = DbType.String;
                parameters.Add(p_new_prv_external_identifier);
                //Name
                System.Data.IDataParameter p_new_prv_name = dataProvider.GetParameter("@new_prv_name",
                    f.Name == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Name);
                p_new_prv_name.Direction = System.Data.ParameterDirection.Input;
                p_new_prv_name.DbType = DbType.String;
                parameters.Add(p_new_prv_name);
                //Contract
                System.Data.IDataParameter p_new_prv_contract = dataProvider.GetParameter("@new_prv_contract",
                    f.Contract == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Contract);
                p_new_prv_contract.Direction = System.Data.ParameterDirection.Input;
                p_new_prv_contract.DbType = DbType.String;
                parameters.Add(p_new_prv_contract);
                //Document
                System.Data.IDataParameter p_new_prv_document = dataProvider.GetParameter("@new_prv_document",
                    f.Document == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Document);
                p_new_prv_document.Direction = System.Data.ParameterDirection.Input;
                p_new_prv_document.DbType = DbType.String;
                parameters.Add(p_new_prv_document);
                //State
                System.Data.IDataParameter p_new_prv_state = dataProvider.GetParameter("@new_prv_state",
                    f.State == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.State);
                p_new_prv_state.Direction = System.Data.ParameterDirection.Input;
                p_new_prv_state.DbType = DbType.String;
                parameters.Add(p_new_prv_state);

                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.FA.Provider> mapper = (reader) =>
            {
                long longHelper = default(long);

                SOFTTEK.SCMS.Entity.FA.Provider value = new SOFTTEK.SCMS.Entity.FA.Provider
                {
                    Identifier = reader["v_prv_id_Pk"] != null ?
                        (long.TryParse(reader["v_prv_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    ExternalIdentifier = reader["v_prv_external_identifier"] != null ?
                        reader["v_prv_external_identifier"].ToString() : string.Empty,
                    Name = reader["v_prv_name"] != null ?
                        reader["v_prv_name"].ToString() : string.Empty,
                    Contract = reader["v_prv_contract"] != null ?
                        reader["v_prv_contract"].ToString() : string.Empty,
                    Document = reader["v_prv_document"] != null ?
                        reader["v_prv_document"].ToString() : string.Empty,
                    State = reader["v_prv_state"] != null ?
                        reader["v_prv_state"].ToString() : string.Empty,
                };
                return value;
            };


            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            SOFTTEK.SCMS.Entity.FA.Provider result = dataProvider.ExecuteForEntityWithFilter(SCMS.FA_SP_Update_Provider, filterDelegate, filter, mapper);
            return result;
        }
        #endregion
        #region RetirementRequest

        /// <summary>
        /// Retrieves a list of RetirementRequest instances that matches a given filter criteria by the RetirementRequest object instance 
        /// </summary>
        /// <param name="filter">Object with the information required to filter the set of results</param>
        /// <returns>List of RetirementRequest instances that matches the filter.</returns>
        public List<SOFTTEK.SCMS.Entity.FA.RetirementRequest> GetRetirementRequests(SOFTTEK.SCMS.Entity.FA.RetirementRequest filter)
        {
            Func<SOFTTEK.SCMS.Entity.FA.RetirementRequest, List<System.Data.IDataParameter>> filterDelegate = (f) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
                //Token
                System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p_tok.Direction = System.Data.ParameterDirection.Input;
                p_tok.DbType = System.Data.DbType.String;
                parameters.Add(p_tok);
                //ExternalIdentifier
                System.Data.IDataParameter p_filter_rtr_external_identifier = dataProvider.GetParameter("@filter_rtr_external_identifier",
                    f.ExternalIdentifier == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.ExternalIdentifier);
                p_filter_rtr_external_identifier.Direction = System.Data.ParameterDirection.Input;
                p_filter_rtr_external_identifier.DbType = DbType.String;
                parameters.Add(p_filter_rtr_external_identifier);
                //Novelty
                System.Data.IDataParameter p_filter_rtr_novelty_Fk = dataProvider.GetParameter("@filter_rtr_novelty_Fk",
                    f.Novelty == default(SOFTTEK.SCMS.Entity.FA.NoveltyRequest) ?
                        (object)System.DBNull.Value :
                        (object)f.Novelty.Identifier);
                p_filter_rtr_novelty_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_rtr_novelty_Fk.DbType = DbType.Int64;
                parameters.Add(p_filter_rtr_novelty_Fk);
                //Reason
                System.Data.IDataParameter p_filter_rtr_reason = dataProvider.GetParameter("@filter_rtr_reason",
                    f.Reason == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Reason);
                p_filter_rtr_reason.Direction = System.Data.ParameterDirection.Input;
                p_filter_rtr_reason.DbType = DbType.String;
                parameters.Add(p_filter_rtr_reason);
                //Identifier
                System.Data.IDataParameter p_filter_rtr_id_Pk = dataProvider.GetParameter("@filter_rtr_id_Pk",
                    f.Identifier == default(System.Int64) ?
                        (object)System.DBNull.Value :
                        (object)f.Identifier);
                p_filter_rtr_id_Pk.Direction = System.Data.ParameterDirection.Input;
                p_filter_rtr_id_Pk.DbType = DbType.Int64;
                parameters.Add(p_filter_rtr_id_Pk);
                //Number
                System.Data.IDataParameter p_filter_rtr_number = dataProvider.GetParameter("@filter_rtr_number",
                    f.Number == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Number);
                p_filter_rtr_number.Direction = System.Data.ParameterDirection.Input;
                p_filter_rtr_number.DbType = DbType.String;
                parameters.Add(p_filter_rtr_number);
                //Type
                System.Data.IDataParameter p_filter_rtr_type_Fk = dataProvider.GetParameter("@filter_rtr_type_Fk",
                    f.Type == default(SOFTTEK.SCMS.Entity.Shared.Parameter<System.String>) ?
                        (object)System.DBNull.Value :
                        (object)f.Type.Identifier);
                p_filter_rtr_type_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_rtr_type_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_rtr_type_Fk);
                //Informed
                System.Data.IDataParameter p_filter_rtr_informed_Fk = dataProvider.GetParameter("@filter_rtr_informed_Fk",
                    f.Informed == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Informed.Identifier);
                p_filter_rtr_informed_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_rtr_informed_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_rtr_informed_Fk);
                //Responsible
                System.Data.IDataParameter p_filter_rtr_responsible_Fk = dataProvider.GetParameter("@filter_rtr_responsible_Fk",
                    f.Responsible == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Responsible.Identifier);
                p_filter_rtr_responsible_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_rtr_responsible_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_rtr_responsible_Fk);
                //Accountable
                System.Data.IDataParameter p_filter_rtr_accountable_Fk = dataProvider.GetParameter("@filter_rtr_accountable_Fk",
                    f.Accountable == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Accountable.Identifier);
                p_filter_rtr_accountable_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_rtr_accountable_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_rtr_accountable_Fk);
                //Details
                System.Data.IDataParameter p_filter_rtr_details = dataProvider.GetParameter("@filter_rtr_details",
                    f.Details == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Details);
                p_filter_rtr_details.Direction = System.Data.ParameterDirection.Input;
                p_filter_rtr_details.DbType = DbType.String;
                parameters.Add(p_filter_rtr_details);
                //Comments
                System.Data.IDataParameter p_filter_rtr_comments = dataProvider.GetParameter("@filter_rtr_comments",
                    f.Comments == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Comments);
                p_filter_rtr_comments.Direction = System.Data.ParameterDirection.Input;
                p_filter_rtr_comments.DbType = DbType.String;
                parameters.Add(p_filter_rtr_comments);
                //Topic
                System.Data.IDataParameter p_filter_rtr_topic = dataProvider.GetParameter("@filter_rtr_topic",
                    f.Topic == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Topic);
                p_filter_rtr_topic.Direction = System.Data.ParameterDirection.Input;
                p_filter_rtr_topic.DbType = DbType.String;
                parameters.Add(p_filter_rtr_topic);
                //Status
                System.Data.IDataParameter p_filter_rtr_status = dataProvider.GetParameter("@filter_rtr_status",
                    f.Status == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Status);
                p_filter_rtr_status.Direction = System.Data.ParameterDirection.Input;
                p_filter_rtr_status.DbType = DbType.String;
                parameters.Add(p_filter_rtr_status);
                //UpdatedAt
                System.Data.IDataParameter p_filter_rtr_updated_at = dataProvider.GetParameter("@filter_rtr_updated_at",
                    f.UpdatedAt == default(System.DateTime) ?
                        (object)System.DBNull.Value :
                        (object)f.UpdatedAt);
                p_filter_rtr_updated_at.Direction = System.Data.ParameterDirection.Input;
                p_filter_rtr_updated_at.DbType = DbType.DateTime;
                parameters.Add(p_filter_rtr_updated_at);
                //RegisteredAt
                System.Data.IDataParameter p_filter_rtr_registered_at = dataProvider.GetParameter("@filter_rtr_registered_at",
                    f.RegisteredAt == default(System.DateTime) ?
                        (object)System.DBNull.Value :
                        (object)f.RegisteredAt);
                p_filter_rtr_registered_at.Direction = System.Data.ParameterDirection.Input;
                p_filter_rtr_registered_at.DbType = DbType.DateTime;
                parameters.Add(p_filter_rtr_registered_at);
                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.FA.RetirementRequest> mapper = (reader) =>
            {
                long longHelper = default(long);
                int intHelper = default(int);
                DateTime dateHelper = default(DateTime);

                SOFTTEK.SCMS.Entity.FA.RetirementRequest value = new SOFTTEK.SCMS.Entity.FA.RetirementRequest
                {
                    ExternalIdentifier = reader["rtr_external_identifier"] != null ?
                        reader["rtr_external_identifier"].ToString() : string.Empty,
                    Novelty = reader["rtr_novelty_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.FA.NoveltyRequest { Identifier = long.TryParse(reader["rtr_novelty_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null,
                    Reason = reader["rtr_reason"] != null ?
                        reader["rtr_reason"].ToString() : string.Empty,
                    Identifier = reader["rtr_id_Pk"] != null ?
                        (long.TryParse(reader["rtr_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    Number = reader["rtr_number"] != null ?
                        reader["rtr_number"].ToString() : string.Empty,
                    Type = reader["rtr_type_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Parameter<System.String> { Identifier = int.TryParse(reader["rtr_type_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Informed = reader["rtr_informed_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["rtr_informed_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Responsible = reader["rtr_responsible_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["rtr_responsible_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Accountable = reader["rtr_accountable_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["rtr_accountable_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Details = reader["rtr_details"] != null ?
                        reader["rtr_details"].ToString() : string.Empty,
                    Comments = reader["rtr_comments"] != null ?
                        reader["rtr_comments"].ToString() : string.Empty,
                    Topic = reader["rtr_topic"] != null ?
                        reader["rtr_topic"].ToString() : string.Empty,
                    Status = reader["rtr_status"] != null ?
                        reader["rtr_status"].ToString() : string.Empty,
                    UpdatedAt = reader["rtr_updated_at"] != null ?
                        (DateTime.TryParse(reader["rtr_updated_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                    RegisteredAt = reader["rtr_registered_at"] != null ?
                        (DateTime.TryParse(reader["rtr_registered_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                };
                return value;
            };

            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            List<SOFTTEK.SCMS.Entity.FA.RetirementRequest> result = dataProvider.ExecuteReaderWithFilter(SCMS.FA_SP_Select_RetirementRequest, filterDelegate, filter, mapper);
            return result;
        }

        /// <summary>
        /// Inserts the instance of the provided RetirementRequest at the Data Context datasource  
        /// </summary>
        /// <param name="instance">Instance of RetirementRequest with the entity´s information to store</param>
        /// <returns>Instance of RetirementRequest with the aditional information provided by the Data Context datasource</returns>
        public SOFTTEK.SCMS.Entity.FA.RetirementRequest InsertRetirementRequest(SOFTTEK.SCMS.Entity.FA.RetirementRequest instance)
        {
            Func<SOFTTEK.SCMS.Entity.FA.RetirementRequest, List<System.Data.IDataParameter>> input = (i) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();

                //Token
                System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p_tok.Direction = System.Data.ParameterDirection.Input;
                p_tok.DbType = System.Data.DbType.String;
                parameters.Add(p_tok);

                // Insert
                //ExternalIdentifier
                System.Data.IDataParameter p_new_rtr_external_identifier = dataProvider.GetParameter("@new_rtr_external_identifier",
                    i.ExternalIdentifier == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.ExternalIdentifier);
                p_new_rtr_external_identifier.Direction = System.Data.ParameterDirection.Input;
                p_new_rtr_external_identifier.DbType = DbType.String;
                parameters.Add(p_new_rtr_external_identifier);
                //Novelty
                System.Data.IDataParameter p_new_rtr_novelty_Fk = dataProvider.GetParameter("@new_rtr_novelty_Fk",
                    i.Novelty == default(SOFTTEK.SCMS.Entity.FA.NoveltyRequest) ?
                        (object)System.DBNull.Value :
                        (object)i.Novelty.Identifier);
                p_new_rtr_novelty_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_rtr_novelty_Fk.DbType = DbType.Int64;
                parameters.Add(p_new_rtr_novelty_Fk);
                //Reason
                System.Data.IDataParameter p_new_rtr_reason = dataProvider.GetParameter("@new_rtr_reason",
                    i.Reason == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Reason);
                p_new_rtr_reason.Direction = System.Data.ParameterDirection.Input;
                p_new_rtr_reason.DbType = DbType.String;
                parameters.Add(p_new_rtr_reason);
                //Number
                System.Data.IDataParameter p_new_rtr_number = dataProvider.GetParameter("@new_rtr_number",
                    i.Number == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Number);
                p_new_rtr_number.Direction = System.Data.ParameterDirection.Input;
                p_new_rtr_number.DbType = DbType.String;
                parameters.Add(p_new_rtr_number);
                //Type
                System.Data.IDataParameter p_new_rtr_type_Fk = dataProvider.GetParameter("@new_rtr_type_Fk",
                    i.Type == default(SOFTTEK.SCMS.Entity.Shared.Parameter<System.String>) ?
                        (object)System.DBNull.Value :
                        (object)i.Type.Identifier);
                p_new_rtr_type_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_rtr_type_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_rtr_type_Fk);
                //Informed
                System.Data.IDataParameter p_new_rtr_informed_Fk = dataProvider.GetParameter("@new_rtr_informed_Fk",
                    i.Informed == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)i.Informed.Identifier);
                p_new_rtr_informed_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_rtr_informed_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_rtr_informed_Fk);
                //Responsible
                System.Data.IDataParameter p_new_rtr_responsible_Fk = dataProvider.GetParameter("@new_rtr_responsible_Fk",
                    i.Responsible == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)i.Responsible.Identifier);
                p_new_rtr_responsible_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_rtr_responsible_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_rtr_responsible_Fk);
                //Accountable
                System.Data.IDataParameter p_new_rtr_accountable_Fk = dataProvider.GetParameter("@new_rtr_accountable_Fk",
                    i.Accountable == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)i.Accountable.Identifier);
                p_new_rtr_accountable_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_rtr_accountable_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_rtr_accountable_Fk);
                //Details
                System.Data.IDataParameter p_new_rtr_details = dataProvider.GetParameter("@new_rtr_details",
                    i.Details == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Details);
                p_new_rtr_details.Direction = System.Data.ParameterDirection.Input;
                p_new_rtr_details.DbType = DbType.String;
                parameters.Add(p_new_rtr_details);
                //Comments
                System.Data.IDataParameter p_new_rtr_comments = dataProvider.GetParameter("@new_rtr_comments",
                    i.Comments == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Comments);
                p_new_rtr_comments.Direction = System.Data.ParameterDirection.Input;
                p_new_rtr_comments.DbType = DbType.String;
                parameters.Add(p_new_rtr_comments);
                //Topic
                System.Data.IDataParameter p_new_rtr_topic = dataProvider.GetParameter("@new_rtr_topic",
                    i.Topic == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Topic);
                p_new_rtr_topic.Direction = System.Data.ParameterDirection.Input;
                p_new_rtr_topic.DbType = DbType.String;
                parameters.Add(p_new_rtr_topic);
                //Status
                System.Data.IDataParameter p_new_rtr_status = dataProvider.GetParameter("@new_rtr_status",
                    i.Status == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Status);
                p_new_rtr_status.Direction = System.Data.ParameterDirection.Input;
                p_new_rtr_status.DbType = DbType.String;
                parameters.Add(p_new_rtr_status);

                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.FA.RetirementRequest> mapper = (reader) =>
            {
                long longHelper = default(long);
                int intHelper = default(int);
                DateTime dateHelper = default(DateTime);

                SOFTTEK.SCMS.Entity.FA.RetirementRequest value = new SOFTTEK.SCMS.Entity.FA.RetirementRequest
                {
                    ExternalIdentifier = reader["v_rtr_external_identifier"] != null ?
                        reader["v_rtr_external_identifier"].ToString() : string.Empty,
                    Novelty = reader["v_rtr_novelty_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.FA.NoveltyRequest { Identifier = long.TryParse(reader["v_rtr_novelty_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null,
                    Reason = reader["v_rtr_reason"] != null ?
                        reader["v_rtr_reason"].ToString() : string.Empty,
                    Identifier = reader["v_rtr_id_Pk"] != null ?
                        (long.TryParse(reader["v_rtr_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    Number = reader["v_rtr_number"] != null ?
                        reader["v_rtr_number"].ToString() : string.Empty,
                    Type = reader["v_rtr_type_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Parameter<System.String> { Identifier = int.TryParse(reader["v_rtr_type_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Informed = reader["v_rtr_informed_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_rtr_informed_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Responsible = reader["v_rtr_responsible_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_rtr_responsible_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Accountable = reader["v_rtr_accountable_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_rtr_accountable_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Details = reader["v_rtr_details"] != null ?
                        reader["v_rtr_details"].ToString() : string.Empty,
                    Comments = reader["v_rtr_comments"] != null ?
                        reader["v_rtr_comments"].ToString() : string.Empty,
                    Topic = reader["v_rtr_topic"] != null ?
                        reader["v_rtr_topic"].ToString() : string.Empty,
                    Status = reader["v_rtr_status"] != null ?
                        reader["v_rtr_status"].ToString() : string.Empty,
                    UpdatedAt = reader["v_rtr_updated_at"] != null ?
                        (DateTime.TryParse(reader["v_rtr_updated_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                    RegisteredAt = reader["v_rtr_registered_at"] != null ?
                        (DateTime.TryParse(reader["v_rtr_registered_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                };
                return value;
            };


            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            SOFTTEK.SCMS.Entity.FA.RetirementRequest result = dataProvider.ExecuteForEntityWithFilter(SCMS.FA_SP_Insert_RetirementRequest, input, instance, mapper);
            return result;
        }

        /// <summary>
        /// Update the entity's stored information with that provided by the instance of RetirementRequest at the Data Context datasource  
        /// </summary>
        /// <param name="instance">Instance of RetirementRequest with the entity´s information to update</param>
        /// <param name="filter">Object with the information required to filter the target stored entity´s information</param>
        /// <returns>Instance of RetirementRequest with the updated information provided by the Data Context datasource</returns>
        public SOFTTEK.SCMS.Entity.FA.RetirementRequest UpdateRetirementRequest(SOFTTEK.SCMS.Entity.FA.RetirementRequest instance, SOFTTEK.SCMS.Entity.FA.RetirementRequest filter)
        {
            Func<SOFTTEK.SCMS.Entity.FA.RetirementRequest, List<System.Data.IDataParameter>> filterDelegate = (f) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();

                //Token
                System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p_tok.Direction = System.Data.ParameterDirection.Input;
                p_tok.DbType = System.Data.DbType.String;
                parameters.Add(p_tok);

                // Filter
                //ExternalIdentifier
                System.Data.IDataParameter p_filter_rtr_external_identifier = dataProvider.GetParameter("@filter_rtr_external_identifier",
                    f.ExternalIdentifier == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.ExternalIdentifier);
                p_filter_rtr_external_identifier.Direction = System.Data.ParameterDirection.Input;
                p_filter_rtr_external_identifier.DbType = DbType.String;
                parameters.Add(p_filter_rtr_external_identifier);
                //Novelty
                System.Data.IDataParameter p_filter_rtr_novelty_Fk = dataProvider.GetParameter("@filter_rtr_novelty_Fk",
                    f.Novelty == default(SOFTTEK.SCMS.Entity.FA.NoveltyRequest) ?
                        (object)System.DBNull.Value :
                        (object)f.Novelty.Identifier);
                p_filter_rtr_novelty_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_rtr_novelty_Fk.DbType = DbType.Int64;
                parameters.Add(p_filter_rtr_novelty_Fk);
                //Reason
                System.Data.IDataParameter p_filter_rtr_reason = dataProvider.GetParameter("@filter_rtr_reason",
                    f.Reason == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Reason);
                p_filter_rtr_reason.Direction = System.Data.ParameterDirection.Input;
                p_filter_rtr_reason.DbType = DbType.String;
                parameters.Add(p_filter_rtr_reason);
                //Identifier
                System.Data.IDataParameter p_filter_rtr_id_Pk = dataProvider.GetParameter("@filter_rtr_id_Pk",
                    f.Identifier == default(System.Int64) ?
                        (object)System.DBNull.Value :
                        (object)f.Identifier);
                p_filter_rtr_id_Pk.Direction = System.Data.ParameterDirection.Input;
                p_filter_rtr_id_Pk.DbType = DbType.Int64;
                parameters.Add(p_filter_rtr_id_Pk);
                //Number
                System.Data.IDataParameter p_filter_rtr_number = dataProvider.GetParameter("@filter_rtr_number",
                    f.Number == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Number);
                p_filter_rtr_number.Direction = System.Data.ParameterDirection.Input;
                p_filter_rtr_number.DbType = DbType.String;
                parameters.Add(p_filter_rtr_number);
                //Type
                System.Data.IDataParameter p_filter_rtr_type_Fk = dataProvider.GetParameter("@filter_rtr_type_Fk",
                    f.Type == default(SOFTTEK.SCMS.Entity.Shared.Parameter<System.String>) ?
                        (object)System.DBNull.Value :
                        (object)f.Type.Identifier);
                p_filter_rtr_type_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_rtr_type_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_rtr_type_Fk);
                //Informed
                System.Data.IDataParameter p_filter_rtr_informed_Fk = dataProvider.GetParameter("@filter_rtr_informed_Fk",
                    f.Informed == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Informed.Identifier);
                p_filter_rtr_informed_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_rtr_informed_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_rtr_informed_Fk);
                //Responsible
                System.Data.IDataParameter p_filter_rtr_responsible_Fk = dataProvider.GetParameter("@filter_rtr_responsible_Fk",
                    f.Responsible == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Responsible.Identifier);
                p_filter_rtr_responsible_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_rtr_responsible_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_rtr_responsible_Fk);
                //Accountable
                System.Data.IDataParameter p_filter_rtr_accountable_Fk = dataProvider.GetParameter("@filter_rtr_accountable_Fk",
                    f.Accountable == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Accountable.Identifier);
                p_filter_rtr_accountable_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_rtr_accountable_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_rtr_accountable_Fk);
                //Details
                System.Data.IDataParameter p_filter_rtr_details = dataProvider.GetParameter("@filter_rtr_details",
                    f.Details == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Details);
                p_filter_rtr_details.Direction = System.Data.ParameterDirection.Input;
                p_filter_rtr_details.DbType = DbType.String;
                parameters.Add(p_filter_rtr_details);
                //Comments
                System.Data.IDataParameter p_filter_rtr_comments = dataProvider.GetParameter("@filter_rtr_comments",
                    f.Comments == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Comments);
                p_filter_rtr_comments.Direction = System.Data.ParameterDirection.Input;
                p_filter_rtr_comments.DbType = DbType.String;
                parameters.Add(p_filter_rtr_comments);
                //Topic
                System.Data.IDataParameter p_filter_rtr_topic = dataProvider.GetParameter("@filter_rtr_topic",
                    f.Topic == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Topic);
                p_filter_rtr_topic.Direction = System.Data.ParameterDirection.Input;
                p_filter_rtr_topic.DbType = DbType.String;
                parameters.Add(p_filter_rtr_topic);
                //Status
                System.Data.IDataParameter p_filter_rtr_status = dataProvider.GetParameter("@filter_rtr_status",
                    f.Status == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Status);
                p_filter_rtr_status.Direction = System.Data.ParameterDirection.Input;
                p_filter_rtr_status.DbType = DbType.String;
                parameters.Add(p_filter_rtr_status);
                //UpdatedAt
                System.Data.IDataParameter p_filter_rtr_updated_at = dataProvider.GetParameter("@filter_rtr_updated_at",
                    f.UpdatedAt == default(System.DateTime) ?
                        (object)System.DBNull.Value :
                        (object)f.UpdatedAt);
                p_filter_rtr_updated_at.Direction = System.Data.ParameterDirection.Input;
                p_filter_rtr_updated_at.DbType = DbType.DateTime;
                parameters.Add(p_filter_rtr_updated_at);
                //RegisteredAt
                System.Data.IDataParameter p_filter_rtr_registered_at = dataProvider.GetParameter("@filter_rtr_registered_at",
                    f.RegisteredAt == default(System.DateTime) ?
                        (object)System.DBNull.Value :
                        (object)f.RegisteredAt);
                p_filter_rtr_registered_at.Direction = System.Data.ParameterDirection.Input;
                p_filter_rtr_registered_at.DbType = DbType.DateTime;
                parameters.Add(p_filter_rtr_registered_at);
                // Update
                //ExternalIdentifier
                System.Data.IDataParameter p_new_rtr_external_identifier = dataProvider.GetParameter("@new_rtr_external_identifier",
                    f.ExternalIdentifier == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.ExternalIdentifier);
                p_new_rtr_external_identifier.Direction = System.Data.ParameterDirection.Input;
                p_new_rtr_external_identifier.DbType = DbType.String;
                parameters.Add(p_new_rtr_external_identifier);
                //Novelty
                System.Data.IDataParameter p_new_rtr_novelty_Fk = dataProvider.GetParameter("@new_rtr_novelty_Fk",
                    instance.Novelty == default(SOFTTEK.SCMS.Entity.FA.NoveltyRequest) ?
                        (object)System.DBNull.Value :
                        (object)instance.Novelty.Identifier);
                p_new_rtr_novelty_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_rtr_novelty_Fk.DbType = DbType.Int64;
                parameters.Add(p_new_rtr_novelty_Fk);
                //Reason
                System.Data.IDataParameter p_new_rtr_reason = dataProvider.GetParameter("@new_rtr_reason",
                    f.Reason == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Reason);
                p_new_rtr_reason.Direction = System.Data.ParameterDirection.Input;
                p_new_rtr_reason.DbType = DbType.String;
                parameters.Add(p_new_rtr_reason);
                //Number
                System.Data.IDataParameter p_new_rtr_number = dataProvider.GetParameter("@new_rtr_number",
                    f.Number == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Number);
                p_new_rtr_number.Direction = System.Data.ParameterDirection.Input;
                p_new_rtr_number.DbType = DbType.String;
                parameters.Add(p_new_rtr_number);
                //Type
                System.Data.IDataParameter p_new_rtr_type_Fk = dataProvider.GetParameter("@new_rtr_type_Fk",
                    instance.Type == default(SOFTTEK.SCMS.Entity.Shared.Parameter<System.String>) ?
                        (object)System.DBNull.Value :
                        (object)instance.Type.Identifier);
                p_new_rtr_type_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_rtr_type_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_rtr_type_Fk);
                //Informed
                System.Data.IDataParameter p_new_rtr_informed_Fk = dataProvider.GetParameter("@new_rtr_informed_Fk",
                    instance.Informed == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)instance.Informed.Identifier);
                p_new_rtr_informed_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_rtr_informed_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_rtr_informed_Fk);
                //Responsible
                System.Data.IDataParameter p_new_rtr_responsible_Fk = dataProvider.GetParameter("@new_rtr_responsible_Fk",
                    instance.Responsible == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)instance.Responsible.Identifier);
                p_new_rtr_responsible_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_rtr_responsible_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_rtr_responsible_Fk);
                //Accountable
                System.Data.IDataParameter p_new_rtr_accountable_Fk = dataProvider.GetParameter("@new_rtr_accountable_Fk",
                    instance.Accountable == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)instance.Accountable.Identifier);
                p_new_rtr_accountable_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_rtr_accountable_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_rtr_accountable_Fk);
                //Details
                System.Data.IDataParameter p_new_rtr_details = dataProvider.GetParameter("@new_rtr_details",
                    f.Details == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Details);
                p_new_rtr_details.Direction = System.Data.ParameterDirection.Input;
                p_new_rtr_details.DbType = DbType.String;
                parameters.Add(p_new_rtr_details);
                //Comments
                System.Data.IDataParameter p_new_rtr_comments = dataProvider.GetParameter("@new_rtr_comments",
                    f.Comments == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Comments);
                p_new_rtr_comments.Direction = System.Data.ParameterDirection.Input;
                p_new_rtr_comments.DbType = DbType.String;
                parameters.Add(p_new_rtr_comments);
                //Topic
                System.Data.IDataParameter p_new_rtr_topic = dataProvider.GetParameter("@new_rtr_topic",
                    f.Topic == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Topic);
                p_new_rtr_topic.Direction = System.Data.ParameterDirection.Input;
                p_new_rtr_topic.DbType = DbType.String;
                parameters.Add(p_new_rtr_topic);
                //Status
                System.Data.IDataParameter p_new_rtr_status = dataProvider.GetParameter("@new_rtr_status",
                    f.Status == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Status);
                p_new_rtr_status.Direction = System.Data.ParameterDirection.Input;
                p_new_rtr_status.DbType = DbType.String;
                parameters.Add(p_new_rtr_status);

                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.FA.RetirementRequest> mapper = (reader) =>
            {
                long longHelper = default(long);
                int intHelper = default(int);
                DateTime dateHelper = default(DateTime);

                SOFTTEK.SCMS.Entity.FA.RetirementRequest value = new SOFTTEK.SCMS.Entity.FA.RetirementRequest
                {
                    ExternalIdentifier = reader["v_rtr_external_identifier"] != null ?
                        reader["v_rtr_external_identifier"].ToString() : string.Empty,
                    Novelty = reader["v_rtr_novelty_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.FA.NoveltyRequest { Identifier = long.TryParse(reader["v_rtr_novelty_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null,
                    Reason = reader["v_rtr_reason"] != null ?
                        reader["v_rtr_reason"].ToString() : string.Empty,
                    Identifier = reader["v_rtr_id_Pk"] != null ?
                        (long.TryParse(reader["v_rtr_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    Number = reader["v_rtr_number"] != null ?
                        reader["v_rtr_number"].ToString() : string.Empty,
                    Type = reader["v_rtr_type_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Parameter<System.String> { Identifier = int.TryParse(reader["v_rtr_type_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Informed = reader["v_rtr_informed_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_rtr_informed_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Responsible = reader["v_rtr_responsible_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_rtr_responsible_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Accountable = reader["v_rtr_accountable_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_rtr_accountable_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Details = reader["v_rtr_details"] != null ?
                        reader["v_rtr_details"].ToString() : string.Empty,
                    Comments = reader["v_rtr_comments"] != null ?
                        reader["v_rtr_comments"].ToString() : string.Empty,
                    Topic = reader["v_rtr_topic"] != null ?
                        reader["v_rtr_topic"].ToString() : string.Empty,
                    Status = reader["v_rtr_status"] != null ?
                        reader["v_rtr_status"].ToString() : string.Empty,
                    UpdatedAt = reader["v_rtr_updated_at"] != null ?
                        (DateTime.TryParse(reader["v_rtr_updated_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                    RegisteredAt = reader["v_rtr_registered_at"] != null ?
                        (DateTime.TryParse(reader["v_rtr_registered_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                };
                return value;
            };


            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            SOFTTEK.SCMS.Entity.FA.RetirementRequest result = dataProvider.ExecuteForEntityWithFilter(SCMS.FA_SP_Update_RetirementRequest, filterDelegate, filter, mapper);
            return result;
        }
        #endregion
        #region TechnicalEvaluationRequest

        /// <summary>
        /// Retrieves a list of TechnicalEvaluationRequest instances that matches a given filter criteria by the TechnicalEvaluationRequest object instance 
        /// </summary>
        /// <param name="filter">Object with the information required to filter the set of results</param>
        /// <returns>List of TechnicalEvaluationRequest instances that matches the filter.</returns>
        public List<SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest> GetTechnicalEvaluationRequests(SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest filter)
        {
            Func<SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest, List<System.Data.IDataParameter>> filterDelegate = (f) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
                //Token
                System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p_tok.Direction = System.Data.ParameterDirection.Input;
                p_tok.DbType = System.Data.DbType.String;
                parameters.Add(p_tok);
                //Concept
                System.Data.IDataParameter p_filter_tchevlrqs_concept = dataProvider.GetParameter("@filter_tchevlrqs_concept",
                    f.Concept == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Concept);
                p_filter_tchevlrqs_concept.Direction = System.Data.ParameterDirection.Input;
                p_filter_tchevlrqs_concept.DbType = DbType.String;
                parameters.Add(p_filter_tchevlrqs_concept);
                //Novelty
                System.Data.IDataParameter p_filter_tchevlrqs_novelty_Fk = dataProvider.GetParameter("@filter_tchevlrqs_novelty_Fk",
                    f.Novelty == default(SOFTTEK.SCMS.Entity.FA.NoveltyRequest) ?
                        (object)System.DBNull.Value :
                        (object)f.Novelty.Identifier);
                p_filter_tchevlrqs_novelty_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_tchevlrqs_novelty_Fk.DbType = DbType.Int64;
                parameters.Add(p_filter_tchevlrqs_novelty_Fk);
                //Identifier
                System.Data.IDataParameter p_filter_tchevlrqs_id_Pk = dataProvider.GetParameter("@filter_tchevlrqs_id_Pk",
                    f.Identifier == default(System.Int64) ?
                        (object)System.DBNull.Value :
                        (object)f.Identifier);
                p_filter_tchevlrqs_id_Pk.Direction = System.Data.ParameterDirection.Input;
                p_filter_tchevlrqs_id_Pk.DbType = DbType.Int64;
                parameters.Add(p_filter_tchevlrqs_id_Pk);
                //Number
                System.Data.IDataParameter p_filter_tchevlrqs_number = dataProvider.GetParameter("@filter_tchevlrqs_number",
                    f.Number == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Number);
                p_filter_tchevlrqs_number.Direction = System.Data.ParameterDirection.Input;
                p_filter_tchevlrqs_number.DbType = DbType.String;
                parameters.Add(p_filter_tchevlrqs_number);
                //Type
                System.Data.IDataParameter p_filter_tchevlrqs_type_Fk = dataProvider.GetParameter("@filter_tchevlrqs_type_Fk",
                    f.Type == default(SOFTTEK.SCMS.Entity.Shared.Parameter<System.String>) ?
                        (object)System.DBNull.Value :
                        (object)f.Type.Identifier);
                p_filter_tchevlrqs_type_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_tchevlrqs_type_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_tchevlrqs_type_Fk);
                //Informed
                System.Data.IDataParameter p_filter_tchevlrqs_informed_Fk = dataProvider.GetParameter("@filter_tchevlrqs_informed_Fk",
                    f.Informed == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Informed.Identifier);
                p_filter_tchevlrqs_informed_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_tchevlrqs_informed_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_tchevlrqs_informed_Fk);
                //Responsible
                System.Data.IDataParameter p_filter_tchevlrqs_responsible_Fk = dataProvider.GetParameter("@filter_tchevlrqs_responsible_Fk",
                    f.Responsible == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Responsible.Identifier);
                p_filter_tchevlrqs_responsible_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_tchevlrqs_responsible_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_tchevlrqs_responsible_Fk);
                //Accountable
                System.Data.IDataParameter p_filter_tchevlrqs_accountable_Fk = dataProvider.GetParameter("@filter_tchevlrqs_accountable_Fk",
                    f.Accountable == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Accountable.Identifier);
                p_filter_tchevlrqs_accountable_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_tchevlrqs_accountable_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_tchevlrqs_accountable_Fk);
                //Details
                System.Data.IDataParameter p_filter_tchevlrqs_details = dataProvider.GetParameter("@filter_tchevlrqs_details",
                    f.Details == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Details);
                p_filter_tchevlrqs_details.Direction = System.Data.ParameterDirection.Input;
                p_filter_tchevlrqs_details.DbType = DbType.String;
                parameters.Add(p_filter_tchevlrqs_details);
                //Comments
                System.Data.IDataParameter p_filter_tchevlrqs_comments = dataProvider.GetParameter("@filter_tchevlrqs_comments",
                    f.Comments == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Comments);
                p_filter_tchevlrqs_comments.Direction = System.Data.ParameterDirection.Input;
                p_filter_tchevlrqs_comments.DbType = DbType.String;
                parameters.Add(p_filter_tchevlrqs_comments);
                //Topic
                System.Data.IDataParameter p_filter_tchevlrqs_topic = dataProvider.GetParameter("@filter_tchevlrqs_topic",
                    f.Topic == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Topic);
                p_filter_tchevlrqs_topic.Direction = System.Data.ParameterDirection.Input;
                p_filter_tchevlrqs_topic.DbType = DbType.String;
                parameters.Add(p_filter_tchevlrqs_topic);
                //Status
                System.Data.IDataParameter p_filter_tchevlrqs_status = dataProvider.GetParameter("@filter_tchevlrqs_status",
                    f.Status == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Status);
                p_filter_tchevlrqs_status.Direction = System.Data.ParameterDirection.Input;
                p_filter_tchevlrqs_status.DbType = DbType.String;
                parameters.Add(p_filter_tchevlrqs_status);
                //UpdatedAt
                System.Data.IDataParameter p_filter_tchevlrqs_updated_at = dataProvider.GetParameter("@filter_tchevlrqs_updated_at",
                    f.UpdatedAt == default(System.DateTime) ?
                        (object)System.DBNull.Value :
                        (object)f.UpdatedAt);
                p_filter_tchevlrqs_updated_at.Direction = System.Data.ParameterDirection.Input;
                p_filter_tchevlrqs_updated_at.DbType = DbType.DateTime;
                parameters.Add(p_filter_tchevlrqs_updated_at);
                //RegisteredAt
                System.Data.IDataParameter p_filter_tchevlrqs_registered_at = dataProvider.GetParameter("@filter_tchevlrqs_registered_at",
                    f.RegisteredAt == default(System.DateTime) ?
                        (object)System.DBNull.Value :
                        (object)f.RegisteredAt);
                p_filter_tchevlrqs_registered_at.Direction = System.Data.ParameterDirection.Input;
                p_filter_tchevlrqs_registered_at.DbType = DbType.DateTime;
                parameters.Add(p_filter_tchevlrqs_registered_at);
                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest> mapper = (reader) =>
            {
                long longHelper = default(long);
                int intHelper = default(int);
                DateTime dateHelper = default(DateTime);

                SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest value = new SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest
                {
                    Concept = reader["tchevlrqs_concept"] != null ?
                        reader["tchevlrqs_concept"].ToString() : string.Empty,
                    Novelty = reader["tchevlrqs_novelty_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.FA.NoveltyRequest { Identifier = long.TryParse(reader["tchevlrqs_novelty_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null,
                    Identifier = reader["tchevlrqs_id_Pk"] != null ?
                        (long.TryParse(reader["tchevlrqs_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    Number = reader["tchevlrqs_number"] != null ?
                        reader["tchevlrqs_number"].ToString() : string.Empty,
                    Type = reader["tchevlrqs_type_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Parameter<System.String> { Identifier = int.TryParse(reader["tchevlrqs_type_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Informed = reader["tchevlrqs_informed_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["tchevlrqs_informed_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Responsible = reader["tchevlrqs_responsible_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["tchevlrqs_responsible_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Accountable = reader["tchevlrqs_accountable_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["tchevlrqs_accountable_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Details = reader["tchevlrqs_details"] != null ?
                        reader["tchevlrqs_details"].ToString() : string.Empty,
                    Comments = reader["tchevlrqs_comments"] != null ?
                        reader["tchevlrqs_comments"].ToString() : string.Empty,
                    Topic = reader["tchevlrqs_topic"] != null ?
                        reader["tchevlrqs_topic"].ToString() : string.Empty,
                    Status = reader["tchevlrqs_status"] != null ?
                        reader["tchevlrqs_status"].ToString() : string.Empty,
                    UpdatedAt = reader["tchevlrqs_updated_at"] != null ?
                        (DateTime.TryParse(reader["tchevlrqs_updated_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                    RegisteredAt = reader["tchevlrqs_registered_at"] != null ?
                        (DateTime.TryParse(reader["tchevlrqs_registered_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                };
                return value;
            };

            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            List<SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest> result = dataProvider.ExecuteReaderWithFilter(SCMS.FA_SP_Select_TechnicalEvaluationRequest, filterDelegate, filter, mapper);
            return result;
        }

        /// <summary>
        /// Inserts the instance of the provided TechnicalEvaluationRequest at the Data Context datasource  
        /// </summary>
        /// <param name="instance">Instance of TechnicalEvaluationRequest with the entity´s information to store</param>
        /// <returns>Instance of TechnicalEvaluationRequest with the aditional information provided by the Data Context datasource</returns>
        public SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest InsertTechnicalEvaluationRequest(SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest instance)
        {
            Func<SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest, List<System.Data.IDataParameter>> input = (i) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();

                //Token
                System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p_tok.Direction = System.Data.ParameterDirection.Input;
                p_tok.DbType = System.Data.DbType.String;
                parameters.Add(p_tok);

                // Insert
                //Concept
                System.Data.IDataParameter p_new_tchevlrqs_concept = dataProvider.GetParameter("@new_tchevlrqs_concept",
                    i.Concept == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Concept);
                p_new_tchevlrqs_concept.Direction = System.Data.ParameterDirection.Input;
                p_new_tchevlrqs_concept.DbType = DbType.String;
                parameters.Add(p_new_tchevlrqs_concept);
                //Novelty
                System.Data.IDataParameter p_new_tchevlrqs_novelty_Fk = dataProvider.GetParameter("@new_tchevlrqs_novelty_Fk",
                    i.Novelty == default(SOFTTEK.SCMS.Entity.FA.NoveltyRequest) ?
                        (object)System.DBNull.Value :
                        (object)i.Novelty.Identifier);
                p_new_tchevlrqs_novelty_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_tchevlrqs_novelty_Fk.DbType = DbType.Int64;
                parameters.Add(p_new_tchevlrqs_novelty_Fk);
                //Number
                System.Data.IDataParameter p_new_tchevlrqs_number = dataProvider.GetParameter("@new_tchevlrqs_number",
                    i.Number == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Number);
                p_new_tchevlrqs_number.Direction = System.Data.ParameterDirection.Input;
                p_new_tchevlrqs_number.DbType = DbType.String;
                parameters.Add(p_new_tchevlrqs_number);
                //Type
                System.Data.IDataParameter p_new_tchevlrqs_type_Fk = dataProvider.GetParameter("@new_tchevlrqs_type_Fk",
                    i.Type == default(SOFTTEK.SCMS.Entity.Shared.Parameter<System.String>) ?
                        (object)System.DBNull.Value :
                        (object)i.Type.Identifier);
                p_new_tchevlrqs_type_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_tchevlrqs_type_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_tchevlrqs_type_Fk);
                //Informed
                System.Data.IDataParameter p_new_tchevlrqs_informed_Fk = dataProvider.GetParameter("@new_tchevlrqs_informed_Fk",
                    i.Informed == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)i.Informed.Identifier);
                p_new_tchevlrqs_informed_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_tchevlrqs_informed_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_tchevlrqs_informed_Fk);
                //Responsible
                System.Data.IDataParameter p_new_tchevlrqs_responsible_Fk = dataProvider.GetParameter("@new_tchevlrqs_responsible_Fk",
                    i.Responsible == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)i.Responsible.Identifier);
                p_new_tchevlrqs_responsible_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_tchevlrqs_responsible_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_tchevlrqs_responsible_Fk);
                //Accountable
                System.Data.IDataParameter p_new_tchevlrqs_accountable_Fk = dataProvider.GetParameter("@new_tchevlrqs_accountable_Fk",
                    i.Accountable == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)i.Accountable.Identifier);
                p_new_tchevlrqs_accountable_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_tchevlrqs_accountable_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_tchevlrqs_accountable_Fk);
                //Details
                System.Data.IDataParameter p_new_tchevlrqs_details = dataProvider.GetParameter("@new_tchevlrqs_details",
                    i.Details == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Details);
                p_new_tchevlrqs_details.Direction = System.Data.ParameterDirection.Input;
                p_new_tchevlrqs_details.DbType = DbType.String;
                parameters.Add(p_new_tchevlrqs_details);
                //Comments
                System.Data.IDataParameter p_new_tchevlrqs_comments = dataProvider.GetParameter("@new_tchevlrqs_comments",
                    i.Comments == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Comments);
                p_new_tchevlrqs_comments.Direction = System.Data.ParameterDirection.Input;
                p_new_tchevlrqs_comments.DbType = DbType.String;
                parameters.Add(p_new_tchevlrqs_comments);
                //Topic
                System.Data.IDataParameter p_new_tchevlrqs_topic = dataProvider.GetParameter("@new_tchevlrqs_topic",
                    i.Topic == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Topic);
                p_new_tchevlrqs_topic.Direction = System.Data.ParameterDirection.Input;
                p_new_tchevlrqs_topic.DbType = DbType.String;
                parameters.Add(p_new_tchevlrqs_topic);
                //Status
                System.Data.IDataParameter p_new_tchevlrqs_status = dataProvider.GetParameter("@new_tchevlrqs_status",
                    i.Status == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Status);
                p_new_tchevlrqs_status.Direction = System.Data.ParameterDirection.Input;
                p_new_tchevlrqs_status.DbType = DbType.String;
                parameters.Add(p_new_tchevlrqs_status);

                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest> mapper = (reader) =>
            {
                long longHelper = default(long);
                int intHelper = default(int);
                DateTime dateHelper = default(DateTime);

                SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest value = new SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest
                {
                    Concept = reader["v_tchevlrqs_concept"] != null ?
                        reader["v_tchevlrqs_concept"].ToString() : string.Empty,
                    Novelty = reader["v_tchevlrqs_novelty_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.FA.NoveltyRequest { Identifier = long.TryParse(reader["v_tchevlrqs_novelty_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null,
                    Identifier = reader["v_tchevlrqs_id_Pk"] != null ?
                        (long.TryParse(reader["v_tchevlrqs_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    Number = reader["v_tchevlrqs_number"] != null ?
                        reader["v_tchevlrqs_number"].ToString() : string.Empty,
                    Type = reader["v_tchevlrqs_type_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Parameter<System.String> { Identifier = int.TryParse(reader["v_tchevlrqs_type_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Informed = reader["v_tchevlrqs_informed_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_tchevlrqs_informed_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Responsible = reader["v_tchevlrqs_responsible_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_tchevlrqs_responsible_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Accountable = reader["v_tchevlrqs_accountable_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_tchevlrqs_accountable_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Details = reader["v_tchevlrqs_details"] != null ?
                        reader["v_tchevlrqs_details"].ToString() : string.Empty,
                    Comments = reader["v_tchevlrqs_comments"] != null ?
                        reader["v_tchevlrqs_comments"].ToString() : string.Empty,
                    Topic = reader["v_tchevlrqs_topic"] != null ?
                        reader["v_tchevlrqs_topic"].ToString() : string.Empty,
                    Status = reader["v_tchevlrqs_status"] != null ?
                        reader["v_tchevlrqs_status"].ToString() : string.Empty,
                    UpdatedAt = reader["v_tchevlrqs_updated_at"] != null ?
                        (DateTime.TryParse(reader["v_tchevlrqs_updated_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                    RegisteredAt = reader["v_tchevlrqs_registered_at"] != null ?
                        (DateTime.TryParse(reader["v_tchevlrqs_registered_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                };
                return value;
            };


            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest result = dataProvider.ExecuteForEntityWithFilter(SCMS.FA_SP_Insert_TechnicalEvaluationRequest, input, instance, mapper);
            return result;
        }

        /// <summary>
        /// Update the entity's stored information with that provided by the instance of TechnicalEvaluationRequest at the Data Context datasource  
        /// </summary>
        /// <param name="instance">Instance of TechnicalEvaluationRequest with the entity´s information to update</param>
        /// <param name="filter">Object with the information required to filter the target stored entity´s information</param>
        /// <returns>Instance of TechnicalEvaluationRequest with the updated information provided by the Data Context datasource</returns>
        public SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest UpdateTechnicalEvaluationRequest(SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest instance, SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest filter)
        {
            Func<SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest, List<System.Data.IDataParameter>> filterDelegate = (f) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();

                //Token
                System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p_tok.Direction = System.Data.ParameterDirection.Input;
                p_tok.DbType = System.Data.DbType.String;
                parameters.Add(p_tok);

                // Filter
                //Concept
                System.Data.IDataParameter p_filter_tchevlrqs_concept = dataProvider.GetParameter("@filter_tchevlrqs_concept",
                    f.Concept == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Concept);
                p_filter_tchevlrqs_concept.Direction = System.Data.ParameterDirection.Input;
                p_filter_tchevlrqs_concept.DbType = DbType.String;
                parameters.Add(p_filter_tchevlrqs_concept);
                //Novelty
                System.Data.IDataParameter p_filter_tchevlrqs_novelty_Fk = dataProvider.GetParameter("@filter_tchevlrqs_novelty_Fk",
                    f.Novelty == default(SOFTTEK.SCMS.Entity.FA.NoveltyRequest) ?
                        (object)System.DBNull.Value :
                        (object)f.Novelty.Identifier);
                p_filter_tchevlrqs_novelty_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_tchevlrqs_novelty_Fk.DbType = DbType.Int64;
                parameters.Add(p_filter_tchevlrqs_novelty_Fk);
                //Identifier
                System.Data.IDataParameter p_filter_tchevlrqs_id_Pk = dataProvider.GetParameter("@filter_tchevlrqs_id_Pk",
                    f.Identifier == default(System.Int64) ?
                        (object)System.DBNull.Value :
                        (object)f.Identifier);
                p_filter_tchevlrqs_id_Pk.Direction = System.Data.ParameterDirection.Input;
                p_filter_tchevlrqs_id_Pk.DbType = DbType.Int64;
                parameters.Add(p_filter_tchevlrqs_id_Pk);
                //Number
                System.Data.IDataParameter p_filter_tchevlrqs_number = dataProvider.GetParameter("@filter_tchevlrqs_number",
                    f.Number == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Number);
                p_filter_tchevlrqs_number.Direction = System.Data.ParameterDirection.Input;
                p_filter_tchevlrqs_number.DbType = DbType.String;
                parameters.Add(p_filter_tchevlrqs_number);
                //Type
                System.Data.IDataParameter p_filter_tchevlrqs_type_Fk = dataProvider.GetParameter("@filter_tchevlrqs_type_Fk",
                    f.Type == default(SOFTTEK.SCMS.Entity.Shared.Parameter<System.String>) ?
                        (object)System.DBNull.Value :
                        (object)f.Type.Identifier);
                p_filter_tchevlrqs_type_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_tchevlrqs_type_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_tchevlrqs_type_Fk);
                //Informed
                System.Data.IDataParameter p_filter_tchevlrqs_informed_Fk = dataProvider.GetParameter("@filter_tchevlrqs_informed_Fk",
                    f.Informed == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Informed.Identifier);
                p_filter_tchevlrqs_informed_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_tchevlrqs_informed_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_tchevlrqs_informed_Fk);
                //Responsible
                System.Data.IDataParameter p_filter_tchevlrqs_responsible_Fk = dataProvider.GetParameter("@filter_tchevlrqs_responsible_Fk",
                    f.Responsible == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Responsible.Identifier);
                p_filter_tchevlrqs_responsible_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_tchevlrqs_responsible_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_tchevlrqs_responsible_Fk);
                //Accountable
                System.Data.IDataParameter p_filter_tchevlrqs_accountable_Fk = dataProvider.GetParameter("@filter_tchevlrqs_accountable_Fk",
                    f.Accountable == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Accountable.Identifier);
                p_filter_tchevlrqs_accountable_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_tchevlrqs_accountable_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_tchevlrqs_accountable_Fk);
                //Details
                System.Data.IDataParameter p_filter_tchevlrqs_details = dataProvider.GetParameter("@filter_tchevlrqs_details",
                    f.Details == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Details);
                p_filter_tchevlrqs_details.Direction = System.Data.ParameterDirection.Input;
                p_filter_tchevlrqs_details.DbType = DbType.String;
                parameters.Add(p_filter_tchevlrqs_details);
                //Comments
                System.Data.IDataParameter p_filter_tchevlrqs_comments = dataProvider.GetParameter("@filter_tchevlrqs_comments",
                    f.Comments == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Comments);
                p_filter_tchevlrqs_comments.Direction = System.Data.ParameterDirection.Input;
                p_filter_tchevlrqs_comments.DbType = DbType.String;
                parameters.Add(p_filter_tchevlrqs_comments);
                //Topic
                System.Data.IDataParameter p_filter_tchevlrqs_topic = dataProvider.GetParameter("@filter_tchevlrqs_topic",
                    f.Topic == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Topic);
                p_filter_tchevlrqs_topic.Direction = System.Data.ParameterDirection.Input;
                p_filter_tchevlrqs_topic.DbType = DbType.String;
                parameters.Add(p_filter_tchevlrqs_topic);
                //Status
                System.Data.IDataParameter p_filter_tchevlrqs_status = dataProvider.GetParameter("@filter_tchevlrqs_status",
                    f.Status == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Status);
                p_filter_tchevlrqs_status.Direction = System.Data.ParameterDirection.Input;
                p_filter_tchevlrqs_status.DbType = DbType.String;
                parameters.Add(p_filter_tchevlrqs_status);
                //UpdatedAt
                System.Data.IDataParameter p_filter_tchevlrqs_updated_at = dataProvider.GetParameter("@filter_tchevlrqs_updated_at",
                    f.UpdatedAt == default(System.DateTime) ?
                        (object)System.DBNull.Value :
                        (object)f.UpdatedAt);
                p_filter_tchevlrqs_updated_at.Direction = System.Data.ParameterDirection.Input;
                p_filter_tchevlrqs_updated_at.DbType = DbType.DateTime;
                parameters.Add(p_filter_tchevlrqs_updated_at);
                //RegisteredAt
                System.Data.IDataParameter p_filter_tchevlrqs_registered_at = dataProvider.GetParameter("@filter_tchevlrqs_registered_at",
                    f.RegisteredAt == default(System.DateTime) ?
                        (object)System.DBNull.Value :
                        (object)f.RegisteredAt);
                p_filter_tchevlrqs_registered_at.Direction = System.Data.ParameterDirection.Input;
                p_filter_tchevlrqs_registered_at.DbType = DbType.DateTime;
                parameters.Add(p_filter_tchevlrqs_registered_at);
                // Update
                //Concept
                System.Data.IDataParameter p_new_tchevlrqs_concept = dataProvider.GetParameter("@new_tchevlrqs_concept",
                    f.Concept == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Concept);
                p_new_tchevlrqs_concept.Direction = System.Data.ParameterDirection.Input;
                p_new_tchevlrqs_concept.DbType = DbType.String;
                parameters.Add(p_new_tchevlrqs_concept);
                //Novelty
                System.Data.IDataParameter p_new_tchevlrqs_novelty_Fk = dataProvider.GetParameter("@new_tchevlrqs_novelty_Fk",
                    instance.Novelty == default(SOFTTEK.SCMS.Entity.FA.NoveltyRequest) ?
                        (object)System.DBNull.Value :
                        (object)instance.Novelty.Identifier);
                p_new_tchevlrqs_novelty_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_tchevlrqs_novelty_Fk.DbType = DbType.Int64;
                parameters.Add(p_new_tchevlrqs_novelty_Fk);
                //Number
                System.Data.IDataParameter p_new_tchevlrqs_number = dataProvider.GetParameter("@new_tchevlrqs_number",
                    f.Number == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Number);
                p_new_tchevlrqs_number.Direction = System.Data.ParameterDirection.Input;
                p_new_tchevlrqs_number.DbType = DbType.String;
                parameters.Add(p_new_tchevlrqs_number);
                //Type
                System.Data.IDataParameter p_new_tchevlrqs_type_Fk = dataProvider.GetParameter("@new_tchevlrqs_type_Fk",
                    instance.Type == default(SOFTTEK.SCMS.Entity.Shared.Parameter<System.String>) ?
                        (object)System.DBNull.Value :
                        (object)instance.Type.Identifier);
                p_new_tchevlrqs_type_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_tchevlrqs_type_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_tchevlrqs_type_Fk);
                //Informed
                System.Data.IDataParameter p_new_tchevlrqs_informed_Fk = dataProvider.GetParameter("@new_tchevlrqs_informed_Fk",
                    instance.Informed == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)instance.Informed.Identifier);
                p_new_tchevlrqs_informed_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_tchevlrqs_informed_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_tchevlrqs_informed_Fk);
                //Responsible
                System.Data.IDataParameter p_new_tchevlrqs_responsible_Fk = dataProvider.GetParameter("@new_tchevlrqs_responsible_Fk",
                    instance.Responsible == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)instance.Responsible.Identifier);
                p_new_tchevlrqs_responsible_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_tchevlrqs_responsible_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_tchevlrqs_responsible_Fk);
                //Accountable
                System.Data.IDataParameter p_new_tchevlrqs_accountable_Fk = dataProvider.GetParameter("@new_tchevlrqs_accountable_Fk",
                    instance.Accountable == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)instance.Accountable.Identifier);
                p_new_tchevlrqs_accountable_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_tchevlrqs_accountable_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_tchevlrqs_accountable_Fk);
                //Details
                System.Data.IDataParameter p_new_tchevlrqs_details = dataProvider.GetParameter("@new_tchevlrqs_details",
                    f.Details == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Details);
                p_new_tchevlrqs_details.Direction = System.Data.ParameterDirection.Input;
                p_new_tchevlrqs_details.DbType = DbType.String;
                parameters.Add(p_new_tchevlrqs_details);
                //Comments
                System.Data.IDataParameter p_new_tchevlrqs_comments = dataProvider.GetParameter("@new_tchevlrqs_comments",
                    f.Comments == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Comments);
                p_new_tchevlrqs_comments.Direction = System.Data.ParameterDirection.Input;
                p_new_tchevlrqs_comments.DbType = DbType.String;
                parameters.Add(p_new_tchevlrqs_comments);
                //Topic
                System.Data.IDataParameter p_new_tchevlrqs_topic = dataProvider.GetParameter("@new_tchevlrqs_topic",
                    f.Topic == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Topic);
                p_new_tchevlrqs_topic.Direction = System.Data.ParameterDirection.Input;
                p_new_tchevlrqs_topic.DbType = DbType.String;
                parameters.Add(p_new_tchevlrqs_topic);
                //Status
                System.Data.IDataParameter p_new_tchevlrqs_status = dataProvider.GetParameter("@new_tchevlrqs_status",
                    f.Status == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Status);
                p_new_tchevlrqs_status.Direction = System.Data.ParameterDirection.Input;
                p_new_tchevlrqs_status.DbType = DbType.String;
                parameters.Add(p_new_tchevlrqs_status);

                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest> mapper = (reader) =>
            {
                long longHelper = default(long);
                int intHelper = default(int);
                DateTime dateHelper = default(DateTime);

                SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest value = new SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest
                {
                    Concept = reader["v_tchevlrqs_concept"] != null ?
                        reader["v_tchevlrqs_concept"].ToString() : string.Empty,
                    Novelty = reader["v_tchevlrqs_novelty_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.FA.NoveltyRequest { Identifier = long.TryParse(reader["v_tchevlrqs_novelty_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null,
                    Identifier = reader["v_tchevlrqs_id_Pk"] != null ?
                        (long.TryParse(reader["v_tchevlrqs_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    Number = reader["v_tchevlrqs_number"] != null ?
                        reader["v_tchevlrqs_number"].ToString() : string.Empty,
                    Type = reader["v_tchevlrqs_type_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Parameter<System.String> { Identifier = int.TryParse(reader["v_tchevlrqs_type_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Informed = reader["v_tchevlrqs_informed_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_tchevlrqs_informed_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Responsible = reader["v_tchevlrqs_responsible_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_tchevlrqs_responsible_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Accountable = reader["v_tchevlrqs_accountable_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_tchevlrqs_accountable_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Details = reader["v_tchevlrqs_details"] != null ?
                        reader["v_tchevlrqs_details"].ToString() : string.Empty,
                    Comments = reader["v_tchevlrqs_comments"] != null ?
                        reader["v_tchevlrqs_comments"].ToString() : string.Empty,
                    Topic = reader["v_tchevlrqs_topic"] != null ?
                        reader["v_tchevlrqs_topic"].ToString() : string.Empty,
                    Status = reader["v_tchevlrqs_status"] != null ?
                        reader["v_tchevlrqs_status"].ToString() : string.Empty,
                    UpdatedAt = reader["v_tchevlrqs_updated_at"] != null ?
                        (DateTime.TryParse(reader["v_tchevlrqs_updated_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                    RegisteredAt = reader["v_tchevlrqs_registered_at"] != null ?
                        (DateTime.TryParse(reader["v_tchevlrqs_registered_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                };
                return value;
            };


            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest result = dataProvider.ExecuteForEntityWithFilter(SCMS.FA_SP_Update_TechnicalEvaluationRequest, filterDelegate, filter, mapper);
            return result;
        }
        #endregion
        #region TransferRequest

        /// <summary>
        /// Retrieves a list of TransferRequest instances that matches a given filter criteria by the TransferRequest object instance 
        /// </summary>
        /// <param name="filter">Object with the information required to filter the set of results</param>
        /// <returns>List of TransferRequest instances that matches the filter.</returns>
        public List<SOFTTEK.SCMS.Entity.FA.TransferRequest> GetTransferRequests(SOFTTEK.SCMS.Entity.FA.TransferRequest filter)
        {
            Func<SOFTTEK.SCMS.Entity.FA.TransferRequest, List<System.Data.IDataParameter>> filterDelegate = (f) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
                //Token
                System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p_tok.Direction = System.Data.ParameterDirection.Input;
                p_tok.DbType = System.Data.DbType.String;
                parameters.Add(p_tok);
                //Origin
                System.Data.IDataParameter p_filter_trnrqs_origin = dataProvider.GetParameter("@filter_trnrqs_origin",
                    f.Origin == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Origin);
                p_filter_trnrqs_origin.Direction = System.Data.ParameterDirection.Input;
                p_filter_trnrqs_origin.DbType = DbType.String;
                parameters.Add(p_filter_trnrqs_origin);
                //Destination
                System.Data.IDataParameter p_filter_trnrqs_destination = dataProvider.GetParameter("@filter_trnrqs_destination",
                    f.Destination == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Destination);
                p_filter_trnrqs_destination.Direction = System.Data.ParameterDirection.Input;
                p_filter_trnrqs_destination.DbType = DbType.String;
                parameters.Add(p_filter_trnrqs_destination);
                //ExternalIdentifier
                System.Data.IDataParameter p_filter_trnrqs_external_identifier = dataProvider.GetParameter("@filter_trnrqs_external_identifier",
                    f.ExternalIdentifier == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.ExternalIdentifier);
                p_filter_trnrqs_external_identifier.Direction = System.Data.ParameterDirection.Input;
                p_filter_trnrqs_external_identifier.DbType = DbType.String;
                parameters.Add(p_filter_trnrqs_external_identifier);
                //Novelty
                System.Data.IDataParameter p_filter_trnrqs_novelty_Fk = dataProvider.GetParameter("@filter_trnrqs_novelty_Fk",
                    f.Novelty == default(SOFTTEK.SCMS.Entity.FA.NoveltyRequest) ?
                        (object)System.DBNull.Value :
                        (object)f.Novelty.Identifier);
                p_filter_trnrqs_novelty_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_trnrqs_novelty_Fk.DbType = DbType.Int64;
                parameters.Add(p_filter_trnrqs_novelty_Fk);
                //Identifier
                System.Data.IDataParameter p_filter_trnrqs_id_Pk = dataProvider.GetParameter("@filter_trnrqs_id_Pk",
                    f.Identifier == default(System.Int64) ?
                        (object)System.DBNull.Value :
                        (object)f.Identifier);
                p_filter_trnrqs_id_Pk.Direction = System.Data.ParameterDirection.Input;
                p_filter_trnrqs_id_Pk.DbType = DbType.Int64;
                parameters.Add(p_filter_trnrqs_id_Pk);
                //Number
                System.Data.IDataParameter p_filter_trnrqs_number = dataProvider.GetParameter("@filter_trnrqs_number",
                    f.Number == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Number);
                p_filter_trnrqs_number.Direction = System.Data.ParameterDirection.Input;
                p_filter_trnrqs_number.DbType = DbType.String;
                parameters.Add(p_filter_trnrqs_number);
                //Type
                System.Data.IDataParameter p_filter_trnrqs_type_Fk = dataProvider.GetParameter("@filter_trnrqs_type_Fk",
                    f.Type == default(SOFTTEK.SCMS.Entity.Shared.Parameter<System.String>) ?
                        (object)System.DBNull.Value :
                        (object)f.Type.Identifier);
                p_filter_trnrqs_type_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_trnrqs_type_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_trnrqs_type_Fk);
                //Informed
                System.Data.IDataParameter p_filter_trnrqs_informed_Fk = dataProvider.GetParameter("@filter_trnrqs_informed_Fk",
                    f.Informed == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Informed.Identifier);
                p_filter_trnrqs_informed_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_trnrqs_informed_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_trnrqs_informed_Fk);
                //Responsible
                System.Data.IDataParameter p_filter_trnrqs_responsible_Fk = dataProvider.GetParameter("@filter_trnrqs_responsible_Fk",
                    f.Responsible == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Responsible.Identifier);
                p_filter_trnrqs_responsible_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_trnrqs_responsible_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_trnrqs_responsible_Fk);
                //Accountable
                System.Data.IDataParameter p_filter_trnrqs_accountable_Fk = dataProvider.GetParameter("@filter_trnrqs_accountable_Fk",
                    f.Accountable == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Accountable.Identifier);
                p_filter_trnrqs_accountable_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_trnrqs_accountable_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_trnrqs_accountable_Fk);
                //Details
                System.Data.IDataParameter p_filter_trnrqs_details = dataProvider.GetParameter("@filter_trnrqs_details",
                    f.Details == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Details);
                p_filter_trnrqs_details.Direction = System.Data.ParameterDirection.Input;
                p_filter_trnrqs_details.DbType = DbType.String;
                parameters.Add(p_filter_trnrqs_details);
                //Comments
                System.Data.IDataParameter p_filter_trnrqs_comments = dataProvider.GetParameter("@filter_trnrqs_comments",
                    f.Comments == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Comments);
                p_filter_trnrqs_comments.Direction = System.Data.ParameterDirection.Input;
                p_filter_trnrqs_comments.DbType = DbType.String;
                parameters.Add(p_filter_trnrqs_comments);
                //Topic
                System.Data.IDataParameter p_filter_trnrqs_topic = dataProvider.GetParameter("@filter_trnrqs_topic",
                    f.Topic == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Topic);
                p_filter_trnrqs_topic.Direction = System.Data.ParameterDirection.Input;
                p_filter_trnrqs_topic.DbType = DbType.String;
                parameters.Add(p_filter_trnrqs_topic);
                //Status
                System.Data.IDataParameter p_filter_trnrqs_status = dataProvider.GetParameter("@filter_trnrqs_status",
                    f.Status == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Status);
                p_filter_trnrqs_status.Direction = System.Data.ParameterDirection.Input;
                p_filter_trnrqs_status.DbType = DbType.String;
                parameters.Add(p_filter_trnrqs_status);
                //UpdatedAt
                System.Data.IDataParameter p_filter_trnrqs_updated_at = dataProvider.GetParameter("@filter_trnrqs_updated_at",
                    f.UpdatedAt == default(System.DateTime) ?
                        (object)System.DBNull.Value :
                        (object)f.UpdatedAt);
                p_filter_trnrqs_updated_at.Direction = System.Data.ParameterDirection.Input;
                p_filter_trnrqs_updated_at.DbType = DbType.DateTime;
                parameters.Add(p_filter_trnrqs_updated_at);
                //RegisteredAt
                System.Data.IDataParameter p_filter_trnrqs_registered_at = dataProvider.GetParameter("@filter_trnrqs_registered_at",
                    f.RegisteredAt == default(System.DateTime) ?
                        (object)System.DBNull.Value :
                        (object)f.RegisteredAt);
                p_filter_trnrqs_registered_at.Direction = System.Data.ParameterDirection.Input;
                p_filter_trnrqs_registered_at.DbType = DbType.DateTime;
                parameters.Add(p_filter_trnrqs_registered_at);
                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.FA.TransferRequest> mapper = (reader) =>
            {
                long longHelper = default(long);
                int intHelper = default(int);
                DateTime dateHelper = default(DateTime);

                SOFTTEK.SCMS.Entity.FA.TransferRequest value = new SOFTTEK.SCMS.Entity.FA.TransferRequest
                {
                    Origin = reader["trnrqs_origin"] != null ?
                        reader["trnrqs_origin"].ToString() : string.Empty,
                    Destination = reader["trnrqs_destination"] != null ?
                        reader["trnrqs_destination"].ToString() : string.Empty,
                    ExternalIdentifier = reader["trnrqs_external_identifier"] != null ?
                        reader["trnrqs_external_identifier"].ToString() : string.Empty,
                    Novelty = reader["trnrqs_novelty_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.FA.NoveltyRequest { Identifier = long.TryParse(reader["trnrqs_novelty_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null,
                    Identifier = reader["trnrqs_id_Pk"] != null ?
                        (long.TryParse(reader["trnrqs_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    Number = reader["trnrqs_number"] != null ?
                        reader["trnrqs_number"].ToString() : string.Empty,
                    Type = reader["trnrqs_type_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Parameter<System.String> { Identifier = int.TryParse(reader["trnrqs_type_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Informed = reader["trnrqs_informed_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["trnrqs_informed_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Responsible = reader["trnrqs_responsible_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["trnrqs_responsible_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Accountable = reader["trnrqs_accountable_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["trnrqs_accountable_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Details = reader["trnrqs_details"] != null ?
                        reader["trnrqs_details"].ToString() : string.Empty,
                    Comments = reader["trnrqs_comments"] != null ?
                        reader["trnrqs_comments"].ToString() : string.Empty,
                    Topic = reader["trnrqs_topic"] != null ?
                        reader["trnrqs_topic"].ToString() : string.Empty,
                    Status = reader["trnrqs_status"] != null ?
                        reader["trnrqs_status"].ToString() : string.Empty,
                    UpdatedAt = reader["trnrqs_updated_at"] != null ?
                        (DateTime.TryParse(reader["trnrqs_updated_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                    RegisteredAt = reader["trnrqs_registered_at"] != null ?
                        (DateTime.TryParse(reader["trnrqs_registered_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                };
                return value;
            };

            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            List<SOFTTEK.SCMS.Entity.FA.TransferRequest> result = dataProvider.ExecuteReaderWithFilter(SCMS.FA_SP_Select_TransferRequest, filterDelegate, filter, mapper);
            return result;
        }

        /// <summary>
        /// Inserts the instance of the provided TransferRequest at the Data Context datasource  
        /// </summary>
        /// <param name="instance">Instance of TransferRequest with the entity´s information to store</param>
        /// <returns>Instance of TransferRequest with the aditional information provided by the Data Context datasource</returns>
        public SOFTTEK.SCMS.Entity.FA.TransferRequest InsertTransferRequest(SOFTTEK.SCMS.Entity.FA.TransferRequest instance)
        {
            Func<SOFTTEK.SCMS.Entity.FA.TransferRequest, List<System.Data.IDataParameter>> input = (i) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();

                //Token
                System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p_tok.Direction = System.Data.ParameterDirection.Input;
                p_tok.DbType = System.Data.DbType.String;
                parameters.Add(p_tok);

                // Insert
                //Origin
                System.Data.IDataParameter p_new_trnrqs_origin = dataProvider.GetParameter("@new_trnrqs_origin",
                    i.Origin == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Origin);
                p_new_trnrqs_origin.Direction = System.Data.ParameterDirection.Input;
                p_new_trnrqs_origin.DbType = DbType.String;
                parameters.Add(p_new_trnrqs_origin);
                //Destination
                System.Data.IDataParameter p_new_trnrqs_destination = dataProvider.GetParameter("@new_trnrqs_destination",
                    i.Destination == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Destination);
                p_new_trnrqs_destination.Direction = System.Data.ParameterDirection.Input;
                p_new_trnrqs_destination.DbType = DbType.String;
                parameters.Add(p_new_trnrqs_destination);
                //ExternalIdentifier
                System.Data.IDataParameter p_new_trnrqs_external_identifier = dataProvider.GetParameter("@new_trnrqs_external_identifier",
                    i.ExternalIdentifier == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.ExternalIdentifier);
                p_new_trnrqs_external_identifier.Direction = System.Data.ParameterDirection.Input;
                p_new_trnrqs_external_identifier.DbType = DbType.String;
                parameters.Add(p_new_trnrqs_external_identifier);
                //Novelty
                System.Data.IDataParameter p_new_trnrqs_novelty_Fk = dataProvider.GetParameter("@new_trnrqs_novelty_Fk",
                    i.Novelty == default(SOFTTEK.SCMS.Entity.FA.NoveltyRequest) ?
                        (object)System.DBNull.Value :
                        (object)i.Novelty.Identifier);
                p_new_trnrqs_novelty_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_trnrqs_novelty_Fk.DbType = DbType.Int64;
                parameters.Add(p_new_trnrqs_novelty_Fk);
                //Number
                System.Data.IDataParameter p_new_trnrqs_number = dataProvider.GetParameter("@new_trnrqs_number",
                    i.Number == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Number);
                p_new_trnrqs_number.Direction = System.Data.ParameterDirection.Input;
                p_new_trnrqs_number.DbType = DbType.String;
                parameters.Add(p_new_trnrqs_number);
                //Type
                System.Data.IDataParameter p_new_trnrqs_type_Fk = dataProvider.GetParameter("@new_trnrqs_type_Fk",
                    i.Type == default(SOFTTEK.SCMS.Entity.Shared.Parameter<System.String>) ?
                        (object)System.DBNull.Value :
                        (object)i.Type.Identifier);
                p_new_trnrqs_type_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_trnrqs_type_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_trnrqs_type_Fk);
                //Informed
                System.Data.IDataParameter p_new_trnrqs_informed_Fk = dataProvider.GetParameter("@new_trnrqs_informed_Fk",
                    i.Informed == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)i.Informed.Identifier);
                p_new_trnrqs_informed_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_trnrqs_informed_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_trnrqs_informed_Fk);
                //Responsible
                System.Data.IDataParameter p_new_trnrqs_responsible_Fk = dataProvider.GetParameter("@new_trnrqs_responsible_Fk",
                    i.Responsible == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)i.Responsible.Identifier);
                p_new_trnrqs_responsible_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_trnrqs_responsible_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_trnrqs_responsible_Fk);
                //Accountable
                System.Data.IDataParameter p_new_trnrqs_accountable_Fk = dataProvider.GetParameter("@new_trnrqs_accountable_Fk",
                    i.Accountable == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)i.Accountable.Identifier);
                p_new_trnrqs_accountable_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_trnrqs_accountable_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_trnrqs_accountable_Fk);
                //Details
                System.Data.IDataParameter p_new_trnrqs_details = dataProvider.GetParameter("@new_trnrqs_details",
                    i.Details == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Details);
                p_new_trnrqs_details.Direction = System.Data.ParameterDirection.Input;
                p_new_trnrqs_details.DbType = DbType.String;
                parameters.Add(p_new_trnrqs_details);
                //Comments
                System.Data.IDataParameter p_new_trnrqs_comments = dataProvider.GetParameter("@new_trnrqs_comments",
                    i.Comments == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Comments);
                p_new_trnrqs_comments.Direction = System.Data.ParameterDirection.Input;
                p_new_trnrqs_comments.DbType = DbType.String;
                parameters.Add(p_new_trnrqs_comments);
                //Topic
                System.Data.IDataParameter p_new_trnrqs_topic = dataProvider.GetParameter("@new_trnrqs_topic",
                    i.Topic == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Topic);
                p_new_trnrqs_topic.Direction = System.Data.ParameterDirection.Input;
                p_new_trnrqs_topic.DbType = DbType.String;
                parameters.Add(p_new_trnrqs_topic);
                //Status
                System.Data.IDataParameter p_new_trnrqs_status = dataProvider.GetParameter("@new_trnrqs_status",
                    i.Status == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Status);
                p_new_trnrqs_status.Direction = System.Data.ParameterDirection.Input;
                p_new_trnrqs_status.DbType = DbType.String;
                parameters.Add(p_new_trnrqs_status);

                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.FA.TransferRequest> mapper = (reader) =>
            {
                long longHelper = default(long);
                int intHelper = default(int);
                DateTime dateHelper = default(DateTime);

                SOFTTEK.SCMS.Entity.FA.TransferRequest value = new SOFTTEK.SCMS.Entity.FA.TransferRequest
                {
                    Origin = reader["v_trnrqs_origin"] != null ?
                        reader["v_trnrqs_origin"].ToString() : string.Empty,
                    Destination = reader["v_trnrqs_destination"] != null ?
                        reader["v_trnrqs_destination"].ToString() : string.Empty,
                    ExternalIdentifier = reader["v_trnrqs_external_identifier"] != null ?
                        reader["v_trnrqs_external_identifier"].ToString() : string.Empty,
                    Novelty = reader["v_trnrqs_novelty_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.FA.NoveltyRequest { Identifier = long.TryParse(reader["v_trnrqs_novelty_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null,
                    Identifier = reader["v_trnrqs_id_Pk"] != null ?
                        (long.TryParse(reader["v_trnrqs_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    Number = reader["v_trnrqs_number"] != null ?
                        reader["v_trnrqs_number"].ToString() : string.Empty,
                    Type = reader["v_trnrqs_type_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Parameter<System.String> { Identifier = int.TryParse(reader["v_trnrqs_type_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Informed = reader["v_trnrqs_informed_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_trnrqs_informed_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Responsible = reader["v_trnrqs_responsible_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_trnrqs_responsible_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Accountable = reader["v_trnrqs_accountable_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_trnrqs_accountable_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Details = reader["v_trnrqs_details"] != null ?
                        reader["v_trnrqs_details"].ToString() : string.Empty,
                    Comments = reader["v_trnrqs_comments"] != null ?
                        reader["v_trnrqs_comments"].ToString() : string.Empty,
                    Topic = reader["v_trnrqs_topic"] != null ?
                        reader["v_trnrqs_topic"].ToString() : string.Empty,
                    Status = reader["v_trnrqs_status"] != null ?
                        reader["v_trnrqs_status"].ToString() : string.Empty,
                    UpdatedAt = reader["v_trnrqs_updated_at"] != null ?
                        (DateTime.TryParse(reader["v_trnrqs_updated_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                    RegisteredAt = reader["v_trnrqs_registered_at"] != null ?
                        (DateTime.TryParse(reader["v_trnrqs_registered_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                };
                return value;
            };


            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            SOFTTEK.SCMS.Entity.FA.TransferRequest result = dataProvider.ExecuteForEntityWithFilter(SCMS.FA_SP_Insert_TransferRequest, input, instance, mapper);
            return result;
        }

        /// <summary>
        /// Update the entity's stored information with that provided by the instance of TransferRequest at the Data Context datasource  
        /// </summary>
        /// <param name="instance">Instance of TransferRequest with the entity´s information to update</param>
        /// <param name="filter">Object with the information required to filter the target stored entity´s information</param>
        /// <returns>Instance of TransferRequest with the updated information provided by the Data Context datasource</returns>
        public SOFTTEK.SCMS.Entity.FA.TransferRequest UpdateTransferRequest(SOFTTEK.SCMS.Entity.FA.TransferRequest instance, SOFTTEK.SCMS.Entity.FA.TransferRequest filter)
        {
            Func<SOFTTEK.SCMS.Entity.FA.TransferRequest, List<System.Data.IDataParameter>> filterDelegate = (f) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();

                //Token
                System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p_tok.Direction = System.Data.ParameterDirection.Input;
                p_tok.DbType = System.Data.DbType.String;
                parameters.Add(p_tok);

                // Filter
                //Origin
                System.Data.IDataParameter p_filter_trnrqs_origin = dataProvider.GetParameter("@filter_trnrqs_origin",
                    f.Origin == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Origin);
                p_filter_trnrqs_origin.Direction = System.Data.ParameterDirection.Input;
                p_filter_trnrqs_origin.DbType = DbType.String;
                parameters.Add(p_filter_trnrqs_origin);
                //Destination
                System.Data.IDataParameter p_filter_trnrqs_destination = dataProvider.GetParameter("@filter_trnrqs_destination",
                    f.Destination == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Destination);
                p_filter_trnrqs_destination.Direction = System.Data.ParameterDirection.Input;
                p_filter_trnrqs_destination.DbType = DbType.String;
                parameters.Add(p_filter_trnrqs_destination);
                //ExternalIdentifier
                System.Data.IDataParameter p_filter_trnrqs_external_identifier = dataProvider.GetParameter("@filter_trnrqs_external_identifier",
                    f.ExternalIdentifier == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.ExternalIdentifier);
                p_filter_trnrqs_external_identifier.Direction = System.Data.ParameterDirection.Input;
                p_filter_trnrqs_external_identifier.DbType = DbType.String;
                parameters.Add(p_filter_trnrqs_external_identifier);
                //Novelty
                System.Data.IDataParameter p_filter_trnrqs_novelty_Fk = dataProvider.GetParameter("@filter_trnrqs_novelty_Fk",
                    f.Novelty == default(SOFTTEK.SCMS.Entity.FA.NoveltyRequest) ?
                        (object)System.DBNull.Value :
                        (object)f.Novelty.Identifier);
                p_filter_trnrqs_novelty_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_trnrqs_novelty_Fk.DbType = DbType.Int64;
                parameters.Add(p_filter_trnrqs_novelty_Fk);
                //Identifier
                System.Data.IDataParameter p_filter_trnrqs_id_Pk = dataProvider.GetParameter("@filter_trnrqs_id_Pk",
                    f.Identifier == default(System.Int64) ?
                        (object)System.DBNull.Value :
                        (object)f.Identifier);
                p_filter_trnrqs_id_Pk.Direction = System.Data.ParameterDirection.Input;
                p_filter_trnrqs_id_Pk.DbType = DbType.Int64;
                parameters.Add(p_filter_trnrqs_id_Pk);
                //Number
                System.Data.IDataParameter p_filter_trnrqs_number = dataProvider.GetParameter("@filter_trnrqs_number",
                    f.Number == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Number);
                p_filter_trnrqs_number.Direction = System.Data.ParameterDirection.Input;
                p_filter_trnrqs_number.DbType = DbType.String;
                parameters.Add(p_filter_trnrqs_number);
                //Type
                System.Data.IDataParameter p_filter_trnrqs_type_Fk = dataProvider.GetParameter("@filter_trnrqs_type_Fk",
                    f.Type == default(SOFTTEK.SCMS.Entity.Shared.Parameter<System.String>) ?
                        (object)System.DBNull.Value :
                        (object)f.Type.Identifier);
                p_filter_trnrqs_type_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_trnrqs_type_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_trnrqs_type_Fk);
                //Informed
                System.Data.IDataParameter p_filter_trnrqs_informed_Fk = dataProvider.GetParameter("@filter_trnrqs_informed_Fk",
                    f.Informed == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Informed.Identifier);
                p_filter_trnrqs_informed_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_trnrqs_informed_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_trnrqs_informed_Fk);
                //Responsible
                System.Data.IDataParameter p_filter_trnrqs_responsible_Fk = dataProvider.GetParameter("@filter_trnrqs_responsible_Fk",
                    f.Responsible == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Responsible.Identifier);
                p_filter_trnrqs_responsible_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_trnrqs_responsible_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_trnrqs_responsible_Fk);
                //Accountable
                System.Data.IDataParameter p_filter_trnrqs_accountable_Fk = dataProvider.GetParameter("@filter_trnrqs_accountable_Fk",
                    f.Accountable == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)f.Accountable.Identifier);
                p_filter_trnrqs_accountable_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_trnrqs_accountable_Fk.DbType = DbType.Int32;
                parameters.Add(p_filter_trnrqs_accountable_Fk);
                //Details
                System.Data.IDataParameter p_filter_trnrqs_details = dataProvider.GetParameter("@filter_trnrqs_details",
                    f.Details == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Details);
                p_filter_trnrqs_details.Direction = System.Data.ParameterDirection.Input;
                p_filter_trnrqs_details.DbType = DbType.String;
                parameters.Add(p_filter_trnrqs_details);
                //Comments
                System.Data.IDataParameter p_filter_trnrqs_comments = dataProvider.GetParameter("@filter_trnrqs_comments",
                    f.Comments == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Comments);
                p_filter_trnrqs_comments.Direction = System.Data.ParameterDirection.Input;
                p_filter_trnrqs_comments.DbType = DbType.String;
                parameters.Add(p_filter_trnrqs_comments);
                //Topic
                System.Data.IDataParameter p_filter_trnrqs_topic = dataProvider.GetParameter("@filter_trnrqs_topic",
                    f.Topic == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Topic);
                p_filter_trnrqs_topic.Direction = System.Data.ParameterDirection.Input;
                p_filter_trnrqs_topic.DbType = DbType.String;
                parameters.Add(p_filter_trnrqs_topic);
                //Status
                System.Data.IDataParameter p_filter_trnrqs_status = dataProvider.GetParameter("@filter_trnrqs_status",
                    f.Status == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Status);
                p_filter_trnrqs_status.Direction = System.Data.ParameterDirection.Input;
                p_filter_trnrqs_status.DbType = DbType.String;
                parameters.Add(p_filter_trnrqs_status);
                //UpdatedAt
                System.Data.IDataParameter p_filter_trnrqs_updated_at = dataProvider.GetParameter("@filter_trnrqs_updated_at",
                    f.UpdatedAt == default(System.DateTime) ?
                        (object)System.DBNull.Value :
                        (object)f.UpdatedAt);
                p_filter_trnrqs_updated_at.Direction = System.Data.ParameterDirection.Input;
                p_filter_trnrqs_updated_at.DbType = DbType.DateTime;
                parameters.Add(p_filter_trnrqs_updated_at);
                //RegisteredAt
                System.Data.IDataParameter p_filter_trnrqs_registered_at = dataProvider.GetParameter("@filter_trnrqs_registered_at",
                    f.RegisteredAt == default(System.DateTime) ?
                        (object)System.DBNull.Value :
                        (object)f.RegisteredAt);
                p_filter_trnrqs_registered_at.Direction = System.Data.ParameterDirection.Input;
                p_filter_trnrqs_registered_at.DbType = DbType.DateTime;
                parameters.Add(p_filter_trnrqs_registered_at);
                // Update
                //Origin
                System.Data.IDataParameter p_new_trnrqs_origin = dataProvider.GetParameter("@new_trnrqs_origin",
                    f.Origin == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Origin);
                p_new_trnrqs_origin.Direction = System.Data.ParameterDirection.Input;
                p_new_trnrqs_origin.DbType = DbType.String;
                parameters.Add(p_new_trnrqs_origin);
                //Destination
                System.Data.IDataParameter p_new_trnrqs_destination = dataProvider.GetParameter("@new_trnrqs_destination",
                    f.Destination == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Destination);
                p_new_trnrqs_destination.Direction = System.Data.ParameterDirection.Input;
                p_new_trnrqs_destination.DbType = DbType.String;
                parameters.Add(p_new_trnrqs_destination);
                //ExternalIdentifier
                System.Data.IDataParameter p_new_trnrqs_external_identifier = dataProvider.GetParameter("@new_trnrqs_external_identifier",
                    f.ExternalIdentifier == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.ExternalIdentifier);
                p_new_trnrqs_external_identifier.Direction = System.Data.ParameterDirection.Input;
                p_new_trnrqs_external_identifier.DbType = DbType.String;
                parameters.Add(p_new_trnrqs_external_identifier);
                //Novelty
                System.Data.IDataParameter p_new_trnrqs_novelty_Fk = dataProvider.GetParameter("@new_trnrqs_novelty_Fk",
                    instance.Novelty == default(SOFTTEK.SCMS.Entity.FA.NoveltyRequest) ?
                        (object)System.DBNull.Value :
                        (object)instance.Novelty.Identifier);
                p_new_trnrqs_novelty_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_trnrqs_novelty_Fk.DbType = DbType.Int64;
                parameters.Add(p_new_trnrqs_novelty_Fk);
                //Number
                System.Data.IDataParameter p_new_trnrqs_number = dataProvider.GetParameter("@new_trnrqs_number",
                    f.Number == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Number);
                p_new_trnrqs_number.Direction = System.Data.ParameterDirection.Input;
                p_new_trnrqs_number.DbType = DbType.String;
                parameters.Add(p_new_trnrqs_number);
                //Type
                System.Data.IDataParameter p_new_trnrqs_type_Fk = dataProvider.GetParameter("@new_trnrqs_type_Fk",
                    instance.Type == default(SOFTTEK.SCMS.Entity.Shared.Parameter<System.String>) ?
                        (object)System.DBNull.Value :
                        (object)instance.Type.Identifier);
                p_new_trnrqs_type_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_trnrqs_type_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_trnrqs_type_Fk);
                //Informed
                System.Data.IDataParameter p_new_trnrqs_informed_Fk = dataProvider.GetParameter("@new_trnrqs_informed_Fk",
                    instance.Informed == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)instance.Informed.Identifier);
                p_new_trnrqs_informed_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_trnrqs_informed_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_trnrqs_informed_Fk);
                //Responsible
                System.Data.IDataParameter p_new_trnrqs_responsible_Fk = dataProvider.GetParameter("@new_trnrqs_responsible_Fk",
                    instance.Responsible == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)instance.Responsible.Identifier);
                p_new_trnrqs_responsible_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_trnrqs_responsible_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_trnrqs_responsible_Fk);
                //Accountable
                System.Data.IDataParameter p_new_trnrqs_accountable_Fk = dataProvider.GetParameter("@new_trnrqs_accountable_Fk",
                    instance.Accountable == default(SOFTTEK.SCMS.Entity.Shared.Employee) ?
                        (object)System.DBNull.Value :
                        (object)instance.Accountable.Identifier);
                p_new_trnrqs_accountable_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_trnrqs_accountable_Fk.DbType = DbType.Int32;
                parameters.Add(p_new_trnrqs_accountable_Fk);
                //Details
                System.Data.IDataParameter p_new_trnrqs_details = dataProvider.GetParameter("@new_trnrqs_details",
                    f.Details == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Details);
                p_new_trnrqs_details.Direction = System.Data.ParameterDirection.Input;
                p_new_trnrqs_details.DbType = DbType.String;
                parameters.Add(p_new_trnrqs_details);
                //Comments
                System.Data.IDataParameter p_new_trnrqs_comments = dataProvider.GetParameter("@new_trnrqs_comments",
                    f.Comments == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Comments);
                p_new_trnrqs_comments.Direction = System.Data.ParameterDirection.Input;
                p_new_trnrqs_comments.DbType = DbType.String;
                parameters.Add(p_new_trnrqs_comments);
                //Topic
                System.Data.IDataParameter p_new_trnrqs_topic = dataProvider.GetParameter("@new_trnrqs_topic",
                    f.Topic == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Topic);
                p_new_trnrqs_topic.Direction = System.Data.ParameterDirection.Input;
                p_new_trnrqs_topic.DbType = DbType.String;
                parameters.Add(p_new_trnrqs_topic);
                //Status
                System.Data.IDataParameter p_new_trnrqs_status = dataProvider.GetParameter("@new_trnrqs_status",
                    f.Status == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Status);
                p_new_trnrqs_status.Direction = System.Data.ParameterDirection.Input;
                p_new_trnrqs_status.DbType = DbType.String;
                parameters.Add(p_new_trnrqs_status);

                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.FA.TransferRequest> mapper = (reader) =>
            {
                long longHelper = default(long);
                int intHelper = default(int);
                DateTime dateHelper = default(DateTime);

                SOFTTEK.SCMS.Entity.FA.TransferRequest value = new SOFTTEK.SCMS.Entity.FA.TransferRequest
                {
                    Origin = reader["v_trnrqs_origin"] != null ?
                        reader["v_trnrqs_origin"].ToString() : string.Empty,
                    Destination = reader["v_trnrqs_destination"] != null ?
                        reader["v_trnrqs_destination"].ToString() : string.Empty,
                    ExternalIdentifier = reader["v_trnrqs_external_identifier"] != null ?
                        reader["v_trnrqs_external_identifier"].ToString() : string.Empty,
                    Novelty = reader["v_trnrqs_novelty_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.FA.NoveltyRequest { Identifier = long.TryParse(reader["v_trnrqs_novelty_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null,
                    Identifier = reader["v_trnrqs_id_Pk"] != null ?
                        (long.TryParse(reader["v_trnrqs_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    Number = reader["v_trnrqs_number"] != null ?
                        reader["v_trnrqs_number"].ToString() : string.Empty,
                    Type = reader["v_trnrqs_type_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Parameter<System.String> { Identifier = int.TryParse(reader["v_trnrqs_type_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Informed = reader["v_trnrqs_informed_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_trnrqs_informed_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Responsible = reader["v_trnrqs_responsible_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_trnrqs_responsible_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Accountable = reader["v_trnrqs_accountable_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.Shared.Employee { Identifier = int.TryParse(reader["v_trnrqs_accountable_Fk"].ToString(), out intHelper) ? intHelper : -1 } : null,
                    Details = reader["v_trnrqs_details"] != null ?
                        reader["v_trnrqs_details"].ToString() : string.Empty,
                    Comments = reader["v_trnrqs_comments"] != null ?
                        reader["v_trnrqs_comments"].ToString() : string.Empty,
                    Topic = reader["v_trnrqs_topic"] != null ?
                        reader["v_trnrqs_topic"].ToString() : string.Empty,
                    Status = reader["v_trnrqs_status"] != null ?
                        reader["v_trnrqs_status"].ToString() : string.Empty,
                    UpdatedAt = reader["v_trnrqs_updated_at"] != null ?
                        (DateTime.TryParse(reader["v_trnrqs_updated_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                    RegisteredAt = reader["v_trnrqs_registered_at"] != null ?
                        (DateTime.TryParse(reader["v_trnrqs_registered_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                };
                return value;
            };


            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            SOFTTEK.SCMS.Entity.FA.TransferRequest result = dataProvider.ExecuteForEntityWithFilter(SCMS.FA_SP_Update_TransferRequest, filterDelegate, filter, mapper);
            return result;
        }
        #endregion
        #region WorkOrder

        /// <summary>
        /// Retrieves a list of WorkOrder instances that matches a given filter criteria by the WorkOrder object instance 
        /// </summary>
        /// <param name="filter">Object with the information required to filter the set of results</param>
        /// <returns>List of WorkOrder instances that matches the filter.</returns>
        public List<SOFTTEK.SCMS.Entity.FA.WorkOrder> GetWorkOrders(SOFTTEK.SCMS.Entity.FA.WorkOrder filter)
        {
            Func<SOFTTEK.SCMS.Entity.FA.WorkOrder, List<System.Data.IDataParameter>> filterDelegate = (f) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
                //Token
                System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p_tok.Direction = System.Data.ParameterDirection.Input;
                p_tok.DbType = System.Data.DbType.String;
                parameters.Add(p_tok);
                //Identifier
                System.Data.IDataParameter p_filter_wrkord_id_Pk = dataProvider.GetParameter("@filter_wrkord_id_Pk",
                    f.Identifier == default(System.Int64) ?
                        (object)System.DBNull.Value :
                        (object)f.Identifier);
                p_filter_wrkord_id_Pk.Direction = System.Data.ParameterDirection.Input;
                p_filter_wrkord_id_Pk.DbType = DbType.Int64;
                parameters.Add(p_filter_wrkord_id_Pk);
                //Number
                System.Data.IDataParameter p_filter_wrkord_number = dataProvider.GetParameter("@filter_wrkord_number",
                    f.Number == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Number);
                p_filter_wrkord_number.Direction = System.Data.ParameterDirection.Input;
                p_filter_wrkord_number.DbType = DbType.String;
                parameters.Add(p_filter_wrkord_number);
                //Status
                System.Data.IDataParameter p_filter_wrkord_status = dataProvider.GetParameter("@filter_wrkord_status",
                    f.Status == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Status);
                p_filter_wrkord_status.Direction = System.Data.ParameterDirection.Input;
                p_filter_wrkord_status.DbType = DbType.String;
                parameters.Add(p_filter_wrkord_status);
                //IssuedAt
                System.Data.IDataParameter p_filter_wrkord_issued_at = dataProvider.GetParameter("@filter_wrkord_issued_at",
                    f.IssuedAt == default(System.DateTime) ?
                        (object)System.DBNull.Value :
                        (object)f.IssuedAt);
                p_filter_wrkord_issued_at.Direction = System.Data.ParameterDirection.Input;
                p_filter_wrkord_issued_at.DbType = DbType.DateTime;
                parameters.Add(p_filter_wrkord_issued_at);
                //ScheduledTo
                System.Data.IDataParameter p_filter_wrkord_scheduled_to = dataProvider.GetParameter("@filter_wrkord_scheduled_to",
                    f.ScheduledTo == default(System.DateTime) ?
                        (object)System.DBNull.Value :
                        (object)f.ScheduledTo);
                p_filter_wrkord_scheduled_to.Direction = System.Data.ParameterDirection.Input;
                p_filter_wrkord_scheduled_to.DbType = DbType.DateTime;
                parameters.Add(p_filter_wrkord_scheduled_to);
                //Provider
                System.Data.IDataParameter p_filter_wrkord_provider_Fk = dataProvider.GetParameter("@filter_wrkord_provider_Fk",
                    f.Provider == default(SOFTTEK.SCMS.Entity.FA.Provider) ?
                        (object)System.DBNull.Value :
                        (object)f.Provider.Identifier);
                p_filter_wrkord_provider_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_wrkord_provider_Fk.DbType = DbType.Int64;
                parameters.Add(p_filter_wrkord_provider_Fk);
                //PhysicalInventoryTaking
                System.Data.IDataParameter p_filter_wrkord_physical_inventory_taking_Fk = dataProvider.GetParameter("@filter_wrkord_physical_inventory_taking_Fk",
                    f.PhysicalInventoryTaking == default(SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking) ?
                        (object)System.DBNull.Value :
                        (object)f.PhysicalInventoryTaking.Identifier);
                p_filter_wrkord_physical_inventory_taking_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_wrkord_physical_inventory_taking_Fk.DbType = DbType.Int64;
                parameters.Add(p_filter_wrkord_physical_inventory_taking_Fk);
                //Description
                System.Data.IDataParameter p_filter_wrkord_description = dataProvider.GetParameter("@filter_wrkord_description",
                    f.Description == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Description);
                p_filter_wrkord_description.Direction = System.Data.ParameterDirection.Input;
                p_filter_wrkord_description.DbType = DbType.String;
                parameters.Add(p_filter_wrkord_description);
                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.FA.WorkOrder> mapper = (reader) =>
            {
                long longHelper = default(long);
                DateTime dateHelper = default(DateTime);

                SOFTTEK.SCMS.Entity.FA.WorkOrder value = new SOFTTEK.SCMS.Entity.FA.WorkOrder
                {
                    Identifier = reader["wrkord_id_Pk"] != null ?
                        (long.TryParse(reader["wrkord_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    Number = reader["wrkord_number"] != null ?
                        reader["wrkord_number"].ToString() : string.Empty,
                    Status = reader["wrkord_status"] != null ?
                        reader["wrkord_status"].ToString() : string.Empty,
                    IssuedAt = reader["wrkord_issued_at"] != null ?
                        (DateTime.TryParse(reader["wrkord_issued_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                    ScheduledTo = reader["wrkord_scheduled_to"] != null ?
                        (DateTime.TryParse(reader["wrkord_scheduled_to"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                    Provider = reader["wrkord_provider_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.FA.Provider { Identifier = long.TryParse(reader["wrkord_provider_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null,
                    PhysicalInventoryTaking = reader["wrkord_physical_inventory_taking_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking { Identifier = long.TryParse(reader["wrkord_physical_inventory_taking_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null,
                    Description = reader["wrkord_description"] != null ?
                        reader["wrkord_description"].ToString() : string.Empty,
                };
                return value;
            };

            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            List<SOFTTEK.SCMS.Entity.FA.WorkOrder> result = dataProvider.ExecuteReaderWithFilter(SCMS.FA_SP_Select_WorkOrder, filterDelegate, filter, mapper);
            return result;
        }

        /// <summary>
        /// Inserts the instance of the provided WorkOrder at the Data Context datasource  
        /// </summary>
        /// <param name="instance">Instance of WorkOrder with the entity´s information to store</param>
        /// <returns>Instance of WorkOrder with the aditional information provided by the Data Context datasource</returns>
        public SOFTTEK.SCMS.Entity.FA.WorkOrder InsertWorkOrder(SOFTTEK.SCMS.Entity.FA.WorkOrder instance)
        {
            Func<SOFTTEK.SCMS.Entity.FA.WorkOrder, List<System.Data.IDataParameter>> input = (i) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();

                //Token
                System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p_tok.Direction = System.Data.ParameterDirection.Input;
                p_tok.DbType = System.Data.DbType.String;
                parameters.Add(p_tok);

                // Insert
                //Number
                System.Data.IDataParameter p_new_wrkord_number = dataProvider.GetParameter("@new_wrkord_number",
                    i.Number == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Number);
                p_new_wrkord_number.Direction = System.Data.ParameterDirection.Input;
                p_new_wrkord_number.DbType = DbType.String;
                parameters.Add(p_new_wrkord_number);
                //Status
                System.Data.IDataParameter p_new_wrkord_status = dataProvider.GetParameter("@new_wrkord_status",
                    i.Status == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Status);
                p_new_wrkord_status.Direction = System.Data.ParameterDirection.Input;
                p_new_wrkord_status.DbType = DbType.String;
                parameters.Add(p_new_wrkord_status);
                //ScheduledTo
                System.Data.IDataParameter p_new_wrkord_scheduled_to = dataProvider.GetParameter("@new_wrkord_scheduled_to",
                    i.ScheduledTo == default(System.DateTime) ?
                        (object)System.DBNull.Value :
                        (object)i.ScheduledTo);
                p_new_wrkord_scheduled_to.Direction = System.Data.ParameterDirection.Input;
                p_new_wrkord_scheduled_to.DbType = DbType.DateTime;
                parameters.Add(p_new_wrkord_scheduled_to);
                //Provider
                System.Data.IDataParameter p_new_wrkord_provider_Fk = dataProvider.GetParameter("@new_wrkord_provider_Fk",
                    i.Provider == default(SOFTTEK.SCMS.Entity.FA.Provider) ?
                        (object)System.DBNull.Value :
                        (object)i.Provider.Identifier);
                p_new_wrkord_provider_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_wrkord_provider_Fk.DbType = DbType.Int64;
                parameters.Add(p_new_wrkord_provider_Fk);
                //PhysicalInventoryTaking
                System.Data.IDataParameter p_new_wrkord_physical_inventory_taking_Fk = dataProvider.GetParameter("@new_wrkord_physical_inventory_taking_Fk",
                    i.PhysicalInventoryTaking == default(SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking) ?
                        (object)System.DBNull.Value :
                        (object)i.PhysicalInventoryTaking.Identifier);
                p_new_wrkord_physical_inventory_taking_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_wrkord_physical_inventory_taking_Fk.DbType = DbType.Int64;
                parameters.Add(p_new_wrkord_physical_inventory_taking_Fk);
                //Description
                System.Data.IDataParameter p_new_wrkord_description = dataProvider.GetParameter("@new_wrkord_description",
                    i.Description == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)i.Description);
                p_new_wrkord_description.Direction = System.Data.ParameterDirection.Input;
                p_new_wrkord_description.DbType = DbType.String;
                parameters.Add(p_new_wrkord_description);

                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.FA.WorkOrder> mapper = (reader) =>
            {
                long longHelper = default(long);
                DateTime dateHelper = default(DateTime);

                SOFTTEK.SCMS.Entity.FA.WorkOrder value = new SOFTTEK.SCMS.Entity.FA.WorkOrder
                {
                    Identifier = reader["v_wrkord_id_Pk"] != null ?
                        (long.TryParse(reader["v_wrkord_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    Number = reader["v_wrkord_number"] != null ?
                        reader["v_wrkord_number"].ToString() : string.Empty,
                    Status = reader["v_wrkord_status"] != null ?
                        reader["v_wrkord_status"].ToString() : string.Empty,
                    IssuedAt = reader["v_wrkord_issued_at"] != null ?
                        (DateTime.TryParse(reader["v_wrkord_issued_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                    ScheduledTo = reader["v_wrkord_scheduled_to"] != null ?
                        (DateTime.TryParse(reader["v_wrkord_scheduled_to"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                    Provider = reader["v_wrkord_provider_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.FA.Provider { Identifier = long.TryParse(reader["v_wrkord_provider_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null,
                    PhysicalInventoryTaking = reader["v_wrkord_physical_inventory_taking_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking { Identifier = long.TryParse(reader["v_wrkord_physical_inventory_taking_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null,
                    Description = reader["v_wrkord_description"] != null ?
                        reader["v_wrkord_description"].ToString() : string.Empty,
                };
                return value;
            };


            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            SOFTTEK.SCMS.Entity.FA.WorkOrder result = dataProvider.ExecuteForEntityWithFilter(SCMS.FA_SP_Insert_WorkOrder, input, instance, mapper);
            return result;
        }

        /// <summary>
        /// Update the entity's stored information with that provided by the instance of WorkOrder at the Data Context datasource  
        /// </summary>
        /// <param name="instance">Instance of WorkOrder with the entity´s information to update</param>
        /// <param name="filter">Object with the information required to filter the target stored entity´s information</param>
        /// <returns>Instance of WorkOrder with the updated information provided by the Data Context datasource</returns>
        public SOFTTEK.SCMS.Entity.FA.WorkOrder UpdateWorkOrder(SOFTTEK.SCMS.Entity.FA.WorkOrder instance, SOFTTEK.SCMS.Entity.FA.WorkOrder filter)
        {
            Func<SOFTTEK.SCMS.Entity.FA.WorkOrder, List<System.Data.IDataParameter>> filterDelegate = (f) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();

                //Token
                System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p_tok.Direction = System.Data.ParameterDirection.Input;
                p_tok.DbType = System.Data.DbType.String;
                parameters.Add(p_tok);

                // Filter
                //Identifier
                System.Data.IDataParameter p_filter_wrkord_id_Pk = dataProvider.GetParameter("@filter_wrkord_id_Pk",
                    f.Identifier == default(System.Int64) ?
                        (object)System.DBNull.Value :
                        (object)f.Identifier);
                p_filter_wrkord_id_Pk.Direction = System.Data.ParameterDirection.Input;
                p_filter_wrkord_id_Pk.DbType = DbType.Int64;
                parameters.Add(p_filter_wrkord_id_Pk);
                //Number
                System.Data.IDataParameter p_filter_wrkord_number = dataProvider.GetParameter("@filter_wrkord_number",
                    f.Number == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Number);
                p_filter_wrkord_number.Direction = System.Data.ParameterDirection.Input;
                p_filter_wrkord_number.DbType = DbType.String;
                parameters.Add(p_filter_wrkord_number);
                //Status
                System.Data.IDataParameter p_filter_wrkord_status = dataProvider.GetParameter("@filter_wrkord_status",
                    f.Status == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Status);
                p_filter_wrkord_status.Direction = System.Data.ParameterDirection.Input;
                p_filter_wrkord_status.DbType = DbType.String;
                parameters.Add(p_filter_wrkord_status);
                //IssuedAt
                System.Data.IDataParameter p_filter_wrkord_issued_at = dataProvider.GetParameter("@filter_wrkord_issued_at",
                    f.IssuedAt == default(System.DateTime) ?
                        (object)System.DBNull.Value :
                        (object)f.IssuedAt);
                p_filter_wrkord_issued_at.Direction = System.Data.ParameterDirection.Input;
                p_filter_wrkord_issued_at.DbType = DbType.DateTime;
                parameters.Add(p_filter_wrkord_issued_at);
                //ScheduledTo
                System.Data.IDataParameter p_filter_wrkord_scheduled_to = dataProvider.GetParameter("@filter_wrkord_scheduled_to",
                    f.ScheduledTo == default(System.DateTime) ?
                        (object)System.DBNull.Value :
                        (object)f.ScheduledTo);
                p_filter_wrkord_scheduled_to.Direction = System.Data.ParameterDirection.Input;
                p_filter_wrkord_scheduled_to.DbType = DbType.DateTime;
                parameters.Add(p_filter_wrkord_scheduled_to);
                //Provider
                System.Data.IDataParameter p_filter_wrkord_provider_Fk = dataProvider.GetParameter("@filter_wrkord_provider_Fk",
                    f.Provider == default(SOFTTEK.SCMS.Entity.FA.Provider) ?
                        (object)System.DBNull.Value :
                        (object)f.Provider.Identifier);
                p_filter_wrkord_provider_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_wrkord_provider_Fk.DbType = DbType.Int64;
                parameters.Add(p_filter_wrkord_provider_Fk);
                //PhysicalInventoryTaking
                System.Data.IDataParameter p_filter_wrkord_physical_inventory_taking_Fk = dataProvider.GetParameter("@filter_wrkord_physical_inventory_taking_Fk",
                    f.PhysicalInventoryTaking == default(SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking) ?
                        (object)System.DBNull.Value :
                        (object)f.PhysicalInventoryTaking.Identifier);
                p_filter_wrkord_physical_inventory_taking_Fk.Direction = System.Data.ParameterDirection.Input;
                p_filter_wrkord_physical_inventory_taking_Fk.DbType = DbType.Int64;
                parameters.Add(p_filter_wrkord_physical_inventory_taking_Fk);
                //Description
                System.Data.IDataParameter p_filter_wrkord_description = dataProvider.GetParameter("@filter_wrkord_description",
                    f.Description == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)f.Description);
                p_filter_wrkord_description.Direction = System.Data.ParameterDirection.Input;
                p_filter_wrkord_description.DbType = DbType.String;
                parameters.Add(p_filter_wrkord_description);
                // Update
                //Number
                System.Data.IDataParameter p_new_wrkord_number = dataProvider.GetParameter("@new_wrkord_number",
                    f.Number == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Number);
                p_new_wrkord_number.Direction = System.Data.ParameterDirection.Input;
                p_new_wrkord_number.DbType = DbType.String;
                parameters.Add(p_new_wrkord_number);
                //Status
                System.Data.IDataParameter p_new_wrkord_status = dataProvider.GetParameter("@new_wrkord_status",
                    f.Status == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Status);
                p_new_wrkord_status.Direction = System.Data.ParameterDirection.Input;
                p_new_wrkord_status.DbType = DbType.String;
                parameters.Add(p_new_wrkord_status);
                //ScheduledTo
                System.Data.IDataParameter p_new_wrkord_scheduled_to = dataProvider.GetParameter("@new_wrkord_scheduled_to",
                    f.ScheduledTo == default(System.DateTime) ?
                        (object)System.DBNull.Value :
                        (object)instance.ScheduledTo);
                p_new_wrkord_scheduled_to.Direction = System.Data.ParameterDirection.Input;
                p_new_wrkord_scheduled_to.DbType = DbType.DateTime;
                parameters.Add(p_new_wrkord_scheduled_to);
                //Provider
                System.Data.IDataParameter p_new_wrkord_provider_Fk = dataProvider.GetParameter("@new_wrkord_provider_Fk",
                    instance.Provider == default(SOFTTEK.SCMS.Entity.FA.Provider) ?
                        (object)System.DBNull.Value :
                        (object)instance.Provider.Identifier);
                p_new_wrkord_provider_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_wrkord_provider_Fk.DbType = DbType.Int64;
                parameters.Add(p_new_wrkord_provider_Fk);
                //PhysicalInventoryTaking
                System.Data.IDataParameter p_new_wrkord_physical_inventory_taking_Fk = dataProvider.GetParameter("@new_wrkord_physical_inventory_taking_Fk",
                    instance.PhysicalInventoryTaking == default(SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking) ?
                        (object)System.DBNull.Value :
                        (object)instance.PhysicalInventoryTaking.Identifier);
                p_new_wrkord_physical_inventory_taking_Fk.Direction = System.Data.ParameterDirection.Input;
                p_new_wrkord_physical_inventory_taking_Fk.DbType = DbType.Int64;
                parameters.Add(p_new_wrkord_physical_inventory_taking_Fk);
                //Description
                System.Data.IDataParameter p_new_wrkord_description = dataProvider.GetParameter("@new_wrkord_description",
                    f.Description == default(System.String) ?
                        (object)System.DBNull.Value :
                        (object)instance.Description);
                p_new_wrkord_description.Direction = System.Data.ParameterDirection.Input;
                p_new_wrkord_description.DbType = DbType.String;
                parameters.Add(p_new_wrkord_description);

                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.FA.WorkOrder> mapper = (reader) =>
            {
                long longHelper = default(long);
                DateTime dateHelper = default(DateTime);

                SOFTTEK.SCMS.Entity.FA.WorkOrder value = new SOFTTEK.SCMS.Entity.FA.WorkOrder
                {
                    Identifier = reader["v_wrkord_id_Pk"] != null ?
                        (long.TryParse(reader["v_wrkord_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    Number = reader["v_wrkord_number"] != null ?
                        reader["v_wrkord_number"].ToString() : string.Empty,
                    Status = reader["v_wrkord_status"] != null ?
                        reader["v_wrkord_status"].ToString() : string.Empty,
                    IssuedAt = reader["v_wrkord_issued_at"] != null ?
                        (DateTime.TryParse(reader["v_wrkord_issued_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                    ScheduledTo = reader["v_wrkord_scheduled_to"] != null ?
                        (DateTime.TryParse(reader["v_wrkord_scheduled_to"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
                    Provider = reader["v_wrkord_provider_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.FA.Provider { Identifier = long.TryParse(reader["v_wrkord_provider_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null,
                    PhysicalInventoryTaking = reader["v_wrkord_physical_inventory_taking_Fk"] != null ?
                        new SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking { Identifier = long.TryParse(reader["v_wrkord_physical_inventory_taking_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null,
                    Description = reader["v_wrkord_description"] != null ?
                        reader["v_wrkord_description"].ToString() : string.Empty,
                };
                return value;
            };


            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            SOFTTEK.SCMS.Entity.FA.WorkOrder result = dataProvider.ExecuteForEntityWithFilter(SCMS.FA_SP_Update_WorkOrder, filterDelegate, filter, mapper);
            return result;
        }
        #endregion

        #endregion
    }
}


