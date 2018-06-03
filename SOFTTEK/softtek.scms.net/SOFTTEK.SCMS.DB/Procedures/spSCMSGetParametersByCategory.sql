CREATE PROCEDURE [dbo].[spSCMSGetParametersByCategory]
	@token_id nvarchar(50),
	@param_category nvarchar(50)
AS
	SELECT 
		TBL_SCMS_PARAM.[id], 
		TBL_SCMS_PARAM.[value], 
		TBL_SCMS_PARAM.category as "category_id",
		TBL_SCMS_PARAM_CATEGORY.name as "category_name",
		TBL_SCMS_PARAM_TYPE.name as "type", 
		TBL_SCMS_PARAM.[description],
		TBL_SCMS_PARAM.[comments],
		TBL_SCMS_PARAM.external_id, 
		TBL_SCMS_PARAM.[order], 
		TBL_SCMS_PARAM.[is_active]
	FROM TBL_SCMS_TOKEN, TBL_SCMS_PARAM 
		INNER JOIN TBL_SCMS_PARAM_CATEGORY ON TBL_SCMS_PARAM.category = TBL_SCMS_PARAM_CATEGORY.id
		INNER JOIN TBL_SCMS_PARAM_TYPE ON TBL_SCMS_PARAM_TYPE.id = TBL_SCMS_PARAM_CATEGORY.[type]
	WHERE
		TBL_SCMS_TOKEN.token_id = @token_id 
		AND TBL_SCMS_PARAM.is_active = 1 
		AND TBL_SCMS_PARAM_CATEGORY.name = @param_category
	ORDER BY TBL_SCMS_PARAM.[order], TBL_SCMS_PARAM.value 
RETURN 0
