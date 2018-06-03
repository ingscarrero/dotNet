CREATE TABLE [dbo].[TBL_SRA_ACTIVITY]
(
	[activity_id] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [activity_project] NVARCHAR(20) NOT NULL, 
    [activity_code] NVARCHAR(10) NOT NULL, 
    [activity_description] NVARCHAR(500) NOT NULL, 
    [activity_effort] NUMERIC(3, 1) NOT NULL, 
    [activity_employee] INT NOT NULL, 
    [activity_status] CHAR NOT NULL , 
    [activity_reported_at] DATETIME NOT NULL DEFAULT GETDATE(), 
    [activity_executed_at] DATETIME NOT NULL, 
    [activity_validated_at] DATETIME NULL, 
    [activity_validated_by] NVARCHAR(20) NULL, 
    [activity_created_at] DATETIME NOT NULL DEFAULT GETDATE(), 
    [activity_created_by] NVARCHAR(20) NOT NULL, 
    [activity_modified_by] NVARCHAR(20) NULL, 
    [activity_modified_at] DATETIME NULL, 
    [activity_jornade_type] NVARCHAR(20) NULL, 
    CONSTRAINT [FK_TBL_SRA_ACTIVITY_TBL_SCMS_EMPLOYEE] FOREIGN KEY (activity_employee) REFERENCES [TBL_SRA_EMPLOYEE](id), 
    CONSTRAINT [CK_TBL_SRA_ACTIVITY_ACTIVITY_CODE] CHECK (dbo.[fnSRACheckActivityCode]([activity_project], [activity_code]) = 1), 
	CONSTRAINT [CK_TBL_SRA_ACTIVITY_JORNADE_TYPE] CHECK (dbo.[fnSRACheckActivityJornadeType]([activity_jornade_type]) = 1) 
)
