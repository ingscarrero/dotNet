CREATE PROCEDURE [dbo].[spSRAInsertPermitsAndAbsences]
	@token_id NVARCHAR(50),
	@perabs_activity_code INT, 
    @perabs_start_at DATETIME, 
    @perabs_end_at DATETIME, 
    @perabs_total_hours NUMERIC(3, 1), 
    @perabs_description NVARCHAR(MAX), 
    @perabs_created_by NVARCHAR(20), 
    @perabs_employee INT
AS
--BEGIN TRY	
	DECLARE @v_default_activity_status CHAR;
	DECLARE @v_token_employee_id int;
	DECLARE @v_token_employee_user nvarchar(20);

	SELECT @v_default_activity_status = SUBSTRING(TBL_SCMS_PARAM.value, 1, 1)
	FROM [TBL_SCMS_PARAM] 
		INNER JOIN [TBL_SCMS_PARAM_CATEGORY] ON [TBL_SCMS_PARAM_CATEGORY].id = [TBL_SCMS_PARAM].category
			AND [TBL_SCMS_PARAM_CATEGORY].name = 'SRA_PERMITS_AND_ABSENCES'
	WHERE [TBL_SCMS_PARAM].comments = 'DEFAULT';

	SELECT @v_token_employee_id = [TBL_SRA_EMPLOYEE].id,
		@v_token_employee_user = [TBL_SRA_EMPLOYEE].[user]
	FROM [TBL_SRA_EMPLOYEE]
		INNER JOIN [TBL_SCMS_USER] ON [TBL_SCMS_USER].[user_id] = [TBL_SRA_EMPLOYEE].[user]
		INNER JOIN [TBL_SCMS_TOKEN] ON [TBL_SCMS_TOKEN].token_user_id = [TBL_SCMS_USER].[user_id]
			AND [TBL_SCMS_TOKEN].token_id = @token_id
			AND [TBL_SCMS_TOKEN].token_expires_at > GETDATE()

	IF @v_default_activity_status IS NOT NULL AND @v_token_employee_id IS NOT NULL
		BEGIN
		
		IF(convert(date ,@perabs_start_at) = convert(date ,GETDATE()))
		Begin
			 RAISERROR ('La fecha de solicitud de permiso o ausencia no puede ser la fecha actual.', -- Message text.
               16, -- Severity.
               1 -- State.
               );
		End
	Else
		Begin
			Insert Into TBL_SRA_PERMITS_AND_ABSENCES([perabs_activity_code], [perabs_start_at], [perabs_end_at], 
			[perabs_total_hours], [perabs_description], [perabs_created_by],
			 [perabs_employee])
			Values(@perabs_activity_code, @perabs_start_at, @perabs_end_at, @perabs_total_hours, 
			@perabs_description, @perabs_created_by,  @perabs_employee)

		
		End
		Select * from TBL_SRA_PERMITS_AND_ABSENCES
		where [perabs_employee] = @perabs_employee
	END

	RETURN 0
--END TRY
--BEGIN CATCH
--	DECLARE @ErrorMessage NVARCHAR(4000);
--    DECLARE @ErrorSeverity INT;
--    DECLARE @ErrorState INT;

--    SELECT 
--        @ErrorMessage = ERROR_MESSAGE(),
--        @ErrorSeverity = ERROR_SEVERITY(),
--        @ErrorState = ERROR_STATE();

--		RAISERROR (@ErrorMessage, -- Message text.
--               @ErrorSeverity, -- Severity.
--               @ErrorState -- State.
--               );
--END CATCH;
	