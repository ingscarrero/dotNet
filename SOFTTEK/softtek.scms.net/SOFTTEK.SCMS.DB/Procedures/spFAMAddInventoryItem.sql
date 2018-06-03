CREATE PROCEDURE [dbo].[spFAMAddInventoryItem]
	@token_id NVARCHAR(50), 
	@new_phyinvtknitm_fixed_asset_Fk  BIGINT, 
	@new_phyinvtknitm_verified_at DATETIME, 
	@new_phyinvtknitm_fixed_asset_state NVARCHAR(10), 
	@new_phyinvtknitm_comments NVARCHAR(500) , 
	@new_phyinvtknitm_responsible_Fk INT, 
	@new_phyinvtknitm_physical_inventory_taking_Fk BIGINT
AS
	DECLARE @v_token_user nvarchar(20);
	DECLARE @v_token_employee_id int;
	DECLARE @v_INVENTORY_ITEM table (
		v_inv_item_id BIGINT, 
		v_phyinvtknitm_fixed_asset_Fk  BIGINT, 
		v_phyinvtknitm_verified_at DATETIME, 
		v_phyinvtknitm_fixed_asset_state NVARCHAR(10), 
		v_phyinvtknitm_comments NVARCHAR(500) , 
		v_phyinvtknitm_responsible_Fk INT, 
		v_phyinvtknitm_physical_inventory_taking_Fk BIGINT
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
			INSERT INTO [TBL_FAM_INVENTORY_ITEM] (
				[TBL_FAM_INVENTORY_ITEM].[phyinvtknitm_fixed_asset_Fk], 
				[TBL_FAM_INVENTORY_ITEM].[phyinvtknitm_verified_at], 
				[TBL_FAM_INVENTORY_ITEM].[phyinvtknitm_fixed_asset_state], 
				[TBL_FAM_INVENTORY_ITEM].[phyinvtknitm_comments], 
				[TBL_FAM_INVENTORY_ITEM].[phyinvtknitm_responsible_Fk], 
				[TBL_FAM_INVENTORY_ITEM].[phyinvtknitm_physical_inventory_taking_Fk])
			OUTPUT 
				inserted.[phyinvtknitm_id_Pk], 
				inserted.[phyinvtknitm_fixed_asset_Fk ], 
				inserted.[phyinvtknitm_verified_at], 
				inserted.[phyinvtknitm_fixed_asset_state], 
				inserted.[phyinvtknitm_comments], 
				inserted.[phyinvtknitm_responsible_Fk], 
				inserted.[phyinvtknitm_physical_inventory_taking_Fk]
			INTO @v_INVENTORY_ITEM
			VALUES (
				@new_phyinvtknitm_fixed_asset_Fk , 
				@new_phyinvtknitm_verified_at, 
				@new_phyinvtknitm_fixed_asset_state, 
				@new_phyinvtknitm_comments, 
				@new_phyinvtknitm_responsible_Fk, 
				@new_phyinvtknitm_physical_inventory_taking_Fk);

			SELECT 
				v_inv_item_id, 
				v_phyinvtknitm_fixed_asset_Fk , 
				v_phyinvtknitm_verified_at, 
				v_phyinvtknitm_fixed_asset_state, 
				v_phyinvtknitm_comments, 
				v_phyinvtknitm_responsible_Fk, 
				v_phyinvtknitm_physical_inventory_taking_Fk
			FROM @v_INVENTORY_ITEM;
	END
RETURN 0									
