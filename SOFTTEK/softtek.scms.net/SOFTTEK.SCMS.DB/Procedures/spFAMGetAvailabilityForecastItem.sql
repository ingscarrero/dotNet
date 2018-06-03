CREATE PROCEDURE [dbo].[spFAMGetAvailabilityForecastItem]
	@token_id NVARCHAR(50), 
	@filter_avlfrcitm_id_Pk BIGINT = NULL, 
	@filter_avlfrcitm_fixed_asset_Fk BIGINT = NULL, 
	@filter_avlfrcitm_stock INT = NULL, 
	@filter_avlfrcitm_status NVARCHAR(100) = NULL, 
	@filter_avlfrcitm_from DATETIME = NULL, 
	@filter_avlfrcitm_to DATETIME = NULL, 
	@filter_avlfrcitm_availability_forecast_Fk BIGINT = NULL
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
				[TBL_FAM_AVAILABILITY_FORECAST_ITEM].[avlfrcitm_id_Pk], 
				[TBL_FAM_FIXEDASSET].[fxdast_id_Pk], 
				[TBL_FAM_AVAILABILITY_FORECAST_ITEM].[avlfrcitm_stock], 
				[TBL_FAM_AVAILABILITY_FORECAST_ITEM].[avlfrcitm_status], 
				[TBL_FAM_AVAILABILITY_FORECAST_ITEM].[avlfrcitm_from], 
				[TBL_FAM_AVAILABILITY_FORECAST_ITEM].[avlfrcitm_to], 
				[TBL_FAM_AVAILABILITY_FORECAST].[avlfrc_id_Pk]
			FROM [TBL_FAM_AVAILABILITY_FORECAST_ITEM]
				INNER JOIN [TBL_FAM_FIXEDASSET] ON [TBL_FAM_FIXEDASSET].[fxdast_id_Pk] = [TBL_FAM_AVAILABILITY_FORECAST_ITEM].[avlfrcitm_fixed_asset_Fk]
				INNER JOIN [TBL_FAM_AVAILABILITY_FORECAST] ON [TBL_FAM_AVAILABILITY_FORECAST].[avlfrc_id_Pk] = [TBL_FAM_AVAILABILITY_FORECAST_ITEM].[avlfrcitm_availability_forecast_Fk]
			WHERE 
				[TBL_FAM_AVAILABILITY_FORECAST_ITEM].[avlfrcitm_id_Pk] = ISNULL(@filter_avlfrcitm_id_Pk, [TBL_FAM_AVAILABILITY_FORECAST_ITEM].[avlfrcitm_id_Pk])
				AND [TBL_FAM_AVAILABILITY_FORECAST_ITEM].[avlfrcitm_fixed_asset_Fk] = ISNULL(@filter_avlfrcitm_fixed_asset_Fk, [TBL_FAM_AVAILABILITY_FORECAST_ITEM].[avlfrcitm_fixed_asset_Fk])
				AND [TBL_FAM_AVAILABILITY_FORECAST_ITEM].[avlfrcitm_stock] = ISNULL(@filter_avlfrcitm_stock, [TBL_FAM_AVAILABILITY_FORECAST_ITEM].[avlfrcitm_stock])
				AND [TBL_FAM_AVAILABILITY_FORECAST_ITEM].[avlfrcitm_status] = ISNULL(@filter_avlfrcitm_status, [TBL_FAM_AVAILABILITY_FORECAST_ITEM].[avlfrcitm_status])
				AND [TBL_FAM_AVAILABILITY_FORECAST_ITEM].[avlfrcitm_from] = ISNULL(@filter_avlfrcitm_from, [TBL_FAM_AVAILABILITY_FORECAST_ITEM].[avlfrcitm_from])
				AND [TBL_FAM_AVAILABILITY_FORECAST_ITEM].[avlfrcitm_to] = ISNULL(@filter_avlfrcitm_to, [TBL_FAM_AVAILABILITY_FORECAST_ITEM].[avlfrcitm_to])
				AND [TBL_FAM_AVAILABILITY_FORECAST_ITEM].[avlfrcitm_availability_forecast_Fk] = ISNULL(@filter_avlfrcitm_availability_forecast_Fk, [TBL_FAM_AVAILABILITY_FORECAST_ITEM].[avlfrcitm_availability_forecast_Fk]);

	END
RETURN 0									
