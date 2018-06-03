CREATE PROCEDURE [dbo].spPMGetMaintenancePlan
	@token_id NVARCHAR(50), 
	@filter_mntpln_id_Pk BIGINT = NULL, 
	@filter_mntpln_external_identifier NVARCHAR(500) = NULL,
	--@filter_mntpln_activities NVARCHAR(500) = NULL, 
	@filter_mntpln_description NVARCHAR(max) = NULL, 
	@filter_mntpln_comments NVARCHAR(500) = NULL, 
	@filter_mntpln_device_type NVARCHAR(20) = NULL,
	@filter_mntpln_work_order BIGINT = NULL
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
				[TBL_PM_MAINTENANCE_PLAN].[mntpln_id_Pk], 
				[TBL_PM_MAINTENANCE_PLAN].[mntpln_external_identifier],
				[TBL_PM_MAINTENANCE_PLAN].[mntpln_activities], 
				[TBL_PM_MAINTENANCE_PLAN].[mntpln_description], 
				[TBL_PM_MAINTENANCE_PLAN].[mntpln_comments], 
				[TBL_PM_MAINTENANCE_PLAN].[mntpln_device_type],
				[TBL_PM_MAINTENANCE_PLAN].[mntpln_work_order]
			FROM [TBL_PM_MAINTENANCE_PLAN]
			WHERE 
				[TBL_PM_MAINTENANCE_PLAN].[mntpln_id_Pk] = ISNULL(@filter_mntpln_id_Pk, [TBL_PM_MAINTENANCE_PLAN].[mntpln_id_Pk])
				AND [TBL_PM_MAINTENANCE_PLAN].[mntpln_external_identifier] = ISNULL(@filter_mntpln_external_identifier, [TBL_PM_MAINTENANCE_PLAN].mntpln_external_identifier)
				--AND [TBL_PM_MAINTENANCE_PLAN].[mntpln_activities] = ISNULL(@filter_mntpln_activities, [TBL_PM_MAINTENANCE_PLAN].[mntpln_activities])
				AND [TBL_PM_MAINTENANCE_PLAN].[mntpln_description] = ISNULL(@filter_mntpln_description, [TBL_PM_MAINTENANCE_PLAN].[mntpln_description])
				AND [TBL_PM_MAINTENANCE_PLAN].[mntpln_comments] = ISNULL(@filter_mntpln_comments, [TBL_PM_MAINTENANCE_PLAN].[mntpln_comments])
				AND [TBL_PM_MAINTENANCE_PLAN].[mntpln_device_type] = ISNULL(@filter_mntpln_device_type, [TBL_PM_MAINTENANCE_PLAN].[mntpln_device_type])
				AND [TBL_PM_MAINTENANCE_PLAN].[mntpln_work_order] = ISNULL(@filter_mntpln_work_order, [TBL_PM_MAINTENANCE_PLAN].[mntpln_work_order]);

	END
RETURN 0									
