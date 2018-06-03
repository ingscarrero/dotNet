CREATE PROCEDURE [dbo].[spPMGetActivity]
	@token_id NVARCHAR(50), 
	@filter_act_id_Pk BIGINT = NULL, 
	@filter_act_external_identifier NVARCHAR(500) = NULL,
	@filter_act_name VARCHAR(200) = NULL, 
	@filter_act_description VARCHAR(MAX) = NULL, 
	@filter_act_execution_start_at DATETIME = NULL, 
	@filter_act_execution_finished_at DATETIME = NULL, 
	@filter_act_total_duration INT = NULL, 
	@filter_act_status VARCHAR(100) = NULL, 
	@filter_act_comments VARCHAR(MAX) = NULL, 
	@filter_act_maintenance_plan BIGINT = NULL
AS
	DECLARE @v_token_user nvarchar(20);
	DECLARE @v_token_employee_id int;
	BEGIN

		SELECT
			@v_token_employee_id = [TBL_SRA_EMPLOYEE].[id], 
			@v_token_user = [TBL_SCMS_TOKEN].[token_user_id]
		FROM [TBL_SRA_EMPLOYEE]
			INNER JOIN [TBL_SCMS_USER] ON [TBL_SCMS_USER].[user_id] = [TBL_SRA_EMPLOYEE].[user]
			INNER JOIN [TBL_SCMS_TOKEN] ON [TBL_SCMS_TOKEN].[token_user_id] = [TBL_SCMS_USER].[user_id]
				AND [TBL_SCMS_TOKEN].[token_id] = @token_id
				AND [TBL_SCMS_TOKEN].[token_expires_at] > GETDATE();

		IF @v_token_employee_id IS NULL
			RAISERROR ('Invalid token', 16, 1);
		ELSE
			SELECT
				[TBL_PM_ACTIVITY].[act_id_Pk],
				[TBL_PM_ACTIVITY].[act_external_identifier], 
				[TBL_PM_ACTIVITY].[act_name], 
				[TBL_PM_ACTIVITY].[act_description], 
				[TBL_PM_ACTIVITY].[act_execution_start_at], 
				[TBL_PM_ACTIVITY].[act_execution_finished_at], 
				[TBL_PM_ACTIVITY].[act_total_duration], 
				[TBL_PM_ACTIVITY].[act_status], 
				[TBL_PM_ACTIVITY].[act_comments], 
				[TBL_PM_ACTIVITY].act_maintenance_plan
			FROM [TBL_PM_ACTIVITY]
				INNER JOIN [TBL_PM_MAINTENANCE_PLAN] ON [TBL_PM_MAINTENANCE_PLAN].[mntpln_id_Pk] = [TBL_PM_ACTIVITY].[act_maintenance_plan]
			WHERE 
				[TBL_PM_ACTIVITY].[act_id_Pk] = ISNULL(@filter_act_id_Pk, [TBL_PM_ACTIVITY].[act_id_Pk])
				AND [TBL_PM_ACTIVITY].[act_external_identifier] = ISNULL(@filter_act_external_identifier, [TBL_PM_ACTIVITY].[act_external_identifier])
				AND [TBL_PM_ACTIVITY].[act_name] = ISNULL(@filter_act_name, [TBL_PM_ACTIVITY].[act_name])
				AND [TBL_PM_ACTIVITY].[act_description] = ISNULL(@filter_act_description, [TBL_PM_ACTIVITY].[act_description])
				AND [TBL_PM_ACTIVITY].[act_execution_start_at] = ISNULL(@filter_act_execution_start_at, [TBL_PM_ACTIVITY].[act_execution_start_at])
				AND [TBL_PM_ACTIVITY].[act_execution_finished_at] = ISNULL(@filter_act_execution_finished_at, [TBL_PM_ACTIVITY].[act_execution_finished_at])
				AND [TBL_PM_ACTIVITY].[act_total_duration] = ISNULL(@filter_act_total_duration, [TBL_PM_ACTIVITY].[act_total_duration])
				AND [TBL_PM_ACTIVITY].[act_status] = ISNULL(@filter_act_status, [TBL_PM_ACTIVITY].[act_status])
				AND [TBL_PM_ACTIVITY].[act_comments] = ISNULL(@filter_act_comments, [TBL_PM_ACTIVITY].[act_comments])
				AND [TBL_PM_ACTIVITY].[act_maintenance_plan] = ISNULL(@filter_act_maintenance_plan, [TBL_PM_ACTIVITY].[act_maintenance_plan]);

	END
RETURN 0									
