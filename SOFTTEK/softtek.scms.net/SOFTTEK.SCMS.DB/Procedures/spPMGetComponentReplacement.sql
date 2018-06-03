CREATE PROCEDURE [dbo].[spPMGetComponentReplacement]
	@token_id NVARCHAR(50), 
	@filter_cmprpl_id_Pk BIGINT = NULL, 
	@filter_cmprpl_external_identifier NVARCHAR(500) = NULL, 
	@filter_cmprpl_task_Fk BIGINT = NULL, 
	@filter_cmprpl_material_Fk BIGINT = NULL, 
	@filter_cmprpl_replaced_material_Fk BIGINT = NULL
AS
	DECLARE @v_token_user nvarchar(20);
	DECLARE @v_token_employee_id int;
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
			SELECT
				[TBL_PM_COMPONENT_REPLACEMENT].[cmprpl_id_Pk], 
				[TBL_PM_COMPONENT_REPLACEMENT].[cmprpl_external_identifier], 
				[TBL_PM_TASK].[tsk_id_Pk] as [cmprpl_material_Fk], 
				[material].[mtr_id_Pk] as [cmprpl_replaced_material_Fk], 
				[material_replaced].[mtr_id_Pk],
				[TBL_PM_COMPONENT_REPLACEMENT].[cmprpl_comments]
			FROM [TBL_PM_COMPONENT_REPLACEMENT]
				INNER JOIN [TBL_PM_TASK] ON [TBL_PM_TASK].[tsk_id_Pk] = [TBL_PM_COMPONENT_REPLACEMENT].[cmprpl_task_Fk]
				INNER JOIN [TBL_PM_MATERIAL] AS [material] ON [material].[mtr_id_Pk] = [TBL_PM_COMPONENT_REPLACEMENT].[cmprpl_material_Fk]
				INNER JOIN [TBL_PM_MATERIAL] AS [material_replaced] ON [material_replaced].[mtr_id_Pk] = [TBL_PM_COMPONENT_REPLACEMENT].[cmprpl_replaced_material_Fk]
			WHERE 
				[TBL_PM_COMPONENT_REPLACEMENT].[cmprpl_id_Pk] = ISNULL(@filter_cmprpl_id_Pk, [TBL_PM_COMPONENT_REPLACEMENT].[cmprpl_id_Pk])
				AND [TBL_PM_COMPONENT_REPLACEMENT].[cmprpl_external_identifier] = ISNULL(@filter_cmprpl_external_identifier, [TBL_PM_COMPONENT_REPLACEMENT].[cmprpl_external_identifier])
				AND [TBL_PM_COMPONENT_REPLACEMENT].[cmprpl_task_Fk] = ISNULL(@filter_cmprpl_task_Fk, [TBL_PM_COMPONENT_REPLACEMENT].[cmprpl_task_Fk])
				AND [TBL_PM_COMPONENT_REPLACEMENT].[cmprpl_material_Fk] = ISNULL(@filter_cmprpl_material_Fk, [TBL_PM_COMPONENT_REPLACEMENT].[cmprpl_material_Fk])
				AND [TBL_PM_COMPONENT_REPLACEMENT].[cmprpl_replaced_material_Fk] = ISNULL(@filter_cmprpl_replaced_material_Fk, [TBL_PM_COMPONENT_REPLACEMENT].[cmprpl_replaced_material_Fk]);

	END
RETURN 0									
