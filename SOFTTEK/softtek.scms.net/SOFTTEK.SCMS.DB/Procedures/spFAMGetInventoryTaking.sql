CREATE PROCEDURE [dbo].[spFAMGetInventoryTaking]
	@token_id NVARCHAR(50), 
	@filter_phyinvtkn_id_Pk BIGINT = NULL, 
	@filter_phyinvtkn_accountable_Fk INT = NULL, 
	@filter_phyinvtkn_responsible_Fk INT = NULL, 
	@filter_phyinvtkn_informed_Fk INT = NULL, 
	@filter_phyinvtkn_work_order_Fk INT = NULL, 
	@filter_phyinvtkn_location NVARCHAR(50) = NULL, 
	@filter_phyinvtkn_status NVARCHAR(10) = NULL, 
	@filter_phyinvtkn_comments NVARCHAR(500) = NULL, 
	@filter_phyinvtkn_registered_at DATETIME = NULL, 
	@filter_phyinvtkn_updated_at DATETIME = NULL
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
				[TBL_FAM_INVENTORY_TAKING].[phyinvtkn_id_Pk], 
				accountable.[id] AS phyinvtkn_accountable_Fk, 
				responsible.[id] AS phyinvtkn_responsible_Fk, 
				informed.[id] AS phyinvtkn_informed_Fk, 
				[TBL_FAM_WORK_ORDER].[wrkord_id_Pk], 
				[TBL_FAM_INVENTORY_TAKING].[phyinvtkn_location], 
				[TBL_FAM_INVENTORY_TAKING].[phyinvtkn_status], 
				[TBL_FAM_INVENTORY_TAKING].[phyinvtkn_comments], 
				[TBL_FAM_INVENTORY_TAKING].[phyinvtkn_registered_at], 
				[TBL_FAM_INVENTORY_TAKING].[phyinvtkn_updated_at]
			FROM [TBL_FAM_INVENTORY_TAKING]
				INNER JOIN [TBL_SRA_EMPLOYEE] AS accountable ON accountable.[id] = [TBL_FAM_INVENTORY_TAKING].[phyinvtkn_accountable_Fk]
				INNER JOIN [TBL_SRA_EMPLOYEE] AS responsible ON responsible.[id] = [TBL_FAM_INVENTORY_TAKING].[phyinvtkn_responsible_Fk]
				INNER JOIN [TBL_SRA_EMPLOYEE] AS informed ON informed.[id] = [TBL_FAM_INVENTORY_TAKING].[phyinvtkn_informed_Fk]
				INNER JOIN [TBL_FAM_WORK_ORDER] ON [TBL_FAM_WORK_ORDER].[wrkord_id_Pk] = [TBL_FAM_INVENTORY_TAKING].[phyinvtkn_work_order_Fk]
			WHERE 
				[TBL_FAM_INVENTORY_TAKING].[phyinvtkn_id_Pk] = ISNULL(@filter_phyinvtkn_id_Pk, [TBL_FAM_INVENTORY_TAKING].[phyinvtkn_id_Pk])
				AND [TBL_FAM_INVENTORY_TAKING].[phyinvtkn_accountable_Fk] = ISNULL(@filter_phyinvtkn_accountable_Fk, [TBL_FAM_INVENTORY_TAKING].[phyinvtkn_accountable_Fk])
				AND [TBL_FAM_INVENTORY_TAKING].[phyinvtkn_responsible_Fk] = ISNULL(@filter_phyinvtkn_responsible_Fk, [TBL_FAM_INVENTORY_TAKING].[phyinvtkn_responsible_Fk])
				AND [TBL_FAM_INVENTORY_TAKING].[phyinvtkn_informed_Fk] = ISNULL(@filter_phyinvtkn_informed_Fk, [TBL_FAM_INVENTORY_TAKING].[phyinvtkn_informed_Fk])
				AND [TBL_FAM_INVENTORY_TAKING].[phyinvtkn_work_order_Fk] = ISNULL(@filter_phyinvtkn_work_order_Fk, [TBL_FAM_INVENTORY_TAKING].[phyinvtkn_work_order_Fk])
				AND [TBL_FAM_INVENTORY_TAKING].[phyinvtkn_location] = ISNULL(@filter_phyinvtkn_location, [TBL_FAM_INVENTORY_TAKING].[phyinvtkn_location])
				AND [TBL_FAM_INVENTORY_TAKING].[phyinvtkn_status] = ISNULL(@filter_phyinvtkn_status, [TBL_FAM_INVENTORY_TAKING].[phyinvtkn_status])
				AND [TBL_FAM_INVENTORY_TAKING].[phyinvtkn_comments] = ISNULL(@filter_phyinvtkn_comments, [TBL_FAM_INVENTORY_TAKING].[phyinvtkn_comments])
				AND [TBL_FAM_INVENTORY_TAKING].[phyinvtkn_registered_at] = ISNULL(@filter_phyinvtkn_registered_at, [TBL_FAM_INVENTORY_TAKING].[phyinvtkn_registered_at])
				AND [TBL_FAM_INVENTORY_TAKING].[phyinvtkn_updated_at] = ISNULL(@filter_phyinvtkn_updated_at, [TBL_FAM_INVENTORY_TAKING].[phyinvtkn_updated_at]);

	END
RETURN 0									
