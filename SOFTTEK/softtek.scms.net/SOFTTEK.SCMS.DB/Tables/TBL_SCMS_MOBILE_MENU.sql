CREATE TABLE [dbo].[TBL_SCMS_MOBILE_MENU]
(
	[item_id] INT NOT NULL PRIMARY KEY, 
    [item_title] NVARCHAR(150) NULL, 
    [item_caption] NVARCHAR(MAX) NULL, 
    [item_view] INT NULL, 
    [item_image_url] NVARCHAR(250) NULL, 
    [item_is_active] BIT NULL DEFAULT 1, 
    [item_created] DATETIME NOT NULL DEFAULT GETDATE(), 
    [item_modified] DATETIME NULL, 
    [item_created_by] NVARCHAR(20) NOT NULL, 
    [item_modified_by] NVARCHAR(20) NULL, 
    CONSTRAINT [FK_TBL_SCMS_MOB_MEN_TBL_SCMS_USER] FOREIGN KEY (item_created_by) REFERENCES [TBL_SCMS_USER]([user_id]), 
    CONSTRAINT [FK_TBL_SCMS_MOB_MEN_TBL_SCMS-USER_2] FOREIGN KEY (item_modified_by) REFERENCES [TBL_SCMS_USER]([user_id]), 
    CONSTRAINT [FK_TBL_SCMS_MOBILE_MENU_TBL_SCMS_VIEW] FOREIGN KEY ([item_view]) REFERENCES [TBL_SCMS_VIEW]([id])
)