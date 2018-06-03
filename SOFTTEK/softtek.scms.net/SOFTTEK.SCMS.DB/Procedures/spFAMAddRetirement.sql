CREATE PROCEDURE [dbo].[spFAMAddRetirement]
	@token_id NVARCHAR(50), 
	@new_rtr_number NVARCHAR(MAX), 
	@new_rtr_external_identifier NVARCHAR(500), 
	@new_rtr_informed_Fk INT, 
	@new_rtr_responsible_Fk INT, 
	@new_rtr_accountable_Fk INT, 
	@new_rtr_novelty_Fk INT, 
	@new_rtr_details NVARCHAR(MAX), 
	@new_rtr_type_Fk BIGINT, 
	@new_rtr_topic NVARCHAR(50), 
	@new_rtr_registered_at DATETIME, 
	@new_rtr_updated_at DATETIME, 
	@new_rtr_status NVARCHAR(10), 
	@new_rtr_comments NVARCHAR(500), 
	@new_rtr_reason NVARCHAR(500)
AS
	DECLARE @v_token_user nvarchar(20);
	DECLARE @v_token_employee_id int;
	DECLARE @v_RETIREMENT_REQUEST table (
		v_rtr_id_Pk BIGINT, 
		v_rtr_number NVARCHAR(MAX), 
		v_rtr_external_identifier NVARCHAR(500), 
		v_rtr_informed_Fk INT, 
		v_rtr_responsible_Fk INT, 
		v_rtr_accountable_Fk INT, 
		v_rtr_novelty_Fk INT, 
		v_rtr_details NVARCHAR(MAX), 
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
			INSERT INTO [TBL_FAM_RETIREMENT_REQUEST] (
				[TBL_FAM_RETIREMENT_REQUEST].[rtr_number], 
				[TBL_FAM_RETIREMENT_REQUEST].[rtr_external_identifier], 
				[TBL_FAM_RETIREMENT_REQUEST].[rtr_informed_Fk], 
				[TBL_FAM_RETIREMENT_REQUEST].[rtr_responsible_Fk], 
				[TBL_FAM_RETIREMENT_REQUEST].[rtr_accountable_Fk], 
				[TBL_FAM_RETIREMENT_REQUEST].[rtr_novelty_Fk], 
				[TBL_FAM_RETIREMENT_REQUEST].[rtr_details], 
				[TBL_FAM_RETIREMENT_REQUEST].[rtr_type_Fk], 
				[TBL_FAM_RETIREMENT_REQUEST].[rtr_topic], 
				[TBL_FAM_RETIREMENT_REQUEST].[rtr_registered_at], 
				[TBL_FAM_RETIREMENT_REQUEST].[rtr_updated_at], 
				[TBL_FAM_RETIREMENT_REQUEST].[rtr_status], 
				[TBL_FAM_RETIREMENT_REQUEST].[rtr_comments], 
				[TBL_FAM_RETIREMENT_REQUEST].[rtr_reason])
			OUTPUT 
				inserted.[rtr_id_Pk], 
				inserted.[rtr_number], 
				inserted.[rtr_external_identifier], 
				inserted.[rtr_informed_Fk], 
				inserted.[rtr_responsible_Fk], 
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
			VALUES (
				@new_rtr_number, 
				@new_rtr_external_identifier, 
				@new_rtr_informed_Fk, 
				@new_rtr_responsible_Fk, 
				@new_rtr_accountable_Fk, 
				@new_rtr_novelty_Fk, 
				@new_rtr_details, 
				@new_rtr_type_Fk, 
				@new_rtr_topic, 
				@new_rtr_registered_at, 
				@new_rtr_updated_at, 
				@new_rtr_status, 
				@new_rtr_comments, 
				@new_rtr_reason);

			SELECT 
				v_rtr_id_Pk, 
				v_rtr_number, 
				v_rtr_external_identifier, 
				v_rtr_informed_Fk, 
				v_rtr_responsible_Fk, 
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
