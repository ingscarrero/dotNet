CREATE PROCEDURE [dbo].[spFAMUpdateTransferRequest]
	@token_id NVARCHAR(50), 
	@filter_trnrqs_id_Pk BIGINT = NULL, 
	@filter_trnrqs_number  VARCHAR(MAX)  = NULL, 
	@filter_trnrqs_informed_Fk INT = NULL, 
	@filter_trnrqs_responsible_Fk INT = NULL, 
	@filter_trnrqs_accountable_Fk INT = NULL, 
	@filter_trnrqs_details NVARCHAR(500) = NULL, 
	@filter_trnrqs_type_Fk  BIGINT = NULL, 
	@filter_trnrqs_topic NVARCHAR(50) = NULL, 
	@filter_trnrqs_registered_at DATETIME  = NULL, 
	@filter_trnrqs_updated_at  DATETIME  = NULL, 
	@filter_trnrqs_status NVARCHAR(10)  = NULL, 
	@filter_trnrqs_origin NVARCHAR(500) = NULL, 
	@filter_trnrqs_destination NVARCHAR(500) = NULL, 
	@filter_trnrqs_external_identifier NVARCHAR(500) = NULL, 
	@filter_trnrqs_novelty_Fk BIGINT = NULL, 
	@new_trnrqs_number  VARCHAR(MAX)  = NULL, 
	@new_trnrqs_informed_Fk INT = NULL, 
	@new_trnrqs_responsible_Fk INT = NULL, 
	@new_trnrqs_accountable_Fk INT = NULL, 
	@new_trnrqs_details NVARCHAR(500) = NULL, 
	@new_trnrqs_type_Fk  BIGINT = NULL, 
	@new_trnrqs_topic NVARCHAR(50) = NULL, 
	@new_trnrqs_registered_at DATETIME  = NULL, 
	@new_trnrqs_updated_at  DATETIME  = NULL, 
	@new_trnrqs_status NVARCHAR(10)  = NULL, 
	@new_trnrqs_comments NVARCHAR(500) = NULL, 
	@new_trnrqs_origin NVARCHAR(500) = NULL, 
	@new_trnrqs_destination NVARCHAR(500) = NULL, 
	@new_trnrqs_external_identifier NVARCHAR(500) = NULL, 
	@new_trnrqs_novelty_Fk BIGINT = NULL
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
			UPDATE [TBL_FAM_TRANSFER_REQUEST]
			SET
				[TBL_FAM_TRANSFER_REQUEST].[trnrqs_number] = ISNULL(@new_trnrqs_number , [TBL_FAM_TRANSFER_REQUEST].[trnrqs_number]), 
				[TBL_FAM_TRANSFER_REQUEST].[trnrqs_informed_Fk] = ISNULL(@new_trnrqs_informed_Fk, [TBL_FAM_TRANSFER_REQUEST].[trnrqs_informed_Fk]), 
				[TBL_FAM_TRANSFER_REQUEST].[trnrqs_responsible_Fk] = ISNULL(@new_trnrqs_responsible_Fk, [TBL_FAM_TRANSFER_REQUEST].[trnrqs_responsible_Fk]), 
				[TBL_FAM_TRANSFER_REQUEST].[trnrqs_accountable_Fk] = ISNULL(@new_trnrqs_accountable_Fk, [TBL_FAM_TRANSFER_REQUEST].[trnrqs_accountable_Fk]), 
				[TBL_FAM_TRANSFER_REQUEST].[trnrqs_details] = ISNULL(@new_trnrqs_details, [TBL_FAM_TRANSFER_REQUEST].[trnrqs_details]), 
				[TBL_FAM_TRANSFER_REQUEST].[trnrqs_type_Fk] = ISNULL(@new_trnrqs_type_Fk , [TBL_FAM_TRANSFER_REQUEST].[trnrqs_type_Fk]), 
				[TBL_FAM_TRANSFER_REQUEST].[trnrqs_topic] = ISNULL(@new_trnrqs_topic, [TBL_FAM_TRANSFER_REQUEST].[trnrqs_topic]), 
				[TBL_FAM_TRANSFER_REQUEST].[trnrqs_registered_at] = ISNULL(@new_trnrqs_registered_at, [TBL_FAM_TRANSFER_REQUEST].[trnrqs_registered_at]), 
				[TBL_FAM_TRANSFER_REQUEST].[trnrqs_updated_at] = ISNULL(@new_trnrqs_updated_at , [TBL_FAM_TRANSFER_REQUEST].[trnrqs_updated_at]), 
				[TBL_FAM_TRANSFER_REQUEST].[trnrqs_status] = ISNULL(@new_trnrqs_status, [TBL_FAM_TRANSFER_REQUEST].[trnrqs_status]), 
				[TBL_FAM_TRANSFER_REQUEST].[trnrqs_comments] = ISNULL(@new_trnrqs_comments, [TBL_FAM_TRANSFER_REQUEST].[trnrqs_comments]), 
				[TBL_FAM_TRANSFER_REQUEST].[trnrqs_origin] = ISNULL(@new_trnrqs_origin, [TBL_FAM_TRANSFER_REQUEST].[trnrqs_origin]), 
				[TBL_FAM_TRANSFER_REQUEST].[trnrqs_destination] = ISNULL(@new_trnrqs_destination, [TBL_FAM_TRANSFER_REQUEST].[trnrqs_destination]), 
				[TBL_FAM_TRANSFER_REQUEST].[trnrqs_external_identifier] = ISNULL(@new_trnrqs_external_identifier, [TBL_FAM_TRANSFER_REQUEST].[trnrqs_external_identifier]), 
				[TBL_FAM_TRANSFER_REQUEST].[trnrqs_novelty_Fk] = ISNULL(@new_trnrqs_novelty_Fk, [TBL_FAM_TRANSFER_REQUEST].[trnrqs_novelty_Fk])
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
			WHERE 
				[TBL_FAM_TRANSFER_REQUEST].[trnrqs_id_Pk] = ISNULL(@filter_trnrqs_id_Pk, [TBL_FAM_TRANSFER_REQUEST].[trnrqs_id_Pk])
				AND [TBL_FAM_TRANSFER_REQUEST].[trnrqs_number] = ISNULL(@filter_trnrqs_number , [TBL_FAM_TRANSFER_REQUEST].[trnrqs_number])
				AND [TBL_FAM_TRANSFER_REQUEST].[trnrqs_informed_Fk] = ISNULL(@filter_trnrqs_informed_Fk, [TBL_FAM_TRANSFER_REQUEST].[trnrqs_informed_Fk])
				AND [TBL_FAM_TRANSFER_REQUEST].[trnrqs_responsible_Fk] = ISNULL(@filter_trnrqs_responsible_Fk, [TBL_FAM_TRANSFER_REQUEST].[trnrqs_responsible_Fk])
				AND [TBL_FAM_TRANSFER_REQUEST].[trnrqs_accountable_Fk] = ISNULL(@filter_trnrqs_accountable_Fk, [TBL_FAM_TRANSFER_REQUEST].[trnrqs_accountable_Fk])
				AND [TBL_FAM_TRANSFER_REQUEST].[trnrqs_details] = ISNULL(@filter_trnrqs_details, [TBL_FAM_TRANSFER_REQUEST].[trnrqs_details])
				AND [TBL_FAM_TRANSFER_REQUEST].[trnrqs_type_Fk] = ISNULL(@filter_trnrqs_type_Fk , [TBL_FAM_TRANSFER_REQUEST].[trnrqs_type_Fk])
				AND [TBL_FAM_TRANSFER_REQUEST].[trnrqs_topic] = ISNULL(@filter_trnrqs_topic, [TBL_FAM_TRANSFER_REQUEST].[trnrqs_topic])
				AND [TBL_FAM_TRANSFER_REQUEST].[trnrqs_registered_at] = ISNULL(@filter_trnrqs_registered_at, [TBL_FAM_TRANSFER_REQUEST].[trnrqs_registered_at])
				AND [TBL_FAM_TRANSFER_REQUEST].[trnrqs_updated_at] = ISNULL(@filter_trnrqs_updated_at , [TBL_FAM_TRANSFER_REQUEST].[trnrqs_updated_at])
				AND [TBL_FAM_TRANSFER_REQUEST].[trnrqs_status] = ISNULL(@filter_trnrqs_status, [TBL_FAM_TRANSFER_REQUEST].[trnrqs_status])
				AND [TBL_FAM_TRANSFER_REQUEST].[trnrqs_origin] = ISNULL(@filter_trnrqs_origin, [TBL_FAM_TRANSFER_REQUEST].[trnrqs_origin])
				AND [TBL_FAM_TRANSFER_REQUEST].[trnrqs_destination] = ISNULL(@filter_trnrqs_destination, [TBL_FAM_TRANSFER_REQUEST].[trnrqs_destination])
				AND [TBL_FAM_TRANSFER_REQUEST].[trnrqs_external_identifier] = ISNULL(@filter_trnrqs_external_identifier, [TBL_FAM_TRANSFER_REQUEST].[trnrqs_external_identifier])
				AND [TBL_FAM_TRANSFER_REQUEST].[trnrqs_novelty_Fk] = ISNULL(@filter_trnrqs_novelty_Fk, [TBL_FAM_TRANSFER_REQUEST].[trnrqs_novelty_Fk]);

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
