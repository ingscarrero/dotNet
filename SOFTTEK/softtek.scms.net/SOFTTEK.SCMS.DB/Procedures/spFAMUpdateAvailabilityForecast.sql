CREATE PROCEDURE [dbo].[spFAMUpdateAvailabilityForecast]
	@token_id NVARCHAR(50), 
	@filter_avlfrc_id_Pk BIGINT = NULL, 
	@filter_avlfrc_request_Fk BIGINT = NULL, 
	@filter_avlfrc_generated_at DATETIME = NULL, 
	@filter_avlfrc_valid_until DATETIME = NULL, 
	@filter_avlfrc_validated_by_Fk INT  = NULL, 
	@new_avlfrc_request_Fk BIGINT = NULL, 
	@new_avlfrc_generated_at DATETIME = NULL, 
	@new_avlfrc_valid_until DATETIME = NULL, 
	@new_avlfrc_validated_by_Fk INT  = NULL
AS
	DECLARE @v_token_user nvarchar(20);
	DECLARE @v_token_employee_id int;
	DECLARE @v_AVAILABILITY_FORECAST table (
		v_avlfrc_id_Pk BIGINT, 
		v_avlfrc_request_Fk BIGINT, 
		v_avlfrc_generated_at DATETIME, 
		v_avlfrc_valid_until DATETIME, 
		v_avlfrc_validated_by_Fk INT 
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
			UPDATE [TBL_FAM_AVAILABILITY_FORECAST]
			SET
				[TBL_FAM_AVAILABILITY_FORECAST].[avlfrc_request_Fk] = ISNULL(@new_avlfrc_request_Fk, [TBL_FAM_AVAILABILITY_FORECAST].[avlfrc_request_Fk]), 
				[TBL_FAM_AVAILABILITY_FORECAST].[avlfrc_generated_at] = ISNULL(@new_avlfrc_generated_at, [TBL_FAM_AVAILABILITY_FORECAST].[avlfrc_generated_at]), 
				[TBL_FAM_AVAILABILITY_FORECAST].[avlfrc_valid_until] = ISNULL(@new_avlfrc_valid_until, [TBL_FAM_AVAILABILITY_FORECAST].[avlfrc_valid_until]), 
				[TBL_FAM_AVAILABILITY_FORECAST].[avlfrc_validated_by_Fk] = ISNULL(@new_avlfrc_validated_by_Fk, [TBL_FAM_AVAILABILITY_FORECAST].[avlfrc_validated_by_Fk])
			OUTPUT 
				inserted.[avlfrc_id_Pk], 
				inserted.[avlfrc_request_Fk], 
				inserted.[avlfrc_generated_at], 
				inserted.[avlfrc_valid_until], 
				inserted.[avlfrc_validated_by_Fk]
			INTO @v_AVAILABILITY_FORECAST
			WHERE 
				[TBL_FAM_AVAILABILITY_FORECAST].[avlfrc_id_Pk] = ISNULL(@filter_avlfrc_id_Pk, [TBL_FAM_AVAILABILITY_FORECAST].[avlfrc_id_Pk])
				AND [TBL_FAM_AVAILABILITY_FORECAST].[avlfrc_request_Fk] = ISNULL(@filter_avlfrc_request_Fk, [TBL_FAM_AVAILABILITY_FORECAST].[avlfrc_request_Fk])
				AND [TBL_FAM_AVAILABILITY_FORECAST].[avlfrc_generated_at] = ISNULL(@filter_avlfrc_generated_at, [TBL_FAM_AVAILABILITY_FORECAST].[avlfrc_generated_at])
				AND [TBL_FAM_AVAILABILITY_FORECAST].[avlfrc_valid_until] = ISNULL(@filter_avlfrc_valid_until, [TBL_FAM_AVAILABILITY_FORECAST].[avlfrc_valid_until])
				AND [TBL_FAM_AVAILABILITY_FORECAST].[avlfrc_validated_by_Fk] = ISNULL(@filter_avlfrc_validated_by_Fk, [TBL_FAM_AVAILABILITY_FORECAST].[avlfrc_validated_by_Fk]);

			SELECT 
				v_avlfrc_id_Pk, 
				v_avlfrc_request_Fk, 
				v_avlfrc_generated_at, 
				v_avlfrc_valid_until, 
				v_avlfrc_validated_by_Fk
			FROM @v_AVAILABILITY_FORECAST;
	END
RETURN 0									
