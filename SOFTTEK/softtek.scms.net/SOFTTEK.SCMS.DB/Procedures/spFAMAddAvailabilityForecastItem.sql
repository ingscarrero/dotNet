CREATE PROCEDURE [dbo].[spFAMAddAvailabilityForecastItem]
	@token_id NVARCHAR(50), 
	@new_avlfrcitm_fixed_asset_Fk BIGINT, 
	@new_avlfrcitm_stock INT, 
	@new_avlfrcitm_status NVARCHAR(100), 
	@new_avlfrcitm_from DATETIME, 
	@new_avlfrcitm_to DATETIME, 
	@new_avlfrcitm_availability_forecast_Fk BIGINT
AS
	DECLARE @v_token_user nvarchar(20);
	DECLARE @v_token_employee_id int;
	DECLARE @v_AVAILABILITY_FORECAST_ITEM table (
		v_avlfrcitm_id_Pk BIGINT, 
		v_avlfrcitm_fixed_asset_Fk BIGINT, 
		v_avlfrcitm_stock INT, 
		v_avlfrcitm_status NVARCHAR(100), 
		v_avlfrcitm_from DATETIME, 
		v_avlfrcitm_to DATETIME, 
		v_avlfrcitm_availability_forecast_Fk BIGINT
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
			INSERT INTO [TBL_FAM_AVAILABILITY_FORECAST_ITEM] (
				[TBL_FAM_AVAILABILITY_FORECAST_ITEM].[avlfrcitm_fixed_asset_Fk], 
				[TBL_FAM_AVAILABILITY_FORECAST_ITEM].[avlfrcitm_stock], 
				[TBL_FAM_AVAILABILITY_FORECAST_ITEM].[avlfrcitm_status], 
				[TBL_FAM_AVAILABILITY_FORECAST_ITEM].[avlfrcitm_from], 
				[TBL_FAM_AVAILABILITY_FORECAST_ITEM].[avlfrcitm_to], 
				[TBL_FAM_AVAILABILITY_FORECAST_ITEM].[avlfrcitm_availability_forecast_Fk])
			OUTPUT 
				inserted.[avlfrcitm_id_Pk], 
				inserted.[avlfrcitm_fixed_asset_Fk], 
				inserted.[avlfrcitm_stock], 
				inserted.[avlfrcitm_status], 
				inserted.[avlfrcitm_from], 
				inserted.[avlfrcitm_to], 
				inserted.[avlfrcitm_availability_forecast_Fk]
			INTO @v_AVAILABILITY_FORECAST_ITEM
			VALUES (
				@new_avlfrcitm_fixed_asset_Fk, 
				@new_avlfrcitm_stock, 
				@new_avlfrcitm_status, 
				@new_avlfrcitm_from, 
				@new_avlfrcitm_to, 
				@new_avlfrcitm_availability_forecast_Fk);

			SELECT 
				v_avlfrcitm_id_Pk, 
				v_avlfrcitm_fixed_asset_Fk, 
				v_avlfrcitm_stock, 
				v_avlfrcitm_status, 
				v_avlfrcitm_from, 
				v_avlfrcitm_to, 
				v_avlfrcitm_availability_forecast_Fk
			FROM @v_AVAILABILITY_FORECAST_ITEM;
	END
RETURN 0									
