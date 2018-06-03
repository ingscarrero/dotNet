CREATE PROCEDURE [dbo].[spFAMGetInventory]
	@fa_inventory_number NVARCHAR(20), 
    @fa_number NVARCHAR(20), 
    @fa_subnumber NVARCHAR(20), 
    @fa_denomination NVARCHAR(50), 
    @fa_class NVARCHAR(50), 
    @fa_type NVARCHAR(50), 
    @fa_sn NVARCHAR(20), 
    @fa_removal_mark BIT, 
    @fa_entry_locked BIT, 
    @fa_cost_center INT, 
    @fa_profit_center INT, 
    @fa_usage NVARCHAR(50), 
    @fa_supernumber NVARCHAR(50), 
    @fa_capitalization_at DATETIME, 
    @fa_decapitalization_at DATETIME, 
    @fa_last_disposal_at DATETIME, 
    @fa_entry_at DATETIME, 
    @fa_created_at DATETIME, 
    @fa_created_by NVARCHAR(20), 
    @fa_modified_at DATETIME, 
    @fa_modified_by NVARCHAR(20), 
    @fa_state NVARCHAR(20)
AS
	SELECT 
		[TBL_FAM_INVENTORY].[fa_inventory_number],
	    [TBL_FAM_INVENTORY].[fa_number],
	    [TBL_FAM_INVENTORY].[fa_subnumber],
	    [TBL_FAM_INVENTORY].[fa_denomination],
	    [TBL_FAM_INVENTORY].[fa_class],
	    [TBL_FAM_INVENTORY].[fa_type],
	    [TBL_FAM_INVENTORY].[fa_sn],
	    [TBL_FAM_INVENTORY].[fa_removal_mark],
	    [TBL_FAM_INVENTORY].[fa_entry_locked],
	    [TBL_FAM_INVENTORY].[fa_cost_center],
	    [TBL_FAM_INVENTORY].[fa_profit_center],
	    [TBL_FAM_INVENTORY].[fa_usage],
	    [TBL_FAM_INVENTORY].[fa_supernumber],
	    [TBL_FAM_INVENTORY].[fa_capitalization_at],
	    [TBL_FAM_INVENTORY].[fa_decapitalization_at],
	    [TBL_FAM_INVENTORY].[fa_last_disposal_at],
	    [TBL_FAM_INVENTORY].[fa_entry_at],
	    [TBL_FAM_INVENTORY].[fa_created_at],
	    [TBL_FAM_INVENTORY].[fa_created_by],
	    [TBL_FAM_INVENTORY].[fa_modified_at],
	    [TBL_FAM_INVENTORY].[fa_modified_by],
	    [TBL_FAM_INVENTORY].[fa_state]
	FROM
		[TBL_FAM_INVENTORY]
	WHERE
		[TBL_FAM_INVENTORY].[fa_inventory_number] =  COALESCE(NULLIF(@fa_inventory_number, ''), [TBL_FAM_INVENTORY].[fa_inventory_number]) AND
	    [TBL_FAM_INVENTORY].[fa_number] =  COALESCE(NULLIF(@fa_number, ''), [TBL_FAM_INVENTORY].[fa_number]) AND
	    [TBL_FAM_INVENTORY].[fa_subnumber] =  COALESCE(NULLIF(@fa_subnumber, ''), [TBL_FAM_INVENTORY].[fa_subnumber]) AND
	    [TBL_FAM_INVENTORY].[fa_denomination] =  COALESCE(NULLIF(@fa_denomination, ''), [TBL_FAM_INVENTORY].[fa_denomination]) AND
	    [TBL_FAM_INVENTORY].[fa_class] =  COALESCE(NULLIF(@fa_class, ''), [TBL_FAM_INVENTORY].[fa_class]) AND
	    [TBL_FAM_INVENTORY].[fa_type] =  COALESCE(NULLIF(@fa_type, ''), [TBL_FAM_INVENTORY].[fa_type]) AND
	    [TBL_FAM_INVENTORY].[fa_sn] =  COALESCE(NULLIF(@fa_sn, ''), [TBL_FAM_INVENTORY].[fa_sn]) AND
	    [TBL_FAM_INVENTORY].[fa_removal_mark] =  COALESCE(NULLIF(@fa_removal_mark, ''), [TBL_FAM_INVENTORY].[fa_removal_mark]) AND
	    [TBL_FAM_INVENTORY].[fa_entry_locked] =  COALESCE(NULLIF(@fa_entry_locked, ''), [TBL_FAM_INVENTORY].[fa_entry_locked]) AND
	    [TBL_FAM_INVENTORY].[fa_cost_center] =  COALESCE(NULLIF(@fa_cost_center, ''), [TBL_FAM_INVENTORY].[fa_cost_center]) AND
	    [TBL_FAM_INVENTORY].[fa_profit_center] =  COALESCE(NULLIF(@fa_profit_center, ''), [TBL_FAM_INVENTORY].[fa_profit_center]) AND
	    [TBL_FAM_INVENTORY].[fa_usage] =  COALESCE(NULLIF(@fa_usage, ''), [TBL_FAM_INVENTORY].[fa_usage]) AND
	    [TBL_FAM_INVENTORY].[fa_supernumber] =  COALESCE(NULLIF(@fa_supernumber, ''), [TBL_FAM_INVENTORY].[fa_supernumber]) AND
	    [TBL_FAM_INVENTORY].[fa_capitalization_at] =  COALESCE(NULLIF(@fa_capitalization_at, ''), [TBL_FAM_INVENTORY].[fa_capitalization_at]) AND
	    [TBL_FAM_INVENTORY].[fa_decapitalization_at] =  COALESCE(NULLIF(@fa_decapitalization_at, ''), [TBL_FAM_INVENTORY].[fa_decapitalization_at]) AND
	    [TBL_FAM_INVENTORY].[fa_last_disposal_at] =  COALESCE(NULLIF(@fa_last_disposal_at, ''), [TBL_FAM_INVENTORY].[fa_last_disposal_at]) AND
	    [TBL_FAM_INVENTORY].[fa_entry_at] =  COALESCE(NULLIF(@fa_entry_at, ''), [TBL_FAM_INVENTORY].[fa_entry_at]) AND
	    [TBL_FAM_INVENTORY].[fa_created_at] =  COALESCE(NULLIF(@fa_created_at, ''), [TBL_FAM_INVENTORY].[fa_created_at]) AND
	    [TBL_FAM_INVENTORY].[fa_created_by] =  COALESCE(NULLIF(@fa_created_by, ''), [TBL_FAM_INVENTORY].[fa_created_by]) AND
	    [TBL_FAM_INVENTORY].[fa_modified_at] =  COALESCE(NULLIF(@fa_modified_at, ''), [TBL_FAM_INVENTORY].[fa_modified_at]) AND
	    [TBL_FAM_INVENTORY].[fa_modified_by] =  COALESCE(NULLIF(@fa_modified_by, ''), [TBL_FAM_INVENTORY].[fa_modified_by]) AND
	    [TBL_FAM_INVENTORY].[fa_state] =  COALESCE(NULLIF(@fa_state, ''), [TBL_FAM_INVENTORY].[fa_state]);
RETURN 0
