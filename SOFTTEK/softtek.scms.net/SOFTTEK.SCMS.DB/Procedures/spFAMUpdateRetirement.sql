CREATE PROCEDURE [dbo].[spFAMUpdateRetirement]
	@token_id NVARCHAR(50), 
	@filter_rtr_id_Pk BIGINT = NULL, 
	@filter_rtr_number VARCHAR(MAX) = NULL, 
	@filter_rtr_external_identifier NVARCHAR(500) = NULL, 
	@filter_rtr_responsible_Fk INT = NULL, 
	@filter_rtr_informed_Fk INT = NULL, 
	@filter_rtr_accountable_Fk INT = NULL, 
	@filter_rtr_novelty_Fk BIGINT = NULL, 
	@filter_rtr_details NVARCHAR(500) = NULL, 
	@filter_rtr_type_Fk BIGINT = NULL, 
	@filter_rtr_topic NVARCHAR(50) = NULL, 
	@filter_rtr_status NVARCHAR(10) = NULL, 
	@filter_rtr_comments NVARCHAR(500) = NULL, 
	@filter_rtr_reason NVARCHAR(500) = NULL, 
	@new_rtr_number VARCHAR(MAX) = NULL, 
	@new_rtr_external_identifier NVARCHAR(500) = NULL, 
	@new_rtr_responsible_Fk INT = NULL, 
	@new_rtr_informed_Fk INT = NULL, 
	@new_rtr_accountable_Fk INT = NULL, 
	@new_rtr_novelty_Fk BIGINT = NULL, 
	@new_rtr_details NVARCHAR(500) = NULL, 
	@new_rtr_type_Fk BIGINT = NULL, 
	@new_rtr_topic NVARCHAR(50) = NULL, 
	@new_rtr_registered_at DATETIME = NULL, 
	@new_rtr_updated_at DATETIME = NULL, 
	@new_rtr_status NVARCHAR(10) = NULL, 
	@new_rtr_comments NVARCHAR(500) = NULL, 
	@new_rtr_reason NVARCHAR(500) = NULL
