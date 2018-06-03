CREATE PROCEDURE [dbo].[spFAMGetNovelty]
	@token_id NVARCHAR(50), 
	@filter_nvlrqs_id_Pk BIGINT = NULL, 
	@filter_nvlrqs_number NVARCHAR(MAX) = NULL, 
	@filter_nvlrqs_external_identifier NVARCHAR(500) = NULL, 
	@filter_nvlrqs_informed_Fk INT = NULL, 
	@filter_nvlrqs_responsible_Fk INT = NULL, 
	@filter_nvlrqs_accountable_Fk INT = NULL, 
	@filter_nvlrqs_request_Fk INT = NULL, 
	@filter_nvlrqs_type_Fk BIGINT = NULL, 
	@filter_nvlrqs_topic NVARCHAR(50) = NULL, 
	@filter_nvlrqs_status NVARCHAR(10) = NULL, 
	@filter_nvlrqs_comments NVARCHAR(500) = NULL
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
				[TBL_FAM_NOVELTY].[nvlrqs_id_Pk],
				[TBL_FAM_NOVELTY].[nvlrqs_request_Fk], 
				[TBL_FAM_NOVELTY].[nvlrqs_number], 
				[TBL_FAM_NOVELTY].[nvlrqs_external_identifier], 
				[informed].[id] AS nvlrqs_informed_Fk, [informed].[user], 
				[responsible].[id]AS nvlrqs_responsible_Fk, [responsible].[user], 
				[accountable].[id] AS nvlrqs_accountable_Fk, [accountable].[user], 
				[TBL_FAM_REQUEST].[rqs_id_Pk], 
				[TBL_FAM_NOVELTY].[nvlrqs_details], 
				[TBL_FAM_NOVELTY].[nvlrqs_type_Fk], 
				[TBL_FAM_NOVELTY].[nvlrqs_topic], 
				[TBL_FAM_NOVELTY].[nvlrqs_registered_at], 
				[TBL_FAM_NOVELTY].[nvlrqs_updated_at], 
				[TBL_FAM_NOVELTY].[nvlrqs_status], 
				[TBL_FAM_NOVELTY].[nvlrqs_comments]
			FROM [TBL_FAM_NOVELTY]
				INNER JOIN [TBL_SRA_EMPLOYEE] AS [informed] ON [informed].[id] = [TBL_FAM_NOVELTY].[nvlrqs_informed_Fk]
				INNER JOIN [TBL_SRA_EMPLOYEE] AS [responsible] ON [responsible].[id] = [TBL_FAM_NOVELTY].[nvlrqs_responsible_Fk]
				INNER JOIN [TBL_SRA_EMPLOYEE] AS [accountable] ON [accountable].[id] = [TBL_FAM_NOVELTY].[nvlrqs_accountable_Fk]
				INNER JOIN [TBL_FAM_REQUEST] ON [TBL_FAM_REQUEST].[rqs_id_Pk] = [TBL_FAM_NOVELTY].[nvlrqs_request_Fk]
			WHERE 
			[TBL_FAM_NOVELTY].[nvlrqs_id_Pk] = ISNULL(@filter_nvlrqs_id_Pk, [TBL_FAM_NOVELTY].[nvlrqs_id_Pk])
				AND [TBL_FAM_NOVELTY].[nvlrqs_request_Fk] = ISNULL(@filter_nvlrqs_request_Fk, [TBL_FAM_NOVELTY].[nvlrqs_request_Fk])
				AND [TBL_FAM_NOVELTY].[nvlrqs_number] = ISNULL(@filter_nvlrqs_number, [TBL_FAM_NOVELTY].[nvlrqs_number])
				AND [TBL_FAM_NOVELTY].[nvlrqs_external_identifier] = ISNULL(@filter_nvlrqs_external_identifier, [TBL_FAM_NOVELTY].[nvlrqs_external_identifier])
				AND [TBL_FAM_NOVELTY].[nvlrqs_informed_Fk] = ISNULL(@filter_nvlrqs_informed_Fk, [TBL_FAM_NOVELTY].[nvlrqs_informed_Fk])
				AND [TBL_FAM_NOVELTY].[nvlrqs_responsible_Fk] = ISNULL(@filter_nvlrqs_responsible_Fk, [TBL_FAM_NOVELTY].[nvlrqs_responsible_Fk])
				AND [TBL_FAM_NOVELTY].[nvlrqs_accountable_Fk] = ISNULL(@filter_nvlrqs_accountable_Fk, [TBL_FAM_NOVELTY].[nvlrqs_accountable_Fk])
				AND [TBL_FAM_NOVELTY].[nvlrqs_request_Fk] = ISNULL(@filter_nvlrqs_request_Fk, [TBL_FAM_NOVELTY].[nvlrqs_request_Fk])
				AND [TBL_FAM_NOVELTY].[nvlrqs_type_Fk] = ISNULL(@filter_nvlrqs_type_Fk, [TBL_FAM_NOVELTY].[nvlrqs_type_Fk])
				AND [TBL_FAM_NOVELTY].[nvlrqs_topic] = ISNULL(@filter_nvlrqs_topic, [TBL_FAM_NOVELTY].[nvlrqs_topic])
				AND [TBL_FAM_NOVELTY].[nvlrqs_status] = ISNULL(@filter_nvlrqs_status, [TBL_FAM_NOVELTY].[nvlrqs_status])
				AND [TBL_FAM_NOVELTY].[nvlrqs_comments] = ISNULL(@filter_nvlrqs_comments, [TBL_FAM_NOVELTY].[nvlrqs_comments]);

	END
RETURN 0									
