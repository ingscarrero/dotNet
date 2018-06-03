CREATE PROCEDURE [dbo].[spFAMGetWorkOrder]
	@token_id NVARCHAR(50), 
	@filter_wrkord_id_Pk BIGINT = NULL, 
	@filter_wrkord_number NVARCHAR(100) = NULL, 
	@filter_wrkord_status NVARCHAR(100) = NULL, 
	@filter_wrkord_issued_at DATETIME = NULL, 
	@filter_wrkord_scheduled_to DATETIME = NULL, 
	@filter_wrkord_provider_Fk BIGINT = NULL, 
	@filter_wrkord_description NVARCHAR(MAX) = NULL,
	@filter_wrkord_physical_inventory_taking_Fk BIGINT = NULL
AS
	DECLARE @v_token_user nvarchar(20);
	DECLARE @v_token_employee_id int;
	BEGIN

		SELECT
			@v_token_employee_id = [TBL_SRA_EMPLOYEE].[id], 
			@v_token_user = [TBL_SCMS_TOKEN].[token_user_id]
		FROM [TBL_SRA_EMPLOYEE]
			INNER JOIN [TBL_SCMS_USER] ON [TBL_SCMS_USER].[user_id] = [TBL_SRA_EMPLOYEE].[user]
			INNER JOIN [TBL_SCMS_TOKEN] ON [TBL_SCMS_TOKEN].[token_user_id] = [TBL_SCMS_USER].[user_id]
				AND [TBL_SCMS_TOKEN].[token_id] = @token_id
				AND [TBL_SCMS_TOKEN].[token_expires_at] > GETDATE();

		IF @v_token_employee_id IS NULL
			RAISERROR ('Invalid token', 16, 1);
		ELSE
			SELECT
				[TBL_FAM_WORK_ORDER].[wrkord_id_Pk], 
				[TBL_FAM_WORK_ORDER].[wrkord_number], 
				[TBL_FAM_WORK_ORDER].[wrkord_status], 
				[TBL_FAM_WORK_ORDER].[wrkord_issued_at], 
				[TBL_FAM_WORK_ORDER].[wrkord_scheduled_to], 
				[TBL_FAM_PROVIDER].[prv_id_Pk], 
				[TBL_FAM_WORK_ORDER].[wrkord_description],
				[TBL_FAM_INVENTORY_TAKING].[phyinvtkn_id_Pk]
			FROM [TBL_FAM_WORK_ORDER]
				INNER JOIN [TBL_FAM_PROVIDER] ON [TBL_FAM_PROVIDER].[prv_id_Pk] = [TBL_FAM_WORK_ORDER].[wrkord_provider_Fk]
				INNER JOIN [TBL_FAM_INVENTORY_TAKING] ON [TBL_FAM_INVENTORY_TAKING].[phyinvtkn_id_Pk] = [TBL_FAM_WORK_ORDER].[wrkord_physical_inventory_taking_Fk]
			WHERE 
				[TBL_FAM_WORK_ORDER].[wrkord_id_Pk] = ISNULL(@filter_wrkord_id_Pk, [TBL_FAM_WORK_ORDER].[wrkord_id_Pk])
				AND [TBL_FAM_WORK_ORDER].[wrkord_number] = ISNULL(@filter_wrkord_number, [TBL_FAM_WORK_ORDER].[wrkord_number])
				AND [TBL_FAM_WORK_ORDER].[wrkord_status] = ISNULL(@filter_wrkord_status, [TBL_FAM_WORK_ORDER].[wrkord_status])
				AND [TBL_FAM_WORK_ORDER].[wrkord_issued_at] = ISNULL(@filter_wrkord_issued_at, [TBL_FAM_WORK_ORDER].[wrkord_issued_at])
				AND [TBL_FAM_WORK_ORDER].[wrkord_scheduled_to] = ISNULL(@filter_wrkord_scheduled_to, [TBL_FAM_WORK_ORDER].[wrkord_scheduled_to])
				AND [TBL_FAM_WORK_ORDER].[wrkord_provider_Fk] = ISNULL(@filter_wrkord_provider_Fk, [TBL_FAM_WORK_ORDER].[wrkord_provider_Fk])
				AND [TBL_FAM_WORK_ORDER].[wrkord_description] = ISNULL(@filter_wrkord_description, [TBL_FAM_WORK_ORDER].[wrkord_description])
				AND [TBL_FAM_WORK_ORDER].[wrkord_physical_inventory_taking_Fk] = ISNULL(@filter_wrkord_physical_inventory_taking_Fk, [TBL_FAM_WORK_ORDER].[wrkord_physical_inventory_taking_Fk]);

	END
RETURN 0									
