CREATE FUNCTION [dbo].[fnCheckValueForParamField]
(
	@param_value NVARCHAR(MAX),
	@param_category NVARCHAR(50)
)
RETURNS INT
AS
BEGIN
	IF EXISTS(SELECT [TBL_SCMS_PARAM].[value]
				FROM [TBL_SCMS_PARAM] 
					INNER JOIN [TBL_SCMS_PARAM_CATEGORY] ON [TBL_SCMS_PARAM_CATEGORY].id = [TBL_SCMS_PARAM].category 
						AND [TBL_SCMS_PARAM_CATEGORY].[name] = @param_category 
				WHERE [TBL_SCMS_PARAM].[value] = @param_value)
		RETURN 1
	RETURN 0
END
