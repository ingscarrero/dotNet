CREATE PROCEDURE [dbo].[spPMUpdateTask]
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
	@filter_tsk_work_order_Fk bigint = NULL, 
	@new_tsk_external_identifier nvarchar(500) = NULL, 
	@new_tsk_name nvarchar(200) = NULL, 
	@new_tsk_description nvarchar(MAX) = NULL, 
	@new_tsk_performer nvarchar(50) = NULL, 
	@new_tsk_started_at datetime = NULL, 
	@new_tsk_finished_at datetime = NULL, 
	@new_tsk_status nvarchar(50) = NULL, 
	@new_tsk_comments nvarchar(300) = NULL, 
	@new_tsk_quantity_capacity nvarchar(50) = NULL,
	@new_tsk_duration_operation nvarchar(50) = NULL,
	@new_tsk_plan_Fk bigint = NULL, 
	@new_tsk_work_order_Fk bigint = NULL
AS
	DECLARE @v_token_user nvarchar(20);
	DECLARE @v_token_employee_id int;
	DECLARE @v_ASK table (
		v_tsk_id_Pk bigint, 
		v_tsk_external_identifier nvarchar(500), 
		v_tsk_name nvarchar(200), 
		v_tsk_description nvarchar(MAX), 
		v_tsk_performer nvarchar(50), 
		v_tsk_started_at datetime, 
		v_tsk_finished_at datetime, 
		v_tsk_status nvarchar(50), 
		v_tsk_comments nvarchar(300), 
		v_tsk_quantity_capacity nvarchar(50),
		v_tsk_duration_operation nvarchar(50),
		v_tsk_plan_Fk bigint, 
		v_tsk_work_order_Fk bigint
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
			UPDATE [TBL_PM_TASK]
			SET
				[TBL_PM_TASK].[tsk_external_identifier] = ISNULL(@new_tsk_external_identifier, [TBL_PM_TASK].[tsk_external_identifier]), 
				[TBL_PM_TASK].[tsk_name] = ISNULL(@new_tsk_name, [TBL_PM_TASK].[tsk_name]), 
				[TBL_PM_TASK].[tsk_description] = ISNULL(@new_tsk_description, [TBL_PM_TASK].[tsk_description]), 
				[TBL_PM_TASK].[tsk_performer] = ISNULL(@new_tsk_performer, [TBL_PM_TASK].[tsk_performer]), 
				[TBL_PM_TASK].[tsk_started_at] = ISNULL(@new_tsk_started_at, [TBL_PM_TASK].[tsk_started_at]), 
				[TBL_PM_TASK].[tsk_finished_at] = ISNULL(@new_tsk_finished_at, [TBL_PM_TASK].[tsk_finished_at]), 
				[TBL_PM_TASK].[tsk_status] = ISNULL(@new_tsk_status, [TBL_PM_TASK].[tsk_status]), 
				[TBL_PM_TASK].[tsk_comments] = ISNULL(@new_tsk_comments, [TBL_PM_TASK].[tsk_comments]), 
				[TBL_PM_TASK].[tsk_quantity_capacity] = ISNULL(@new_tsk_quantity_capacity, [TBL_PM_TASK].[tsk_quantity_capacity]), 
				[TBL_PM_TASK].[tsk_duration_operation] = ISNULL(@new_tsk_duration_operation, [TBL_PM_TASK].[tsk_duration_operation]), 
				[TBL_PM_TASK].[tsk_plan_Fk] = ISNULL(@new_tsk_plan_Fk, [TBL_PM_TASK].[tsk_plan_Fk]), 
				[TBL_PM_TASK].[tsk_work_order_Fk] = ISNULL(@new_tsk_work_order_Fk, [TBL_PM_TASK].[tsk_work_order_Fk])
			OUTPUT 
				inserted.[tsk_id_Pk], 
				inserted.[tsk_external_identifier], 
				inserted.[tsk_name], 
				inserted.[tsk_description], 
				inserted.[tsk_performer], 
				inserted.[tsk_started_at], 
				inserted.[tsk_finished_at], 
				inserted.[tsk_status], 
				inserted.[tsk_comments], 
				inserted.[tsk_quantity_capacity],
				inserted.[tsk_duration_operation],
				inserted.[tsk_plan_Fk], 
				inserted.[tsk_work_order_Fk]
			INTO @v_ASK
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

			SELECT 
				v_tsk_id_Pk, 
				v_tsk_external_identifier, 
				v_tsk_name, 
				v_tsk_description, 
				v_tsk_performer, 
				v_tsk_started_at, 
				v_tsk_finished_at, 
				v_tsk_status, 
				v_tsk_comments, 
				v_tsk_quantity_capacity,
				v_tsk_duration_operation,
				v_tsk_plan_Fk, 
				v_tsk_work_order_Fk
			FROM @v_ASK;
	END
RETURN 0							
