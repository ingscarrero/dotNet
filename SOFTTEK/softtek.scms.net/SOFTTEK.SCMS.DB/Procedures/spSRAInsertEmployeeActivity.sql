CREATE PROCEDURE [dbo].[spSRAInsertEmployeeActivity]
    @token_id NVARCHAR(50),
	@activity_project NVARCHAR(20), 
    @activity_code NVARCHAR(10), 
    @activity_description NVARCHAR(500), 
    @activity_effort NUMERIC(3, 1),  
    @activity_executed_at DATETIME, 
    @activity_jornade_type NVARCHAR(20)
AS
	DECLARE @v_parameter table( 
		v_id BIGINT, 
		v_project NVARCHAR(20), 
		v_code NVARCHAR(10), 
		v_description NVARCHAR(500), 
		v_effort NUMERIC(3, 1),
		v_employee INT,
		v_executed_at DATETIME,
		v_jornade_type NVARCHAR(20), 
		v_status CHAR
	);
	
	DECLARE @v_default_activity_status CHAR;
	DECLARE @v_token_employee_id int;
	DECLARE @v_token_employee_user nvarchar(20);

	SELECT @v_default_activity_status = SUBSTRING(TBL_SCMS_PARAM.value, 1, 1)
	FROM [TBL_SCMS_PARAM] 
		INNER JOIN [TBL_SCMS_PARAM_CATEGORY] ON [TBL_SCMS_PARAM_CATEGORY].id = [TBL_SCMS_PARAM].category
			AND [TBL_SCMS_PARAM_CATEGORY].name = 'SRA_ACTIVITY_STATUS'
	WHERE [TBL_SCMS_PARAM].comments = 'DEFAULT';

	SELECT @v_token_employee_id = [TBL_SRA_EMPLOYEE].id,
		@v_token_employee_user = [TBL_SRA_EMPLOYEE].[user]
	FROM [TBL_SRA_EMPLOYEE]
		INNER JOIN [TBL_SCMS_USER] ON [TBL_SCMS_USER].[user_id] = [TBL_SRA_EMPLOYEE].[user]
		INNER JOIN [TBL_SCMS_TOKEN] ON [TBL_SCMS_TOKEN].token_user_id = [TBL_SCMS_USER].[user_id]
			AND [TBL_SCMS_TOKEN].token_id = @token_id
			AND [TBL_SCMS_TOKEN].token_expires_at > GETDATE()
		
	IF @v_default_activity_status IS NOT NULL AND @v_token_employee_id IS NOT NULL
			
		BEGIN
			INSERT INTO TBL_SRA_ACTIVITY (
				activity_project, 
				activity_code, 
				activity_description, 
				activity_effort, 
				activity_employee, 
				activity_executed_at, 
				activity_jornade_type, 
				activity_status, 
				activity_created_by
			)
			OUTPUT inserted.activity_id, inserted.activity_code, inserted.activity_project, inserted.activity_description, inserted.activity_effort, inserted.activity_employee, inserted.activity_executed_at, inserted.activity_jornade_type, inserted.activity_status 
				INTO @v_parameter
			VALUES (
				@activity_project, 
				@activity_code, 
				@activity_description, 
				@activity_effort, 
				@v_token_employee_id, 
				@activity_executed_at, 
				@activity_jornade_type, 
				@v_default_activity_status, 
				@v_token_employee_user
			)
				
		END

		SELECT v_id, 
			v_project, 
			v_code, 
			v_description, 
			v_effort,
			v_employee,
			v_executed_at,
			v_jornade_type, 
			v_status
		FROM @v_parameter;
RETURN 0
