CREATE TABLE [dbo].[TBL_SRA_PERMITS_AND_ABSENCES]
(
	[perabs_id] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [perabs_activity_code] INT NOT NULL, 
    [perabs_start_at] DATETIME NOT NULL, 
    [perabs_end_at] DATETIME NOT NULL, 
    [perabs_total_hours] NUMERIC(3, 1) NOT NULL, 
    [perabs_validated_by] NVARCHAR(20) NULL, 
    [perabs_description] NVARCHAR(MAX) NOT NULL, 
    [perabs_created_at] DATETIME NULL DEFAULT GETDATE(), 
    [perabs_created_by] NVARCHAR(20) NOT NULL, 
    [perabs_modified_at] DATETIME NULL, 
    [perabs_modified_by] NVARCHAR(20) NULL, 
    [perabs_validated_comments] NVARCHAR(250) NULL, 
    [perabs_employee] INT NOT NULL, 
    [perabs_validated_at] DATETIME NULL,
	[perabs_Status] NVARCHAR(20) NOT NULL DEFAULT 'R', 
    CONSTRAINT [FK_TBL_SRA_PERMITS_AND_ABSENCES_TBL_SCMS_EMPLOYEE] FOREIGN KEY (perabs_employee) REFERENCES [TBL_SRA_EMPLOYEE](id),
	CONSTRAINT [FK_TBL_SRA_PERMITS_AND_ABSENCES_TBL_SCMS_USER_CREATED] FOREIGN KEY (perabs_created_by) REFERENCES [TBL_SCMS_USER](user_id),
	CONSTRAINT [FK_TBL_SRA_PERMITS_AND_ABSENCES_TBL_SCMS_USER_MODIFIED] FOREIGN KEY (perabs_modified_by) REFERENCES [TBL_SCMS_USER](user_id),
	CONSTRAINT [FK_TBL_SRA_PERMITS_AND_ABSENCES_TBL_SCMS_USER_VALIDATED] FOREIGN KEY (perabs_validated_by) REFERENCES [TBL_SCMS_USER](user_id),
	CONSTRAINT [CK_TBL_SRA_PERMITS_AND_ABSENCES_ACTIVITY_CODE] CHECK (dbo.[fnSRACheckPermitsAndAbsencesCode]( [perabs_activity_code]) = 1)
)
