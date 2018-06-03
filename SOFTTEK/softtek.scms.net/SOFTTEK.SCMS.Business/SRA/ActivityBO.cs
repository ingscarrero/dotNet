using SOFTTEK.SCMS.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFTTEK.SCMS.Business.SRA
{
    public class ActivityBO : SOFTTEK.SCMS.Foundation.Business.BusinessObject
    {
        #region Constants
        const int DAYS_PER_WEEK = 7;
        const int MAX_EFFORT_PER_DAY = 8;
        const int LABOR_DAYS_PER_WEEK = 5;
        const string APPROVED_ACTIVITY_STATE = "A";
        const string DISABLED_ACTIVITY_STATE = "D";

        #endregion

        #region Fields

        private SRADataContext dataSource;

        #endregion

        #region Constructors
        public ActivityBO(SOFTTEK.SCMS.Foundation.Business.BusinessContext ctx)
            : base(ctx)
        {
        }

        #endregion

        #region Public Functionalities
        /// <summary>
        /// Get the range/2 previous and the range/2 fallowings weeks, by using the Iso8601 Week Of Year calculation  
        /// </summary>
        /// <returns>List of week entities</returns>
        public List<Entity.SRA.Week<T>> GetAvailableWeeksInformationForEmployee<T>(int employeeID)
            where T : SOFTTEK.SCMS.Entity.SRA.Activity, new()
        {

            DateTime firstThursday;
            int range = 4;
            int startingWeek = GetStartingWeekNumberInRangeForCurrentWeek(range, out firstThursday);

            

            return context.Execute(() =>
            {

                List<Entity.SRA.Week<T>> weeks = new List<Entity.SRA.Week<T>>();
                for (int i = 0; i < range; i++)
                {
                    Entity.SRA.Week<T> week = new Entity.SRA.Week<T>();
                    week.From = firstThursday.AddDays((startingWeek + i) * DAYS_PER_WEEK).AddDays(-(int)DayOfWeek.Thursday + 1);
                    week.To = week.From.AddDays(DAYS_PER_WEEK - 1);
                    week.Number = startingWeek + i;
                    week.ReportedActivities = GetActivitiesForEmployeeIdInPeriod<T>(employeeID, week.From, week.To);
                    weeks.Add(week);
                }

                return weeks;
            }, "Retrive previous 2 weeks, and next 2 weeks for the week corresponding to the current day, and get the registered activities for the provided employee identifier.");
        }

        /// <summary>
        /// Get the available incharge employee´s activities pending for approval
        /// </summary>
        /// <param name="employeeID">Approver Employee Identifier</param>
        /// <param name="projectID">Activities target project</param>
        /// <returns>List if the activities to be approved,</returns>
        public List<T> GetActivitiesToApprove<T>(int employeeID, string projectID)
            where T : SOFTTEK.SCMS.Entity.SRA.Activity, new()
        {
            List<T> activitiesToApprove = new List<T>();

            return context.Execute(() =>
            {

                using (dataSource = new SRADataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var results = dataSource.GetActivitiesToApprove(employeeID, projectID);

                    results.ForEach(a => activitiesToApprove.Add(new T
                    {
                        ActivityCode = a.ActivityCode,
                        ApprovedAt = a.ApprovedAt,
                        ApprovedBy = a.ApprovedBy,
                        ApprovedComments = a.ApprovedComments,
                        Details = a.Details,
                        Effort = a.Effort,
                        Employee = a.Employee,
                        ExecutedAt = a.ExecutedAt,
                        Identifier = a.Identifier,
                        Jornade = a.Jornade,
                        Project = a.Project,
                        ReportedAt = a.ReportedAt,
                        State = a.State
                    }));
                }
                return activitiesToApprove;
            }, "Retrieve the incharge employee´s activities to approve");
        }

        /// <summary>
        /// Register an activity for approval.
        /// </summary>
        /// <param name="activity">Activity information</param>
        /// <returns>Registered activity information.</returns>
        public T RegisterActivity<T>(SOFTTEK.SCMS.Entity.SRA.Activity activity)
            where T : SOFTTEK.SCMS.Entity.SRA.Activity, new()
        {
            T registeredActivity = new T();

            ValidateReportedActivityEffort(activity);
            ValidateReportedActivityInformation(activity);

            return context.Execute(() =>
            {

                using (dataSource = new SRADataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var result = dataSource.RegisterActivity(activity);

                    registeredActivity = new T
                    {
                        ActivityCode = result.ActivityCode,
                        ApprovedAt = result.ApprovedAt,
                        ApprovedBy = result.ApprovedBy,
                        ApprovedComments = result.ApprovedComments,
                        Details = result.Details,
                        Effort = result.Effort,
                        Employee = result.Employee,
                        ExecutedAt = result.ExecutedAt,
                        Identifier = result.Identifier,
                        Jornade = result.Jornade,
                        Project = result.Project,
                        ReportedAt = result.ReportedAt,
                        State = result.State
                    };
                }
                return registeredActivity;
            }, "Register an activity for approval");
        }

        /// <summary>
        /// Change a non approved registered activity.
        /// </summary>
        /// <param name="activity">Activity information</param>
        /// <returns>Registered activity information.</returns>
        public T ChangeActivity<T>(long activityID, SOFTTEK.SCMS.Entity.SRA.Activity activity)
            where T : SOFTTEK.SCMS.Entity.SRA.Activity, new()
        {

            T changedActivity =
            context.Execute(() =>
            {

                T targetActivity = new T();
                using (dataSource = new SRADataContext(context.SecurityContext))
                {

                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var result = dataSource.GetActivityByID(activityID);
                    targetActivity = new T
                    {
                        ActivityCode = result.ActivityCode,
                        ApprovedAt = result.ApprovedAt,
                        ApprovedBy = result.ApprovedBy,
                        ApprovedComments = result.ApprovedComments,
                        Details = result.Details,
                        Effort = result.Effort,
                        Employee = result.Employee,
                        ExecutedAt = result.ExecutedAt,
                        Identifier = result.Identifier,
                        Jornade = result.Jornade,
                        Project = result.Project,
                        ReportedAt = result.ReportedAt,
                        State = result.State
                    };
                }
                return targetActivity;
            }, "Retrieve information for an activity to be updated");

            ValidateModifiableActivityState(changedActivity);

            changedActivity.Effort = activity.Effort;
            changedActivity.Details = activity.Details;
            changedActivity.ActivityCode = activity.ActivityCode;
            changedActivity.Project = activity.Project;
            changedActivity.Details = activity.Details;

            changedActivity = UpdateActivity<T>(activity);

            return changedActivity;
        }

        /// <summary>
        /// Approve a non approved registede activity.
        /// </summary>
        /// <param name="activityID">Activity Identifier</param>
        /// <returns></returns>
        public T ApproveActivity<T>(long activityID, SOFTTEK.SCMS.Entity.Shared.Employee approver, string comments)
            where T : SOFTTEK.SCMS.Entity.SRA.Activity, new()
        {

            T changedActivity =
            context.Execute(() =>
            {

                T targetActivity = new T();
                using (dataSource = new SRADataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var result = dataSource.GetActivityByID(activityID);

                    targetActivity = new T
                    {
                        ActivityCode = result.ActivityCode,
                        ApprovedAt = result.ApprovedAt,
                        ApprovedBy = result.ApprovedBy,
                        ApprovedComments = result.ApprovedComments,
                        Details = result.Details,
                        Effort = result.Effort,
                        Employee = result.Employee,
                        ExecutedAt = result.ExecutedAt,
                        Identifier = result.Identifier,
                        Jornade = result.Jornade,
                        Project = result.Project,
                        ReportedAt = result.ReportedAt,
                        State = result.State
                    };
                }
                return targetActivity;
            }, "Retrieve information for an activity to be updated");

            ValidateModifiableActivityState(changedActivity);
            ValidateApproval(changedActivity, approver);

            changedActivity.State = APPROVED_ACTIVITY_STATE;
            changedActivity.ApprovedAt = DateTime.Now;
            changedActivity.ApprovedComments = comments;

            changedActivity = UpdateActivity<T>(changedActivity);

            return changedActivity;
        }

        /// <summary>
        /// Get all the activities reported by an employee in a period.
        /// </summary>
        /// <param name="employeeId">Employee Identifier</param>
        /// <param name="from">Period start date</param>
        /// <param name="to">Period end date</param>
        /// <returns></returns>
        public List<T> GetActivitiesForEmployeeIdInPeriod<T>(int employeeId, DateTime from, DateTime to)
            where T : SOFTTEK.SCMS.Entity.SRA.Activity, new()
        {
            List<T> activities = new List<T>();

            return context.Execute(() =>
            {


                using (dataSource = new SRADataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var results = dataSource.GetActivities(employeeId, from, to);

                    results.ForEach(a => activities.Add(new T
                    {
                        ActivityCode = a.ActivityCode,
                        ApprovedAt = a.ApprovedAt,
                        ApprovedBy = a.ApprovedBy,
                        ApprovedComments = a.ApprovedComments,
                        Details = a.Details,
                        Effort = a.Effort,
                        Employee = a.Employee,
                        ExecutedAt = a.ExecutedAt,
                        Identifier = a.Identifier,
                        Jornade = a.Jornade,
                        Project = a.Project,
                        ReportedAt = a.ReportedAt,
                        State = a.State
                    }));
                }

                return activities;
            }, "Retrieve the registered activities for the employee corresponding to the provided employee identifier.");

        }

        #endregion

        #region Private Functionalities

        /// <summary>
        /// Update an registered activity.
        /// </summary>
        /// <param name="activity">Activity information</param>
        /// <returns>Registered activity information.</returns>
        private T UpdateActivity<T>(SOFTTEK.SCMS.Entity.SRA.Activity activity)
            where T : SOFTTEK.SCMS.Entity.SRA.Activity, new()
        {
            T updatedActivity = new T();

            ValidateUpdatedActivityEffort(activity);
            ValidateUpdatedActivityInformation(activity);

            return context.Execute(() =>
            {


                using (dataSource = new SRADataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();
                    var result = dataSource.UpdateActivity(activity);
                    updatedActivity = new T
                    {
                        ActivityCode = result.ActivityCode,
                        ApprovedAt = result.ApprovedAt,
                        ApprovedBy = result.ApprovedBy,
                        ApprovedComments = result.ApprovedComments,
                        Details = result.Details,
                        Effort = result.Effort,
                        Employee = result.Employee,
                        ExecutedAt = result.ExecutedAt,
                        Identifier = result.Identifier,
                        Jornade = result.Jornade,
                        Project = result.Project,
                        ReportedAt = result.ReportedAt,
                        State = result.State
                    };
                }
                return updatedActivity;
            }, "Update an registered activity");
        }

        #endregion

        #region Validations

        private void ValidateModifiableActivityState(SOFTTEK.SCMS.Entity.SRA.Activity changedActivity)
        {
            if (changedActivity.State == APPROVED_ACTIVITY_STATE)
            {
                throw new Exception("La actividad proporcionada no es modificable.", new InvalidOperationException("La actividad a actualizar se encuentra en estado 'Aprobado' y no puede ser modificada."));
            }
            if (changedActivity.State == DISABLED_ACTIVITY_STATE)
            {
                throw new Exception("La actividad proporcionada no es modificable.", new InvalidOperationException("La actividad a actualizar se encuentra en estado 'Deshabilitado' y no puede ser modificada."));
            }
        }

        private void ValidateUpdatedActivityInformation(SCMS.Entity.SRA.Activity activity)
        {
            throw new NotImplementedException();
        }

        private void ValidateUpdatedActivityEffort(SCMS.Entity.SRA.Activity activity)
        {
            Entity.SRA.Week<SOFTTEK.SCMS.Entity.SRA.Activity> week = GetWeekForDate<SOFTTEK.SCMS.Entity.SRA.Activity>(activity.ExecutedAt);
            week.ReportedActivities = GetActivitiesForEmployeeIdInPeriod<SOFTTEK.SCMS.Entity.SRA.Activity>(activity.Employee.Identifier, week.From, week.To);
        }

        private void ValidateReportedActivityInformation(SCMS.Entity.SRA.Activity activity)
        {
            //Valid Project Check

            //Valid Activity Code for Project Status Check
        }

        private void ValidateReportedActivityEffort(SOFTTEK.SCMS.Entity.SRA.Activity activity)
        {
            // Retrieve activities for the corresponding week of the validated activity
            Entity.SRA.Week<SOFTTEK.SCMS.Entity.SRA.Activity> week = GetWeekForDate<SOFTTEK.SCMS.Entity.SRA.Activity>(activity.ExecutedAt);
            week.ReportedActivities = GetActivitiesForEmployeeIdInPeriod<SOFTTEK.SCMS.Entity.SRA.Activity>(activity.Employee.Identifier, week.From, week.To);


            if (activity.Jornade == "IH")
            {
                // Daily Activities Total Effort Maximum Duration Check
                if ((week.ReportedActivities.Where(a => a.ExecutedAt.Date == activity.ExecutedAt.Date && a.Jornade == "IH")
                    .Sum(a => a.Effort) + activity.Effort) > MAX_EFFORT_PER_DAY)
                {
                    throw new Exception("La duración de la actividad supera el máximo de horas permitidas para reportar en día.",
                        new InvalidOperationException(string.Format("El máximo de horas permitido para reportar por día es de {0}",
                            MAX_EFFORT_PER_DAY)
                        )
                    );
                }

                // Weekly Activities Total Effort Maximum Duration Check
                if ((week.ReportedActivities.Where(a => a.Jornade == "IH").Sum(a => a.Effort) + activity.Effort) > (MAX_EFFORT_PER_DAY * LABOR_DAYS_PER_WEEK))
                {
                    throw new Exception("La duración de la actividad supera el máximo de horas permitidas para reportar en una semana.",
                        new InvalidOperationException(string.Format("El máximo de horas permitido para reportar por semana es de {0}",
                            (MAX_EFFORT_PER_DAY * LABOR_DAYS_PER_WEEK))
                        )
                    );
                }
            }
            else if (activity.Jornade == "AH")
            {
                DateTime dateExecuteAt = activity.ExecutedAt.Date;
                var dateValue = new DateTime(dateExecuteAt.Year, dateExecuteAt.Month, dateExecuteAt.Day).AddDays(1);

                TimeSpan oSpan = dateValue.Subtract(dateExecuteAt);
                double countHoursDay = oSpan.TotalHours;

                if ((week.ReportedActivities.Where(a => a.ExecutedAt.Date == activity.ExecutedAt.Date)
                    .Sum(a => a.Effort) + activity.Effort) > countHoursDay)
                {
                    throw new Exception("La duración de la actividad supera el máximo de horas permitidas para reportar en un día.",
                        new InvalidOperationException(string.Format("El máximo de horas permitido para reportar por día es de {0} incluyendo las horas adicionales.",
                            countHoursDay)
                        )
                    );
                }
            }

        }

        private void ValidateApproval(SOFTTEK.SCMS.Entity.SRA.Activity targetActivity, SCMS.Entity.Shared.Employee approver)
        {

            bool isValidApproverForActivity = context.Execute(() =>
            {
                EmployeeBO employeeBO = new EmployeeBO(context);
                SOFTTEK.SCMS.Entity.Shared.Employee employee = employeeBO.GetEmployeeInfoById(targetActivity.Employee.Identifier);
                if (employee.Supervisor.Identifier == approver.Identifier)
                {
                    return true;
                }
                return false;
            }, "Validate approver with activity's employee supervisor");

            if (!isValidApproverForActivity)
            {
                throw new Exception("El aprovador no corresponde al supervisor de quien reporta la actividad a aprovar",
                    new InvalidOperationException("Inconsistencia entre el supervisor del empleado asociado a la actividad, y el aprovador proporcionado para la solicitud."));
            }

        }

        #endregion

        #region Helpers

        private static int GetStartingWeekNumberInRangeForCurrentWeek(int range, out DateTime firstThursday)
        {

            int startingWeek;
            DateTime today = DateTime.Now;
            DateTime firstdayOfYear = new DateTime(today.Year, 1, 1);
            int offset = DayOfWeek.Thursday - firstdayOfYear.DayOfWeek;
            firstThursday = firstdayOfYear.AddDays(offset);

            int firstWeek = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            //Get Current Week
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(today);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                today = today.AddDays(3);
            }

            int currentWeek = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(today, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            if (firstWeek <= 1)
            {
                currentWeek -= 1;
            }

            startingWeek = currentWeek - (range / 2);

            return startingWeek;
        }

        private static SOFTTEK.SCMS.Business.Entity.SRA.Week<T> GetWeekForDate<T>(DateTime date)
            where T : SOFTTEK.SCMS.Entity.SRA.Activity, new()
        {
            int weekNumber = -1;
            DateTime firstdayOfYear = new DateTime(date.Year, 1, 1);
            int offset = DayOfWeek.Thursday - firstdayOfYear.DayOfWeek;
            DateTime firstThursday = firstdayOfYear.AddDays(offset);
            int firstWeek = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            //Get Current Week
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(date);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                date = date.AddDays(3);
            }
            weekNumber = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            DateTime start = firstThursday.AddDays(weekNumber * DAYS_PER_WEEK).AddDays(-(int)DayOfWeek.Thursday + 1);
            DateTime end = start.AddDays(DAYS_PER_WEEK - 1);

            SOFTTEK.SCMS.Business.Entity.SRA.Week<T> week = new Entity.SRA.Week<T>
            {
                From = start,
                To = end,
                Number = weekNumber
            };
            return week;
        }

        #endregion
    }
}
