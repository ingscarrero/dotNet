CREATE TABLE [dbo].[TBL_SCMS_COMPANY]
(
	[cmp_id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [cmp_name] INT NOT NULL, 
    [cmp_is_active] BIT NOT NULL DEFAULT 0, 
    [cmp_legal_representative] INT NOT NULL, 
    [cmp_created] DATETIME NOT NULL DEFAULT GETDATE(), 
    [cmp_crested_by] NVARCHAR(20) NOT NULL, 
    [cmp_modified] DATETIME NULL, 
    [cmp_modified_by] NVARCHAR(20) NULL, 
    CONSTRAINT [FK_TBL_SCMS_COMPANY_TBL_SRA_PERSON] FOREIGN KEY ([cmp_legal_representative]) REFERENCES [TBL_SRA_PERSON]([id])

)
