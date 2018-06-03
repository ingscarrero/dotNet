CREATE PROCEDURE [dbo].[spSRAGetEmployeeActivitiesInPeriod]
	@token_id nvarchar(50),
	@employee_id int,
	@activities_from datetime,
	@activities_to datetime
AS
	SELECT 
		[TBL_SRA_ACTIVITY].activity_code,
		[TBL_SRA_ACTIVITY].activity_effort,
		[TBL_SRA_ACTIVITY].activity_description,
		[TBL_SRA_ACTIVITY].activity_employee,
		[TBL_SRA_ACTIVITY].activity_executed_at,
		[TBL_SRA_ACTIVITY].activity_reported_at,
		[TBL_SRA_ACTIVITY].activity_validated_at,
		[TBL_SRA_ACTIVITY].activity_id,
		[TBL_SRA_ACTIVITY].activity_project,
		[TBL_SRA_ACTIVITY].activity_status,
		[TBL_SRA_ACTIVITY].activity_jornade_type
	FROM [TBL_SRA_ACTIVITY]
	INNER JOIN [TBL_SRA_EMPLOYEE] employee ON employee.id = [TBL_SRA_ACTIVITY].activity_employee
	INNER JOIN [TBL_SRA_PERSON] ON [TBL_SRA_PERSON].id = employee.person
	INNER JOIN [TBL_SRA_EMPLOYEE] supervisor ON supervisor.id = employee.supervisor
	INNER JOIN [TBL_SRA_PERSON] supervisor_person on supervisor_person.id = supervisor.person
	INNER JOIN [TBL_SCMS_TOKEN] ON [TBL_SCMS_TOKEN].token_id = @token_id
		AND [TBL_SCMS_TOKEN].token_expires_at > GETDATE()
	WHERE 
		[TBL_SRA_ACTIVITY].activity_employee = @employee_id
		AND [TBL_SRA_ACTIVITY].activity_executed_at BETWEEN @activities_from AND @activities_to;
RETURN 0
