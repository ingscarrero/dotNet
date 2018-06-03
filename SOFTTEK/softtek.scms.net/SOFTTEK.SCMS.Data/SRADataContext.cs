using SOFTTEK.SCMS.Foundation.Security;
using SOFTTEK.SCMS.Foundation.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFTTEK.SCMS.Data
{
    public class SRADataContext : DataContext, IDisposable
    {
        public string ConnectionString { get; set; }
        public string DefaultUser { private get; set; }

        private SOFTTEK.SCMS.Foundation.Data.DataProvider dataProvider;

        public string ProviderName { get; private set; }

        public SRADataContext(SecurityContext securityContext)
            : base(securityContext)
        {

        }

        public void Initialize()
        {
            if (string.IsNullOrEmpty(ConnectionString))
            {
                throw new Exception("ConnectionString is required.", new ArgumentException("ConnectionString cannot be null."));
            }
            ProviderName = System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionString].ProviderName;
            switch (ProviderName)
            {
                case "System.Data.SqlClient":
                    dataProvider = new SOFTTEK.SCMS.Foundation.Data.DataProviders.MSSQLDataProvider();
                    dataProvider.ConnectionString = ConnectionString;
                    dataProvider.Context = this;
                    break;
                case "System.Data.OracleClient":
                    break;
                default:
                    break;
            }
        }

        public void Dispose()
        {
            dataProvider.Dispose();
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Dispose();
            }
            dataProvider = null;
        }



        public List<SOFTTEK.SCMS.Entity.SRA.Mobile.HomeItem> GetMobileHomeItems<T>()
        {


            Func<object, List<System.Data.IDataParameter>> filter = (t) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();

                System.Data.IDataParameter p1 = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p1.Direction = System.Data.ParameterDirection.Input;
                p1.DbType = System.Data.DbType.String;

                parameters.Add(p1);

                System.Data.IDataParameter p2 = dataProvider.GetParameter("@app_id", SecurityContext.AppID);
                p2.Direction = System.Data.ParameterDirection.Input;
                p2.DbType = System.Data.DbType.String;

                parameters.Add(p2);

                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.SRA.Mobile.HomeItem> mapper = (reader) =>
            {
                SOFTTEK.SCMS.Entity.SRA.Mobile.HomeItem instance = new SOFTTEK.SCMS.Entity.SRA.Mobile.HomeItem
                {
                    Title = reader["item_title"] != null ? reader["item_title"].ToString() : string.Empty,
                    Caption = reader["item_caption"] != null ? reader["item_caption"].ToString() : string.Empty,
                    Action = reader["item_view"] != null ? reader["item_view"].ToString() : string.Empty,
                    ImageUrl = reader["item_image_url"] != null ? reader["item_image_url"].ToString() : string.Empty
                };
                return instance;
            };


            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            return dataProvider.ExecuteReaderWithFilter(SCMS.SCMS_SP_GetMobileAppMenuItems, filter, null, mapper);
        }



        public SOFTTEK.SCMS.Entity.Security.Token AuthenticateUser(SOFTTEK.SCMS.Entity.Security.User user)
        {
            Func<SOFTTEK.SCMS.Entity.Security.User, List<System.Data.IDataParameter>> filter = (u) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();

                System.Data.IDataParameter p1 = dataProvider.GetParameter("@device_id", u.DeviceIdentifier);
                p1.Direction = System.Data.ParameterDirection.Input;
                p1.DbType = System.Data.DbType.String;

                parameters.Add(p1);

                System.Data.IDataParameter p2 = dataProvider.GetParameter("@login", u.NetworkAccount);
                p2.Direction = System.Data.ParameterDirection.Input;
                p2.DbType = System.Data.DbType.String;

                parameters.Add(p2);

                System.Data.IDataParameter p3 = dataProvider.GetParameter("@password", u.Password);
                p3.Direction = System.Data.ParameterDirection.Input;
                p3.DbType = System.Data.DbType.String;

                parameters.Add(p3);

                System.Data.IDataParameter p4 = dataProvider.GetParameter("@caller_id", DefaultUser);
                p4.Direction = System.Data.ParameterDirection.Input;
                p4.DbType = System.Data.DbType.String;

                parameters.Add(p4);

                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.Security.Token> mapper = (reader) =>
            {
                SOFTTEK.SCMS.Entity.Security.Token instance = new SOFTTEK.SCMS.Entity.Security.Token
                {
                    Identifier = reader["v_id"] != null ? reader["v_id"].ToString() : string.Empty,
                    CreatedAt = reader["v_created"] != null ? DateTime.Parse(reader["v_created"].ToString()) : default(DateTime),
                    ExpiresAt = reader["v_expires_at"] != null ? DateTime.Parse(reader["v_expires_at"].ToString()) : default(DateTime),
                    UserIS = reader["v_user_id"] != null ? reader["v_user_id"].ToString() : string.Empty
                };
                return instance;
            };

            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            SOFTTEK.SCMS.Entity.Security.Token result = dataProvider.ExecuteForEntityWithFilter(SCMS.SCMS_SP_Authorize, filter, user, mapper);
            return result;
        }

        public SOFTTEK.SCMS.Entity.Security.Token CreateUser(Entity.Security.User user)
        {
            Func<SOFTTEK.SCMS.Entity.Security.User, List<System.Data.IDataParameter>> filter = (u) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();

                System.Data.IDataParameter p1 = dataProvider.GetParameter("@device_id", u.DeviceIdentifier);
                p1.Direction = System.Data.ParameterDirection.Input;
                p1.DbType = System.Data.DbType.String;

                parameters.Add(p1);


                System.Data.IDataParameter p2 = dataProvider.GetParameter("@user_id", u.Identifier);
                p2.Direction = System.Data.ParameterDirection.Input;
                p2.DbType = System.Data.DbType.String;

                parameters.Add(p2);

                System.Data.IDataParameter p3 = dataProvider.GetParameter("@login", u.NetworkAccount);
                p3.Direction = System.Data.ParameterDirection.Input;
                p3.DbType = System.Data.DbType.String;

                parameters.Add(p3);

                System.Data.IDataParameter p4 = dataProvider.GetParameter("@password", u.Password);
                p4.Direction = System.Data.ParameterDirection.Input;
                p4.DbType = System.Data.DbType.String;

                parameters.Add(p4);

                System.Data.IDataParameter p5 = dataProvider.GetParameter("@caller_id", DefaultUser);
                p5.Direction = System.Data.ParameterDirection.Input;
                p5.DbType = System.Data.DbType.String;

                parameters.Add(p5);

                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.Security.Token> mapper = (reader) =>
            {

                SOFTTEK.SCMS.Entity.Security.Token instance = new SOFTTEK.SCMS.Entity.Security.Token
                {
                    Identifier = reader["v_id"] != null ? reader["v_id"].ToString() : string.Empty,
                    CreatedAt = reader["v_created"] != null ? DateTime.Parse(reader["v_created"].ToString()) : default(DateTime),
                    ExpiresAt = reader["v_expires_at"] != null ? DateTime.Parse(reader["v_expires_at"].ToString()) : default(DateTime),
                    UserIS = reader["v_user_id"] != null ? reader["v_user_id"].ToString() : string.Empty
                };
                return instance;

            };


            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;

            SOFTTEK.SCMS.Entity.Security.Token result = dataProvider.ExecuteForEntityWithFilter(SCMS.SCMS_SP_AddUser, filter, user, mapper);
            return result;
        }

        public Entity.Security.Token GetToken()
        {

            if (string.IsNullOrEmpty(SecurityContext.AuthorizationTicket))
            {
                throw new Exception("Token is required.", new ArgumentException("Token cannot be null"));
            }

            if (string.IsNullOrEmpty(SecurityContext.DeviceID))
            {
                throw new Exception("Device is required.", new ArgumentException("Device cannot be null"));
            }

            Func<object, List<System.Data.IDataParameter>> filter = (i) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();

                System.Data.IDataParameter p1 = dataProvider.GetParameter("@device_id", SecurityContext.DeviceID);
                p1.Direction = System.Data.ParameterDirection.Input;
                p1.DbType = System.Data.DbType.String;

                parameters.Add(p1);


                System.Data.IDataParameter p2 = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p2.Direction = System.Data.ParameterDirection.Input;
                p2.DbType = System.Data.DbType.String;

                parameters.Add(p2);


                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.Security.Token> mapper = (reader) =>
            {
                SOFTTEK.SCMS.Entity.Security.Token instance = new SOFTTEK.SCMS.Entity.Security.Token
                {
                    Identifier = reader["token_id"] != null ? reader["token_id"].ToString() : string.Empty,
                    CreatedAt = reader["token_created"] != null ? DateTime.Parse(reader["token_created"].ToString()) : default(DateTime),
                    ExpiresAt = reader["token_expires_at"] != null ? DateTime.Parse(reader["token_expires_at"].ToString()) : default(DateTime),
                    UserIS = reader["token_user_id"] != null ? reader["token_user_id"].ToString() : string.Empty
                };
                return instance;
            };


            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            //Move to a resource file
            SOFTTEK.SCMS.Entity.Security.Token result = dataProvider.ExecuteForEntityWithFilter(SCMS.SCMS_SP_GetToken, filter, null, mapper);
            return result;
        }



        public List<Entity.Shared.Parameter<T>> GetParametersForCategory<T>(string category)
        {
            if (string.IsNullOrEmpty(SecurityContext.AuthorizationTicket))
            {
                throw new Exception("Token is required.", new ArgumentException("Token cannot be null"));
            }

            if (string.IsNullOrEmpty(SecurityContext.DeviceID))
            {
                throw new Exception("Device is required.", new ArgumentException("Device cannot be null"));
            }

            Func<object, List<System.Data.IDataParameter>> filter = (t) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();

                System.Data.IDataParameter p1 = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p1.Direction = System.Data.ParameterDirection.Input;
                p1.DbType = System.Data.DbType.String;

                parameters.Add(p1);


                System.Data.IDataParameter p2 = dataProvider.GetParameter("@param_category", category);
                p2.Direction = System.Data.ParameterDirection.Input;
                p2.DbType = System.Data.DbType.String;

                parameters.Add(p2);

                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.Shared.Parameter<T>> mapper = (reader) =>
            {

                int helperNumber;
                bool helperBoolean;

                SOFTTEK.SCMS.Entity.Shared.Category _category = new Entity.Shared.Category
                {
                    Identifier = reader["category_id"] != null ? (int.TryParse(reader["category_id"].ToString(), out helperNumber) ? helperNumber : -1) : -1,
                    Name = reader["category_name"] != null ? reader["category_name"].ToString() : string.Empty,
                };
                
                SOFTTEK.SCMS.Entity.Shared.Parameter<T> instance = new SOFTTEK.SCMS.Entity.Shared.Parameter<T>
                {
                    Identifier = reader["id"] != null ? (int.TryParse(reader["id"].ToString(), out helperNumber) ? helperNumber : -1) : -1,
                    Value = reader["value"] != null ? (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(reader["value"].ToString()) : default(T),
                    Category = _category,
                    Description = reader["description"] != null ? reader["description"].ToString() : string.Empty,
                    Comments = reader["comments"] != null ? reader["comments"].ToString() : string.Empty,
                    Order = reader["order"] != null ? (int.TryParse(reader["order"].ToString(), out helperNumber) ? helperNumber : 0) : 0,
                    ExternalIdentifier = reader["external_id"] != null ? reader["external_id"].ToString() : string.Empty,
                    IsActive = reader["is_active"] != null ? (bool.TryParse(reader["is_active"].ToString(), out helperBoolean) ? helperBoolean : false) : false
                };
                return instance;

            };


            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;

            return dataProvider.ExecuteReaderWithFilter(SCMS.SRA_SP_GetParametersByCategory, filter, null, mapper);
        }



        public Entity.Shared.Employee GetEmployeeWithToken()
        {
            if (string.IsNullOrEmpty(SecurityContext.AuthorizationTicket))
            {
                throw new Exception("Token is required.", new ArgumentException("Token cannot be null"));
            }

            if (string.IsNullOrEmpty(SecurityContext.DeviceID))
            {
                throw new Exception("Device is required.", new ArgumentException("Device cannot be null"));
            }

            Func<object, List<System.Data.IDataParameter>> filter = (i) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();


                System.Data.IDataParameter p1 = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p1.Direction = System.Data.ParameterDirection.Input;
                p1.DbType = System.Data.DbType.String;

                parameters.Add(p1);

                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.Shared.Employee> mapper = (reader) =>
            {
                int numberHelper = 0;
                SOFTTEK.SCMS.Entity.Shared.Employee instance = new SOFTTEK.SCMS.Entity.Shared.Employee
                {
                    Identifier = reader["id"] != null ? (int.TryParse(reader["id"].ToString(), out numberHelper) ? numberHelper : -1) : -1,
                    Area = reader["area"] != null ? reader["area"].ToString() : string.Empty,
                    Role = reader["role"] != null ? reader["role"].ToString() : string.Empty,
                    Person = new Entity.Shared.Person
                    {
                        Identifier = reader["person_id"] != null ? (int.TryParse(reader["person_id"].ToString(), out numberHelper) ? numberHelper : -1) : -1,
                        Identification = reader["identification"] != null ? reader["identification"].ToString() : string.Empty,
                        Name = reader["name"] != null ? reader["name"].ToString() : string.Empty,
                        MiddleName = reader["middle_name"] != null ? reader["middle_name"].ToString() : string.Empty,
                        LastName = reader["last_name"] != null ? reader["last_name"].ToString() : string.Empty
                    },
                    Supervisor = new Entity.Shared.Employee
                    {
                        Identifier = reader["supervisor_id"] != null ? (int.TryParse(reader["supervisor_id"].ToString(), out numberHelper) ? numberHelper : -1) : -1,
                        Area = reader["supervisor_area"] != null ? reader["supervisor_area"].ToString() : string.Empty,
                        Role = reader["supervisor_role"] != null ? reader["supervisor_role"].ToString() : string.Empty,
                        Person = new Entity.Shared.Person
                        {
                            Identifier = reader["supervisor_person_id"] != null ? (int.TryParse(reader["supervisor_person_id"].ToString(), out numberHelper) ? numberHelper : -1) : -1,
                            Name = reader["supervisor_name"] != null ? reader["supervisor_name"].ToString() : string.Empty,
                            MiddleName = reader["supervisor_middle_name"] != null ? reader["supervisor_middle_name"].ToString() : string.Empty,
                            LastName = reader["supervisor_last_name"] != null ? reader["supervisor_last_name"].ToString() : string.Empty
                        }
                    },
                    ImageURL = reader["image_url"] != null ? reader["image_url"].ToString() : string.Empty
                };
                return instance;
            };


            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            //Move to a resource file
            SOFTTEK.SCMS.Entity.Shared.Employee result = dataProvider.ExecuteForEntityWithFilter(SCMS.SRA_SP_GetEmployeeForToken, filter, null, mapper);
            return result;
        }

        public Entity.Shared.Employee GetEmployeeById(int employeeId)
        {
            if (string.IsNullOrEmpty(SecurityContext.AuthorizationTicket))
            {
                throw new Exception("Token is required.", new ArgumentException("Token cannot be null"));
            }

            if (string.IsNullOrEmpty(SecurityContext.DeviceID))
            {
                throw new Exception("Device is required.", new ArgumentException("Device cannot be null"));
            }

            Func<object, List<System.Data.IDataParameter>> filter = (i) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();

                System.Data.IDataParameter p1 = dataProvider.GetParameter("@employee_id", employeeId);
                p1.Direction = System.Data.ParameterDirection.Input;
                p1.DbType = System.Data.DbType.Int32;

                parameters.Add(p1);


                System.Data.IDataParameter p2 = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p2.Direction = System.Data.ParameterDirection.Input;
                p2.DbType = System.Data.DbType.String;

                parameters.Add(p2);


                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.Shared.Employee> mapper = (reader) =>
            {
                int numberHelper = 0;
                SOFTTEK.SCMS.Entity.Shared.Employee instance = new SOFTTEK.SCMS.Entity.Shared.Employee
                {

                    Identifier = employeeId,
                    Area = reader["area"] != null ? reader["area"].ToString() : string.Empty,
                    Role = reader["role"] != null ? reader["role"].ToString() : string.Empty,
                    Person = new Entity.Shared.Person
                    {
                        Identifier = reader["person_id"] != null ? (int.TryParse(reader["person_id"].ToString(), out numberHelper) ? numberHelper : -1) : -1,
                        Identification = reader["identification"] != null ? reader["identification"].ToString() : string.Empty,
                        Name = reader["name"] != null ? reader["name"].ToString() : string.Empty,
                        MiddleName = reader["middle_name"] != null ? reader["middle_name"].ToString() : string.Empty,
                        LastName = reader["last_name"] != null ? reader["last_name"].ToString() : string.Empty
                    },
                    Supervisor = new Entity.Shared.Employee
                    {
                        Identifier = reader["supervisor_id"] != null ? (int.TryParse(reader["supervisor_id"].ToString(), out numberHelper) ? numberHelper : -1) : -1,
                        Area = reader["supervisor_area"] != null ? reader["supervisor_area"].ToString() : string.Empty,
                        Role = reader["supervisor_role"] != null ? reader["supervisor_role"].ToString() : string.Empty,
                        Person = new Entity.Shared.Person
                        {
                            Identifier = reader["supervisor_person_id"] != null ? (int.TryParse(reader["supervisor_person_id"].ToString(), out numberHelper) ? numberHelper : -1) : -1,
                            Name = reader["supervisor_name"] != null ? reader["supervisor_name"].ToString() : string.Empty,
                            MiddleName = reader["supervisor_middle_name"] != null ? reader["supervisor_middle_name"].ToString() : string.Empty,
                            LastName = reader["supervisor_last_name"] != null ? reader["supervisor_last_name"].ToString() : string.Empty
                        }
                    },
                    ImageURL = reader["image_url"] != null ? reader["image_url"].ToString() : string.Empty
                };
                return instance;
            };


            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            //Move to a resource file
            SOFTTEK.SCMS.Entity.Shared.Employee result = dataProvider.ExecuteForEntityWithFilter(SCMS.SRA_SP_GetEmployeeById, filter, null, mapper);
            return result;
        }

        public bool UpdateEmployee(Entity.Shared.Employee employee)
        {
            if (string.IsNullOrEmpty(SecurityContext.AuthorizationTicket))
            {
                throw new Exception("Token is required.", new ArgumentException("Token cannot be null"));
            }

            if (string.IsNullOrEmpty(SecurityContext.DeviceID))
            {
                throw new Exception("Device is required.", new ArgumentException("Device cannot be null"));
            }

            bool result = false;
            Func<SOFTTEK.SCMS.Entity.Shared.Employee, List<System.Data.IDataParameter>> filter = (e) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();

                System.Data.IDataParameter p1 = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p1.Direction = System.Data.ParameterDirection.Input;
                p1.DbType = System.Data.DbType.String;

                parameters.Add(p1);

                System.Data.IDataParameter p2 = dataProvider.GetParameter("@employee_id", e.Identifier);
                p2.Direction = System.Data.ParameterDirection.Input;
                p2.DbType = System.Data.DbType.Int32;

                parameters.Add(p2);

                System.Data.IDataParameter p3 = dataProvider.GetParameter("@employee_image_url", e.ImageURL);
                p3.Direction = System.Data.ParameterDirection.Input;
                p3.DbType = System.Data.DbType.String;

                parameters.Add(p3);

                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.Shared.Employee> mapper = (reader) =>
            {
                SOFTTEK.SCMS.Entity.Shared.Employee instance = new SOFTTEK.SCMS.Entity.Shared.Employee
                {
                    Area = reader["area"] != null ? reader["area"].ToString() : string.Empty,
                    Role = reader["role"] != null ? reader["role"].ToString() : string.Empty,
                    Person = new Entity.Shared.Person
                    {
                        Identification = reader["identification"] != null ? reader["identification"].ToString() : string.Empty,
                        Name = reader["name"] != null ? reader["name"].ToString() : string.Empty,
                        MiddleName = reader["middle_name"] != null ? reader["middle_name"].ToString() : string.Empty,
                        LastName = reader["last_name"] != null ? reader["last_name"].ToString() : string.Empty
                    },
                    Supervisor = new Entity.Shared.Employee
                    {
                        Area = reader["supervisor_area"] != null ? reader["supervisor_area"].ToString() : string.Empty,
                        Role = reader["supervisor_role"] != null ? reader["supervisor_role"].ToString() : string.Empty,
                        Person = new Entity.Shared.Person
                        {
                            Name = reader["supervisor_name"] != null ? reader["supervisor_name"].ToString() : string.Empty,
                            MiddleName = reader["supervisor_middle_name"] != null ? reader["supervisor_middle_name"].ToString() : string.Empty,
                            LastName = reader["supervisor_last_name"] != null ? reader["supervisor_last_name"].ToString() : string.Empty
                        }
                    },
                    ImageURL = reader["image_url"] != null ? reader["image_url"].ToString() : string.Empty
                };
                return instance;
            };

            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            //Move to a resource file
            SOFTTEK.SCMS.Entity.Shared.Employee updatedEmployee = dataProvider.ExecuteForEntityWithFilter(SCMS.SRA_SP_GetEmployeeById, filter, employee, mapper);
            result = updatedEmployee != null;

            return result;
        }



        public Entity.SRA.Activity RegisterActivity(Entity.SRA.Activity activity)
        {
            if (string.IsNullOrEmpty(SecurityContext.AuthorizationTicket))
            {
                throw new Exception("Token is required.", new ArgumentException("Token cannot be null"));
            }

            if (string.IsNullOrEmpty(SecurityContext.DeviceID))
            {
                throw new Exception("Device is required.", new ArgumentException("Device cannot be null"));
            }

            Entity.SRA.Activity registeredActivity = new Entity.SRA.Activity();
            Func<Entity.SRA.Activity, List<System.Data.IDataParameter>> filter = (e) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();

                System.Data.IDataParameter p1 = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p1.Direction = System.Data.ParameterDirection.Input;
                p1.DbType = System.Data.DbType.String;

                parameters.Add(p1);

                System.Data.IDataParameter p2 = dataProvider.GetParameter("@activity_project", e.Project);
                p2.Direction = System.Data.ParameterDirection.Input;
                p2.DbType = System.Data.DbType.String;

                parameters.Add(p2);

                System.Data.IDataParameter p3 = dataProvider.GetParameter("@activity_code", e.ActivityCode);
                p3.Direction = System.Data.ParameterDirection.Input;
                p3.DbType = System.Data.DbType.String;

                parameters.Add(p3);

                System.Data.IDataParameter p4 = dataProvider.GetParameter("@activity_description", e.Details);
                p4.Direction = System.Data.ParameterDirection.Input;
                p4.DbType = System.Data.DbType.String;

                parameters.Add(p4);

                System.Data.IDataParameter p5 = dataProvider.GetParameter("@activity_effort", e.Effort);
                p5.Direction = System.Data.ParameterDirection.Input;
                p5.DbType = System.Data.DbType.Double;

                parameters.Add(p5);

                System.Data.IDataParameter p6 = dataProvider.GetParameter("@activity_executed_at", e.ExecutedAt);
                p6.Direction = System.Data.ParameterDirection.Input;
                p6.DbType = System.Data.DbType.DateTime;

                parameters.Add(p6);

                System.Data.IDataParameter p7 = dataProvider.GetParameter("@activity_jornade_type", e.Jornade);
                p7.Direction = System.Data.ParameterDirection.Input;
                p7.DbType = System.Data.DbType.String;

                parameters.Add(p7);

                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.SRA.Activity> mapper = (reader) =>
            {
                DateTime dateTimeHelper;
                Double doubleHelper;
                long longHelper;
                int intHelper;

                SOFTTEK.SCMS.Entity.SRA.Activity instance = new SOFTTEK.SCMS.Entity.SRA.Activity
                {
                    Identifier = reader["v_id"] != null ? (long.TryParse(reader["v_id"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    ActivityCode = reader["v_code"] != null ? reader["v_code"].ToString() : string.Empty,
                    Details = reader["v_description"] != null ? reader["v_description"].ToString() : string.Empty,
                    Project = reader["v_project"] != null ? reader["v_project"].ToString() : string.Empty,
                    State = reader["v_status"] != null ? reader["v_status"].ToString() : string.Empty,
                    ExecutedAt = reader["v_executed_at"] != null ? (DateTime.TryParse(reader["v_executed_at"].ToString(), out dateTimeHelper) ? dateTimeHelper : default(DateTime)) : default(DateTime),
                    ReportedAt = DateTime.Now,
                    Effort = reader["v_effort"] != null ? (Double.TryParse(reader["v_effort"].ToString(), out doubleHelper) ? doubleHelper : default(Double)) : default(Double),
                    Jornade = reader["v_jornade_type"] != null ? reader["v_jornade_type"].ToString() : string.Empty,
                    Employee = new Entity.Shared.Employee { Identifier = reader["v_employee"] != null ? (int.TryParse(reader["v_employee"].ToString(), out intHelper) ? intHelper : -1) : -1 },
                };
                return instance;
            };

            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;

            registeredActivity = dataProvider.ExecuteForEntityWithFilter(SCMS.SRA_SP_InsertEmployeeActivity, filter, activity, mapper);

            return registeredActivity;
        }

        public Entity.SRA.Activity UpdateActivity(Entity.SRA.Activity activity)
        {
            if (string.IsNullOrEmpty(SecurityContext.AuthorizationTicket))
            {
                throw new Exception("Token is required.", new ArgumentException("Token cannot be null"));
            }

            if (string.IsNullOrEmpty(SecurityContext.DeviceID))
            {
                throw new Exception("Device is required.", new ArgumentException("Device cannot be null"));
            }

            Entity.SRA.Activity updatedActivity = new Entity.SRA.Activity();
            Func<Entity.SRA.Activity, List<System.Data.IDataParameter>> filter = (e) =>
            {


                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();

                System.Data.IDataParameter p1 = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p1.Direction = System.Data.ParameterDirection.Input;
                p1.DbType = System.Data.DbType.String;

                parameters.Add(p1);

                System.Data.IDataParameter p2 = dataProvider.GetParameter("@activity_id", e.Identifier);
                p2.Direction = System.Data.ParameterDirection.Input;
                p2.DbType = System.Data.DbType.Int64;

                parameters.Add(p2);

                System.Data.IDataParameter p3 = dataProvider.GetParameter("@activity_project", e.Project);
                p3.Direction = System.Data.ParameterDirection.Input;
                p3.DbType = System.Data.DbType.String;

                parameters.Add(p3);

                System.Data.IDataParameter p4 = dataProvider.GetParameter("@activity_code", e.ActivityCode);
                p4.Direction = System.Data.ParameterDirection.Input;
                p4.DbType = System.Data.DbType.String;

                parameters.Add(p4);

                System.Data.IDataParameter p5 = dataProvider.GetParameter("@activity_description", e.Details);
                p5.Direction = System.Data.ParameterDirection.Input;
                p5.DbType = System.Data.DbType.String;

                parameters.Add(p5);

                System.Data.IDataParameter p6 = dataProvider.GetParameter("@activity_effort", e.Effort);
                p6.Direction = System.Data.ParameterDirection.Input;
                p6.DbType = System.Data.DbType.Double;

                parameters.Add(p6);

                System.Data.IDataParameter p7 = dataProvider.GetParameter("@activity_status", e.State);
                p7.Direction = System.Data.ParameterDirection.Input;
                p7.DbType = System.Data.DbType.String;

                parameters.Add(p7);

                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.SRA.Activity> mapper = (reader) =>
            {
                DateTime dateTimeHelper;
                Double doubleHelper;
                long longHelper;
                int intHelper;

                SOFTTEK.SCMS.Entity.SRA.Activity instance = new SOFTTEK.SCMS.Entity.SRA.Activity
                {
                    Identifier = reader["v_id"] != null ? (long.TryParse(reader["v_id"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    ActivityCode = reader["v_code"] != null ? reader["v_code"].ToString() : string.Empty,
                    Details = reader["v_description"] != null ? reader["v_description"].ToString() : string.Empty,
                    Project = reader["v_project"] != null ? reader["v_project"].ToString() : string.Empty,
                    State = reader["v_status"] != null ? reader["v_status"].ToString() : string.Empty,
                    ExecutedAt = reader["v_executed_at"] != null ? (DateTime.TryParse(reader["v_executed_at"].ToString(), out dateTimeHelper) ? dateTimeHelper : default(DateTime)) : default(DateTime),
                    ReportedAt = DateTime.Now,
                    Effort = reader["v_effort"] != null ? (Double.TryParse(reader["v_effort"].ToString(), out doubleHelper) ? doubleHelper : default(Double)) : default(Double),
                    Jornade = reader["v_jornade_type"] != null ? reader["v_jornade_type"].ToString() : string.Empty,
                    Employee = new Entity.Shared.Employee { Identifier = reader["v_employee"] != null ? (int.TryParse(reader["v_employee"].ToString(), out intHelper) ? intHelper : -1) : -1 },
                };
                return instance;
            };

            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;

            updatedActivity = dataProvider.ExecuteForEntityWithFilter(SCMS.SRA_SP_Update_PermitsAndAbsences, filter, activity, mapper);

            return updatedActivity;
        }

        public List<Entity.SRA.Activity> GetActivities(int employeeId, DateTime from, DateTime to)
        {
            if (string.IsNullOrEmpty(SecurityContext.AuthorizationTicket))
            {
                throw new Exception("Token is required.", new ArgumentException("Token cannot be null"));
            }

            if (string.IsNullOrEmpty(SecurityContext.DeviceID))
            {
                throw new Exception("Device is required.", new ArgumentException("Device cannot be null"));
            }


            List<Entity.SRA.Activity> activities = new List<Entity.SRA.Activity>();
            Func<object, List<System.Data.IDataParameter>> filter = (e) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();

                System.Data.IDataParameter p1 = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p1.Direction = System.Data.ParameterDirection.Input;
                p1.DbType = System.Data.DbType.String;

                parameters.Add(p1);

                System.Data.IDataParameter p2 = dataProvider.GetParameter("@employee_id", employeeId);
                p2.Direction = System.Data.ParameterDirection.Input;
                p2.DbType = System.Data.DbType.Int32;

                parameters.Add(p2);

                System.Data.IDataParameter p3 = dataProvider.GetParameter("@activities_from", from);
                p3.Direction = System.Data.ParameterDirection.Input;
                p3.DbType = System.Data.DbType.DateTime;

                parameters.Add(p3);


                System.Data.IDataParameter p4 = dataProvider.GetParameter("@activities_to", to);
                p4.Direction = System.Data.ParameterDirection.Input;
                p4.DbType = System.Data.DbType.DateTime;

                parameters.Add(p4);

                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.SRA.Activity> mapper = (reader) =>
            {
                DateTime dateTimeHelper;
                Double doubleHelper;
                long longHelper;
                SOFTTEK.SCMS.Entity.SRA.Activity instance = new SOFTTEK.SCMS.Entity.SRA.Activity
                {
                    Identifier = reader["activity_id"] != null ? (long.TryParse(reader["activity_id"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    ActivityCode = reader["activity_code"] != null ? reader["activity_code"].ToString() : string.Empty,
                    Details = reader["activity_description"] != null ? reader["activity_description"].ToString() : string.Empty,
                    Project = reader["activity_project"] != null ? reader["activity_project"].ToString() : string.Empty,
                    State = reader["activity_status"] != null ? reader["activity_status"].ToString() : string.Empty,
                    ExecutedAt = reader["activity_executed_at"] != null ? (DateTime.TryParse(reader["activity_executed_at"].ToString(), out dateTimeHelper) ? dateTimeHelper : default(DateTime)) : default(DateTime),
                    ReportedAt = reader["activity_reported_at"] != null ? (DateTime.TryParse(reader["activity_reported_at"].ToString(), out dateTimeHelper) ? dateTimeHelper : default(DateTime)) : default(DateTime),
                    ApprovedAt = reader["activity_validated_at"] != null ? (DateTime.TryParse(reader["activity_validated_at"].ToString(), out dateTimeHelper) ? dateTimeHelper : default(DateTime)) : default(DateTime),
                    Effort = reader["activity_effort"] != null ? (Double.TryParse(reader["activity_effort"].ToString(), out doubleHelper) ? doubleHelper : default(Double)) : default(Double),
                    Jornade = reader["activity_jornade_type"] != null ? reader["activity_jornade_type"].ToString() : string.Empty,
                    Employee = new Entity.Shared.Employee { Identifier = employeeId },
                };
                return instance;
            };

            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            //Move to a resource file
            activities = dataProvider.ExecuteReaderWithFilter(SCMS.SRA_SP_GetEmployeeActivitiesInPeriod, filter, null, mapper);

            return activities;
        }

        public List<Entity.SRA.Activity> GetActivitiesToApprove(int employeeID, string projectID)
        {
            if (string.IsNullOrEmpty(SecurityContext.AuthorizationTicket))
            {
                throw new Exception("Token is required.", new ArgumentException("Token cannot be null"));
            }

            if (string.IsNullOrEmpty(SecurityContext.DeviceID))
            {
                throw new Exception("Device is required.", new ArgumentException("Device cannot be null"));
            }


            List<Entity.SRA.Activity> activities = new List<Entity.SRA.Activity>();
            Func<object, List<System.Data.IDataParameter>> filter = (e) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();

                System.Data.IDataParameter p1 = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p1.Direction = System.Data.ParameterDirection.Input;
                p1.DbType = System.Data.DbType.String;

                parameters.Add(p1);

                System.Data.IDataParameter p2 = dataProvider.GetParameter("@project_id", projectID);
                p2.Direction = System.Data.ParameterDirection.Input;
                p2.DbType = System.Data.DbType.String;

                parameters.Add(p2);

                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.SRA.Activity> mapper = (reader) =>
            {
                DateTime dateTimeHelper;
                Double doubleHelper;
                long longHelper;
                int intHelper;

                SOFTTEK.SCMS.Entity.SRA.Activity instance = new SOFTTEK.SCMS.Entity.SRA.Activity
                {
                    Identifier = reader["activity_id"] != null ? (long.TryParse(reader["activity_id"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    ActivityCode = reader["activity_code"] != null ? reader["activity_code"].ToString() : string.Empty,
                    Details = reader["activity_description"] != null ? reader["activity_description"].ToString() : string.Empty,
                    Project = reader["activity_project"] != null ? reader["activity_project"].ToString() : string.Empty,
                    State = reader["activity_status"] != null ? reader["activity_status"].ToString() : string.Empty,
                    ExecutedAt = reader["activity_executed_at"] != null ? (DateTime.TryParse(reader["activity_executed_at"].ToString(), out dateTimeHelper) ? dateTimeHelper : default(DateTime)) : default(DateTime),
                    ReportedAt = reader["activity_reported_at"] != null ? (DateTime.TryParse(reader["activity_reported_at"].ToString(), out dateTimeHelper) ? dateTimeHelper : default(DateTime)) : default(DateTime),
                    ApprovedAt = reader["activity_validated_at"] != null ? (DateTime.TryParse(reader["activity_validated_at"].ToString(), out dateTimeHelper) ? dateTimeHelper : default(DateTime)) : default(DateTime),
                    Effort = reader["activity_effort"] != null ? (Double.TryParse(reader["activity_effort"].ToString(), out doubleHelper) ? doubleHelper : default(Double)) : default(Double),
                    Jornade = reader["activity_jornade_type"] != null ? reader["activity_jornade_type"].ToString() : string.Empty,
                    Employee = new Entity.Shared.Employee { Identifier = reader["activity_employee"] != null ? (int.TryParse(reader["activity_employee"].ToString(), out intHelper) ? intHelper : -1) : -1 },
                };
                return instance;
            };

            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            //Move to a resource file
            activities = dataProvider.ExecuteReaderWithFilter(SCMS.SRA_SP_GetEmployeeActivitiesForApproval, filter, null, mapper);

            return activities;
        }

        public Entity.SRA.Activity GetActivityByID(long activityID)
        {
            throw new NotImplementedException();
        }


        #region Permits And Absences
        public List<Entity.SRA.PermitsAndAbsences> GetPermitsAndAbsences(int employeeId)
        {
            if (string.IsNullOrEmpty(SecurityContext.AuthorizationTicket))
            {
                throw new Exception("Token is required.", new ArgumentException("Token cannot be null"));
            }

            if (string.IsNullOrEmpty(SecurityContext.DeviceID))
            {
                throw new Exception("Device is required.", new ArgumentException("Device cannot be null"));
            }


            List<Entity.SRA.PermitsAndAbsences> activities = new List<Entity.SRA.PermitsAndAbsences>();
            Func<object, List<System.Data.IDataParameter>> filter = (e) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();

                System.Data.IDataParameter p1 = dataProvider.GetParameter("@employee_id", employeeId);
                p1.Direction = System.Data.ParameterDirection.Input;
                p1.DbType = System.Data.DbType.Int32;

                parameters.Add(p1);

                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.SRA.PermitsAndAbsences> mapper = (reader) =>
            {
                DateTime dateTimeHelper;
                Double doubleHelper;
                long longHelper;
                int intHelper;
                SOFTTEK.SCMS.Entity.SRA.PermitsAndAbsences instance = new SOFTTEK.SCMS.Entity.SRA.PermitsAndAbsences
                {
                    perabs_id = reader["perabs_id"] != null ? (long.TryParse(reader["perabs_id"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    perabs_activity_code = reader["perabs_activity_code"] != null ? (int.TryParse(reader["perabs_activity_code"].ToString(), out intHelper) ? intHelper : default(int)) : default(int),
                    perabs_start_at = reader["perabs_start_at"] != null ? (DateTime.TryParse(reader["perabs_start_at"].ToString(), out dateTimeHelper) ? dateTimeHelper : default(DateTime)) : default(DateTime),
                    perabs_end_at = reader["perabs_end_at"] != null ? (DateTime.TryParse(reader["perabs_end_at"].ToString(), out dateTimeHelper) ? dateTimeHelper : default(DateTime)) : default(DateTime),
                    perabs_total_hours = reader["perabs_total_hours"] != null ? (double.TryParse(reader["perabs_total_hours"].ToString(), out doubleHelper) ? doubleHelper : default(double)) : default(double),
                    perabs_validated_by = reader["perabs_validated_by"] != null ? reader["perabs_validated_by"].ToString() : string.Empty,
                    perabs_description = reader["perabs_description"] != null ? reader["perabs_description"].ToString() : string.Empty,
                    perabs_created_at = reader["perabs_created_at"] != null ? (DateTime.TryParse(reader["perabs_created_at"].ToString(), out dateTimeHelper) ? dateTimeHelper : default(DateTime)) : default(DateTime),
                    perabs_created_by = reader["perabs_created_by"] != null ? reader["perabs_created_by"].ToString() : string.Empty,
                    perabs_modified_at = reader["perabs_modified_at"] != null ? (DateTime.TryParse(reader["perabs_modified_at"].ToString(), out dateTimeHelper) ? dateTimeHelper : default(DateTime)) : default(DateTime),
                    perabs_modified_by = reader["perabs_modified_by"] != null ? reader["perabs_modified_by"].ToString() : string.Empty,
                    perabs_validated_comments = reader["perabs_validated_comments"] != null ? reader["perabs_validated_comments"].ToString() : string.Empty,
                    perabs_employee = new Entity.Shared.Employee { Identifier = employeeId },
                    perabs_validated_at = reader["perabs_validated_at"] != null ? (DateTime.TryParse(reader["perabs_validated_at"].ToString(), out dateTimeHelper) ? dateTimeHelper : default(DateTime)) : default(DateTime),
                    perabs_Status = reader["perabs_Status"] != null ? (reader["perabs_Status"].ToString()) : string.Empty,
                };
                return instance;
            };

            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            //Move to a resource file
            activities = dataProvider.ExecuteReaderWithFilter(SCMS.SRA_SP_EmployeeIdForPermitsAndAbsences, filter, null, mapper);

            return activities;
        }
        #endregion

        #region InsertPermitsAndAbsences
        public List<SOFTTEK.SCMS.Entity.SRA.PermitsAndAbsences> RegisterPermitsAndAbsences(SOFTTEK.SCMS.Entity.SRA.PermitsAndAbsences permitsAndAbsences)
        {
            if (string.IsNullOrEmpty(SecurityContext.AuthorizationTicket))
            {
                throw new Exception("Token is required.", new ArgumentException("Token cannot be null"));
            }

            if (string.IsNullOrEmpty(SecurityContext.DeviceID))
            {
                throw new Exception("Device is required.", new ArgumentException("Device cannot be null"));
            }

            List<Entity.SRA.PermitsAndAbsences> registeredActivity = new List<Entity.SRA.PermitsAndAbsences>();
            Func<Entity.SRA.PermitsAndAbsences, List<System.Data.IDataParameter>> filter = (e) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();

                System.Data.IDataParameter p1 = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p1.Direction = System.Data.ParameterDirection.Input;
                p1.DbType = System.Data.DbType.String;

                parameters.Add(p1);

                System.Data.IDataParameter p2 = dataProvider.GetParameter("@perabs_activity_code", e.perabs_activity_code);
                p2.Direction = System.Data.ParameterDirection.Input;
                p2.DbType = System.Data.DbType.String;

                parameters.Add(p2);

                System.Data.IDataParameter p3 = dataProvider.GetParameter("@perabs_start_at", e.perabs_start_at);
                p3.Direction = System.Data.ParameterDirection.Input;
                p3.DbType = System.Data.DbType.DateTime;

                parameters.Add(p3);

                System.Data.IDataParameter p4 = dataProvider.GetParameter("@perabs_end_at", e.perabs_end_at);
                p4.Direction = System.Data.ParameterDirection.Input;
                p4.DbType = System.Data.DbType.DateTime;

                parameters.Add(p4);

                System.Data.IDataParameter p5 = dataProvider.GetParameter("@perabs_total_hours", e.perabs_total_hours);
                p5.Direction = System.Data.ParameterDirection.Input;
                p5.DbType = System.Data.DbType.Double;

                parameters.Add(p5);

                System.Data.IDataParameter p6 = dataProvider.GetParameter("@perabs_description", e.perabs_description);
                p6.Direction = System.Data.ParameterDirection.Input;
                p6.DbType = System.Data.DbType.String;

                parameters.Add(p6);
                
                System.Data.IDataParameter p7 = dataProvider.GetParameter("@perabs_created_by", e.perabs_created_by);
                p7.Direction = System.Data.ParameterDirection.Input;
                p7.DbType = System.Data.DbType.String;

                parameters.Add(p7);
                
                System.Data.IDataParameter p8 = dataProvider.GetParameter("@perabs_employee", e.perabs_employee.Identifier);
                p8.Direction = System.Data.ParameterDirection.Input;
                p8.DbType = System.Data.DbType.Int64;

                parameters.Add(p8);

                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.SRA.PermitsAndAbsences> mapper = (reader) =>
            {
                DateTime dateTimeHelper;
                Double doubleHelper;
                long longHelper;
                int intHelper;


                SOFTTEK.SCMS.Entity.SRA.PermitsAndAbsences instance = new SOFTTEK.SCMS.Entity.SRA.PermitsAndAbsences
                {
                    perabs_id = reader["perabs_id"] != null ? (long.TryParse(reader["perabs_id"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    perabs_activity_code = reader["perabs_activity_code"] != null ? (int.TryParse(reader["perabs_activity_code"].ToString(), out intHelper) ? intHelper : default(int)) : default(int),
                    perabs_start_at = reader["perabs_start_at"] != null ? (DateTime.TryParse(reader["perabs_start_at"].ToString(), out dateTimeHelper) ? dateTimeHelper : default(DateTime)) : default(DateTime),
                    perabs_end_at = reader["perabs_end_at"] != null ? (DateTime.TryParse(reader["perabs_end_at"].ToString(), out dateTimeHelper) ? dateTimeHelper : default(DateTime)) : default(DateTime),
                    perabs_total_hours = reader["perabs_total_hours"] != null ? (double.TryParse(reader["perabs_total_hours"].ToString(), out doubleHelper) ? doubleHelper : default(double)) : default(double),
                    perabs_validated_by = reader["perabs_validated_by"] != null ? reader["perabs_validated_by"].ToString() : string.Empty,
                    perabs_description = reader["perabs_description"] != null ? reader["perabs_description"].ToString() : string.Empty,
                    perabs_created_at = reader["perabs_created_at"] != null ? (DateTime.TryParse(reader["perabs_created_at"].ToString(), out dateTimeHelper) ? dateTimeHelper : default(DateTime)) : default(DateTime),
                    perabs_created_by = reader["perabs_created_by"] != null ? reader["perabs_created_by"].ToString() : string.Empty,
                    perabs_modified_at = reader["perabs_modified_at"] != null ? (DateTime.TryParse(reader["perabs_modified_at"].ToString(), out dateTimeHelper) ? dateTimeHelper : default(DateTime)) : default(DateTime),
                    perabs_modified_by = reader["perabs_modified_by"] != null ? reader["perabs_modified_by"].ToString() : string.Empty,
                    perabs_validated_comments = reader["perabs_validated_comments"] != null ? reader["perabs_validated_comments"].ToString() : string.Empty,
                    perabs_employee = new Entity.Shared.Employee { Identifier = reader["perabs_employee"] != null ? (int.TryParse(reader["perabs_employee"].ToString(), out intHelper) ? intHelper : -1) : -1 },
                    perabs_validated_at = reader["perabs_validated_at"] != null ? (DateTime.TryParse(reader["perabs_validated_at"].ToString(), out dateTimeHelper) ? dateTimeHelper : default(DateTime)) : default(DateTime),
                    perabs_Status = reader["perabs_Status"] != null ? (reader["perabs_Status"].ToString()) : string.Empty,
                };
                return instance;
            };

            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;

            registeredActivity = dataProvider.ExecuteReaderWithFilter(SCMS.SRA_SP_Insert_PermitsAndAbsences, filter, permitsAndAbsences, mapper);

            return registeredActivity;
        }
        #endregion

        #region UpdatePermitsAndbsences
        public List<SOFTTEK.SCMS.Entity.SRA.PermitsAndAbsences> UpdatePermitsAndAbsences(SOFTTEK.SCMS.Entity.SRA.PermitsAndAbsences permitsAndAbsences)
        {
            if (string.IsNullOrEmpty(SecurityContext.AuthorizationTicket))
            {
                throw new Exception("Token is required.", new ArgumentException("Token cannot be null"));
            }

            if (string.IsNullOrEmpty(SecurityContext.DeviceID))
            {
                throw new Exception("Device is required.", new ArgumentException("Device cannot be null"));
            }

            List<Entity.SRA.PermitsAndAbsences> registeredActivity = new List<Entity.SRA.PermitsAndAbsences>();
            Func<Entity.SRA.PermitsAndAbsences, List<System.Data.IDataParameter>> filter = (e) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();

                System.Data.IDataParameter p = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p.Direction = System.Data.ParameterDirection.Input;
                p.DbType = System.Data.DbType.String;

                parameters.Add(p);

                System.Data.IDataParameter p1 = dataProvider.GetParameter("@perabs_id", e.perabs_id);
                p1.Direction = System.Data.ParameterDirection.Input;
                p1.DbType = System.Data.DbType.Int32;

                parameters.Add(p1);

                System.Data.IDataParameter p2 = dataProvider.GetParameter("@perabs_activity_code", e.perabs_activity_code);
                p2.Direction = System.Data.ParameterDirection.Input;
                p2.DbType = System.Data.DbType.String;

                parameters.Add(p2);

                System.Data.IDataParameter p3 = dataProvider.GetParameter("@perabs_start_at", e.perabs_start_at);
                p3.Direction = System.Data.ParameterDirection.Input;
                p3.DbType = System.Data.DbType.DateTime;

                parameters.Add(p3);

                System.Data.IDataParameter p4 = dataProvider.GetParameter("@perabs_end_at", e.perabs_end_at);
                p4.Direction = System.Data.ParameterDirection.Input;
                p4.DbType = System.Data.DbType.DateTime;

                parameters.Add(p4);

                System.Data.IDataParameter p5 = dataProvider.GetParameter("@perabs_total_hours", e.perabs_total_hours);
                p5.Direction = System.Data.ParameterDirection.Input;
                p5.DbType = System.Data.DbType.Double;

                parameters.Add(p5);

                System.Data.IDataParameter p6 = dataProvider.GetParameter("@perabs_validated_by", e.perabs_validated_by);
                p6.Direction = System.Data.ParameterDirection.Input;
                p6.DbType = System.Data.DbType.String;

                parameters.Add(p6);

                System.Data.IDataParameter p7 = dataProvider.GetParameter("@perabs_description", e.perabs_description);
                p7.Direction = System.Data.ParameterDirection.Input;
                p7.DbType = System.Data.DbType.String;

                parameters.Add(p7);

                System.Data.IDataParameter p8 = dataProvider.GetParameter("@perabs_created_at", e.perabs_created_at);
                p8.Direction = System.Data.ParameterDirection.Input;
                p8.DbType = System.Data.DbType.DateTime;

                parameters.Add(p8);

                System.Data.IDataParameter p9 = dataProvider.GetParameter("@perabs_created_by", e.perabs_created_by);
                p9.Direction = System.Data.ParameterDirection.Input;
                p9.DbType = System.Data.DbType.String;

                parameters.Add(p9);

                System.Data.IDataParameter p10 = dataProvider.GetParameter("@perabs_modified_at", e.perabs_modified_at);
                p10.Direction = System.Data.ParameterDirection.Input;
                p10.DbType = System.Data.DbType.DateTime;

                parameters.Add(p10);

                System.Data.IDataParameter p11 = dataProvider.GetParameter("@perabs_modified_by", e.perabs_modified_by);
                p11.Direction = System.Data.ParameterDirection.Input;
                p11.DbType = System.Data.DbType.String;

                parameters.Add(p11);

                System.Data.IDataParameter p12 = dataProvider.GetParameter("@perabs_validated_comments", e.perabs_validated_comments);
                p12.Direction = System.Data.ParameterDirection.Input;
                p12.DbType = System.Data.DbType.String;

                parameters.Add(p12);

                System.Data.IDataParameter p13 = dataProvider.GetParameter("@perabs_employee", e.perabs_employee.Identifier);
                p13.Direction = System.Data.ParameterDirection.Input;
                p13.DbType = System.Data.DbType.Int64;

                parameters.Add(p13);

                System.Data.IDataParameter p14 = dataProvider.GetParameter("@perabs_validated_at", e.perabs_validated_at);
                p14.Direction = System.Data.ParameterDirection.Input;
                p14.DbType = System.Data.DbType.DateTime;

                parameters.Add(p14);

                System.Data.IDataParameter p15 = dataProvider.GetParameter("@perabs_Status", e.perabs_Status);
                p15.Direction = System.Data.ParameterDirection.Input;
                p15.DbType = System.Data.DbType.String;

                parameters.Add(p15);

                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.SRA.PermitsAndAbsences> mapper = (reader) =>
            {
                DateTime dateTimeHelper;
                Double doubleHelper;
                long longHelper;
                int intHelper;


                SOFTTEK.SCMS.Entity.SRA.PermitsAndAbsences instance = new SOFTTEK.SCMS.Entity.SRA.PermitsAndAbsences
                {
                    perabs_id = reader["perabs_id"] != null ? (long.TryParse(reader["perabs_id"].ToString(), out longHelper) ? longHelper : -1) : -1,
                    perabs_activity_code = reader["perabs_activity_code"] != null ? (int.TryParse(reader["perabs_activity_code"].ToString(), out intHelper) ? intHelper : default(int)) : default(int),
                    perabs_start_at = reader["perabs_start_at"] != null ? (DateTime.TryParse(reader["perabs_start_at"].ToString(), out dateTimeHelper) ? dateTimeHelper : default(DateTime)) : default(DateTime),
                    perabs_end_at = reader["perabs_end_at"] != null ? (DateTime.TryParse(reader["perabs_end_at"].ToString(), out dateTimeHelper) ? dateTimeHelper : default(DateTime)) : default(DateTime),
                    perabs_total_hours = reader["perabs_total_hours"] != null ? (double.TryParse(reader["perabs_total_hours"].ToString(), out doubleHelper) ? doubleHelper : default(double)) : default(double),
                    perabs_validated_by = reader["perabs_validated_by"] != null ? reader["perabs_validated_by"].ToString() : string.Empty,
                    perabs_description = reader["perabs_description"] != null ? reader["perabs_description"].ToString() : string.Empty,
                    perabs_created_at = reader["perabs_created_at"] != null ? (DateTime.TryParse(reader["perabs_created_at"].ToString(), out dateTimeHelper) ? dateTimeHelper : default(DateTime)) : default(DateTime),
                    perabs_created_by = reader["perabs_created_by"] != null ? reader["perabs_created_by"].ToString() : string.Empty,
                    perabs_modified_at = reader["perabs_modified_at"] != null ? (DateTime.TryParse(reader["perabs_modified_at"].ToString(), out dateTimeHelper) ? dateTimeHelper : default(DateTime)) : default(DateTime),
                    perabs_modified_by = reader["perabs_modified_by"] != null ? reader["perabs_modified_by"].ToString() : string.Empty,
                    perabs_validated_comments = reader["perabs_validated_comments"] != null ? reader["perabs_validated_comments"].ToString() : string.Empty,
                    perabs_employee = new Entity.Shared.Employee { Identifier = reader["perabs_employee"] != null ? (int.TryParse(reader["perabs_employee"].ToString(), out intHelper) ? intHelper : -1) : -1 },
                    perabs_validated_at = reader["perabs_validated_at"] != null ? (DateTime.TryParse(reader["perabs_validated_at"].ToString(), out dateTimeHelper) ? dateTimeHelper : default(DateTime)) : default(DateTime),
                    perabs_Status = reader["perabs_Status"] != null ? (reader["perabs_Status"].ToString()) : string.Empty,
                };
                return instance;
            };

            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;

            registeredActivity = dataProvider.ExecuteReaderWithFilter(SCMS.SRA_SP_Update_PermitsAndAbsences, filter, permitsAndAbsences, mapper);

            return registeredActivity;
        }
        #endregion


        public Entity.Shared.Parameter<T> RegisterParameterForCategory<T>(string categoryName, Entity.Shared.Parameter<T> parameter)
        {
            if (string.IsNullOrEmpty(SecurityContext.AuthorizationTicket))
            {
                throw new Exception("Token is required.", new ArgumentException("Token cannot be null"));
            }

            if (string.IsNullOrEmpty(SecurityContext.DeviceID))
            {
                throw new Exception("Device is required.", new ArgumentException("Device cannot be null"));
            }


            Entity.Shared.Parameter<T> registeredParameter = new Entity.Shared.Parameter<T>();
            Func<object, List<System.Data.IDataParameter>> filter = (e) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();

                System.Data.IDataParameter p1 = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p1.Direction = System.Data.ParameterDirection.Input;
                p1.DbType = System.Data.DbType.String;

                parameters.Add(p1);

                System.Data.IDataParameter p2 = dataProvider.GetParameter("@param_category", categoryName);
                p2.Direction = System.Data.ParameterDirection.Input;
                p2.DbType = System.Data.DbType.String;

                parameters.Add(p2);

                System.Data.IDataParameter p3 = dataProvider.GetParameter("@param_value", parameter.Value.ToString());
                p3.Direction = System.Data.ParameterDirection.Input;
                p3.DbType = System.Data.DbType.String;

                parameters.Add(p3);

                System.Data.IDataParameter p4 = dataProvider.GetParameter("@param_description", parameter.Description);
                p4.Direction = System.Data.ParameterDirection.Input;
                p4.DbType = System.Data.DbType.String;

                parameters.Add(p4);

                System.Data.IDataParameter p5 = dataProvider.GetParameter("@param_comments", parameter.Comments);
                p5.Direction = System.Data.ParameterDirection.Input;
                p5.DbType = System.Data.DbType.String;

                parameters.Add(p5);

                System.Data.IDataParameter p6 = dataProvider.GetParameter("@param_external_id", parameter.ExternalIdentifier);
                p6.Direction = System.Data.ParameterDirection.Input;
                p6.DbType = System.Data.DbType.String;

                parameters.Add(p6);

                System.Data.IDataParameter p7 = dataProvider.GetParameter("@param_order", parameter.Order);
                p7.Direction = System.Data.ParameterDirection.Input;
                p7.DbType = System.Data.DbType.Int32;

                parameters.Add(p7);

                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.Shared.Parameter<T>> mapper = (reader) =>
            {
                /*DateTime dateTimeHelper;
                Double doubleHelper;
                long longHelper;*/
                int intHelper;
                

                SOFTTEK.SCMS.Entity.Shared.Category category = new Entity.Shared.Category
                {
                    Name = categoryName,
                    Identifier = reader["v_category_id"] != null ? (int.TryParse(reader["v_category_id"].ToString(), out intHelper) ? intHelper : -1) : -1
                };


                SOFTTEK.SCMS.Entity.Shared.Parameter<T> instance = new SOFTTEK.SCMS.Entity.Shared.Parameter<T>
                {
                    Identifier = reader["v_id"] != null ? (int.TryParse(reader["v_id"].ToString(), out intHelper) ? intHelper : -1) : -1,
                    Value = reader["v_value"] != null ? (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(reader["v_value"].ToString()) : default(T),
                    Description = reader["v_description"] != null ? reader["v_description"].ToString() : string.Empty,
                    Comments = reader["v_comments"] != null ? reader["v_comments"].ToString() : string.Empty,
                    ExternalIdentifier = reader["v_external_id"] != null ? reader["v_external_id"].ToString() : string.Empty,
                    Category = category,
                    Order = reader["v_order"] != null ? (int.TryParse(reader["v_order"].ToString(), out intHelper) ? intHelper : -1) : -1,
                    IsActive = true
                };
                return instance;
            };

            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            //Move to a resource file
            registeredParameter = dataProvider.ExecuteForEntityWithFilter(SCMS.SCMS_SP_AddParameterForCategory, filter, null, mapper);

            return registeredParameter;
        }

        public Entity.Shared.Parameter<T> UpdateParameterForCategory<T>(string categoryName, Entity.Shared.Parameter<T> parameter)
        {
            if (string.IsNullOrEmpty(SecurityContext.AuthorizationTicket))
            {
                throw new Exception("Token is required.", new ArgumentException("Token cannot be null"));
            }

            if (string.IsNullOrEmpty(SecurityContext.DeviceID))
            {
                throw new Exception("Device is required.", new ArgumentException("Device cannot be null"));
            }


            Entity.Shared.Parameter<T> updatedParameter = new Entity.Shared.Parameter<T>();
            Func<object, List<System.Data.IDataParameter>> filter = (e) =>
            {
                List<System.Data.IDataParameter> parameters = new List<System.Data.IDataParameter>();

                System.Data.IDataParameter p1 = dataProvider.GetParameter("@token_id", SecurityContext.AuthorizationTicket);
                p1.Direction = System.Data.ParameterDirection.Input;
                p1.DbType = System.Data.DbType.String;

                parameters.Add(p1);

                System.Data.IDataParameter p2 = dataProvider.GetParameter("@param_category", categoryName);
                p2.Direction = System.Data.ParameterDirection.Input;
                p2.DbType = System.Data.DbType.String;

                parameters.Add(p2);

                System.Data.IDataParameter p3 = dataProvider.GetParameter("@param_id", parameter.Identifier);
                p3.Direction = System.Data.ParameterDirection.Input;
                p3.DbType = System.Data.DbType.Int32;

                parameters.Add(p3);

                System.Data.IDataParameter p4 = dataProvider.GetParameter("@param_external_id", parameter.ExternalIdentifier);
                p4.Direction = System.Data.ParameterDirection.Input;
                p4.DbType = System.Data.DbType.String;

                parameters.Add(p4);

                System.Data.IDataParameter p5 = dataProvider.GetParameter("@param_value", parameter.Value.ToString());
                p5.Direction = System.Data.ParameterDirection.Input;
                p5.DbType = System.Data.DbType.String;

                parameters.Add(p5);

                System.Data.IDataParameter p6 = dataProvider.GetParameter("@param_description", parameter.Description);
                p6.Direction = System.Data.ParameterDirection.Input;
                p6.DbType = System.Data.DbType.String;

                parameters.Add(p6);

                System.Data.IDataParameter p7 = dataProvider.GetParameter("@param_comments", parameter.Comments);
                p7.Direction = System.Data.ParameterDirection.Input;
                p7.DbType = System.Data.DbType.String;

                parameters.Add(p7);

                System.Data.IDataParameter p8 = dataProvider.GetParameter("@param_order", parameter.Order);
                p8.Direction = System.Data.ParameterDirection.Input;
                p8.DbType = System.Data.DbType.Int32;

                parameters.Add(p8);

                System.Data.IDataParameter p9 = dataProvider.GetParameter("@param_is_active", parameter.IsActive);
                p9.Direction = System.Data.ParameterDirection.Input;
                p9.DbType = System.Data.DbType.Boolean;

                parameters.Add(p9);


                return parameters;
            };

            Func<System.Data.IDataReader, SOFTTEK.SCMS.Entity.Shared.Parameter<T>> mapper = (reader) =>
            {
                /*DateTime dateTimeHelper;
                Double doubleHelper;
                long longHelper;*/
                int intHelper;
                bool boolHelper;

                SOFTTEK.SCMS.Entity.Shared.Category category = new Entity.Shared.Category
                {
                    Name = categoryName,
                    Identifier = reader["v_category_id"] != null ? (int.TryParse(reader["v_category_id"].ToString(), out intHelper) ? intHelper : -1) : -1
                };


                SOFTTEK.SCMS.Entity.Shared.Parameter<T> instance = new SOFTTEK.SCMS.Entity.Shared.Parameter<T>
                {
                    Identifier = reader["v_id"] != null ? (int.TryParse(reader["v_id"].ToString(), out intHelper) ? intHelper : -1) : -1,
                    Value = reader["v_value"] != null ? (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(reader["v_value"].ToString()) : default(T),
                    Description = reader["v_description"] != null ? reader["v_description"].ToString() : string.Empty,
                    Comments = reader["v_comments"] != null ? reader["v_comments"].ToString() : string.Empty,
                    ExternalIdentifier = reader["v_external_id"] != null ? reader["v_external_id"].ToString() : string.Empty,
                    Category = category,
                    Order = reader["v_order"] != null ? (int.TryParse(reader["v_order"].ToString(), out intHelper) ? intHelper : -1) : -1,
                    IsActive = reader["v_is_active"] != null ? (bool.TryParse(reader["v_is_active"].ToString(), out boolHelper) ? boolHelper : false) : false
                };
                return instance;
            };

            dataProvider.CommandType = System.Data.CommandType.StoredProcedure;
            //Move to a resource file
            updatedParameter = dataProvider.ExecuteForEntityWithFilter(SCMS.SCMS_SP_UpdateParameterForCategory, filter, null, mapper);

            return updatedParameter;
        }
    }
}
