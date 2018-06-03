CREATE TABLE [dbo].[TBL_SCMS_TOKEN]
(
	[token_id] NVARCHAR(100) NOT NULL PRIMARY KEY, 
    [token_user_id] NVARCHAR(20) NOT NULL, 
    [token_created] DATETIME NOT NULL DEFAULT GETDATE(), 
    [token_created_by] NVARCHAR(20) NOT NULL, 
    [token_expires_at] DATETIME NOT NULL, 
    [token_source_device_id] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_TBL_SCMS_TOKEN_TBL_SCMS_USER] FOREIGN KEY (token_user_id) REFERENCES [TBL_SCMS_USER]([user_id]), 
    CONSTRAINT [FK_TBL_SCMS_TOKEN_TBL_SCMS_USER_CRE] FOREIGN KEY (token_created_by) REFERENCES [TBL_SCMS_USER]([user_id]) 
)
