CREATE PROCEDURE [dbo].[spPMUpdateMeasure]
	@token_id NVARCHAR(50), 
	@filter_msr_id_Pk BIGINT = NULL, 
	@filter_msr_unity_of_measurement NVARCHAR(500) = NULL, 
	@filter_msr_value NVARCHAR(500) = NULL, 
	@filter_msr_external_identifier NVARCHAR(500) = NULL, 
	@filter_msr_comments NVARCHAR(500) = NULL, 
	@filter_msr_document_Fk BIGINT = NULL, 
	@filter_msr_device_type NVARCHAR(20) = NULL, 
	@filter_msr_technical_object_Fk BIGINT = NULL,
	@new_msr_unity_of_measurement NVARCHAR(500) = NULL, 
	@new_msr_value NVARCHAR(500) = NULL, 
	@new_msr_external_identifier NVARCHAR(500) = NULL, 
	@new_msr_comments NVARCHAR(500) = NULL, 
	@new_msr_document_Fk BIGINT = NULL, 
	@new_msr_device_type NVARCHAR(20) = NULL,
	@new_msr_technical_object_Fk BIGINT = NULL
AS
	DECLARE @v_token_user nvarchar(20);
	DECLARE @v_token_employee_id int;
	DECLARE @v_EASURE table (
		v_msr_id_Pk BIGINT, 
		v_msr_unity_of_measurement NVARCHAR(500), 
		v_msr_value NVARCHAR(500), 
		v_msr_external_identifier NVARCHAR(500), 
		v_msr_comments NVARCHAR(500), 
		v_msr_document_Fk BIGINT, 
		v_msr_device_type NVARCHAR(20),
		v_msr_technical_object_Fk BIGINT 
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
			UPDATE [TBL_PM_MEASURE]
			SET
				[TBL_PM_MEASURE].[msr_unity_of_measurement] = ISNULL(@new_msr_unity_of_measurement, [TBL_PM_MEASURE].[msr_unity_of_measurement]), 
				[TBL_PM_MEASURE].[msr_value] = ISNULL(@new_msr_value, [TBL_PM_MEASURE].[msr_value]), 
				[TBL_PM_MEASURE].[msr_external_identifier] = ISNULL(@new_msr_external_identifier, [TBL_PM_MEASURE].[msr_external_identifier]), 
				[TBL_PM_MEASURE].[msr_comments] = ISNULL(@new_msr_comments, [TBL_PM_MEASURE].[msr_comments]), 
				[TBL_PM_MEASURE].[msr_document_Fk] = ISNULL(@new_msr_document_Fk, [TBL_PM_MEASURE].[msr_document_Fk]), 
				[TBL_PM_MEASURE].[msr_device_type] = ISNULL(@new_msr_device_type, [TBL_PM_MEASURE].[msr_device_type]),
				[TBL_PM_MEASURE].[msr_technical_object_Fk] = ISNULL(@new_msr_technical_object_Fk, [TBL_PM_MEASURE].[msr_technical_object_Fk])
			OUTPUT 
				inserted.[msr_id_Pk], 
				inserted.[msr_unity_of_measurement], 
				inserted.[msr_value], 
				inserted.[msr_external_identifier], 
				inserted.[msr_comments], 
				inserted.[msr_document_Fk], 
				inserted.[msr_device_type],
				inserted.[msr_technical_object_Fk]
			INTO @v_EASURE
			WHERE 
				[TBL_PM_MEASURE].[msr_id_Pk] = ISNULL(@filter_msr_id_Pk, [TBL_PM_MEASURE].[msr_id_Pk])
				AND [TBL_PM_MEASURE].[msr_unity_of_measurement] = ISNULL(@filter_msr_unity_of_measurement, [TBL_PM_MEASURE].[msr_unity_of_measurement])
				AND [TBL_PM_MEASURE].[msr_value] = ISNULL(@filter_msr_value, [TBL_PM_MEASURE].[msr_value])
				AND [TBL_PM_MEASURE].[msr_external_identifier] = ISNULL(@filter_msr_external_identifier, [TBL_PM_MEASURE].[msr_external_identifier])
				AND [TBL_PM_MEASURE].[msr_comments] = ISNULL(@filter_msr_comments, [TBL_PM_MEASURE].[msr_comments])
				AND [TBL_PM_MEASURE].[msr_document_Fk] = ISNULL(@filter_msr_document_Fk, [TBL_PM_MEASURE].[msr_document_Fk])
				AND [TBL_PM_MEASURE].[msr_device_type] = ISNULL(@filter_msr_device_type, [TBL_PM_MEASURE].[msr_device_type])
				AND [TBL_PM_MEASURE].[msr_technical_object_Fk] = ISNULL(@filter_msr_technical_object_Fk, [TBL_PM_MEASURE].[msr_technical_object_Fk]);;

			SELECT 
				v_msr_id_Pk, 
				v_msr_unity_of_measurement, 
				v_msr_value, 
				v_msr_external_identifier, 
				v_msr_comments, 
				v_msr_document_Fk, 
				v_msr_device_type,
				v_msr_technical_object_Fk
			FROM @v_EASURE;
	END
RETURN 0									
