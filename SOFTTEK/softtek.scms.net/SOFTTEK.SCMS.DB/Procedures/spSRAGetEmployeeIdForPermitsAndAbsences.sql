CREATE PROCEDURE [dbo].[spSRAGetEmployeeIdForPermitsAndAbsences]
	@employee_id int
AS
	SELECT * from TBL_SRA_PERMITS_AND_ABSENCES
	where perabs_employee = @employee_id
RETURN 1
