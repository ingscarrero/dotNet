CREATE PROCEDURE [dbo].[spFAMGetFixedAssetById]
	@token_id NVARCHAR(50), 
	@filter_fxdast_id_Pk BIGINT = NULL, 
	@filter_fxdast_image_url NVARCHAR(500) = NULL, 
	@filter_fxdast_serial_number NVARCHAR(500) = NULL, 
	@filter_fxdast_external_identifier NVARCHAR(500) = NULL, 
	@filter_fxdast_description NVARCHAR(MAX) = NULL, 
	@filter_fxdast_placement NVARCHAR(MAX) = NULL, 
	@filter_fxdast_plannificator_center NVARCHAR(MAX) = NULL, 
	@filter_fxdast_area NVARCHAR(MAX) = NULL, 
	@filter_fxdast_cost_center NVARCHAR(MAX) = NULL, 
	@filter_fxdast_activity NVARCHAR(MAX) = NULL
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
				[TBL_FAM_FIXEDASSET].[fxdast_id_Pk], 
				[TBL_FAM_FIXEDASSET].[fxdast_image_url], 
				[TBL_FAM_FIXEDASSET].[fxdast_serial_number], 
				[TBL_FAM_FIXEDASSET].[fxdast_external_identifier], 
				[TBL_FAM_FIXEDASSET].[fxdast_description], 
				[TBL_FAM_FIXEDASSET].[fxdast_placement], 
				[TBL_FAM_FIXEDASSET].[fxdast_plannificator_center], 
				[TBL_FAM_FIXEDASSET].[fxdast_area], 
				[TBL_FAM_FIXEDASSET].[fxdast_cost_center], 
				[TBL_FAM_FIXEDASSET].[fxdast_activity]
			FROM [TBL_FAM_FIXEDASSET]
			WHERE 
				[TBL_FAM_FIXEDASSET].[fxdast_id_Pk] = ISNULL(@filter_fxdast_id_Pk, [TBL_FAM_FIXEDASSET].[fxdast_id_Pk])
				AND [TBL_FAM_FIXEDASSET].[fxdast_image_url] = ISNULL(@filter_fxdast_image_url, [TBL_FAM_FIXEDASSET].[fxdast_image_url])
				AND [TBL_FAM_FIXEDASSET].[fxdast_serial_number] = ISNULL(@filter_fxdast_serial_number, [TBL_FAM_FIXEDASSET].[fxdast_serial_number])
				AND [TBL_FAM_FIXEDASSET].[fxdast_external_identifier] = ISNULL(@filter_fxdast_external_identifier, [TBL_FAM_FIXEDASSET].[fxdast_external_identifier])
				AND [TBL_FAM_FIXEDASSET].[fxdast_description] = ISNULL(@filter_fxdast_description, [TBL_FAM_FIXEDASSET].[fxdast_description])
				AND [TBL_FAM_FIXEDASSET].[fxdast_placement] = ISNULL(@filter_fxdast_placement, [TBL_FAM_FIXEDASSET].[fxdast_placement])
				AND [TBL_FAM_FIXEDASSET].[fxdast_plannificator_center] = ISNULL(@filter_fxdast_plannificator_center, [TBL_FAM_FIXEDASSET].[fxdast_plannificator_center])
				AND [TBL_FAM_FIXEDASSET].[fxdast_area] = ISNULL(@filter_fxdast_area, [TBL_FAM_FIXEDASSET].[fxdast_area])
				AND [TBL_FAM_FIXEDASSET].[fxdast_cost_center] = ISNULL(@filter_fxdast_cost_center, [TBL_FAM_FIXEDASSET].[fxdast_cost_center])
				AND [TBL_FAM_FIXEDASSET].[fxdast_activity] = ISNULL(@filter_fxdast_activity, [TBL_FAM_FIXEDASSET].[fxdast_activity]);

	END
RETURN 0									
