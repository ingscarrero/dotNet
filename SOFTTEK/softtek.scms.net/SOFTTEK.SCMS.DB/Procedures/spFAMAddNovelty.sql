CREATE PROCEDURE [dbo].[spFAMAddNovelty]
	@token_id NVARCHAR(50), 
	@new_nvlrqs_number NVARCHAR(MAX), 
	@new_nvlrqs_external_identifier NVARCHAR(500), 
	@new_nvlrqs_informed_Fk INT, 
	@new_nvlrqs_responsible_Fk INT, 
	@new_nvlrqs_accountable_Fk INT, 
	@new_nvlrqs_request_Fk INT, 
	@new_nvlrqs_details NVARCHAR(MAX), 
	@new_nvlrqs_type_Fk BIGINT, 
	@new_nvlrqs_topic NVARCHAR(50), 
	@new_nvlrqs_registered_at DATETIME, 
	@new_nvlrqs_updated_at DATETIME, 
	@new_nvlrqs_status NVARCHAR(10), 
	@new_nvlrqs_comments NVARCHAR(500)
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
			INSERT INTO [TBL_FAM_NOVELTY] (
				[TBL_FAM_NOVELTY].[nvlrqs_number], 
				[TBL_FAM_NOVELTY].[nvlrqs_external_identifier], 
				[TBL_FAM_NOVELTY].[nvlrqs_informed_Fk], 
				[TBL_FAM_NOVELTY].[nvlrqs_responsible_Fk], 
				[TBL_FAM_NOVELTY].[nvlrqs_accountable_Fk], 
				[TBL_FAM_NOVELTY].[nvlrqs_request_Fk], 
				[TBL_FAM_NOVELTY].[nvlrqs_details], 
				[TBL_FAM_NOVELTY].[nvlrqs_type_Fk], 
				[TBL_FAM_NOVELTY].[nvlrqs_topic], 
				[TBL_FAM_NOVELTY].[nvlrqs_registered_at], 
				[TBL_FAM_NOVELTY].[nvlrqs_updated_at], 
				[TBL_FAM_NOVELTY].[nvlrqs_status], 
				[TBL_FAM_NOVELTY].[nvlrqs_comments])
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
			VALUES (
				@new_nvlrqs_number, 
				@new_nvlrqs_external_identifier, 
				@new_nvlrqs_informed_Fk, 
				@new_nvlrqs_responsible_Fk, 
				@new_nvlrqs_accountable_Fk, 
				@new_nvlrqs_request_Fk, 
				@new_nvlrqs_details, 
				@new_nvlrqs_type_Fk, 
				@new_nvlrqs_topic, 
				@new_nvlrqs_registered_at, 
				@new_nvlrqs_updated_at, 
				@new_nvlrqs_status, 
				@new_nvlrqs_comments);

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
