CREATE PROCEDURE [dbo].[spFAMAddTecnicalEvaluationRequest]
	@token_id NVARCHAR(50), 
	@new_tchevlrqs_number VARCHAR(MAX) , 
	@new_tchevlrqs_informed_Fk INT, 
	@new_tchevlrqs_responsible_Fk INT, 
	@new_tchevlrqs_accountable_Fk INT, 
	@new_tchevlrqs_details NVARCHAR(500), 
	@new_tchevlrqs_type_Fk BIGINT, 
	@new_tchevlrqs_topic NVARCHAR(50), 
	@new_tchevlrqs_registered_at DATETIME , 
	@new_tchevlrqs_updated_at DATETIME , 
	@new_tchevlrqs_status NVARCHAR(10) , 
	@new_tchevlrqs_comments NVARCHAR(500), 
	@new_tchevlrqs_concept NVARCHAR(500), 
	@new_tchevlrqs_novelty_Fk BIGINT
AS
	DECLARE @v_token_user nvarchar(20);
	DECLARE @v_token_employee_id int;
	DECLARE @v_TECNICAL_EVALUATION_RQ table (
		v_tchevlrqs_id_Pk BIGINT, 
		v_tchevlrqs_number VARCHAR(MAX) , 
		v_tchevlrqs_informed_Fk INT, 
		v_tchevlrqs_responsible_Fk INT, 
		v_tchevlrqs_accountable_Fk INT, 
		v_tchevlrqs_details NVARCHAR(500), 
		v_tchevlrqs_type_Fk BIGINT, 
		v_tchevlrqs_topic NVARCHAR(50), 
		v_tchevlrqs_registered_at DATETIME , 
		v_tchevlrqs_updated_at DATETIME , 
		v_tchevlrqs_status NVARCHAR(10) , 
		v_tchevlrqs_comments NVARCHAR(500), 
		v_tchevlrqs_concept NVARCHAR(500), 
		v_tchevlrqs_novelty_Fk BIGINT
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
			INSERT INTO [TBL_FAM_TECNICAL_EVALUATION_RQ] (
				[TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_number], 
				[TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_informed_Fk], 
				[TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_responsible_Fk], 
				[TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_accountable_Fk], 
				[TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_details], 
				[TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_type_Fk], 
				[TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_topic], 
				[TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_registered_at], 
				[TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_updated_at], 
				[TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_status], 
				[TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_comments], 
				[TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_concept], 
				[TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_novelty_Fk])
			OUTPUT 
				inserted.[tchevlrqs_id_Pk], 
				inserted.[tchevlrqs_number], 
				inserted.[tchevlrqs_informed_Fk], 
				inserted.[tchevlrqs_responsible_Fk], 
				inserted.[tchevlrqs_accountable_Fk], 
				inserted.[tchevlrqs_details], 
				inserted.[tchevlrqs_type_Fk], 
				inserted.[tchevlrqs_topic], 
				inserted.[tchevlrqs_registered_at], 
				inserted.[tchevlrqs_updated_at], 
				inserted.[tchevlrqs_status], 
				inserted.[tchevlrqs_comments], 
				inserted.[tchevlrqs_concept], 
				inserted.[tchevlrqs_novelty_Fk]
			INTO @v_TECNICAL_EVALUATION_RQ
			VALUES (
				@new_tchevlrqs_number, 
				@new_tchevlrqs_informed_Fk, 
				@new_tchevlrqs_responsible_Fk, 
				@new_tchevlrqs_accountable_Fk, 
				@new_tchevlrqs_details, 
				@new_tchevlrqs_type_Fk, 
				@new_tchevlrqs_topic, 
				@new_tchevlrqs_registered_at, 
				@new_tchevlrqs_updated_at, 
				@new_tchevlrqs_status, 
				@new_tchevlrqs_comments, 
				@new_tchevlrqs_concept, 
				@new_tchevlrqs_novelty_Fk);

			SELECT 
				v_tchevlrqs_id_Pk, 
				v_tchevlrqs_number, 
				v_tchevlrqs_informed_Fk, 
				v_tchevlrqs_responsible_Fk, 
				v_tchevlrqs_accountable_Fk, 
				v_tchevlrqs_details, 
				v_tchevlrqs_type_Fk, 
				v_tchevlrqs_topic, 
				v_tchevlrqs_registered_at, 
				v_tchevlrqs_updated_at, 
				v_tchevlrqs_status, 
				v_tchevlrqs_comments, 
				v_tchevlrqs_concept, 
				v_tchevlrqs_novelty_Fk
			FROM @v_TECNICAL_EVALUATION_RQ;
	END
RETURN 0									
