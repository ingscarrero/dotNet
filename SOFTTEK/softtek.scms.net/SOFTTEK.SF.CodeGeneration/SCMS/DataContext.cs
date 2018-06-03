 
 
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SCMS.Foundation.Security;
using SOFTTEK.SCMS.Foundation.Data;
using SOFTTEK.SCMS.Entity;

namespace SOFTTEK.SCMS.Data {
	//public class PMDataContext: DataContext, IDisposable{
		//#region Fields
		///// <summary>
		///// Get or Set the instance of the Data Context Data Provider
  //      /// </summary>
		//private SOFTTEK.SCMS.Foundation.Data.DataProvider dataProvider;
		//#endregion

		//#region Propierties
		///// <summary>
		///// Get or Set the string that represents the connection parameters for the Data Context Instance
  //      /// </summary>
		//public string ConnectionString { get; set; }
		
		///// <summary>
		///// Get or Set the Provider Identifier of the Data Provider to instance at the initialization of the Data Context
  //      /// </summary>
		//public string ProviderName { get; private set; }

		///// <summary>
  //      /// Get or Set The Default DB user for the Data Provider
  //      /// </summary>
  //      public string DefaultUser { private get; set; }
		//#endregion

  //      #region Configuration
		///// <summary>
		///// Default Constructor for the instance of the Data Context
		///// <param name="securityContext">Security Context instance to be used by the Data Context instance</param>
  //      /// </summary>
		//public PMDataContext(SecurityContext securityContext)
  //         : base(securityContext)
  //      {

  //      }

		///// <summary>
		///// Perform the initialization routine for the Data Context Instance, creating an instance for the Data Provider.
  //      /// </summary>
		//public void Initialize()
  //      {
  //          if (string.IsNullOrEmpty(ConnectionString))
  //          {
  //              throw new Exception("ConnectionString is required.", new ArgumentException("ConnectionString cannot be null."));
  //          }
  //          ProviderName = System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionString].ProviderName;
  //          switch (ProviderName)
  //          {
  //              case "System.Data.SqlClient":
  //                  dataProvider = new SOFTTEK.SCMS.Foundation.Data.DataProviders.MSSQLDataProvider();
  //                  dataProvider.ConnectionString = ConnectionString;
		//			dataProvider.Context = this;
  //                  break;
  //              case "System.Data.OracleClient":
  //                  break;
  //              default:
  //                  break;
  //          }
  //      }
		///// <summary>
		///// Release the instance´s resources
		///// </summary>
  //      public void Dispose()
  //      {
  //          dataProvider.Dispose();
  //          Dispose(false);
  //      }

		///// <summary>
		///// Implementation of the Dispose Pattern for the disposable instance of the Data Context by releasing all its disposable resources.
  //      /// <param name="disposing">Flag that indicates the current disposing status for the Data Context instance</param>
		///// </summary>
		//protected virtual void Dispose(bool disposing)
  //      {
  //          if (disposing)
  //          {
  //              Dispose();
  //          }
  //          dataProvider = null;
  //      }
  //      #endregion

		//#region Accessors

		//#region Activity
		
