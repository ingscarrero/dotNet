CREATE PROCEDURE [dbo].[spPMGetTechnicalObject]
	@token_id NVARCHAR(50), 
	@filter_tchobj_id_Pk BIGINT = NULL, 
	@filter_tchobj_name NVARCHAR(100) = NULL, 
	@filter_tchobj_type_object NVARCHAR(10) = NULL,
	@filter_tchobj_external_identifier NVARCHAR(500) = NULL, 
	@filter_tchobj_description nvarchar(500) = NULL, 
	@filter_tchobj_placement nvarchar(500) = NULL, 
	@filter_tchobj_plannification_center nvarchar(500) = NULL, 
	@filter_tchobj_area nvarchar(500) = NULL, 
	@filter_tchobj_cost_center nvarchar(500) = NULL
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
				[TBL_PM_TECHNICALOBJECT].[tchobj_id_Pk], 
				[TBL_PM_TECHNICALOBJECT].[tchobj_name], 
				[TBL_PM_TECHNICALOBJECT].[tchobj_type_object],
				[TBL_PM_TECHNICALOBJECT].[tchobj_external_identifier], 
				[TBL_PM_TECHNICALOBJECT].[tchobj_description], 
				[TBL_PM_TECHNICALOBJECT].[tchobj_placement], 
				[TBL_PM_TECHNICALOBJECT].[tchobj_plannification_center], 
				[TBL_PM_TECHNICALOBJECT].[tchobj_area], 
				[TBL_PM_TECHNICALOBJECT].[tchobj_cost_center]
			FROM [TBL_PM_TECHNICALOBJECT]
			WHERE 
				[TBL_PM_TECHNICALOBJECT].[tchobj_id_Pk] = ISNULL(@filter_tchobj_id_Pk, [TBL_PM_TECHNICALOBJECT].[tchobj_id_Pk])
				AND [TBL_PM_TECHNICALOBJECT].[tchobj_name] = ISNULL(@filter_tchobj_name, [TBL_PM_TECHNICALOBJECT].[tchobj_name])
				AND [TBL_PM_TECHNICALOBJECT].[tchobj_type_object] = ISNULL(@filter_tchobj_type_object, [TBL_PM_TECHNICALOBJECT].[tchobj_type_object])
				AND [TBL_PM_TECHNICALOBJECT].[tchobj_external_identifier] = ISNULL(@filter_tchobj_external_identifier, [TBL_PM_TECHNICALOBJECT].[tchobj_external_identifier])
				AND [TBL_PM_TECHNICALOBJECT].[tchobj_description] = ISNULL(@filter_tchobj_description, [TBL_PM_TECHNICALOBJECT].[tchobj_description])
				AND [TBL_PM_TECHNICALOBJECT].[tchobj_placement] = ISNULL(@filter_tchobj_placement, [TBL_PM_TECHNICALOBJECT].[tchobj_placement])
				AND [TBL_PM_TECHNICALOBJECT].[tchobj_plannification_center] = ISNULL(@filter_tchobj_plannification_center, [TBL_PM_TECHNICALOBJECT].[tchobj_plannification_center])
				AND [TBL_PM_TECHNICALOBJECT].[tchobj_area] = ISNULL(@filter_tchobj_area, [TBL_PM_TECHNICALOBJECT].[tchobj_area])
				AND [TBL_PM_TECHNICALOBJECT].[tchobj_cost_center] = ISNULL(@filter_tchobj_cost_center, [TBL_PM_TECHNICALOBJECT].[tchobj_cost_center]);

	END
RETURN 0									
