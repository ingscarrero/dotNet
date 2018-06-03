 CREATE PROCEDURE [dbo].[spFAMUpdateNovelty]
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
	@filter_nvlrqs_comments NVARCHAR(500) = NULL, 
	@new_nvlrqs_number NVARCHAR(MAX) = NULL, 
	@new_nvlrqs_external_identifier NVARCHAR(500) = NULL, 
	@new_nvlrqs_informed_Fk INT = NULL, 
	@new_nvlrqs_responsible_Fk INT = NULL, 
	@new_nvlrqs_accountable_Fk INT = NULL, 
	@new_nvlrqs_request_Fk INT = NULL, 
	@new_nvlrqs_details NVARCHAR(MAX) = NULL, 
	@new_nvlrqs_type_Fk BIGINT = NULL, 
	@new_nvlrqs_topic NVARCHAR(50) = NULL, 
	@new_nvlrqs_registered_at DATETIME = NULL, 
	@new_nvlrqs_updated_at DATETIME = NULL, 
	@new_nvlrqs_status NVARCHAR(10) = NULL, 
	@new_nvlrqs_comments NVARCHAR(500) = NULL
AS
	DECLARE @v_token_user nvarchar(20);
	DECLARE @v_token_employee_id int;
	DECLARE @v_NOVELTY table (
		v_nvlrqs_id_Pk BIGINT, 
		v_nvlrqs_number NVARCHAR(MAX), 
		v_nvlrqs_external_identifier NVARCHAR(500), 
		v_nvlrqs_informed_Fk INT, 
		v_nvlrqs_responsible_Fk INT, 
		v_nvlrqs_accountable_Fk INT, 
		v_nvlrqs_request_Fk INT, 
		v_nvlrqs_details NVARCHAR(MAX), 
		v_nvlrqs_type_Fk BIGINT, 
		v_nvlrqs_topic NVARCHAR(50), 
		v_nvlrqs_registered_at DATETIME, 
		v_nvlrqs_updated_at DATETIME, 
		v_nvlrqs_status NVARCHAR(10), 
		v_nvlrqs_comments NVARCHAR(500)
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
			UPDATE [TBL_FAM_NOVELTY]
			SET
				[TBL_FAM_NOVELTY].[nvlrqs_number] = ISNULL(@new_nvlrqs_number, [TBL_FAM_NOVELTY].[nvlrqs_number]), 
				[TBL_FAM_NOVELTY].[nvlrqs_external_identifier] = ISNULL(@new_nvlrqs_external_identifier, [TBL_FAM_NOVELTY].[nvlrqs_external_identifier]), 
				[TBL_FAM_NOVELTY].[nvlrqs_informed_Fk] = ISNULL(@new_nvlrqs_informed_Fk, [TBL_FAM_NOVELTY].[nvlrqs_informed_Fk]), 
				[TBL_FAM_NOVELTY].[nvlrqs_responsible_Fk] = ISNULL(@new_nvlrqs_responsible_Fk, [TBL_FAM_NOVELTY].[nvlrqs_responsible_Fk]), 
				[TBL_FAM_NOVELTY].[nvlrqs_accountable_Fk] = ISNULL(@new_nvlrqs_accountable_Fk, [TBL_FAM_NOVELTY].[nvlrqs_accountable_Fk]), 
				[TBL_FAM_NOVELTY].[nvlrqs_request_Fk] = ISNULL(@new_nvlrqs_request_Fk, [TBL_FAM_NOVELTY].[nvlrqs_request_Fk]), 
				[TBL_FAM_NOVELTY].[nvlrqs_details] = ISNULL(@new_nvlrqs_details, [TBL_FAM_NOVELTY].[nvlrqs_details]), 
				[TBL_FAM_NOVELTY].[nvlrqs_type_Fk] = ISNULL(@new_nvlrqs_type_Fk, [TBL_FAM_NOVELTY].[nvlrqs_type_Fk]), 
				[TBL_FAM_NOVELTY].[nvlrqs_topic] = ISNULL(@new_nvlrqs_topic, [TBL_FAM_NOVELTY].[nvlrqs_topic]), 
				[TBL_FAM_NOVELTY].[nvlrqs_registered_at] = ISNULL(@new_nvlrqs_registered_at, [TBL_FAM_NOVELTY].[nvlrqs_registered_at]), 
				[TBL_FAM_NOVELTY].[nvlrqs_updated_at] = ISNULL(@new_nvlrqs_updated_at, [TBL_FAM_NOVELTY].[nvlrqs_updated_at]), 
				[TBL_FAM_NOVELTY].[nvlrqs_status] = ISNULL(@new_nvlrqs_status, [TBL_FAM_NOVELTY].[nvlrqs_status]), 
				[TBL_FAM_NOVELTY].[nvlrqs_comments] = ISNULL(@new_nvlrqs_comments, [TBL_FAM_NOVELTY].[nvlrqs_comments])
			OUTPUT 
				inserted.[nvlrqs_id_Pk], 
				inserted.[nvlrqs_number], 
				inserted.[nvlrqs_external_identifier], 
				inserted.[nvlrqs_informed_Fk], 
				inserted.[nvlrqs_responsible_Fk], 
				inserted.[nvlrqs_accountable_Fk], 
				inserted.[nvlrqs_request_Fk], 
				inserted.[nvlrqs_details], 
				inserted.[nvlrqs_type_Fk], 
				inserted.[nvlrqs_topic], 
				inserted.[nvlrqs_registered_at], 
				inserted.[nvlrqs_updated_at], 
				inserted.[nvlrqs_status], 
				inserted.[nvlrqs_comments]
			INTO @v_NOVELTY
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

			SELECT 
				v_nvlrqs_id_Pk, 
				v_nvlrqs_number, 
				v_nvlrqs_external_identifier, 
				v_nvlrqs_informed_Fk, 
				v_nvlrqs_responsible_Fk, 
				v_nvlrqs_accountable_Fk, 
				v_nvlrqs_request_Fk, 
				v_nvlrqs_details, 
				v_nvlrqs_type_Fk, 
				v_nvlrqs_topic, 
				v_nvlrqs_registered_at, 
				v_nvlrqs_updated_at, 
				v_nvlrqs_status, 
				v_nvlrqs_comments
			FROM @v_NOVELTY;
	END
RETURN 0									
