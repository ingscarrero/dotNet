CREATE TABLE [dbo].[TBL_SCMS_USER]
(
	[user_id] NVARCHAR(20) NOT NULL PRIMARY KEY, 
    [user_login] NVARCHAR(50) NOT NULL, 
    [user_password] NVARCHAR(100) NOT NULL, 
    [user_is_active] BIT NOT NULL DEFAULT 1, 
    [user_created] DATETIME NOT NULL DEFAULT GETDATE(), 
    [user_created_by] NVARCHAR(20) NOT NULL, 
    [user_modified] DATETIME NULL, 
    [user_modified_by] NVARCHAR(20) NULL, 
    [user_status] SMALLINT NOT NULL DEFAULT 1, 
    CONSTRAINT [FK_TBL_SCMS_USER_TBL_SCMS_USER] FOREIGN KEY (user_created_by) REFERENCES [TBL_SCMS_USER]([user_id]), 
    CONSTRAINT [FK_TBL_SCMS_USER_TBL_SCMS-USER_2] FOREIGN KEY (user_modified_by) REFERENCES [TBL_SCMS_USER]([user_id])
)
