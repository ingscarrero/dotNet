CREATE PROCEDURE [dbo].[spSCMSGetMobileAppMenuItems]
	@token_id nvarchar(50),
	@app_id nvarchar(50)
AS
SELECT 
TBL_SCMS_VIEW.name as "item_view", 
		TBL_SCMS_MOBILE_MENU.item_caption,
		TBL_SCMS_MOBILE_MENU.item_title,
		TBL_SCMS_MOBILE_MENU.item_image_url,
		TBL_SCMS_MOBILE_MENU.item_id,
		TBL_SCMS_VIEWS_BY_PROFILE.can_read,
		TBL_SCMS_VIEWS_BY_PROFILE.can_insert,
		TBL_SCMS_VIEWS_BY_PROFILE.can_update,
		TBL_SCMS_VIEWS_BY_PROFILE.can_delete
 FROM TBL_SCMS_MOBILE_MENU
	INNER JOIN TBL_SCMS_VIEW ON TBL_SCMS_VIEW.id = TBL_SCMS_MOBILE_MENU.item_view
							AND TBL_SCMS_VIEW.is_enabled = 1
                            AND ISNULL(TBL_SCMS_VIEW.approved_at, 0) <> 0
                            AND TBL_SCMS_VIEW.approved_at < GETDATE()
	INNER JOIN TBL_SCMS_TOKEN ON TBL_SCMS_TOKEN.token_id = @token_id AND TBL_SCMS_TOKEN.token_expires_at > GETDATE() 
    INNER JOIN TBL_SCMS_USER ON TBL_SCMS_USER.user_id = TBL_SCMS_TOKEN.token_user_id AND TBL_SCMS_USER.user_is_active = 1
    INNER JOIN TBL_SCMS_USERS_BY_ROLE ON TBL_SCMS_USERS_BY_ROLE.[user] = TBL_SCMS_TOKEN.token_user_id
    INNER JOIN TBL_SCMS_ROLE ON TBL_SCMS_ROLE.app = @app_id AND TBL_SCMS_ROLE.id = TBL_SCMS_USERS_BY_ROLE.role AND TBL_SCMS_ROLE.is_active = 1
    INNER JOIN TBL_SCMS_PROFILES_BY_ROLE ON TBL_SCMS_USERS_BY_ROLE.[role] = TBL_SCMS_PROFILES_BY_ROLE.[role]
	INNER JOIN TBL_SCMS_PROFILE ON TBL_SCMS_PROFILE.app = @app_id AND TBL_SCMS_PROFILE.id = TBL_SCMS_PROFILES_BY_ROLE.[profile] AND TBL_SCMS_PROFILE.is_active = 1
	INNER JOIN TBL_SCMS_VIEWS_BY_PROFILE ON TBL_SCMS_VIEWS_BY_PROFILE.[profile] = TBL_SCMS_PROFILE.id
	INNER JOIN TBL_SCMS_VIEW vp ON vp.id = TBL_SCMS_VIEWS_BY_PROFILE.[view] AND vp.id = TBL_SCMS_VIEW.id
	INNER JOIN TBL_SCMS_APP ON TBL_SCMS_APP.id = TBL_SCMS_VIEW.app
                            AND TBL_SCMS_APP.id = @app_id
                            AND TBL_SCMS_APP.is_published = 1
                            AND ISNULL(TBL_SCMS_APP.approved_at, 0) <> 0
							AND TBL_SCMS_APP.approved_at < GETDATE();
RETURN 0
