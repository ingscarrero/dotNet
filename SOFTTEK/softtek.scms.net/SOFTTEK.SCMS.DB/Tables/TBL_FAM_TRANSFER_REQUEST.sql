﻿CREATE TABLE [dbo].[TBL_FAM_TRANSFER_REQUEST]
(
trnrqs_id_Pk	BIGINT PRIMARY KEY NOT NULL,
trnrqs_number 	VARCHAR(MAX) NOT NULL,
trnrqs_informed_Fk	INT NOT NULL,
trnrqs_responsible_Fk	INT NOT NULL,
trnrqs_accountable_Fk	INT NOT NULL,
trnrqs_details	NVARCHAR(500) NOT NULL,
trnrqs_type_Fk 	BIGINT NOT NULL,
trnrqs_topic	NVARCHAR(50) NOT NULL,
trnrqs_registered_at	DATETIME NOT NULL,
trnrqs_updated_at 	DATETIME NOT NULL,
trnrqs_status	NVARCHAR(10) NOT NULL,
trnrqs_comments	NVARCHAR(500) NOT NULL,
trnrqs_origin	NVARCHAR(500) NOT NULL,
trnrqs_destination	NVARCHAR(500) NOT NULL,
trnrqs_external_identifier	NVARCHAR(500) NOT NULL,
trnrqs_novelty_Fk	BIGINT NOT NULL,
CONSTRAINT [FK_TBL_FAM_TRANSFER_REQUEST_TBL_SRA_EMPLOYEE_APP] FOREIGN KEY ([trnrqs_informed_Fk]) REFERENCES [TBL_SRA_EMPLOYEE]([id]), 
CONSTRAINT [FK_TBL_FAM_TRANSFER_REQUEST_TBL_SRA_EMPLOYEE_RES] FOREIGN KEY ([trnrqs_responsible_Fk]) REFERENCES [TBL_SRA_EMPLOYEE]([id]), 
CONSTRAINT [FK_TBL_FAM_TRANSFER_REQUEST_TBL_SRA_EMPLOYEE_ACC] FOREIGN KEY ([trnrqs_accountable_Fk]) REFERENCES [TBL_SRA_EMPLOYEE]([id]),
CONSTRAINT [FK_TBL_FAM_TRANSFER_REQUEST_TBL_FAM_NOVELTY] FOREIGN KEY ([trnrqs_novelty_Fk]) REFERENCES [TBL_FAM_NOVELTY]([nvlrqs_id_Pk]),

)
