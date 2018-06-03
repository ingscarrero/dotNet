CREATE PROCEDURE [dbo].[spSCMSGetToken]
	@device_id nvarchar(100),
	@token_id nvarchar(100) = 0
AS
	SELECT TBL_SCMS_TOKEN.token_id, TBL_SCMS_TOKEN.token_created, TBL_SCMS_TOKEN.token_expires_at, TBL_SCMS_TOKEN.token_user_id
	FROM TBL_SCMS_TOKEN
	WHERE TBL_SCMS_TOKEN.token_source_device_id = @device_id 
		AND TBL_SCMS_TOKEN.token_id = @token_id 
		AND TBL_SCMS_TOKEN.token_expires_at > GETDATE()
RETURN 0
