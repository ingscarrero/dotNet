CREATE PROCEDURE [dbo].[spPMAddTechnicalObject]
	@token_id NVARCHAR(50), 
	@new_tchobj_name NVARCHAR(100), 
	@new_tchobj_type_object NVARCHAR(10),
	@new_tchobj_external_identifier NVARCHAR(500), 
	@new_tchobj_description nvarchar(500), 
	@new_tchobj_placement nvarchar(500), 
	@new_tchobj_plannification_center nvarchar(500), 
	@new_tchobj_area nvarchar(500), 
	@new_tchobj_cost_center nvarchar(500)
AS
	DECLARE @v_token_user nvarchar(20);
	DECLARE @v_token_employee_id int;
	DECLARE @v_ECHNICALOBJECT table (
		v_tchobj_id_Pk BIGINT, 
		v_tchobj_name NVARCHAR(100), 
		v_tchobj_type_object NVARCHAR(10),
		v_tchobj_external_identifier NVARCHAR(500), 
		v_tchobj_description nvarchar(500), 
		v_tchobj_placement nvarchar(500), 
		v_tchobj_plannification_center nvarchar(500), 
		v_tchobj_area nvarchar(500), 
		v_tchobj_cost_center nvarchar(500)
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
			INSERT INTO [TBL_PM_TECHNICALOBJECT] (
				[TBL_PM_TECHNICALOBJECT].[tchobj_name], 
				[TBL_PM_TECHNICALOBJECT].[tchobj_type_object],
				[TBL_PM_TECHNICALOBJECT].[tchobj_external_identifier], 
				[TBL_PM_TECHNICALOBJECT].[tchobj_description], 
				[TBL_PM_TECHNICALOBJECT].[tchobj_placement], 
				[TBL_PM_TECHNICALOBJECT].[tchobj_plannification_center], 
				[TBL_PM_TECHNICALOBJECT].[tchobj_area], 
				[TBL_PM_TECHNICALOBJECT].[tchobj_cost_center])
			OUTPUT 
				inserted.[tchobj_id_Pk], 
				inserted.[tchobj_name],
				inserted.[tchobj_type_object], 
				inserted.[tchobj_external_identifier], 
				inserted.[tchobj_description], 
				inserted.[tchobj_placement], 
				inserted.[tchobj_plannification_center], 
				inserted.[tchobj_area], 
				inserted.[tchobj_cost_center]
			INTO @v_ECHNICALOBJECT
			VALUES (
				@new_tchobj_name, 
				@new_tchobj_type_object,
				@new_tchobj_external_identifier, 
				@new_tchobj_description, 
				@new_tchobj_placement, 
				@new_tchobj_plannification_center, 
				@new_tchobj_area, 
				@new_tchobj_cost_center);

			SELECT 
				v_tchobj_id_Pk, 
				v_tchobj_name, 
				v_tchobj_type_object,
				v_tchobj_external_identifier, 
				v_tchobj_description, 
				v_tchobj_placement, 
				v_tchobj_plannification_center, 
				v_tchobj_area, 
				v_tchobj_cost_center
			FROM @v_ECHNICALOBJECT;
	END
RETURN 0									
