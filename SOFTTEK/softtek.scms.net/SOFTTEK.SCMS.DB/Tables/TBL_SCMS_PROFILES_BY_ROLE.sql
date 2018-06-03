﻿CREATE TABLE [dbo].[TBL_SCMS_PROFILES_BY_ROLE]
(
	[role] INT NOT NULL , 
    [profile] INT NOT NULL, 
    [created_at] DATETIME NOT NULL DEFAULT GETDATE(), 
    [created_by] NVARCHAR(20) NOT NULL, 
    [modified_at] DATETIME NULL, 
    [modified_by] NVARCHAR(20) NULL, 
    [is_active] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_TBL_SCMS_PROFILES_BY_ROLE_TBL_SCMS_USER_CRE] FOREIGN KEY (created_by) REFERENCES [TBL_SCMS_USER]([user_id]), 
    CONSTRAINT [FK_TBL_SCMS_PROFILES_BY_ROLE_TBL_SCMS-USER_MOD] FOREIGN KEY (modified_by) REFERENCES [TBL_SCMS_USER]([user_id]), 
    PRIMARY KEY ([role], [profile]), 
    CONSTRAINT [FK_TBL_SCMS_PROFILES_BY_ROLE_TBL_SCMS_ROLE] FOREIGN KEY ([role]) REFERENCES [TBL_SCMS_ROLE]([id]), 
    CONSTRAINT [FK_TBL_SCMS_PROFILES_BY_ROLE_TBL_SCMS_PROFILE] FOREIGN KEY ([profile]) REFERENCES [TBL_SCMS_PROFILE]([id])
)