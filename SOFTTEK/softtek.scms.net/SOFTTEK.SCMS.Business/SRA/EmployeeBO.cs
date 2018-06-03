using SOFTTEK.SCMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFTTEK.SCMS.Business.SRA
{
    public class EmployeeBO : SOFTTEK.SCMS.Foundation.Business.BusinessObject
    {
        private SRADataContext dataSource;

        public EmployeeBO(SOFTTEK.SCMS.Foundation.Business.BusinessContext ctx)
            : base(ctx)
        {
        }

        public SOFTTEK.SCMS.Entity.Shared.Employee GetEmployeeInfoForToken()
        {
            SOFTTEK.SCMS.Entity.Shared.Employee employee = null;
            return context.Execute(() =>
            {

                using (dataSource = new SRADataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    employee = dataSource.GetEmployeeWithToken();
                }
                return employee;
            }, "Retrieve the employee information for the user related to the provided token.");
        }

        public SCMS.Entity.Shared.Employee GetEmployeeInfoById(int employeeId)
        {
            SOFTTEK.SCMS.Entity.Shared.Employee employee = null;
            return context.Execute(() =>
            {


                using (dataSource = new SRADataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();

                    employee = dataSource.GetEmployeeById(employeeId);
                }
                return employee;
            }, "Retrieve the employee information for the provided employee identifier.");
        }

        public bool SetEmployeeImage(string ftpURL, int employeeID)
        {
            bool result = false;
            return context.Execute(() =>
            {

                using (dataSource = new SRADataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();

                    dataSource.Initialize();
                    SOFTTEK.SCMS.Entity.Shared.Employee employee = dataSource.GetEmployeeById(employeeID);
                    if (employee != null)
                    {
                        employee.ImageURL = ftpURL;
                        result = dataSource.UpdateEmployee(employee);
                    };

                    return result;
                }
            }, "Upload to the images repository, the provided binary image file.");


            
        }
    }
}
