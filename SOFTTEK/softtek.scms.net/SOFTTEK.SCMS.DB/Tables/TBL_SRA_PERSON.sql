CREATE TABLE [dbo].[TBL_SRA_PERSON]
(
	[id] INT NOT NULL PRIMARY KEY, 
    [name] NVARCHAR(50) NOT NULL, 
    [middle_name] NVARCHAR(50) NULL, 
    [last_name] NVARCHAR(50) NOT NULL, 
    [gender] NVARCHAR(1) NULL, 
    [from] NVARCHAR(100) NOT NULL, 
    [bornAt] DATETIME NOT NULL, 
    [identification] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [AK_TBL_SRA_PERSON_idENTIFICATION] UNIQUE ([identification]), 
    CONSTRAINT [CK_TBL_SRA_PERSON_GENDER] CHECK (dbo.fnCheckGender([gender]) = 1) 
)
