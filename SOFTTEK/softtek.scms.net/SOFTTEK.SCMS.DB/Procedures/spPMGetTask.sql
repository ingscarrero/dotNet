CREATE PROCEDURE [dbo].[spPMGetTask]
	@token_id NVARCHAR(50), 
	@filter_tsk_id_Pk bigint = NULL, 
	@filter_tsk_external_identifier nvarchar(500) = NULL, 
	@filter_tsk_name nvarchar(200) = NULL, 
	@filter_tsk_description nvarchar(MAX) = NULL, 
	@filter_tsk_performer nvarchar(50) = NULL, 
	@filter_tsk_started_at datetime = NULL, 
	@filter_tsk_finished_at datetime = NULL, 
	@filter_tsk_status nvarchar(50) = NULL, 
	@filter_tsk_comments nvarchar(300) = NULL, 
	@filter_tsk_quantity_capacity nvarchar(50) = NULL,
	@filter_tsk_duration_operation nvarchar(50) = NULL,
	@filter_tsk_plan_Fk bigint = NULL, 
	@filter_tsk_work_order_Fk bigint = NULL
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
				[TBL_PM_TASK].[tsk_id_Pk], 
				[TBL_PM_TASK].[tsk_external_identifier], 
				[TBL_PM_TASK].[tsk_name], 
				[TBL_PM_TASK].[tsk_description], 
				[TBL_PM_TASK].[tsk_performer], 
				[TBL_PM_TASK].[tsk_started_at], 
				[TBL_PM_TASK].[tsk_finished_at], 
				[TBL_PM_TASK].[tsk_status], 
				[TBL_PM_TASK].[tsk_comments], 
				[TBL_PM_TASK].[tsk_quantity_capacity],
				[TBL_PM_TASK].[tsk_duration_operation],
				[TBL_PM_TASK].tsk_plan_Fk, 
				[TBL_PM_TASK].tsk_work_order_Fk
			FROM [TBL_PM_TASK]
				INNER JOIN [TBL_PM_MAINTENANCE_PLAN] ON [TBL_PM_MAINTENANCE_PLAN].[mntpln_id_Pk] = [TBL_PM_TASK].[tsk_plan_Fk]
				INNER JOIN [TBL_PM_WORK_ORDER] ON [TBL_PM_WORK_ORDER].[wrkord_id_Pk] = [TBL_PM_TASK].[tsk_work_order_Fk]
			WHERE 
				[TBL_PM_TASK].[tsk_id_Pk] = ISNULL(@filter_tsk_id_Pk, [TBL_PM_TASK].[tsk_id_Pk])
				AND [TBL_PM_TASK].[tsk_external_identifier] = ISNULL(@filter_tsk_external_identifier, [TBL_PM_TASK].[tsk_external_identifier])
				AND [TBL_PM_TASK].[tsk_name] = ISNULL(@filter_tsk_name, [TBL_PM_TASK].[tsk_name])
				AND [TBL_PM_TASK].[tsk_description] = ISNULL(@filter_tsk_description, [TBL_PM_TASK].[tsk_description])
				AND [TBL_PM_TASK].[tsk_performer] = ISNULL(@filter_tsk_performer, [TBL_PM_TASK].[tsk_performer])
				AND [TBL_PM_TASK].[tsk_started_at] = ISNULL(@filter_tsk_started_at, [TBL_PM_TASK].[tsk_started_at])
				AND [TBL_PM_TASK].[tsk_finished_at] = ISNULL(@filter_tsk_finished_at, [TBL_PM_TASK].[tsk_finished_at])
				AND [TBL_PM_TASK].[tsk_status] = ISNULL(@filter_tsk_status, [TBL_PM_TASK].[tsk_status])
				AND [TBL_PM_TASK].[tsk_comments] = ISNULL(@filter_tsk_comments, [TBL_PM_TASK].[tsk_comments])
				AND [TBL_PM_TASK].[tsk_quantity_capacity] = ISNULL(@filter_tsk_quantity_capacity, [TBL_PM_TASK].[tsk_quantity_capacity])
				AND [TBL_PM_TASK].[tsk_duration_operation] = ISNULL(@filter_tsk_duration_operation, [TBL_PM_TASK].[tsk_duration_operation])
				AND [TBL_PM_TASK].[tsk_plan_Fk] = ISNULL(@filter_tsk_plan_Fk, [TBL_PM_TASK].[tsk_plan_Fk])
				AND [TBL_PM_TASK].[tsk_work_order_Fk] = ISNULL(@filter_tsk_work_order_Fk, [TBL_PM_TASK].[tsk_work_order_Fk]);

	END
RETURN 0									
