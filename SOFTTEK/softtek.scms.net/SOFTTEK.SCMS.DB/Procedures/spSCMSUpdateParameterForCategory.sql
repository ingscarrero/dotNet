CREATE PROCEDURE [dbo].[spSCMSUpdateParameterForCategory]
	@token_id nvarchar(50),
	@param_category nvarchar(50),
	@param_id int,
	@param_external_id nvarchar(50),
	@param_value nvarchar(MAX),
	@param_description nvarchar(250) = NULL,
	@param_comments nvarchar(250) = NULL,
	@param_order int = NULL,
	@param_is_active bit = NULL
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
		v_order int, 
		v_is_active bit);
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
					UPDATE TBL_SCMS_PARAM
					SET value = @param_value,
						category = @v_parameter_category_id,
						[description] = ISNULL(@param_description, [description]),
						[comments] = ISNULL(@param_comments, [comments]),
						[external_id] = @param_external_id,
						[order] = ISNULL(@param_order, [order]),
						is_active = ISNULL(@param_is_active, is_active),
						modified_by = @v_token_user
					OUTPUT 
						inserted.id, 
						inserted.value, 
						inserted.[description], 
						inserted.[comments],
						inserted.external_id,
						inserted.category, 
						inserted.[order], 
						inserted.is_active 
					INTO 
						@v_parameter
					WHERE 
						TBL_SCMS_PARAM.category = @v_parameter_category_id
						AND TBL_SCMS_PARAM.id = @param_id;

					SELECT 
						v_id, 
						v_value, 
						v_description, 
						v_comments, 
						v_external_id, 
						v_category_id,
						v_order, 
						v_is_active 
					FROM @v_parameter;
				END

RETURN 0
