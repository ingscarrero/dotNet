CREATE TABLE [dbo].[TBL_FAM_WORK_ORDER]
(
	[wrkord_id_Pk] BIGINT PRIMARY KEY IDENTITY NOT NULL,
	[wrkord_number] NVARCHAR(100) NOT NULL,
	[wrkord_status] NVARCHAR(100) NOT NULL,
	[wrkord_issued_at] DATETIME NOT NULL,
	[wrkord_scheduled_to] DATETIME NOT NULL,
	[wrkord_provider_Fk] BIGINT NOT NULL,
	[wrkord_physical_inventory_taking_Fk] BIGINT NOT NULL,
	[wrkord_description] NVARCHAR(MAX) NULL,
	CONSTRAINT [FK_TBL_FAM_WORK_ORDER_TBL_FAM_PROVIDER] FOREIGN KEY ([wrkord_provider_Fk]) REFERENCES [TBL_FAM_PROVIDER]([prv_id_Pk]),
	CONSTRAINT [FK_TBL_FAM_WORK_ORDER_TBL_FAM_INVENTORY_TAKING] FOREIGN KEY ([wrkord_physical_inventory_taking_Fk]) REFERENCES [TBL_FAM_INVENTORY_TAKING]([phyinvtkn_id_Pk])
)
