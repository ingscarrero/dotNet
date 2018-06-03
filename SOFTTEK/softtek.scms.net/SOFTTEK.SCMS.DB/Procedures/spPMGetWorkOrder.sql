CREATE PROCEDURE [dbo].[spPMGetWorkOrder]
	@token_id NVARCHAR(50), 
	@filter_wrkord_id_Pk BIGINT = NULL, 
	@filter_wrkord_external_identifier NVARCHAR(50) = NULL, 
	@filter_wrkord_type NVARCHAR(50) = NULL, 
	@filter_wrkord_company  NVARCHAR(50) = NULL, 
	@filter_wrkord_priority NVARCHAR(50) = NULL, 
	@filter_wrkord_performer NVARCHAR(50) = NULL, 
	@filter_wrkord_state NVARCHAR(50) = NULL, 
	@filter_wrkord_technical_object_Fk BIGINT = NULL,
	@filter_wrkord_scheduled_to DATETIME = NULL, 
	@filter_wrkord_execution_start_at DATETIME = NULL, 
	@filter_wrkord_execution_finished_at DATETIME = NULL,
	@filter_wrkord_release_date DATETIME = NULL,
	@filter_wrkord_planning_group NVARCHAR(100) = NULL,
	@filter_wrkord_workstation  NVARCHAR(100) = NULL,
	@filter_wrkord_activity NVARCHAR(500) = NULL
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
				[TBL_PM_WORK_ORDER].[wrkord_id_Pk], 
				[TBL_PM_WORK_ORDER].[wrkord_external_identifier], 
				[TBL_PM_WORK_ORDER].[wrkord_type], 
				[TBL_PM_WORK_ORDER].[wrkord_company], 
				[TBL_PM_WORK_ORDER].[wrkord_priority], 
				[TBL_PM_WORK_ORDER].[wrkord_performer], 
				[TBL_PM_WORK_ORDER].[wrkord_state], 
				[TBL_PM_WORK_ORDER].[wrkord_technical_object_Fk],
				[TBL_PM_WORK_ORDER].[wrkord_scheduled_to], 
				[TBL_PM_WORK_ORDER].[wrkord_execution_start_at], 
				[TBL_PM_WORK_ORDER].[wrkord_execution_finished_at],
				[TBL_PM_WORK_ORDER].[wrkord_release_date],
				[TBL_PM_WORK_ORDER].[wrkord_planning_group],
				[TBL_PM_WORK_ORDER].[wrkord_workstation],
				[TBL_PM_WORK_ORDER].[wrkord_activity]

			FROM [TBL_PM_WORK_ORDER]
				INNER JOIN [TBL_PM_TECHNICALOBJECT] ON [TBL_PM_TECHNICALOBJECT].[tchobj_id_Pk] = [TBL_PM_WORK_ORDER].[wrkord_technical_object_Fk]
			WHERE 
				[TBL_PM_WORK_ORDER].[wrkord_id_Pk] = ISNULL(@filter_wrkord_id_Pk, [TBL_PM_WORK_ORDER].[wrkord_id_Pk])
				AND [TBL_PM_WORK_ORDER].[wrkord_external_identifier] = ISNULL(@filter_wrkord_external_identifier, [TBL_PM_WORK_ORDER].[wrkord_external_identifier])
				AND [TBL_PM_WORK_ORDER].[wrkord_type] = ISNULL(@filter_wrkord_type, [TBL_PM_WORK_ORDER].[wrkord_type])
				AND [TBL_PM_WORK_ORDER].[wrkord_company] = ISNULL(@filter_wrkord_company, [TBL_PM_WORK_ORDER].[wrkord_company])
				AND [TBL_PM_WORK_ORDER].[wrkord_priority] = ISNULL(@filter_wrkord_priority, [TBL_PM_WORK_ORDER].[wrkord_priority])
				AND [TBL_PM_WORK_ORDER].[wrkord_performer] = ISNULL(@filter_wrkord_performer, [TBL_PM_WORK_ORDER].[wrkord_performer])
				AND [TBL_PM_WORK_ORDER].[wrkord_state] = ISNULL(@filter_wrkord_state, [TBL_PM_WORK_ORDER].[wrkord_state])
				AND [TBL_PM_WORK_ORDER].[wrkord_scheduled_to] = ISNULL(@filter_wrkord_scheduled_to, [TBL_PM_WORK_ORDER].[wrkord_scheduled_to])
				AND [TBL_PM_WORK_ORDER].[wrkord_execution_start_at] = ISNULL(@filter_wrkord_execution_start_at, [TBL_PM_WORK_ORDER].[wrkord_execution_start_at])
				AND [TBL_PM_WORK_ORDER].[wrkord_execution_finished_at] = ISNULL(@filter_wrkord_execution_finished_at, [TBL_PM_WORK_ORDER].[wrkord_execution_finished_at])
				AND [TBL_PM_WORK_ORDER].[wrkord_technical_object_Fk] = ISNULL(@filter_wrkord_technical_object_Fk, [TBL_PM_WORK_ORDER].[wrkord_technical_object_Fk])
				AND [TBL_PM_WORK_ORDER].[wrkord_release_date] = ISNULL(@filter_wrkord_release_date, [TBL_PM_WORK_ORDER].[wrkord_release_date])
				AND [TBL_PM_WORK_ORDER].[wrkord_planning_group] = ISNULL(@filter_wrkord_planning_group, [TBL_PM_WORK_ORDER].[wrkord_planning_group])
				AND [TBL_PM_WORK_ORDER].[wrkord_workstation] = ISNULL(@filter_wrkord_workstation, [TBL_PM_WORK_ORDER].[wrkord_workstation])
				AND [TBL_PM_WORK_ORDER].[wrkord_activity] = ISNULL(@filter_wrkord_activity, [TBL_PM_WORK_ORDER].[wrkord_activity]);

	END
RETURN 0									
