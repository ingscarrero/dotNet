CREATE TABLE [dbo].[TBL_SRA_CONTACT]
(
	[id] INT NOT NULL PRIMARY KEY, 
    [country] NVARCHAR(100) NOT NULL, 
    [subdivision] NVARCHAR(100) NOT NULL, 
    [city] NVARCHAR(100) NOT NULL, 
    [address] NVARCHAR(250) NULL, 
    [zip] NVARCHAR(20) NULL, 
    [phone_numbers] NVARCHAR(250) NULL, 
    [e_mail] NVARCHAR(100) NOT NULL, 
    [person] INT NOT NULL, 
    [is_principal] BIT NOT NULL DEFAULT 1, 
    CONSTRAINT [FK_TBL_SRA_CONTACT_TBL_SRA_PERSON] FOREIGN KEY ([person]) REFERENCES [TBL_SRA_PERSON]([id])
)
