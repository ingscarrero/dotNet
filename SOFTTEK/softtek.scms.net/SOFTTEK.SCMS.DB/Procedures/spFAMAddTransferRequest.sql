CREATE PROCEDURE [dbo].[spFAMAddTransferRequest]
	@token_id NVARCHAR(50), 
	@new_trnrqs_number  VARCHAR(MAX) , 
	@new_trnrqs_informed_Fk INT, 
	@new_trnrqs_responsible_Fk INT, 
	@new_trnrqs_accountable_Fk INT, 
	@new_trnrqs_details NVARCHAR(500), 
	@new_trnrqs_type_Fk  BIGINT, 
	@new_trnrqs_topic NVARCHAR(50), 
	@new_trnrqs_registered_at DATETIME , 
	@new_trnrqs_updated_at  DATETIME , 
	@new_trnrqs_status NVARCHAR(10) , 
	@new_trnrqs_comments NVARCHAR(500), 
	@new_trnrqs_origin NVARCHAR(500), 
	@new_trnrqs_destination NVARCHAR(500), 
	@new_trnrqs_external_identifier NVARCHAR(500), 
	@new_trnrqs_novelty_Fk BIGINT
AS
	DECLARE @v_token_user nvarchar(20);
	DECLARE @v_token_employee_id int;
	DECLARE @v_TRANSFER_REQUEST table (
		v_trnrqs_id_Pk BIGINT, 
		v_trnrqs_number  VARCHAR(MAX) , 
		v_trnrqs_informed_Fk INT, 
		v_trnrqs_responsible_Fk INT, 
		v_trnrqs_accountable_Fk INT, 
		v_trnrqs_details NVARCHAR(500), 
		v_trnrqs_type_Fk  BIGINT, 
		v_trnrqs_topic NVARCHAR(50), 
		v_trnrqs_registered_at DATETIME , 
		v_trnrqs_updated_at  DATETIME , 
		v_trnrqs_status NVARCHAR(10) , 
		v_trnrqs_comments NVARCHAR(500), 
		v_trnrqs_origin NVARCHAR(500), 
		v_trnrqs_destination NVARCHAR(500), 
		v_trnrqs_external_identifier NVARCHAR(500), 
		v_trnrqs_novelty_Fk BIGINT
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
			INSERT INTO [TBL_FAM_TRANSFER_REQUEST] (
				[TBL_FAM_TRANSFER_REQUEST].[trnrqs_number], 
				[TBL_FAM_TRANSFER_REQUEST].[trnrqs_informed_Fk], 
				[TBL_FAM_TRANSFER_REQUEST].[trnrqs_responsible_Fk], 
				[TBL_FAM_TRANSFER_REQUEST].[trnrqs_accountable_Fk], 
				[TBL_FAM_TRANSFER_REQUEST].[trnrqs_details], 
				[TBL_FAM_TRANSFER_REQUEST].[trnrqs_type_Fk], 
				[TBL_FAM_TRANSFER_REQUEST].[trnrqs_topic], 
				[TBL_FAM_TRANSFER_REQUEST].[trnrqs_registered_at], 
				[TBL_FAM_TRANSFER_REQUEST].[trnrqs_updated_at], 
				[TBL_FAM_TRANSFER_REQUEST].[trnrqs_status], 
				[TBL_FAM_TRANSFER_REQUEST].[trnrqs_comments], 
				[TBL_FAM_TRANSFER_REQUEST].[trnrqs_origin], 
				[TBL_FAM_TRANSFER_REQUEST].[trnrqs_destination], 
				[TBL_FAM_TRANSFER_REQUEST].[trnrqs_external_identifier], 
				[TBL_FAM_TRANSFER_REQUEST].[trnrqs_novelty_Fk])
			OUTPUT 
				inserted.[trnrqs_id_Pk], 
				inserted.[trnrqs_number], 
				inserted.[trnrqs_informed_Fk], 
				inserted.[trnrqs_responsible_Fk], 
				inserted.[trnrqs_accountable_Fk], 
				inserted.[trnrqs_details], 
				inserted.[trnrqs_type_Fk], 
				inserted.[trnrqs_topic], 
				inserted.[trnrqs_registered_at], 
				inserted.[trnrqs_updated_at], 
				inserted.[trnrqs_status], 
				inserted.[trnrqs_comments], 
				inserted.[trnrqs_origin], 
				inserted.[trnrqs_destination], 
				inserted.[trnrqs_external_identifier], 
				inserted.[trnrqs_novelty_Fk]
			INTO @v_TRANSFER_REQUEST
			VALUES (
				@new_trnrqs_number , 
				@new_trnrqs_informed_Fk, 
				@new_trnrqs_responsible_Fk, 
				@new_trnrqs_accountable_Fk, 
				@new_trnrqs_details, 
				@new_trnrqs_type_Fk , 
				@new_trnrqs_topic, 
				@new_trnrqs_registered_at, 
				@new_trnrqs_updated_at , 
				@new_trnrqs_status, 
				@new_trnrqs_comments, 
				@new_trnrqs_origin, 
				@new_trnrqs_destination, 
				@new_trnrqs_external_identifier, 
				@new_trnrqs_novelty_Fk);

			SELECT 
				v_trnrqs_id_Pk, 
				v_trnrqs_number , 
				v_trnrqs_informed_Fk, 
				v_trnrqs_responsible_Fk, 
				v_trnrqs_accountable_Fk, 
				v_trnrqs_details, 
				v_trnrqs_type_Fk , 
				v_trnrqs_topic, 
				v_trnrqs_registered_at, 
				v_trnrqs_updated_at , 
				v_trnrqs_status, 
				v_trnrqs_comments, 
				v_trnrqs_origin, 
				v_trnrqs_destination, 
				v_trnrqs_external_identifier, 
				v_trnrqs_novelty_Fk
			FROM @v_TRANSFER_REQUEST;
	END
RETURN 0									
