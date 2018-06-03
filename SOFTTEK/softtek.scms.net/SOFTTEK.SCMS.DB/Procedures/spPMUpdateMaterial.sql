CREATE PROCEDURE [dbo].[spPMUpdateMaterial]
	@token_id NVARCHAR(50), 
	@filter_mtr_id_Pk BIGINT = NULL, 
	@filter_mtr_external_identifier NVARCHAR(500) = NULL,
	@filter_mtr_name nvarchar(50) = NULL, 
	@filter_mtr_description nvarchar(500) = NULL, 
	@filter_mtr_class nvarchar(50) = NULL, 
	@filter_mtr_stock nvarchar(100) = NULL, 
	@filter_mtr_task_Fk BIGINT = NULL,
	@filter_mtr_material_parameter BIGINT = NULL,
	@new_mtr_external_identifier NVARCHAR(500) = NULL,
	@new_mtr_name nvarchar(50) = NULL, 
	@new_mtr_description nvarchar(500) = NULL, 
	@new_mtr_class nvarchar(50) = NULL, 
	@new_mtr_stock nvarchar(100) = NULL,
	@new_mtr_task_Fk BIGINT = NULL,
	@new_mtr_material_parameter BIGINT = NULL,
	@new_mtr_observations BIGINT = NULL
AS
	DECLARE @v_token_user nvarchar(20);
	DECLARE @v_token_employee_id int;
	DECLARE @v_ATERIAL table (
		v_mtr_id_Pk BIGINT, 
		v_mtr_external_identifier NVARCHAR(500),
		v_mtr_name nvarchar(50), 
		v_mtr_description nvarchar(500), 
		v_mtr_class nvarchar(50), 
		v_mtr_stock nvarchar(100),
		v_mtr_task_Fk BIGINT,
		v_mtr_material_parameter BIGINT,
		v_mtr_observations NVARCHAR(MAX)
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
			UPDATE [TBL_PM_MATERIAL]
			SET
				[TBL_PM_MATERIAL].[mtr_external_identifier] = ISNULL(@new_mtr_external_identifier, [TBL_PM_MATERIAL].[mtr_external_identifier]),
				[TBL_PM_MATERIAL].[mtr_name] = ISNULL(@new_mtr_name, [TBL_PM_MATERIAL].[mtr_name]), 
				[TBL_PM_MATERIAL].[mtr_description] = ISNULL(@new_mtr_description, [TBL_PM_MATERIAL].[mtr_description]), 
				[TBL_PM_MATERIAL].[mtr_class] = ISNULL(@new_mtr_class, [TBL_PM_MATERIAL].[mtr_class]), 
				[TBL_PM_MATERIAL].[mtr_task_Fk] = ISNULL(@new_mtr_task_Fk, [TBL_PM_MATERIAL].[mtr_task_Fk]),
				[TBL_PM_MATERIAL].[mtr_material_parameter] = ISNULL(@new_mtr_material_parameter, [TBL_PM_MATERIAL].[mtr_material_parameter]),
				[TBL_PM_MATERIAL].[mtr_observations] = ISNULL(@new_mtr_observations, [TBL_PM_MATERIAL].[mtr_observations])
			OUTPUT 
				inserted.[mtr_id_Pk], 
				inserted.[mtr_external_identifier],
				inserted.[mtr_name], 
				inserted.[mtr_description], 
				inserted.[mtr_class], 
				inserted.[mtr_stock],
				inserted.[mtr_task_Fk],
				inserted.[mtr_material_parameter],
				inserted.[mtr_observations]
			INTO @v_ATERIAL
			WHERE 
				[TBL_PM_MATERIAL].[mtr_id_Pk] = ISNULL(@filter_mtr_id_Pk, [TBL_PM_MATERIAL].[mtr_id_Pk])
				AND [TBL_PM_MATERIAL].[mtr_external_identifier] = ISNULL(@filter_mtr_external_identifier, [TBL_PM_MATERIAL].[mtr_external_identifier])
				AND [TBL_PM_MATERIAL].[mtr_name] = ISNULL(@filter_mtr_name, [TBL_PM_MATERIAL].[mtr_name])
				AND [TBL_PM_MATERIAL].[mtr_description] = ISNULL(@filter_mtr_description, [TBL_PM_MATERIAL].[mtr_description])
				AND [TBL_PM_MATERIAL].[mtr_class] = ISNULL(@filter_mtr_class, [TBL_PM_MATERIAL].[mtr_class])
				AND [TBL_PM_MATERIAL].[mtr_stock] = ISNULL(@filter_mtr_stock, [TBL_PM_MATERIAL].[mtr_stock])
				AND [TBL_PM_MATERIAL].[mtr_task_Fk] = ISNULL(@filter_mtr_task_Fk, [TBL_PM_MATERIAL].[mtr_task_Fk])
				AND [TBL_PM_MATERIAL].[mtr_material_parameter] = ISNULL(@filter_mtr_material_parameter, [TBL_PM_MATERIAL].[mtr_material_parameter]);

			SELECT 
				v_mtr_id_Pk, 
				v_mtr_external_identifier,
				v_mtr_name, 
				v_mtr_description, 
				v_mtr_class, 
				v_mtr_stock,
				v_mtr_task_Fk,
				v_mtr_material_parameter,
				v_mtr_observations
			FROM @v_ATERIAL;
	END
RETURN 0									
