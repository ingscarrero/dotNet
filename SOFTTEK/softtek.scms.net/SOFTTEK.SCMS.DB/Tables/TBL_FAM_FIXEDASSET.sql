CREATE TABLE [dbo].[TBL_FAM_FIXEDASSET]
(
	[fxdast_id_Pk] BIGINT IDENTITY NOT NULL PRIMARY KEY, 
    [fxdast_image_url] NVARCHAR(500) NOT NULL, 
    [fxdast_serial_number] NVARCHAR(500) NOT NULL, 
    [fxdast_external_identifier] NVARCHAR(500) NOT NULL,
	[fxdast_description] NVARCHAR(MAX) NULL,
	[fxdast_placement] NVARCHAR(MAX) NOT NULL,
	[fxdast_plannificator_center] NVARCHAR(MAX) NOT NULL,
	[fxdast_area] NVARCHAR(MAX) NOT NULL,
	[fxdast_cost_center] NVARCHAR(MAX) NOT NULL,
	[fxdast_activity] NVARCHAR(MAX) NOT NULL
)