		///// <summary>
  //      /// Retrieves a list of Activity instances that matches a given filter criteria by the Activity object instance 
  //      /// </summary>
  //      /// <param name="filter">Object with the information required to filter the set of results</param>
  //      /// <returns>List of Activity instances that matches the filter.</returns>
		//public List<SOFTTEK.SCMS.Entity.PM.Activity> GetActivitys(SOFTTEK.SCMS.Entity.PM.Activity filter)
		//{
		//	Func<SOFTTEK.SCMS.Entity.PM.Activity, List<System.Data.IDataParameter>> filterDelegate = (f) =>
  //          {
  //              List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
		//		//Token
		//		System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
  //              p_tok.Direction = System.Data.ParameterDirection.Input;
  //              p_tok.DbType = System.Data.DbType.String;
  //              parameters.Add(p_tok);
		//		//Identifier
		//		System.Data.IDataParameter p_filter_act_id_Pk = dataProvider.GetParameter("@filter_act_id_Pk", 
		//			f.Identifier == default(System.Int64) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Identifier);
		//		p_filter_act_id_Pk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_act_id_Pk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_act_id_Pk);
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_filter_act_external_identifier = dataProvider.GetParameter("@filter_act_external_identifier", 
		//			f.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.ExternalIdentifier);
		//		p_filter_act_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_act_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_filter_act_external_identifier);
		//		//Name
		//		System.Data.IDataParameter p_filter_act_name = dataProvider.GetParameter("@filter_act_name", 
		//			f.Name == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Name);
		//		p_filter_act_name.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_act_name.DbType = DbType.String;
		//		parameters.Add(p_filter_act_name);
		//		//Description
		//		System.Data.IDataParameter p_filter_act_description = dataProvider.GetParameter("@filter_act_description", 
		//			f.Description == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Description);
		//		p_filter_act_description.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_act_description.DbType = DbType.String;
		//		parameters.Add(p_filter_act_description);
		//		//TotalDuration
		//		System.Data.IDataParameter p_filter_act_total_duration = dataProvider.GetParameter("@filter_act_total_duration", 
		//			f.TotalDuration == default(System.Int32) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.TotalDuration);
		//		p_filter_act_total_duration.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_act_total_duration.DbType = DbType.Int32;
		//		parameters.Add(p_filter_act_total_duration);
		//		//Status
		//		System.Data.IDataParameter p_filter_act_status = dataProvider.GetParameter("@filter_act_status", 
		//			f.Status == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Status);
		//		p_filter_act_status.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_act_status.DbType = DbType.String;
		//		parameters.Add(p_filter_act_status);
		//		//Comments
		//		System.Data.IDataParameter p_filter_act_comments = dataProvider.GetParameter("@filter_act_comments", 
		//			f.Comments == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Comments);
		//		p_filter_act_comments.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_act_comments.DbType = DbType.String;
		//		parameters.Add(p_filter_act_comments);
		//		//MaintenancePlan
		//		System.Data.IDataParameter p_filter_act_maintenance_plan = dataProvider.GetParameter("@filter_act_maintenance_plan", 
		//			f.MaintenancePlan == default(System.Int64) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.MaintenancePlan);
		//		p_filter_act_maintenance_plan.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_act_maintenance_plan.DbType = DbType.Int64;
		//		parameters.Add(p_filter_act_maintenance_plan);
  //              return parameters;
  //          };

  //          Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.PM.Activity> mapper = (reader) =>
  //          {
		//		long longHelper = default(long);
		//		int intHelper = default(int);
		//		DateTime dateHelper = default(DateTime);

		//		SOFTTEK.SCMS.Entity.PM.Activity value = new SOFTTEK.SCMS.Entity.PM.Activity{
		//			Identifier = reader["act_id_Pk"] != null ? 
		//				(long.TryParse(reader["act_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
		//			ExternalIdentifier = reader["act_external_identifier"] != null ? 
		//				reader["act_external_identifier"].ToString() : string.Empty,
		//			Name = reader["act_name"] != null ? 
		//				reader["act_name"].ToString() : string.Empty,
		//			Description = reader["act_description"] != null ? 
		//				reader["act_description"].ToString() : string.Empty,
		//			ExecutionStartAt = reader["act_execution_start_at"] != null ? 
		//				(DateTime.TryParse(reader["act_execution_start_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
		//			ExecutionFinishedAt = reader["act_execution_finished_at"] != null ? 
		//				(DateTime.TryParse(reader["act_execution_finished_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
		//			TotalDuration = reader["act_total_duration"] != null ? 
		//				(int.TryParse(reader["act_total_duration"].ToString(), out intHelper) ? intHelper : -1) : -1,
		//			Status = reader["act_status"] != null ? 
		//				reader["act_status"].ToString() : string.Empty,
		//			Comments = reader["act_comments"] != null ? 
		//				reader["act_comments"].ToString() : string.Empty,
		//			MaintenancePlan = reader["act_maintenance_plan"] != null ? 
		//				(long.TryParse(reader["act_maintenance_plan"].ToString(), out longHelper) ? longHelper : -1) : -1,
		//		};
  //              return value;
  //          };

  //          dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
  //          List<SOFTTEK.SCMS.Entity.PM.Activity> result = dataProvider.ExecuteReaderWithFilter(SCMS.PM_SP_Select_Activity, filterDelegate, filter, mapper);
  //          return result;
		//}

		///// <summary>
  //      /// Inserts the instance of the provided Activity at the Data Context datasource  
  //      /// </summary>
  //      /// <param name="instance">Instance of Activity with the entity´s information to store</param>
  //      /// <returns>Instance of Activity with the aditional information provided by the Data Context datasource</returns>
		//public SOFTTEK.SCMS.Entity.PM.Activity InsertActivity(SOFTTEK.SCMS.Entity.PM.Activity instance){
		//	Func<SOFTTEK.SCMS.Entity.PM.Activity, List<System.Data.IDataParameter>> input = (i) =>
  //          {
  //              List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
			
		//		//Token
		//		System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
  //              p_tok.Direction = System.Data.ParameterDirection.Input;
  //              p_tok.DbType = System.Data.DbType.String;
  //              parameters.Add(p_tok);
				
		//		// Insert
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_new_act_external_identifier = dataProvider.GetParameter("@new_act_external_identifier", 
		//			i.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.ExternalIdentifier);
		//		p_new_act_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_new_act_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_new_act_external_identifier);
		//		//Name
		//		System.Data.IDataParameter p_new_act_name = dataProvider.GetParameter("@new_act_name", 
		//			i.Name == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.Name);
		//		p_new_act_name.Direction = System.Data.ParameterDirection.Input;
		//		p_new_act_name.DbType = DbType.String;
		//		parameters.Add(p_new_act_name);
		//		//Description
		//		System.Data.IDataParameter p_new_act_description = dataProvider.GetParameter("@new_act_description", 
		//			i.Description == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.Description);
		//		p_new_act_description.Direction = System.Data.ParameterDirection.Input;
		//		p_new_act_description.DbType = DbType.String;
		//		parameters.Add(p_new_act_description);
		//		//ExecutionStartAt
		//		System.Data.IDataParameter p_new_act_execution_start_at = dataProvider.GetParameter("@new_act_execution_start_at", 
		//			i.ExecutionStartAt == default(System.DateTime) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.ExecutionStartAt);
		//		p_new_act_execution_start_at.Direction = System.Data.ParameterDirection.Input;
		//		p_new_act_execution_start_at.DbType = DbType.DateTime;
		//		parameters.Add(p_new_act_execution_start_at);
		//		//ExecutionFinishedAt
		//		System.Data.IDataParameter p_new_act_execution_finished_at = dataProvider.GetParameter("@new_act_execution_finished_at", 
		//			i.ExecutionFinishedAt == default(System.DateTime) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.ExecutionFinishedAt);
		//		p_new_act_execution_finished_at.Direction = System.Data.ParameterDirection.Input;
		//		p_new_act_execution_finished_at.DbType = DbType.DateTime;
		//		parameters.Add(p_new_act_execution_finished_at);
		//		//TotalDuration
		//		System.Data.IDataParameter p_new_act_total_duration = dataProvider.GetParameter("@new_act_total_duration", 
		//			i.TotalDuration == default(System.Int32) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.TotalDuration);
		//		p_new_act_total_duration.Direction = System.Data.ParameterDirection.Input;
		//		p_new_act_total_duration.DbType = DbType.Int32;
		//		parameters.Add(p_new_act_total_duration);
		//		//Status
		//		System.Data.IDataParameter p_new_act_status = dataProvider.GetParameter("@new_act_status", 
		//			i.Status == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.Status);
		//		p_new_act_status.Direction = System.Data.ParameterDirection.Input;
		//		p_new_act_status.DbType = DbType.String;
		//		parameters.Add(p_new_act_status);
		//		//Comments
		//		System.Data.IDataParameter p_new_act_comments = dataProvider.GetParameter("@new_act_comments", 
		//			i.Comments == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.Comments);
		//		p_new_act_comments.Direction = System.Data.ParameterDirection.Input;
		//		p_new_act_comments.DbType = DbType.String;
		//		parameters.Add(p_new_act_comments);
		//		//MaintenancePlan
		//		System.Data.IDataParameter p_new_act_maintenance_plan = dataProvider.GetParameter("@new_act_maintenance_plan", 
		//			i.MaintenancePlan == default(System.Int64) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.MaintenancePlan);
		//		p_new_act_maintenance_plan.Direction = System.Data.ParameterDirection.Input;
		//		p_new_act_maintenance_plan.DbType = DbType.Int64;
		//		parameters.Add(p_new_act_maintenance_plan);

  //              return parameters;
  //          };

		//	Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.PM.Activity> mapper = (reader) =>
  //          {
		//		long longHelper = default(long);
		//		int intHelper = default(int);
		//		DateTime dateHelper = default(DateTime);

		//		SOFTTEK.SCMS.Entity.PM.Activity value = new SOFTTEK.SCMS.Entity.PM.Activity{
		//			Identifier = reader["v_act_id_Pk"] != null ? 
		//				(long.TryParse(reader["v_act_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
		//			ExternalIdentifier = reader["v_act_external_identifier"] != null ? 
		//				reader["v_act_external_identifier"].ToString() : string.Empty,
		//			Name = reader["v_act_name"] != null ? 
		//				reader["v_act_name"].ToString() : string.Empty,
		//			Description = reader["v_act_description"] != null ? 
		//				reader["v_act_description"].ToString() : string.Empty,
		//			ExecutionStartAt = reader["v_act_execution_start_at"] != null ? 
		//				(DateTime.TryParse(reader["v_act_execution_start_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
		//			ExecutionFinishedAt = reader["v_act_execution_finished_at"] != null ? 
		//				(DateTime.TryParse(reader["v_act_execution_finished_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
		//			TotalDuration = reader["v_act_total_duration"] != null ? 
		//				(int.TryParse(reader["v_act_total_duration"].ToString(), out intHelper) ? intHelper : -1) : -1,
		//			Status = reader["v_act_status"] != null ? 
		//				reader["v_act_status"].ToString() : string.Empty,
		//			Comments = reader["v_act_comments"] != null ? 
		//				reader["v_act_comments"].ToString() : string.Empty,
		//			MaintenancePlan = reader["v_act_maintenance_plan"] != null ? 
		//				(long.TryParse(reader["v_act_maintenance_plan"].ToString(), out longHelper) ? longHelper : -1) : -1,
		//		};
  //              return value;
  //          };


		//	dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
  //          SOFTTEK.SCMS.Entity.PM.Activity result = dataProvider.ExecuteForEntityWithFilter(SCMS.PM_SP_Insert_Activity, input, instance, mapper);
  //          return result;
		//}

		///// <summary>
  //      /// Update the entity's stored information with that provided by the instance of Activity at the Data Context datasource  
  //      /// </summary>
  //      /// <param name="instance">Instance of Activity with the entity´s information to update</param>
  //      /// <param name="filter">Object with the information required to filter the target stored entity´s information</param>
		///// <returns>Instance of Activity with the updated information provided by the Data Context datasource</returns>
		//public SOFTTEK.SCMS.Entity.PM.Activity UpdateActivity(SOFTTEK.SCMS.Entity.PM.Activity instance, SOFTTEK.SCMS.Entity.PM.Activity filter){
		//	Func<SOFTTEK.SCMS.Entity.PM.Activity, List<System.Data.IDataParameter>> filterDelegate = (f) =>
  //          {
  //              List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
			
		//		//Token
		//		System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
  //              p_tok.Direction = System.Data.ParameterDirection.Input;
  //              p_tok.DbType = System.Data.DbType.String;
  //              parameters.Add(p_tok);
				
		//		// Filter
		//		//Identifier
		//		System.Data.IDataParameter p_filter_act_id_Pk = dataProvider.GetParameter("@filter_act_id_Pk", 
		//			f.Identifier == default(System.Int64) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Identifier);
		//		p_filter_act_id_Pk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_act_id_Pk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_act_id_Pk);
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_filter_act_external_identifier = dataProvider.GetParameter("@filter_act_external_identifier", 
		//			f.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.ExternalIdentifier);
		//		p_filter_act_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_act_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_filter_act_external_identifier);
		//		//Name
		//		System.Data.IDataParameter p_filter_act_name = dataProvider.GetParameter("@filter_act_name", 
		//			f.Name == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Name);
		//		p_filter_act_name.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_act_name.DbType = DbType.String;
		//		parameters.Add(p_filter_act_name);
		//		//Description
		//		System.Data.IDataParameter p_filter_act_description = dataProvider.GetParameter("@filter_act_description", 
		//			f.Description == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Description);
		//		p_filter_act_description.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_act_description.DbType = DbType.String;
		//		parameters.Add(p_filter_act_description);
		//		//TotalDuration
		//		System.Data.IDataParameter p_filter_act_total_duration = dataProvider.GetParameter("@filter_act_total_duration", 
		//			f.TotalDuration == default(System.Int32) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.TotalDuration);
		//		p_filter_act_total_duration.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_act_total_duration.DbType = DbType.Int32;
		//		parameters.Add(p_filter_act_total_duration);
		//		//Status
		//		System.Data.IDataParameter p_filter_act_status = dataProvider.GetParameter("@filter_act_status", 
		//			f.Status == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Status);
		//		p_filter_act_status.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_act_status.DbType = DbType.String;
		//		parameters.Add(p_filter_act_status);
		//		//Comments
		//		System.Data.IDataParameter p_filter_act_comments = dataProvider.GetParameter("@filter_act_comments", 
		//			f.Comments == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Comments);
		//		p_filter_act_comments.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_act_comments.DbType = DbType.String;
		//		parameters.Add(p_filter_act_comments);
		//		//MaintenancePlan
		//		System.Data.IDataParameter p_filter_act_maintenance_plan = dataProvider.GetParameter("@filter_act_maintenance_plan", 
		//			f.MaintenancePlan == default(System.Int64) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.MaintenancePlan);
		//		p_filter_act_maintenance_plan.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_act_maintenance_plan.DbType = DbType.Int64;
		//		parameters.Add(p_filter_act_maintenance_plan);
		//		// Update
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_new_act_external_identifier = dataProvider.GetParameter("@new_act_external_identifier", 
		//			f.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.ExternalIdentifier);
		//		p_new_act_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_new_act_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_new_act_external_identifier);
		//		//Name
		//		System.Data.IDataParameter p_new_act_name = dataProvider.GetParameter("@new_act_name", 
		//			f.Name == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.Name);
		//		p_new_act_name.Direction = System.Data.ParameterDirection.Input;
		//		p_new_act_name.DbType = DbType.String;
		//		parameters.Add(p_new_act_name);
		//		//Description
		//		System.Data.IDataParameter p_new_act_description = dataProvider.GetParameter("@new_act_description", 
		//			f.Description == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.Description);
		//		p_new_act_description.Direction = System.Data.ParameterDirection.Input;
		//		p_new_act_description.DbType = DbType.String;
		//		parameters.Add(p_new_act_description);
		//		//ExecutionStartAt
		//		System.Data.IDataParameter p_new_act_execution_start_at = dataProvider.GetParameter("@new_act_execution_start_at", 
		//			f.ExecutionStartAt == default(System.DateTime) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.ExecutionStartAt);
		//		p_new_act_execution_start_at.Direction = System.Data.ParameterDirection.Input;
		//		p_new_act_execution_start_at.DbType = DbType.DateTime;
		//		parameters.Add(p_new_act_execution_start_at);
		//		//ExecutionFinishedAt
		//		System.Data.IDataParameter p_new_act_execution_finished_at = dataProvider.GetParameter("@new_act_execution_finished_at", 
		//			f.ExecutionFinishedAt == default(System.DateTime) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.ExecutionFinishedAt);
		//		p_new_act_execution_finished_at.Direction = System.Data.ParameterDirection.Input;
		//		p_new_act_execution_finished_at.DbType = DbType.DateTime;
		//		parameters.Add(p_new_act_execution_finished_at);
		//		//TotalDuration
		//		System.Data.IDataParameter p_new_act_total_duration = dataProvider.GetParameter("@new_act_total_duration", 
		//			f.TotalDuration == default(System.Int32) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.TotalDuration);
		//		p_new_act_total_duration.Direction = System.Data.ParameterDirection.Input;
		//		p_new_act_total_duration.DbType = DbType.Int32;
		//		parameters.Add(p_new_act_total_duration);
		//		//Status
		//		System.Data.IDataParameter p_new_act_status = dataProvider.GetParameter("@new_act_status", 
		//			f.Status == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.Status);
		//		p_new_act_status.Direction = System.Data.ParameterDirection.Input;
		//		p_new_act_status.DbType = DbType.String;
		//		parameters.Add(p_new_act_status);
		//		//Comments
		//		System.Data.IDataParameter p_new_act_comments = dataProvider.GetParameter("@new_act_comments", 
		//			f.Comments == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.Comments);
		//		p_new_act_comments.Direction = System.Data.ParameterDirection.Input;
		//		p_new_act_comments.DbType = DbType.String;
		//		parameters.Add(p_new_act_comments);
		//		//MaintenancePlan
		//		System.Data.IDataParameter p_new_act_maintenance_plan = dataProvider.GetParameter("@new_act_maintenance_plan", 
		//			f.MaintenancePlan == default(System.Int64) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.MaintenancePlan);
		//		p_new_act_maintenance_plan.Direction = System.Data.ParameterDirection.Input;
		//		p_new_act_maintenance_plan.DbType = DbType.Int64;
		//		parameters.Add(p_new_act_maintenance_plan);

  //              return parameters;
  //          };

		//	Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.PM.Activity> mapper = (reader) =>
  //          {
		//		long longHelper = default(long);
		//		int intHelper = default(int);
		//		DateTime dateHelper = default(DateTime);

		//		SOFTTEK.SCMS.Entity.PM.Activity value = new SOFTTEK.SCMS.Entity.PM.Activity{
		//			Identifier = reader["v_act_id_Pk"] != null ? 
		//				(long.TryParse(reader["v_act_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
		//			ExternalIdentifier = reader["v_act_external_identifier"] != null ? 
		//				reader["v_act_external_identifier"].ToString() : string.Empty,
		//			Name = reader["v_act_name"] != null ? 
		//				reader["v_act_name"].ToString() : string.Empty,
		//			Description = reader["v_act_description"] != null ? 
		//				reader["v_act_description"].ToString() : string.Empty,
		//			ExecutionStartAt = reader["v_act_execution_start_at"] != null ? 
		//				(DateTime.TryParse(reader["v_act_execution_start_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
		//			ExecutionFinishedAt = reader["v_act_execution_finished_at"] != null ? 
		//				(DateTime.TryParse(reader["v_act_execution_finished_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
		//			TotalDuration = reader["v_act_total_duration"] != null ? 
		//				(int.TryParse(reader["v_act_total_duration"].ToString(), out intHelper) ? intHelper : -1) : -1,
		//			Status = reader["v_act_status"] != null ? 
		//				reader["v_act_status"].ToString() : string.Empty,
		//			Comments = reader["v_act_comments"] != null ? 
		//				reader["v_act_comments"].ToString() : string.Empty,
		//			MaintenancePlan = reader["v_act_maintenance_plan"] != null ? 
		//				(long.TryParse(reader["v_act_maintenance_plan"].ToString(), out longHelper) ? longHelper : -1) : -1,
		//		};
  //              return value;
  //          };


		//	dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
  //          SOFTTEK.SCMS.Entity.PM.Activity result = dataProvider.ExecuteForEntityWithFilter(SCMS.PM_SP_Update_Activity, filterDelegate, filter, mapper);
  //          return result;
		//}
		//#endregion
		//#region Advice
		
		///// <summary>
  //      /// Retrieves a list of Advice instances that matches a given filter criteria by the Advice object instance 
  //      /// </summary>
  //      /// <param name="filter">Object with the information required to filter the set of results</param>
  //      /// <returns>List of Advice instances that matches the filter.</returns>
		//public List<SOFTTEK.SCMS.Entity.PM.Advice> GetAdvices(SOFTTEK.SCMS.Entity.PM.Advice filter)
		//{
		//	Func<SOFTTEK.SCMS.Entity.PM.Advice, List<System.Data.IDataParameter>> filterDelegate = (f) =>
  //          {
  //              List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
		//		//Token
		//		System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
  //              p_tok.Direction = System.Data.ParameterDirection.Input;
  //              p_tok.DbType = System.Data.DbType.String;
  //              parameters.Add(p_tok);
		//		//Identifier
		//		System.Data.IDataParameter p_filter_adv_id_Pk = dataProvider.GetParameter("@filter_adv_id_Pk", 
		//			f.Identifier == default(System.Int64) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Identifier);
		//		p_filter_adv_id_Pk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_adv_id_Pk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_adv_id_Pk);
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_filter_adv_external_identifier = dataProvider.GetParameter("@filter_adv_external_identifier", 
		//			f.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.ExternalIdentifier);
		//		p_filter_adv_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_adv_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_filter_adv_external_identifier);
		//		//Priority
		//		System.Data.IDataParameter p_filter_adv_priority = dataProvider.GetParameter("@filter_adv_priority", 
		//			f.Priority == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Priority);
		//		p_filter_adv_priority.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_adv_priority.DbType = DbType.String;
		//		parameters.Add(p_filter_adv_priority);
		//		//Type
		//		System.Data.IDataParameter p_filter_adv_type = dataProvider.GetParameter("@filter_adv_type", 
		//			f.Type == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Type);
		//		p_filter_adv_type.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_adv_type.DbType = DbType.String;
		//		parameters.Add(p_filter_adv_type);
		//		//Task
		//		System.Data.IDataParameter p_filter_adv_task_Fk = dataProvider.GetParameter("@filter_adv_task_Fk", 
		//			f.Task == default(SOFTTEK.SCMS.Entity.PM.Task) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Task.Identifier);
		//		p_filter_adv_task_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_adv_task_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_adv_task_Fk);
		//		//DeviceType
		//		System.Data.IDataParameter p_filter_adv_device_type = dataProvider.GetParameter("@filter_adv_device_type", 
		//			f.DeviceType == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.DeviceType);
		//		p_filter_adv_device_type.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_adv_device_type.DbType = DbType.String;
		//		parameters.Add(p_filter_adv_device_type);
		//		//TechnicalObject
		//		System.Data.IDataParameter p_filter_adv_technical_object_Fk = dataProvider.GetParameter("@filter_adv_technical_object_Fk", 
		//			f.TechnicalObject == default(SOFTTEK.SCMS.Entity.PM.TechnicalObject) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.TechnicalObject.Identifier);
		//		p_filter_adv_technical_object_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_adv_technical_object_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_adv_technical_object_Fk);
		//		//Comments
		//		System.Data.IDataParameter p_filter_adv_comments = dataProvider.GetParameter("@filter_adv_comments", 
		//			f.Comments == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Comments);
		//		p_filter_adv_comments.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_adv_comments.DbType = DbType.String;
		//		parameters.Add(p_filter_adv_comments);
  //              return parameters;
  //          };

  //          Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.PM.Advice> mapper = (reader) =>
  //          {
		//		long longHelper = default(long);
		//		DateTime dateHelper = default(DateTime);

		//		SOFTTEK.SCMS.Entity.PM.Advice value = new SOFTTEK.SCMS.Entity.PM.Advice{
		//			Identifier = reader["adv_id_Pk"] != null ? 
		//				(long.TryParse(reader["adv_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
		//			ExternalIdentifier = reader["adv_external_identifier"] != null ? 
		//				reader["adv_external_identifier"].ToString() : string.Empty,
		//			Priority = reader["adv_priority"] != null ? 
		//				reader["adv_priority"].ToString() : string.Empty,
		//			Type = reader["adv_type"] != null ? 
		//				reader["adv_type"].ToString() : string.Empty,
		//			Task = reader["adv_task_Fk"] != null ? 
		//				new SOFTTEK.SCMS.Entity.PM.Task { Identifier = long.TryParse(reader["adv_task_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null, 
		//			DeviceType = reader["adv_device_type"] != null ? 
		//				reader["adv_device_type"].ToString() : string.Empty,
		//			TechnicalObject = reader["adv_technical_object_Fk"] != null ? 
		//				new SOFTTEK.SCMS.Entity.PM.TechnicalObject { Identifier = long.TryParse(reader["adv_technical_object_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null, 
		//			Comments = reader["adv_comments"] != null ? 
		//				reader["adv_comments"].ToString() : string.Empty,
		//			ScheduledTo = reader["adv_scheduled_to"] != null ? 
		//				(DateTime.TryParse(reader["adv_scheduled_to"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
		//			ExecutionStartAt = reader["adv_execution_start_at"] != null ? 
		//				(DateTime.TryParse(reader["adv_execution_start_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
		//			ExecutionFinishedAt = reader["adv_execution_finished_at"] != null ? 
		//				(DateTime.TryParse(reader["adv_execution_finished_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
		//			ExecutionHourStartAt = reader["adv_execution_hour_start_at"] != null ? 
		//				reader["adv_execution_hour_start_at"].ToString() : string.Empty,
		//			ExecutionHourFinishedAt = reader["adv_execution_hour_finished_at"] != null ? 
		//				reader["adv_execution_hour_finished_at"].ToString() : string.Empty,
		//		};
  //              return value;
  //          };

  //          dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
  //          List<SOFTTEK.SCMS.Entity.PM.Advice> result = dataProvider.ExecuteReaderWithFilter(SCMS.PM_SP_Select_Advice, filterDelegate, filter, mapper);
  //          return result;
		//}

		///// <summary>
  //      /// Inserts the instance of the provided Advice at the Data Context datasource  
  //      /// </summary>
  //      /// <param name="instance">Instance of Advice with the entity´s information to store</param>
  //      /// <returns>Instance of Advice with the aditional information provided by the Data Context datasource</returns>
		//public SOFTTEK.SCMS.Entity.PM.Advice InsertAdvice(SOFTTEK.SCMS.Entity.PM.Advice instance){
		//	Func<SOFTTEK.SCMS.Entity.PM.Advice, List<System.Data.IDataParameter>> input = (i) =>
  //          {
  //              List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
			
		//		//Token
		//		System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
  //              p_tok.Direction = System.Data.ParameterDirection.Input;
  //              p_tok.DbType = System.Data.DbType.String;
  //              parameters.Add(p_tok);
				
		//		// Insert
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_new_adv_external_identifier = dataProvider.GetParameter("@new_adv_external_identifier", 
		//			i.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.ExternalIdentifier);
		//		p_new_adv_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_new_adv_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_new_adv_external_identifier);
		//		//Priority
		//		System.Data.IDataParameter p_new_adv_priority = dataProvider.GetParameter("@new_adv_priority", 
		//			i.Priority == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.Priority);
		//		p_new_adv_priority.Direction = System.Data.ParameterDirection.Input;
		//		p_new_adv_priority.DbType = DbType.String;
		//		parameters.Add(p_new_adv_priority);
		//		//Type
		//		System.Data.IDataParameter p_new_adv_type = dataProvider.GetParameter("@new_adv_type", 
		//			i.Type == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.Type);
		//		p_new_adv_type.Direction = System.Data.ParameterDirection.Input;
		//		p_new_adv_type.DbType = DbType.String;
		//		parameters.Add(p_new_adv_type);
		//		//Task
		//		System.Data.IDataParameter p_new_adv_task_Fk = dataProvider.GetParameter("@new_adv_task_Fk", 
		//			i.Task == default(SOFTTEK.SCMS.Entity.PM.Task) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.Task.Identifier);
		//		p_new_adv_task_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_new_adv_task_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_new_adv_task_Fk);
		//		//DeviceType
		//		System.Data.IDataParameter p_new_adv_device_type = dataProvider.GetParameter("@new_adv_device_type", 
		//			i.DeviceType == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.DeviceType);
		//		p_new_adv_device_type.Direction = System.Data.ParameterDirection.Input;
		//		p_new_adv_device_type.DbType = DbType.String;
		//		parameters.Add(p_new_adv_device_type);
		//		//TechnicalObject
		//		System.Data.IDataParameter p_new_adv_technical_object_Fk = dataProvider.GetParameter("@new_adv_technical_object_Fk", 
		//			i.TechnicalObject == default(SOFTTEK.SCMS.Entity.PM.TechnicalObject) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.TechnicalObject.Identifier);
		//		p_new_adv_technical_object_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_new_adv_technical_object_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_new_adv_technical_object_Fk);
		//		//Comments
		//		System.Data.IDataParameter p_new_adv_comments = dataProvider.GetParameter("@new_adv_comments", 
		//			i.Comments == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.Comments);
		//		p_new_adv_comments.Direction = System.Data.ParameterDirection.Input;
		//		p_new_adv_comments.DbType = DbType.String;
		//		parameters.Add(p_new_adv_comments);
		//		//ScheduledTo
		//		System.Data.IDataParameter p_new_adv_scheduled_to = dataProvider.GetParameter("@new_adv_scheduled_to", 
		//			i.ScheduledTo == default(System.DateTime) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.ScheduledTo);
		//		p_new_adv_scheduled_to.Direction = System.Data.ParameterDirection.Input;
		//		p_new_adv_scheduled_to.DbType = DbType.DateTime;
		//		parameters.Add(p_new_adv_scheduled_to);
		//		//ExecutionStartAt
		//		System.Data.IDataParameter p_new_adv_execution_start_at = dataProvider.GetParameter("@new_adv_execution_start_at", 
		//			i.ExecutionStartAt == default(System.DateTime) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.ExecutionStartAt);
		//		p_new_adv_execution_start_at.Direction = System.Data.ParameterDirection.Input;
		//		p_new_adv_execution_start_at.DbType = DbType.DateTime;
		//		parameters.Add(p_new_adv_execution_start_at);
		//		//ExecutionFinishedAt
		//		System.Data.IDataParameter p_new_adv_execution_finished_at = dataProvider.GetParameter("@new_adv_execution_finished_at", 
		//			i.ExecutionFinishedAt == default(System.DateTime) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.ExecutionFinishedAt);
		//		p_new_adv_execution_finished_at.Direction = System.Data.ParameterDirection.Input;
		//		p_new_adv_execution_finished_at.DbType = DbType.DateTime;
		//		parameters.Add(p_new_adv_execution_finished_at);
		//		//ExecutionHourStartAt
		//		System.Data.IDataParameter p_new_adv_execution_hour_start_at = dataProvider.GetParameter("@new_adv_execution_hour_start_at", 
		//			i.ExecutionHourStartAt == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.ExecutionHourStartAt);
		//		p_new_adv_execution_hour_start_at.Direction = System.Data.ParameterDirection.Input;
		//		p_new_adv_execution_hour_start_at.DbType = DbType.String;
		//		parameters.Add(p_new_adv_execution_hour_start_at);
		//		//ExecutionHourFinishedAt
		//		System.Data.IDataParameter p_new_adv_execution_hour_finished_at = dataProvider.GetParameter("@new_adv_execution_hour_finished_at", 
		//			i.ExecutionHourFinishedAt == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.ExecutionHourFinishedAt);
		//		p_new_adv_execution_hour_finished_at.Direction = System.Data.ParameterDirection.Input;
		//		p_new_adv_execution_hour_finished_at.DbType = DbType.String;
		//		parameters.Add(p_new_adv_execution_hour_finished_at);

  //              return parameters;
  //          };

		//	Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.PM.Advice> mapper = (reader) =>
  //          {
		//		long longHelper = default(long);
		//		DateTime dateHelper = default(DateTime);

		//		SOFTTEK.SCMS.Entity.PM.Advice value = new SOFTTEK.SCMS.Entity.PM.Advice{
		//			Identifier = reader["v_adv_id_Pk"] != null ? 
		//				(long.TryParse(reader["v_adv_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
		//			ExternalIdentifier = reader["v_adv_external_identifier"] != null ? 
		//				reader["v_adv_external_identifier"].ToString() : string.Empty,
		//			Priority = reader["v_adv_priority"] != null ? 
		//				reader["v_adv_priority"].ToString() : string.Empty,
		//			Type = reader["v_adv_type"] != null ? 
		//				reader["v_adv_type"].ToString() : string.Empty,
		//			Task = reader["v_adv_task_Fk"] != null ? 
		//				new SOFTTEK.SCMS.Entity.PM.Task { Identifier = long.TryParse(reader["v_adv_task_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null, 
		//			DeviceType = reader["v_adv_device_type"] != null ? 
		//				reader["v_adv_device_type"].ToString() : string.Empty,
		//			TechnicalObject = reader["v_adv_technical_object_Fk"] != null ? 
		//				new SOFTTEK.SCMS.Entity.PM.TechnicalObject { Identifier = long.TryParse(reader["v_adv_technical_object_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null, 
		//			Comments = reader["v_adv_comments"] != null ? 
		//				reader["v_adv_comments"].ToString() : string.Empty,
		//			ScheduledTo = reader["v_adv_scheduled_to"] != null ? 
		//				(DateTime.TryParse(reader["v_adv_scheduled_to"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
		//			ExecutionStartAt = reader["v_adv_execution_start_at"] != null ? 
		//				(DateTime.TryParse(reader["v_adv_execution_start_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
		//			ExecutionFinishedAt = reader["v_adv_execution_finished_at"] != null ? 
		//				(DateTime.TryParse(reader["v_adv_execution_finished_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
		//			ExecutionHourStartAt = reader["v_adv_execution_hour_start_at"] != null ? 
		//				reader["v_adv_execution_hour_start_at"].ToString() : string.Empty,
		//			ExecutionHourFinishedAt = reader["v_adv_execution_hour_finished_at"] != null ? 
		//				reader["v_adv_execution_hour_finished_at"].ToString() : string.Empty,
		//		};
  //              return value;
  //          };


		//	dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
  //          SOFTTEK.SCMS.Entity.PM.Advice result = dataProvider.ExecuteForEntityWithFilter(SCMS.PM_SP_Insert_Advice, input, instance, mapper);
  //          return result;
		//}

		///// <summary>
  //      /// Update the entity's stored information with that provided by the instance of Advice at the Data Context datasource  
  //      /// </summary>
  //      /// <param name="instance">Instance of Advice with the entity´s information to update</param>
  //      /// <param name="filter">Object with the information required to filter the target stored entity´s information</param>
		///// <returns>Instance of Advice with the updated information provided by the Data Context datasource</returns>
		//public SOFTTEK.SCMS.Entity.PM.Advice UpdateAdvice(SOFTTEK.SCMS.Entity.PM.Advice instance, SOFTTEK.SCMS.Entity.PM.Advice filter){
		//	Func<SOFTTEK.SCMS.Entity.PM.Advice, List<System.Data.IDataParameter>> filterDelegate = (f) =>
  //          {
  //              List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
			
		//		//Token
		//		System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
  //              p_tok.Direction = System.Data.ParameterDirection.Input;
  //              p_tok.DbType = System.Data.DbType.String;
  //              parameters.Add(p_tok);
				
		//		// Filter
		//		//Identifier
		//		System.Data.IDataParameter p_filter_adv_id_Pk = dataProvider.GetParameter("@filter_adv_id_Pk", 
		//			f.Identifier == default(System.Int64) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Identifier);
		//		p_filter_adv_id_Pk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_adv_id_Pk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_adv_id_Pk);
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_filter_adv_external_identifier = dataProvider.GetParameter("@filter_adv_external_identifier", 
		//			f.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.ExternalIdentifier);
		//		p_filter_adv_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_adv_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_filter_adv_external_identifier);
		//		//Priority
		//		System.Data.IDataParameter p_filter_adv_priority = dataProvider.GetParameter("@filter_adv_priority", 
		//			f.Priority == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Priority);
		//		p_filter_adv_priority.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_adv_priority.DbType = DbType.String;
		//		parameters.Add(p_filter_adv_priority);
		//		//Type
		//		System.Data.IDataParameter p_filter_adv_type = dataProvider.GetParameter("@filter_adv_type", 
		//			f.Type == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Type);
		//		p_filter_adv_type.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_adv_type.DbType = DbType.String;
		//		parameters.Add(p_filter_adv_type);
		//		//Task
		//		System.Data.IDataParameter p_filter_adv_task_Fk = dataProvider.GetParameter("@filter_adv_task_Fk", 
		//			f.Task == default(SOFTTEK.SCMS.Entity.PM.Task) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Task.Identifier);
		//		p_filter_adv_task_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_adv_task_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_adv_task_Fk);
		//		//DeviceType
		//		System.Data.IDataParameter p_filter_adv_device_type = dataProvider.GetParameter("@filter_adv_device_type", 
		//			f.DeviceType == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.DeviceType);
		//		p_filter_adv_device_type.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_adv_device_type.DbType = DbType.String;
		//		parameters.Add(p_filter_adv_device_type);
		//		//TechnicalObject
		//		System.Data.IDataParameter p_filter_adv_technical_object_Fk = dataProvider.GetParameter("@filter_adv_technical_object_Fk", 
		//			f.TechnicalObject == default(SOFTTEK.SCMS.Entity.PM.TechnicalObject) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.TechnicalObject.Identifier);
		//		p_filter_adv_technical_object_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_adv_technical_object_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_adv_technical_object_Fk);
		//		//Comments
		//		System.Data.IDataParameter p_filter_adv_comments = dataProvider.GetParameter("@filter_adv_comments", 
		//			f.Comments == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Comments);
		//		p_filter_adv_comments.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_adv_comments.DbType = DbType.String;
		//		parameters.Add(p_filter_adv_comments);
		//		// Update
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_new_adv_external_identifier = dataProvider.GetParameter("@new_adv_external_identifier", 
		//			f.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.ExternalIdentifier);
		//		p_new_adv_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_new_adv_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_new_adv_external_identifier);
		//		//Priority
		//		System.Data.IDataParameter p_new_adv_priority = dataProvider.GetParameter("@new_adv_priority", 
		//			f.Priority == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.Priority);
		//		p_new_adv_priority.Direction = System.Data.ParameterDirection.Input;
		//		p_new_adv_priority.DbType = DbType.String;
		//		parameters.Add(p_new_adv_priority);
		//		//Type
		//		System.Data.IDataParameter p_new_adv_type = dataProvider.GetParameter("@new_adv_type", 
		//			f.Type == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.Type);
		//		p_new_adv_type.Direction = System.Data.ParameterDirection.Input;
		//		p_new_adv_type.DbType = DbType.String;
		//		parameters.Add(p_new_adv_type);
		//		//Task
		//		System.Data.IDataParameter p_new_adv_task_Fk = dataProvider.GetParameter("@new_adv_task_Fk", 
		//			instance.Task == default(SOFTTEK.SCMS.Entity.PM.Task) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.Task.Identifier);
		//		p_new_adv_task_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_new_adv_task_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_new_adv_task_Fk);
		//		//DeviceType
		//		System.Data.IDataParameter p_new_adv_device_type = dataProvider.GetParameter("@new_adv_device_type", 
		//			f.DeviceType == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.DeviceType);
		//		p_new_adv_device_type.Direction = System.Data.ParameterDirection.Input;
		//		p_new_adv_device_type.DbType = DbType.String;
		//		parameters.Add(p_new_adv_device_type);
		//		//TechnicalObject
		//		System.Data.IDataParameter p_new_adv_technical_object_Fk = dataProvider.GetParameter("@new_adv_technical_object_Fk", 
		//			instance.TechnicalObject == default(SOFTTEK.SCMS.Entity.PM.TechnicalObject) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.TechnicalObject.Identifier);
		//		p_new_adv_technical_object_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_new_adv_technical_object_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_new_adv_technical_object_Fk);
		//		//Comments
		//		System.Data.IDataParameter p_new_adv_comments = dataProvider.GetParameter("@new_adv_comments", 
		//			f.Comments == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.Comments);
		//		p_new_adv_comments.Direction = System.Data.ParameterDirection.Input;
		//		p_new_adv_comments.DbType = DbType.String;
		//		parameters.Add(p_new_adv_comments);
		//		//ScheduledTo
		//		System.Data.IDataParameter p_new_adv_scheduled_to = dataProvider.GetParameter("@new_adv_scheduled_to", 
		//			f.ScheduledTo == default(System.DateTime) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.ScheduledTo);
		//		p_new_adv_scheduled_to.Direction = System.Data.ParameterDirection.Input;
		//		p_new_adv_scheduled_to.DbType = DbType.DateTime;
		//		parameters.Add(p_new_adv_scheduled_to);
		//		//ExecutionStartAt
		//		System.Data.IDataParameter p_new_adv_execution_start_at = dataProvider.GetParameter("@new_adv_execution_start_at", 
		//			f.ExecutionStartAt == default(System.DateTime) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.ExecutionStartAt);
		//		p_new_adv_execution_start_at.Direction = System.Data.ParameterDirection.Input;
		//		p_new_adv_execution_start_at.DbType = DbType.DateTime;
		//		parameters.Add(p_new_adv_execution_start_at);
		//		//ExecutionFinishedAt
		//		System.Data.IDataParameter p_new_adv_execution_finished_at = dataProvider.GetParameter("@new_adv_execution_finished_at", 
		//			f.ExecutionFinishedAt == default(System.DateTime) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.ExecutionFinishedAt);
		//		p_new_adv_execution_finished_at.Direction = System.Data.ParameterDirection.Input;
		//		p_new_adv_execution_finished_at.DbType = DbType.DateTime;
		//		parameters.Add(p_new_adv_execution_finished_at);
		//		//ExecutionHourStartAt
		//		System.Data.IDataParameter p_new_adv_execution_hour_start_at = dataProvider.GetParameter("@new_adv_execution_hour_start_at", 
		//			f.ExecutionHourStartAt == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.ExecutionHourStartAt);
		//		p_new_adv_execution_hour_start_at.Direction = System.Data.ParameterDirection.Input;
		//		p_new_adv_execution_hour_start_at.DbType = DbType.String;
		//		parameters.Add(p_new_adv_execution_hour_start_at);
		//		//ExecutionHourFinishedAt
		//		System.Data.IDataParameter p_new_adv_execution_hour_finished_at = dataProvider.GetParameter("@new_adv_execution_hour_finished_at", 
		//			f.ExecutionHourFinishedAt == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.ExecutionHourFinishedAt);
		//		p_new_adv_execution_hour_finished_at.Direction = System.Data.ParameterDirection.Input;
		//		p_new_adv_execution_hour_finished_at.DbType = DbType.String;
		//		parameters.Add(p_new_adv_execution_hour_finished_at);

  //              return parameters;
  //          };

		//	Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.PM.Advice> mapper = (reader) =>
  //          {
		//		long longHelper = default(long);
		//		DateTime dateHelper = default(DateTime);

		//		SOFTTEK.SCMS.Entity.PM.Advice value = new SOFTTEK.SCMS.Entity.PM.Advice{
		//			Identifier = reader["v_adv_id_Pk"] != null ? 
		//				(long.TryParse(reader["v_adv_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
		//			ExternalIdentifier = reader["v_adv_external_identifier"] != null ? 
		//				reader["v_adv_external_identifier"].ToString() : string.Empty,
		//			Priority = reader["v_adv_priority"] != null ? 
		//				reader["v_adv_priority"].ToString() : string.Empty,
		//			Type = reader["v_adv_type"] != null ? 
		//				reader["v_adv_type"].ToString() : string.Empty,
		//			Task = reader["v_adv_task_Fk"] != null ? 
		//				new SOFTTEK.SCMS.Entity.PM.Task { Identifier = long.TryParse(reader["v_adv_task_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null, 
		//			DeviceType = reader["v_adv_device_type"] != null ? 
		//				reader["v_adv_device_type"].ToString() : string.Empty,
		//			TechnicalObject = reader["v_adv_technical_object_Fk"] != null ? 
		//				new SOFTTEK.SCMS.Entity.PM.TechnicalObject { Identifier = long.TryParse(reader["v_adv_technical_object_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null, 
		//			Comments = reader["v_adv_comments"] != null ? 
		//				reader["v_adv_comments"].ToString() : string.Empty,
		//			ScheduledTo = reader["v_adv_scheduled_to"] != null ? 
		//				(DateTime.TryParse(reader["v_adv_scheduled_to"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
		//			ExecutionStartAt = reader["v_adv_execution_start_at"] != null ? 
		//				(DateTime.TryParse(reader["v_adv_execution_start_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
		//			ExecutionFinishedAt = reader["v_adv_execution_finished_at"] != null ? 
		//				(DateTime.TryParse(reader["v_adv_execution_finished_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
		//			ExecutionHourStartAt = reader["v_adv_execution_hour_start_at"] != null ? 
		//				reader["v_adv_execution_hour_start_at"].ToString() : string.Empty,
		//			ExecutionHourFinishedAt = reader["v_adv_execution_hour_finished_at"] != null ? 
		//				reader["v_adv_execution_hour_finished_at"].ToString() : string.Empty,
		//		};
  //              return value;
  //          };


		//	dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
  //          SOFTTEK.SCMS.Entity.PM.Advice result = dataProvider.ExecuteForEntityWithFilter(SCMS.PM_SP_Update_Advice, filterDelegate, filter, mapper);
  //          return result;
		//}
		//#endregion
		//#region ComponentReplacement
		
		///// <summary>
  //      /// Retrieves a list of ComponentReplacement instances that matches a given filter criteria by the ComponentReplacement object instance 
  //      /// </summary>
  //      /// <param name="filter">Object with the information required to filter the set of results</param>
  //      /// <returns>List of ComponentReplacement instances that matches the filter.</returns>
		//public List<SOFTTEK.SCMS.Entity.PM.ComponentReplacement> GetComponentReplacements(SOFTTEK.SCMS.Entity.PM.ComponentReplacement filter)
		//{
		//	Func<SOFTTEK.SCMS.Entity.PM.ComponentReplacement, List<System.Data.IDataParameter>> filterDelegate = (f) =>
  //          {
  //              List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
		//		//Token
		//		System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
  //              p_tok.Direction = System.Data.ParameterDirection.Input;
  //              p_tok.DbType = System.Data.DbType.String;
  //              parameters.Add(p_tok);
		//		//Identifier
		//		System.Data.IDataParameter p_filter_cmprpl_id_Pk = dataProvider.GetParameter("@filter_cmprpl_id_Pk", 
		//			f.Identifier == default(System.Int64) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Identifier);
		//		p_filter_cmprpl_id_Pk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_cmprpl_id_Pk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_cmprpl_id_Pk);
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_filter_cmprpl_external_identifier = dataProvider.GetParameter("@filter_cmprpl_external_identifier", 
		//			f.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.ExternalIdentifier);
		//		p_filter_cmprpl_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_cmprpl_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_filter_cmprpl_external_identifier);
		//		//Task
		//		System.Data.IDataParameter p_filter_cmprpl_task_Fk = dataProvider.GetParameter("@filter_cmprpl_task_Fk", 
		//			f.Task == default(SOFTTEK.SCMS.Entity.PM.Task) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Task.Identifier);
		//		p_filter_cmprpl_task_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_cmprpl_task_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_cmprpl_task_Fk);
		//		//Material
		//		System.Data.IDataParameter p_filter_cmprpl_material_Fk = dataProvider.GetParameter("@filter_cmprpl_material_Fk", 
		//			f.Material == default(SOFTTEK.SCMS.Entity.PM.Material) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Material.Identifier);
		//		p_filter_cmprpl_material_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_cmprpl_material_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_cmprpl_material_Fk);
		//		//ReplacedMaterial
		//		System.Data.IDataParameter p_filter_cmprpl_replaced_material_Fk = dataProvider.GetParameter("@filter_cmprpl_replaced_material_Fk", 
		//			f.ReplacedMaterial == default(SOFTTEK.SCMS.Entity.PM.Material) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.ReplacedMaterial.Identifier);
		//		p_filter_cmprpl_replaced_material_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_cmprpl_replaced_material_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_cmprpl_replaced_material_Fk);
		//		//Comments
		//		System.Data.IDataParameter p_filter_cmprpl_comments = dataProvider.GetParameter("@filter_cmprpl_comments", 
		//			f.Comments == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Comments);
		//		p_filter_cmprpl_comments.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_cmprpl_comments.DbType = DbType.String;
		//		parameters.Add(p_filter_cmprpl_comments);
  //              return parameters;
  //          };

  //          Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.PM.ComponentReplacement> mapper = (reader) =>
  //          {
		//		long longHelper = default(long);

		//		SOFTTEK.SCMS.Entity.PM.ComponentReplacement value = new SOFTTEK.SCMS.Entity.PM.ComponentReplacement{
		//			Identifier = reader["cmprpl_id_Pk"] != null ? 
		//				(long.TryParse(reader["cmprpl_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
		//			ExternalIdentifier = reader["cmprpl_external_identifier"] != null ? 
		//				reader["cmprpl_external_identifier"].ToString() : string.Empty,
		//			Task = reader["cmprpl_task_Fk"] != null ? 
		//				new SOFTTEK.SCMS.Entity.PM.Task { Identifier = long.TryParse(reader["cmprpl_task_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null, 
		//			Material = reader["cmprpl_material_Fk"] != null ? 
		//				new SOFTTEK.SCMS.Entity.PM.Material { Identifier = long.TryParse(reader["cmprpl_material_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null, 
		//			ReplacedMaterial = reader["cmprpl_replaced_material_Fk"] != null ? 
		//				new SOFTTEK.SCMS.Entity.PM.Material { Identifier = long.TryParse(reader["cmprpl_replaced_material_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null, 
		//			Comments = reader["cmprpl_comments"] != null ? 
		//				reader["cmprpl_comments"].ToString() : string.Empty,
		//		};
  //              return value;
  //          };

  //          dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
  //          List<SOFTTEK.SCMS.Entity.PM.ComponentReplacement> result = dataProvider.ExecuteReaderWithFilter(SCMS.PM_SP_Select_ComponentReplacement, filterDelegate, filter, mapper);
  //          return result;
		//}

		///// <summary>
  //      /// Inserts the instance of the provided ComponentReplacement at the Data Context datasource  
  //      /// </summary>
  //      /// <param name="instance">Instance of ComponentReplacement with the entity´s information to store</param>
  //      /// <returns>Instance of ComponentReplacement with the aditional information provided by the Data Context datasource</returns>
		//public SOFTTEK.SCMS.Entity.PM.ComponentReplacement InsertComponentReplacement(SOFTTEK.SCMS.Entity.PM.ComponentReplacement instance){
		//	Func<SOFTTEK.SCMS.Entity.PM.ComponentReplacement, List<System.Data.IDataParameter>> input = (i) =>
  //          {
  //              List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
			
		//		//Token
		//		System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
  //              p_tok.Direction = System.Data.ParameterDirection.Input;
  //              p_tok.DbType = System.Data.DbType.String;
  //              parameters.Add(p_tok);
				
		//		// Insert
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_new_cmprpl_external_identifier = dataProvider.GetParameter("@new_cmprpl_external_identifier", 
		//			i.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.ExternalIdentifier);
		//		p_new_cmprpl_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_new_cmprpl_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_new_cmprpl_external_identifier);
		//		//Task
		//		System.Data.IDataParameter p_new_cmprpl_task_Fk = dataProvider.GetParameter("@new_cmprpl_task_Fk", 
		//			i.Task == default(SOFTTEK.SCMS.Entity.PM.Task) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.Task.Identifier);
		//		p_new_cmprpl_task_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_new_cmprpl_task_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_new_cmprpl_task_Fk);
		//		//Material
		//		System.Data.IDataParameter p_new_cmprpl_material_Fk = dataProvider.GetParameter("@new_cmprpl_material_Fk", 
		//			i.Material == default(SOFTTEK.SCMS.Entity.PM.Material) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.Material.Identifier);
		//		p_new_cmprpl_material_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_new_cmprpl_material_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_new_cmprpl_material_Fk);
		//		//ReplacedMaterial
		//		System.Data.IDataParameter p_new_cmprpl_replaced_material_Fk = dataProvider.GetParameter("@new_cmprpl_replaced_material_Fk", 
		//			i.ReplacedMaterial == default(SOFTTEK.SCMS.Entity.PM.Material) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.ReplacedMaterial.Identifier);
		//		p_new_cmprpl_replaced_material_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_new_cmprpl_replaced_material_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_new_cmprpl_replaced_material_Fk);
		//		//Comments
		//		System.Data.IDataParameter p_new_cmprpl_comments = dataProvider.GetParameter("@new_cmprpl_comments", 
		//			i.Comments == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.Comments);
		//		p_new_cmprpl_comments.Direction = System.Data.ParameterDirection.Input;
		//		p_new_cmprpl_comments.DbType = DbType.String;
		//		parameters.Add(p_new_cmprpl_comments);

  //              return parameters;
  //          };

		//	Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.PM.ComponentReplacement> mapper = (reader) =>
  //          {
		//		long longHelper = default(long);

		//		SOFTTEK.SCMS.Entity.PM.ComponentReplacement value = new SOFTTEK.SCMS.Entity.PM.ComponentReplacement{
		//			Identifier = reader["v_cmprpl_id_Pk"] != null ? 
		//				(long.TryParse(reader["v_cmprpl_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
		//			ExternalIdentifier = reader["v_cmprpl_external_identifier"] != null ? 
		//				reader["v_cmprpl_external_identifier"].ToString() : string.Empty,
		//			Task = reader["v_cmprpl_task_Fk"] != null ? 
		//				new SOFTTEK.SCMS.Entity.PM.Task { Identifier = long.TryParse(reader["v_cmprpl_task_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null, 
		//			Material = reader["v_cmprpl_material_Fk"] != null ? 
		//				new SOFTTEK.SCMS.Entity.PM.Material { Identifier = long.TryParse(reader["v_cmprpl_material_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null, 
		//			ReplacedMaterial = reader["v_cmprpl_replaced_material_Fk"] != null ? 
		//				new SOFTTEK.SCMS.Entity.PM.Material { Identifier = long.TryParse(reader["v_cmprpl_replaced_material_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null, 
		//			Comments = reader["v_cmprpl_comments"] != null ? 
		//				reader["v_cmprpl_comments"].ToString() : string.Empty,
		//		};
  //              return value;
  //          };


		//	dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
  //          SOFTTEK.SCMS.Entity.PM.ComponentReplacement result = dataProvider.ExecuteForEntityWithFilter(SCMS.PM_SP_Insert_ComponentReplacement, input, instance, mapper);
  //          return result;
		//}

		///// <summary>
  //      /// Update the entity's stored information with that provided by the instance of ComponentReplacement at the Data Context datasource  
  //      /// </summary>
  //      /// <param name="instance">Instance of ComponentReplacement with the entity´s information to update</param>
  //      /// <param name="filter">Object with the information required to filter the target stored entity´s information</param>
		///// <returns>Instance of ComponentReplacement with the updated information provided by the Data Context datasource</returns>
		//public SOFTTEK.SCMS.Entity.PM.ComponentReplacement UpdateComponentReplacement(SOFTTEK.SCMS.Entity.PM.ComponentReplacement instance, SOFTTEK.SCMS.Entity.PM.ComponentReplacement filter){
		//	Func<SOFTTEK.SCMS.Entity.PM.ComponentReplacement, List<System.Data.IDataParameter>> filterDelegate = (f) =>
  //          {
  //              List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
			
		//		//Token
		//		System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
  //              p_tok.Direction = System.Data.ParameterDirection.Input;
  //              p_tok.DbType = System.Data.DbType.String;
  //              parameters.Add(p_tok);
				
		//		// Filter
		//		//Identifier
		//		System.Data.IDataParameter p_filter_cmprpl_id_Pk = dataProvider.GetParameter("@filter_cmprpl_id_Pk", 
		//			f.Identifier == default(System.Int64) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Identifier);
		//		p_filter_cmprpl_id_Pk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_cmprpl_id_Pk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_cmprpl_id_Pk);
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_filter_cmprpl_external_identifier = dataProvider.GetParameter("@filter_cmprpl_external_identifier", 
		//			f.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.ExternalIdentifier);
		//		p_filter_cmprpl_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_cmprpl_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_filter_cmprpl_external_identifier);
		//		//Task
		//		System.Data.IDataParameter p_filter_cmprpl_task_Fk = dataProvider.GetParameter("@filter_cmprpl_task_Fk", 
		//			f.Task == default(SOFTTEK.SCMS.Entity.PM.Task) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Task.Identifier);
		//		p_filter_cmprpl_task_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_cmprpl_task_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_cmprpl_task_Fk);
		//		//Material
		//		System.Data.IDataParameter p_filter_cmprpl_material_Fk = dataProvider.GetParameter("@filter_cmprpl_material_Fk", 
		//			f.Material == default(SOFTTEK.SCMS.Entity.PM.Material) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Material.Identifier);
		//		p_filter_cmprpl_material_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_cmprpl_material_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_cmprpl_material_Fk);
		//		//ReplacedMaterial
		//		System.Data.IDataParameter p_filter_cmprpl_replaced_material_Fk = dataProvider.GetParameter("@filter_cmprpl_replaced_material_Fk", 
		//			f.ReplacedMaterial == default(SOFTTEK.SCMS.Entity.PM.Material) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.ReplacedMaterial.Identifier);
		//		p_filter_cmprpl_replaced_material_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_cmprpl_replaced_material_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_cmprpl_replaced_material_Fk);
		//		//Comments
		//		System.Data.IDataParameter p_filter_cmprpl_comments = dataProvider.GetParameter("@filter_cmprpl_comments", 
		//			f.Comments == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Comments);
		//		p_filter_cmprpl_comments.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_cmprpl_comments.DbType = DbType.String;
		//		parameters.Add(p_filter_cmprpl_comments);
		//		// Update
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_new_cmprpl_external_identifier = dataProvider.GetParameter("@new_cmprpl_external_identifier", 
		//			f.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.ExternalIdentifier);
		//		p_new_cmprpl_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_new_cmprpl_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_new_cmprpl_external_identifier);
		//		//Task
		//		System.Data.IDataParameter p_new_cmprpl_task_Fk = dataProvider.GetParameter("@new_cmprpl_task_Fk", 
		//			instance.Task == default(SOFTTEK.SCMS.Entity.PM.Task) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.Task.Identifier);
		//		p_new_cmprpl_task_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_new_cmprpl_task_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_new_cmprpl_task_Fk);
		//		//Material
		//		System.Data.IDataParameter p_new_cmprpl_material_Fk = dataProvider.GetParameter("@new_cmprpl_material_Fk", 
		//			instance.Material == default(SOFTTEK.SCMS.Entity.PM.Material) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.Material.Identifier);
		//		p_new_cmprpl_material_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_new_cmprpl_material_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_new_cmprpl_material_Fk);
		//		//ReplacedMaterial
		//		System.Data.IDataParameter p_new_cmprpl_replaced_material_Fk = dataProvider.GetParameter("@new_cmprpl_replaced_material_Fk", 
		//			instance.ReplacedMaterial == default(SOFTTEK.SCMS.Entity.PM.Material) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.ReplacedMaterial.Identifier);
		//		p_new_cmprpl_replaced_material_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_new_cmprpl_replaced_material_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_new_cmprpl_replaced_material_Fk);
		//		//Comments
		//		System.Data.IDataParameter p_new_cmprpl_comments = dataProvider.GetParameter("@new_cmprpl_comments", 
		//			f.Comments == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.Comments);
		//		p_new_cmprpl_comments.Direction = System.Data.ParameterDirection.Input;
		//		p_new_cmprpl_comments.DbType = DbType.String;
		//		parameters.Add(p_new_cmprpl_comments);

  //              return parameters;
  //          };

		//	Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.PM.ComponentReplacement> mapper = (reader) =>
  //          {
		//		long longHelper = default(long);

		//		SOFTTEK.SCMS.Entity.PM.ComponentReplacement value = new SOFTTEK.SCMS.Entity.PM.ComponentReplacement{
		//			Identifier = reader["v_cmprpl_id_Pk"] != null ? 
		//				(long.TryParse(reader["v_cmprpl_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
		//			ExternalIdentifier = reader["v_cmprpl_external_identifier"] != null ? 
		//				reader["v_cmprpl_external_identifier"].ToString() : string.Empty,
		//			Task = reader["v_cmprpl_task_Fk"] != null ? 
		//				new SOFTTEK.SCMS.Entity.PM.Task { Identifier = long.TryParse(reader["v_cmprpl_task_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null, 
		//			Material = reader["v_cmprpl_material_Fk"] != null ? 
		//				new SOFTTEK.SCMS.Entity.PM.Material { Identifier = long.TryParse(reader["v_cmprpl_material_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null, 
		//			ReplacedMaterial = reader["v_cmprpl_replaced_material_Fk"] != null ? 
		//				new SOFTTEK.SCMS.Entity.PM.Material { Identifier = long.TryParse(reader["v_cmprpl_replaced_material_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null, 
		//			Comments = reader["v_cmprpl_comments"] != null ? 
		//				reader["v_cmprpl_comments"].ToString() : string.Empty,
		//		};
  //              return value;
  //          };


		//	dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
  //          SOFTTEK.SCMS.Entity.PM.ComponentReplacement result = dataProvider.ExecuteForEntityWithFilter(SCMS.PM_SP_Update_ComponentReplacement, filterDelegate, filter, mapper);
  //          return result;
		//}
		//#endregion
		//#region Measure
		
		///// <summary>
  //      /// Retrieves a list of Measure instances that matches a given filter criteria by the Measure object instance 
  //      /// </summary>
  //      /// <param name="filter">Object with the information required to filter the set of results</param>
  //      /// <returns>List of Measure instances that matches the filter.</returns>
		//public List<SOFTTEK.SCMS.Entity.PM.Measure> GetMeasures(SOFTTEK.SCMS.Entity.PM.Measure filter)
		//{
		//	Func<SOFTTEK.SCMS.Entity.PM.Measure, List<System.Data.IDataParameter>> filterDelegate = (f) =>
  //          {
  //              List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
		//		//Token
		//		System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
  //              p_tok.Direction = System.Data.ParameterDirection.Input;
  //              p_tok.DbType = System.Data.DbType.String;
  //              parameters.Add(p_tok);
		//		//Identifier
		//		System.Data.IDataParameter p_filter_msr_id_Pk = dataProvider.GetParameter("@filter_msr_id_Pk", 
		//			f.Identifier == default(System.Int64) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Identifier);
		//		p_filter_msr_id_Pk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_msr_id_Pk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_msr_id_Pk);
		//		//UnityOfMeasurement
		//		System.Data.IDataParameter p_filter_msr_unity_of_measurement = dataProvider.GetParameter("@filter_msr_unity_of_measurement", 
		//			f.UnityOfMeasurement == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.UnityOfMeasurement);
		//		p_filter_msr_unity_of_measurement.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_msr_unity_of_measurement.DbType = DbType.String;
		//		parameters.Add(p_filter_msr_unity_of_measurement);
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_filter_msr_external_identifier = dataProvider.GetParameter("@filter_msr_external_identifier", 
		//			f.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.ExternalIdentifier);
		//		p_filter_msr_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_msr_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_filter_msr_external_identifier);
		//		//Comments
		//		System.Data.IDataParameter p_filter_msr_comments = dataProvider.GetParameter("@filter_msr_comments", 
		//			f.Comments == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Comments);
		//		p_filter_msr_comments.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_msr_comments.DbType = DbType.String;
		//		parameters.Add(p_filter_msr_comments);
		//		//Document
		//		System.Data.IDataParameter p_filter_msr_document_Fk = dataProvider.GetParameter("@filter_msr_document_Fk", 
		//			f.Document == default(SOFTTEK.SCMS.Entity.PM.MeasurementDocument) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Document.Identifier);
		//		p_filter_msr_document_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_msr_document_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_msr_document_Fk);
		//		//DeviceType
		//		System.Data.IDataParameter p_filter_msr_device_type = dataProvider.GetParameter("@filter_msr_device_type", 
		//			f.DeviceType == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.DeviceType);
		//		p_filter_msr_device_type.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_msr_device_type.DbType = DbType.String;
		//		parameters.Add(p_filter_msr_device_type);
		//		//TechnicalObject
		//		System.Data.IDataParameter p_filter_msr_technical_object_Fk = dataProvider.GetParameter("@filter_msr_technical_object_Fk", 
		//			f.TechnicalObject == default(SOFTTEK.SCMS.Entity.PM.TechnicalObject) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.TechnicalObject.Identifier);
		//		p_filter_msr_technical_object_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_msr_technical_object_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_msr_technical_object_Fk);
  //              return parameters;
  //          };

  //          Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.PM.Measure> mapper = (reader) =>
  //          {
		//		long longHelper = default(long);

		//		SOFTTEK.SCMS.Entity.PM.Measure value = new SOFTTEK.SCMS.Entity.PM.Measure{
		//			Identifier = reader["msr_id_Pk"] != null ? 
		//				(long.TryParse(reader["msr_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
		//			UnityOfMeasurement = reader["msr_unity_of_measurement"] != null ? 
		//				reader["msr_unity_of_measurement"].ToString() : string.Empty,
		//			Value = reader["msr_value"] != null ? 
		//				reader["msr_value"].ToString() : string.Empty,
		//			ExternalIdentifier = reader["msr_external_identifier"] != null ? 
		//				reader["msr_external_identifier"].ToString() : string.Empty,
		//			Comments = reader["msr_comments"] != null ? 
		//				reader["msr_comments"].ToString() : string.Empty,
		//			Document = reader["msr_document_Fk"] != null ? 
		//				new SOFTTEK.SCMS.Entity.PM.MeasurementDocument { Identifier = long.TryParse(reader["msr_document_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null, 
		//			DeviceType = reader["msr_device_type"] != null ? 
		//				reader["msr_device_type"].ToString() : string.Empty,
		//			TechnicalObject = reader["msr_technical_object_Fk"] != null ? 
		//				new SOFTTEK.SCMS.Entity.PM.TechnicalObject { Identifier = long.TryParse(reader["msr_technical_object_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null, 
		//		};
  //              return value;
  //          };

  //          dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
  //          List<SOFTTEK.SCMS.Entity.PM.Measure> result = dataProvider.ExecuteReaderWithFilter(SCMS.PM_SP_Select_Measure, filterDelegate, filter, mapper);
  //          return result;
		//}

		///// <summary>
  //      /// Inserts the instance of the provided Measure at the Data Context datasource  
  //      /// </summary>
  //      /// <param name="instance">Instance of Measure with the entity´s information to store</param>
  //      /// <returns>Instance of Measure with the aditional information provided by the Data Context datasource</returns>
		//public SOFTTEK.SCMS.Entity.PM.Measure InsertMeasure(SOFTTEK.SCMS.Entity.PM.Measure instance){
		//	Func<SOFTTEK.SCMS.Entity.PM.Measure, List<System.Data.IDataParameter>> input = (i) =>
  //          {
  //              List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
			
		//		//Token
		//		System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
  //              p_tok.Direction = System.Data.ParameterDirection.Input;
  //              p_tok.DbType = System.Data.DbType.String;
  //              parameters.Add(p_tok);
				
		//		// Insert
		//		//UnityOfMeasurement
		//		System.Data.IDataParameter p_new_msr_unity_of_measurement = dataProvider.GetParameter("@new_msr_unity_of_measurement", 
		//			i.UnityOfMeasurement == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.UnityOfMeasurement);
		//		p_new_msr_unity_of_measurement.Direction = System.Data.ParameterDirection.Input;
		//		p_new_msr_unity_of_measurement.DbType = DbType.String;
		//		parameters.Add(p_new_msr_unity_of_measurement);
		//		//Value
		//		System.Data.IDataParameter p_new_msr_value = dataProvider.GetParameter("@new_msr_value", 
		//			i.Value == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.Value);
		//		p_new_msr_value.Direction = System.Data.ParameterDirection.Input;
		//		p_new_msr_value.DbType = DbType.String;
		//		parameters.Add(p_new_msr_value);
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_new_msr_external_identifier = dataProvider.GetParameter("@new_msr_external_identifier", 
		//			i.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.ExternalIdentifier);
		//		p_new_msr_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_new_msr_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_new_msr_external_identifier);
		//		//Comments
		//		System.Data.IDataParameter p_new_msr_comments = dataProvider.GetParameter("@new_msr_comments", 
		//			i.Comments == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.Comments);
		//		p_new_msr_comments.Direction = System.Data.ParameterDirection.Input;
		//		p_new_msr_comments.DbType = DbType.String;
		//		parameters.Add(p_new_msr_comments);
		//		//Document
		//		System.Data.IDataParameter p_new_msr_document_Fk = dataProvider.GetParameter("@new_msr_document_Fk", 
		//			i.Document == default(SOFTTEK.SCMS.Entity.PM.MeasurementDocument) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.Document.Identifier);
		//		p_new_msr_document_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_new_msr_document_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_new_msr_document_Fk);
		//		//DeviceType
		//		System.Data.IDataParameter p_new_msr_device_type = dataProvider.GetParameter("@new_msr_device_type", 
		//			i.DeviceType == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.DeviceType);
		//		p_new_msr_device_type.Direction = System.Data.ParameterDirection.Input;
		//		p_new_msr_device_type.DbType = DbType.String;
		//		parameters.Add(p_new_msr_device_type);
		//		//TechnicalObject
		//		System.Data.IDataParameter p_new_msr_technical_object_Fk = dataProvider.GetParameter("@new_msr_technical_object_Fk", 
		//			i.TechnicalObject == default(SOFTTEK.SCMS.Entity.PM.TechnicalObject) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.TechnicalObject.Identifier);
		//		p_new_msr_technical_object_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_new_msr_technical_object_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_new_msr_technical_object_Fk);

  //              return parameters;
  //          };

		//	Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.PM.Measure> mapper = (reader) =>
  //          {
		//		long longHelper = default(long);

		//		SOFTTEK.SCMS.Entity.PM.Measure value = new SOFTTEK.SCMS.Entity.PM.Measure{
		//			Identifier = reader["v_msr_id_Pk"] != null ? 
		//				(long.TryParse(reader["v_msr_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
		//			UnityOfMeasurement = reader["v_msr_unity_of_measurement"] != null ? 
		//				reader["v_msr_unity_of_measurement"].ToString() : string.Empty,
		//			Value = reader["v_msr_value"] != null ? 
		//				reader["v_msr_value"].ToString() : string.Empty,
		//			ExternalIdentifier = reader["v_msr_external_identifier"] != null ? 
		//				reader["v_msr_external_identifier"].ToString() : string.Empty,
		//			Comments = reader["v_msr_comments"] != null ? 
		//				reader["v_msr_comments"].ToString() : string.Empty,
		//			Document = reader["v_msr_document_Fk"] != null ? 
		//				new SOFTTEK.SCMS.Entity.PM.MeasurementDocument { Identifier = long.TryParse(reader["v_msr_document_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null, 
		//			DeviceType = reader["v_msr_device_type"] != null ? 
		//				reader["v_msr_device_type"].ToString() : string.Empty,
		//			TechnicalObject = reader["v_msr_technical_object_Fk"] != null ? 
		//				new SOFTTEK.SCMS.Entity.PM.TechnicalObject { Identifier = long.TryParse(reader["v_msr_technical_object_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null, 
		//		};
  //              return value;
  //          };


		//	dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
  //          SOFTTEK.SCMS.Entity.PM.Measure result = dataProvider.ExecuteForEntityWithFilter(SCMS.PM_SP_Insert_Measure, input, instance, mapper);
  //          return result;
		//}

		///// <summary>
  //      /// Update the entity's stored information with that provided by the instance of Measure at the Data Context datasource  
  //      /// </summary>
  //      /// <param name="instance">Instance of Measure with the entity´s information to update</param>
  //      /// <param name="filter">Object with the information required to filter the target stored entity´s information</param>
		///// <returns>Instance of Measure with the updated information provided by the Data Context datasource</returns>
		//public SOFTTEK.SCMS.Entity.PM.Measure UpdateMeasure(SOFTTEK.SCMS.Entity.PM.Measure instance, SOFTTEK.SCMS.Entity.PM.Measure filter){
		//	Func<SOFTTEK.SCMS.Entity.PM.Measure, List<System.Data.IDataParameter>> filterDelegate = (f) =>
  //          {
  //              List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
			
		//		//Token
		//		System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
  //              p_tok.Direction = System.Data.ParameterDirection.Input;
  //              p_tok.DbType = System.Data.DbType.String;
  //              parameters.Add(p_tok);
				
		//		// Filter
		//		//Identifier
		//		System.Data.IDataParameter p_filter_msr_id_Pk = dataProvider.GetParameter("@filter_msr_id_Pk", 
		//			f.Identifier == default(System.Int64) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Identifier);
		//		p_filter_msr_id_Pk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_msr_id_Pk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_msr_id_Pk);
		//		//UnityOfMeasurement
		//		System.Data.IDataParameter p_filter_msr_unity_of_measurement = dataProvider.GetParameter("@filter_msr_unity_of_measurement", 
		//			f.UnityOfMeasurement == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.UnityOfMeasurement);
		//		p_filter_msr_unity_of_measurement.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_msr_unity_of_measurement.DbType = DbType.String;
		//		parameters.Add(p_filter_msr_unity_of_measurement);
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_filter_msr_external_identifier = dataProvider.GetParameter("@filter_msr_external_identifier", 
		//			f.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.ExternalIdentifier);
		//		p_filter_msr_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_msr_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_filter_msr_external_identifier);
		//		//Comments
		//		System.Data.IDataParameter p_filter_msr_comments = dataProvider.GetParameter("@filter_msr_comments", 
		//			f.Comments == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Comments);
		//		p_filter_msr_comments.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_msr_comments.DbType = DbType.String;
		//		parameters.Add(p_filter_msr_comments);
		//		//Document
		//		System.Data.IDataParameter p_filter_msr_document_Fk = dataProvider.GetParameter("@filter_msr_document_Fk", 
		//			f.Document == default(SOFTTEK.SCMS.Entity.PM.MeasurementDocument) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Document.Identifier);
		//		p_filter_msr_document_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_msr_document_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_msr_document_Fk);
		//		//DeviceType
		//		System.Data.IDataParameter p_filter_msr_device_type = dataProvider.GetParameter("@filter_msr_device_type", 
		//			f.DeviceType == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.DeviceType);
		//		p_filter_msr_device_type.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_msr_device_type.DbType = DbType.String;
		//		parameters.Add(p_filter_msr_device_type);
		//		//TechnicalObject
		//		System.Data.IDataParameter p_filter_msr_technical_object_Fk = dataProvider.GetParameter("@filter_msr_technical_object_Fk", 
		//			f.TechnicalObject == default(SOFTTEK.SCMS.Entity.PM.TechnicalObject) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.TechnicalObject.Identifier);
		//		p_filter_msr_technical_object_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_msr_technical_object_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_msr_technical_object_Fk);
		//		// Update
		//		//UnityOfMeasurement
		//		System.Data.IDataParameter p_new_msr_unity_of_measurement = dataProvider.GetParameter("@new_msr_unity_of_measurement", 
		//			f.UnityOfMeasurement == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.UnityOfMeasurement);
		//		p_new_msr_unity_of_measurement.Direction = System.Data.ParameterDirection.Input;
		//		p_new_msr_unity_of_measurement.DbType = DbType.String;
		//		parameters.Add(p_new_msr_unity_of_measurement);
		//		//Value
		//		System.Data.IDataParameter p_new_msr_value = dataProvider.GetParameter("@new_msr_value", 
		//			f.Value == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.Value);
		//		p_new_msr_value.Direction = System.Data.ParameterDirection.Input;
		//		p_new_msr_value.DbType = DbType.String;
		//		parameters.Add(p_new_msr_value);
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_new_msr_external_identifier = dataProvider.GetParameter("@new_msr_external_identifier", 
		//			f.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.ExternalIdentifier);
		//		p_new_msr_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_new_msr_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_new_msr_external_identifier);
		//		//Comments
		//		System.Data.IDataParameter p_new_msr_comments = dataProvider.GetParameter("@new_msr_comments", 
		//			f.Comments == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.Comments);
		//		p_new_msr_comments.Direction = System.Data.ParameterDirection.Input;
		//		p_new_msr_comments.DbType = DbType.String;
		//		parameters.Add(p_new_msr_comments);
		//		//Document
		//		System.Data.IDataParameter p_new_msr_document_Fk = dataProvider.GetParameter("@new_msr_document_Fk", 
		//			instance.Document == default(SOFTTEK.SCMS.Entity.PM.MeasurementDocument) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.Document.Identifier);
		//		p_new_msr_document_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_new_msr_document_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_new_msr_document_Fk);
		//		//DeviceType
		//		System.Data.IDataParameter p_new_msr_device_type = dataProvider.GetParameter("@new_msr_device_type", 
		//			f.DeviceType == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.DeviceType);
		//		p_new_msr_device_type.Direction = System.Data.ParameterDirection.Input;
		//		p_new_msr_device_type.DbType = DbType.String;
		//		parameters.Add(p_new_msr_device_type);
		//		//TechnicalObject
		//		System.Data.IDataParameter p_new_msr_technical_object_Fk = dataProvider.GetParameter("@new_msr_technical_object_Fk", 
		//			instance.TechnicalObject == default(SOFTTEK.SCMS.Entity.PM.TechnicalObject) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.TechnicalObject.Identifier);
		//		p_new_msr_technical_object_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_new_msr_technical_object_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_new_msr_technical_object_Fk);

  //              return parameters;
  //          };

		//	Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.PM.Measure> mapper = (reader) =>
  //          {
		//		long longHelper = default(long);

		//		SOFTTEK.SCMS.Entity.PM.Measure value = new SOFTTEK.SCMS.Entity.PM.Measure{
		//			Identifier = reader["v_msr_id_Pk"] != null ? 
		//				(long.TryParse(reader["v_msr_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
		//			UnityOfMeasurement = reader["v_msr_unity_of_measurement"] != null ? 
		//				reader["v_msr_unity_of_measurement"].ToString() : string.Empty,
		//			Value = reader["v_msr_value"] != null ? 
		//				reader["v_msr_value"].ToString() : string.Empty,
		//			ExternalIdentifier = reader["v_msr_external_identifier"] != null ? 
		//				reader["v_msr_external_identifier"].ToString() : string.Empty,
		//			Comments = reader["v_msr_comments"] != null ? 
		//				reader["v_msr_comments"].ToString() : string.Empty,
		//			Document = reader["v_msr_document_Fk"] != null ? 
		//				new SOFTTEK.SCMS.Entity.PM.MeasurementDocument { Identifier = long.TryParse(reader["v_msr_document_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null, 
		//			DeviceType = reader["v_msr_device_type"] != null ? 
		//				reader["v_msr_device_type"].ToString() : string.Empty,
		//			TechnicalObject = reader["v_msr_technical_object_Fk"] != null ? 
		//				new SOFTTEK.SCMS.Entity.PM.TechnicalObject { Identifier = long.TryParse(reader["v_msr_technical_object_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null, 
		//		};
  //              return value;
  //          };


		//	dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
  //          SOFTTEK.SCMS.Entity.PM.Measure result = dataProvider.ExecuteForEntityWithFilter(SCMS.PM_SP_Update_Measure, filterDelegate, filter, mapper);
  //          return result;
		//}
		//#endregion
		//#region MeasurementDocument
		
		///// <summary>
  //      /// Retrieves a list of MeasurementDocument instances that matches a given filter criteria by the MeasurementDocument object instance 
  //      /// </summary>
  //      /// <param name="filter">Object with the information required to filter the set of results</param>
  //      /// <returns>List of MeasurementDocument instances that matches the filter.</returns>
		//public List<SOFTTEK.SCMS.Entity.PM.MeasurementDocument> GetMeasurementDocuments(SOFTTEK.SCMS.Entity.PM.MeasurementDocument filter)
		//{
		//	Func<SOFTTEK.SCMS.Entity.PM.MeasurementDocument, List<System.Data.IDataParameter>> filterDelegate = (f) =>
  //          {
  //              List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
		//		//Token
		//		System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
  //              p_tok.Direction = System.Data.ParameterDirection.Input;
  //              p_tok.DbType = System.Data.DbType.String;
  //              parameters.Add(p_tok);
		//		//Identifier
		//		System.Data.IDataParameter p_filter_msrdcm_id_Pk = dataProvider.GetParameter("@filter_msrdcm_id_Pk", 
		//			f.Identifier == default(System.Int64) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Identifier);
		//		p_filter_msrdcm_id_Pk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_msrdcm_id_Pk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_msrdcm_id_Pk);
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_filter_msrdcm_external_identifier = dataProvider.GetParameter("@filter_msrdcm_external_identifier", 
		//			f.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.ExternalIdentifier);
		//		p_filter_msrdcm_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_msrdcm_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_filter_msrdcm_external_identifier);
		//		//Task
		//		System.Data.IDataParameter p_filter_msrdcm_task_Fk = dataProvider.GetParameter("@filter_msrdcm_task_Fk", 
		//			f.Task == default(SOFTTEK.SCMS.Entity.PM.Task) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Task.Identifier);
		//		p_filter_msrdcm_task_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_msrdcm_task_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_msrdcm_task_Fk);
		//		//DeviceType
		//		System.Data.IDataParameter p_filter_msrdcm_device_type = dataProvider.GetParameter("@filter_msrdcm_device_type", 
		//			f.DeviceType == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.DeviceType);
		//		p_filter_msrdcm_device_type.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_msrdcm_device_type.DbType = DbType.String;
		//		parameters.Add(p_filter_msrdcm_device_type);
		//		//TechnicalObject
		//		System.Data.IDataParameter p_filter_msrdcm_technical_object_Fk = dataProvider.GetParameter("@filter_msrdcm_technical_object_Fk", 
		//			f.TechnicalObject == default(SOFTTEK.SCMS.Entity.PM.TechnicalObject) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.TechnicalObject.Identifier);
		//		p_filter_msrdcm_technical_object_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_msrdcm_technical_object_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_msrdcm_technical_object_Fk);
  //              return parameters;
  //          };

  //          Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.PM.MeasurementDocument> mapper = (reader) =>
  //          {
		//		long longHelper = default(long);

		//		SOFTTEK.SCMS.Entity.PM.MeasurementDocument value = new SOFTTEK.SCMS.Entity.PM.MeasurementDocument{
		//			Identifier = reader["msrdcm_id_Pk"] != null ? 
		//				(long.TryParse(reader["msrdcm_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
		//			ExternalIdentifier = reader["msrdcm_external_identifier"] != null ? 
		//				reader["msrdcm_external_identifier"].ToString() : string.Empty,
		//			Task = reader["msrdcm_task_Fk"] != null ? 
		//				new SOFTTEK.SCMS.Entity.PM.Task { Identifier = long.TryParse(reader["msrdcm_task_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null, 
		//			DeviceType = reader["msrdcm_device_type"] != null ? 
		//				reader["msrdcm_device_type"].ToString() : string.Empty,
		//			TechnicalObject = reader["msrdcm_technical_object_Fk"] != null ? 
		//				new SOFTTEK.SCMS.Entity.PM.TechnicalObject { Identifier = long.TryParse(reader["msrdcm_technical_object_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null, 
		//		};
  //              return value;
  //          };

  //          dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
  //          List<SOFTTEK.SCMS.Entity.PM.MeasurementDocument> result = dataProvider.ExecuteReaderWithFilter(SCMS.PM_SP_Select_MeasurementDocument, filterDelegate, filter, mapper);
  //          return result;
		//}

		///// <summary>
  //      /// Inserts the instance of the provided MeasurementDocument at the Data Context datasource  
  //      /// </summary>
  //      /// <param name="instance">Instance of MeasurementDocument with the entity´s information to store</param>
  //      /// <returns>Instance of MeasurementDocument with the aditional information provided by the Data Context datasource</returns>
		//public SOFTTEK.SCMS.Entity.PM.MeasurementDocument InsertMeasurementDocument(SOFTTEK.SCMS.Entity.PM.MeasurementDocument instance){
		//	Func<SOFTTEK.SCMS.Entity.PM.MeasurementDocument, List<System.Data.IDataParameter>> input = (i) =>
  //          {
  //              List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
			
		//		//Token
		//		System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
  //              p_tok.Direction = System.Data.ParameterDirection.Input;
  //              p_tok.DbType = System.Data.DbType.String;
  //              parameters.Add(p_tok);
				
		//		// Insert
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_new_msrdcm_external_identifier = dataProvider.GetParameter("@new_msrdcm_external_identifier", 
		//			i.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.ExternalIdentifier);
		//		p_new_msrdcm_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_new_msrdcm_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_new_msrdcm_external_identifier);
		//		//Task
		//		System.Data.IDataParameter p_new_msrdcm_task_Fk = dataProvider.GetParameter("@new_msrdcm_task_Fk", 
		//			i.Task == default(SOFTTEK.SCMS.Entity.PM.Task) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.Task.Identifier);
		//		p_new_msrdcm_task_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_new_msrdcm_task_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_new_msrdcm_task_Fk);
		//		//DeviceType
		//		System.Data.IDataParameter p_new_msrdcm_device_type = dataProvider.GetParameter("@new_msrdcm_device_type", 
		//			i.DeviceType == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.DeviceType);
		//		p_new_msrdcm_device_type.Direction = System.Data.ParameterDirection.Input;
		//		p_new_msrdcm_device_type.DbType = DbType.String;
		//		parameters.Add(p_new_msrdcm_device_type);
		//		//TechnicalObject
		//		System.Data.IDataParameter p_new_msrdcm_technical_object_Fk = dataProvider.GetParameter("@new_msrdcm_technical_object_Fk", 
		//			i.TechnicalObject == default(SOFTTEK.SCMS.Entity.PM.TechnicalObject) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.TechnicalObject.Identifier);
		//		p_new_msrdcm_technical_object_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_new_msrdcm_technical_object_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_new_msrdcm_technical_object_Fk);

  //              return parameters;
  //          };

		//	Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.PM.MeasurementDocument> mapper = (reader) =>
  //          {
		//		long longHelper = default(long);

		//		SOFTTEK.SCMS.Entity.PM.MeasurementDocument value = new SOFTTEK.SCMS.Entity.PM.MeasurementDocument{
		//			Identifier = reader["v_msrdcm_id_Pk"] != null ? 
		//				(long.TryParse(reader["v_msrdcm_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
		//			ExternalIdentifier = reader["v_msrdcm_external_identifier"] != null ? 
		//				reader["v_msrdcm_external_identifier"].ToString() : string.Empty,
		//			Task = reader["v_msrdcm_task_Fk"] != null ? 
		//				new SOFTTEK.SCMS.Entity.PM.Task { Identifier = long.TryParse(reader["v_msrdcm_task_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null, 
		//			DeviceType = reader["v_msrdcm_device_type"] != null ? 
		//				reader["v_msrdcm_device_type"].ToString() : string.Empty,
		//			TechnicalObject = reader["v_msrdcm_technical_object_Fk"] != null ? 
		//				new SOFTTEK.SCMS.Entity.PM.TechnicalObject { Identifier = long.TryParse(reader["v_msrdcm_technical_object_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null, 
		//		};
  //              return value;
  //          };


		//	dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
  //          SOFTTEK.SCMS.Entity.PM.MeasurementDocument result = dataProvider.ExecuteForEntityWithFilter(SCMS.PM_SP_Insert_MeasurementDocument, input, instance, mapper);
  //          return result;
		//}

		///// <summary>
  //      /// Update the entity's stored information with that provided by the instance of MeasurementDocument at the Data Context datasource  
  //      /// </summary>
  //      /// <param name="instance">Instance of MeasurementDocument with the entity´s information to update</param>
  //      /// <param name="filter">Object with the information required to filter the target stored entity´s information</param>
		///// <returns>Instance of MeasurementDocument with the updated information provided by the Data Context datasource</returns>
		//public SOFTTEK.SCMS.Entity.PM.MeasurementDocument UpdateMeasurementDocument(SOFTTEK.SCMS.Entity.PM.MeasurementDocument instance, SOFTTEK.SCMS.Entity.PM.MeasurementDocument filter){
		//	Func<SOFTTEK.SCMS.Entity.PM.MeasurementDocument, List<System.Data.IDataParameter>> filterDelegate = (f) =>
  //          {
  //              List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
			
		//		//Token
		//		System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
  //              p_tok.Direction = System.Data.ParameterDirection.Input;
  //              p_tok.DbType = System.Data.DbType.String;
  //              parameters.Add(p_tok);
				
		//		// Filter
		//		//Identifier
		//		System.Data.IDataParameter p_filter_msrdcm_id_Pk = dataProvider.GetParameter("@filter_msrdcm_id_Pk", 
		//			f.Identifier == default(System.Int64) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Identifier);
		//		p_filter_msrdcm_id_Pk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_msrdcm_id_Pk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_msrdcm_id_Pk);
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_filter_msrdcm_external_identifier = dataProvider.GetParameter("@filter_msrdcm_external_identifier", 
		//			f.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.ExternalIdentifier);
		//		p_filter_msrdcm_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_msrdcm_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_filter_msrdcm_external_identifier);
		//		//Task
		//		System.Data.IDataParameter p_filter_msrdcm_task_Fk = dataProvider.GetParameter("@filter_msrdcm_task_Fk", 
		//			f.Task == default(SOFTTEK.SCMS.Entity.PM.Task) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Task.Identifier);
		//		p_filter_msrdcm_task_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_msrdcm_task_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_msrdcm_task_Fk);
		//		//DeviceType
		//		System.Data.IDataParameter p_filter_msrdcm_device_type = dataProvider.GetParameter("@filter_msrdcm_device_type", 
		//			f.DeviceType == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.DeviceType);
		//		p_filter_msrdcm_device_type.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_msrdcm_device_type.DbType = DbType.String;
		//		parameters.Add(p_filter_msrdcm_device_type);
		//		//TechnicalObject
		//		System.Data.IDataParameter p_filter_msrdcm_technical_object_Fk = dataProvider.GetParameter("@filter_msrdcm_technical_object_Fk", 
		//			f.TechnicalObject == default(SOFTTEK.SCMS.Entity.PM.TechnicalObject) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.TechnicalObject.Identifier);
		//		p_filter_msrdcm_technical_object_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_msrdcm_technical_object_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_msrdcm_technical_object_Fk);
		//		// Update
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_new_msrdcm_external_identifier = dataProvider.GetParameter("@new_msrdcm_external_identifier", 
		//			f.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.ExternalIdentifier);
		//		p_new_msrdcm_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_new_msrdcm_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_new_msrdcm_external_identifier);
		//		//Task
		//		System.Data.IDataParameter p_new_msrdcm_task_Fk = dataProvider.GetParameter("@new_msrdcm_task_Fk", 
		//			instance.Task == default(SOFTTEK.SCMS.Entity.PM.Task) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.Task.Identifier);
		//		p_new_msrdcm_task_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_new_msrdcm_task_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_new_msrdcm_task_Fk);
		//		//DeviceType
		//		System.Data.IDataParameter p_new_msrdcm_device_type = dataProvider.GetParameter("@new_msrdcm_device_type", 
		//			f.DeviceType == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.DeviceType);
		//		p_new_msrdcm_device_type.Direction = System.Data.ParameterDirection.Input;
		//		p_new_msrdcm_device_type.DbType = DbType.String;
		//		parameters.Add(p_new_msrdcm_device_type);
		//		//TechnicalObject
		//		System.Data.IDataParameter p_new_msrdcm_technical_object_Fk = dataProvider.GetParameter("@new_msrdcm_technical_object_Fk", 
		//			instance.TechnicalObject == default(SOFTTEK.SCMS.Entity.PM.TechnicalObject) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.TechnicalObject.Identifier);
		//		p_new_msrdcm_technical_object_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_new_msrdcm_technical_object_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_new_msrdcm_technical_object_Fk);

  //              return parameters;
  //          };

		//	Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.PM.MeasurementDocument> mapper = (reader) =>
  //          {
		//		long longHelper = default(long);

		//		SOFTTEK.SCMS.Entity.PM.MeasurementDocument value = new SOFTTEK.SCMS.Entity.PM.MeasurementDocument{
		//			Identifier = reader["v_msrdcm_id_Pk"] != null ? 
		//				(long.TryParse(reader["v_msrdcm_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
		//			ExternalIdentifier = reader["v_msrdcm_external_identifier"] != null ? 
		//				reader["v_msrdcm_external_identifier"].ToString() : string.Empty,
		//			Task = reader["v_msrdcm_task_Fk"] != null ? 
		//				new SOFTTEK.SCMS.Entity.PM.Task { Identifier = long.TryParse(reader["v_msrdcm_task_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null, 
		//			DeviceType = reader["v_msrdcm_device_type"] != null ? 
		//				reader["v_msrdcm_device_type"].ToString() : string.Empty,
		//			TechnicalObject = reader["v_msrdcm_technical_object_Fk"] != null ? 
		//				new SOFTTEK.SCMS.Entity.PM.TechnicalObject { Identifier = long.TryParse(reader["v_msrdcm_technical_object_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null, 
		//		};
  //              return value;
  //          };


		//	dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
  //          SOFTTEK.SCMS.Entity.PM.MeasurementDocument result = dataProvider.ExecuteForEntityWithFilter(SCMS.PM_SP_Update_MeasurementDocument, filterDelegate, filter, mapper);
  //          return result;
		//}
		//#endregion
		//#region Notifications
		
		///// <summary>
  //      /// Retrieves a list of Notifications instances that matches a given filter criteria by the Notifications object instance 
  //      /// </summary>
  //      /// <param name="filter">Object with the information required to filter the set of results</param>
  //      /// <returns>List of Notifications instances that matches the filter.</returns>
		//public List<SOFTTEK.SCMS.Entity.PM.Notifications> GetNotificationss(SOFTTEK.SCMS.Entity.PM.Notifications filter)
		//{
		//	Func<SOFTTEK.SCMS.Entity.PM.Notifications, List<System.Data.IDataParameter>> filterDelegate = (f) =>
  //          {
  //              List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
		//		//Token
		//		System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
  //              p_tok.Direction = System.Data.ParameterDirection.Input;
  //              p_tok.DbType = System.Data.DbType.String;
  //              parameters.Add(p_tok);
		//		//Identifier
		//		System.Data.IDataParameter p_filter_ntf_id_Pk = dataProvider.GetParameter("@filter_ntf_id_Pk", 
		//			f.Identifier == default(System.Int64) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Identifier);
		//		p_filter_ntf_id_Pk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_ntf_id_Pk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_ntf_id_Pk);
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_filter_ntf_external_identifier = dataProvider.GetParameter("@filter_ntf_external_identifier", 
		//			f.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.ExternalIdentifier);
		//		p_filter_ntf_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_ntf_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_filter_ntf_external_identifier);
		//		//Priority
		//		System.Data.IDataParameter p_filter_ntf_priority = dataProvider.GetParameter("@filter_ntf_priority", 
		//			f.Priority == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Priority);
		//		p_filter_ntf_priority.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_ntf_priority.DbType = DbType.String;
		//		parameters.Add(p_filter_ntf_priority);
		//		//Comments
		//		System.Data.IDataParameter p_filter_ntf_comments = dataProvider.GetParameter("@filter_ntf_comments", 
		//			f.Comments == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Comments);
		//		p_filter_ntf_comments.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_ntf_comments.DbType = DbType.String;
		//		parameters.Add(p_filter_ntf_comments);
		//		//ActualWork
		//		System.Data.IDataParameter p_filter_ntf_actual_work = dataProvider.GetParameter("@filter_ntf_actual_work", 
		//			f.ActualWork == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.ActualWork);
		//		p_filter_ntf_actual_work.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_ntf_actual_work.DbType = DbType.String;
		//		parameters.Add(p_filter_ntf_actual_work);
		//		//ExecutionStartAt
		//		System.Data.IDataParameter p_filter_ntf_execution_start_at = dataProvider.GetParameter("@filter_ntf_execution_start_at", 
		//			f.ExecutionStartAt == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.ExecutionStartAt);
		//		p_filter_ntf_execution_start_at.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_ntf_execution_start_at.DbType = DbType.String;
		//		parameters.Add(p_filter_ntf_execution_start_at);
  //              return parameters;
  //          };

  //          Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.PM.Notifications> mapper = (reader) =>
  //          {
		//		long longHelper = default(long);

		//		SOFTTEK.SCMS.Entity.PM.Notifications value = new SOFTTEK.SCMS.Entity.PM.Notifications{
		//			Identifier = reader["ntf_id_Pk"] != null ? 
		//				(long.TryParse(reader["ntf_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
		//			ExternalIdentifier = reader["ntf_external_identifier"] != null ? 
		//				reader["ntf_external_identifier"].ToString() : string.Empty,
		//			Priority = reader["ntf_priority"] != null ? 
		//				reader["ntf_priority"].ToString() : string.Empty,
		//			Comments = reader["ntf_comments"] != null ? 
		//				reader["ntf_comments"].ToString() : string.Empty,
		//			ActualWork = reader["ntf_actual_work"] != null ? 
		//				reader["ntf_actual_work"].ToString() : string.Empty,
		//			ExecutionStartAt = reader["ntf_execution_start_at"] != null ? 
		//				reader["ntf_execution_start_at"].ToString() : string.Empty,
		//		};
  //              return value;
  //          };

  //          dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
  //          List<SOFTTEK.SCMS.Entity.PM.Notifications> result = dataProvider.ExecuteReaderWithFilter(SCMS.PM_SP_Select_Notifications, filterDelegate, filter, mapper);
  //          return result;
		//}

		///// <summary>
  //      /// Inserts the instance of the provided Notifications at the Data Context datasource  
  //      /// </summary>
  //      /// <param name="instance">Instance of Notifications with the entity´s information to store</param>
  //      /// <returns>Instance of Notifications with the aditional information provided by the Data Context datasource</returns>
		//public SOFTTEK.SCMS.Entity.PM.Notifications InsertNotifications(SOFTTEK.SCMS.Entity.PM.Notifications instance){
		//	Func<SOFTTEK.SCMS.Entity.PM.Notifications, List<System.Data.IDataParameter>> input = (i) =>
  //          {
  //              List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
			
		//		//Token
		//		System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
  //              p_tok.Direction = System.Data.ParameterDirection.Input;
  //              p_tok.DbType = System.Data.DbType.String;
  //              parameters.Add(p_tok);
				
		//		// Insert
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_new_ntf_external_identifier = dataProvider.GetParameter("@new_ntf_external_identifier", 
		//			i.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.ExternalIdentifier);
		//		p_new_ntf_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_new_ntf_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_new_ntf_external_identifier);
		//		//Priority
		//		System.Data.IDataParameter p_new_ntf_priority = dataProvider.GetParameter("@new_ntf_priority", 
		//			i.Priority == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.Priority);
		//		p_new_ntf_priority.Direction = System.Data.ParameterDirection.Input;
		//		p_new_ntf_priority.DbType = DbType.String;
		//		parameters.Add(p_new_ntf_priority);
		//		//Comments
		//		System.Data.IDataParameter p_new_ntf_comments = dataProvider.GetParameter("@new_ntf_comments", 
		//			i.Comments == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.Comments);
		//		p_new_ntf_comments.Direction = System.Data.ParameterDirection.Input;
		//		p_new_ntf_comments.DbType = DbType.String;
		//		parameters.Add(p_new_ntf_comments);
		//		//ActualWork
		//		System.Data.IDataParameter p_new_ntf_actual_work = dataProvider.GetParameter("@new_ntf_actual_work", 
		//			i.ActualWork == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.ActualWork);
		//		p_new_ntf_actual_work.Direction = System.Data.ParameterDirection.Input;
		//		p_new_ntf_actual_work.DbType = DbType.String;
		//		parameters.Add(p_new_ntf_actual_work);
		//		//ExecutionStartAt
		//		System.Data.IDataParameter p_new_ntf_execution_start_at = dataProvider.GetParameter("@new_ntf_execution_start_at", 
		//			i.ExecutionStartAt == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.ExecutionStartAt);
		//		p_new_ntf_execution_start_at.Direction = System.Data.ParameterDirection.Input;
		//		p_new_ntf_execution_start_at.DbType = DbType.String;
		//		parameters.Add(p_new_ntf_execution_start_at);

  //              return parameters;
  //          };

		//	Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.PM.Notifications> mapper = (reader) =>
  //          {
		//		long longHelper = default(long);

		//		SOFTTEK.SCMS.Entity.PM.Notifications value = new SOFTTEK.SCMS.Entity.PM.Notifications{
		//			Identifier = reader["v_ntf_id_Pk"] != null ? 
		//				(long.TryParse(reader["v_ntf_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
		//			ExternalIdentifier = reader["v_ntf_external_identifier"] != null ? 
		//				reader["v_ntf_external_identifier"].ToString() : string.Empty,
		//			Priority = reader["v_ntf_priority"] != null ? 
		//				reader["v_ntf_priority"].ToString() : string.Empty,
		//			Comments = reader["v_ntf_comments"] != null ? 
		//				reader["v_ntf_comments"].ToString() : string.Empty,
		//			ActualWork = reader["v_ntf_actual_work"] != null ? 
		//				reader["v_ntf_actual_work"].ToString() : string.Empty,
		//			ExecutionStartAt = reader["v_ntf_execution_start_at"] != null ? 
		//				reader["v_ntf_execution_start_at"].ToString() : string.Empty,
		//		};
  //              return value;
  //          };


		//	dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
  //          SOFTTEK.SCMS.Entity.PM.Notifications result = dataProvider.ExecuteForEntityWithFilter(SCMS.PM_SP_Insert_Notifications, input, instance, mapper);
  //          return result;
		//}

		///// <summary>
  //      /// Update the entity's stored information with that provided by the instance of Notifications at the Data Context datasource  
  //      /// </summary>
  //      /// <param name="instance">Instance of Notifications with the entity´s information to update</param>
  //      /// <param name="filter">Object with the information required to filter the target stored entity´s information</param>
		///// <returns>Instance of Notifications with the updated information provided by the Data Context datasource</returns>
		//public SOFTTEK.SCMS.Entity.PM.Notifications UpdateNotifications(SOFTTEK.SCMS.Entity.PM.Notifications instance, SOFTTEK.SCMS.Entity.PM.Notifications filter){
		//	Func<SOFTTEK.SCMS.Entity.PM.Notifications, List<System.Data.IDataParameter>> filterDelegate = (f) =>
  //          {
  //              List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
			
		//		//Token
		//		System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
  //              p_tok.Direction = System.Data.ParameterDirection.Input;
  //              p_tok.DbType = System.Data.DbType.String;
  //              parameters.Add(p_tok);
				
		//		// Filter
		//		//Identifier
		//		System.Data.IDataParameter p_filter_ntf_id_Pk = dataProvider.GetParameter("@filter_ntf_id_Pk", 
		//			f.Identifier == default(System.Int64) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Identifier);
		//		p_filter_ntf_id_Pk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_ntf_id_Pk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_ntf_id_Pk);
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_filter_ntf_external_identifier = dataProvider.GetParameter("@filter_ntf_external_identifier", 
		//			f.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.ExternalIdentifier);
		//		p_filter_ntf_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_ntf_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_filter_ntf_external_identifier);
		//		//Priority
		//		System.Data.IDataParameter p_filter_ntf_priority = dataProvider.GetParameter("@filter_ntf_priority", 
		//			f.Priority == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Priority);
		//		p_filter_ntf_priority.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_ntf_priority.DbType = DbType.String;
		//		parameters.Add(p_filter_ntf_priority);
		//		//Comments
		//		System.Data.IDataParameter p_filter_ntf_comments = dataProvider.GetParameter("@filter_ntf_comments", 
		//			f.Comments == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Comments);
		//		p_filter_ntf_comments.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_ntf_comments.DbType = DbType.String;
		//		parameters.Add(p_filter_ntf_comments);
		//		//ActualWork
		//		System.Data.IDataParameter p_filter_ntf_actual_work = dataProvider.GetParameter("@filter_ntf_actual_work", 
		//			f.ActualWork == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.ActualWork);
		//		p_filter_ntf_actual_work.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_ntf_actual_work.DbType = DbType.String;
		//		parameters.Add(p_filter_ntf_actual_work);
		//		//ExecutionStartAt
		//		System.Data.IDataParameter p_filter_ntf_execution_start_at = dataProvider.GetParameter("@filter_ntf_execution_start_at", 
		//			f.ExecutionStartAt == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.ExecutionStartAt);
		//		p_filter_ntf_execution_start_at.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_ntf_execution_start_at.DbType = DbType.String;
		//		parameters.Add(p_filter_ntf_execution_start_at);
		//		// Update
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_new_ntf_external_identifier = dataProvider.GetParameter("@new_ntf_external_identifier", 
		//			f.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.ExternalIdentifier);
		//		p_new_ntf_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_new_ntf_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_new_ntf_external_identifier);
		//		//Priority
		//		System.Data.IDataParameter p_new_ntf_priority = dataProvider.GetParameter("@new_ntf_priority", 
		//			f.Priority == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.Priority);
		//		p_new_ntf_priority.Direction = System.Data.ParameterDirection.Input;
		//		p_new_ntf_priority.DbType = DbType.String;
		//		parameters.Add(p_new_ntf_priority);
		//		//Comments
		//		System.Data.IDataParameter p_new_ntf_comments = dataProvider.GetParameter("@new_ntf_comments", 
		//			f.Comments == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.Comments);
		//		p_new_ntf_comments.Direction = System.Data.ParameterDirection.Input;
		//		p_new_ntf_comments.DbType = DbType.String;
		//		parameters.Add(p_new_ntf_comments);
		//		//ActualWork
		//		System.Data.IDataParameter p_new_ntf_actual_work = dataProvider.GetParameter("@new_ntf_actual_work", 
		//			f.ActualWork == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.ActualWork);
		//		p_new_ntf_actual_work.Direction = System.Data.ParameterDirection.Input;
		//		p_new_ntf_actual_work.DbType = DbType.String;
		//		parameters.Add(p_new_ntf_actual_work);
		//		//ExecutionStartAt
		//		System.Data.IDataParameter p_new_ntf_execution_start_at = dataProvider.GetParameter("@new_ntf_execution_start_at", 
		//			f.ExecutionStartAt == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.ExecutionStartAt);
		//		p_new_ntf_execution_start_at.Direction = System.Data.ParameterDirection.Input;
		//		p_new_ntf_execution_start_at.DbType = DbType.String;
		//		parameters.Add(p_new_ntf_execution_start_at);

  //              return parameters;
  //          };

		//	Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.PM.Notifications> mapper = (reader) =>
  //          {
		//		long longHelper = default(long);

		//		SOFTTEK.SCMS.Entity.PM.Notifications value = new SOFTTEK.SCMS.Entity.PM.Notifications{
		//			Identifier = reader["v_ntf_id_Pk"] != null ? 
		//				(long.TryParse(reader["v_ntf_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
		//			ExternalIdentifier = reader["v_ntf_external_identifier"] != null ? 
		//				reader["v_ntf_external_identifier"].ToString() : string.Empty,
		//			Priority = reader["v_ntf_priority"] != null ? 
		//				reader["v_ntf_priority"].ToString() : string.Empty,
		//			Comments = reader["v_ntf_comments"] != null ? 
		//				reader["v_ntf_comments"].ToString() : string.Empty,
		//			ActualWork = reader["v_ntf_actual_work"] != null ? 
		//				reader["v_ntf_actual_work"].ToString() : string.Empty,
		//			ExecutionStartAt = reader["v_ntf_execution_start_at"] != null ? 
		//				reader["v_ntf_execution_start_at"].ToString() : string.Empty,
		//		};
  //              return value;
  //          };


		//	dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
  //          SOFTTEK.SCMS.Entity.PM.Notifications result = dataProvider.ExecuteForEntityWithFilter(SCMS.PM_SP_Update_Notifications, filterDelegate, filter, mapper);
  //          return result;
		//}
		//#endregion
		//#region TechnicalObject
		
		///// <summary>
  //      /// Retrieves a list of TechnicalObject instances that matches a given filter criteria by the TechnicalObject object instance 
  //      /// </summary>
  //      /// <param name="filter">Object with the information required to filter the set of results</param>
  //      /// <returns>List of TechnicalObject instances that matches the filter.</returns>
		//public List<SOFTTEK.SCMS.Entity.PM.TechnicalObject> GetTechnicalObjects(SOFTTEK.SCMS.Entity.PM.TechnicalObject filter)
		//{
		//	Func<SOFTTEK.SCMS.Entity.PM.TechnicalObject, List<System.Data.IDataParameter>> filterDelegate = (f) =>
  //          {
  //              List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
		//		//Token
		//		System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
  //              p_tok.Direction = System.Data.ParameterDirection.Input;
  //              p_tok.DbType = System.Data.DbType.String;
  //              parameters.Add(p_tok);
		//		//Name
		//		System.Data.IDataParameter p_filter_tchobj_name = dataProvider.GetParameter("@filter_tchobj_name", 
		//			f.Name == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Name);
		//		p_filter_tchobj_name.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_tchobj_name.DbType = DbType.String;
		//		parameters.Add(p_filter_tchobj_name);
		//		//TypeObject
		//		System.Data.IDataParameter p_filter_tchobj_type_object = dataProvider.GetParameter("@filter_tchobj_type_object", 
		//			f.TypeObject == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.TypeObject);
		//		p_filter_tchobj_type_object.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_tchobj_type_object.DbType = DbType.String;
		//		parameters.Add(p_filter_tchobj_type_object);
		//		//Identifier
		//		System.Data.IDataParameter p_filter_tchobj_id_Pk = dataProvider.GetParameter("@filter_tchobj_id_Pk", 
		//			f.Identifier == default(System.Int64) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Identifier);
		//		p_filter_tchobj_id_Pk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_tchobj_id_Pk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_tchobj_id_Pk);
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_filter_tchobj_external_identifier = dataProvider.GetParameter("@filter_tchobj_external_identifier", 
		//			f.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.ExternalIdentifier);
		//		p_filter_tchobj_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_tchobj_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_filter_tchobj_external_identifier);
		//		//Description
		//		System.Data.IDataParameter p_filter_tchobj_description = dataProvider.GetParameter("@filter_tchobj_description", 
		//			f.Description == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Description);
		//		p_filter_tchobj_description.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_tchobj_description.DbType = DbType.String;
		//		parameters.Add(p_filter_tchobj_description);
		//		//Placement
		//		System.Data.IDataParameter p_filter_tchobj_placement = dataProvider.GetParameter("@filter_tchobj_placement", 
		//			f.Placement == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Placement);
		//		p_filter_tchobj_placement.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_tchobj_placement.DbType = DbType.String;
		//		parameters.Add(p_filter_tchobj_placement);
		//		//PlannificationCenter
		//		System.Data.IDataParameter p_filter_tchobj_plannification_center = dataProvider.GetParameter("@filter_tchobj_plannification_center", 
		//			f.PlannificationCenter == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.PlannificationCenter);
		//		p_filter_tchobj_plannification_center.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_tchobj_plannification_center.DbType = DbType.String;
		//		parameters.Add(p_filter_tchobj_plannification_center);
		//		//Area
		//		System.Data.IDataParameter p_filter_tchobj_area = dataProvider.GetParameter("@filter_tchobj_area", 
		//			f.Area == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Area);
		//		p_filter_tchobj_area.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_tchobj_area.DbType = DbType.String;
		//		parameters.Add(p_filter_tchobj_area);
		//		//CostCenter
		//		System.Data.IDataParameter p_filter_tchobj_cost_center = dataProvider.GetParameter("@filter_tchobj_cost_center", 
		//			f.CostCenter == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.CostCenter);
		//		p_filter_tchobj_cost_center.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_tchobj_cost_center.DbType = DbType.String;
		//		parameters.Add(p_filter_tchobj_cost_center);
  //              return parameters;
  //          };

  //          Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.PM.TechnicalObject> mapper = (reader) =>
  //          {
		//		long longHelper = default(long);

		//		SOFTTEK.SCMS.Entity.PM.TechnicalObject value = new SOFTTEK.SCMS.Entity.PM.TechnicalObject{
		//			Name = reader["tchobj_name"] != null ? 
		//				reader["tchobj_name"].ToString() : string.Empty,
		//			TypeObject = reader["tchobj_type_object"] != null ? 
		//				reader["tchobj_type_object"].ToString() : string.Empty,
		//			Identifier = reader["tchobj_id_Pk"] != null ? 
		//				(long.TryParse(reader["tchobj_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
		//			ExternalIdentifier = reader["tchobj_external_identifier"] != null ? 
		//				reader["tchobj_external_identifier"].ToString() : string.Empty,
		//			Description = reader["tchobj_description"] != null ? 
		//				reader["tchobj_description"].ToString() : string.Empty,
		//			Placement = reader["tchobj_placement"] != null ? 
		//				reader["tchobj_placement"].ToString() : string.Empty,
		//			PlannificationCenter = reader["tchobj_plannification_center"] != null ? 
		//				reader["tchobj_plannification_center"].ToString() : string.Empty,
		//			Area = reader["tchobj_area"] != null ? 
		//				reader["tchobj_area"].ToString() : string.Empty,
		//			CostCenter = reader["tchobj_cost_center"] != null ? 
		//				reader["tchobj_cost_center"].ToString() : string.Empty,
		//		};
  //              return value;
  //          };

  //          dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
  //          List<SOFTTEK.SCMS.Entity.PM.TechnicalObject> result = dataProvider.ExecuteReaderWithFilter(SCMS.PM_SP_Select_TechnicalObject, filterDelegate, filter, mapper);
  //          return result;
		//}

		///// <summary>
  //      /// Inserts the instance of the provided TechnicalObject at the Data Context datasource  
  //      /// </summary>
  //      /// <param name="instance">Instance of TechnicalObject with the entity´s information to store</param>
  //      /// <returns>Instance of TechnicalObject with the aditional information provided by the Data Context datasource</returns>
		//public SOFTTEK.SCMS.Entity.PM.TechnicalObject InsertTechnicalObject(SOFTTEK.SCMS.Entity.PM.TechnicalObject instance){
		//	Func<SOFTTEK.SCMS.Entity.PM.TechnicalObject, List<System.Data.IDataParameter>> input = (i) =>
  //          {
  //              List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
			
		//		//Token
		//		System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
  //              p_tok.Direction = System.Data.ParameterDirection.Input;
  //              p_tok.DbType = System.Data.DbType.String;
  //              parameters.Add(p_tok);
				
		//		// Insert
		//		//Name
		//		System.Data.IDataParameter p_new_tchobj_name = dataProvider.GetParameter("@new_tchobj_name", 
		//			i.Name == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.Name);
		//		p_new_tchobj_name.Direction = System.Data.ParameterDirection.Input;
		//		p_new_tchobj_name.DbType = DbType.String;
		//		parameters.Add(p_new_tchobj_name);
		//		//TypeObject
		//		System.Data.IDataParameter p_new_tchobj_type_object = dataProvider.GetParameter("@new_tchobj_type_object", 
		//			i.TypeObject == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.TypeObject);
		//		p_new_tchobj_type_object.Direction = System.Data.ParameterDirection.Input;
		//		p_new_tchobj_type_object.DbType = DbType.String;
		//		parameters.Add(p_new_tchobj_type_object);
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_new_tchobj_external_identifier = dataProvider.GetParameter("@new_tchobj_external_identifier", 
		//			i.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.ExternalIdentifier);
		//		p_new_tchobj_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_new_tchobj_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_new_tchobj_external_identifier);
		//		//Description
		//		System.Data.IDataParameter p_new_tchobj_description = dataProvider.GetParameter("@new_tchobj_description", 
		//			i.Description == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.Description);
		//		p_new_tchobj_description.Direction = System.Data.ParameterDirection.Input;
		//		p_new_tchobj_description.DbType = DbType.String;
		//		parameters.Add(p_new_tchobj_description);
		//		//Placement
		//		System.Data.IDataParameter p_new_tchobj_placement = dataProvider.GetParameter("@new_tchobj_placement", 
		//			i.Placement == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.Placement);
		//		p_new_tchobj_placement.Direction = System.Data.ParameterDirection.Input;
		//		p_new_tchobj_placement.DbType = DbType.String;
		//		parameters.Add(p_new_tchobj_placement);
		//		//PlannificationCenter
		//		System.Data.IDataParameter p_new_tchobj_plannification_center = dataProvider.GetParameter("@new_tchobj_plannification_center", 
		//			i.PlannificationCenter == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.PlannificationCenter);
		//		p_new_tchobj_plannification_center.Direction = System.Data.ParameterDirection.Input;
		//		p_new_tchobj_plannification_center.DbType = DbType.String;
		//		parameters.Add(p_new_tchobj_plannification_center);
		//		//Area
		//		System.Data.IDataParameter p_new_tchobj_area = dataProvider.GetParameter("@new_tchobj_area", 
		//			i.Area == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.Area);
		//		p_new_tchobj_area.Direction = System.Data.ParameterDirection.Input;
		//		p_new_tchobj_area.DbType = DbType.String;
		//		parameters.Add(p_new_tchobj_area);
		//		//CostCenter
		//		System.Data.IDataParameter p_new_tchobj_cost_center = dataProvider.GetParameter("@new_tchobj_cost_center", 
		//			i.CostCenter == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.CostCenter);
		//		p_new_tchobj_cost_center.Direction = System.Data.ParameterDirection.Input;
		//		p_new_tchobj_cost_center.DbType = DbType.String;
		//		parameters.Add(p_new_tchobj_cost_center);

  //              return parameters;
  //          };

		//	Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.PM.TechnicalObject> mapper = (reader) =>
  //          {
		//		long longHelper = default(long);

		//		SOFTTEK.SCMS.Entity.PM.TechnicalObject value = new SOFTTEK.SCMS.Entity.PM.TechnicalObject{
		//			Name = reader["v_tchobj_name"] != null ? 
		//				reader["v_tchobj_name"].ToString() : string.Empty,
		//			TypeObject = reader["v_tchobj_type_object"] != null ? 
		//				reader["v_tchobj_type_object"].ToString() : string.Empty,
		//			Identifier = reader["v_tchobj_id_Pk"] != null ? 
		//				(long.TryParse(reader["v_tchobj_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
		//			ExternalIdentifier = reader["v_tchobj_external_identifier"] != null ? 
		//				reader["v_tchobj_external_identifier"].ToString() : string.Empty,
		//			Description = reader["v_tchobj_description"] != null ? 
		//				reader["v_tchobj_description"].ToString() : string.Empty,
		//			Placement = reader["v_tchobj_placement"] != null ? 
		//				reader["v_tchobj_placement"].ToString() : string.Empty,
		//			PlannificationCenter = reader["v_tchobj_plannification_center"] != null ? 
		//				reader["v_tchobj_plannification_center"].ToString() : string.Empty,
		//			Area = reader["v_tchobj_area"] != null ? 
		//				reader["v_tchobj_area"].ToString() : string.Empty,
		//			CostCenter = reader["v_tchobj_cost_center"] != null ? 
		//				reader["v_tchobj_cost_center"].ToString() : string.Empty,
		//		};
  //              return value;
  //          };


		//	dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
  //          SOFTTEK.SCMS.Entity.PM.TechnicalObject result = dataProvider.ExecuteForEntityWithFilter(SCMS.PM_SP_Insert_TechnicalObject, input, instance, mapper);
  //          return result;
		//}

		///// <summary>
  //      /// Update the entity's stored information with that provided by the instance of TechnicalObject at the Data Context datasource  
  //      /// </summary>
  //      /// <param name="instance">Instance of TechnicalObject with the entity´s information to update</param>
  //      /// <param name="filter">Object with the information required to filter the target stored entity´s information</param>
		///// <returns>Instance of TechnicalObject with the updated information provided by the Data Context datasource</returns>
		//public SOFTTEK.SCMS.Entity.PM.TechnicalObject UpdateTechnicalObject(SOFTTEK.SCMS.Entity.PM.TechnicalObject instance, SOFTTEK.SCMS.Entity.PM.TechnicalObject filter){
		//	Func<SOFTTEK.SCMS.Entity.PM.TechnicalObject, List<System.Data.IDataParameter>> filterDelegate = (f) =>
  //          {
  //              List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
			
		//		//Token
		//		System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
  //              p_tok.Direction = System.Data.ParameterDirection.Input;
  //              p_tok.DbType = System.Data.DbType.String;
  //              parameters.Add(p_tok);
				
		//		// Filter
		//		//Name
		//		System.Data.IDataParameter p_filter_tchobj_name = dataProvider.GetParameter("@filter_tchobj_name", 
		//			f.Name == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Name);
		//		p_filter_tchobj_name.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_tchobj_name.DbType = DbType.String;
		//		parameters.Add(p_filter_tchobj_name);
		//		//TypeObject
		//		System.Data.IDataParameter p_filter_tchobj_type_object = dataProvider.GetParameter("@filter_tchobj_type_object", 
		//			f.TypeObject == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.TypeObject);
		//		p_filter_tchobj_type_object.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_tchobj_type_object.DbType = DbType.String;
		//		parameters.Add(p_filter_tchobj_type_object);
		//		//Identifier
		//		System.Data.IDataParameter p_filter_tchobj_id_Pk = dataProvider.GetParameter("@filter_tchobj_id_Pk", 
		//			f.Identifier == default(System.Int64) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Identifier);
		//		p_filter_tchobj_id_Pk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_tchobj_id_Pk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_tchobj_id_Pk);
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_filter_tchobj_external_identifier = dataProvider.GetParameter("@filter_tchobj_external_identifier", 
		//			f.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.ExternalIdentifier);
		//		p_filter_tchobj_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_tchobj_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_filter_tchobj_external_identifier);
		//		//Description
		//		System.Data.IDataParameter p_filter_tchobj_description = dataProvider.GetParameter("@filter_tchobj_description", 
		//			f.Description == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Description);
		//		p_filter_tchobj_description.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_tchobj_description.DbType = DbType.String;
		//		parameters.Add(p_filter_tchobj_description);
		//		//Placement
		//		System.Data.IDataParameter p_filter_tchobj_placement = dataProvider.GetParameter("@filter_tchobj_placement", 
		//			f.Placement == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Placement);
		//		p_filter_tchobj_placement.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_tchobj_placement.DbType = DbType.String;
		//		parameters.Add(p_filter_tchobj_placement);
		//		//PlannificationCenter
		//		System.Data.IDataParameter p_filter_tchobj_plannification_center = dataProvider.GetParameter("@filter_tchobj_plannification_center", 
		//			f.PlannificationCenter == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.PlannificationCenter);
		//		p_filter_tchobj_plannification_center.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_tchobj_plannification_center.DbType = DbType.String;
		//		parameters.Add(p_filter_tchobj_plannification_center);
		//		//Area
		//		System.Data.IDataParameter p_filter_tchobj_area = dataProvider.GetParameter("@filter_tchobj_area", 
		//			f.Area == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Area);
		//		p_filter_tchobj_area.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_tchobj_area.DbType = DbType.String;
		//		parameters.Add(p_filter_tchobj_area);
		//		//CostCenter
		//		System.Data.IDataParameter p_filter_tchobj_cost_center = dataProvider.GetParameter("@filter_tchobj_cost_center", 
		//			f.CostCenter == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.CostCenter);
		//		p_filter_tchobj_cost_center.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_tchobj_cost_center.DbType = DbType.String;
		//		parameters.Add(p_filter_tchobj_cost_center);
		//		// Update
		//		//Name
		//		System.Data.IDataParameter p_new_tchobj_name = dataProvider.GetParameter("@new_tchobj_name", 
		//			f.Name == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.Name);
		//		p_new_tchobj_name.Direction = System.Data.ParameterDirection.Input;
		//		p_new_tchobj_name.DbType = DbType.String;
		//		parameters.Add(p_new_tchobj_name);
		//		//TypeObject
		//		System.Data.IDataParameter p_new_tchobj_type_object = dataProvider.GetParameter("@new_tchobj_type_object", 
		//			f.TypeObject == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.TypeObject);
		//		p_new_tchobj_type_object.Direction = System.Data.ParameterDirection.Input;
		//		p_new_tchobj_type_object.DbType = DbType.String;
		//		parameters.Add(p_new_tchobj_type_object);
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_new_tchobj_external_identifier = dataProvider.GetParameter("@new_tchobj_external_identifier", 
		//			f.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.ExternalIdentifier);
		//		p_new_tchobj_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_new_tchobj_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_new_tchobj_external_identifier);
		//		//Description
		//		System.Data.IDataParameter p_new_tchobj_description = dataProvider.GetParameter("@new_tchobj_description", 
		//			f.Description == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.Description);
		//		p_new_tchobj_description.Direction = System.Data.ParameterDirection.Input;
		//		p_new_tchobj_description.DbType = DbType.String;
		//		parameters.Add(p_new_tchobj_description);
		//		//Placement
		//		System.Data.IDataParameter p_new_tchobj_placement = dataProvider.GetParameter("@new_tchobj_placement", 
		//			f.Placement == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.Placement);
		//		p_new_tchobj_placement.Direction = System.Data.ParameterDirection.Input;
		//		p_new_tchobj_placement.DbType = DbType.String;
		//		parameters.Add(p_new_tchobj_placement);
		//		//PlannificationCenter
		//		System.Data.IDataParameter p_new_tchobj_plannification_center = dataProvider.GetParameter("@new_tchobj_plannification_center", 
		//			f.PlannificationCenter == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.PlannificationCenter);
		//		p_new_tchobj_plannification_center.Direction = System.Data.ParameterDirection.Input;
		//		p_new_tchobj_plannification_center.DbType = DbType.String;
		//		parameters.Add(p_new_tchobj_plannification_center);
		//		//Area
		//		System.Data.IDataParameter p_new_tchobj_area = dataProvider.GetParameter("@new_tchobj_area", 
		//			f.Area == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.Area);
		//		p_new_tchobj_area.Direction = System.Data.ParameterDirection.Input;
		//		p_new_tchobj_area.DbType = DbType.String;
		//		parameters.Add(p_new_tchobj_area);
		//		//CostCenter
		//		System.Data.IDataParameter p_new_tchobj_cost_center = dataProvider.GetParameter("@new_tchobj_cost_center", 
		//			f.CostCenter == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.CostCenter);
		//		p_new_tchobj_cost_center.Direction = System.Data.ParameterDirection.Input;
		//		p_new_tchobj_cost_center.DbType = DbType.String;
		//		parameters.Add(p_new_tchobj_cost_center);

  //              return parameters;
  //          };

		//	Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.PM.TechnicalObject> mapper = (reader) =>
  //          {
		//		long longHelper = default(long);

		//		SOFTTEK.SCMS.Entity.PM.TechnicalObject value = new SOFTTEK.SCMS.Entity.PM.TechnicalObject{
		//			Name = reader["v_tchobj_name"] != null ? 
		//				reader["v_tchobj_name"].ToString() : string.Empty,
		//			TypeObject = reader["v_tchobj_type_object"] != null ? 
		//				reader["v_tchobj_type_object"].ToString() : string.Empty,
		//			Identifier = reader["v_tchobj_id_Pk"] != null ? 
		//				(long.TryParse(reader["v_tchobj_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
		//			ExternalIdentifier = reader["v_tchobj_external_identifier"] != null ? 
		//				reader["v_tchobj_external_identifier"].ToString() : string.Empty,
		//			Description = reader["v_tchobj_description"] != null ? 
		//				reader["v_tchobj_description"].ToString() : string.Empty,
		//			Placement = reader["v_tchobj_placement"] != null ? 
		//				reader["v_tchobj_placement"].ToString() : string.Empty,
		//			PlannificationCenter = reader["v_tchobj_plannification_center"] != null ? 
		//				reader["v_tchobj_plannification_center"].ToString() : string.Empty,
		//			Area = reader["v_tchobj_area"] != null ? 
		//				reader["v_tchobj_area"].ToString() : string.Empty,
		//			CostCenter = reader["v_tchobj_cost_center"] != null ? 
		//				reader["v_tchobj_cost_center"].ToString() : string.Empty,
		//		};
  //              return value;
  //          };


		//	dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
  //          SOFTTEK.SCMS.Entity.PM.TechnicalObject result = dataProvider.ExecuteForEntityWithFilter(SCMS.PM_SP_Update_TechnicalObject, filterDelegate, filter, mapper);
  //          return result;
		//}
		//#endregion
		//#region MaintenancePlan
		
		///// <summary>
  //      /// Retrieves a list of MaintenancePlan instances that matches a given filter criteria by the MaintenancePlan object instance 
  //      /// </summary>
  //      /// <param name="filter">Object with the information required to filter the set of results</param>
  //      /// <returns>List of MaintenancePlan instances that matches the filter.</returns>
		//public List<SOFTTEK.SCMS.Entity.PM.MaintenancePlan> GetMaintenancePlans(SOFTTEK.SCMS.Entity.PM.MaintenancePlan filter)
		//{
		//	Func<SOFTTEK.SCMS.Entity.PM.MaintenancePlan, List<System.Data.IDataParameter>> filterDelegate = (f) =>
  //          {
  //              List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
		//		//Token
		//		System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
  //              p_tok.Direction = System.Data.ParameterDirection.Input;
  //              p_tok.DbType = System.Data.DbType.String;
  //              parameters.Add(p_tok);
		//		//Identifier
		//		System.Data.IDataParameter p_filter_mntpln_id_Pk = dataProvider.GetParameter("@filter_mntpln_id_Pk", 
		//			f.Identifier == default(System.Int64) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Identifier);
		//		p_filter_mntpln_id_Pk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_mntpln_id_Pk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_mntpln_id_Pk);
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_filter_mntpln_external_identifier = dataProvider.GetParameter("@filter_mntpln_external_identifier", 
		//			f.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.ExternalIdentifier);
		//		p_filter_mntpln_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_mntpln_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_filter_mntpln_external_identifier);
		//		//Description
		//		System.Data.IDataParameter p_filter_mntpln_description = dataProvider.GetParameter("@filter_mntpln_description", 
		//			f.Description == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Description);
		//		p_filter_mntpln_description.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_mntpln_description.DbType = DbType.String;
		//		parameters.Add(p_filter_mntpln_description);
		//		//Comments
		//		System.Data.IDataParameter p_filter_mntpln_comments = dataProvider.GetParameter("@filter_mntpln_comments", 
		//			f.Comments == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Comments);
		//		p_filter_mntpln_comments.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_mntpln_comments.DbType = DbType.String;
		//		parameters.Add(p_filter_mntpln_comments);
		//		//DeviceType
		//		System.Data.IDataParameter p_filter_mntpln_device_type = dataProvider.GetParameter("@filter_mntpln_device_type", 
		//			f.DeviceType == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.DeviceType);
		//		p_filter_mntpln_device_type.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_mntpln_device_type.DbType = DbType.String;
		//		parameters.Add(p_filter_mntpln_device_type);
		//		//WorkOrder
		//		System.Data.IDataParameter p_filter_mntpln_work_order = dataProvider.GetParameter("@filter_mntpln_work_order", 
		//			f.WorkOrder == default(System.Int64) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.WorkOrder);
		//		p_filter_mntpln_work_order.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_mntpln_work_order.DbType = DbType.Int64;
		//		parameters.Add(p_filter_mntpln_work_order);
  //              return parameters;
  //          };

  //          Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.PM.MaintenancePlan> mapper = (reader) =>
  //          {
		//		long longHelper = default(long);

		//		SOFTTEK.SCMS.Entity.PM.MaintenancePlan value = new SOFTTEK.SCMS.Entity.PM.MaintenancePlan{
		//			Identifier = reader["mntpln_id_Pk"] != null ? 
		//				(long.TryParse(reader["mntpln_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
		//			ExternalIdentifier = reader["mntpln_external_identifier"] != null ? 
		//				reader["mntpln_external_identifier"].ToString() : string.Empty,
		//			Description = reader["mntpln_description"] != null ? 
		//				reader["mntpln_description"].ToString() : string.Empty,
		//			Comments = reader["mntpln_comments"] != null ? 
		//				reader["mntpln_comments"].ToString() : string.Empty,
		//			DeviceType = reader["mntpln_device_type"] != null ? 
		//				reader["mntpln_device_type"].ToString() : string.Empty,
		//			WorkOrder = reader["mntpln_work_order"] != null ? 
		//				(long.TryParse(reader["mntpln_work_order"].ToString(), out longHelper) ? longHelper : -1) : -1,
		//		};
  //              return value;
  //          };

  //          dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
  //          List<SOFTTEK.SCMS.Entity.PM.MaintenancePlan> result = dataProvider.ExecuteReaderWithFilter(SCMS.PM_SP_Select_MaintenancePlan, filterDelegate, filter, mapper);
  //          return result;
		//}

		///// <summary>
  //      /// Inserts the instance of the provided MaintenancePlan at the Data Context datasource  
  //      /// </summary>
  //      /// <param name="instance">Instance of MaintenancePlan with the entity´s information to store</param>
  //      /// <returns>Instance of MaintenancePlan with the aditional information provided by the Data Context datasource</returns>
		//public SOFTTEK.SCMS.Entity.PM.MaintenancePlan InsertMaintenancePlan(SOFTTEK.SCMS.Entity.PM.MaintenancePlan instance){
		//	Func<SOFTTEK.SCMS.Entity.PM.MaintenancePlan, List<System.Data.IDataParameter>> input = (i) =>
  //          {
  //              List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
			
		//		//Token
		//		System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
  //              p_tok.Direction = System.Data.ParameterDirection.Input;
  //              p_tok.DbType = System.Data.DbType.String;
  //              parameters.Add(p_tok);
				
		//		// Insert
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_new_mntpln_external_identifier = dataProvider.GetParameter("@new_mntpln_external_identifier", 
		//			i.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.ExternalIdentifier);
		//		p_new_mntpln_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_new_mntpln_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_new_mntpln_external_identifier);
		//		//Description
		//		System.Data.IDataParameter p_new_mntpln_description = dataProvider.GetParameter("@new_mntpln_description", 
		//			i.Description == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.Description);
		//		p_new_mntpln_description.Direction = System.Data.ParameterDirection.Input;
		//		p_new_mntpln_description.DbType = DbType.String;
		//		parameters.Add(p_new_mntpln_description);
		//		//Comments
		//		System.Data.IDataParameter p_new_mntpln_comments = dataProvider.GetParameter("@new_mntpln_comments", 
		//			i.Comments == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.Comments);
		//		p_new_mntpln_comments.Direction = System.Data.ParameterDirection.Input;
		//		p_new_mntpln_comments.DbType = DbType.String;
		//		parameters.Add(p_new_mntpln_comments);
		//		//DeviceType
		//		System.Data.IDataParameter p_new_mntpln_device_type = dataProvider.GetParameter("@new_mntpln_device_type", 
		//			i.DeviceType == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.DeviceType);
		//		p_new_mntpln_device_type.Direction = System.Data.ParameterDirection.Input;
		//		p_new_mntpln_device_type.DbType = DbType.String;
		//		parameters.Add(p_new_mntpln_device_type);
		//		//WorkOrder
		//		System.Data.IDataParameter p_new_mntpln_work_order = dataProvider.GetParameter("@new_mntpln_work_order", 
		//			i.WorkOrder == default(System.Int64) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.WorkOrder);
		//		p_new_mntpln_work_order.Direction = System.Data.ParameterDirection.Input;
		//		p_new_mntpln_work_order.DbType = DbType.Int64;
		//		parameters.Add(p_new_mntpln_work_order);

  //              return parameters;
  //          };

		//	Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.PM.MaintenancePlan> mapper = (reader) =>
  //          {
		//		long longHelper = default(long);

		//		SOFTTEK.SCMS.Entity.PM.MaintenancePlan value = new SOFTTEK.SCMS.Entity.PM.MaintenancePlan{
		//			Identifier = reader["v_mntpln_id_Pk"] != null ? 
		//				(long.TryParse(reader["v_mntpln_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
		//			ExternalIdentifier = reader["v_mntpln_external_identifier"] != null ? 
		//				reader["v_mntpln_external_identifier"].ToString() : string.Empty,
		//			Description = reader["v_mntpln_description"] != null ? 
		//				reader["v_mntpln_description"].ToString() : string.Empty,
		//			Comments = reader["v_mntpln_comments"] != null ? 
		//				reader["v_mntpln_comments"].ToString() : string.Empty,
		//			DeviceType = reader["v_mntpln_device_type"] != null ? 
		//				reader["v_mntpln_device_type"].ToString() : string.Empty,
		//			WorkOrder = reader["v_mntpln_work_order"] != null ? 
		//				(long.TryParse(reader["v_mntpln_work_order"].ToString(), out longHelper) ? longHelper : -1) : -1,
		//		};
  //              return value;
  //          };


		//	dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
  //          SOFTTEK.SCMS.Entity.PM.MaintenancePlan result = dataProvider.ExecuteForEntityWithFilter(SCMS.PM_SP_Insert_MaintenancePlan, input, instance, mapper);
  //          return result;
		//}

		///// <summary>
  //      /// Update the entity's stored information with that provided by the instance of MaintenancePlan at the Data Context datasource  
  //      /// </summary>
  //      /// <param name="instance">Instance of MaintenancePlan with the entity´s information to update</param>
  //      /// <param name="filter">Object with the information required to filter the target stored entity´s information</param>
		///// <returns>Instance of MaintenancePlan with the updated information provided by the Data Context datasource</returns>
		//public SOFTTEK.SCMS.Entity.PM.MaintenancePlan UpdateMaintenancePlan(SOFTTEK.SCMS.Entity.PM.MaintenancePlan instance, SOFTTEK.SCMS.Entity.PM.MaintenancePlan filter){
		//	Func<SOFTTEK.SCMS.Entity.PM.MaintenancePlan, List<System.Data.IDataParameter>> filterDelegate = (f) =>
  //          {
  //              List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
			
		//		//Token
		//		System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
  //              p_tok.Direction = System.Data.ParameterDirection.Input;
  //              p_tok.DbType = System.Data.DbType.String;
  //              parameters.Add(p_tok);
				
		//		// Filter
		//		//Identifier
		//		System.Data.IDataParameter p_filter_mntpln_id_Pk = dataProvider.GetParameter("@filter_mntpln_id_Pk", 
		//			f.Identifier == default(System.Int64) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Identifier);
		//		p_filter_mntpln_id_Pk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_mntpln_id_Pk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_mntpln_id_Pk);
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_filter_mntpln_external_identifier = dataProvider.GetParameter("@filter_mntpln_external_identifier", 
		//			f.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.ExternalIdentifier);
		//		p_filter_mntpln_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_mntpln_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_filter_mntpln_external_identifier);
		//		//Description
		//		System.Data.IDataParameter p_filter_mntpln_description = dataProvider.GetParameter("@filter_mntpln_description", 
		//			f.Description == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Description);
		//		p_filter_mntpln_description.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_mntpln_description.DbType = DbType.String;
		//		parameters.Add(p_filter_mntpln_description);
		//		//Comments
		//		System.Data.IDataParameter p_filter_mntpln_comments = dataProvider.GetParameter("@filter_mntpln_comments", 
		//			f.Comments == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Comments);
		//		p_filter_mntpln_comments.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_mntpln_comments.DbType = DbType.String;
		//		parameters.Add(p_filter_mntpln_comments);
		//		//DeviceType
		//		System.Data.IDataParameter p_filter_mntpln_device_type = dataProvider.GetParameter("@filter_mntpln_device_type", 
		//			f.DeviceType == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.DeviceType);
		//		p_filter_mntpln_device_type.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_mntpln_device_type.DbType = DbType.String;
		//		parameters.Add(p_filter_mntpln_device_type);
		//		//WorkOrder
		//		System.Data.IDataParameter p_filter_mntpln_work_order = dataProvider.GetParameter("@filter_mntpln_work_order", 
		//			f.WorkOrder == default(System.Int64) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.WorkOrder);
		//		p_filter_mntpln_work_order.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_mntpln_work_order.DbType = DbType.Int64;
		//		parameters.Add(p_filter_mntpln_work_order);
		//		// Update
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_new_mntpln_external_identifier = dataProvider.GetParameter("@new_mntpln_external_identifier", 
		//			f.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.ExternalIdentifier);
		//		p_new_mntpln_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_new_mntpln_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_new_mntpln_external_identifier);
		//		//Description
		//		System.Data.IDataParameter p_new_mntpln_description = dataProvider.GetParameter("@new_mntpln_description", 
		//			f.Description == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.Description);
		//		p_new_mntpln_description.Direction = System.Data.ParameterDirection.Input;
		//		p_new_mntpln_description.DbType = DbType.String;
		//		parameters.Add(p_new_mntpln_description);
		//		//Comments
		//		System.Data.IDataParameter p_new_mntpln_comments = dataProvider.GetParameter("@new_mntpln_comments", 
		//			f.Comments == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.Comments);
		//		p_new_mntpln_comments.Direction = System.Data.ParameterDirection.Input;
		//		p_new_mntpln_comments.DbType = DbType.String;
		//		parameters.Add(p_new_mntpln_comments);
		//		//DeviceType
		//		System.Data.IDataParameter p_new_mntpln_device_type = dataProvider.GetParameter("@new_mntpln_device_type", 
		//			f.DeviceType == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.DeviceType);
		//		p_new_mntpln_device_type.Direction = System.Data.ParameterDirection.Input;
		//		p_new_mntpln_device_type.DbType = DbType.String;
		//		parameters.Add(p_new_mntpln_device_type);
		//		//WorkOrder
		//		System.Data.IDataParameter p_new_mntpln_work_order = dataProvider.GetParameter("@new_mntpln_work_order", 
		//			f.WorkOrder == default(System.Int64) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.WorkOrder);
		//		p_new_mntpln_work_order.Direction = System.Data.ParameterDirection.Input;
		//		p_new_mntpln_work_order.DbType = DbType.Int64;
		//		parameters.Add(p_new_mntpln_work_order);

  //              return parameters;
  //          };

		//	Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.PM.MaintenancePlan> mapper = (reader) =>
  //          {
		//		long longHelper = default(long);

		//		SOFTTEK.SCMS.Entity.PM.MaintenancePlan value = new SOFTTEK.SCMS.Entity.PM.MaintenancePlan{
		//			Identifier = reader["v_mntpln_id_Pk"] != null ? 
		//				(long.TryParse(reader["v_mntpln_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
		//			ExternalIdentifier = reader["v_mntpln_external_identifier"] != null ? 
		//				reader["v_mntpln_external_identifier"].ToString() : string.Empty,
		//			Description = reader["v_mntpln_description"] != null ? 
		//				reader["v_mntpln_description"].ToString() : string.Empty,
		//			Comments = reader["v_mntpln_comments"] != null ? 
		//				reader["v_mntpln_comments"].ToString() : string.Empty,
		//			DeviceType = reader["v_mntpln_device_type"] != null ? 
		//				reader["v_mntpln_device_type"].ToString() : string.Empty,
		//			WorkOrder = reader["v_mntpln_work_order"] != null ? 
		//				(long.TryParse(reader["v_mntpln_work_order"].ToString(), out longHelper) ? longHelper : -1) : -1,
		//		};
  //              return value;
  //          };


		//	dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
  //          SOFTTEK.SCMS.Entity.PM.MaintenancePlan result = dataProvider.ExecuteForEntityWithFilter(SCMS.PM_SP_Update_MaintenancePlan, filterDelegate, filter, mapper);
  //          return result;
		//}
		//#endregion
		//#region Material
		
		///// <summary>
  //      /// Retrieves a list of Material instances that matches a given filter criteria by the Material object instance 
  //      /// </summary>
  //      /// <param name="filter">Object with the information required to filter the set of results</param>
  //      /// <returns>List of Material instances that matches the filter.</returns>
		//public List<SOFTTEK.SCMS.Entity.PM.Material> GetMaterials(SOFTTEK.SCMS.Entity.PM.Material filter)
		//{
		//	Func<SOFTTEK.SCMS.Entity.PM.Material, List<System.Data.IDataParameter>> filterDelegate = (f) =>
  //          {
  //              List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
		//		//Token
		//		System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
  //              p_tok.Direction = System.Data.ParameterDirection.Input;
  //              p_tok.DbType = System.Data.DbType.String;
  //              parameters.Add(p_tok);
		//		//Identifier
		//		System.Data.IDataParameter p_filter_mtr_id_Pk = dataProvider.GetParameter("@filter_mtr_id_Pk", 
		//			f.Identifier == default(System.Int64) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Identifier);
		//		p_filter_mtr_id_Pk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_mtr_id_Pk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_mtr_id_Pk);
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_filter_mtr_external_identifier = dataProvider.GetParameter("@filter_mtr_external_identifier", 
		//			f.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.ExternalIdentifier);
		//		p_filter_mtr_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_mtr_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_filter_mtr_external_identifier);
		//		//Name
		//		System.Data.IDataParameter p_filter_mtr_name = dataProvider.GetParameter("@filter_mtr_name", 
		//			f.Name == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Name);
		//		p_filter_mtr_name.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_mtr_name.DbType = DbType.String;
		//		parameters.Add(p_filter_mtr_name);
		//		//Description
		//		System.Data.IDataParameter p_filter_mtr_description = dataProvider.GetParameter("@filter_mtr_description", 
		//			f.Description == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Description);
		//		p_filter_mtr_description.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_mtr_description.DbType = DbType.String;
		//		parameters.Add(p_filter_mtr_description);
		//		//Class
		//		System.Data.IDataParameter p_filter_mtr_class = dataProvider.GetParameter("@filter_mtr_class", 
		//			f.Class == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Class);
		//		p_filter_mtr_class.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_mtr_class.DbType = DbType.String;
		//		parameters.Add(p_filter_mtr_class);
		//		//Task
		//		System.Data.IDataParameter p_filter_mtr_task_Fk = dataProvider.GetParameter("@filter_mtr_task_Fk", 
		//			f.Task == default(SOFTTEK.SCMS.Entity.PM.Task) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Task.Identifier);
		//		p_filter_mtr_task_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_mtr_task_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_mtr_task_Fk);
		//		//MaterialParameter
		//		System.Data.IDataParameter p_filter_mtr_material_parameter = dataProvider.GetParameter("@filter_mtr_material_parameter", 
		//			f.MaterialParameter == default(System.Int64) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.MaterialParameter);
		//		p_filter_mtr_material_parameter.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_mtr_material_parameter.DbType = DbType.Int64;
		//		parameters.Add(p_filter_mtr_material_parameter);
  //              return parameters;
  //          };

  //          Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.PM.Material> mapper = (reader) =>
  //          {
		//		long longHelper = default(long);
		//		double doubleHelper = default(double);

		//		SOFTTEK.SCMS.Entity.PM.Material value = new SOFTTEK.SCMS.Entity.PM.Material{
		//			Identifier = reader["mtr_id_Pk"] != null ? 
		//				(long.TryParse(reader["mtr_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
		//			ExternalIdentifier = reader["mtr_external_identifier"] != null ? 
		//				reader["mtr_external_identifier"].ToString() : string.Empty,
		//			Name = reader["mtr_name"] != null ? 
		//				reader["mtr_name"].ToString() : string.Empty,
		//			Description = reader["mtr_description"] != null ? 
		//				reader["mtr_description"].ToString() : string.Empty,
		//			Class = reader["mtr_class"] != null ? 
		//				reader["mtr_class"].ToString() : string.Empty,
		//			Stock = reader["mtr_stock"] != null ? 
		//				(double.TryParse(reader["mtr_stock"].ToString(), out doubleHelper) ? doubleHelper : 0.0) : 0.0,
		//			Task = reader["mtr_task_Fk"] != null ? 
		//				new SOFTTEK.SCMS.Entity.PM.Task { Identifier = long.TryParse(reader["mtr_task_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null, 
		//			MaterialParameter = reader["mtr_material_parameter"] != null ? 
		//				(long.TryParse(reader["mtr_material_parameter"].ToString(), out longHelper) ? longHelper : -1) : -1,
		//			Observations = reader["mtr_observations"] != null ? 
		//				reader["mtr_observations"].ToString() : string.Empty,
		//		};
  //              return value;
  //          };

  //          dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
  //          List<SOFTTEK.SCMS.Entity.PM.Material> result = dataProvider.ExecuteReaderWithFilter(SCMS.PM_SP_Select_Material, filterDelegate, filter, mapper);
  //          return result;
		//}

		///// <summary>
  //      /// Inserts the instance of the provided Material at the Data Context datasource  
  //      /// </summary>
  //      /// <param name="instance">Instance of Material with the entity´s information to store</param>
  //      /// <returns>Instance of Material with the aditional information provided by the Data Context datasource</returns>
		//public SOFTTEK.SCMS.Entity.PM.Material InsertMaterial(SOFTTEK.SCMS.Entity.PM.Material instance){
		//	Func<SOFTTEK.SCMS.Entity.PM.Material, List<System.Data.IDataParameter>> input = (i) =>
  //          {
  //              List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
			
		//		//Token
		//		System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
  //              p_tok.Direction = System.Data.ParameterDirection.Input;
  //              p_tok.DbType = System.Data.DbType.String;
  //              parameters.Add(p_tok);
				
		//		// Insert
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_new_mtr_external_identifier = dataProvider.GetParameter("@new_mtr_external_identifier", 
		//			i.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.ExternalIdentifier);
		//		p_new_mtr_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_new_mtr_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_new_mtr_external_identifier);
		//		//Name
		//		System.Data.IDataParameter p_new_mtr_name = dataProvider.GetParameter("@new_mtr_name", 
		//			i.Name == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.Name);
		//		p_new_mtr_name.Direction = System.Data.ParameterDirection.Input;
		//		p_new_mtr_name.DbType = DbType.String;
		//		parameters.Add(p_new_mtr_name);
		//		//Description
		//		System.Data.IDataParameter p_new_mtr_description = dataProvider.GetParameter("@new_mtr_description", 
		//			i.Description == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.Description);
		//		p_new_mtr_description.Direction = System.Data.ParameterDirection.Input;
		//		p_new_mtr_description.DbType = DbType.String;
		//		parameters.Add(p_new_mtr_description);
		//		//Class
		//		System.Data.IDataParameter p_new_mtr_class = dataProvider.GetParameter("@new_mtr_class", 
		//			i.Class == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.Class);
		//		p_new_mtr_class.Direction = System.Data.ParameterDirection.Input;
		//		p_new_mtr_class.DbType = DbType.String;
		//		parameters.Add(p_new_mtr_class);
		//		//Stock
		//		System.Data.IDataParameter p_new_mtr_stock = dataProvider.GetParameter("@new_mtr_stock", 
		//			i.Stock == default(System.Double) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.Stock);
		//		p_new_mtr_stock.Direction = System.Data.ParameterDirection.Input;
		//		p_new_mtr_stock.DbType = DbType.Double;
		//		parameters.Add(p_new_mtr_stock);
		//		//Task
		//		System.Data.IDataParameter p_new_mtr_task_Fk = dataProvider.GetParameter("@new_mtr_task_Fk", 
		//			i.Task == default(SOFTTEK.SCMS.Entity.PM.Task) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.Task.Identifier);
		//		p_new_mtr_task_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_new_mtr_task_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_new_mtr_task_Fk);
		//		//MaterialParameter
		//		System.Data.IDataParameter p_new_mtr_material_parameter = dataProvider.GetParameter("@new_mtr_material_parameter", 
		//			i.MaterialParameter == default(System.Int64) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.MaterialParameter);
		//		p_new_mtr_material_parameter.Direction = System.Data.ParameterDirection.Input;
		//		p_new_mtr_material_parameter.DbType = DbType.Int64;
		//		parameters.Add(p_new_mtr_material_parameter);
		//		//Observations
		//		System.Data.IDataParameter p_new_mtr_observations = dataProvider.GetParameter("@new_mtr_observations", 
		//			i.Observations == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.Observations);
		//		p_new_mtr_observations.Direction = System.Data.ParameterDirection.Input;
		//		p_new_mtr_observations.DbType = DbType.String;
		//		parameters.Add(p_new_mtr_observations);

  //              return parameters;
  //          };

		//	Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.PM.Material> mapper = (reader) =>
  //          {
		//		long longHelper = default(long);
		//		double doubleHelper = default(double);

		//		SOFTTEK.SCMS.Entity.PM.Material value = new SOFTTEK.SCMS.Entity.PM.Material{
		//			Identifier = reader["v_mtr_id_Pk"] != null ? 
		//				(long.TryParse(reader["v_mtr_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
		//			ExternalIdentifier = reader["v_mtr_external_identifier"] != null ? 
		//				reader["v_mtr_external_identifier"].ToString() : string.Empty,
		//			Name = reader["v_mtr_name"] != null ? 
		//				reader["v_mtr_name"].ToString() : string.Empty,
		//			Description = reader["v_mtr_description"] != null ? 
		//				reader["v_mtr_description"].ToString() : string.Empty,
		//			Class = reader["v_mtr_class"] != null ? 
		//				reader["v_mtr_class"].ToString() : string.Empty,
		//			Stock = reader["v_mtr_stock"] != null ? 
		//				(double.TryParse(reader["v_mtr_stock"].ToString(), out doubleHelper) ? doubleHelper : 0.0) : 0.0,
		//			Task = reader["v_mtr_task_Fk"] != null ? 
		//				new SOFTTEK.SCMS.Entity.PM.Task { Identifier = long.TryParse(reader["v_mtr_task_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null, 
		//			MaterialParameter = reader["v_mtr_material_parameter"] != null ? 
		//				(long.TryParse(reader["v_mtr_material_parameter"].ToString(), out longHelper) ? longHelper : -1) : -1,
		//			Observations = reader["v_mtr_observations"] != null ? 
		//				reader["v_mtr_observations"].ToString() : string.Empty,
		//		};
  //              return value;
  //          };


		//	dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
  //          SOFTTEK.SCMS.Entity.PM.Material result = dataProvider.ExecuteForEntityWithFilter(SCMS.PM_SP_Insert_Material, input, instance, mapper);
  //          return result;
		//}

		///// <summary>
  //      /// Update the entity's stored information with that provided by the instance of Material at the Data Context datasource  
  //      /// </summary>
  //      /// <param name="instance">Instance of Material with the entity´s information to update</param>
  //      /// <param name="filter">Object with the information required to filter the target stored entity´s information</param>
		///// <returns>Instance of Material with the updated information provided by the Data Context datasource</returns>
		//public SOFTTEK.SCMS.Entity.PM.Material UpdateMaterial(SOFTTEK.SCMS.Entity.PM.Material instance, SOFTTEK.SCMS.Entity.PM.Material filter){
		//	Func<SOFTTEK.SCMS.Entity.PM.Material, List<System.Data.IDataParameter>> filterDelegate = (f) =>
  //          {
  //              List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
			
		//		//Token
		//		System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
  //              p_tok.Direction = System.Data.ParameterDirection.Input;
  //              p_tok.DbType = System.Data.DbType.String;
  //              parameters.Add(p_tok);
				
		//		// Filter
		//		//Identifier
		//		System.Data.IDataParameter p_filter_mtr_id_Pk = dataProvider.GetParameter("@filter_mtr_id_Pk", 
		//			f.Identifier == default(System.Int64) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Identifier);
		//		p_filter_mtr_id_Pk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_mtr_id_Pk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_mtr_id_Pk);
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_filter_mtr_external_identifier = dataProvider.GetParameter("@filter_mtr_external_identifier", 
		//			f.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.ExternalIdentifier);
		//		p_filter_mtr_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_mtr_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_filter_mtr_external_identifier);
		//		//Name
		//		System.Data.IDataParameter p_filter_mtr_name = dataProvider.GetParameter("@filter_mtr_name", 
		//			f.Name == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Name);
		//		p_filter_mtr_name.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_mtr_name.DbType = DbType.String;
		//		parameters.Add(p_filter_mtr_name);
		//		//Description
		//		System.Data.IDataParameter p_filter_mtr_description = dataProvider.GetParameter("@filter_mtr_description", 
		//			f.Description == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Description);
		//		p_filter_mtr_description.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_mtr_description.DbType = DbType.String;
		//		parameters.Add(p_filter_mtr_description);
		//		//Class
		//		System.Data.IDataParameter p_filter_mtr_class = dataProvider.GetParameter("@filter_mtr_class", 
		//			f.Class == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Class);
		//		p_filter_mtr_class.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_mtr_class.DbType = DbType.String;
		//		parameters.Add(p_filter_mtr_class);
		//		//Task
		//		System.Data.IDataParameter p_filter_mtr_task_Fk = dataProvider.GetParameter("@filter_mtr_task_Fk", 
		//			f.Task == default(SOFTTEK.SCMS.Entity.PM.Task) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Task.Identifier);
		//		p_filter_mtr_task_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_mtr_task_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_mtr_task_Fk);
		//		//MaterialParameter
		//		System.Data.IDataParameter p_filter_mtr_material_parameter = dataProvider.GetParameter("@filter_mtr_material_parameter", 
		//			f.MaterialParameter == default(System.Int64) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.MaterialParameter);
		//		p_filter_mtr_material_parameter.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_mtr_material_parameter.DbType = DbType.Int64;
		//		parameters.Add(p_filter_mtr_material_parameter);
		//		// Update
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_new_mtr_external_identifier = dataProvider.GetParameter("@new_mtr_external_identifier", 
		//			f.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.ExternalIdentifier);
		//		p_new_mtr_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_new_mtr_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_new_mtr_external_identifier);
		//		//Name
		//		System.Data.IDataParameter p_new_mtr_name = dataProvider.GetParameter("@new_mtr_name", 
		//			f.Name == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.Name);
		//		p_new_mtr_name.Direction = System.Data.ParameterDirection.Input;
		//		p_new_mtr_name.DbType = DbType.String;
		//		parameters.Add(p_new_mtr_name);
		//		//Description
		//		System.Data.IDataParameter p_new_mtr_description = dataProvider.GetParameter("@new_mtr_description", 
		//			f.Description == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.Description);
		//		p_new_mtr_description.Direction = System.Data.ParameterDirection.Input;
		//		p_new_mtr_description.DbType = DbType.String;
		//		parameters.Add(p_new_mtr_description);
		//		//Class
		//		System.Data.IDataParameter p_new_mtr_class = dataProvider.GetParameter("@new_mtr_class", 
		//			f.Class == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.Class);
		//		p_new_mtr_class.Direction = System.Data.ParameterDirection.Input;
		//		p_new_mtr_class.DbType = DbType.String;
		//		parameters.Add(p_new_mtr_class);
		//		//Stock
		//		System.Data.IDataParameter p_new_mtr_stock = dataProvider.GetParameter("@new_mtr_stock", 
		//			f.Stock == default(System.Double) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.Stock);
		//		p_new_mtr_stock.Direction = System.Data.ParameterDirection.Input;
		//		p_new_mtr_stock.DbType = DbType.Double;
		//		parameters.Add(p_new_mtr_stock);
		//		//Task
		//		System.Data.IDataParameter p_new_mtr_task_Fk = dataProvider.GetParameter("@new_mtr_task_Fk", 
		//			instance.Task == default(SOFTTEK.SCMS.Entity.PM.Task) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.Task.Identifier);
		//		p_new_mtr_task_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_new_mtr_task_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_new_mtr_task_Fk);
		//		//MaterialParameter
		//		System.Data.IDataParameter p_new_mtr_material_parameter = dataProvider.GetParameter("@new_mtr_material_parameter", 
		//			f.MaterialParameter == default(System.Int64) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.MaterialParameter);
		//		p_new_mtr_material_parameter.Direction = System.Data.ParameterDirection.Input;
		//		p_new_mtr_material_parameter.DbType = DbType.Int64;
		//		parameters.Add(p_new_mtr_material_parameter);
		//		//Observations
		//		System.Data.IDataParameter p_new_mtr_observations = dataProvider.GetParameter("@new_mtr_observations", 
		//			f.Observations == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.Observations);
		//		p_new_mtr_observations.Direction = System.Data.ParameterDirection.Input;
		//		p_new_mtr_observations.DbType = DbType.String;
		//		parameters.Add(p_new_mtr_observations);

  //              return parameters;
  //          };

		//	Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.PM.Material> mapper = (reader) =>
  //          {
		//		long longHelper = default(long);
		//		double doubleHelper = default(double);

		//		SOFTTEK.SCMS.Entity.PM.Material value = new SOFTTEK.SCMS.Entity.PM.Material{
		//			Identifier = reader["v_mtr_id_Pk"] != null ? 
		//				(long.TryParse(reader["v_mtr_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
		//			ExternalIdentifier = reader["v_mtr_external_identifier"] != null ? 
		//				reader["v_mtr_external_identifier"].ToString() : string.Empty,
		//			Name = reader["v_mtr_name"] != null ? 
		//				reader["v_mtr_name"].ToString() : string.Empty,
		//			Description = reader["v_mtr_description"] != null ? 
		//				reader["v_mtr_description"].ToString() : string.Empty,
		//			Class = reader["v_mtr_class"] != null ? 
		//				reader["v_mtr_class"].ToString() : string.Empty,
		//			Stock = reader["v_mtr_stock"] != null ? 
		//				(double.TryParse(reader["v_mtr_stock"].ToString(), out doubleHelper) ? doubleHelper : 0.0) : 0.0,
		//			Task = reader["v_mtr_task_Fk"] != null ? 
		//				new SOFTTEK.SCMS.Entity.PM.Task { Identifier = long.TryParse(reader["v_mtr_task_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null, 
		//			MaterialParameter = reader["v_mtr_material_parameter"] != null ? 
		//				(long.TryParse(reader["v_mtr_material_parameter"].ToString(), out longHelper) ? longHelper : -1) : -1,
		//			Observations = reader["v_mtr_observations"] != null ? 
		//				reader["v_mtr_observations"].ToString() : string.Empty,
		//		};
  //              return value;
  //          };


		//	dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
  //          SOFTTEK.SCMS.Entity.PM.Material result = dataProvider.ExecuteForEntityWithFilter(SCMS.PM_SP_Update_Material, filterDelegate, filter, mapper);
  //          return result;
		//}
		//#endregion
		//#region Task
		
		///// <summary>
  //      /// Retrieves a list of Task instances that matches a given filter criteria by the Task object instance 
  //      /// </summary>
  //      /// <param name="filter">Object with the information required to filter the set of results</param>
  //      /// <returns>List of Task instances that matches the filter.</returns>
		//public List<SOFTTEK.SCMS.Entity.PM.Task> GetTasks(SOFTTEK.SCMS.Entity.PM.Task filter)
		//{
		//	Func<SOFTTEK.SCMS.Entity.PM.Task, List<System.Data.IDataParameter>> filterDelegate = (f) =>
  //          {
  //              List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
		//		//Token
		//		System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
  //              p_tok.Direction = System.Data.ParameterDirection.Input;
  //              p_tok.DbType = System.Data.DbType.String;
  //              parameters.Add(p_tok);
		//		//Identifier
		//		System.Data.IDataParameter p_filter_tsk_id_Pk = dataProvider.GetParameter("@filter_tsk_id_Pk", 
		//			f.Identifier == default(System.Int64) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Identifier);
		//		p_filter_tsk_id_Pk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_tsk_id_Pk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_tsk_id_Pk);
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_filter_tsk_external_identifier = dataProvider.GetParameter("@filter_tsk_external_identifier", 
		//			f.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.ExternalIdentifier);
		//		p_filter_tsk_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_tsk_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_filter_tsk_external_identifier);
		//		//Name
		//		System.Data.IDataParameter p_filter_tsk_name = dataProvider.GetParameter("@filter_tsk_name", 
		//			f.Name == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Name);
		//		p_filter_tsk_name.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_tsk_name.DbType = DbType.String;
		//		parameters.Add(p_filter_tsk_name);
		//		//Description
		//		System.Data.IDataParameter p_filter_tsk_description = dataProvider.GetParameter("@filter_tsk_description", 
		//			f.Description == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Description);
		//		p_filter_tsk_description.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_tsk_description.DbType = DbType.String;
		//		parameters.Add(p_filter_tsk_description);
		//		//Performer
		//		System.Data.IDataParameter p_filter_tsk_performer = dataProvider.GetParameter("@filter_tsk_performer", 
		//			f.Performer == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Performer);
		//		p_filter_tsk_performer.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_tsk_performer.DbType = DbType.String;
		//		parameters.Add(p_filter_tsk_performer);
		//		//Status
		//		System.Data.IDataParameter p_filter_tsk_status = dataProvider.GetParameter("@filter_tsk_status", 
		//			f.Status == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Status);
		//		p_filter_tsk_status.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_tsk_status.DbType = DbType.String;
		//		parameters.Add(p_filter_tsk_status);
		//		//Comments
		//		System.Data.IDataParameter p_filter_tsk_comments = dataProvider.GetParameter("@filter_tsk_comments", 
		//			f.Comments == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Comments);
		//		p_filter_tsk_comments.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_tsk_comments.DbType = DbType.String;
		//		parameters.Add(p_filter_tsk_comments);
		//		//QuantityCapacity
		//		System.Data.IDataParameter p_filter_tsk_quantity_capacity = dataProvider.GetParameter("@filter_tsk_quantity_capacity", 
		//			f.QuantityCapacity == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.QuantityCapacity);
		//		p_filter_tsk_quantity_capacity.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_tsk_quantity_capacity.DbType = DbType.String;
		//		parameters.Add(p_filter_tsk_quantity_capacity);
		//		//DurationOperation
		//		System.Data.IDataParameter p_filter_tsk_duration_operation = dataProvider.GetParameter("@filter_tsk_duration_operation", 
		//			f.DurationOperation == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.DurationOperation);
		//		p_filter_tsk_duration_operation.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_tsk_duration_operation.DbType = DbType.String;
		//		parameters.Add(p_filter_tsk_duration_operation);
		//		//Plan
		//		System.Data.IDataParameter p_filter_tsk_plan_Fk = dataProvider.GetParameter("@filter_tsk_plan_Fk", 
		//			f.Plan == default(SOFTTEK.SCMS.Entity.PM.MaintenancePlan) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Plan.Identifier);
		//		p_filter_tsk_plan_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_tsk_plan_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_tsk_plan_Fk);
		//		//WorkOrder
		//		System.Data.IDataParameter p_filter_tsk_work_order_Fk = dataProvider.GetParameter("@filter_tsk_work_order_Fk", 
		//			f.WorkOrder == default(SOFTTEK.SCMS.Entity.PM.WorkOrder) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.WorkOrder.Identifier);
		//		p_filter_tsk_work_order_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_tsk_work_order_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_tsk_work_order_Fk);
  //              return parameters;
  //          };

  //          Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.PM.Task> mapper = (reader) =>
  //          {
		//		long longHelper = default(long);
		//		DateTime dateHelper = default(DateTime);

		//		SOFTTEK.SCMS.Entity.PM.Task value = new SOFTTEK.SCMS.Entity.PM.Task{
		//			Identifier = reader["tsk_id_Pk"] != null ? 
		//				(long.TryParse(reader["tsk_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
		//			ExternalIdentifier = reader["tsk_external_identifier"] != null ? 
		//				reader["tsk_external_identifier"].ToString() : string.Empty,
		//			Name = reader["tsk_name"] != null ? 
		//				reader["tsk_name"].ToString() : string.Empty,
		//			Description = reader["tsk_description"] != null ? 
		//				reader["tsk_description"].ToString() : string.Empty,
		//			Performer = reader["tsk_performer"] != null ? 
		//				reader["tsk_performer"].ToString() : string.Empty,
		//			StartedAt = reader["tsk_started_at"] != null ? 
		//				(DateTime.TryParse(reader["tsk_started_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
		//			FinishedAt = reader["tsk_finished_at"] != null ? 
		//				(DateTime.TryParse(reader["tsk_finished_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
		//			Status = reader["tsk_status"] != null ? 
		//				reader["tsk_status"].ToString() : string.Empty,
		//			Comments = reader["tsk_comments"] != null ? 
		//				reader["tsk_comments"].ToString() : string.Empty,
		//			QuantityCapacity = reader["tsk_quantity_capacity"] != null ? 
		//				reader["tsk_quantity_capacity"].ToString() : string.Empty,
		//			DurationOperation = reader["tsk_duration_operation"] != null ? 
		//				reader["tsk_duration_operation"].ToString() : string.Empty,
		//			Plan = reader["tsk_plan_Fk"] != null ? 
		//				new SOFTTEK.SCMS.Entity.PM.MaintenancePlan { Identifier = long.TryParse(reader["tsk_plan_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null, 
		//			WorkOrder = reader["tsk_work_order_Fk"] != null ? 
		//				new SOFTTEK.SCMS.Entity.PM.WorkOrder { Identifier = long.TryParse(reader["tsk_work_order_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null, 
		//		};
  //              return value;
  //          };

  //          dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
  //          List<SOFTTEK.SCMS.Entity.PM.Task> result = dataProvider.ExecuteReaderWithFilter(SCMS.PM_SP_Select_Task, filterDelegate, filter, mapper);
  //          return result;
		//}

		///// <summary>
  //      /// Inserts the instance of the provided Task at the Data Context datasource  
  //      /// </summary>
  //      /// <param name="instance">Instance of Task with the entity´s information to store</param>
  //      /// <returns>Instance of Task with the aditional information provided by the Data Context datasource</returns>
		//public SOFTTEK.SCMS.Entity.PM.Task InsertTask(SOFTTEK.SCMS.Entity.PM.Task instance){
		//	Func<SOFTTEK.SCMS.Entity.PM.Task, List<System.Data.IDataParameter>> input = (i) =>
  //          {
  //              List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
			
		//		//Token
		//		System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
  //              p_tok.Direction = System.Data.ParameterDirection.Input;
  //              p_tok.DbType = System.Data.DbType.String;
  //              parameters.Add(p_tok);
				
		//		// Insert
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_new_tsk_external_identifier = dataProvider.GetParameter("@new_tsk_external_identifier", 
		//			i.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.ExternalIdentifier);
		//		p_new_tsk_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_new_tsk_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_new_tsk_external_identifier);
		//		//Name
		//		System.Data.IDataParameter p_new_tsk_name = dataProvider.GetParameter("@new_tsk_name", 
		//			i.Name == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.Name);
		//		p_new_tsk_name.Direction = System.Data.ParameterDirection.Input;
		//		p_new_tsk_name.DbType = DbType.String;
		//		parameters.Add(p_new_tsk_name);
		//		//Description
		//		System.Data.IDataParameter p_new_tsk_description = dataProvider.GetParameter("@new_tsk_description", 
		//			i.Description == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.Description);
		//		p_new_tsk_description.Direction = System.Data.ParameterDirection.Input;
		//		p_new_tsk_description.DbType = DbType.String;
		//		parameters.Add(p_new_tsk_description);
		//		//Performer
		//		System.Data.IDataParameter p_new_tsk_performer = dataProvider.GetParameter("@new_tsk_performer", 
		//			i.Performer == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.Performer);
		//		p_new_tsk_performer.Direction = System.Data.ParameterDirection.Input;
		//		p_new_tsk_performer.DbType = DbType.String;
		//		parameters.Add(p_new_tsk_performer);
		//		//StartedAt
		//		System.Data.IDataParameter p_new_tsk_started_at = dataProvider.GetParameter("@new_tsk_started_at", 
		//			i.StartedAt == default(System.DateTime) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.StartedAt);
		//		p_new_tsk_started_at.Direction = System.Data.ParameterDirection.Input;
		//		p_new_tsk_started_at.DbType = DbType.DateTime;
		//		parameters.Add(p_new_tsk_started_at);
		//		//FinishedAt
		//		System.Data.IDataParameter p_new_tsk_finished_at = dataProvider.GetParameter("@new_tsk_finished_at", 
		//			i.FinishedAt == default(System.DateTime) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.FinishedAt);
		//		p_new_tsk_finished_at.Direction = System.Data.ParameterDirection.Input;
		//		p_new_tsk_finished_at.DbType = DbType.DateTime;
		//		parameters.Add(p_new_tsk_finished_at);
		//		//Status
		//		System.Data.IDataParameter p_new_tsk_status = dataProvider.GetParameter("@new_tsk_status", 
		//			i.Status == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.Status);
		//		p_new_tsk_status.Direction = System.Data.ParameterDirection.Input;
		//		p_new_tsk_status.DbType = DbType.String;
		//		parameters.Add(p_new_tsk_status);
		//		//Comments
		//		System.Data.IDataParameter p_new_tsk_comments = dataProvider.GetParameter("@new_tsk_comments", 
		//			i.Comments == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.Comments);
		//		p_new_tsk_comments.Direction = System.Data.ParameterDirection.Input;
		//		p_new_tsk_comments.DbType = DbType.String;
		//		parameters.Add(p_new_tsk_comments);
		//		//QuantityCapacity
		//		System.Data.IDataParameter p_new_tsk_quantity_capacity = dataProvider.GetParameter("@new_tsk_quantity_capacity", 
		//			i.QuantityCapacity == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.QuantityCapacity);
		//		p_new_tsk_quantity_capacity.Direction = System.Data.ParameterDirection.Input;
		//		p_new_tsk_quantity_capacity.DbType = DbType.String;
		//		parameters.Add(p_new_tsk_quantity_capacity);
		//		//DurationOperation
		//		System.Data.IDataParameter p_new_tsk_duration_operation = dataProvider.GetParameter("@new_tsk_duration_operation", 
		//			i.DurationOperation == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.DurationOperation);
		//		p_new_tsk_duration_operation.Direction = System.Data.ParameterDirection.Input;
		//		p_new_tsk_duration_operation.DbType = DbType.String;
		//		parameters.Add(p_new_tsk_duration_operation);
		//		//Plan
		//		System.Data.IDataParameter p_new_tsk_plan_Fk = dataProvider.GetParameter("@new_tsk_plan_Fk", 
		//			i.Plan == default(SOFTTEK.SCMS.Entity.PM.MaintenancePlan) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.Plan.Identifier);
		//		p_new_tsk_plan_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_new_tsk_plan_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_new_tsk_plan_Fk);
		//		//WorkOrder
		//		System.Data.IDataParameter p_new_tsk_work_order_Fk = dataProvider.GetParameter("@new_tsk_work_order_Fk", 
		//			i.WorkOrder == default(SOFTTEK.SCMS.Entity.PM.WorkOrder) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.WorkOrder.Identifier);
		//		p_new_tsk_work_order_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_new_tsk_work_order_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_new_tsk_work_order_Fk);

  //              return parameters;
  //          };

		//	Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.PM.Task> mapper = (reader) =>
  //          {
		//		long longHelper = default(long);
		//		DateTime dateHelper = default(DateTime);

		//		SOFTTEK.SCMS.Entity.PM.Task value = new SOFTTEK.SCMS.Entity.PM.Task{
		//			Identifier = reader["v_tsk_id_Pk"] != null ? 
		//				(long.TryParse(reader["v_tsk_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
		//			ExternalIdentifier = reader["v_tsk_external_identifier"] != null ? 
		//				reader["v_tsk_external_identifier"].ToString() : string.Empty,
		//			Name = reader["v_tsk_name"] != null ? 
		//				reader["v_tsk_name"].ToString() : string.Empty,
		//			Description = reader["v_tsk_description"] != null ? 
		//				reader["v_tsk_description"].ToString() : string.Empty,
		//			Performer = reader["v_tsk_performer"] != null ? 
		//				reader["v_tsk_performer"].ToString() : string.Empty,
		//			StartedAt = reader["v_tsk_started_at"] != null ? 
		//				(DateTime.TryParse(reader["v_tsk_started_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
		//			FinishedAt = reader["v_tsk_finished_at"] != null ? 
		//				(DateTime.TryParse(reader["v_tsk_finished_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
		//			Status = reader["v_tsk_status"] != null ? 
		//				reader["v_tsk_status"].ToString() : string.Empty,
		//			Comments = reader["v_tsk_comments"] != null ? 
		//				reader["v_tsk_comments"].ToString() : string.Empty,
		//			QuantityCapacity = reader["v_tsk_quantity_capacity"] != null ? 
		//				reader["v_tsk_quantity_capacity"].ToString() : string.Empty,
		//			DurationOperation = reader["v_tsk_duration_operation"] != null ? 
		//				reader["v_tsk_duration_operation"].ToString() : string.Empty,
		//			Plan = reader["v_tsk_plan_Fk"] != null ? 
		//				new SOFTTEK.SCMS.Entity.PM.MaintenancePlan { Identifier = long.TryParse(reader["v_tsk_plan_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null, 
		//			WorkOrder = reader["v_tsk_work_order_Fk"] != null ? 
		//				new SOFTTEK.SCMS.Entity.PM.WorkOrder { Identifier = long.TryParse(reader["v_tsk_work_order_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null, 
		//		};
  //              return value;
  //          };


		//	dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
  //          SOFTTEK.SCMS.Entity.PM.Task result = dataProvider.ExecuteForEntityWithFilter(SCMS.PM_SP_Insert_Task, input, instance, mapper);
  //          return result;
		//}

		///// <summary>
  //      /// Update the entity's stored information with that provided by the instance of Task at the Data Context datasource  
  //      /// </summary>
  //      /// <param name="instance">Instance of Task with the entity´s information to update</param>
  //      /// <param name="filter">Object with the information required to filter the target stored entity´s information</param>
		///// <returns>Instance of Task with the updated information provided by the Data Context datasource</returns>
		//public SOFTTEK.SCMS.Entity.PM.Task UpdateTask(SOFTTEK.SCMS.Entity.PM.Task instance, SOFTTEK.SCMS.Entity.PM.Task filter){
		//	Func<SOFTTEK.SCMS.Entity.PM.Task, List<System.Data.IDataParameter>> filterDelegate = (f) =>
  //          {
  //              List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
			
		//		//Token
		//		System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
  //              p_tok.Direction = System.Data.ParameterDirection.Input;
  //              p_tok.DbType = System.Data.DbType.String;
  //              parameters.Add(p_tok);
				
		//		// Filter
		//		//Identifier
		//		System.Data.IDataParameter p_filter_tsk_id_Pk = dataProvider.GetParameter("@filter_tsk_id_Pk", 
		//			f.Identifier == default(System.Int64) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Identifier);
		//		p_filter_tsk_id_Pk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_tsk_id_Pk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_tsk_id_Pk);
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_filter_tsk_external_identifier = dataProvider.GetParameter("@filter_tsk_external_identifier", 
		//			f.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.ExternalIdentifier);
		//		p_filter_tsk_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_tsk_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_filter_tsk_external_identifier);
		//		//Name
		//		System.Data.IDataParameter p_filter_tsk_name = dataProvider.GetParameter("@filter_tsk_name", 
		//			f.Name == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Name);
		//		p_filter_tsk_name.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_tsk_name.DbType = DbType.String;
		//		parameters.Add(p_filter_tsk_name);
		//		//Description
		//		System.Data.IDataParameter p_filter_tsk_description = dataProvider.GetParameter("@filter_tsk_description", 
		//			f.Description == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Description);
		//		p_filter_tsk_description.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_tsk_description.DbType = DbType.String;
		//		parameters.Add(p_filter_tsk_description);
		//		//Performer
		//		System.Data.IDataParameter p_filter_tsk_performer = dataProvider.GetParameter("@filter_tsk_performer", 
		//			f.Performer == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Performer);
		//		p_filter_tsk_performer.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_tsk_performer.DbType = DbType.String;
		//		parameters.Add(p_filter_tsk_performer);
		//		//Status
		//		System.Data.IDataParameter p_filter_tsk_status = dataProvider.GetParameter("@filter_tsk_status", 
		//			f.Status == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Status);
		//		p_filter_tsk_status.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_tsk_status.DbType = DbType.String;
		//		parameters.Add(p_filter_tsk_status);
		//		//Comments
		//		System.Data.IDataParameter p_filter_tsk_comments = dataProvider.GetParameter("@filter_tsk_comments", 
		//			f.Comments == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Comments);
		//		p_filter_tsk_comments.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_tsk_comments.DbType = DbType.String;
		//		parameters.Add(p_filter_tsk_comments);
		//		//QuantityCapacity
		//		System.Data.IDataParameter p_filter_tsk_quantity_capacity = dataProvider.GetParameter("@filter_tsk_quantity_capacity", 
		//			f.QuantityCapacity == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.QuantityCapacity);
		//		p_filter_tsk_quantity_capacity.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_tsk_quantity_capacity.DbType = DbType.String;
		//		parameters.Add(p_filter_tsk_quantity_capacity);
		//		//DurationOperation
		//		System.Data.IDataParameter p_filter_tsk_duration_operation = dataProvider.GetParameter("@filter_tsk_duration_operation", 
		//			f.DurationOperation == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.DurationOperation);
		//		p_filter_tsk_duration_operation.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_tsk_duration_operation.DbType = DbType.String;
		//		parameters.Add(p_filter_tsk_duration_operation);
		//		//Plan
		//		System.Data.IDataParameter p_filter_tsk_plan_Fk = dataProvider.GetParameter("@filter_tsk_plan_Fk", 
		//			f.Plan == default(SOFTTEK.SCMS.Entity.PM.MaintenancePlan) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Plan.Identifier);
		//		p_filter_tsk_plan_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_tsk_plan_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_tsk_plan_Fk);
		//		//WorkOrder
		//		System.Data.IDataParameter p_filter_tsk_work_order_Fk = dataProvider.GetParameter("@filter_tsk_work_order_Fk", 
		//			f.WorkOrder == default(SOFTTEK.SCMS.Entity.PM.WorkOrder) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.WorkOrder.Identifier);
		//		p_filter_tsk_work_order_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_tsk_work_order_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_tsk_work_order_Fk);
		//		// Update
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_new_tsk_external_identifier = dataProvider.GetParameter("@new_tsk_external_identifier", 
		//			f.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.ExternalIdentifier);
		//		p_new_tsk_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_new_tsk_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_new_tsk_external_identifier);
		//		//Name
		//		System.Data.IDataParameter p_new_tsk_name = dataProvider.GetParameter("@new_tsk_name", 
		//			f.Name == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.Name);
		//		p_new_tsk_name.Direction = System.Data.ParameterDirection.Input;
		//		p_new_tsk_name.DbType = DbType.String;
		//		parameters.Add(p_new_tsk_name);
		//		//Description
		//		System.Data.IDataParameter p_new_tsk_description = dataProvider.GetParameter("@new_tsk_description", 
		//			f.Description == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.Description);
		//		p_new_tsk_description.Direction = System.Data.ParameterDirection.Input;
		//		p_new_tsk_description.DbType = DbType.String;
		//		parameters.Add(p_new_tsk_description);
		//		//Performer
		//		System.Data.IDataParameter p_new_tsk_performer = dataProvider.GetParameter("@new_tsk_performer", 
		//			f.Performer == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.Performer);
		//		p_new_tsk_performer.Direction = System.Data.ParameterDirection.Input;
		//		p_new_tsk_performer.DbType = DbType.String;
		//		parameters.Add(p_new_tsk_performer);
		//		//StartedAt
		//		System.Data.IDataParameter p_new_tsk_started_at = dataProvider.GetParameter("@new_tsk_started_at", 
		//			f.StartedAt == default(System.DateTime) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.StartedAt);
		//		p_new_tsk_started_at.Direction = System.Data.ParameterDirection.Input;
		//		p_new_tsk_started_at.DbType = DbType.DateTime;
		//		parameters.Add(p_new_tsk_started_at);
		//		//FinishedAt
		//		System.Data.IDataParameter p_new_tsk_finished_at = dataProvider.GetParameter("@new_tsk_finished_at", 
		//			f.FinishedAt == default(System.DateTime) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.FinishedAt);
		//		p_new_tsk_finished_at.Direction = System.Data.ParameterDirection.Input;
		//		p_new_tsk_finished_at.DbType = DbType.DateTime;
		//		parameters.Add(p_new_tsk_finished_at);
		//		//Status
		//		System.Data.IDataParameter p_new_tsk_status = dataProvider.GetParameter("@new_tsk_status", 
		//			f.Status == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.Status);
		//		p_new_tsk_status.Direction = System.Data.ParameterDirection.Input;
		//		p_new_tsk_status.DbType = DbType.String;
		//		parameters.Add(p_new_tsk_status);
		//		//Comments
		//		System.Data.IDataParameter p_new_tsk_comments = dataProvider.GetParameter("@new_tsk_comments", 
		//			f.Comments == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.Comments);
		//		p_new_tsk_comments.Direction = System.Data.ParameterDirection.Input;
		//		p_new_tsk_comments.DbType = DbType.String;
		//		parameters.Add(p_new_tsk_comments);
		//		//QuantityCapacity
		//		System.Data.IDataParameter p_new_tsk_quantity_capacity = dataProvider.GetParameter("@new_tsk_quantity_capacity", 
		//			f.QuantityCapacity == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.QuantityCapacity);
		//		p_new_tsk_quantity_capacity.Direction = System.Data.ParameterDirection.Input;
		//		p_new_tsk_quantity_capacity.DbType = DbType.String;
		//		parameters.Add(p_new_tsk_quantity_capacity);
		//		//DurationOperation
		//		System.Data.IDataParameter p_new_tsk_duration_operation = dataProvider.GetParameter("@new_tsk_duration_operation", 
		//			f.DurationOperation == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.DurationOperation);
		//		p_new_tsk_duration_operation.Direction = System.Data.ParameterDirection.Input;
		//		p_new_tsk_duration_operation.DbType = DbType.String;
		//		parameters.Add(p_new_tsk_duration_operation);
		//		//Plan
		//		System.Data.IDataParameter p_new_tsk_plan_Fk = dataProvider.GetParameter("@new_tsk_plan_Fk", 
		//			instance.Plan == default(SOFTTEK.SCMS.Entity.PM.MaintenancePlan) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.Plan.Identifier);
		//		p_new_tsk_plan_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_new_tsk_plan_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_new_tsk_plan_Fk);
		//		//WorkOrder
		//		System.Data.IDataParameter p_new_tsk_work_order_Fk = dataProvider.GetParameter("@new_tsk_work_order_Fk", 
		//			instance.WorkOrder == default(SOFTTEK.SCMS.Entity.PM.WorkOrder) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.WorkOrder.Identifier);
		//		p_new_tsk_work_order_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_new_tsk_work_order_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_new_tsk_work_order_Fk);

  //              return parameters;
  //          };

		//	Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.PM.Task> mapper = (reader) =>
  //          {
		//		long longHelper = default(long);
		//		DateTime dateHelper = default(DateTime);

		//		SOFTTEK.SCMS.Entity.PM.Task value = new SOFTTEK.SCMS.Entity.PM.Task{
		//			Identifier = reader["v_tsk_id_Pk"] != null ? 
		//				(long.TryParse(reader["v_tsk_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
		//			ExternalIdentifier = reader["v_tsk_external_identifier"] != null ? 
		//				reader["v_tsk_external_identifier"].ToString() : string.Empty,
		//			Name = reader["v_tsk_name"] != null ? 
		//				reader["v_tsk_name"].ToString() : string.Empty,
		//			Description = reader["v_tsk_description"] != null ? 
		//				reader["v_tsk_description"].ToString() : string.Empty,
		//			Performer = reader["v_tsk_performer"] != null ? 
		//				reader["v_tsk_performer"].ToString() : string.Empty,
		//			StartedAt = reader["v_tsk_started_at"] != null ? 
		//				(DateTime.TryParse(reader["v_tsk_started_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
		//			FinishedAt = reader["v_tsk_finished_at"] != null ? 
		//				(DateTime.TryParse(reader["v_tsk_finished_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
		//			Status = reader["v_tsk_status"] != null ? 
		//				reader["v_tsk_status"].ToString() : string.Empty,
		//			Comments = reader["v_tsk_comments"] != null ? 
		//				reader["v_tsk_comments"].ToString() : string.Empty,
		//			QuantityCapacity = reader["v_tsk_quantity_capacity"] != null ? 
		//				reader["v_tsk_quantity_capacity"].ToString() : string.Empty,
		//			DurationOperation = reader["v_tsk_duration_operation"] != null ? 
		//				reader["v_tsk_duration_operation"].ToString() : string.Empty,
		//			Plan = reader["v_tsk_plan_Fk"] != null ? 
		//				new SOFTTEK.SCMS.Entity.PM.MaintenancePlan { Identifier = long.TryParse(reader["v_tsk_plan_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null, 
		//			WorkOrder = reader["v_tsk_work_order_Fk"] != null ? 
		//				new SOFTTEK.SCMS.Entity.PM.WorkOrder { Identifier = long.TryParse(reader["v_tsk_work_order_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null, 
		//		};
  //              return value;
  //          };


		//	dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
  //          SOFTTEK.SCMS.Entity.PM.Task result = dataProvider.ExecuteForEntityWithFilter(SCMS.PM_SP_Update_Task, filterDelegate, filter, mapper);
  //          return result;
		//}
		//#endregion
		//#region WorkOrder
		
		///// <summary>
  //      /// Retrieves a list of WorkOrder instances that matches a given filter criteria by the WorkOrder object instance 
  //      /// </summary>
  //      /// <param name="filter">Object with the information required to filter the set of results</param>
  //      /// <returns>List of WorkOrder instances that matches the filter.</returns>
		//public List<SOFTTEK.SCMS.Entity.PM.WorkOrder> GetWorkOrders(SOFTTEK.SCMS.Entity.PM.WorkOrder filter)
		//{
		//	Func<SOFTTEK.SCMS.Entity.PM.WorkOrder, List<System.Data.IDataParameter>> filterDelegate = (f) =>
  //          {
  //              List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
		//		//Token
		//		System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
  //              p_tok.Direction = System.Data.ParameterDirection.Input;
  //              p_tok.DbType = System.Data.DbType.String;
  //              parameters.Add(p_tok);
		//		//Identifier
		//		System.Data.IDataParameter p_filter_wrkord_id_Pk = dataProvider.GetParameter("@filter_wrkord_id_Pk", 
		//			f.Identifier == default(System.Int64) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Identifier);
		//		p_filter_wrkord_id_Pk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_wrkord_id_Pk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_wrkord_id_Pk);
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_filter_wrkord_external_identifier = dataProvider.GetParameter("@filter_wrkord_external_identifier", 
		//			f.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.ExternalIdentifier);
		//		p_filter_wrkord_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_wrkord_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_filter_wrkord_external_identifier);
		//		//Type
		//		System.Data.IDataParameter p_filter_wrkord_type = dataProvider.GetParameter("@filter_wrkord_type", 
		//			f.Type == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Type);
		//		p_filter_wrkord_type.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_wrkord_type.DbType = DbType.String;
		//		parameters.Add(p_filter_wrkord_type);
		//		//Company
		//		System.Data.IDataParameter p_filter_wrkord_company = dataProvider.GetParameter("@filter_wrkord_company", 
		//			f.Company == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Company);
		//		p_filter_wrkord_company.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_wrkord_company.DbType = DbType.String;
		//		parameters.Add(p_filter_wrkord_company);
		//		//Priority
		//		System.Data.IDataParameter p_filter_wrkord_priority = dataProvider.GetParameter("@filter_wrkord_priority", 
		//			f.Priority == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Priority);
		//		p_filter_wrkord_priority.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_wrkord_priority.DbType = DbType.String;
		//		parameters.Add(p_filter_wrkord_priority);
		//		//Performer
		//		System.Data.IDataParameter p_filter_wrkord_performer = dataProvider.GetParameter("@filter_wrkord_performer", 
		//			f.Performer == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Performer);
		//		p_filter_wrkord_performer.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_wrkord_performer.DbType = DbType.String;
		//		parameters.Add(p_filter_wrkord_performer);
		//		//State
		//		System.Data.IDataParameter p_filter_wrkord_state = dataProvider.GetParameter("@filter_wrkord_state", 
		//			f.State == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.State);
		//		p_filter_wrkord_state.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_wrkord_state.DbType = DbType.String;
		//		parameters.Add(p_filter_wrkord_state);
		//		//TechnicalObject
		//		System.Data.IDataParameter p_filter_wrkord_technical_object_Fk = dataProvider.GetParameter("@filter_wrkord_technical_object_Fk", 
		//			f.TechnicalObject == default(SOFTTEK.SCMS.Entity.PM.TechnicalObject) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.TechnicalObject.Identifier);
		//		p_filter_wrkord_technical_object_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_wrkord_technical_object_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_wrkord_technical_object_Fk);
		//		//ReleaseDate
		//		System.Data.IDataParameter p_filter_wrkord_release_date = dataProvider.GetParameter("@filter_wrkord_release_date", 
		//			f.ReleaseDate == default(System.DateTime) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.ReleaseDate);
		//		p_filter_wrkord_release_date.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_wrkord_release_date.DbType = DbType.DateTime;
		//		parameters.Add(p_filter_wrkord_release_date);
		//		//PlanningGroup
		//		System.Data.IDataParameter p_filter_wrkord_planning_group = dataProvider.GetParameter("@filter_wrkord_planning_group", 
		//			f.PlanningGroup == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.PlanningGroup);
		//		p_filter_wrkord_planning_group.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_wrkord_planning_group.DbType = DbType.String;
		//		parameters.Add(p_filter_wrkord_planning_group);
		//		//Workstation
		//		System.Data.IDataParameter p_filter_wrkord_workstation = dataProvider.GetParameter("@filter_wrkord_workstation", 
		//			f.Workstation == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Workstation);
		//		p_filter_wrkord_workstation.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_wrkord_workstation.DbType = DbType.String;
		//		parameters.Add(p_filter_wrkord_workstation);
		//		//Activity
		//		System.Data.IDataParameter p_filter_wrkord_activity = dataProvider.GetParameter("@filter_wrkord_activity", 
		//			f.Activity == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Activity);
		//		p_filter_wrkord_activity.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_wrkord_activity.DbType = DbType.String;
		//		parameters.Add(p_filter_wrkord_activity);
  //              return parameters;
  //          };

  //          Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.PM.WorkOrder> mapper = (reader) =>
  //          {
		//		long longHelper = default(long);
		//		DateTime dateHelper = default(DateTime);

		//		SOFTTEK.SCMS.Entity.PM.WorkOrder value = new SOFTTEK.SCMS.Entity.PM.WorkOrder{
		//			Identifier = reader["wrkord_id_Pk"] != null ? 
		//				(long.TryParse(reader["wrkord_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
		//			ExternalIdentifier = reader["wrkord_external_identifier"] != null ? 
		//				reader["wrkord_external_identifier"].ToString() : string.Empty,
		//			Type = reader["wrkord_type"] != null ? 
		//				reader["wrkord_type"].ToString() : string.Empty,
		//			Company = reader["wrkord_company"] != null ? 
		//				reader["wrkord_company"].ToString() : string.Empty,
		//			Priority = reader["wrkord_priority"] != null ? 
		//				reader["wrkord_priority"].ToString() : string.Empty,
		//			Performer = reader["wrkord_performer"] != null ? 
		//				reader["wrkord_performer"].ToString() : string.Empty,
		//			State = reader["wrkord_state"] != null ? 
		//				reader["wrkord_state"].ToString() : string.Empty,
		//			TechnicalObject = reader["wrkord_technical_object_Fk"] != null ? 
		//				new SOFTTEK.SCMS.Entity.PM.TechnicalObject { Identifier = long.TryParse(reader["wrkord_technical_object_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null, 
		//			ScheduledTo = reader["wrkord_scheduled_to"] != null ? 
		//				(DateTime.TryParse(reader["wrkord_scheduled_to"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
		//			ExecutionStartAt = reader["wrkord_execution_start_at"] != null ? 
		//				(DateTime.TryParse(reader["wrkord_execution_start_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
		//			ExecutionFinishedAt = reader["wrkord_execution_finished_at"] != null ? 
		//				(DateTime.TryParse(reader["wrkord_execution_finished_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
		//			ReleaseDate = reader["wrkord_release_date"] != null ? 
		//				(DateTime.TryParse(reader["wrkord_release_date"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
		//			PlanningGroup = reader["wrkord_planning_group"] != null ? 
		//				reader["wrkord_planning_group"].ToString() : string.Empty,
		//			Workstation = reader["wrkord_workstation"] != null ? 
		//				reader["wrkord_workstation"].ToString() : string.Empty,
		//			Activity = reader["wrkord_activity"] != null ? 
		//				reader["wrkord_activity"].ToString() : string.Empty,
		//		};
  //              return value;
  //          };

  //          dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
  //          List<SOFTTEK.SCMS.Entity.PM.WorkOrder> result = dataProvider.ExecuteReaderWithFilter(SCMS.PM_SP_Select_WorkOrder, filterDelegate, filter, mapper);
  //          return result;
		//}

		///// <summary>
  //      /// Inserts the instance of the provided WorkOrder at the Data Context datasource  
  //      /// </summary>
  //      /// <param name="instance">Instance of WorkOrder with the entity´s information to store</param>
  //      /// <returns>Instance of WorkOrder with the aditional information provided by the Data Context datasource</returns>
		//public SOFTTEK.SCMS.Entity.PM.WorkOrder InsertWorkOrder(SOFTTEK.SCMS.Entity.PM.WorkOrder instance){
		//	Func<SOFTTEK.SCMS.Entity.PM.WorkOrder, List<System.Data.IDataParameter>> input = (i) =>
  //          {
  //              List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
			
		//		//Token
		//		System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
  //              p_tok.Direction = System.Data.ParameterDirection.Input;
  //              p_tok.DbType = System.Data.DbType.String;
  //              parameters.Add(p_tok);
				
		//		// Insert
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_new_wrkord_external_identifier = dataProvider.GetParameter("@new_wrkord_external_identifier", 
		//			i.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.ExternalIdentifier);
		//		p_new_wrkord_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_new_wrkord_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_new_wrkord_external_identifier);
		//		//Type
		//		System.Data.IDataParameter p_new_wrkord_type = dataProvider.GetParameter("@new_wrkord_type", 
		//			i.Type == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.Type);
		//		p_new_wrkord_type.Direction = System.Data.ParameterDirection.Input;
		//		p_new_wrkord_type.DbType = DbType.String;
		//		parameters.Add(p_new_wrkord_type);
		//		//Company
		//		System.Data.IDataParameter p_new_wrkord_company = dataProvider.GetParameter("@new_wrkord_company", 
		//			i.Company == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.Company);
		//		p_new_wrkord_company.Direction = System.Data.ParameterDirection.Input;
		//		p_new_wrkord_company.DbType = DbType.String;
		//		parameters.Add(p_new_wrkord_company);
		//		//Priority
		//		System.Data.IDataParameter p_new_wrkord_priority = dataProvider.GetParameter("@new_wrkord_priority", 
		//			i.Priority == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.Priority);
		//		p_new_wrkord_priority.Direction = System.Data.ParameterDirection.Input;
		//		p_new_wrkord_priority.DbType = DbType.String;
		//		parameters.Add(p_new_wrkord_priority);
		//		//Performer
		//		System.Data.IDataParameter p_new_wrkord_performer = dataProvider.GetParameter("@new_wrkord_performer", 
		//			i.Performer == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.Performer);
		//		p_new_wrkord_performer.Direction = System.Data.ParameterDirection.Input;
		//		p_new_wrkord_performer.DbType = DbType.String;
		//		parameters.Add(p_new_wrkord_performer);
		//		//State
		//		System.Data.IDataParameter p_new_wrkord_state = dataProvider.GetParameter("@new_wrkord_state", 
		//			i.State == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.State);
		//		p_new_wrkord_state.Direction = System.Data.ParameterDirection.Input;
		//		p_new_wrkord_state.DbType = DbType.String;
		//		parameters.Add(p_new_wrkord_state);
		//		//TechnicalObject
		//		System.Data.IDataParameter p_new_wrkord_technical_object_Fk = dataProvider.GetParameter("@new_wrkord_technical_object_Fk", 
		//			i.TechnicalObject == default(SOFTTEK.SCMS.Entity.PM.TechnicalObject) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.TechnicalObject.Identifier);
		//		p_new_wrkord_technical_object_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_new_wrkord_technical_object_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_new_wrkord_technical_object_Fk);
		//		//ScheduledTo
		//		System.Data.IDataParameter p_new_wrkord_scheduled_to = dataProvider.GetParameter("@new_wrkord_scheduled_to", 
		//			i.ScheduledTo == default(System.DateTime) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.ScheduledTo);
		//		p_new_wrkord_scheduled_to.Direction = System.Data.ParameterDirection.Input;
		//		p_new_wrkord_scheduled_to.DbType = DbType.DateTime;
		//		parameters.Add(p_new_wrkord_scheduled_to);
		//		//ExecutionStartAt
		//		System.Data.IDataParameter p_new_wrkord_execution_start_at = dataProvider.GetParameter("@new_wrkord_execution_start_at", 
		//			i.ExecutionStartAt == default(System.DateTime) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.ExecutionStartAt);
		//		p_new_wrkord_execution_start_at.Direction = System.Data.ParameterDirection.Input;
		//		p_new_wrkord_execution_start_at.DbType = DbType.DateTime;
		//		parameters.Add(p_new_wrkord_execution_start_at);
		//		//ExecutionFinishedAt
		//		System.Data.IDataParameter p_new_wrkord_execution_finished_at = dataProvider.GetParameter("@new_wrkord_execution_finished_at", 
		//			i.ExecutionFinishedAt == default(System.DateTime) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.ExecutionFinishedAt);
		//		p_new_wrkord_execution_finished_at.Direction = System.Data.ParameterDirection.Input;
		//		p_new_wrkord_execution_finished_at.DbType = DbType.DateTime;
		//		parameters.Add(p_new_wrkord_execution_finished_at);
		//		//ReleaseDate
		//		System.Data.IDataParameter p_new_wrkord_release_date = dataProvider.GetParameter("@new_wrkord_release_date", 
		//			i.ReleaseDate == default(System.DateTime) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.ReleaseDate);
		//		p_new_wrkord_release_date.Direction = System.Data.ParameterDirection.Input;
		//		p_new_wrkord_release_date.DbType = DbType.DateTime;
		//		parameters.Add(p_new_wrkord_release_date);
		//		//PlanningGroup
		//		System.Data.IDataParameter p_new_wrkord_planning_group = dataProvider.GetParameter("@new_wrkord_planning_group", 
		//			i.PlanningGroup == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.PlanningGroup);
		//		p_new_wrkord_planning_group.Direction = System.Data.ParameterDirection.Input;
		//		p_new_wrkord_planning_group.DbType = DbType.String;
		//		parameters.Add(p_new_wrkord_planning_group);
		//		//Workstation
		//		System.Data.IDataParameter p_new_wrkord_workstation = dataProvider.GetParameter("@new_wrkord_workstation", 
		//			i.Workstation == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.Workstation);
		//		p_new_wrkord_workstation.Direction = System.Data.ParameterDirection.Input;
		//		p_new_wrkord_workstation.DbType = DbType.String;
		//		parameters.Add(p_new_wrkord_workstation);
		//		//Activity
		//		System.Data.IDataParameter p_new_wrkord_activity = dataProvider.GetParameter("@new_wrkord_activity", 
		//			i.Activity == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)i.Activity);
		//		p_new_wrkord_activity.Direction = System.Data.ParameterDirection.Input;
		//		p_new_wrkord_activity.DbType = DbType.String;
		//		parameters.Add(p_new_wrkord_activity);

  //              return parameters;
  //          };

		//	Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.PM.WorkOrder> mapper = (reader) =>
  //          {
		//		long longHelper = default(long);
		//		DateTime dateHelper = default(DateTime);

		//		SOFTTEK.SCMS.Entity.PM.WorkOrder value = new SOFTTEK.SCMS.Entity.PM.WorkOrder{
		//			Identifier = reader["v_wrkord_id_Pk"] != null ? 
		//				(long.TryParse(reader["v_wrkord_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
		//			ExternalIdentifier = reader["v_wrkord_external_identifier"] != null ? 
		//				reader["v_wrkord_external_identifier"].ToString() : string.Empty,
		//			Type = reader["v_wrkord_type"] != null ? 
		//				reader["v_wrkord_type"].ToString() : string.Empty,
		//			Company = reader["v_wrkord_company"] != null ? 
		//				reader["v_wrkord_company"].ToString() : string.Empty,
		//			Priority = reader["v_wrkord_priority"] != null ? 
		//				reader["v_wrkord_priority"].ToString() : string.Empty,
		//			Performer = reader["v_wrkord_performer"] != null ? 
		//				reader["v_wrkord_performer"].ToString() : string.Empty,
		//			State = reader["v_wrkord_state"] != null ? 
		//				reader["v_wrkord_state"].ToString() : string.Empty,
		//			TechnicalObject = reader["v_wrkord_technical_object_Fk"] != null ? 
		//				new SOFTTEK.SCMS.Entity.PM.TechnicalObject { Identifier = long.TryParse(reader["v_wrkord_technical_object_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null, 
		//			ScheduledTo = reader["v_wrkord_scheduled_to"] != null ? 
		//				(DateTime.TryParse(reader["v_wrkord_scheduled_to"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
		//			ExecutionStartAt = reader["v_wrkord_execution_start_at"] != null ? 
		//				(DateTime.TryParse(reader["v_wrkord_execution_start_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
		//			ExecutionFinishedAt = reader["v_wrkord_execution_finished_at"] != null ? 
		//				(DateTime.TryParse(reader["v_wrkord_execution_finished_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
		//			ReleaseDate = reader["v_wrkord_release_date"] != null ? 
		//				(DateTime.TryParse(reader["v_wrkord_release_date"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
		//			PlanningGroup = reader["v_wrkord_planning_group"] != null ? 
		//				reader["v_wrkord_planning_group"].ToString() : string.Empty,
		//			Workstation = reader["v_wrkord_workstation"] != null ? 
		//				reader["v_wrkord_workstation"].ToString() : string.Empty,
		//			Activity = reader["v_wrkord_activity"] != null ? 
		//				reader["v_wrkord_activity"].ToString() : string.Empty,
		//		};
  //              return value;
  //          };


		//	dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
  //          SOFTTEK.SCMS.Entity.PM.WorkOrder result = dataProvider.ExecuteForEntityWithFilter(SCMS.PM_SP_Insert_WorkOrder, input, instance, mapper);
  //          return result;
		//}

		///// <summary>
  //      /// Update the entity's stored information with that provided by the instance of WorkOrder at the Data Context datasource  
  //      /// </summary>
  //      /// <param name="instance">Instance of WorkOrder with the entity´s information to update</param>
  //      /// <param name="filter">Object with the information required to filter the target stored entity´s information</param>
		///// <returns>Instance of WorkOrder with the updated information provided by the Data Context datasource</returns>
		//public SOFTTEK.SCMS.Entity.PM.WorkOrder UpdateWorkOrder(SOFTTEK.SCMS.Entity.PM.WorkOrder instance, SOFTTEK.SCMS.Entity.PM.WorkOrder filter){
		//	Func<SOFTTEK.SCMS.Entity.PM.WorkOrder, List<System.Data.IDataParameter>> filterDelegate = (f) =>
  //          {
  //              List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();
			
		//		//Token
		//		System.Data.IDataParameter p_tok = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
  //              p_tok.Direction = System.Data.ParameterDirection.Input;
  //              p_tok.DbType = System.Data.DbType.String;
  //              parameters.Add(p_tok);
				
		//		// Filter
		//		//Identifier
		//		System.Data.IDataParameter p_filter_wrkord_id_Pk = dataProvider.GetParameter("@filter_wrkord_id_Pk", 
		//			f.Identifier == default(System.Int64) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Identifier);
		//		p_filter_wrkord_id_Pk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_wrkord_id_Pk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_wrkord_id_Pk);
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_filter_wrkord_external_identifier = dataProvider.GetParameter("@filter_wrkord_external_identifier", 
		//			f.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.ExternalIdentifier);
		//		p_filter_wrkord_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_wrkord_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_filter_wrkord_external_identifier);
		//		//Type
		//		System.Data.IDataParameter p_filter_wrkord_type = dataProvider.GetParameter("@filter_wrkord_type", 
		//			f.Type == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Type);
		//		p_filter_wrkord_type.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_wrkord_type.DbType = DbType.String;
		//		parameters.Add(p_filter_wrkord_type);
		//		//Company
		//		System.Data.IDataParameter p_filter_wrkord_company = dataProvider.GetParameter("@filter_wrkord_company", 
		//			f.Company == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Company);
		//		p_filter_wrkord_company.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_wrkord_company.DbType = DbType.String;
		//		parameters.Add(p_filter_wrkord_company);
		//		//Priority
		//		System.Data.IDataParameter p_filter_wrkord_priority = dataProvider.GetParameter("@filter_wrkord_priority", 
		//			f.Priority == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Priority);
		//		p_filter_wrkord_priority.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_wrkord_priority.DbType = DbType.String;
		//		parameters.Add(p_filter_wrkord_priority);
		//		//Performer
		//		System.Data.IDataParameter p_filter_wrkord_performer = dataProvider.GetParameter("@filter_wrkord_performer", 
		//			f.Performer == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Performer);
		//		p_filter_wrkord_performer.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_wrkord_performer.DbType = DbType.String;
		//		parameters.Add(p_filter_wrkord_performer);
		//		//State
		//		System.Data.IDataParameter p_filter_wrkord_state = dataProvider.GetParameter("@filter_wrkord_state", 
		//			f.State == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.State);
		//		p_filter_wrkord_state.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_wrkord_state.DbType = DbType.String;
		//		parameters.Add(p_filter_wrkord_state);
		//		//TechnicalObject
		//		System.Data.IDataParameter p_filter_wrkord_technical_object_Fk = dataProvider.GetParameter("@filter_wrkord_technical_object_Fk", 
		//			f.TechnicalObject == default(SOFTTEK.SCMS.Entity.PM.TechnicalObject) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.TechnicalObject.Identifier);
		//		p_filter_wrkord_technical_object_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_wrkord_technical_object_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_filter_wrkord_technical_object_Fk);
		//		//ReleaseDate
		//		System.Data.IDataParameter p_filter_wrkord_release_date = dataProvider.GetParameter("@filter_wrkord_release_date", 
		//			f.ReleaseDate == default(System.DateTime) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.ReleaseDate);
		//		p_filter_wrkord_release_date.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_wrkord_release_date.DbType = DbType.DateTime;
		//		parameters.Add(p_filter_wrkord_release_date);
		//		//PlanningGroup
		//		System.Data.IDataParameter p_filter_wrkord_planning_group = dataProvider.GetParameter("@filter_wrkord_planning_group", 
		//			f.PlanningGroup == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.PlanningGroup);
		//		p_filter_wrkord_planning_group.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_wrkord_planning_group.DbType = DbType.String;
		//		parameters.Add(p_filter_wrkord_planning_group);
		//		//Workstation
		//		System.Data.IDataParameter p_filter_wrkord_workstation = dataProvider.GetParameter("@filter_wrkord_workstation", 
		//			f.Workstation == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Workstation);
		//		p_filter_wrkord_workstation.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_wrkord_workstation.DbType = DbType.String;
		//		parameters.Add(p_filter_wrkord_workstation);
		//		//Activity
		//		System.Data.IDataParameter p_filter_wrkord_activity = dataProvider.GetParameter("@filter_wrkord_activity", 
		//			f.Activity == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)f.Activity);
		//		p_filter_wrkord_activity.Direction = System.Data.ParameterDirection.Input;
		//		p_filter_wrkord_activity.DbType = DbType.String;
		//		parameters.Add(p_filter_wrkord_activity);
		//		// Update
		//		//ExternalIdentifier
		//		System.Data.IDataParameter p_new_wrkord_external_identifier = dataProvider.GetParameter("@new_wrkord_external_identifier", 
		//			f.ExternalIdentifier == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.ExternalIdentifier);
		//		p_new_wrkord_external_identifier.Direction = System.Data.ParameterDirection.Input;
		//		p_new_wrkord_external_identifier.DbType = DbType.String;
		//		parameters.Add(p_new_wrkord_external_identifier);
		//		//Type
		//		System.Data.IDataParameter p_new_wrkord_type = dataProvider.GetParameter("@new_wrkord_type", 
		//			f.Type == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.Type);
		//		p_new_wrkord_type.Direction = System.Data.ParameterDirection.Input;
		//		p_new_wrkord_type.DbType = DbType.String;
		//		parameters.Add(p_new_wrkord_type);
		//		//Company
		//		System.Data.IDataParameter p_new_wrkord_company = dataProvider.GetParameter("@new_wrkord_company", 
		//			f.Company == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.Company);
		//		p_new_wrkord_company.Direction = System.Data.ParameterDirection.Input;
		//		p_new_wrkord_company.DbType = DbType.String;
		//		parameters.Add(p_new_wrkord_company);
		//		//Priority
		//		System.Data.IDataParameter p_new_wrkord_priority = dataProvider.GetParameter("@new_wrkord_priority", 
		//			f.Priority == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.Priority);
		//		p_new_wrkord_priority.Direction = System.Data.ParameterDirection.Input;
		//		p_new_wrkord_priority.DbType = DbType.String;
		//		parameters.Add(p_new_wrkord_priority);
		//		//Performer
		//		System.Data.IDataParameter p_new_wrkord_performer = dataProvider.GetParameter("@new_wrkord_performer", 
		//			f.Performer == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.Performer);
		//		p_new_wrkord_performer.Direction = System.Data.ParameterDirection.Input;
		//		p_new_wrkord_performer.DbType = DbType.String;
		//		parameters.Add(p_new_wrkord_performer);
		//		//State
		//		System.Data.IDataParameter p_new_wrkord_state = dataProvider.GetParameter("@new_wrkord_state", 
		//			f.State == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.State);
		//		p_new_wrkord_state.Direction = System.Data.ParameterDirection.Input;
		//		p_new_wrkord_state.DbType = DbType.String;
		//		parameters.Add(p_new_wrkord_state);
		//		//TechnicalObject
		//		System.Data.IDataParameter p_new_wrkord_technical_object_Fk = dataProvider.GetParameter("@new_wrkord_technical_object_Fk", 
		//			instance.TechnicalObject == default(SOFTTEK.SCMS.Entity.PM.TechnicalObject) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.TechnicalObject.Identifier);
		//		p_new_wrkord_technical_object_Fk.Direction = System.Data.ParameterDirection.Input;
		//		p_new_wrkord_technical_object_Fk.DbType = DbType.Int64;
		//		parameters.Add(p_new_wrkord_technical_object_Fk);
		//		//ScheduledTo
		//		System.Data.IDataParameter p_new_wrkord_scheduled_to = dataProvider.GetParameter("@new_wrkord_scheduled_to", 
		//			f.ScheduledTo == default(System.DateTime) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.ScheduledTo);
		//		p_new_wrkord_scheduled_to.Direction = System.Data.ParameterDirection.Input;
		//		p_new_wrkord_scheduled_to.DbType = DbType.DateTime;
		//		parameters.Add(p_new_wrkord_scheduled_to);
		//		//ExecutionStartAt
		//		System.Data.IDataParameter p_new_wrkord_execution_start_at = dataProvider.GetParameter("@new_wrkord_execution_start_at", 
		//			f.ExecutionStartAt == default(System.DateTime) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.ExecutionStartAt);
		//		p_new_wrkord_execution_start_at.Direction = System.Data.ParameterDirection.Input;
		//		p_new_wrkord_execution_start_at.DbType = DbType.DateTime;
		//		parameters.Add(p_new_wrkord_execution_start_at);
		//		//ExecutionFinishedAt
		//		System.Data.IDataParameter p_new_wrkord_execution_finished_at = dataProvider.GetParameter("@new_wrkord_execution_finished_at", 
		//			f.ExecutionFinishedAt == default(System.DateTime) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.ExecutionFinishedAt);
		//		p_new_wrkord_execution_finished_at.Direction = System.Data.ParameterDirection.Input;
		//		p_new_wrkord_execution_finished_at.DbType = DbType.DateTime;
		//		parameters.Add(p_new_wrkord_execution_finished_at);
		//		//ReleaseDate
		//		System.Data.IDataParameter p_new_wrkord_release_date = dataProvider.GetParameter("@new_wrkord_release_date", 
		//			f.ReleaseDate == default(System.DateTime) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.ReleaseDate);
		//		p_new_wrkord_release_date.Direction = System.Data.ParameterDirection.Input;
		//		p_new_wrkord_release_date.DbType = DbType.DateTime;
		//		parameters.Add(p_new_wrkord_release_date);
		//		//PlanningGroup
		//		System.Data.IDataParameter p_new_wrkord_planning_group = dataProvider.GetParameter("@new_wrkord_planning_group", 
		//			f.PlanningGroup == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.PlanningGroup);
		//		p_new_wrkord_planning_group.Direction = System.Data.ParameterDirection.Input;
		//		p_new_wrkord_planning_group.DbType = DbType.String;
		//		parameters.Add(p_new_wrkord_planning_group);
		//		//Workstation
		//		System.Data.IDataParameter p_new_wrkord_workstation = dataProvider.GetParameter("@new_wrkord_workstation", 
		//			f.Workstation == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.Workstation);
		//		p_new_wrkord_workstation.Direction = System.Data.ParameterDirection.Input;
		//		p_new_wrkord_workstation.DbType = DbType.String;
		//		parameters.Add(p_new_wrkord_workstation);
		//		//Activity
		//		System.Data.IDataParameter p_new_wrkord_activity = dataProvider.GetParameter("@new_wrkord_activity", 
		//			f.Activity == default(System.String) ? 
		//				(object)System.DBNull.Value : 
		//				(object)instance.Activity);
		//		p_new_wrkord_activity.Direction = System.Data.ParameterDirection.Input;
		//		p_new_wrkord_activity.DbType = DbType.String;
		//		parameters.Add(p_new_wrkord_activity);

  //              return parameters;
  //          };

		//	Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.PM.WorkOrder> mapper = (reader) =>
  //          {
		//		long longHelper = default(long);
		//		DateTime dateHelper = default(DateTime);

		//		SOFTTEK.SCMS.Entity.PM.WorkOrder value = new SOFTTEK.SCMS.Entity.PM.WorkOrder{
		//			Identifier = reader["v_wrkord_id_Pk"] != null ? 
		//				(long.TryParse(reader["v_wrkord_id_Pk"].ToString(), out longHelper) ? longHelper : -1) : -1,
		//			ExternalIdentifier = reader["v_wrkord_external_identifier"] != null ? 
		//				reader["v_wrkord_external_identifier"].ToString() : string.Empty,
		//			Type = reader["v_wrkord_type"] != null ? 
		//				reader["v_wrkord_type"].ToString() : string.Empty,
		//			Company = reader["v_wrkord_company"] != null ? 
		//				reader["v_wrkord_company"].ToString() : string.Empty,
		//			Priority = reader["v_wrkord_priority"] != null ? 
		//				reader["v_wrkord_priority"].ToString() : string.Empty,
		//			Performer = reader["v_wrkord_performer"] != null ? 
		//				reader["v_wrkord_performer"].ToString() : string.Empty,
		//			State = reader["v_wrkord_state"] != null ? 
		//				reader["v_wrkord_state"].ToString() : string.Empty,
		//			TechnicalObject = reader["v_wrkord_technical_object_Fk"] != null ? 
		//				new SOFTTEK.SCMS.Entity.PM.TechnicalObject { Identifier = long.TryParse(reader["v_wrkord_technical_object_Fk"].ToString(), out longHelper) ? longHelper : -1 } : null, 
		//			ScheduledTo = reader["v_wrkord_scheduled_to"] != null ? 
		//				(DateTime.TryParse(reader["v_wrkord_scheduled_to"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
		//			ExecutionStartAt = reader["v_wrkord_execution_start_at"] != null ? 
		//				(DateTime.TryParse(reader["v_wrkord_execution_start_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
		//			ExecutionFinishedAt = reader["v_wrkord_execution_finished_at"] != null ? 
		//				(DateTime.TryParse(reader["v_wrkord_execution_finished_at"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
		//			ReleaseDate = reader["v_wrkord_release_date"] != null ? 
		//				(DateTime.TryParse(reader["v_wrkord_release_date"].ToString(), out dateHelper) ? dateHelper : default(DateTime)) : default(DateTime),
		//			PlanningGroup = reader["v_wrkord_planning_group"] != null ? 
		//				reader["v_wrkord_planning_group"].ToString() : string.Empty,
		//			Workstation = reader["v_wrkord_workstation"] != null ? 
		//				reader["v_wrkord_workstation"].ToString() : string.Empty,
		//			Activity = reader["v_wrkord_activity"] != null ? 
		//				reader["v_wrkord_activity"].ToString() : string.Empty,
		//		};
  //              return value;
  //          };


		//	dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
  //          SOFTTEK.SCMS.Entity.PM.WorkOrder result = dataProvider.ExecuteForEntityWithFilter(SCMS.PM_SP_Update_WorkOrder, filterDelegate, filter, mapper);
  //          return result;
		//}
		//#endregion

		//#endregion
	//}
}


