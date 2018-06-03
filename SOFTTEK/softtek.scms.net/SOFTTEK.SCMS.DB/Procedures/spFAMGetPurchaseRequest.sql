CREATE PROCEDURE [dbo].[spFAMGetPurchaseRequest]
	@token_id NVARCHAR(50), 
	@filter_prcrqs_id_Pk BIGINT = NULL, 
	@filter_prcrqs_number VARCHAR(MAX)  = NULL, 
	@filter_prcrqs_informed_Fk INT = NULL, 
	@filter_prcrqs_responsible_Fk INT = NULL, 
	@filter_prcrqs_accountable_Fk INT = NULL, 
	@filter_prcrqs_details NVARCHAR(500) = NULL, 
	@filter_prcrqs_type_Fk BIGINT = NULL, 
	@filter_prcrqs_topic NVARCHAR(50) = NULL, 
	@filter_prcrqs_registered_at DATETIME  = NULL, 
	@filter_prcrqs_updated_at DATETIME  = NULL, 
	@filter_prcrqs_status NVARCHAR(10)  = NULL, 
	@filter_prcrqs_external_identifier NVARCHAR(500) = NULL, 
	@filter_prcrqs_novelty_Fk BIGINT = NULL, 
	@filter_prcrqs_fixed_asset_Fk BIGINT = NULL
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
				[TBL_FAM_PURCHASE_REQUEST].[prcrqs_id_Pk], 
				[TBL_FAM_PURCHASE_REQUEST].[prcrqs_number], 
				[informed].[id] as [prcrqs_informed_Fk], 
				[responsible].[id] as [prcrqs_responsible_Fk], 
				[accountable].[id] as [prcrqs_accountable_Fk], 
				[TBL_FAM_PURCHASE_REQUEST].[prcrqs_details], 
				[TBL_FAM_PURCHASE_REQUEST].[prcrqs_type_Fk], 
				[TBL_FAM_PURCHASE_REQUEST].[prcrqs_topic], 
				[TBL_FAM_PURCHASE_REQUEST].[prcrqs_registered_at], 
				[TBL_FAM_PURCHASE_REQUEST].[prcrqs_updated_at], 
				[TBL_FAM_PURCHASE_REQUEST].[prcrqs_status], 
				[TBL_FAM_PURCHASE_REQUEST].[prcrqs_comments], 
				[TBL_FAM_PURCHASE_REQUEST].[prcrqs_external_identifier], 
				[TBL_FAM_NOVELTY].[nvlrqs_request_Fk], 
				[TBL_FAM_FIXEDASSET].[fxdast_id_Pk]
			FROM [TBL_FAM_PURCHASE_REQUEST]
				INNER JOIN [TBL_SRA_EMPLOYEE] AS [informed] ON [informed].[id] = [TBL_FAM_PURCHASE_REQUEST].[prcrqs_informed_Fk]
				INNER JOIN [TBL_SRA_EMPLOYEE] AS [responsible] ON [responsible].[id] = [TBL_FAM_PURCHASE_REQUEST].[prcrqs_responsible_Fk]
				INNER JOIN [TBL_SRA_EMPLOYEE] AS [accountable] ON [accountable].[id] = [TBL_FAM_PURCHASE_REQUEST].[prcrqs_accountable_Fk]
				INNER JOIN [TBL_FAM_NOVELTY] ON [TBL_FAM_NOVELTY].[nvlrqs_request_Fk] = [TBL_FAM_PURCHASE_REQUEST].[prcrqs_novelty_Fk]
				INNER JOIN [TBL_FAM_FIXEDASSET] ON [TBL_FAM_FIXEDASSET].[fxdast_id_Pk] = [TBL_FAM_PURCHASE_REQUEST].[prcrqs_fixed_asset_Fk]
			WHERE 
				[TBL_FAM_PURCHASE_REQUEST].[prcrqs_id_Pk] = ISNULL(@filter_prcrqs_id_Pk, [TBL_FAM_PURCHASE_REQUEST].[prcrqs_id_Pk])
				AND [TBL_FAM_PURCHASE_REQUEST].[prcrqs_number] = ISNULL(@filter_prcrqs_number, [TBL_FAM_PURCHASE_REQUEST].[prcrqs_number])
				AND [TBL_FAM_PURCHASE_REQUEST].[prcrqs_informed_Fk] = ISNULL(@filter_prcrqs_informed_Fk, [TBL_FAM_PURCHASE_REQUEST].[prcrqs_informed_Fk])
				AND [TBL_FAM_PURCHASE_REQUEST].[prcrqs_responsible_Fk] = ISNULL(@filter_prcrqs_responsible_Fk, [TBL_FAM_PURCHASE_REQUEST].[prcrqs_responsible_Fk])
				AND [TBL_FAM_PURCHASE_REQUEST].[prcrqs_accountable_Fk] = ISNULL(@filter_prcrqs_accountable_Fk, [TBL_FAM_PURCHASE_REQUEST].[prcrqs_accountable_Fk])
				AND [TBL_FAM_PURCHASE_REQUEST].[prcrqs_details] = ISNULL(@filter_prcrqs_details, [TBL_FAM_PURCHASE_REQUEST].[prcrqs_details])
				AND [TBL_FAM_PURCHASE_REQUEST].[prcrqs_type_Fk] = ISNULL(@filter_prcrqs_type_Fk, [TBL_FAM_PURCHASE_REQUEST].[prcrqs_type_Fk])
				AND [TBL_FAM_PURCHASE_REQUEST].[prcrqs_topic] = ISNULL(@filter_prcrqs_topic, [TBL_FAM_PURCHASE_REQUEST].[prcrqs_topic])
				AND [TBL_FAM_PURCHASE_REQUEST].[prcrqs_registered_at] = ISNULL(@filter_prcrqs_registered_at, [TBL_FAM_PURCHASE_REQUEST].[prcrqs_registered_at])
				AND [TBL_FAM_PURCHASE_REQUEST].[prcrqs_updated_at] = ISNULL(@filter_prcrqs_updated_at, [TBL_FAM_PURCHASE_REQUEST].[prcrqs_updated_at])
				AND [TBL_FAM_PURCHASE_REQUEST].[prcrqs_status] = ISNULL(@filter_prcrqs_status, [TBL_FAM_PURCHASE_REQUEST].[prcrqs_status])
				AND [TBL_FAM_PURCHASE_REQUEST].[prcrqs_external_identifier] = ISNULL(@filter_prcrqs_external_identifier, [TBL_FAM_PURCHASE_REQUEST].[prcrqs_external_identifier])
				AND [TBL_FAM_PURCHASE_REQUEST].[prcrqs_novelty_Fk] = ISNULL(@filter_prcrqs_novelty_Fk, [TBL_FAM_PURCHASE_REQUEST].[prcrqs_novelty_Fk])
				AND [TBL_FAM_PURCHASE_REQUEST].[prcrqs_fixed_asset_Fk] = ISNULL(@filter_prcrqs_fixed_asset_Fk, [TBL_FAM_PURCHASE_REQUEST].[prcrqs_fixed_asset_Fk]);

	END
RETURN 0									
