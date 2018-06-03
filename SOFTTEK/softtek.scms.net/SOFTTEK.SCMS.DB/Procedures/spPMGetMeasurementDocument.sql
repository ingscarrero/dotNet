CREATE PROCEDURE [dbo].[spPMGetMeasurementDocument]
	@token_id NVARCHAR(50), 
	@filter_msrdcm_id_Pk BIGINT = NULL, 
	@filter_msrdcm_external_identifier NVARCHAR(500) = NULL, 
	@filter_msrdcm_task_Fk BIGINT = NULL, 
	@filter_msrdcm_device_type NVARCHAR(20) = NULL,
	@filter_msrdcm_technical_object_Fk BIGINT = NULL
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
				[TBL_PM_MEASUREMENT_DOCUMENT].[msrdcm_id_Pk], 
				[TBL_PM_MEASUREMENT_DOCUMENT].[msrdcm_external_identifier], 
				[TBL_PM_TASK].[tsk_id_Pk], 
				[TBL_PM_MEASUREMENT_DOCUMENT].[msrdcm_device_type],
				[TBL_PM_TECHNICALOBJECT].[tchobj_id_Pk]
			FROM [TBL_PM_MEASUREMENT_DOCUMENT]
				INNER JOIN [TBL_PM_TASK] ON [TBL_PM_TASK].[tsk_id_Pk] = [TBL_PM_MEASUREMENT_DOCUMENT].[msrdcm_task_Fk]
				INNER JOIN [TBL_PM_TECHNICALOBJECT] ON [TBL_PM_TECHNICALOBJECT].[tchobj_id_Pk] = [TBL_PM_MEASUREMENT_DOCUMENT].[msrdcm_technical_object_Fk]
			WHERE 
				[TBL_PM_MEASUREMENT_DOCUMENT].[msrdcm_id_Pk] = ISNULL(@filter_msrdcm_id_Pk, [TBL_PM_MEASUREMENT_DOCUMENT].[msrdcm_id_Pk])
				AND [TBL_PM_MEASUREMENT_DOCUMENT].[msrdcm_external_identifier] = ISNULL(@filter_msrdcm_external_identifier, [TBL_PM_MEASUREMENT_DOCUMENT].[msrdcm_external_identifier])
				AND [TBL_PM_MEASUREMENT_DOCUMENT].[msrdcm_task_Fk] = ISNULL(@filter_msrdcm_task_Fk, [TBL_PM_MEASUREMENT_DOCUMENT].[msrdcm_task_Fk])
				AND [TBL_PM_MEASUREMENT_DOCUMENT].[msrdcm_device_type] = ISNULL(@filter_msrdcm_device_type, [TBL_PM_MEASUREMENT_DOCUMENT].[msrdcm_device_type])
				AND [TBL_PM_MEASUREMENT_DOCUMENT].[msrdcm_technical_object_Fk] = ISNULL(@filter_msrdcm_technical_object_Fk, [TBL_PM_MEASUREMENT_DOCUMENT].[msrdcm_technical_object_Fk]);

	END
RETURN 0									
