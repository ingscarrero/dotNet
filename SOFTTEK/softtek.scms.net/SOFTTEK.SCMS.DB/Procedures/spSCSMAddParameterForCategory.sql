CREATE PROCEDURE [dbo].[spSCSMAddParameterForCategory]
	@token_id nvarchar(50),
	@param_category nvarchar(50),
	@param_value nvarchar(MAX),
	@param_description nvarchar(250),
	@param_comments nvarchar(250),
	@param_external_id nvarchar(50) = NULL,
	@param_order int = 0
AS
	DECLARE @v_token_user nvarchar(20);
	DECLARE @v_token_employee_id int;
	DECLARE @v_parameter table( 
		v_id int, 
		v_value nvarchar(MAX), 
		v_description nvarchar(250), 
		v_comments nvarchar(250),
		v_external_id nvarchar(50), 
		v_category_id int, 
		v_order int);
	DECLARE @v_parameter_category_id int;
	DECLARE @v_parameter_category_type nvarchar(50);

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
	
			SELECT @v_parameter_category_id = TBL_SCMS_PARAM_CATEGORY.id, 
					@v_parameter_category_type = TBL_SCMS_PARAM_TYPE.name
					FROM TBL_SCMS_PARAM_CATEGORY
					INNER JOIN TBL_SCMS_PARAM_TYPE ON TBL_SCMS_PARAM_TYPE.id = TBL_SCMS_PARAM_CATEGORY.[type] 
					WHERE TBL_SCMS_PARAM_CATEGORY.name = @param_category;

			IF @v_parameter_category_id IS NOT NULL
				BEGIN
					INSERT INTO TBL_SCMS_PARAM
					(value, category, [description], [comments], [external_id], [order], is_active, created_by)
					OUTPUT 
						inserted.id, 
						inserted.value, 
						inserted.[description], 
						inserted.[comments], 
						inserted.external_id, 
						inserted.category,
						inserted.[order] 
						INTO @v_parameter
					VALUES (@param_value, @v_parameter_category_id, @param_description, @param_comments, @param_external_id, @param_order, 1, @v_token_user);
			
					SELECT 
						v_id, 
						v_value, 
						v_description, 
						v_comments, 
						v_external_id, 
						v_category_id,
						v_order 
					FROM @v_parameter;
				END
RETURN 0
