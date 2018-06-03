CREATE TABLE [dbo].[TBL_PM_MATERIAL]
(
	[mtr_id_Pk] BIGINT IDENTITY NOT NULL , 
	[mtr_external_identifier] NVARCHAR(500),
    [mtr_name] NVARCHAR(50) NOT NULL, 
    [mtr_description] NVARCHAR(500) NOT NULL, 
    [mtr_class] NVARCHAR(50) NOT NULL, 
    [mtr_stock] NVARCHAR(100) NOT NULL, 
	[mtr_task_Fk] bigint NOT NULL,
	[mtr_material_parameter] BIGINT NULL,
	[mtr_observations] NVARCHAR(500) NULL,
    CONSTRAINT [PK_TBL_PM_MATERIAL] PRIMARY KEY ([mtr_id_Pk]),
	CONSTRAINT [FK_TBL_PM_MATERIAL_TBL_PM_TASK] FOREIGN KEY ([mtr_task_Fk]) REFERENCES [TBL_PM_TASK]([tsk_id_Pk])
)
