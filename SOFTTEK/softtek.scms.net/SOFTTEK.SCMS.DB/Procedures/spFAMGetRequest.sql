CREATE PROCEDURE [dbo].[spFAMGetRequest]
	@token_id NVARCHAR(50), 
	@filter_rqs_id_Pk BIGINT = NULL, 
	@filter_rqs_number NVARCHAR(MAX) = NULL, 
	@filter_rqs_informed_Fk INT = NULL, 
	@filter_rqs_responsible_Fk INT = NULL, 
	@filter_rqs_accountable_Fk INT = NULL, 
	@filter_rqs_details NVARCHAR(MAX) = NULL, 
	@filter_rqs_type_Fk BIGINT = NULL, 
	@filter_rqs_topic NVARCHAR(50) = NULL, 
	@filter_rqs_status NVARCHAR(10) = NULL, 
	@filter_rqs_comments NVARCHAR(500) = NULL
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
				[TBL_FAM_REQUEST].[rqs_id_Pk], 
				[TBL_FAM_REQUEST].[rqs_number], 
				[informed].[id] AS [rqs_informed_Fk], [informed].[user], 
				[responsible].[id] AS [rqs_responsible_Fk], [responsible].[user], 
				[accountable].[id] AS [rqs_accountable_Fk], [accountable].[user], 
				[TBL_FAM_REQUEST].[rqs_details], 
				[TBL_FAM_REQUEST].[rqs_type_Fk], 
				[TBL_FAM_REQUEST].[rqs_topic], 
				[TBL_FAM_REQUEST].[rqs_registered_at], 
				[TBL_FAM_REQUEST].[rqs_updated_at], 
				[TBL_FAM_REQUEST].[rqs_status], 
				[TBL_FAM_REQUEST].[rqs_comments]
			FROM [TBL_FAM_REQUEST]
				INNER JOIN [TBL_SRA_EMPLOYEE] AS [informed] ON [informed].[id] = [TBL_FAM_REQUEST].[rqs_informed_Fk]
				INNER JOIN [TBL_SRA_EMPLOYEE] AS [responsible] ON [responsible].[id] = [TBL_FAM_REQUEST].[rqs_responsible_Fk]
				INNER JOIN [TBL_SRA_EMPLOYEE] AS [accountable] ON [accountable].[id] = [TBL_FAM_REQUEST].[rqs_accountable_Fk]
			WHERE 
				[TBL_FAM_REQUEST].[rqs_id_Pk] = ISNULL(@filter_rqs_id_Pk, [TBL_FAM_REQUEST].[rqs_id_Pk])
				AND [TBL_FAM_REQUEST].[rqs_number] = ISNULL(@filter_rqs_number, [TBL_FAM_REQUEST].[rqs_number])
				AND [TBL_FAM_REQUEST].[rqs_informed_Fk] = ISNULL(@filter_rqs_informed_Fk, [TBL_FAM_REQUEST].[rqs_informed_Fk])
				AND [TBL_FAM_REQUEST].[rqs_responsible_Fk] = ISNULL(@filter_rqs_responsible_Fk, [TBL_FAM_REQUEST].[rqs_responsible_Fk])
				AND [TBL_FAM_REQUEST].[rqs_accountable_Fk] = ISNULL(@filter_rqs_accountable_Fk, [TBL_FAM_REQUEST].[rqs_accountable_Fk])
				AND [TBL_FAM_REQUEST].[rqs_details] = ISNULL(@filter_rqs_details, [TBL_FAM_REQUEST].[rqs_details])
				AND [TBL_FAM_REQUEST].[rqs_type_Fk] = ISNULL(@filter_rqs_type_Fk, [TBL_FAM_REQUEST].[rqs_type_Fk])
				AND [TBL_FAM_REQUEST].[rqs_topic] = ISNULL(@filter_rqs_topic, [TBL_FAM_REQUEST].[rqs_topic])
				AND [TBL_FAM_REQUEST].[rqs_status] = ISNULL(@filter_rqs_status, [TBL_FAM_REQUEST].[rqs_status])
				AND [TBL_FAM_REQUEST].[rqs_comments] = ISNULL(@filter_rqs_comments, [TBL_FAM_REQUEST].[rqs_comments]);

	END
RETURN 0							
