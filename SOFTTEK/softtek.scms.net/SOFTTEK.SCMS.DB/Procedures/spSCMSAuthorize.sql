CREATE PROCEDURE [dbo].[spSCMSAuthorize]
	@device_id nvarchar(100),
	@login nvarchar(20),
	@password nvarchar(100),
	@caller_id nvarchar(100)
AS
	DECLARE @v_token_expiration int;
	DECLARE @v_tokens_count int;
	DECLARE @v_user_id nvarchar(20);
	DECLARE @v_auth_data table( v_id nvarchar(100), v_created datetime, v_expires_at datetime, v_user_id nvarchar(20));
	SET @v_user_id = (SELECT TBL_SCMS_USER.user_id
						FROM TBL_SCMS_USER
						WHERE TBL_SCMS_USER.user_login = @login 
							AND TBL_SCMS_USER.user_password = @password
							AND TBL_SCMS_USER.user_is_active = 1
							);
	
	IF (@v_user_id IS NOT NULL)
		

			
			SET @v_tokens_count = (SELECT COUNT(1) FROM TBL_SCMS_TOKEN
									WHERE TBL_SCMS_TOKEN.token_user_id = @v_user_id
									AND TBL_SCMS_TOKEN.token_expires_at > GETDATE());

			IF (@v_tokens_count = 0)
				BEGIN 
					SET @v_token_expiration = (SELECT TOP 1 CONVERT(INT, TBL_SCMS_PARAM.[value])
									FROM TBL_SCMS_PARAM
									INNER JOIN TBL_SCMS_PARAM_CATEGORY ON TBL_SCMS_PARAM_CATEGORY.id = TBL_SCMS_PARAM.category
									AND TBL_SCMS_PARAM_CATEGORY.name = 'SCMS_TOKEN_VALIDITY');


					INSERT INTO TBL_SCMS_TOKEN (token_id, token_user_id, token_created_by, token_expires_at, token_source_device_id) 
					OUTPUT inserted.token_id, inserted.token_created, inserted.token_expires_at, inserted.token_user_id
						INTO @v_auth_data
					VALUES (NEWID(), @v_user_id, @caller_id, DATEADD (second , @v_token_expiration , GETDATE()), @device_id);

					SELECT v_id, v_created, v_expires_at, v_user_id 
					FROM @v_auth_data;
				END
			ELSE
				SELECT TBL_SCMS_TOKEN.token_id as v_id, TBL_SCMS_TOKEN.token_created as v_created, TBL_SCMS_TOKEN.token_expires_at as v_expires_at, TBL_SCMS_TOKEN.token_user_id as v_user_id
					FROM TBL_SCMS_TOKEN
				WHERE TBL_SCMS_TOKEN.token_user_id = @v_user_id
					AND TBL_SCMS_TOKEN.token_expires_at > GETDATE();

RETURN 0