CREATE PROCEDURE [dbo].[spPMAddComponentReplacement]
	@token_id NVARCHAR(50), 
	@new_cmprpl_external_identifier NVARCHAR(500), 
	@new_cmprpl_task_Fk BIGINT, 
	@new_cmprpl_material_Fk BIGINT, 
	@new_cmprpl_replaced_material_Fk BIGINT,
	@new_cmprpl_comments NVARCHAR(MAX)
AS
	DECLARE @v_token_user nvarchar(20);
	DECLARE @v_token_employee_id int;
	DECLARE @v_ONENT_REPLACEMENT table (
		v_cmprpl_id_Pk BIGINT, 
		v_cmprpl_external_identifier NVARCHAR(500), 
		v_cmprpl_task_Fk BIGINT, 
		v_cmprpl_material_Fk BIGINT, 
		v_cmprpl_replaced_material_Fk BIGINT,
		v_cmprpl_comments NVARCHAR(MAX)
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
			INSERT INTO [TBL_PM_COMPONENT_REPLACEMENT] (
				[TBL_PM_COMPONENT_REPLACEMENT].[cmprpl_external_identifier], 
				[TBL_PM_COMPONENT_REPLACEMENT].[cmprpl_task_Fk], 
				[TBL_PM_COMPONENT_REPLACEMENT].[cmprpl_material_Fk], 
				[TBL_PM_COMPONENT_REPLACEMENT].[cmprpl_replaced_material_Fk],
				[TBL_PM_COMPONENT_REPLACEMENT].[cmprpl_comments])
			OUTPUT 
				inserted.[cmprpl_id_Pk], 
				inserted.[cmprpl_external_identifier], 
				inserted.[cmprpl_task_Fk], 
				inserted.[cmprpl_material_Fk], 
				inserted.[cmprpl_replaced_material_Fk],
				inserted.[cmprpl_comments]
			INTO @v_ONENT_REPLACEMENT
			VALUES (
				@new_cmprpl_external_identifier, 
				@new_cmprpl_task_Fk, 
				@new_cmprpl_material_Fk, 
				@new_cmprpl_replaced_material_Fk,
				@new_cmprpl_comments);

			SELECT 
				v_cmprpl_id_Pk, 
				v_cmprpl_external_identifier, 
				v_cmprpl_task_Fk, 
				v_cmprpl_material_Fk, 
				v_cmprpl_replaced_material_Fk,
				v_cmprpl_comments
			FROM @v_ONENT_REPLACEMENT;
	END
RETURN 0									
