CREATE TABLE [dbo].[TBL_PM_TECHNICALOBJECT]
(
	[tchobj_id_Pk] BIGINT IDENTITY NOT NULL PRIMARY KEY, 
    [tchobj_name] NVARCHAR(100) NOT NULL, 
	[tchobj_type_object] NVARCHAR(10) NOT NULL,
    [tchobj_external_identifier] NVARCHAR(500) NOT NULL, 
    [tchobj_description] NVARCHAR(MAX) NULL, 
    [tchobj_placement] NVARCHAR(500) NOT NULL, 
    [tchobj_plannification_center] NVARCHAR(500) NOT NULL, 
    [tchobj_area] NVARCHAR(500) NOT NULL, 
    [tchobj_cost_center] NVARCHAR(500) NOT NULL 
)
