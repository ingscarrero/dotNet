CREATE PROCEDURE [dbo].[spFAMUpdateRequest]
	@token_id NVARCHAR(50), 
	@filter_rqs_id_Pk BIGINT = NULL, 
	@filter_rqs_number NVARCHAR(MAX) = NULL, 
	@filter_rqs_informed_Fk INT = NULL, 
	@filter_rqs_responsible_Fk INT = NULL, 
	@filter_rqs_accountable_Fk INT = NULL, 
	@filter_rqs_details NVARCHAR(500) = NULL, 
	@filter_rqs_type_Fk BIGINT = NULL, 
	@filter_rqs_topic NVARCHAR(50) = NULL, 
	@filter_rqs_registered_at DATETIME = NULL, 
	@filter_rqs_updated_at DATETIME = NULL, 
	@filter_rqs_status NVARCHAR(10) = NULL, 
	@filter_rqs_comments NVARCHAR(500) = NULL, 
	@new_rqs_number NVARCHAR(MAX) = NULL, 
	@new_rqs_informed_Fk INT = NULL, 
	@new_rqs_responsible_Fk INT = NULL, 
	@new_rqs_accountable_Fk INT = NULL, 
	@new_rqs_details NVARCHAR(500) = NULL, 
	@new_rqs_type_Fk BIGINT = NULL, 
	@new_rqs_topic NVARCHAR(50) = NULL, 
	@new_rqs_registered_at DATETIME = NULL,  
	@new_rqs_updated_at DATETIME = NULL,  
	@new_rqs_status NVARCHAR(10) = NULL, 
	@new_rqs_comments NVARCHAR(500) = NULL
