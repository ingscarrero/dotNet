CREATE PROCEDURE [dbo].[spPMGetMaterial]
	@token_id NVARCHAR(50), 
	@filter_mtr_id_Pk bigint = NULL, 
	@filter_mtr_external_identifier NVARCHAR(500) = NULL,
	@filter_mtr_name NVARCHAR(300) = NULL, 
	@filter_mtr_description NVARCHAR(300) = NULL, 
	@filter_mtr_class NVARCHAR(MAX) = NULL, 
	@filter_mtr_stock nvarchar(100) = NULL,
	@filter_mtr_task_Fk BIGINT = NULL,
	@filter_mtr_material_parameter BIGINT = NULL
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
				[TBL_PM_MATERIAL].[mtr_id_Pk], 
				[TBL_PM_MATERIAL].[mtr_external_identifier],
				[TBL_PM_MATERIAL].[mtr_name], 
				[TBL_PM_MATERIAL].[mtr_description], 
				[TBL_PM_MATERIAL].[mtr_class], 
				[TBL_PM_MATERIAL].[mtr_stock],
				[TBL_PM_MATERIAL].[mtr_task_Fk],
				[TBL_PM_MATERIAL].[mtr_material_parameter],
				[TBL_PM_MATERIAL].[mtr_observations]
			FROM [TBL_PM_MATERIAL]
			WHERE 
				[TBL_PM_MATERIAL].[mtr_id_Pk] = ISNULL(@filter_mtr_id_Pk, [TBL_PM_MATERIAL].[mtr_id_Pk])
				AND [TBL_PM_MATERIAL].[mtr_name] = ISNULL(@filter_mtr_name, [TBL_PM_MATERIAL].[mtr_name])
				AND [TBL_PM_MATERIAL].[mtr_external_identifier] = ISNULL(@filter_mtr_external_identifier, [TBL_PM_MATERIAL].[mtr_external_identifier])
				AND [TBL_PM_MATERIAL].[mtr_description] = ISNULL(@filter_mtr_description, [TBL_PM_MATERIAL].[mtr_description])
				AND [TBL_PM_MATERIAL].[mtr_class] = ISNULL(@filter_mtr_class, [TBL_PM_MATERIAL].[mtr_class])
				AND [TBL_PM_MATERIAL].[mtr_stock] = ISNULL(@filter_mtr_stock, [TBL_PM_MATERIAL].[mtr_stock])
				AND [TBL_PM_MATERIAL].[mtr_task_Fk] = ISNULL(@filter_mtr_task_Fk, [TBL_PM_MATERIAL].[mtr_task_Fk])
				AND [TBL_PM_MATERIAL].[mtr_material_parameter] = ISNULL(@filter_mtr_material_parameter, [TBL_PM_MATERIAL].[mtr_material_parameter]);

	END
RETURN 0									
