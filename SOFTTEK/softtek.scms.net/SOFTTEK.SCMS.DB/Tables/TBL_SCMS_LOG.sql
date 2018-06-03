CREATE TABLE [dbo].[TBL_SCMS_LOG]
(
	[log_event_id] NVARCHAR(50) NOT NULL PRIMARY KEY DEFAULT NEWID(), 
    [log_event_type] SMALLINT NOT NULL, 
    [log_event_description] NVARCHAR(MAX) NOT NULL, 
    [log_event_user] NVARCHAR(20) NOT NULL, 
    [log_event_date] DATETIME NOT NULL DEFAULT GETDATE(), 
    [log_event_app] NVARCHAR(50) NOT NULL, 
    [log_event_detail] NVARCHAR(MAX) NOT NULL, 
    [log_event_app_component] NVARCHAR(250) NULL
)
