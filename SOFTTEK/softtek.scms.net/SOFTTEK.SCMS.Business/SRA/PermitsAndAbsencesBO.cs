using SOFTTEK.SCMS.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFTTEK.SCMS.Business.SRA
{
    public class PermitsAndAbsencesBO : SOFTTEK.SCMS.Foundation.Business.BusinessObject
    {
        #region Fields
        const int MAX_EFFORT_PER_DAY = 8;
        private SRADataContext dataSource;
        #endregion

        #region Propierties
        #endregion

        #region Constructors
        public PermitsAndAbsencesBO(SOFTTEK.SCMS.Foundation.Business.BusinessContext ctx)
            : base(ctx)
        {
        }
        #endregion

        

        
        

        #region Public Methods
        /// <summary>
        /// Get all the activities reported by an employee in a period.
        /// </summary>
        /// <param name="employeeId">Employee Identifier</param>
        /// <returns></returns>
        public List<SOFTTEK.SCMS.Entity.SRA.PermitsAndAbsences> GetPermitsAndAbsencesForEmployeeId(int employeeId)
        {
            List<SOFTTEK.SCMS.Entity.SRA.PermitsAndAbsences> activities = new List<SOFTTEK.SCMS.Entity.SRA.PermitsAndAbsences>();
            return context.Execute(() =>
            {

                using (dataSource = new SRADataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var results = dataSource.GetPermitsAndAbsences(employeeId);
                    activities = results;
                }

                return activities;
            }, "Retrieve the registered activities for the employee corresponding to the provided employee identifier.");

        }


        /// <summary>
        /// Register an activity for approval.
        /// </summary>
        /// <param name="permitsAndAbsences">permitsAndAbsences Insert Model information Permits and Absences</param>
        /// <returns>Registered activity information.</returns>
        public List<SOFTTEK.SCMS.Entity.SRA.PermitsAndAbsences> RegisterPermitsAnsAbsences(SOFTTEK.SCMS.Entity.SRA.PermitsAndAbsences permitsAndAbsences)
        {
            List<SOFTTEK.SCMS.Entity.SRA.PermitsAndAbsences> activities = new List<SOFTTEK.SCMS.Entity.SRA.PermitsAndAbsences>();

            ValidatePermitsAndAAbsences(permitsAndAbsences);

            return context.Execute(() =>
            {


                using (dataSource = new SRADataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();
                    var result = dataSource.RegisterPermitsAndAbsences(permitsAndAbsences);
                    activities = result;
                    
                }
                return activities;
            }, "Register an Permits and absences for aprov");
        }

        /// <summary>
        /// Register an activity for approval.
        /// </summary>
        /// <param name="permitsAndAbsences">permitsAndAbsences Update Model information Permits and Absences</param>
        /// <returns>Registered activity information.</returns>
        public List<SOFTTEK.SCMS.Entity.SRA.PermitsAndAbsences> UpdatePermitsAnsAbsences(long activityID, SOFTTEK.SCMS.Entity.SRA.PermitsAndAbsences permitsAndAbsences)
        {
            List<SOFTTEK.SCMS.Entity.SRA.PermitsAndAbsences> activities = new List<SOFTTEK.SCMS.Entity.SRA.PermitsAndAbsences>();

            ValidatePermitsAndAAbsences(permitsAndAbsences);

            return context.Execute(() =>
            {

                using (dataSource = new SRADataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                    dataSource.Initialize();

                    var result = dataSource.UpdatePermitsAndAbsences(permitsAndAbsences);
                    activities = result;

                }
                return activities;
            }, "Register an activity for approval");
        }
        #endregion

        #region Private Methods
        private void ValidatePermitsAndAAbsences(SOFTTEK.SCMS.Entity.SRA.PermitsAndAbsences activityPerAndAbs)
        {
            if(activityPerAndAbs != null)
            {
                var permitsAndAbsencesActuallyInBd = GetPermitsAndAbsencesForEmployeeId(activityPerAndAbs.perabs_employee.Identifier);

                ///Validación para corroborar que no exista una solicitud en la misma fecha
                if (permitsAndAbsencesActuallyInBd.Where(a => a.perabs_start_at >= activityPerAndAbs.perabs_start_at
                && a.perabs_end_at <= activityPerAndAbs.perabs_end_at).Sum(a => a.perabs_total_hours) + activityPerAndAbs.perabs_total_hours > MAX_EFFORT_PER_DAY)
                {
                    throw new Exception("No se puede realizar esta solicitud de Permiso o ausencia, porque ya existe otra actividad programada en esta fecha.",
                        new InvalidOperationException(string.Format("Solo se puede realizar una solicitud en una franja.")
                        )
                    );
                }

                ///Validacion fecha mayor
                if(activityPerAndAbs.perabs_start_at > activityPerAndAbs.perabs_end_at)
                {
                    throw new Exception("La fecha de inicio de la solicitud no puede ser mayor a la fecha de fin de esta.",
                        new InvalidOperationException(string.Format("Error al procesar estas fechas.")
                        )
                    );
                }

                /// Validación cantidad de horas
                //TimeSpan daysDifference = activityPerAndAbs.perabs_end_at.Date.Subtract(activityPerAndAbs.perabs_start_at.Date);
                //int totalHoursMaxSolicitud = Convert.ToInt32(daysDifference.Ticks) * MAX_EFFORT_PER_DAY;
                //if (activityPerAndAbs.perabs_total_hours > totalHoursMaxSolicitud) {
                //    throw new Exception("Esta excediendo el numero maximo de horas que puede realizar en esta solicitud.",
                //       new InvalidOperationException(string.Format("Entre la fecha {0} y {1} se pueden soolictar maximo {2} horas, usted esta solicitando {3} por tanto esta solicitud es invalida.",
                //       activityPerAndAbs.perabs_start_at, activityPerAndAbs.perabs_end_at, totalHoursMaxSolicitud, activityPerAndAbs.perabs_total_hours)
                //       )
                //   );
                //}
                if(activityPerAndAbs.perabs_total_hours > MAX_EFFORT_PER_DAY)
                {
                    throw new Exception(string.Format("La solicitud no puede superar el maximo de {0} horas por día.",MAX_EFFORT_PER_DAY),
                        new InvalidOperationException(string.Format("El total de horas se valida por día y no por el total de dias que solicit el permiso o la ausencia")));
                }
            }
            else
            {
                throw new Exception("No se ha podido realizar la validacion del Permiso o l asencia.",
                        new InvalidOperationException("El permiso o la ausencia no puede ser nulo"));
            }
            
        }
        #endregion
    }
}
