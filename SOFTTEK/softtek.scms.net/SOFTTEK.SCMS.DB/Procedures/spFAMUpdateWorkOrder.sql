CREATE PROCEDURE [dbo].[spFAMUpdateWorkOrder]
	@token_id NVARCHAR(50), 
	@filter_wrkord_id_Pk BIGINT = NULL, 
	@filter_wrkord_number NVARCHAR(100) = NULL, 
	@filter_wrkord_status NVARCHAR(100) = NULL, 
	@filter_wrkord_issued_at DATETIME = NULL, 
	@filter_wrkord_scheduled_to DATETIME = NULL, 
	@filter_wrkord_provider_Fk BIGINT = NULL, 
	@filter_wrkord_description NVARCHAR(MAX) = NULL, 
	@filter_wrkord_physical_inventory_taking_Fk BIGINT = NULL,
	@new_wrkord_number NVARCHAR(100) = NULL, 
	@new_wrkord_status NVARCHAR(100) = NULL, 
	@new_wrkord_issued_at DATETIME = NULL, 
	@new_wrkord_scheduled_to DATETIME = NULL, 
	@new_wrkord_provider_Fk BIGINT = NULL, 
	@new_wrkord_description NVARCHAR(MAX) = NULL,
	@new_wrkord_physical_inventory_taking_Fk BIGINT = NULL
AS
	DECLARE @v_token_user nvarchar(20);
	DECLARE @v_token_employee_id int;
	DECLARE @v_WORK_ORDER table (
		v_wrkord_id_Pk BIGINT, 
		v_wrkord_number NVARCHAR(100), 
		v_wrkord_status NVARCHAR(100), 
		v_wrkord_issued_at DATETIME, 
		v_wrkord_scheduled_to DATETIME, 
		v_wrkord_provider_Fk BIGINT, 
		v_wrkord_description NVARCHAR(MAX),
		v_wrkord_physical_inventory_taking_Fk BIGINT
	);

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
			UPDATE [TBL_FAM_WORK_ORDER]
			SET
				[TBL_FAM_WORK_ORDER].[wrkord_number] = ISNULL(@new_wrkord_number, [TBL_FAM_WORK_ORDER].[wrkord_number]), 
				[TBL_FAM_WORK_ORDER].[wrkord_status] = ISNULL(@new_wrkord_status, [TBL_FAM_WORK_ORDER].[wrkord_status]), 
				[TBL_FAM_WORK_ORDER].[wrkord_issued_at] = ISNULL(@new_wrkord_issued_at, [TBL_FAM_WORK_ORDER].[wrkord_issued_at]), 
				[TBL_FAM_WORK_ORDER].[wrkord_scheduled_to] = ISNULL(@new_wrkord_scheduled_to, [TBL_FAM_WORK_ORDER].[wrkord_scheduled_to]), 
				[TBL_FAM_WORK_ORDER].[wrkord_provider_Fk] = ISNULL(@new_wrkord_provider_Fk, [TBL_FAM_WORK_ORDER].[wrkord_provider_Fk]), 
				[TBL_FAM_WORK_ORDER].[wrkord_description] = ISNULL(@new_wrkord_description, [TBL_FAM_WORK_ORDER].[wrkord_description]),
				[TBL_FAM_WORK_ORDER].[wrkord_physical_inventory_taking_Fk] = ISNULL(@new_wrkord_physical_inventory_taking_Fk, [TBL_FAM_WORK_ORDER].[wrkord_physical_inventory_taking_Fk])
			OUTPUT 
				inserted.[wrkord_id_Pk], 
				inserted.[wrkord_number], 
				inserted.[wrkord_status], 
				inserted.[wrkord_issued_at], 
				inserted.[wrkord_scheduled_to], 
				inserted.[wrkord_provider_Fk], 
				inserted.[wrkord_description],
				inserted.[wrkord_physical_inventory_taking_Fk]
			INTO @v_WORK_ORDER
			WHERE 
				[TBL_FAM_WORK_ORDER].[wrkord_id_Pk] = ISNULL(@filter_wrkord_id_Pk, [TBL_FAM_WORK_ORDER].[wrkord_id_Pk])
				AND [TBL_FAM_WORK_ORDER].[wrkord_number] = ISNULL(@filter_wrkord_number, [TBL_FAM_WORK_ORDER].[wrkord_number])
				AND [TBL_FAM_WORK_ORDER].[wrkord_status] = ISNULL(@filter_wrkord_status, [TBL_FAM_WORK_ORDER].[wrkord_status])
				AND [TBL_FAM_WORK_ORDER].[wrkord_issued_at] = ISNULL(@filter_wrkord_issued_at, [TBL_FAM_WORK_ORDER].[wrkord_issued_at])
				AND [TBL_FAM_WORK_ORDER].[wrkord_scheduled_to] = ISNULL(@filter_wrkord_scheduled_to, [TBL_FAM_WORK_ORDER].[wrkord_scheduled_to])
				AND [TBL_FAM_WORK_ORDER].[wrkord_provider_Fk] = ISNULL(@filter_wrkord_provider_Fk, [TBL_FAM_WORK_ORDER].[wrkord_provider_Fk])
				AND [TBL_FAM_WORK_ORDER].[wrkord_description] = ISNULL(@filter_wrkord_description, [TBL_FAM_WORK_ORDER].[wrkord_description])
				AND [TBL_FAM_WORK_ORDER].[wrkord_physical_inventory_taking_Fk] = ISNULL(@filter_wrkord_physical_inventory_taking_Fk, [TBL_FAM_WORK_ORDER].[wrkord_physical_inventory_taking_Fk]);

			SELECT 
				v_wrkord_id_Pk, 
				v_wrkord_number, 
				v_wrkord_status, 
				v_wrkord_issued_at, 
				v_wrkord_scheduled_to, 
				v_wrkord_provider_Fk, 
				v_wrkord_description,
				v_wrkord_physical_inventory_taking_Fk
			FROM @v_WORK_ORDER;
	END
RETURN 0									
