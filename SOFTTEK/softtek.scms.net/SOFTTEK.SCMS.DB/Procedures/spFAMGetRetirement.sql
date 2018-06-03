CREATE PROCEDURE [dbo].[spFAMGetRetirement]
	@token_id NVARCHAR(50), 
	@filter_rtr_id_Pk BIGINT = NULL, 
	@filter_rtr_number NVARCHAR(MAX) = NULL, 
	@filter_rtr_external_identifier NVARCHAR(500) = NULL, 
	@filter_rtr_informed_Fk INT = NULL, 
	@filter_rtr_responsible_Fk INT = NULL, 
	@filter_rtr_accountable_Fk INT = NULL, 
	@filter_rtr_novelty_Fk INT = NULL, 
	@filter_rtr_type_Fk BIGINT = NULL, 
	@filter_rtr_topic NVARCHAR(50) = NULL, 
	@filter_rtr_status NVARCHAR(10) = NULL, 
	@filter_rtr_comments NVARCHAR(500) = NULL, 
	@filter_rtr_reason NVARCHAR(500) = NULL
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
				[TBL_FAM_RETIREMENT_REQUEST].[rtr_id_Pk], 
				[TBL_FAM_RETIREMENT_REQUEST].[rtr_number], 
				[TBL_FAM_RETIREMENT_REQUEST].[rtr_external_identifier], 
				[informed].[id] AS [rtr_informed_Fk], [informed].[user], 
				[responsible].[id] AS [rtr_responsible_Fk], [responsible].[user], 
				[accountable].[id] AS [rtr_accountable_Fk], [accountable].[user], 
				[TBL_FAM_NOVELTY].[nvlrqs_request_Fk], 
				[TBL_FAM_RETIREMENT_REQUEST].[rtr_details], 
				[TBL_FAM_RETIREMENT_REQUEST].[rtr_type_Fk], 
				[TBL_FAM_RETIREMENT_REQUEST].[rtr_topic], 
				[TBL_FAM_RETIREMENT_REQUEST].[rtr_registered_at], 
				[TBL_FAM_RETIREMENT_REQUEST].[rtr_updated_at], 
				[TBL_FAM_RETIREMENT_REQUEST].[rtr_status], 
				[TBL_FAM_RETIREMENT_REQUEST].[rtr_comments], 
				[TBL_FAM_RETIREMENT_REQUEST].[rtr_reason]
			FROM [TBL_FAM_RETIREMENT_REQUEST]
				INNER JOIN [TBL_SRA_EMPLOYEE] AS [informed] ON [informed].[id] = [TBL_FAM_RETIREMENT_REQUEST].[rtr_informed_Fk]
				INNER JOIN [TBL_SRA_EMPLOYEE] AS [responsible] ON [responsible].[id] = [TBL_FAM_RETIREMENT_REQUEST].[rtr_responsible_Fk]
				INNER JOIN [TBL_SRA_EMPLOYEE] AS [accountable] ON [accountable].[id] = [TBL_FAM_RETIREMENT_REQUEST].[rtr_accountable_Fk]
				INNER JOIN [TBL_FAM_NOVELTY] ON [TBL_FAM_NOVELTY].[nvlrqs_request_Fk] = [TBL_FAM_RETIREMENT_REQUEST].[rtr_novelty_Fk]
			WHERE 
				[TBL_FAM_RETIREMENT_REQUEST].[rtr_id_Pk] = ISNULL(@filter_rtr_id_Pk, [TBL_FAM_RETIREMENT_REQUEST].[rtr_id_Pk])
				AND [TBL_FAM_RETIREMENT_REQUEST].[rtr_number] = ISNULL(@filter_rtr_number, [TBL_FAM_RETIREMENT_REQUEST].[rtr_number])
				AND [TBL_FAM_RETIREMENT_REQUEST].[rtr_external_identifier] = ISNULL(@filter_rtr_external_identifier, [TBL_FAM_RETIREMENT_REQUEST].[rtr_external_identifier])
				AND [TBL_FAM_RETIREMENT_REQUEST].[rtr_informed_Fk] = ISNULL(@filter_rtr_informed_Fk, [TBL_FAM_RETIREMENT_REQUEST].[rtr_informed_Fk])
				AND [TBL_FAM_RETIREMENT_REQUEST].[rtr_responsible_Fk] = ISNULL(@filter_rtr_responsible_Fk, [TBL_FAM_RETIREMENT_REQUEST].[rtr_responsible_Fk])
				AND [TBL_FAM_RETIREMENT_REQUEST].[rtr_accountable_Fk] = ISNULL(@filter_rtr_accountable_Fk, [TBL_FAM_RETIREMENT_REQUEST].[rtr_accountable_Fk])
				AND [TBL_FAM_RETIREMENT_REQUEST].[rtr_novelty_Fk] = ISNULL(@filter_rtr_novelty_Fk, [TBL_FAM_RETIREMENT_REQUEST].[rtr_novelty_Fk])
				AND [TBL_FAM_RETIREMENT_REQUEST].[rtr_type_Fk] = ISNULL(@filter_rtr_type_Fk, [TBL_FAM_RETIREMENT_REQUEST].[rtr_type_Fk])
				AND [TBL_FAM_RETIREMENT_REQUEST].[rtr_topic] = ISNULL(@filter_rtr_topic, [TBL_FAM_RETIREMENT_REQUEST].[rtr_topic])
				AND [TBL_FAM_RETIREMENT_REQUEST].[rtr_status] = ISNULL(@filter_rtr_status, [TBL_FAM_RETIREMENT_REQUEST].[rtr_status])
				AND [TBL_FAM_RETIREMENT_REQUEST].[rtr_comments] = ISNULL(@filter_rtr_comments, [TBL_FAM_RETIREMENT_REQUEST].[rtr_comments])
				AND [TBL_FAM_RETIREMENT_REQUEST].[rtr_reason] = ISNULL(@filter_rtr_reason, [TBL_FAM_RETIREMENT_REQUEST].[rtr_reason]);

	END
RETURN 0									
