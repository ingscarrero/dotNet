CREATE TABLE [dbo].[TBL_SCMS_APP]
(
	[id] NVARCHAR(50) NOT NULL PRIMARY KEY, 
    [name] NVARCHAR(100) NOT NULL, 
    [summary] NVARCHAR(250) NOT NULL, 
    [detailed_description] NVARCHAR(MAX) NULL, 
    [version] NVARCHAR(10) NOT NULL, 
    [is_published] BIT NOT NULL, 
    [approved_by] NVARCHAR(20) NULL, 
    [approved_at] DATETIME NULL, 
    [category] NVARCHAR(100) NOT NULL DEFAULT 'Undefined'
)