AS
	DECLARE @v_token_user nvarchar(20);
	DECLARE @v_token_employee_id int;
	DECLARE @v_RETIREMENT_REQUEST table (
		v_rtr_id_Pk BIGINT, 
		v_rtr_number VARCHAR(MAX), 
		v_rtr_external_identifier NVARCHAR(500), 
		v_rtr_responsible_Fk INT, 
		v_rtr_informed_Fk INT, 
		v_rtr_accountable_Fk INT, 
		v_rtr_novelty_Fk BIGINT, 
		v_rtr_details NVARCHAR(500), 
		v_rtr_type_Fk BIGINT, 
		v_rtr_topic NVARCHAR(50), 
		v_rtr_registered_at DATETIME, 
		v_rtr_updated_at DATETIME, 
		v_rtr_status NVARCHAR(10), 
		v_rtr_comments NVARCHAR(500), 
		v_rtr_reason NVARCHAR(500)
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
			UPDATE [TBL_FAM_RETIREMENT_REQUEST]
			SET
				[TBL_FAM_RETIREMENT_REQUEST].[rtr_number] = ISNULL(@new_rtr_number, [TBL_FAM_RETIREMENT_REQUEST].[rtr_number]), 
				[TBL_FAM_RETIREMENT_REQUEST].[rtr_external_identifier] = ISNULL(@new_rtr_external_identifier, [TBL_FAM_RETIREMENT_REQUEST].[rtr_external_identifier]), 
				[TBL_FAM_RETIREMENT_REQUEST].[rtr_responsible_Fk] = ISNULL(@new_rtr_responsible_Fk, [TBL_FAM_RETIREMENT_REQUEST].[rtr_responsible_Fk]), 
				[TBL_FAM_RETIREMENT_REQUEST].[rtr_informed_Fk] = ISNULL(@new_rtr_informed_Fk, [TBL_FAM_RETIREMENT_REQUEST].[rtr_informed_Fk]), 
				[TBL_FAM_RETIREMENT_REQUEST].[rtr_accountable_Fk] = ISNULL(@new_rtr_accountable_Fk, [TBL_FAM_RETIREMENT_REQUEST].[rtr_accountable_Fk]), 
				[TBL_FAM_RETIREMENT_REQUEST].[rtr_novelty_Fk] = ISNULL(@new_rtr_novelty_Fk, [TBL_FAM_RETIREMENT_REQUEST].[rtr_novelty_Fk]), 
				[TBL_FAM_RETIREMENT_REQUEST].[rtr_details] = ISNULL(@new_rtr_details, [TBL_FAM_RETIREMENT_REQUEST].[rtr_details]), 
				[TBL_FAM_RETIREMENT_REQUEST].[rtr_type_Fk] = ISNULL(@new_rtr_type_Fk, [TBL_FAM_RETIREMENT_REQUEST].[rtr_type_Fk]), 
				[TBL_FAM_RETIREMENT_REQUEST].[rtr_topic] = ISNULL(@new_rtr_topic, [TBL_FAM_RETIREMENT_REQUEST].[rtr_topic]), 
				[TBL_FAM_RETIREMENT_REQUEST].[rtr_registered_at] = ISNULL(@new_rtr_registered_at, [TBL_FAM_RETIREMENT_REQUEST].[rtr_registered_at]), 
				[TBL_FAM_RETIREMENT_REQUEST].[rtr_updated_at] = ISNULL(@new_rtr_updated_at, [TBL_FAM_RETIREMENT_REQUEST].[rtr_updated_at]), 
				[TBL_FAM_RETIREMENT_REQUEST].[rtr_status] = ISNULL(@new_rtr_status, [TBL_FAM_RETIREMENT_REQUEST].[rtr_status]), 
				[TBL_FAM_RETIREMENT_REQUEST].[rtr_comments] = ISNULL(@new_rtr_comments, [TBL_FAM_RETIREMENT_REQUEST].[rtr_comments]), 
				[TBL_FAM_RETIREMENT_REQUEST].[rtr_reason] = ISNULL(@new_rtr_reason, [TBL_FAM_RETIREMENT_REQUEST].[rtr_reason])
			OUTPUT 
				inserted.[rtr_id_Pk], 
				inserted.[rtr_number], 
				inserted.[rtr_external_identifier], 
				inserted.[rtr_responsible_Fk], 
				inserted.[rtr_informed_Fk], 
				inserted.[rtr_accountable_Fk], 
				inserted.[rtr_novelty_Fk], 
				inserted.[rtr_details], 
				inserted.[rtr_type_Fk], 
				inserted.[rtr_topic], 
				inserted.[rtr_registered_at], 
				inserted.[rtr_updated_at], 
				inserted.[rtr_status], 
				inserted.[rtr_comments], 
				inserted.[rtr_reason]
			INTO @v_RETIREMENT_REQUEST
			WHERE 
				[TBL_FAM_RETIREMENT_REQUEST].[rtr_id_Pk] = ISNULL(@filter_rtr_id_Pk, [TBL_FAM_RETIREMENT_REQUEST].[rtr_id_Pk])
				AND [TBL_FAM_RETIREMENT_REQUEST].[rtr_number] = ISNULL(@filter_rtr_number, [TBL_FAM_RETIREMENT_REQUEST].[rtr_number])
				AND [TBL_FAM_RETIREMENT_REQUEST].[rtr_external_identifier] = ISNULL(@filter_rtr_external_identifier, [TBL_FAM_RETIREMENT_REQUEST].[rtr_external_identifier])
				AND [TBL_FAM_RETIREMENT_REQUEST].[rtr_responsible_Fk] = ISNULL(@filter_rtr_responsible_Fk, [TBL_FAM_RETIREMENT_REQUEST].[rtr_responsible_Fk])
				AND [TBL_FAM_RETIREMENT_REQUEST].[rtr_informed_Fk] = ISNULL(@filter_rtr_informed_Fk, [TBL_FAM_RETIREMENT_REQUEST].[rtr_informed_Fk])
				AND [TBL_FAM_RETIREMENT_REQUEST].[rtr_accountable_Fk] = ISNULL(@filter_rtr_accountable_Fk, [TBL_FAM_RETIREMENT_REQUEST].[rtr_accountable_Fk])
				AND [TBL_FAM_RETIREMENT_REQUEST].[rtr_novelty_Fk] = ISNULL(@filter_rtr_novelty_Fk, [TBL_FAM_RETIREMENT_REQUEST].[rtr_novelty_Fk])
				AND [TBL_FAM_RETIREMENT_REQUEST].[rtr_details] = ISNULL(@filter_rtr_details, [TBL_FAM_RETIREMENT_REQUEST].[rtr_details])
				AND [TBL_FAM_RETIREMENT_REQUEST].[rtr_type_Fk] = ISNULL(@filter_rtr_type_Fk, [TBL_FAM_RETIREMENT_REQUEST].[rtr_type_Fk])
				AND [TBL_FAM_RETIREMENT_REQUEST].[rtr_topic] = ISNULL(@filter_rtr_topic, [TBL_FAM_RETIREMENT_REQUEST].[rtr_topic])
				AND [TBL_FAM_RETIREMENT_REQUEST].[rtr_status] = ISNULL(@filter_rtr_status, [TBL_FAM_RETIREMENT_REQUEST].[rtr_status])
				AND [TBL_FAM_RETIREMENT_REQUEST].[rtr_comments] = ISNULL(@filter_rtr_comments, [TBL_FAM_RETIREMENT_REQUEST].[rtr_comments])
				AND [TBL_FAM_RETIREMENT_REQUEST].[rtr_reason] = ISNULL(@filter_rtr_reason, [TBL_FAM_RETIREMENT_REQUEST].[rtr_reason]);

			SELECT 
				v_rtr_id_Pk, 
				v_rtr_number, 
				v_rtr_external_identifier, 
				v_rtr_responsible_Fk, 
				v_rtr_informed_Fk, 
				v_rtr_accountable_Fk, 
				v_rtr_novelty_Fk, 
				v_rtr_details, 
				v_rtr_type_Fk, 
				v_rtr_topic, 
				v_rtr_registered_at, 
				v_rtr_updated_at, 
				v_rtr_status, 
				v_rtr_comments, 
				v_rtr_reason
			FROM @v_RETIREMENT_REQUEST;
	END
RETURN 0									
