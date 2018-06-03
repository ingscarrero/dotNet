CREATE PROCEDURE [dbo].[spFAMGetInventoryItem]
	@token_id NVARCHAR(50), 
	@filter_phyinvtknitm_id_Pk BIGINT = NULL, 
	@filter_phyinvtknitm_fixed_asset_Fk  BIGINT = NULL, 
	@filter_phyinvtknitm_verified_at DATETIME = NULL, 
	@filter_phyinvtknitm_fixed_asset_state NVARCHAR(10) = NULL, 
	@filter_phyinvtknitm_comments NVARCHAR(500)  = NULL, 
	@filter_phyinvtknitm_responsible_Fk INT = NULL, 
	@filter_phyinvtknitm_physical_inventory_taking_Fk BIGINT = NULL
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
				[TBL_FAM_INVENTORY_ITEM].[phyinvtknitm_id_Pk], 
				[TBL_FAM_FIXEDASSET].[fxdast_id_Pk], 
				[TBL_FAM_INVENTORY_ITEM].[phyinvtknitm_verified_at], 
				[TBL_FAM_INVENTORY_ITEM].[phyinvtknitm_fixed_asset_state], 
				[TBL_FAM_INVENTORY_ITEM].[phyinvtknitm_comments], 
				[TBL_SRA_PERSON].[id], 
				[TBL_FAM_INVENTORY_TAKING].[phyinvtkn_id_Pk]
			FROM [TBL_FAM_INVENTORY_ITEM]
				INNER JOIN [TBL_FAM_FIXEDASSET] ON [TBL_FAM_FIXEDASSET].[fxdast_id_Pk] = [TBL_FAM_INVENTORY_ITEM].[phyinvtknitm_fixed_asset_Fk]
				INNER JOIN [TBL_SRA_PERSON] ON [TBL_SRA_PERSON].[id] = [TBL_FAM_INVENTORY_ITEM].[phyinvtknitm_responsible_Fk]
				INNER JOIN [TBL_FAM_INVENTORY_TAKING] ON [TBL_FAM_INVENTORY_TAKING].[phyinvtkn_id_Pk] = [TBL_FAM_INVENTORY_ITEM].[phyinvtknitm_physical_inventory_taking_Fk]
			WHERE 
				[TBL_FAM_INVENTORY_ITEM].[phyinvtknitm_id_Pk] = ISNULL(@filter_phyinvtknitm_id_Pk, [TBL_FAM_INVENTORY_ITEM].[phyinvtknitm_id_Pk])
				AND [TBL_FAM_INVENTORY_ITEM].[phyinvtknitm_fixed_asset_Fk] = ISNULL(@filter_phyinvtknitm_fixed_asset_Fk , [TBL_FAM_INVENTORY_ITEM].[phyinvtknitm_fixed_asset_Fk])
				AND [TBL_FAM_INVENTORY_ITEM].[phyinvtknitm_verified_at] = ISNULL(@filter_phyinvtknitm_verified_at, [TBL_FAM_INVENTORY_ITEM].[phyinvtknitm_verified_at])
				AND [TBL_FAM_INVENTORY_ITEM].[phyinvtknitm_fixed_asset_state] = ISNULL(@filter_phyinvtknitm_fixed_asset_state, [TBL_FAM_INVENTORY_ITEM].[phyinvtknitm_fixed_asset_state])
				AND [TBL_FAM_INVENTORY_ITEM].[phyinvtknitm_comments] = ISNULL(@filter_phyinvtknitm_comments, [TBL_FAM_INVENTORY_ITEM].[phyinvtknitm_comments])
				AND [TBL_FAM_INVENTORY_ITEM].[phyinvtknitm_responsible_Fk] = ISNULL(@filter_phyinvtknitm_responsible_Fk, [TBL_FAM_INVENTORY_ITEM].[phyinvtknitm_responsible_Fk])
				AND [TBL_FAM_INVENTORY_ITEM].[phyinvtknitm_physical_inventory_taking_Fk] = ISNULL(@filter_phyinvtknitm_physical_inventory_taking_Fk, [TBL_FAM_INVENTORY_ITEM].[phyinvtknitm_physical_inventory_taking_Fk]);

	END
RETURN 0									

