﻿CREATE PROCEDURE [dbo].[spFAMUpdateProvider]
	@token_id NVARCHAR(50), 
	@filter_prv_id_Pk BIGINT = NULL, 
	@filter_prov_external_id NVARCHAR(500) = NULL, 
	@filter_prv_name NVARCHAR(50) = NULL, 
	@filter_prv_contract NVARCHAR(50) = NULL, 
	@filter_prv_document NVARCHAR(50) = NULL, 
	@filter_prv_state NVARCHAR(10) = NULL, 
	@new_prov_external_id NVARCHAR(500) = NULL, 
	@new_prv_name NVARCHAR(50) = NULL, 
	@new_prv_contract NVARCHAR(50) = NULL, 
	@new_prv_document NVARCHAR(50) = NULL, 
	@new_prv_state NVARCHAR(10) = NULL
AS
	DECLARE @v_token_user nvarchar(20);
	DECLARE @v_token_employee_id int;
	DECLARE @v_PROVIDER table (
		v_prv_id_Pk BIGINT, 
		v_prov_external_id NVARCHAR(500), 
		v_prv_name NVARCHAR(50), 
		v_prv_contract NVARCHAR(50), 
		v_prv_document NVARCHAR(50), 
		v_prv_state NVARCHAR(10)
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
			UPDATE [TBL_FAM_PROVIDER]
			SET
				[TBL_FAM_PROVIDER].[prv_external_identifier] = ISNULL(@new_prov_external_id, [TBL_FAM_PROVIDER].[prv_external_identifier]), 
				[TBL_FAM_PROVIDER].[prv_name] = ISNULL(@new_prv_name, [TBL_FAM_PROVIDER].[prv_name]), 
				[TBL_FAM_PROVIDER].[prv_contract] = ISNULL(@new_prv_contract, [TBL_FAM_PROVIDER].[prv_contract]), 
				[TBL_FAM_PROVIDER].[prv_document] = ISNULL(@new_prv_document, [TBL_FAM_PROVIDER].[prv_document]), 
				[TBL_FAM_PROVIDER].[prv_state] = ISNULL(@new_prv_state, [TBL_FAM_PROVIDER].[prv_state])
			OUTPUT 
				inserted.[prv_id_Pk], 
				inserted.[prv_external_identifier], 
				inserted.[prv_name], 
				inserted.[prv_contract], 
				inserted.[prv_document], 
				inserted.[prv_state]
			INTO @v_PROVIDER
			WHERE 
				[TBL_FAM_PROVIDER].[prv_id_Pk] = ISNULL(@filter_prv_id_Pk, [TBL_FAM_PROVIDER].[prv_id_Pk])
				AND [TBL_FAM_PROVIDER].[prv_external_identifier] = ISNULL(@filter_prov_external_id, [TBL_FAM_PROVIDER].[prv_external_identifier])
				AND [TBL_FAM_PROVIDER].[prv_name] = ISNULL(@filter_prv_name, [TBL_FAM_PROVIDER].[prv_name])
				AND [TBL_FAM_PROVIDER].[prv_contract] = ISNULL(@filter_prv_contract, [TBL_FAM_PROVIDER].[prv_contract])
				AND [TBL_FAM_PROVIDER].[prv_document] = ISNULL(@filter_prv_document, [TBL_FAM_PROVIDER].[prv_document])
				AND [TBL_FAM_PROVIDER].[prv_state] = ISNULL(@filter_prv_state, [TBL_FAM_PROVIDER].[prv_state]);

			SELECT 
				v_prv_id_Pk, 
				v_prov_external_id, 
				v_prv_name, 
				v_prv_contract, 
				v_prv_document, 
				v_prv_state
			FROM @v_PROVIDER;
	END
RETURN 0									
