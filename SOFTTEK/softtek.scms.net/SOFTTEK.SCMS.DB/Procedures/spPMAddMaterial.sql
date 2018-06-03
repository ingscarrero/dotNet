CREATE PROCEDURE [dbo].[spPMAddMaterial]
	@token_id NVARCHAR(50), 
	@new_mtr_external_identifier NVARCHAR(500),
	@new_mtr_name NVARCHAR(300), 
	@new_mtr_description NVARCHAR(300), 
	@new_mtr_class NVARCHAR(MAX), 
	@new_mtr_stock nvarchar(100),
	@new_mtr_task_Fk BIGINT ,
	@new_mtr_material_parameter BIGINT = NULL,
	@new_mtr_observations BIGINT = NULL
AS
	DECLARE @v_token_user nvarchar(20);
	DECLARE @v_token_employee_id int;
	DECLARE @v_ATERIAL table (
		v_mtr_id_Pk bigint, 
		v_mtr_external_identifier NVARCHAR(500),
		v_mtr_name NVARCHAR(300), 
		v_mtr_description NVARCHAR(300), 
		v_mtr_class NVARCHAR(MAX), 
		v_mtr_stock nvarchar(100),
		v_mtr_task_Fk BIGINT,
		v_mtr_material_parameter BIGINT,
		v_mtr_observations NVARCHAR(500)
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

		IF (@v_token_employee_id IS NULL)
		Begin
			RAISERROR ('Invalid token', 16, 1);
		End
		ELSE
			Begin
			INSERT INTO [TBL_PM_MATERIAL] (
				[TBL_PM_MATERIAL].[mtr_external_identifier],
				[TBL_PM_MATERIAL].[mtr_name], 
				[TBL_PM_MATERIAL].[mtr_description], 
				[TBL_PM_MATERIAL].[mtr_class], 
				[TBL_PM_MATERIAL].[mtr_stock],
				[TBL_PM_MATERIAL].[mtr_task_Fk],
				[TBL_PM_MATERIAL].[mtr_material_parameter],
				[TBL_PM_MATERIAL].[mtr_observations])
			OUTPUT 
				inserted.[mtr_id_Pk], 
				inserted.mtr_external_identifier,
				inserted.[mtr_name], 
				inserted.[mtr_description], 
				inserted.[mtr_class], 
				inserted.[mtr_stock],
				inserted.[mtr_task_Fk],
				inserted.[mtr_material_parameter],
				inserted.[mtr_observations]
			INTO @v_ATERIAL
			VALUES (
				@new_mtr_external_identifier,
				@new_mtr_name, 
				@new_mtr_description, 
				@new_mtr_class, 
				@new_mtr_stock,
				@new_mtr_task_Fk,
				@new_mtr_material_parameter,
				@new_mtr_description);

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
			End
	END
RETURN 0								
