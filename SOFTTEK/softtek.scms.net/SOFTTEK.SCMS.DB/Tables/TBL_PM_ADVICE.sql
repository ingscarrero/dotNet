CREATE TABLE [dbo].[TBL_PM_ADVICE]
(
	adv_id_Pk	BIGINT IDENTITY PRIMARY KEY NOT NULL,
adv_external_identifier	NVARCHAR(500) NOT NULL,
adv_priority	NVARCHAR(20) NOT NULL,
adv_type	NVARCHAR(20) NOT NULL,
[adv_task_Fk]	BIGINT NULL,
[adv_device_type]	NVARCHAR(20) NOT NULL,
[adv_technical_object_Fk] BIGINT NULL,
[adv_comments] NVARCHAR(500) NULL, 
[adv_scheduled_to] DATETIME NOT NULL,
[adv_execution_start_at] DATETIME NOT NULL,
[adv_execution_finished_at] DATETIME NOT NULL,
    [adv_execution_hour_start_at] NVARCHAR(20) NOT NULL, 
    [adv_execution_hour_finished_at] NVARCHAR(20) NOT NULL, 
    CONSTRAINT [FK_TBL_PM_ADVICE_TBL_PM_TASKR] FOREIGN KEY ([adv_task_Fk]) REFERENCES [TBL_PM_TASK]([tsk_id_Pk]),
	CONSTRAINT [FK_TBL_PM_ADVICE_TBL_PM_TECHNICALOBJECT] FOREIGN KEY ([adv_technical_object_Fk]) REFERENCES [TBL_PM_TECHNICALOBJECT]([tchobj_id_Pk])
)
