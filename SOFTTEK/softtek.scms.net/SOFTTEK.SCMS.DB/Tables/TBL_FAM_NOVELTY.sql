CREATE TABLE [dbo].[TBL_FAM_NOVELTY]
(
	[nvlrqs_id_Pk] BIGINT IDENTITY NOT NULL PRIMARY KEY, 
	[nvlrqs_number] VARCHAR(MAX) NOT NULL, 
    [nvlrqs_request_Fk] BIGINT NOT NULL, 
    [nvlrqs_informed_Fk] INT NOT NULL, 
    [nvlrqs_responsible_Fk] INT NOT NULL, 
    [nvlrqs_accountable_Fk] INT NOT NULL, 
    [nvlrqs_external_identifier] NVARCHAR(50) NOT NULL, 
    [nvlrqs_details] NVARCHAR(500) NOT NULL, 
	[nvlrqs_type_Fk] BIGINT NOT NULL, 
    [nvlrqs_topic] NVARCHAR(50) NOT NULL, 
    [nvlrqs_registered_at] DATETIME NOT NULL DEFAULT GETDATE(), 
    [nvlrqs_created_by] NVARCHAR(20) NOT NULL, 
    [nvlrqs_updated_at] DATETIME NULL, 
    [nvlrqs_modified_by] NVARCHAR(20) NULL, 
    [nvlrqs_status] NVARCHAR(10) NOT NULL, 
    [nvlrqs_comments] NVARCHAR(500) NOT NULL, 
    CONSTRAINT [FK_TBL_FAM_NOVELTY_TBL_FAM_REQUEST_id] FOREIGN KEY ([nvlrqs_request_Fk]) REFERENCES [TBL_FAM_REQUEST]([rqs_id_Pk]), 
    CONSTRAINT [FK_TBL_FAM_NOVELTY_TBL_SRA_EMPLOYEE_APP] FOREIGN KEY ([nvlrqs_informed_Fk]) REFERENCES [TBL_SRA_EMPLOYEE]([id]), 
    CONSTRAINT [FK_TBL_FAM_NOVELTY_TBL_SRA_EMPLOYEE_RES] FOREIGN KEY ([nvlrqs_responsible_Fk]) REFERENCES [TBL_SRA_EMPLOYEE]([id]), 
    CONSTRAINT [FK_TBL_FAM_NOVELTY_TBL_SRA_EMPLOYEE_ACC] FOREIGN KEY ([nvlrqs_accountable_Fk]) REFERENCES [TBL_SRA_EMPLOYEE]([id])
)
