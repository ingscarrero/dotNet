CREATE TABLE [dbo].[TBL_FAM_RETIREMENT_REQUEST]
(
	[rtr_id_Pk] BIGINT IDENTITY NOT NULL PRIMARY KEY, 
    [rtr_number] VARCHAR(MAX) NOT NULL, 
	[rtr_external_identifier] NVARCHAR(500) NOT NULL,
	[rtr_informed_Fk] INT NOT NULL,
    [rtr_responsible_Fk] INT NOT NULL, 
    [rtr_accountable_Fk] INT NOT NULL, 
	[rtr_novelty_Fk] BIGINT NOT NULL, 
    [rtr_details] NVARCHAR(500) NOT NULL, 
    [rtr_type_Fk] BIGINT NOT NULL, 
    [rtr_topic] NVARCHAR(50) NOT NULL, 
    [rtr_registered_at] DATETIME NOT NULL DEFAULT GETDATE(), 
    [rtr_registered_by] NVARCHAR(20) NOT NULL, 
    [rtr_updated_at] DATETIME NULL, 
    [rtr_updated_by] NVARCHAR(20) NULL, 
    [rtr_status] NVARCHAR(10) NOT NULL, 
    [rtr_comments] NVARCHAR(500) NOT NULL, 
	[rtr_reason] NVARCHAR(500) NOT NULL
	CONSTRAINT [FK_TBL_FAM_RETIREMENT_REQUEST_TBL_SRA_EMPLOYEE_APP] FOREIGN KEY ([rtr_informed_Fk]) REFERENCES [TBL_SRA_EMPLOYEE]([id]), 
    CONSTRAINT [FK_TBL_FAM_RETIREMENT_REQUEST_TBL_SRA_EMPLOYEE_RES] FOREIGN KEY ([rtr_responsible_Fk]) REFERENCES [TBL_SRA_EMPLOYEE]([id]), 
    CONSTRAINT [FK_TBL_FAM_RETIREMENT_REQUEST_TBL_SRA_EMPLOYEE_ACC] FOREIGN KEY ([rtr_accountable_Fk]) REFERENCES [TBL_SRA_EMPLOYEE]([id]), 
    CONSTRAINT [FK_TBL_FAM_RETIREMENT_REQUEST_TBL_FAM_NOVELTY_id] FOREIGN KEY ([rtr_novelty_Fk]) REFERENCES [TBL_FAM_NOVELTY]([nvlrqs_id_Pk])
)
