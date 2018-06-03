CREATE PROCEDURE [dbo].[spPMUpdateAdvice]
	@token_id NVARCHAR(50), 
	@filter_adv_id_Pk BIGINT = NULL, 
	@filter_adv_external_identifier NVARCHAR(500) = NULL, 
	@filter_adv_priority NVARCHAR(20) = NULL, 
	@filter_adv_type NVARCHAR(20) = NULL, 
	@filter_adv_task_Fk BIGINT = NULL, 
	@filter_adv_device_type NVARCHAR(20) = NULL, 
	@filter_adv_technical_object_Fk BIGINT = NULL, 
	@filter_adv_comments NVARCHAR(500) = NULL, 
	@filter_adv_execution_hour_start_at NVARCHAR(20) = NULL,
	@filter_adv_execution_hour_finished_at NVARCHAR(20) = NULL,
	@new_adv_external_identifier NVARCHAR(500) = NULL, 
	@new_adv_priority NVARCHAR(20) = NULL, 
	@new_adv_type NVARCHAR(20) = NULL, 
	@new_adv_task_Fk BIGINT = NULL, 
	@new_adv_device_type NVARCHAR(20) = NULL, 
	@new_adv_technical_object_Fk BIGINT = NULL, 
	@new_adv_comments NVARCHAR(500) = NULL,
	@new_adv_scheduled_to DATETIME = NULL,
	@new_adv_execution_start_at DATETIME = NULL,
	@new_adv_execution_finished_at DATETIME = NULL,
	@new_adv_execution_hour_start_at NVARCHAR(20) = NULL,
	@new_adv_execution_hour_finished_at NVARCHAR(20) = NULL
AS
	DECLARE @v_token_user nvarchar(20);
	DECLARE @v_token_employee_id int;
	DECLARE @v_DVICE table (
		v_adv_id_Pk BIGINT, 
		v_adv_external_identifier NVARCHAR(500), 
		v_adv_priority NVARCHAR(20), 
		v_adv_type NVARCHAR(20), 
		v_adv_task_Fk BIGINT, 
		v_adv_device_type NVARCHAR(20), 
		v_adv_technical_object_Fk BIGINT, 
		v_adv_comments NVARCHAR(500),
		v_adv_scheduled_to DATETIME,
		v_adv_execution_start_at DATETIME,
		v_adv_execution_finished_at DATETIME,
		v_adv_execution_hour_start_at NVARCHAR(20),
		v_adv_execution_hour_finished_at NVARCHAR(20)
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
			UPDATE [TBL_PM_ADVICE]
			SET
				[TBL_PM_ADVICE].[adv_external_identifier] = ISNULL(@new_adv_external_identifier, [TBL_PM_ADVICE].[adv_external_identifier]), 
				[TBL_PM_ADVICE].[adv_priority] = ISNULL(@new_adv_priority, [TBL_PM_ADVICE].[adv_priority]), 
				[TBL_PM_ADVICE].[adv_type] = ISNULL(@new_adv_type, [TBL_PM_ADVICE].[adv_type]), 
				[TBL_PM_ADVICE].[adv_task_Fk] = ISNULL(@new_adv_task_Fk, [TBL_PM_ADVICE].[adv_task_Fk]), 
				[TBL_PM_ADVICE].[adv_device_type] = ISNULL(@new_adv_device_type, [TBL_PM_ADVICE].[adv_device_type]), 
				[TBL_PM_ADVICE].[adv_technical_object_Fk] = ISNULL(@new_adv_technical_object_Fk, [TBL_PM_ADVICE].[adv_technical_object_Fk]), 
				[TBL_PM_ADVICE].[adv_comments] = ISNULL(@new_adv_comments, [TBL_PM_ADVICE].[adv_comments]),
				[TBL_PM_ADVICE].[adv_scheduled_to] = ISNULL(@new_adv_scheduled_to, [TBL_PM_ADVICE].[adv_scheduled_to]),
				[TBL_PM_ADVICE].[adv_execution_start_at] = ISNULL(@new_adv_execution_start_at, [TBL_PM_ADVICE].[adv_execution_start_at]),
				[TBL_PM_ADVICE].[adv_execution_finished_at] = ISNULL(@new_adv_execution_finished_at, [TBL_PM_ADVICE].[adv_execution_finished_at]),
				[TBL_PM_ADVICE].[adv_execution_hour_start_at] = ISNULL(@new_adv_execution_hour_start_at, [TBL_PM_ADVICE].[adv_execution_hour_start_at]),
				[TBL_PM_ADVICE].[adv_execution_hour_finished_at] = ISNULL(@new_adv_execution_hour_finished_at, [TBL_PM_ADVICE].[adv_execution_hour_finished_at])
			OUTPUT 
				inserted.[adv_id_Pk], 
				inserted.[adv_external_identifier], 
				inserted.[adv_priority], 
				inserted.[adv_type], 
				inserted.[adv_task_Fk], 
				inserted.[adv_device_type], 
				inserted.[adv_technical_object_Fk], 
				inserted.[adv_comments],
				inserted.[adv_scheduled_to],
				inserted.[adv_execution_start_at],
				inserted.[adv_execution_finished_at],
				inserted.[adv_execution_hour_start_at],
				inserted.[adv_execution_hour_finished_at]
			INTO @v_DVICE
			WHERE 
				[TBL_PM_ADVICE].[adv_id_Pk] = ISNULL(@filter_adv_id_Pk, [TBL_PM_ADVICE].[adv_id_Pk])
				AND [TBL_PM_ADVICE].[adv_external_identifier] = ISNULL(@filter_adv_external_identifier, [TBL_PM_ADVICE].[adv_external_identifier])
				AND [TBL_PM_ADVICE].[adv_priority] = ISNULL(@filter_adv_priority, [TBL_PM_ADVICE].[adv_priority])
				AND [TBL_PM_ADVICE].[adv_type] = ISNULL(@filter_adv_type, [TBL_PM_ADVICE].[adv_type])
				AND [TBL_PM_ADVICE].[adv_task_Fk] = ISNULL(@filter_adv_task_Fk, [TBL_PM_ADVICE].[adv_task_Fk])
				AND [TBL_PM_ADVICE].[adv_device_type] = ISNULL(@filter_adv_device_type, [TBL_PM_ADVICE].[adv_device_type])
				AND [TBL_PM_ADVICE].[adv_technical_object_Fk] = ISNULL(@filter_adv_technical_object_Fk, [TBL_PM_ADVICE].[adv_technical_object_Fk])
				AND [TBL_PM_ADVICE].[adv_comments] = ISNULL(@filter_adv_comments, [TBL_PM_ADVICE].[adv_comments]);

			SELECT 
				v_adv_id_Pk, 
				v_adv_external_identifier, 
				v_adv_priority, 
				v_adv_type, 
				v_adv_task_Fk, 
				v_adv_device_type, 
				v_adv_technical_object_Fk, 
				v_adv_comments,
				v_adv_scheduled_to,
				v_adv_execution_start_at,
				v_adv_execution_finished_at,
				v_adv_execution_hour_start_at,
				v_adv_execution_hour_finished_at
			FROM @v_DVICE;
	END
RETURN 0								
