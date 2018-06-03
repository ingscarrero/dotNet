CREATE TABLE [dbo].[TBL_FAM_INVENTORY_ITEM]
(
	[phyinvtknitm_id_Pk] BIGINT NOT NULL PRIMARY KEY, 
    [phyinvtknitm_fixed_asset_Fk] BIGINT NOT NULL, 
    [phyinvtknitm_verified_at] DATETIME NOT NULL, 
    [phyinvtknitm_fixed_asset_state] NVARCHAR(10) NOT NULL, 
    [phyinvtknitm_comments] NVARCHAR(500) NULL, 
    [phyinvtknitm_responsible_Fk] INT NOT NULL, 
    [phyinvtknitm_physical_inventory_taking_Fk] BIGINT NOT NULL, 
    CONSTRAINT [FK_TBL_FAM_INVENTORY_ITEM_TBL_SRA_PERSON_RES] FOREIGN KEY ([phyinvtknitm_responsible_Fk]) REFERENCES [TBL_SRA_PERSON]([id]), 
    CONSTRAINT [FK_TBL_FAM_INVENTORY_ITEM_TBL_FAM_FIXEDASSET] FOREIGN KEY ([phyinvtknitm_fixed_asset_Fk]) REFERENCES [TBL_FAM_FIXEDASSET]([fxdast_id_Pk]), 
    CONSTRAINT [FK_TBL_FAM_INVENTORY_ITEM_TBL_FAM_INVENTORY_TAKING] FOREIGN KEY ([phyinvtknitm_physical_inventory_taking_Fk]) REFERENCES [TBL_FAM_INVENTORY_TAKING]([phyinvtkn_id_Pk])
)
