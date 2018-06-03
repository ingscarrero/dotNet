CREATE PROCEDURE [dbo].[spSCMSCreateUser]
	@device_id nvarchar(100),
	@user_id nvarchar(20),
	@login nvarchar(20),
	@password nvarchar(100),
	@caller_id nvarchar(100)
AS
	DECLARE @v_token_expiration int;
	DECLARE @v_user_id nvarchar(20);
	DECLARE @v_auth_data table( v_id nvarchar(100), v_created datetime, v_expires_at datetime, v_user_id nvarchar(20));
	

	BEGIN
		INSERT INTO TBL_SCMS_USER (user_id, user_login, user_password, user_created_by)
		VALUES (@user_id, @login, @password, @device_id)
	END
	
	BEGIN 
		
		SET @v_token_expiration = (SELECT TOP 1 CONVERT(INT, TBL_SCMS_PARAM.[value])
									FROM TBL_SCMS_PARAM
									INNER JOIN TBL_SCMS_PARAM_CATEGORY ON TBL_SCMS_PARAM_CATEGORY.id = TBL_SCMS_PARAM.category
									AND TBL_SCMS_PARAM_CATEGORY.name = 'SCMS_TOKEN_EXPIRATION');

		INSERT INTO TBL_SCMS_TOKEN (token_id, token_user_id, token_created_by, token_expires_at, token_source_device_id) 
		OUTPUT inserted.token_id, inserted.token_created, inserted.token_expires_at, inserted.token_user_id
			INTO @v_auth_data
		VALUES (NEWID(), @user_id, @caller_id, DATEADD (second , @v_token_expiration , GETDATE()), @device_id);

		SELECT v_id, v_created, v_expires_at, v_user_id 
		FROM @v_auth_data;
	END
RETURN 0