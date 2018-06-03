CREATE PROCEDURE [dbo].[spFAMAddRequest]
	@token_id NVARCHAR(50), 
	@new_rqs_number VARCHAR(MAX), 
	@new_rqs_responsible_Fk INT, 
	@new_rqs_informed_Fk INT, 
	@new_rqs_accountable_Fk INT, 
	@new_rqs_details NVARCHAR(500), 
	@new_rqs_type_Fk BIGINT, 
	@new_rqs_topic NVARCHAR(50), 
	@new_rqs_registered_at DATETIME, 
	@new_rqs_updated_at DATETIME, 
	@new_rqs_status NVARCHAR(10), 
	@new_rqs_comments NVARCHAR(500)
AS
	DECLARE @v_token_user nvarchar(20);
	DECLARE @v_token_employee_id int;
	DECLARE @v_REQUEST table (
		v_rqs_id_Pk BIGINT, 
		v_rqs_number VARCHAR(MAX), 
		v_rqs_responsible_Fk INT, 
		v_rqs_informed_Fk INT, 
		v_rqs_accountable_Fk INT, 
		v_rqs_details NVARCHAR(500), 
		v_rqs_type_Fk BIGINT, 
		v_rqs_topic NVARCHAR(50), 
		v_rqs_registered_at DATETIME, 
		v_rqs_updated_at DATETIME, 
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
			INSERT INTO [TBL_FAM_REQUEST] (
				[TBL_FAM_REQUEST].[rqs_number], 
				[TBL_FAM_REQUEST].[rqs_responsible_Fk], 
				[TBL_FAM_REQUEST].[rqs_informed_Fk], 
				[TBL_FAM_REQUEST].[rqs_accountable_Fk], 
				[TBL_FAM_REQUEST].[rqs_details], 
				[TBL_FAM_REQUEST].[rqs_type_Fk], 
				[TBL_FAM_REQUEST].[rqs_topic], 
				[TBL_FAM_REQUEST].[rqs_registered_at], 
				[TBL_FAM_REQUEST].[rqs_updated_at], 
				[TBL_FAM_REQUEST].[rqs_status], 
				[TBL_FAM_REQUEST].[rqs_comments])
			OUTPUT 
				inserted.[rqs_id_Pk], 
				inserted.[rqs_number], 
				inserted.[rqs_responsible_Fk], 
				inserted.[rqs_informed_Fk], 
				inserted.[rqs_accountable_Fk], 
				inserted.[rqs_details], 
				inserted.[rqs_type_Fk], 
				inserted.[rqs_topic], 
				inserted.[rqs_registered_at], 
				inserted.[rqs_updated_at], 
				inserted.[rqs_status], 
				inserted.[rqs_comments]
			INTO @v_REQUEST
			VALUES (
				@new_rqs_number, 
				@new_rqs_responsible_Fk, 
				@new_rqs_informed_Fk, 
				@new_rqs_accountable_Fk, 
				@new_rqs_details, 
				@new_rqs_type_Fk, 
				@new_rqs_topic, 
				@new_rqs_registered_at, 
				@new_rqs_updated_at, 
				@new_rqs_status, 
				@new_rqs_comments);

			SELECT 
				v_rqs_id_Pk, 
				v_rqs_number, 
				v_rqs_responsible_Fk, 
				v_rqs_informed_Fk, 
				v_rqs_accountable_Fk, 
				v_rqs_details, 
				v_rqs_type_Fk, 
				v_rqs_topic, 
				v_rqs_registered_at, 
				v_rqs_updated_at, 
				v_rqs_status, 
				v_rqs_comments
			FROM @v_REQUEST;
	END
RETURN 0									
