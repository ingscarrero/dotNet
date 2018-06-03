CREATE PROCEDURE [dbo].[spSCMSAddParameterCategory]
	@category_name nvarchar(50),
	@category_description nvarchar(MAX),
	@param_type_name nvarchar(50),
	@category_created_by nvarchar(20)
AS

	DECLARE @v_parameter_category table( v_id int, v_name nvarchar(50), v_description nvarchar(MAX), v_type int);
	DECLARE @v_param_type int;

	SET @v_param_type = (SELECT TBL_SCMS_PARAM_TYPE.id 
						FROM TBL_SCMS_PARAM_TYPE 
						WHERE TBL_SCMS_PARAM_TYPE.name = @param_type_name);

	IF (@v_param_type IS NOT NULL)
		BEGIN
			INSERT INTO TBL_SCMS_PARAM_CATEGORY
			(name, [description], [type], created_by)
			OUTPUT inserted.id, inserted.name, inserted.[description], inserted.[type] 
				INTO @v_parameter_category
			VALUES (@category_name, @category_description, @v_param_type, @category_created_by)

			SELECT v_id, v_name, v_description, v_type
			FROM @v_parameter_category;
		END

RETURN 0
