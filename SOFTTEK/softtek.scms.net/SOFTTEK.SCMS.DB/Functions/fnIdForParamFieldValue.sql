CREATE FUNCTION [dbo].[fnIdForParamFieldValue]
(
	@param_value NVARCHAR(MAX),
	@param_category NVARCHAR(50)
)
RETURNS INT
AS
BEGIN
	DECLARE @v_param_id int;
	SELECT @v_param_id = [TBL_SCMS_PARAM].[id]
	FROM [TBL_SCMS_PARAM] 
		INNER JOIN [TBL_SCMS_PARAM_CATEGORY] ON [TBL_SCMS_PARAM_CATEGORY].id = [TBL_SCMS_PARAM].category 
			AND [TBL_SCMS_PARAM_CATEGORY].[name] = @param_category 
	WHERE [TBL_SCMS_PARAM].[value] = @param_value;

	IF @v_param_id IS NOT NULL
		RETURN @v_param_id;
	ELSE
		RETURN 'Error'
		--RAISERROR('Value not found', 16, 1);
	RETURN -1;
END