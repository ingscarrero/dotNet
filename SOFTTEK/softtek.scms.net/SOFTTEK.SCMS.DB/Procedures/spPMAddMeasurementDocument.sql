CREATE PROCEDURE [dbo].[spPMAddMeasurementDocument]
	@token_id NVARCHAR(50), 
	@new_msrdcm_external_identifier NVARCHAR(500), 
	@new_msrdcm_task_Fk BIGINT, 
	@new_msrdcm_device_type NVARCHAR(20),
	@new_msrdcm_technical_object_Fk BIGINT
AS
	DECLARE @v_token_user nvarchar(20);
	DECLARE @v_token_employee_id int;
	DECLARE @v_EASUREMENT_DOCUMENT table (
		v_msrdcm_id_Pk BIGINT, 
		v_msrdcm_external_identifier NVARCHAR(500), 
		v_msrdcm_task_Fk BIGINT, 
		v_msrdcm_device_type NVARCHAR(20),
		v_msrdcm_technical_object_Fk BIGINT
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
			INSERT INTO [TBL_PM_MEASUREMENT_DOCUMENT] (
				[TBL_PM_MEASUREMENT_DOCUMENT].[msrdcm_external_identifier], 
				[TBL_PM_MEASUREMENT_DOCUMENT].[msrdcm_task_Fk], 
				[TBL_PM_MEASUREMENT_DOCUMENT].[msrdcm_device_type],
				[TBL_PM_MEASUREMENT_DOCUMENT].[msrdcm_technical_object_Fk])
			OUTPUT 
				inserted.[msrdcm_id_Pk], 
				inserted.[msrdcm_external_identifier], 
				inserted.[msrdcm_task_Fk], 
				inserted.[msrdcm_device_type],
				inserted.[msrdcm_technical_object_Fk]
			INTO @v_EASUREMENT_DOCUMENT
			VALUES (
				@new_msrdcm_external_identifier, 
				@new_msrdcm_task_Fk, 
				@new_msrdcm_device_type,
				@new_msrdcm_technical_object_Fk);

			SELECT 
				v_msrdcm_id_Pk, 
				v_msrdcm_external_identifier, 
				v_msrdcm_task_Fk, 
				v_msrdcm_device_type,
				v_msrdcm_technical_object_Fk
			FROM @v_EASUREMENT_DOCUMENT;
	END
RETURN 0									
