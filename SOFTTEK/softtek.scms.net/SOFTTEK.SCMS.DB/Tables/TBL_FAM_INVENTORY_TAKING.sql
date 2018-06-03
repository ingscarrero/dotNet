CREATE TABLE [dbo].[TBL_FAM_INVENTORY_TAKING]
(
	[phyinvtkn_id_Pk] BIGINT IDENTITY NOT NULL PRIMARY KEY, 
    [phyinvtkn_accountable_Fk] INT NOT NULL, 
    [phyinvtkn_responsible_Fk] INT NOT NULL, 
    [phyinvtkn_informed_Fk] INT NOT NULL, 
    [phyinvtkn_work_order_Fk] BIGINT NOT NULL, 
    [phyinvtkn_location] NVARCHAR(50) NOT NULL, 
    [phyinvtkn_status] NVARCHAR(10) NOT NULL, 
    [phyinvtkn_comments] NVARCHAR(500) NOT NULL,
	[phyinvtkn_registered_at] DATETIME NOT NULL DEFAULT GETDATE(), 
    [phyinvtkn_created_by] NVARCHAR(20) NULL, 
    [phyinvtkn_updated_at] DATETIME NULL, 
    [phyinvtkn_modified_by] NVARCHAR(20) NULL, 
    CONSTRAINT [FK_TBL_FAM_INVENTORY_TAKING_TBL_SRA_EMPLOYEE_ACC] FOREIGN KEY ([phyinvtkn_accountable_Fk]) REFERENCES [TBL_SRA_EMPLOYEE]([id]),
	CONSTRAINT [FK_TBL_FAM_INVENTORY_TAKING_TBL_SRA_EMPLOYEE_RES] FOREIGN KEY ([phyinvtkn_responsible_Fk]) REFERENCES [TBL_SRA_EMPLOYEE]([id]),
	CONSTRAINT [FK_TBL_FAM_INVENTORY_TAKING_TBL_SRA_EMPLOYEE_INF] FOREIGN KEY ([phyinvtkn_informed_Fk]) REFERENCES [TBL_SRA_EMPLOYEE]([id]),
	CONSTRAINT [FK_TBL_FAM_INVENTORY_TAKING_TBL_FAM_WORK_ORDER_ID] FOREIGN KEY ([phyinvtkn_work_order_Fk]) REFERENCES [TBL_FAM_WORK_ORDER]([wrkord_id_Pk])
)
