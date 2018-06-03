CREATE PROCEDURE [dbo].[spFAMAddPurchaseRequest]
	@token_id NVARCHAR(50), 
	@new_prcrqs_number VARCHAR(MAX) , 
	@new_prcrqs_informed_Fk INT, 
	@new_prcrqs_responsible_Fk INT, 
	@new_prcrqs_accountable_Fk INT, 
	@new_prcrqs_details NVARCHAR(500), 
	@new_prcrqs_type_Fk BIGINT, 
	@new_prcrqs_topic NVARCHAR(50), 
	@new_prcrqs_registered_at DATETIME , 
	@new_prcrqs_updated_at DATETIME , 
	@new_prcrqs_status NVARCHAR(10) , 
	@new_prcrqs_comments NVARCHAR(500), 
	@new_prcrqs_external_identifier NVARCHAR(500), 
	@new_prcrqs_novelty_Fk BIGINT, 
	@new_prcrqs_fixed_asset_Fk BIGINT
AS
	DECLARE @v_token_user nvarchar(20);
	DECLARE @v_token_employee_id int;
	DECLARE @v_HASE_REQUEST table (
		v_prcrqs_id_Pk BIGINT, 
		v_prcrqs_number VARCHAR(MAX) , 
		v_prcrqs_informed_Fk INT, 
		v_prcrqs_responsible_Fk INT, 
		v_prcrqs_accountable_Fk INT, 
		v_prcrqs_details NVARCHAR(500), 
		v_prcrqs_type_Fk BIGINT, 
		v_prcrqs_topic NVARCHAR(50), 
		v_prcrqs_registered_at DATETIME , 
		v_prcrqs_updated_at DATETIME , 
		v_prcrqs_status NVARCHAR(10) , 
		v_prcrqs_comments NVARCHAR(500), 
		v_prcrqs_external_identifier NVARCHAR(500), 
		v_prcrqs_novelty_Fk BIGINT, 
		v_prcrqs_fixed_asset_Fk BIGINT
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
			INSERT INTO [TBL_FAM_PURCHASE_REQUEST] (
				[TBL_FAM_PURCHASE_REQUEST].[prcrqs_number], 
				[TBL_FAM_PURCHASE_REQUEST].[prcrqs_informed_Fk], 
				[TBL_FAM_PURCHASE_REQUEST].[prcrqs_responsible_Fk], 
				[TBL_FAM_PURCHASE_REQUEST].[prcrqs_accountable_Fk], 
				[TBL_FAM_PURCHASE_REQUEST].[prcrqs_details], 
				[TBL_FAM_PURCHASE_REQUEST].[prcrqs_type_Fk], 
				[TBL_FAM_PURCHASE_REQUEST].[prcrqs_topic], 
				[TBL_FAM_PURCHASE_REQUEST].[prcrqs_registered_at], 
				[TBL_FAM_PURCHASE_REQUEST].[prcrqs_updated_at], 
				[TBL_FAM_PURCHASE_REQUEST].[prcrqs_status], 
				[TBL_FAM_PURCHASE_REQUEST].[prcrqs_comments], 
				[TBL_FAM_PURCHASE_REQUEST].[prcrqs_external_identifier], 
				[TBL_FAM_PURCHASE_REQUEST].[prcrqs_novelty_Fk], 
				[TBL_FAM_PURCHASE_REQUEST].[prcrqs_fixed_asset_Fk])
			OUTPUT 
				inserted.[prcrqs_id_Pk], 
				inserted.[prcrqs_number], 
				inserted.[prcrqs_informed_Fk], 
				inserted.[prcrqs_responsible_Fk], 
				inserted.[prcrqs_accountable_Fk], 
				inserted.[prcrqs_details], 
				inserted.[prcrqs_type_Fk], 
				inserted.[prcrqs_topic], 
				inserted.[prcrqs_registered_at], 
				inserted.[prcrqs_updated_at], 
				inserted.[prcrqs_status], 
				inserted.[prcrqs_comments], 
				inserted.[prcrqs_external_identifier], 
				inserted.[prcrqs_novelty_Fk], 
				inserted.[prcrqs_fixed_asset_Fk]
			INTO @v_HASE_REQUEST
			VALUES (
				@new_prcrqs_number, 
				@new_prcrqs_informed_Fk, 
				@new_prcrqs_responsible_Fk, 
				@new_prcrqs_accountable_Fk, 
				@new_prcrqs_details, 
				@new_prcrqs_type_Fk, 
				@new_prcrqs_topic, 
				@new_prcrqs_registered_at, 
				@new_prcrqs_updated_at, 
				@new_prcrqs_status, 
				@new_prcrqs_comments, 
				@new_prcrqs_external_identifier, 
				@new_prcrqs_novelty_Fk, 
				@new_prcrqs_fixed_asset_Fk);

			SELECT 
				v_prcrqs_id_Pk, 
				v_prcrqs_number, 
				v_prcrqs_informed_Fk, 
				v_prcrqs_responsible_Fk, 
				v_prcrqs_accountable_Fk, 
				v_prcrqs_details, 
				v_prcrqs_type_Fk, 
				v_prcrqs_topic, 
				v_prcrqs_registered_at, 
				v_prcrqs_updated_at, 
				v_prcrqs_status, 
				v_prcrqs_comments, 
				v_prcrqs_external_identifier, 
				v_prcrqs_novelty_Fk, 
				v_prcrqs_fixed_asset_Fk
			FROM @v_HASE_REQUEST;
	END
RETURN 0								
