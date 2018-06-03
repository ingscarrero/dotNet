﻿CREATE TABLE [dbo].[TBL_PM_MEASUREMENT_DOCUMENT]
(
	msrdcm_id_Pk	BIGINT IDENTITY NOT NULL PRIMARY KEY,
msrdcm_external_identifier	NVARCHAR(500) NULL,
msrdcm_task_Fk	BIGINT NULL,
[msrdcm_device_type]	NVARCHAR(20) NOT NULL,
[msrdcm_technical_object_Fk] BIGINT NOT NULL, 
    CONSTRAINT [FK_TBL_PM_MEASUREMENT_DOCUMENT_TBL_PM_TASK] FOREIGN KEY ([msrdcm_task_Fk]) REFERENCES [TBL_PM_TASK]([tsk_id_Pk]),
	CONSTRAINT [FK_TBL_PM_MEASUREMENT_DOCUMENT_TBL_PM_TECHNICALOBJECT] FOREIGN KEY ([msrdcm_technical_object_Fk]) REFERENCES [TBL_PM_TECHNICALOBJECT]([tchobj_id_Pk])
)