CREATE TABLE [dbo].[TBL_PM_MAINTENANCE_PLAN]
(
	mntpln_id_Pk	BIGINT IDENTITY PRIMARY KEY NOT NULL,
mntpln_activities	NVARCHAR(500) NULL,
mntpln_external_identifier NVARCHAR(500) NULL,
mntpln_description	NVARCHAR(max) NULL,
mntpln_comments	NVARCHAR(500) NOT NULL,
mntpln_device_type	NVARCHAR(20) NOT NULL, 
    [mntpln_work_order] BIGINT NOT NULL,
	CONSTRAINT [FK_TBL_PM_MAINTENANCE_PLAN_TBL_PM_WORK_ORDER] FOREIGN KEY ([mntpln_work_order]) REFERENCES [TBL_PM_WORK_ORDER]([wrkord_id_Pk])
)
