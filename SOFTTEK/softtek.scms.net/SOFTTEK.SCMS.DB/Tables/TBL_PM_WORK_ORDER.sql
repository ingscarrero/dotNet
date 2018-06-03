CREATE TABLE [dbo].[TBL_PM_WORK_ORDER]
(
	[wrkord_id_Pk] BIGINT IDENTITY NOT NULL PRIMARY KEY, 
	[wrkord_external_identifier] NVARCHAR(50) NOT NULL,
    [wrkord_type] NVARCHAR(50) NULL, 
    [wrkord_company] NVARCHAR(50) NULL, 
    [wrkord_priority] NVARCHAR(50) NULL, 
    [wrkord_performer] NVARCHAR(50) NULL, 
    [wrkord_state] NVARCHAR(50) NULL, 
	[wrkord_technical_object_Fk] BIGINT NOT NULL,
	[wrkord_scheduled_to] DATETIME NULL, 
    [wrkord_execution_start_at] DATETIME NULL, 
    [wrkord_execution_finished_at] DATETIME NULL, 
	[wrkord_release_date] DATETIME NULL,
	[wrkord_planning_group] NVARCHAR(100) NOT NULL,
	[wrkord_workstation] NVARCHAR(100) NOT NULL,
	[wrkord_activity] NVARCHAR(500) NOT NULL, 
    CONSTRAINT [TBL_PM_WORK_ORDER_TBL_PM_TECHNICALOBJECT] FOREIGN KEY ([wrkord_technical_object_Fk]) REFERENCES [TBL_PM_TECHNICALOBJECT]([tchobj_id_Pk])
)