AS
	DECLARE @v_token_user nvarchar(20);
	DECLARE @v_token_employee_id int;
	DECLARE @v_REQUEST table (
		v_rqs_id_Pk BIGINT, 
		v_rqs_number NVARCHAR(MAX), 
		v_rqs_informed_Fk INT, 
		v_rqs_responsible_Fk INT, 
		v_rqs_accountable_Fk INT, 
		v_rqs_details NVARCHAR(500), 
		v_rqs_type_Fk BIGINT, 
		v_rqs_topic NVARCHAR(50), 
		v_rqs_registered_at DATETIME, 
		v_rqs_created_by NVARCHAR(20), 
		v_rqs_updated_at DATETIME, 
		v_rqs_modified_by NVARCHAR(20), 
		v_rqs_status NVARCHAR(10), 
		v_rqs_comments NVARCHAR(500)
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
			UPDATE [TBL_FAM_REQUEST]
			SET
				[TBL_FAM_REQUEST].[rqs_number] = ISNULL(@new_rqs_number, [TBL_FAM_REQUEST].[rqs_number]), 
				[TBL_FAM_REQUEST].[rqs_informed_Fk] = ISNULL(@new_rqs_informed_Fk, [TBL_FAM_REQUEST].[rqs_informed_Fk]), 
				[TBL_FAM_REQUEST].[rqs_responsible_Fk] = ISNULL(@new_rqs_responsible_Fk, [TBL_FAM_REQUEST].[rqs_responsible_Fk]), 
				[TBL_FAM_REQUEST].[rqs_accountable_Fk] = ISNULL(@new_rqs_accountable_Fk, [TBL_FAM_REQUEST].[rqs_accountable_Fk]), 
				[TBL_FAM_REQUEST].[rqs_details] = ISNULL(@new_rqs_details, [TBL_FAM_REQUEST].[rqs_details]), 
				[TBL_FAM_REQUEST].[rqs_type_Fk] = ISNULL(@new_rqs_type_Fk, [TBL_FAM_REQUEST].[rqs_type_Fk]), 
				[TBL_FAM_REQUEST].[rqs_topic] = ISNULL(@new_rqs_topic, [TBL_FAM_REQUEST].[rqs_topic]), 
				[TBL_FAM_REQUEST].[rqs_registered_at] = ISNULL(@new_rqs_registered_at, [TBL_FAM_REQUEST].[rqs_registered_at]), 
				[TBL_FAM_REQUEST].[rqs_updated_at] = ISNULL(@new_rqs_updated_at, [TBL_FAM_REQUEST].[rqs_updated_at]), 
				[TBL_FAM_REQUEST].[rqs_status] = ISNULL(@new_rqs_status, [TBL_FAM_REQUEST].[rqs_status]), 
				[TBL_FAM_REQUEST].[rqs_comments] = ISNULL(@new_rqs_comments, [TBL_FAM_REQUEST].[rqs_comments])
			OUTPUT 
				inserted.[rqs_id_Pk], 
				inserted.[rqs_number], 
				inserted.[rqs_informed_Fk], 
				inserted.[rqs_responsible_Fk], 
				inserted.[rqs_accountable_Fk], 
				inserted.[rqs_details], 
				inserted.[rqs_type_Fk], 
				inserted.[rqs_topic], 
				inserted.[rqs_registered_at], 
				inserted.[rqs_created_by], 
				inserted.[rqs_updated_at], 
				inserted.[rqs_modified_by], 
				inserted.[rqs_status], 
				inserted.[rqs_comments]
			INTO @v_REQUEST
			WHERE 
				[TBL_FAM_REQUEST].[rqs_id_Pk] = ISNULL(@filter_rqs_id_Pk, [TBL_FAM_REQUEST].[rqs_id_Pk])
				AND [TBL_FAM_REQUEST].[rqs_number] = ISNULL(@filter_rqs_number, [TBL_FAM_REQUEST].[rqs_number])
				AND [TBL_FAM_REQUEST].[rqs_informed_Fk] = ISNULL(@filter_rqs_informed_Fk, [TBL_FAM_REQUEST].[rqs_informed_Fk])
				AND [TBL_FAM_REQUEST].[rqs_responsible_Fk] = ISNULL(@filter_rqs_responsible_Fk, [TBL_FAM_REQUEST].[rqs_responsible_Fk])
				AND [TBL_FAM_REQUEST].[rqs_accountable_Fk] = ISNULL(@filter_rqs_accountable_Fk, [TBL_FAM_REQUEST].[rqs_accountable_Fk])
				AND [TBL_FAM_REQUEST].[rqs_details] = ISNULL(@filter_rqs_details, [TBL_FAM_REQUEST].[rqs_details])
				AND [TBL_FAM_REQUEST].[rqs_type_Fk] = ISNULL(@filter_rqs_type_Fk, [TBL_FAM_REQUEST].[rqs_type_Fk])
				AND [TBL_FAM_REQUEST].[rqs_topic] = ISNULL(@filter_rqs_topic, [TBL_FAM_REQUEST].[rqs_topic])
				AND [TBL_FAM_REQUEST].[rqs_registered_at] = ISNULL(@filter_rqs_registered_at, [TBL_FAM_REQUEST].[rqs_registered_at])
				AND [TBL_FAM_REQUEST].[rqs_updated_at] = ISNULL(@filter_rqs_updated_at, [TBL_FAM_REQUEST].[rqs_updated_at])
				AND [TBL_FAM_REQUEST].[rqs_status] = ISNULL(@filter_rqs_status, [TBL_FAM_REQUEST].[rqs_status])
				AND [TBL_FAM_REQUEST].[rqs_comments] = ISNULL(@filter_rqs_comments, [TBL_FAM_REQUEST].[rqs_comments]);

			SELECT 
				v_rqs_id_Pk, 
				v_rqs_number, 
				v_rqs_informed_Fk, 
				v_rqs_responsible_Fk, 
				v_rqs_accountable_Fk, 
				v_rqs_details, 
				v_rqs_type_Fk, 
				v_rqs_topic, 
				v_rqs_registered_at, 
				v_rqs_created_by, 
				v_rqs_updated_at, 
				v_rqs_modified_by, 
				v_rqs_status, 
				v_rqs_comments
			FROM @v_REQUEST;
	END
RETURN 0									
