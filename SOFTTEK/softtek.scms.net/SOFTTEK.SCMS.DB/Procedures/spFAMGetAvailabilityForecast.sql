CREATE PROCEDURE [dbo].[spFAMGetAvailabilityForecast]
	@token_id NVARCHAR(50), 
	@filter_avlfrc_id_Pk BIGINT = NULL, 
	@filter_avlfrc_request_Fk BIGINT = NULL, 
	@filter_avlfrc_generated_at DATETIME = NULL, 
	@filter_avlfrc_valid_until DATETIME = NULL, 
	@filter_avlfrc_validated_by_Fk INT  = NULL
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
				[TBL_FAM_AVAILABILITY_FORECAST].[avlfrc_id_Pk], 
				[TBL_FAM_REQUEST].[rqs_id_Pk], 
				[TBL_FAM_AVAILABILITY_FORECAST].[avlfrc_generated_at], 
				[TBL_FAM_AVAILABILITY_FORECAST].[avlfrc_valid_until], 
				[TBL_SRA_EMPLOYEE].[id]
			FROM [TBL_FAM_AVAILABILITY_FORECAST]
				INNER JOIN [TBL_FAM_REQUEST] ON [TBL_FAM_REQUEST].[rqs_id_Pk] = [TBL_FAM_AVAILABILITY_FORECAST].[avlfrc_request_Fk]
				INNER JOIN [TBL_SRA_EMPLOYEE] ON [TBL_SRA_EMPLOYEE].[id] = [TBL_FAM_AVAILABILITY_FORECAST].[avlfrc_validated_by_Fk]
			WHERE 
				[TBL_FAM_AVAILABILITY_FORECAST].[avlfrc_id_Pk] = ISNULL(@filter_avlfrc_id_Pk, [TBL_FAM_AVAILABILITY_FORECAST].[avlfrc_id_Pk])
				AND [TBL_FAM_AVAILABILITY_FORECAST].[avlfrc_request_Fk] = ISNULL(@filter_avlfrc_request_Fk, [TBL_FAM_AVAILABILITY_FORECAST].[avlfrc_request_Fk])
				AND [TBL_FAM_AVAILABILITY_FORECAST].[avlfrc_generated_at] = ISNULL(@filter_avlfrc_generated_at, [TBL_FAM_AVAILABILITY_FORECAST].[avlfrc_generated_at])
				AND [TBL_FAM_AVAILABILITY_FORECAST].[avlfrc_valid_until] = ISNULL(@filter_avlfrc_valid_until, [TBL_FAM_AVAILABILITY_FORECAST].[avlfrc_valid_until])
				AND [TBL_FAM_AVAILABILITY_FORECAST].[avlfrc_validated_by_Fk] = ISNULL(@filter_avlfrc_validated_by_Fk, [TBL_FAM_AVAILABILITY_FORECAST].[avlfrc_validated_by_Fk]);

	END
RETURN 0									
