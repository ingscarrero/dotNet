CREATE PROCEDURE [dbo].[spSRAUpdateEmployeeActivity]
	@token_id NVARCHAR(50),
	@activity_id BIGINT,
	@activity_project NVARCHAR(20), 
    @activity_code NVARCHAR(10), 
    @activity_description NVARCHAR(500), 
    @activity_effort NUMERIC(3, 1), 
	@activity_status char
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
	
	DECLARE @v_approval_activity_status CHAR;
	DECLARE @v_token_employee_user nvarchar(20);

	SELECT @v_approval_activity_status = SUBSTRING(TBL_SCMS_PARAM.value, 1, 1)
	FROM [TBL_SCMS_PARAM] 
		INNER JOIN [TBL_SCMS_PARAM_CATEGORY] ON [TBL_SCMS_PARAM_CATEGORY].id = [TBL_SCMS_PARAM].category
			AND [TBL_SCMS_PARAM_CATEGORY].name = 'SRA_ACTIVITY_STATUS'
	WHERE [TBL_SCMS_PARAM].comments = 'APPROVAL';

	SELECT @v_token_employee_user = [TBL_SCMS_USER].[user_id]
	FROM [TBL_SCMS_USER]
		INNER JOIN [TBL_SCMS_TOKEN] ON [TBL_SCMS_TOKEN].token_user_id = [TBL_SCMS_USER].[user_id]
			AND [TBL_SCMS_TOKEN].token_id = @token_id
			AND [TBL_SCMS_TOKEN].token_expires_at > GETDATE()
		
	IF @v_approval_activity_status IS NOT NULL AND @v_token_employee_user IS NOT NULL
		BEGIN
			UPDATE TBL_SRA_ACTIVITY
			SET activity_project = ISNULL(@activity_project, activity_project), 
				activity_code = ISNULL(@activity_code, activity_code), 
				activity_description = ISNULL(@activity_description, activity_description), 
				activity_effort = ISNULL(@activity_effort, activity_effort),
				activity_status = ISNULL(@activity_status, activity_status),
				activity_modified_by = @v_token_employee_user,
				activity_modified_at = GETDATE(),
				activity_validated_at = CASE WHEN @activity_status = @v_approval_activity_status THEN GETDATE() ELSE activity_validated_at END,
				activity_validated_by = CASE WHEN @activity_status = @v_approval_activity_status THEN @v_token_employee_user ELSE activity_validated_by END
			OUTPUT 
				inserted.activity_id, 
				inserted.activity_code,
				inserted.activity_project, 
				inserted.activity_description, 
				inserted.activity_effort, 
				inserted.activity_employee, 
				inserted.activity_executed_at, 
				inserted.activity_jornade_type, 
				inserted.activity_status 
				INTO @v_parameter
			WHERE 
				[TBL_SRA_ACTIVITY].activity_id = @activity_id
		END

		SELECT 
			v_id, 
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
