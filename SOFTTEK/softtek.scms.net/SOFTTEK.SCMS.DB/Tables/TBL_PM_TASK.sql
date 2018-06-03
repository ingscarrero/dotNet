CREATE TABLE [dbo].[TBL_PM_TASK]
(
	[tsk_id_Pk] BIGINT IDENTITY NOT NULL PRIMARY KEY , 
	[tsk_external_identifier] NVARCHAR(500) NOT NULL,
	[tsk_name] NVARCHAR(200) NOT NULL,
	[tsk_description] NVARCHAR(MAX) NOT NULL,
    [tsk_performer] NVARCHAR(50) NOT NULL, 
    [tsk_started_at] DATETIME NOT NULL, 
    [tsk_finished_at] DATETIME NOT NULL, 
	[tsk_status] NVARCHAR(50) NOT NULL,
	[tsk_comments] NVARCHAR(300) NULL,
	[tsk_quantity_capacity] NVARCHAR(50) NOT NULL,
	[tsk_duration_operation] NVARCHAR(50) NOT NULL, 
    [tsk_plan_Fk] BIGINT NOT NULL, 
    [tsk_work_order_Fk] BIGINT NOT NULL, 
  	CONSTRAINT [FK_TBL_PM_tsk_TBL_PM_MAINTENANCE_PLAN] FOREIGN KEY ([tsk_plan_Fk]) REFERENCES [TBL_PM_MAINTENANCE_PLAN]([mntpln_id_Pk]),
	CONSTRAINT [FK_TBL_PM_tsk_TBL_PM_WORK_ORDER] FOREIGN KEY ([tsk_work_order_Fk]) REFERENCES [TBL_PM_WORK_ORDER]([wrkord_id_Pk])
)
