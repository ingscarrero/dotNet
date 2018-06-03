CREATE TABLE [dbo].[TBL_PM_ACTIVITY]
(
	[act_id_Pk] BIGINT IDENTITY NOT NULL PRIMARY KEY,
	[act_external_identifier] NVARCHAR(500) NOT NULL,
	[act_name] VARCHAR(200) NOT NULL,
	[act_description] VARCHAR(MAX) NULL,
	[act_execution_start_at] DATETIME NOT NULL,
	[act_execution_finished_at] DATETIME NOT NULL,
	[act_total_duration] INT NOT NULL DEFAULT 0,
	[act_status] VARCHAR(100) NOT NULL,
	[act_comments] VARCHAR(MAX) NULL,
	[act_maintenance_plan] BIGINT NOT NULL,
	CONSTRAINT [FK_TBL_PM_ACTIVITY_TBL_PM_MAINTENANCE_PLAN] FOREIGN KEY ([act_maintenance_plan]) REFERENCES [TBL_PM_MAINTENANCE_PLAN]([mntpln_id_Pk])
)
