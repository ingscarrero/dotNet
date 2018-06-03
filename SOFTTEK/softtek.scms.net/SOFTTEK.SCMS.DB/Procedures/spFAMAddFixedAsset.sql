CREATE PROCEDURE [dbo].[spFAMAddFixedAsset]
	@token_id NVARCHAR(50), 
	@new_fxdast_image_url NVARCHAR(500), 
	@new_fxdast_serial_number NVARCHAR(500), 
	@new_fxdast_external_identifier NVARCHAR(500), 
	@new_fxdast_description NVARCHAR(MAX), 
	@new_fxdast_placement NVARCHAR(MAX), 
	@new_fxdast_plannificator_center NVARCHAR(MAX), 
	@new_fxdast_area NVARCHAR(MAX), 
	@new_fxdast_cost_center NVARCHAR(MAX), 
	@new_fxdast_activity NVARCHAR(MAX)
AS
	DECLARE @v_token_user nvarchar(20);
	DECLARE @v_token_employee_id int;
	DECLARE @v_FIXEDASSET table (
		v_fxdast_id BIGINT, 
		v_fxdast_image_url NVARCHAR(500), 
		v_fxdast_serial_number NVARCHAR(500), 
		v_fxdast_external_identifier NVARCHAR(500), 
		v_fxdast_description NVARCHAR(MAX), 
		v_fxdast_placement NVARCHAR(MAX), 
		v_fxdast_plannificator_center NVARCHAR(MAX), 
		v_fxdast_area NVARCHAR(MAX), 
		v_fxdast_cost_center NVARCHAR(MAX), 
		v_fxdast_activity NVARCHAR(MAX)
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
			INSERT INTO [TBL_FAM_FIXEDASSET] (
				[TBL_FAM_FIXEDASSET].[fxdast_image_url], 
				[TBL_FAM_FIXEDASSET].[fxdast_serial_number], 
				[TBL_FAM_FIXEDASSET].[fxdast_external_identifier], 
				[TBL_FAM_FIXEDASSET].[fxdast_description], 
				[TBL_FAM_FIXEDASSET].[fxdast_placement], 
				[TBL_FAM_FIXEDASSET].[fxdast_plannificator_center], 
				[TBL_FAM_FIXEDASSET].[fxdast_area], 
				[TBL_FAM_FIXEDASSET].[fxdast_cost_center], 
				[TBL_FAM_FIXEDASSET].[fxdast_activity])
			OUTPUT 
				inserted.[fxdast_id_Pk], 
				inserted.[fxdast_image_url], 
				inserted.[fxdast_serial_number], 
				inserted.[fxdast_external_identifier], 
				inserted.[fxdast_description], 
				inserted.[fxdast_placement], 
				inserted.[fxdast_plannificator_center], 
				inserted.[fxdast_area], 
				inserted.[fxdast_cost_center], 
				inserted.[fxdast_activity]
			INTO @v_FIXEDASSET
			VALUES (
				@new_fxdast_image_url, 
				@new_fxdast_serial_number, 
				@new_fxdast_external_identifier, 
				@new_fxdast_description, 
				@new_fxdast_placement, 
				@new_fxdast_plannificator_center, 
				@new_fxdast_area, 
				@new_fxdast_cost_center, 
				@new_fxdast_activity);

			SELECT 
				v_fxdast_id, 
				v_fxdast_image_url, 
				v_fxdast_serial_number, 
				v_fxdast_external_identifier, 
				v_fxdast_description, 
				v_fxdast_placement, 
				v_fxdast_plannificator_center, 
				v_fxdast_area, 
				v_fxdast_cost_center, 
				v_fxdast_activity
			FROM @v_FIXEDASSET;
	END
RETURN 0									
