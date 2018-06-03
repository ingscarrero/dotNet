CREATE PROCEDURE [dbo].[spPMUpdateWorkOrder]
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
	@filter_wrkord_workstation NVARCHAR(100) = NULL,
	@filter_wrkord_activity NVARCHAR(500) = NULL,
	@new_wrkord_external_identifier NVARCHAR(50) = NULL, 
	@new_wrkord_type NVARCHAR(50) = NULL, 
	@new_wrkord_company  NVARCHAR(50) = NULL, 
	@new_wrkord_priority NVARCHAR(50) = NULL, 
	@new_wrkord_performer NVARCHAR(50) = NULL, 
	@new_wrkord_state NVARCHAR(50) = NULL, 
	@new_wrkord_technical_object_Fk BIGINT = NULL,
	@new_wrkord_scheduled_to DATETIME = NULL, 
	@new_wrkord_execution_start_at DATETIME = NULL, 
	@new_wrkord_execution_finished_at DATETIME = NULL,
	@new_wrkord_release_date DATETIME = NULL,
	@new_wrkord_planning_group NVARCHAR(100) = NULL,
	@new_wrkord_workstation NVARCHAR(100) = NULL,
	@new_wrkord_activity NVARCHAR(500) = NULL
AS
	DECLARE @v_token_user nvarchar(20);
	DECLARE @v_token_employee_id int;
	DECLARE @v_ORK_ORDER table (
		v_wrkord_id_Pk BIGINT, 
		v_wrkord_external_identifier NVARCHAR(50), 
		v_wrkord_type NVARCHAR(50), 
		v_wrkord_company  NVARCHAR(50), 
		v_wrkord_priority NVARCHAR(50), 
		v_wrkord_performer NVARCHAR(50), 
		v_wrkord_state NVARCHAR(50), 
		v_wrkord_technical_object_Fk BIGINT,
		v_wrkord_scheduled_to DATETIME, 
		v_wrkord_execution_start_at DATETIME, 
		v_wrkord_execution_finished_at DATETIME,
		v_wrkord_release_date DATETIME,
		v_wrkord_planning_group NVARCHAR(100),
		v_wrkord_workstation NVARCHAR(100),
		v_wrkord_activity NVARCHAR(500)
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
			UPDATE [TBL_PM_WORK_ORDER]
			SET
				[TBL_PM_WORK_ORDER].[wrkord_external_identifier] = ISNULL(@new_wrkord_external_identifier, [TBL_PM_WORK_ORDER].[wrkord_external_identifier]), 
				[TBL_PM_WORK_ORDER].[wrkord_type] = ISNULL(@new_wrkord_type, [TBL_PM_WORK_ORDER].[wrkord_type]), 
				[TBL_PM_WORK_ORDER].[wrkord_company] = ISNULL(@new_wrkord_company, [TBL_PM_WORK_ORDER].[wrkord_company]), 
				[TBL_PM_WORK_ORDER].[wrkord_priority] = ISNULL(@new_wrkord_priority, [TBL_PM_WORK_ORDER].[wrkord_priority]), 
				[TBL_PM_WORK_ORDER].[wrkord_performer] = ISNULL(@new_wrkord_performer, [TBL_PM_WORK_ORDER].[wrkord_performer]), 
				[TBL_PM_WORK_ORDER].[wrkord_state] = ISNULL(@new_wrkord_state, [TBL_PM_WORK_ORDER].[wrkord_state]), 
				[TBL_PM_WORK_ORDER].[wrkord_technical_object_Fk] = ISNULL(@new_wrkord_technical_object_Fk, [TBL_PM_WORK_ORDER].[wrkord_technical_object_Fk]),
				[TBL_PM_WORK_ORDER].[wrkord_scheduled_to] = ISNULL(@new_wrkord_scheduled_to, [TBL_PM_WORK_ORDER].[wrkord_scheduled_to]), 
				[TBL_PM_WORK_ORDER].[wrkord_execution_start_at] = ISNULL(@new_wrkord_execution_start_at, [TBL_PM_WORK_ORDER].[wrkord_execution_start_at]), 
				[TBL_PM_WORK_ORDER].[wrkord_execution_finished_at] = ISNULL(@new_wrkord_execution_finished_at, [TBL_PM_WORK_ORDER].[wrkord_execution_finished_at]),
				[TBL_PM_WORK_ORDER].[wrkord_release_date] = ISNULL(@new_wrkord_release_date, [TBL_PM_WORK_ORDER].[wrkord_release_date]),
				[TBL_PM_WORK_ORDER].[wrkord_planning_group] = ISNULL(@new_wrkord_planning_group, [TBL_PM_WORK_ORDER].[wrkord_planning_group]),
				[TBL_PM_WORK_ORDER].[wrkord_workstation] = ISNULL(@new_wrkord_workstation, [TBL_PM_WORK_ORDER].[wrkord_workstation]),
				[TBL_PM_WORK_ORDER].[wrkord_activity] = ISNULL(@new_wrkord_activity, [TBL_PM_WORK_ORDER].[wrkord_activity])
				
			OUTPUT 
				inserted.[wrkord_id_Pk], 
				inserted.[wrkord_external_identifier], 
				inserted.[wrkord_type], 
				inserted.[wrkord_company], 
				inserted.[wrkord_priority], 
				inserted.[wrkord_performer], 
				inserted.[wrkord_state], 
				inserted.[wrkord_technical_object_Fk],
				inserted.[wrkord_scheduled_to], 
				inserted.[wrkord_execution_start_at], 
				inserted.[wrkord_execution_finished_at],
				inserted.[wrkord_release_date],
				inserted.[wrkord_planning_group],
				inserted.[wrkord_workstation],
				inserted.[wrkord_activity]
			INTO @v_ORK_ORDER
			WHERE 
				[TBL_PM_WORK_ORDER].[wrkord_id_Pk] = ISNULL(@filter_wrkord_id_Pk, [TBL_PM_WORK_ORDER].[wrkord_id_Pk])
				AND [TBL_PM_WORK_ORDER].[wrkord_external_identifier] = ISNULL(@filter_wrkord_external_identifier, [TBL_PM_WORK_ORDER].[wrkord_external_identifier])
				AND [TBL_PM_WORK_ORDER].[wrkord_type] = ISNULL(@filter_wrkord_type, [TBL_PM_WORK_ORDER].[wrkord_type])
				AND [TBL_PM_WORK_ORDER].[wrkord_company] = ISNULL(@filter_wrkord_company, [TBL_PM_WORK_ORDER].[wrkord_company])
				AND [TBL_PM_WORK_ORDER].[wrkord_priority] = ISNULL(@filter_wrkord_priority, [TBL_PM_WORK_ORDER].[wrkord_priority])
				AND [TBL_PM_WORK_ORDER].[wrkord_performer] = ISNULL(@filter_wrkord_performer, [TBL_PM_WORK_ORDER].[wrkord_performer])
				AND [TBL_PM_WORK_ORDER].[wrkord_state] = ISNULL(@filter_wrkord_state, [TBL_PM_WORK_ORDER].[wrkord_state])
				AND [TBL_PM_WORK_ORDER].[wrkord_technical_object_Fk] = ISNULL(@filter_wrkord_technical_object_Fk, [TBL_PM_WORK_ORDER].[wrkord_technical_object_Fk])
				AND [TBL_PM_WORK_ORDER].[wrkord_scheduled_to] = ISNULL(@filter_wrkord_scheduled_to, [TBL_PM_WORK_ORDER].[wrkord_scheduled_to])
				AND [TBL_PM_WORK_ORDER].[wrkord_execution_start_at] = ISNULL(@filter_wrkord_execution_start_at, [TBL_PM_WORK_ORDER].[wrkord_execution_start_at])
				AND [TBL_PM_WORK_ORDER].[wrkord_execution_finished_at] = ISNULL(@filter_wrkord_execution_finished_at, [TBL_PM_WORK_ORDER].[wrkord_execution_finished_at])
				AND [TBL_PM_WORK_ORDER].[wrkord_release_date] = ISNULL(@filter_wrkord_release_date, [TBL_PM_WORK_ORDER].[wrkord_release_date])
				AND [TBL_PM_WORK_ORDER].[wrkord_planning_group] = ISNULL(@filter_wrkord_planning_group, [TBL_PM_WORK_ORDER].[wrkord_planning_group])
				AND [TBL_PM_WORK_ORDER].[wrkord_workstation] = ISNULL(@filter_wrkord_workstation, [TBL_PM_WORK_ORDER].[wrkord_workstation])
				AND [TBL_PM_WORK_ORDER].[wrkord_activity] = ISNULL(@filter_wrkord_activity, [TBL_PM_WORK_ORDER].[wrkord_activity])
				

			SELECT 
				v_wrkord_id_Pk, 
				v_wrkord_external_identifier, 
				v_wrkord_type, 
				v_wrkord_company, 
				v_wrkord_priority, 
				v_wrkord_performer, 
				v_wrkord_state, 
				v_wrkord_scheduled_to, 
				v_wrkord_execution_start_at, 
				v_wrkord_execution_finished_at,
				v_wrkord_release_date,
				v_wrkord_planning_group,
				v_wrkord_workstation, 
				v_wrkord_technical_object_Fk,
				v_wrkord_activity
			FROM @v_ORK_ORDER;
	END
RETURN 0									
