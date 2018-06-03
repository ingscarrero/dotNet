CREATE TABLE [dbo].[TBL_FAM_REQUEST]
(
	[rqs_id_Pk] BIGINT IDENTITY NOT NULL PRIMARY KEY, 
    [rqs_number] VARCHAR(MAX) NOT NULL, 
	[rqs_informed_Fk] INT NOT NULL,
    [rqs_responsible_Fk] INT NOT NULL, 
    [rqs_accountable_Fk] INT NOT NULL, 
    [rqs_details] NVARCHAR(500) NOT NULL, 
    [rqs_type_Fk] BIGINT NOT NULL, 
    [rqs_topic] NVARCHAR(50) NOT NULL, 
    [rqs_registered_at] DATETIME NOT NULL DEFAULT GETDATE(), 
    [rqs_created_by] NVARCHAR(20) NULL , 
    [rqs_updated_at] DATETIME NULL, 
    [rqs_modified_by] NVARCHAR(20) NULL, 
    [rqs_status] NVARCHAR(10) NOT NULL, 
    [rqs_comments] NVARCHAR(500) NOT NULL, 
    CONSTRAINT [FK_TBL_FAM_REQUEST_TBL_SRA_EMPLOYEE_APP] FOREIGN KEY ([rqs_informed_Fk]) REFERENCES [TBL_SRA_EMPLOYEE]([id]), 
    CONSTRAINT [FK_TBL_FAM_REQUEST_TBL_SRA_EMPLOYEE_RES] FOREIGN KEY ([rqs_responsible_Fk]) REFERENCES [TBL_SRA_EMPLOYEE]([id]), 
    CONSTRAINT [FK_TBL_FAM_REQUEST_TBL_SRA_EMPLOYEE_ACC] FOREIGN KEY ([rqs_accountable_Fk]) REFERENCES [TBL_SRA_EMPLOYEE]([id])
)
