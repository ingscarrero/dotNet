CREATE PROCEDURE [dbo].[spFAMUpdateInventoryTaking]
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
	@filter_phyinvtkn_updated_at DATETIME = NULL, 
	@new_phyinvtkn_accountable_Fk INT = NULL, 
	@new_phyinvtkn_responsible_Fk INT = NULL, 
	@new_phyinvtkn_informed_Fk INT = NULL, 
	@new_phyinvtkn_work_order_Fk INT = NULL, 
	@new_phyinvtkn_location NVARCHAR(50) = NULL, 
	@new_phyinvtkn_status NVARCHAR(10) = NULL, 
	@new_phyinvtkn_comments NVARCHAR(500) = NULL, 
	@new_phyinvtkn_registered_at DATETIME = NULL, 
	@new_phyinvtkn_updated_at DATETIME = NULL
AS
	DECLARE @v_token_user nvarchar(20);
	DECLARE @v_token_employee_id int;
	DECLARE @v_INVENTORY_TAKING table (
		v_phyinvtkn_id_Pk BIGINT, 
		v_phyinvtkn_accountable_Fk INT, 
		v_phyinvtkn_responsible_Fk INT, 
		v_phyinvtkn_informed_Fk INT, 
		v_phyinvtkn_work_order_Fk INT, 
		v_phyinvtkn_location NVARCHAR(50), 
		v_phyinvtkn_status NVARCHAR(10), 
		v_phyinvtkn_comments NVARCHAR(500), 
		v_phyinvtkn_registered_at DATETIME, 
		v_phyinvtkn_updated_at DATETIME
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
			UPDATE [TBL_FAM_INVENTORY_TAKING]
			SET
				[TBL_FAM_INVENTORY_TAKING].[phyinvtkn_accountable_Fk] = ISNULL(@new_phyinvtkn_accountable_Fk, [TBL_FAM_INVENTORY_TAKING].[phyinvtkn_accountable_Fk]), 
				[TBL_FAM_INVENTORY_TAKING].[phyinvtkn_responsible_Fk] = ISNULL(@new_phyinvtkn_responsible_Fk, [TBL_FAM_INVENTORY_TAKING].[phyinvtkn_responsible_Fk]), 
				[TBL_FAM_INVENTORY_TAKING].[phyinvtkn_informed_Fk] = ISNULL(@new_phyinvtkn_informed_Fk, [TBL_FAM_INVENTORY_TAKING].[phyinvtkn_informed_Fk]), 
				[TBL_FAM_INVENTORY_TAKING].[phyinvtkn_work_order_Fk] = ISNULL(@new_phyinvtkn_work_order_Fk, [TBL_FAM_INVENTORY_TAKING].[phyinvtkn_work_order_Fk]), 
				[TBL_FAM_INVENTORY_TAKING].[phyinvtkn_location] = ISNULL(@new_phyinvtkn_location, [TBL_FAM_INVENTORY_TAKING].[phyinvtkn_location]), 
				[TBL_FAM_INVENTORY_TAKING].[phyinvtkn_status] = ISNULL(@new_phyinvtkn_status, [TBL_FAM_INVENTORY_TAKING].[phyinvtkn_status]), 
				[TBL_FAM_INVENTORY_TAKING].[phyinvtkn_comments] = ISNULL(@new_phyinvtkn_comments, [TBL_FAM_INVENTORY_TAKING].[phyinvtkn_comments]), 
				[TBL_FAM_INVENTORY_TAKING].[phyinvtkn_registered_at] = ISNULL(@new_phyinvtkn_registered_at, [TBL_FAM_INVENTORY_TAKING].[phyinvtkn_registered_at]), 
				[TBL_FAM_INVENTORY_TAKING].[phyinvtkn_updated_at] = ISNULL(@new_phyinvtkn_updated_at, [TBL_FAM_INVENTORY_TAKING].[phyinvtkn_updated_at])
			OUTPUT 
				inserted.[phyinvtkn_id_Pk], 
				inserted.[phyinvtkn_accountable_Fk], 
				inserted.[phyinvtkn_responsible_Fk], 
				inserted.[phyinvtkn_informed_Fk], 
				inserted.[phyinvtkn_work_order_Fk], 
				inserted.[phyinvtkn_location], 
				inserted.[phyinvtkn_status], 
				inserted.[phyinvtkn_comments], 
				inserted.[phyinvtkn_registered_at], 
				inserted.[phyinvtkn_updated_at]
			INTO @v_INVENTORY_TAKING
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

			SELECT 
				v_phyinvtkn_id_Pk, 
				v_phyinvtkn_accountable_Fk, 
				v_phyinvtkn_responsible_Fk, 
				v_phyinvtkn_informed_Fk, 
				v_phyinvtkn_work_order_Fk, 
				v_phyinvtkn_location, 
				v_phyinvtkn_status, 
				v_phyinvtkn_comments, 
				v_phyinvtkn_registered_at, 
				v_phyinvtkn_updated_at
			FROM @v_INVENTORY_TAKING;
	END
RETURN 0									
