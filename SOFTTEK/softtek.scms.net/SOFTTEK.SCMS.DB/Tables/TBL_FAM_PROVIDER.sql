CREATE TABLE [dbo].[TBL_FAM_PROVIDER]
(
	[prv_id_Pk] BIGINT IDENTITY NOT NULL PRIMARY KEY, 
    [prv_external_identifier] NVARCHAR(500) NOT NULL, 
    [prv_name] NVARCHAR(50) NOT NULL, 
    [prv_contract] NVARCHAR(50) NOT NULL, 
    [prv_document] NVARCHAR(50) NOT NULL, 
    [prv_state] NVARCHAR(10) NOT NULL
)
