CREATE PROCEDURE [dbo].[spSRAGetEmployeeByToken]
	@token_id nvarchar(50)
AS
	SELECT 
		[TBL_SRA_EMPLOYEE].id,
		[TBL_SRA_EMPLOYEE].area, 
		[TBL_SRA_EMPLOYEE].[role], 
		[TBL_SRA_PERSON].id as "person_id", 
		[TBL_SRA_PERSON].identification, 
		[TBL_SRA_PERSON].name, 
		[TBL_SRA_PERSON].middle_name, 
		[TBL_SRA_PERSON].last_name,
		supervisor.id as "supervisor_id",
		supervisor.area as "supervisor_area",
		supervisor.[role] as "supervisor_role",
		supervisor_person.id as "supervisor_person_id",
		supervisor_person.name as "supervisor_name",
		supervisor_person.middle_name as "supervisor_middle_name",
		supervisor_person.last_name as "supervisor_last_name",
		[TBL_SRA_EMPLOYEE].image_url
	FROM [TBL_SRA_EMPLOYEE]
	INNER JOIN [TBL_SRA_PERSON] ON [TBL_SRA_PERSON].id = [TBL_SRA_EMPLOYEE].person
	INNER JOIN [TBL_SCMS_USER] ON [TBL_SCMS_USER].[user_id] = [TBL_SRA_EMPLOYEE].[user]
	INNER JOIN [TBL_SRA_EMPLOYEE] supervisor ON supervisor.id = [TBL_SRA_EMPLOYEE].supervisor
	INNER JOIN [TBL_SRA_PERSON] supervisor_person on supervisor_person.id = supervisor.person
	INNER JOIN [TBL_SCMS_TOKEN] ON [TBL_SCMS_TOKEN].token_user_id = [TBL_SCMS_USER].[user_id]
		AND [TBL_SCMS_TOKEN].token_id = @token_id
		AND [TBL_SCMS_TOKEN].token_expires_at > GETDATE();
RETURN 0
