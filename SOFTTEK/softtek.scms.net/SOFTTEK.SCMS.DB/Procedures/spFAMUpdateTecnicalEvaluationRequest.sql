CREATE PROCEDURE [dbo].[spFAMUpdateTecnicalEvaluationRequest]
	@token_id NVARCHAR(50), 
	@filter_tchevlrqs_id_Pk BIGINT = NULL, 
	@filter_tchevlrqs_number VARCHAR(MAX)  = NULL, 
	@filter_tchevlrqs_informed_Fk INT = NULL, 
	@filter_tchevlrqs_responsible_Fk INT = NULL, 
	@filter_tchevlrqs_accountable_Fk INT = NULL, 
	@filter_tchevlrqs_details NVARCHAR(500) = NULL, 
	@filter_tchevlrqs_type_Fk BIGINT = NULL, 
	@filter_tchevlrqs_topic NVARCHAR(50) = NULL, 
	@filter_tchevlrqs_registered_at DATETIME  = NULL, 
	@filter_tchevlrqs_updated_at DATETIME  = NULL, 
	@filter_tchevlrqs_status NVARCHAR(10)  = NULL, 
	@filter_tchevlrqs_concept NVARCHAR(500) = NULL, 
	@filter_tchevlrqs_novelty_Fk BIGINT = NULL, 
	@new_tchevlrqs_number VARCHAR(MAX)  = NULL, 
	@new_tchevlrqs_informed_Fk INT = NULL, 
	@new_tchevlrqs_responsible_Fk INT = NULL, 
	@new_tchevlrqs_accountable_Fk INT = NULL, 
	@new_tchevlrqs_details NVARCHAR(500) = NULL, 
	@new_tchevlrqs_type_Fk BIGINT = NULL, 
	@new_tchevlrqs_topic NVARCHAR(50) = NULL, 
	@new_tchevlrqs_registered_at DATETIME  = NULL, 
	@new_tchevlrqs_updated_at DATETIME  = NULL, 
	@new_tchevlrqs_status NVARCHAR(10)  = NULL, 
	@new_tchevlrqs_comments NVARCHAR(500) = NULL, 
	@new_tchevlrqs_concept NVARCHAR(500) = NULL, 
	@new_tchevlrqs_novelty_Fk BIGINT = NULL
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
			UPDATE [TBL_FAM_TECNICAL_EVALUATION_RQ]
			SET
				[TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_number] = ISNULL(@new_tchevlrqs_number, [TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_number]), 
				[TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_informed_Fk] = ISNULL(@new_tchevlrqs_informed_Fk, [TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_informed_Fk]), 
				[TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_responsible_Fk] = ISNULL(@new_tchevlrqs_responsible_Fk, [TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_responsible_Fk]), 
				[TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_accountable_Fk] = ISNULL(@new_tchevlrqs_accountable_Fk, [TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_accountable_Fk]), 
				[TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_details] = ISNULL(@new_tchevlrqs_details, [TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_details]), 
				[TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_type_Fk] = ISNULL(@new_tchevlrqs_type_Fk, [TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_type_Fk]), 
				[TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_topic] = ISNULL(@new_tchevlrqs_topic, [TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_topic]), 
				[TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_registered_at] = ISNULL(@new_tchevlrqs_registered_at, [TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_registered_at]), 
				[TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_updated_at] = ISNULL(@new_tchevlrqs_updated_at, [TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_updated_at]), 
				[TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_status] = ISNULL(@new_tchevlrqs_status, [TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_status]), 
				[TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_comments] = ISNULL(@new_tchevlrqs_comments, [TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_comments]), 
				[TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_concept] = ISNULL(@new_tchevlrqs_concept, [TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_concept]), 
				[TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_novelty_Fk] = ISNULL(@new_tchevlrqs_novelty_Fk, [TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_novelty_Fk])
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
			WHERE 
				[TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_id_Pk] = ISNULL(@filter_tchevlrqs_id_Pk, [TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_id_Pk])
				AND [TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_number] = ISNULL(@filter_tchevlrqs_number, [TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_number])
				AND [TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_informed_Fk] = ISNULL(@filter_tchevlrqs_informed_Fk, [TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_informed_Fk])
				AND [TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_responsible_Fk] = ISNULL(@filter_tchevlrqs_responsible_Fk, [TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_responsible_Fk])
				AND [TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_accountable_Fk] = ISNULL(@filter_tchevlrqs_accountable_Fk, [TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_accountable_Fk])
				AND [TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_details] = ISNULL(@filter_tchevlrqs_details, [TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_details])
				AND [TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_type_Fk] = ISNULL(@filter_tchevlrqs_type_Fk, [TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_type_Fk])
				AND [TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_topic] = ISNULL(@filter_tchevlrqs_topic, [TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_topic])
				AND [TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_registered_at] = ISNULL(@filter_tchevlrqs_registered_at, [TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_registered_at])
				AND [TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_updated_at] = ISNULL(@filter_tchevlrqs_updated_at, [TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_updated_at])
				AND [TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_status] = ISNULL(@filter_tchevlrqs_status, [TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_status])
				AND [TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_concept] = ISNULL(@filter_tchevlrqs_concept, [TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_concept])
				AND [TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_novelty_Fk] = ISNULL(@filter_tchevlrqs_novelty_Fk, [TBL_FAM_TECNICAL_EVALUATION_RQ].[tchevlrqs_novelty_Fk]);

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
