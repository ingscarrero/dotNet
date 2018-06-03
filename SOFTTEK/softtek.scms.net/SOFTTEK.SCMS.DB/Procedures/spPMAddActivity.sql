CREATE PROCEDURE [dbo].[spPMAddActivity]
	@token_id NVARCHAR(50), 
	@new_act_external_identifier NVARCHAR(500),
	@new_act_name VARCHAR(200), 
	@new_act_description VARCHAR(MAX), 
	@new_act_execution_start_at DATETIME, 
	@new_act_execution_finished_at DATETIME, 
	@new_act_total_duration INT, 
	@new_act_status VARCHAR(100), 
	@new_act_comments VARCHAR(MAX), 
	@new_act_maintenance_plan BIGINT
AS
	DECLARE @v_token_user nvarchar(20);
	DECLARE @v_token_employee_id int;
	DECLARE @v_CTIVITY table (
		v_act_id_Pk BIGINT, 
		v_act_external_identifier NVARCHAR(500),
		v_act_name VARCHAR(200), 
		v_act_description VARCHAR(MAX), 
		v_act_execution_start_at DATETIME, 
		v_act_execution_finished_at DATETIME, 
		v_act_total_duration INT, 
		v_act_status VARCHAR(100), 
		v_act_comments VARCHAR(MAX), 
		v_act_maintenance_plan BIGINT
	);

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
			INSERT INTO [TBL_PM_ACTIVITY] (
				[TBL_PM_ACTIVITY].[act_external_identifier],
				[TBL_PM_ACTIVITY].[act_name], 
				[TBL_PM_ACTIVITY].[act_description], 
				[TBL_PM_ACTIVITY].[act_execution_start_at], 
				[TBL_PM_ACTIVITY].[act_execution_finished_at], 
				[TBL_PM_ACTIVITY].[act_total_duration], 
				[TBL_PM_ACTIVITY].[act_status], 
				[TBL_PM_ACTIVITY].[act_comments], 
				[TBL_PM_ACTIVITY].[act_maintenance_plan])
			OUTPUT 
				inserted.[act_id_Pk], 
				inserted.act_external_identifier,
				inserted.[act_name], 
				inserted.[act_description], 
				inserted.[act_execution_start_at], 
				inserted.[act_execution_finished_at], 
				inserted.[act_total_duration], 
				inserted.[act_status], 
				inserted.[act_comments], 
				inserted.[act_maintenance_plan]
			INTO @v_CTIVITY
			VALUES (
				@new_act_external_identifier,
				@new_act_name, 
				@new_act_description, 
				@new_act_execution_start_at, 
				@new_act_execution_finished_at, 
				@new_act_total_duration, 
				@new_act_status, 
				@new_act_comments, 
				@new_act_maintenance_plan);

			SELECT 
				v_act_id_Pk, 
				v_act_external_identifier
				v_act_name, 
				v_act_description, 
				v_act_execution_start_at, 
				v_act_execution_finished_at, 
				v_act_total_duration, 
				v_act_status, 
				v_act_comments, 
				v_act_maintenance_plan
			FROM @v_CTIVITY;
	END
RETURN 0									
