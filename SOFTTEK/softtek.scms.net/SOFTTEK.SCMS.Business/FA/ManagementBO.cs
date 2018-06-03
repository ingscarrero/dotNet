using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFTTEK.SCMS.Business.FA
{
    enum RequestType
    {
        CustomerRequest,
        NoveltyRequest,
        PurchaseRequest,
        TransferRequest,
        RetirementRequest,
        TechnicalEvaluationRequest
    }
    public class ManagementBO:Foundation.Business.BusinessObject
    {


        private const string kFAMCommitedStatus = "Commited";
        private const string kFAMScheduledStatus = "Scheduled";
        private const string kFAMRegisteredStatus = "Registered";
        private const string kFAMAssignedStatus = "Assigned";
        private const string kFAMInProcessStatus = "In process";
        private const string kFAMWaitingForCustomerStatus = "Waiting for customer";
        private const string kFAMClosedStatus = "Closed";

        private const string kFAMAvailableProviderStatus = "Available";

        private const int kFAMDaysBetweenInteractionLimit = 3;

        private SOFTTEK.SCMS.Data.FAMDataContext dataSource;

        public ManagementBO(Foundation.Business.BusinessContext ctx)
            : base(ctx)
        {
           
        }

        #region Physical Inventory Taking
        /// <summary>
        /// Retrieves all available Physical Inventory Taking Providers
        /// </summary>
        /// <returns>List of matching Physical Inventory Taking Providers</returns>
        public List<SOFTTEK.SCMS.Entity.FA.Provider> RetrievePhysicalInventoryTakingProviders() {
            return context.Execute(() =>
            {
                SOFTTEK.SCMS.Entity.FA.Provider filter = new SCMS.Entity.FA.Provider
                {
                    State = kFAMAvailableProviderStatus
                };
                List<SOFTTEK.SCMS.Entity.FA.Provider> providers;

                using (dataSource = new Data.FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    providers = dataSource.GetProviders(filter).ToList();
                }

                return providers;
            }, "Retrive available Provider for Physical Inventory Taking Schedule.");

        }

        /// <summary>
        /// Retrieves all Work Orders for a provider for a provided period.
        /// </summary>
        /// <param name="providerIdentifier">Provider Identifier</param>
        /// <param name="from">Starting Shcedule Date</param>
        /// <param name="to">Ending Schedule Date</param>
        /// <returns>List of matching Physical Inventory Taking Work Orders</returns>
        public List<SOFTTEK.SCMS.Entity.FA.WorkOrder> WorkOrdersByProviderInPeriod(long providerIdentifier, DateTime from, DateTime to) {
            return context.Execute(() => {
                List<SOFTTEK.SCMS.Entity.FA.WorkOrder> workOrders;

                SOFTTEK.SCMS.Entity.FA.WorkOrder filter = new SCMS.Entity.FA.WorkOrder
                {
                    Provider = new SCMS.Entity.FA.Provider { Identifier = providerIdentifier },
                };

                using (dataSource = new Data.FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    workOrders = dataSource.GetWorkOrders(filter).Where(wo => wo.ScheduledTo.Date.CompareTo(from.Date) >= 0 && wo.ScheduledTo.Date.CompareTo(to.Date) < 0).ToList();
                }

                return workOrders;
            }, "Retrieve Provider Work Orders In Period");
        }

        /// <summary>
        /// Create a Work Order and an associated Physical Inventory Taking 
        /// </summary>
        /// <param name="workOrderInformation">Work Order an Physical Inventory Informtion</param>
        /// <returns>Registered Work Order and its details</returns>
        public SOFTTEK.SCMS.Entity.FA.WorkOrder CreateWorkOrder(SOFTTEK.SCMS.Entity.FA.WorkOrder workOrderInformation) {
            return context.Execute(() =>
            {
                SOFTTEK.SCMS.Entity.FA.WorkOrder submittedWorkOrder;
                workOrderInformation.Status = kFAMRegisteredStatus;
                workOrderInformation.IssuedAt = DateTime.Now;
                

                workOrderInformation.PhysicalInventoryTaking.Status = kFAMRegisteredStatus;
                workOrderInformation.PhysicalInventoryTaking.RegisteredAt = DateTime.Now;
                workOrderInformation.PhysicalInventoryTaking.Comments = string.Format("[{0:dd/MM/yyyy HH:mm:ss}]:{1}|{2}-{3}", 
                    DateTime.Now, 
                    workOrderInformation.PhysicalInventoryTaking.Status, 
                    workOrderInformation.Provider,
                    "Waiting for Provider Confirmation");
                

                using (dataSource = new Data.FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    submittedWorkOrder = dataSource.InsertWorkOrder(workOrderInformation);
                    
                    workOrderInformation.PhysicalInventoryTaking.WorkOrder.Identifier = submittedWorkOrder.Identifier;
                    submittedWorkOrder.PhysicalInventoryTaking = dataSource.InsertPhysicalInventoryTaking(workOrderInformation.PhysicalInventoryTaking);
                }

                return submittedWorkOrder;
            }, string.Format("Create Work Order for Physical Inventory Taking issued to provider [{1}]", workOrderInformation.Provider));

        }

        public SOFTTEK.SCMS.Entity.FA.WorkOrder AcceptWorkOrder(SOFTTEK.SCMS.Entity.FA.WorkOrder workOrderInformation) {
            return context.Execute(() =>
            {
                SOFTTEK.SCMS.Entity.FA.WorkOrder submittedWorkOrder;
                

                using (dataSource = new Data.FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();

                    workOrderInformation.Status = kFAMAssignedStatus;
                    submittedWorkOrder = dataSource.InsertWorkOrder(workOrderInformation);
                    
                    submittedWorkOrder.PhysicalInventoryTaking = dataSource.GetPhysicalInventoryTakings(workOrderInformation.PhysicalInventoryTaking).FirstOrDefault();
                    submittedWorkOrder.PhysicalInventoryTaking.Comments = string.Concat(submittedWorkOrder.PhysicalInventoryTaking.Comments,
                        string.Format("\n[{0:dd/MM/yyyy HH:mm:ss}]:{1}=>{2}|{3}-{4}", 
                            DateTime.Now, 
                            workOrderInformation.PhysicalInventoryTaking.Status,
                            kFAMScheduledStatus, 
                            workOrderInformation.Provider,
                            string.Format("Accepted by the provider. The Physical Inventory Taking has been scheduled for {0:dd/MM/yyyy}", submittedWorkOrder.ScheduledTo )));
                    submittedWorkOrder.PhysicalInventoryTaking.Status = kFAMScheduledStatus;
                    submittedWorkOrder.PhysicalInventoryTaking.UpdatedAt = DateTime.Now;

                    submittedWorkOrder.PhysicalInventoryTaking = dataSource.UpdatePhysicalInventoryTaking(submittedWorkOrder.PhysicalInventoryTaking, workOrderInformation.PhysicalInventoryTaking);
                }

                return submittedWorkOrder;
            }, string.Format("Accept a Work Order [{0}] for Physical Inventory Taking [{1}] by the Provider [{2}]",
                    workOrderInformation.Identifier,
                    workOrderInformation.PhysicalInventoryTaking.Identifier,
                    workOrderInformation.Provider)
            );

        }

        public List<SOFTTEK.SCMS.Entity.FA.WorkOrder> GetScheduleInPeriod(DateTime from, DateTime to) {
            return context.Execute(() =>
            {
                List<SOFTTEK.SCMS.Entity.FA.WorkOrder> events;

                SOFTTEK.SCMS.Entity.FA.WorkOrder filter = new SCMS.Entity.FA.WorkOrder {
                    Status = kFAMAssignedStatus
                };

                using (dataSource = new Data.FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    events = dataSource.GetWorkOrders(filter).Where(wo => wo.ScheduledTo.Date.CompareTo(from.Date) >= 0 && wo.ScheduledTo.Date.CompareTo(to.Date) < 0).ToList();
                }

                return events;
            }, string.Format("Retrieve Physical Inventory Taking Work Orders Scheduled between {0:dd/MM/yyyy} to {1:dd/MM/yyyy}", from, to));
        
        }

        public List<SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking> GetPhysicalInventoryTakings(SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking filter)
        {
            return context.Execute(() =>
            {
                List<SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking> events;

                using (dataSource = new Data.FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    events = dataSource.GetPhysicalInventoryTakings(filter).ToList();
                }

                return events;
            }, "Retrieve Physical Inventory Taking filtering by criteria");
        }

        public List<SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking> GetPhysicalInventoryTakingsInPeriod(SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking filter, DateTime from, DateTime to)
        {
            return context.Execute(() =>
            {
                List<SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking> events = GetPhysicalInventoryTakings(filter).Where(pit=>{
                    using (dataSource = new Data.FAMDataContext(context.SecurityContext))
                    {
                        dataSource.ConnectionString = "SRA";
                        dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                        dataSource.Initialize();
                        DateTime scheduledTo = dataSource.GetWorkOrders(pit.WorkOrder).FirstOrDefault().ScheduledTo;
                        return scheduledTo.Date.CompareTo(from.Date) >= 0 && scheduledTo.CompareTo(to.Date) < 0;
                    }
                }).ToList();
                
                return events;
            }, string.Format("Retrieve Physical Inventory Taking scheduled between {0:dd/MM/yyyy} to {1:dd/MM/yyyy}", from, to));
        }

        public SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking PerformPhysicalInventoryTaking(SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking eventInformation) { 
            SRA.EmployeeBO employeeBO = new SRA.EmployeeBO(context);
            eventInformation.Responsible = employeeBO.GetEmployeeInfoById(eventInformation.Responsible.Identifier);

            return context.Execute(() =>
            {
                eventInformation.Comments = string.Concat(eventInformation.Comments, string.Format("\n[{0:dd/MM/yyyy HH:mm:ss}]:{1}=>{2}|WO:{3}|P:{4}",
                    DateTime.Now,
                    eventInformation.Status,
                    kFAMInProcessStatus,
                    eventInformation.Responsible.Area,
                    eventInformation.Responsible.Person.FullName));
                eventInformation.Status = kFAMAssignedStatus;
                SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking submmitedEvent;

                using (dataSource = new Data.FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    submmitedEvent = dataSource.UpdatePhysicalInventoryTaking(eventInformation, new SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking { Identifier = eventInformation.Identifier });

                    List<SOFTTEK.SCMS.Entity.FA.FixedAsset> sapFixedAssetsForLocation = GetSAPFixedAssetsForLocation(eventInformation.Location);

                    submmitedEvent.Items = sapFixedAssetsForLocation.Select(fa => new SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTakingItem
                    {
                        FixedAsset = dataSource.GetFixedAssets(fa).FirstOrDefault(),
                        PhysicalInventoryTaking = new SCMS.Entity.FA.PhysicalInventoryTaking { Identifier = eventInformation.Identifier },
                        Responsible = eventInformation.Responsible,
                        Comments = "Waiting for verification"
                    }).ToList();
                }

                
                return submmitedEvent;

            }, string.Format("Submmit Physical Inventory Taking for WO:{0} verified by employee:{1}", eventInformation.WorkOrder.Number, eventInformation.Responsible.Identifier));
        }

        private List<SCMS.Entity.FA.FixedAsset> GetSAPFixedAssetsForLocation(string locationIdentifier)
        {
            List<SCMS.Entity.FA.FixedAsset> locationFixedAssets = new List<SCMS.Entity.FA.FixedAsset>();
            // TODO: SAP Integration to retrieve a location fixed assets information.
            return locationFixedAssets;
        }

        public SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking SubmitPhysicalInventoryTaking(SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking eventInformation)
        {
            SRA.EmployeeBO employeeBO = new SRA.EmployeeBO(context);
            eventInformation.Responsible = employeeBO.GetEmployeeInfoById(eventInformation.Responsible.Identifier);

            return context.Execute(() =>
            {
                eventInformation.Comments = string.Concat(eventInformation.Comments, string.Format("\n[{0:dd/MM/yyyy HH:mm:ss}]:{1}=>{2}|WO:{3}|P:{4}",
                    DateTime.Now,
                    kFAMAssignedStatus,
                    kFAMCommitedStatus,
                    eventInformation.Responsible.Area,
                    eventInformation.Responsible.Person.FullName));
                eventInformation.Status = kFAMAssignedStatus;
                SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking submmitedEvent;

                using (dataSource = new Data.FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    submmitedEvent = dataSource.UpdatePhysicalInventoryTaking(eventInformation, new SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTaking { Identifier = eventInformation.Identifier });
                }
                submmitedEvent.Items = eventInformation.Items.Select(i => SubmitPhysicalInventoryTakingItem(i)).ToList();
                return submmitedEvent;

            }, string.Format("Submmit Physical Inventory Taking for WO:{0} verified by employee:{1}", eventInformation.WorkOrder.Number, eventInformation.Responsible.Identifier));
        }

        private SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTakingItem SubmitPhysicalInventoryTakingItem(SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTakingItem eventItem)
        {
            return context.Execute(() =>
            {
                SOFTTEK.SCMS.Entity.FA.PhysicalInventoryTakingItem submittedEventItem;
                using (dataSource = new Data.FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    submittedEventItem = dataSource.InsertPhysicalInventoryTakingItem(eventItem);
                }
                return submittedEventItem;
            }, string.Format("Submmit Physical Inventory Taking Item for event {0} verified by employee:{1}", eventItem.PhysicalInventoryTaking.Identifier, eventItem.Responsible.Identifier));
        } 
        
        #endregion

        #region Fixed Assets Management

        #region Customer Request

        public SOFTTEK.SCMS.Entity.FA.Request CreateCustomerRequest(SOFTTEK.SCMS.Entity.FA.Request request) {
            return context.Execute(()=>{

                request.Status = kFAMRegisteredStatus;

                SOFTTEK.SCMS.Entity.FA.Request submmitedRequest;
                using (dataSource = new Data.FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    submmitedRequest = dataSource.InsertRequest(request);
                }

                return submmitedRequest;
            }, string.Format("Create Fixed Asset Customer Request from employee:{0}", request.Accountable.Identifier));
        }

        public SOFTTEK.SCMS.Entity.FA.Request AssignCustomerRequest(SOFTTEK.SCMS.Entity.FA.Request request, string comments)
        {
            return context.Execute(() => {
                request.Status = kFAMAssignedStatus;
                request.Comments = string.Concat(request.Comments, string.Format("\n[0:dd/MM/yyyy HH:mm:ss]:{1}=>{2}|{3}-{4}", DateTime.Now, request.Status, kFAMAssignedStatus, request.Responsible.Area, comments));

                SOFTTEK.SCMS.Entity.FA.Request updatedRequest = UpdateCustomerRequest(request);

                return updatedRequest;
            }, string.Format("Assign  Fixed Asset Customer Request from employe:{0} to area:{1}", request.Accountable.Identifier, request.Responsible.Area));
        }

        public SOFTTEK.SCMS.Entity.FA.Request ProcessCustomerRequest(SOFTTEK.SCMS.Entity.FA.Request request)
        {
            return context.Execute(() =>
            {

                request.Status = kFAMInProcessStatus;
                request.Comments = string.Concat(request.Comments, string.Format("\n[0:dd/MM/yyyy HH:mm:ss]:{1}=>{2}|{3}", DateTime.Now, request.Status, kFAMInProcessStatus, request.Responsible.Area));

                SOFTTEK.SCMS.Entity.FA.Request updatedRequest = UpdateCustomerRequest(request);
                return updatedRequest;
            }, string.Format("Process Fixed Asset Customer Request from employe:{0}", request.Accountable.Identifier));
        }

        public SOFTTEK.SCMS.Entity.FA.Request FulfillCustomerRequest(SOFTTEK.SCMS.Entity.FA.Request request, string comments)
        {
            return context.Execute(() =>
            {
                SRA.EmployeeBO employeeBO = new SRA.EmployeeBO(context);

                request.Accountable = employeeBO.GetEmployeeInfoById(request.Accountable.Identifier);

                request.Status = kFAMWaitingForCustomerStatus;
                request.Comments = string.Concat(request.Comments, string.Format("\n[0:dd/MM/yyyy HH:mm:ss]:{1}=>{2}|{3}-{4}", DateTime.Now, request.Status, kFAMWaitingForCustomerStatus, request.Accountable.Person.FullName, comments));

                SOFTTEK.SCMS.Entity.FA.Request updatedRequest = UpdateCustomerRequest(request);
                return updatedRequest;
            }, string.Format("Fulfill Fixed Asset Customer Request from employe:{0}", request.Accountable.Identifier));
        }

        public SOFTTEK.SCMS.Entity.FA.Request CloseCustomerRequest(SOFTTEK.SCMS.Entity.FA.Request request, string comments)
        {
            return context.Execute(() =>
            {
                SRA.EmployeeBO employeeBO = new SRA.EmployeeBO(context);

                request.Accountable = employeeBO.GetEmployeeInfoById(request.Accountable.Identifier);

                if (!request.Accountable.User.Identifier.Equals(context.SecurityContext.ClientID)
                    && request.UpdatedAt.AddDays(kFAMDaysBetweenInteractionLimit).CompareTo(DateTime.Now) < 0)
                {
                    throw new Exception("The Customer Request cannot be closed until the Accountable confirmation or the number of days to close the request has been reached.");
                }

                request.Status = kFAMClosedStatus;
                request.Comments = string.Concat(request.Comments, string.Format("\n[0:dd/MM/yyyy HH:mm:ss]:{1}=>{2}|{3}-{4}", DateTime.Now, request.Status, kFAMClosedStatus, request.Accountable.Person.FullName, comments));

                SOFTTEK.SCMS.Entity.FA.Request updatedRequest = UpdateCustomerRequest(request);
                return updatedRequest;
            }, string.Format("Close Fixed Asset Customer Request from employe:{0}", request.Accountable.Identifier));
        }

        public SOFTTEK.SCMS.Entity.FA.Request RequestCustomerInformationFromCustomerRequest(SOFTTEK.SCMS.Entity.FA.Request request, string comments)
        {
            return context.Execute(() =>
            {
                SRA.EmployeeBO employeeBO = new SRA.EmployeeBO(context);

                request.Accountable = employeeBO.GetEmployeeInfoById(request.Accountable.Identifier);
                request.Status = kFAMWaitingForCustomerStatus;
                request.Comments = string.Concat(request.Comments, string.Format("\n[0:dd/MM/yyyy HH:mm:ss]:{1}=>{2}|{3}-{4}", DateTime.Now, request.Status, kFAMWaitingForCustomerStatus, request.Accountable.Person.FullName, comments));

                SOFTTEK.SCMS.Entity.FA.Request updatedRequest = UpdateCustomerRequest(request);
                return updatedRequest;
            }, string.Format("Assign  Fixed Asset Customer Request from employe:{0} to area:{1}", request.Accountable.Identifier, request.Responsible.Area));
        }

        private SOFTTEK.SCMS.Entity.FA.Request UpdateCustomerRequest(SOFTTEK.SCMS.Entity.FA.Request request)
        {
            return context.Execute(() =>
            {
                SOFTTEK.SCMS.Entity.FA.Request submmitedRequest;
                using (dataSource = new Data.FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    submmitedRequest = dataSource.UpdateRequest(request, new SCMS.Entity.FA.Request { Identifier = request.Identifier });
                }

                return submmitedRequest;

            }, string.Format("Update Fixed Asset Customer Request {0} from employee:{1}", request.Identifier, request.Accountable.Identifier));
        }

        public List<SOFTTEK.SCMS.Entity.FA.Request> RetrieveFixedAssetCustomersRequests(SOFTTEK.SCMS.Entity.FA.Request filter) {
            return context.Execute(() =>
            {
                List<SOFTTEK.SCMS.Entity.FA.Request> requests;

                using (dataSource = new Data.FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    requests = dataSource.GetRequests(filter);
                }
                
                return requests;
            }, "Retrieve Fixed Asset Customers Requests by filter");
        }

        public List<SOFTTEK.SCMS.Entity.FA.Request> RetrieveFixedAssetCustomersRequestsInPeriod(SOFTTEK.SCMS.Entity.FA.Request filter, DateTime from, DateTime to)
        {
            return context.Execute(() =>
            {
                List<SOFTTEK.SCMS.Entity.FA.Request> requests = RetrieveFixedAssetCustomersRequests(filter).Where(r=>r.UpdatedAt.Date.CompareTo(from.Date) >= 0 && r.UpdatedAt.Date.CompareTo(to) < 0).ToList();
                return requests;
            }, string.Format("Retrieve Fixed Asset Customers Requests by filter between {0:dd/MM/yyyy} and {1:dd/MM/yyyy}", from.Date, to.Date));
        }

        #endregion

        #region Novelty Request 

        public SOFTTEK.SCMS.Entity.FA.NoveltyRequest CreateNoveltyRequest(SOFTTEK.SCMS.Entity.FA.NoveltyRequest request)
        {
            return context.Execute(() => {
                SRA.EmployeeBO employeeBO = new SRA.EmployeeBO(context);
                request.Accountable = employeeBO.GetEmployeeInfoById(request.Accountable.Identifier);

                SOFTTEK.SCMS.Entity.FA.NoveltyRequest submittedRequest;

                using (dataSource = new Data.FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    submittedRequest = dataSource.InsertNoveltyRequest(request);
                }

                SOFTTEK.SCMS.Entity.FA.Request updatedRequestRequest = RetrieveFixedAssetCustomersRequests(request.Request).FirstOrDefault();

                if (updatedRequestRequest.Equals(default(SOFTTEK.SCMS.Entity.FA.Request)))
                {
                    throw new Exception("It was no possible to retrieve the novelty's request information");
                }

                updatedRequestRequest.Responsible = request.Accountable;
                updatedRequestRequest = AssignCustomerRequest(updatedRequestRequest, string.Format("The novelty request {0} with your request information has been generated", submittedRequest.Identifier));

                return submittedRequest;
            }, string.Format("Create Novelty for Request {0}", request.Request.Identifier));
        }

        public SOFTTEK.SCMS.Entity.FA.NoveltyRequest AssignNoveltyRequest(SOFTTEK.SCMS.Entity.FA.NoveltyRequest request, string comments)
        {
            return context.Execute(() =>
            {
                request.Status = kFAMAssignedStatus;
                request.Comments = string.Concat(request.Comments, string.Format("\n[0:dd/MM/yyyy HH:mm:ss]:{1}=>{2}|{3}-{4}", DateTime.Now, request.Status, kFAMAssignedStatus, request.Responsible.Area, comments));

                SOFTTEK.SCMS.Entity.FA.NoveltyRequest updatedRequest = UpdateNoveltyRequest(request);
                return updatedRequest;
            }, string.Format("Assign  Fixed Asset Novelty Request from employe:{0} to area:{1}", request.Accountable.Identifier, request.Responsible.Area));
        }

        public SOFTTEK.SCMS.Entity.FA.NoveltyRequest ProcessNoveltyRequest(SOFTTEK.SCMS.Entity.FA.NoveltyRequest request)
        {
            return context.Execute(() =>
            {

                request.Status = kFAMInProcessStatus;
                request.Comments = string.Concat(request.Comments, string.Format("\n[0:dd/MM/yyyy HH:mm:ss]:{1}=>{2}|{3}", DateTime.Now, request.Status, kFAMInProcessStatus, request.Responsible.Area));

                SOFTTEK.SCMS.Entity.FA.NoveltyRequest updatedRequest = UpdateNoveltyRequest(request);
                return updatedRequest;
            }, string.Format("Process Fixed Asset Novelty Request from employe:{0}", request.Accountable.Identifier));
        }

        public SOFTTEK.SCMS.Entity.FA.NoveltyRequest FulfillNoveltyRequest(SOFTTEK.SCMS.Entity.FA.NoveltyRequest request, string comments)
        {
            return context.Execute(() =>
            {
                SRA.EmployeeBO employeeBO = new SRA.EmployeeBO(context);

                request.Accountable = employeeBO.GetEmployeeInfoById(request.Accountable.Identifier);

                request.Status = kFAMWaitingForCustomerStatus;
                request.Comments = string.Concat(request.Comments, string.Format("\n[0:dd/MM/yyyy HH:mm:ss]:{1}=>{2}|{3}-{4}", DateTime.Now, request.Status, kFAMWaitingForCustomerStatus, request.Accountable.Person.FullName, comments));

                SOFTTEK.SCMS.Entity.FA.NoveltyRequest updatedRequest = UpdateNoveltyRequest(request);
                return updatedRequest;
            }, string.Format("Fulfill Fixed Asset Novelty Request from employe:{0}", request.Accountable.Identifier));
        }

        public SOFTTEK.SCMS.Entity.FA.NoveltyRequest CloseNoveltyRequest(SOFTTEK.SCMS.Entity.FA.NoveltyRequest request, string comments)
        {
            return context.Execute(() =>
            {
                SRA.EmployeeBO employeeBO = new SRA.EmployeeBO(context);

                request.Accountable = employeeBO.GetEmployeeInfoById(request.Accountable.Identifier);

                if (!request.Accountable.User.Identifier.Equals(context.SecurityContext.ClientID)
                    && request.UpdatedAt.AddDays(kFAMDaysBetweenInteractionLimit).CompareTo(DateTime.Now) < 0)
                {
                    throw new Exception("The Novelty Request cannot be closed until the Accountable confirmation or the number of days to close the request has been reached.");
                }

                request.Status = kFAMClosedStatus;
                request.Comments = string.Concat(request.Comments, string.Format("\n[0:dd/MM/yyyy HH:mm:ss]:{1}=>{2}|{3}-{4}", DateTime.Now, request.Status, kFAMClosedStatus, request.Accountable.Person.FullName, comments));

                SOFTTEK.SCMS.Entity.FA.NoveltyRequest updatedRequest = UpdateNoveltyRequest(request);
                return updatedRequest;
            }, string.Format("Close Fixed Asset Novelty Request from employe:{0}", request.Accountable.Identifier));
        }

        public SOFTTEK.SCMS.Entity.FA.NoveltyRequest RequestCustomerInformationFromNoveltyRequest(SOFTTEK.SCMS.Entity.FA.NoveltyRequest request, string comments)
        {
            return context.Execute(() =>
            {
                SRA.EmployeeBO employeeBO = new SRA.EmployeeBO(context);

                request.Accountable = employeeBO.GetEmployeeInfoById(request.Accountable.Identifier);
                request.Status = kFAMWaitingForCustomerStatus;
                request.Comments = string.Concat(request.Comments, string.Format("\n[0:dd/MM/yyyy HH:mm:ss]:{1}=>{2}|{3}-{4}", DateTime.Now, request.Status, kFAMWaitingForCustomerStatus, request.Accountable.Person.FullName, comments));

                SOFTTEK.SCMS.Entity.FA.NoveltyRequest updatedRequest = UpdateNoveltyRequest(request);
                return updatedRequest;
            }, string.Format("Assign  Fixed Asset Novelty Request from employe:{0} to area:{1}", request.Accountable.Identifier, request.Responsible.Area));
        }
  
        private SOFTTEK.SCMS.Entity.FA.NoveltyRequest UpdateNoveltyRequest(SOFTTEK.SCMS.Entity.FA.NoveltyRequest request)
        {
            return context.Execute(() =>
            {
                
                SOFTTEK.SCMS.Entity.FA.Request updatedRequestRequest = RetrieveFixedAssetCustomersRequests(request.Request).FirstOrDefault();

                if (updatedRequestRequest.Equals(default(SOFTTEK.SCMS.Entity.FA.Request)))
                {
                    throw new Exception("It was no possible to retrieve the novelty's request information");
                }

                SOFTTEK.SCMS.Entity.FA.NoveltyRequest updatedRequest;
                using (dataSource = new Data.FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    updatedRequest = dataSource.UpdateNoveltyRequest(request, new SCMS.Entity.FA.NoveltyRequest { Identifier = request.Identifier });
                }
                updatedRequestRequest.Responsible = request.Accountable;
                updatedRequestRequest = AssignCustomerRequest(updatedRequestRequest, string.Format("The novelty request {0} with for your request has been updated with status {1}", updatedRequest.Identifier, updatedRequest.Status));

                return updatedRequest;

            }, string.Format("Update Novelty Request {0} from employee: {1}", request.Identifier, request.Accountable.Identifier));
        }

        public List<SOFTTEK.SCMS.Entity.FA.NoveltyRequest> RetrieveFixedAssetNoveltyRequests(SOFTTEK.SCMS.Entity.FA.NoveltyRequest filter)
        {
            return context.Execute(() =>
            {
                List<SOFTTEK.SCMS.Entity.FA.NoveltyRequest> requests;
                using (dataSource = new Data.FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    requests = dataSource.GetNoveltyRequests(filter);
                }
                return requests;
            }, "Retrieve Fixed Asset Novelty Requests by filter");
        }

        public List<SOFTTEK.SCMS.Entity.FA.NoveltyRequest> RetrieveFixedAssetNoveltyRequestsInPeriod(SOFTTEK.SCMS.Entity.FA.NoveltyRequest filter, DateTime from, DateTime to)
        {
            return context.Execute(() =>
            {
                List<SOFTTEK.SCMS.Entity.FA.NoveltyRequest> requests = RetrieveFixedAssetNoveltyRequests(filter).Where(r => r.UpdatedAt.Date.CompareTo(from.Date) >= 0 && r.UpdatedAt.Date.CompareTo(to) < 0).ToList();
                return requests;
            }, string.Format("Retrieve Fixed Asset Novelty Requests by filter between {0:dd/MM/yyyy} and {1:dd/MM/yyyy}", from.Date, to.Date));
        }

        #endregion

        #region Purchase Request

        public SOFTTEK.SCMS.Entity.FA.PurchaseRequest CreatePurchaseRequest(SOFTTEK.SCMS.Entity.FA.PurchaseRequest request)
        {
            return context.Execute(() =>
            {
                SRA.EmployeeBO employeeBO = new SRA.EmployeeBO(context);
                request.Accountable = employeeBO.GetEmployeeInfoById(request.Accountable.Identifier);

                SOFTTEK.SCMS.Entity.FA.PurchaseRequest submittedRequest;
                using (dataSource = new Data.FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    submittedRequest = dataSource.InsertPurchaseRequest(request);
                }

                SOFTTEK.SCMS.Entity.FA.NoveltyRequest updatedRequestNovelty = RetrieveFixedAssetNoveltyRequests(request.Novelty).FirstOrDefault();

                if (updatedRequestNovelty.Equals(default(SOFTTEK.SCMS.Entity.FA.NoveltyRequest)))
                {
                    throw new Exception("It was no possible to retrieve the requsest's novelty information");
                }

                updatedRequestNovelty.Responsible = request.Accountable;
                updatedRequestNovelty = AssignNoveltyRequest(updatedRequestNovelty, string.Format("The purchase request {0} with your novelty information has been generated", submittedRequest.Identifier));

                return submittedRequest;
            }, string.Format("Create Purchase Request for Novelty {0}", request.Novelty.Identifier));
        }

        public SOFTTEK.SCMS.Entity.FA.PurchaseRequest AssignPurchaseRequest(SOFTTEK.SCMS.Entity.FA.PurchaseRequest request, string comments)
        {
            return context.Execute(() =>
            {
                request.Status = kFAMAssignedStatus;
                request.Comments = string.Concat(request.Comments, string.Format("\n[0:dd/MM/yyyy HH:mm:ss]:{1}=>{2}|{3}-{4}", DateTime.Now, request.Status, kFAMAssignedStatus, request.Responsible.Area, comments));

                SOFTTEK.SCMS.Entity.FA.PurchaseRequest updatedRequest = UpdatePurchaseRequest(request);
                return updatedRequest;
            }, string.Format("Assign Fixed Asset Purchase Request from employe:{0} to area:{1}", request.Accountable.Identifier, request.Responsible.Area));
        }

        public SOFTTEK.SCMS.Entity.FA.PurchaseRequest ProcessPurchaseRequest(SOFTTEK.SCMS.Entity.FA.PurchaseRequest request)
        {
            return context.Execute(() =>
            {

                request.Status = kFAMInProcessStatus;
                request.Comments = string.Concat(request.Comments, string.Format("\n[0:dd/MM/yyyy HH:mm:ss]:{1}=>{2}|{3}", DateTime.Now, request.Status, kFAMInProcessStatus, request.Responsible.Area));

                SOFTTEK.SCMS.Entity.FA.PurchaseRequest updatedRequest = UpdatePurchaseRequest(request);
                return updatedRequest;
            }, string.Format("Process Fixed Asset Purchase Request from employe:{0}", request.Accountable.Identifier));
        }

        public SOFTTEK.SCMS.Entity.FA.PurchaseRequest FulfillPurchaseRequest(SOFTTEK.SCMS.Entity.FA.PurchaseRequest request, string comments)
        {
            return context.Execute(() =>
            {
                SRA.EmployeeBO employeeBO = new SRA.EmployeeBO(context);

                request.Accountable = employeeBO.GetEmployeeInfoById(request.Accountable.Identifier);

                request.Status = kFAMWaitingForCustomerStatus;
                request.Comments = string.Concat(request.Comments, string.Format("\n[0:dd/MM/yyyy HH:mm:ss]:{1}=>{2}|{3}-{4}", DateTime.Now, request.Status, kFAMWaitingForCustomerStatus, request.Accountable.Person.FullName, comments));

                SOFTTEK.SCMS.Entity.FA.PurchaseRequest updatedRequest = UpdatePurchaseRequest(request);
                return updatedRequest;
            }, string.Format("Fulfill Fixed Asset Purchase Request from employe:{0}", request.Accountable.Identifier));
        }

        public SOFTTEK.SCMS.Entity.FA.PurchaseRequest ClosePurchaseRequest(SOFTTEK.SCMS.Entity.FA.PurchaseRequest request, string comments)
        {
            return context.Execute(() =>
            {
                SRA.EmployeeBO employeeBO = new SRA.EmployeeBO(context);

                request.Accountable = employeeBO.GetEmployeeInfoById(request.Accountable.Identifier);

                if (!request.Accountable.User.Identifier.Equals(context.SecurityContext.ClientID)
                    && request.UpdatedAt.AddDays(kFAMDaysBetweenInteractionLimit).CompareTo(DateTime.Now) < 0)
                {
                    throw new Exception("The Purchase Request cannot be closed until the Accountable confirmation or the number of days to close the request has been reached.");
                }

                request.Status = kFAMClosedStatus;
                request.Comments = string.Concat(request.Comments, string.Format("\n[0:dd/MM/yyyy HH:mm:ss]:{1}=>{2}|{3}-{4}", DateTime.Now, request.Status, kFAMClosedStatus, request.Accountable.Person.FullName, comments));

                SOFTTEK.SCMS.Entity.FA.PurchaseRequest updatedRequest = UpdatePurchaseRequest(request);
                return updatedRequest;
            }, string.Format("Close Fixed Asset Purchase Request from employe:{0}", request.Accountable.Identifier));
        }

        public SOFTTEK.SCMS.Entity.FA.PurchaseRequest RequestCustomerInformationFromPurchaseRequest(SOFTTEK.SCMS.Entity.FA.PurchaseRequest request, string comments)
        {
            return context.Execute(() =>
            {
                SRA.EmployeeBO employeeBO = new SRA.EmployeeBO(context);

                request.Accountable = employeeBO.GetEmployeeInfoById(request.Accountable.Identifier);
                request.Status = kFAMWaitingForCustomerStatus;
                request.Comments = string.Concat(request.Comments, string.Format("\n[0:dd/MM/yyyy HH:mm:ss]:{1}=>{2}|{3}-{4}", DateTime.Now, request.Status, kFAMWaitingForCustomerStatus, request.Accountable.Person.FullName, comments));

                SOFTTEK.SCMS.Entity.FA.PurchaseRequest updatedRequest = UpdatePurchaseRequest(request);
                return updatedRequest;
            }, string.Format("Assign  Fixed Asset Purchase Request from employe:{0} to area:{1}", request.Accountable.Identifier, request.Responsible.Area));
        }

        private SOFTTEK.SCMS.Entity.FA.PurchaseRequest UpdatePurchaseRequest(SOFTTEK.SCMS.Entity.FA.PurchaseRequest request)
        {
            return context.Execute(() =>
            {

                SOFTTEK.SCMS.Entity.FA.NoveltyRequest updatedRequestNovelty = RetrieveFixedAssetNoveltyRequests(request.Novelty).FirstOrDefault();

                if (updatedRequestNovelty.Equals(default(SOFTTEK.SCMS.Entity.FA.Request)))
                {
                    throw new Exception("It was no possible to retrieve the novelty's request information");
                }

                SOFTTEK.SCMS.Entity.FA.PurchaseRequest updatedRequest;
                using (dataSource = new Data.FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    updatedRequest = dataSource.UpdatePurchaseRequest(request, new SCMS.Entity.FA.PurchaseRequest { Identifier = request.Identifier });
                }

                updatedRequestNovelty.Responsible = request.Accountable;
                updatedRequestNovelty = AssignNoveltyRequest(updatedRequestNovelty, string.Format("The novelty request {0} with for your request has been updated with status {1}", updatedRequest.Identifier, updatedRequest.Status));

                return updatedRequest;

            }, string.Format("Update Purchase Request {0} from employee: {1}", request.Identifier, request.Accountable.Identifier));
        }

        public List<SOFTTEK.SCMS.Entity.FA.PurchaseRequest> RetrieveFixedAssetPurchaseRequests(SOFTTEK.SCMS.Entity.FA.PurchaseRequest filter)
        {
            return context.Execute(() =>
            {
                List<SOFTTEK.SCMS.Entity.FA.PurchaseRequest> requests;
                using (dataSource = new Data.FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    requests = dataSource.GetPurchaseRequests(filter);
                }

                return requests;
            }, "Retrieve Fixed Asset Purchase Requests by filter");
        }

        public List<SOFTTEK.SCMS.Entity.FA.PurchaseRequest> RetrieveFixedAssetPurchaseRequestsInPeriod(SOFTTEK.SCMS.Entity.FA.PurchaseRequest filter, DateTime from, DateTime to)
        {
            return context.Execute(() =>
            {
                List<SOFTTEK.SCMS.Entity.FA.PurchaseRequest> requests = RetrieveFixedAssetPurchaseRequests(filter).Where(r => r.UpdatedAt.Date.CompareTo(from.Date) >= 0 && r.UpdatedAt.Date.CompareTo(to) < 0).ToList();
                return requests;
            }, string.Format("Retrieve Fixed Asset Purchase Requests by filter between {0:dd/MM/yyyy} and {1:dd/MM/yyyy}", from.Date, to.Date));
        }



        #endregion

        #region Transfer Request

        public SOFTTEK.SCMS.Entity.FA.TransferRequest CreateTransferRequest(SOFTTEK.SCMS.Entity.FA.TransferRequest request)
        {
            return context.Execute(() =>
            {
                SRA.EmployeeBO employeeBO = new SRA.EmployeeBO(context);
                request.Accountable = employeeBO.GetEmployeeInfoById(request.Accountable.Identifier);

                SOFTTEK.SCMS.Entity.FA.TransferRequest submittedRequest;
                using (dataSource = new Data.FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    submittedRequest = dataSource.InsertTransferRequest(request);
                }

                SOFTTEK.SCMS.Entity.FA.NoveltyRequest updatedRequestNovelty = RetrieveFixedAssetNoveltyRequests(request.Novelty).FirstOrDefault();

                if (updatedRequestNovelty.Equals(default(SOFTTEK.SCMS.Entity.FA.NoveltyRequest)))
                {
                    throw new Exception("It was no possible to retrieve the requsest's novelty information");
                }

                updatedRequestNovelty.Responsible = request.Accountable;
                updatedRequestNovelty = AssignNoveltyRequest(updatedRequestNovelty, string.Format("The Transfer request {0} with your novelty information has been generated", submittedRequest.Identifier));

                return submittedRequest;
            }, string.Format("Create Transfer Request for Novelty {0}", request.Novelty.Identifier));
        }

        public SOFTTEK.SCMS.Entity.FA.TransferRequest AssignTransferRequest(SOFTTEK.SCMS.Entity.FA.TransferRequest request, string comments)
        {
            return context.Execute(() =>
            {
                request.Status = kFAMAssignedStatus;
                request.Comments = string.Concat(request.Comments, string.Format("\n[0:dd/MM/yyyy HH:mm:ss]:{1}=>{2}|{3}-{4}", DateTime.Now, request.Status, kFAMAssignedStatus, request.Responsible.Area, comments));

                SOFTTEK.SCMS.Entity.FA.TransferRequest updatedRequest = UpdateTransferRequest(request);
                return updatedRequest;
            }, string.Format("Assign Fixed Asset Transfer Request from employe:{0} to area:{1}", request.Accountable.Identifier, request.Responsible.Area));
        }

        public SOFTTEK.SCMS.Entity.FA.TransferRequest ProcessTransferRequest(SOFTTEK.SCMS.Entity.FA.TransferRequest request)
        {
            return context.Execute(() =>
            {

                request.Status = kFAMInProcessStatus;
                request.Comments = string.Concat(request.Comments, string.Format("\n[0:dd/MM/yyyy HH:mm:ss]:{1}=>{2}|{3}", DateTime.Now, request.Status, kFAMInProcessStatus, request.Responsible.Area));

                SOFTTEK.SCMS.Entity.FA.TransferRequest updatedRequest = UpdateTransferRequest(request);
                return updatedRequest;
            }, string.Format("Process Fixed Asset Transfer Request from employe:{0}", request.Accountable.Identifier));
        }

        public SOFTTEK.SCMS.Entity.FA.TransferRequest FulfillTransferRequest(SOFTTEK.SCMS.Entity.FA.TransferRequest request, string comments)
        {
            return context.Execute(() =>
            {
                SRA.EmployeeBO employeeBO = new SRA.EmployeeBO(context);

                request.Accountable = employeeBO.GetEmployeeInfoById(request.Accountable.Identifier);

                request.Status = kFAMWaitingForCustomerStatus;
                request.Comments = string.Concat(request.Comments, string.Format("\n[0:dd/MM/yyyy HH:mm:ss]:{1}=>{2}|{3}-{4}", DateTime.Now, request.Status, kFAMWaitingForCustomerStatus, request.Accountable.Person.FullName, comments));

                SOFTTEK.SCMS.Entity.FA.TransferRequest updatedRequest = UpdateTransferRequest(request);
                return updatedRequest;
            }, string.Format("Fulfill Fixed Asset Transfer Request from employe:{0}", request.Accountable.Identifier));
        }

        public SOFTTEK.SCMS.Entity.FA.TransferRequest CloseTransferRequest(SOFTTEK.SCMS.Entity.FA.TransferRequest request, string comments)
        {
            return context.Execute(() =>
            {
                SRA.EmployeeBO employeeBO = new SRA.EmployeeBO(context);

                request.Accountable = employeeBO.GetEmployeeInfoById(request.Accountable.Identifier);

                if (!request.Accountable.User.Identifier.Equals(context.SecurityContext.ClientID)
                    && request.UpdatedAt.AddDays(kFAMDaysBetweenInteractionLimit).CompareTo(DateTime.Now) < 0)
                {
                    throw new Exception("The Transfer Request cannot be closed until the Accountable confirmation or the number of days to close the request has been reached.");
                }

                request.Status = kFAMClosedStatus;
                request.Comments = string.Concat(request.Comments, string.Format("\n[0:dd/MM/yyyy HH:mm:ss]:{1}=>{2}|{3}-{4}", DateTime.Now, request.Status, kFAMClosedStatus, request.Accountable.Person.FullName, comments));

                SOFTTEK.SCMS.Entity.FA.TransferRequest updatedRequest = UpdateTransferRequest(request);
                return updatedRequest;
            }, string.Format("Close Fixed Asset Transfer Request from employe:{0}", request.Accountable.Identifier));
        }

        public SOFTTEK.SCMS.Entity.FA.TransferRequest RequestCustomerInformationFromTransferRequest(SOFTTEK.SCMS.Entity.FA.TransferRequest request, string comments)
        {
            return context.Execute(() =>
            {
                SRA.EmployeeBO employeeBO = new SRA.EmployeeBO(context);

                request.Accountable = employeeBO.GetEmployeeInfoById(request.Accountable.Identifier);
                request.Status = kFAMWaitingForCustomerStatus;
                request.Comments = string.Concat(request.Comments, string.Format("\n[0:dd/MM/yyyy HH:mm:ss]:{1}=>{2}|{3}-{4}", DateTime.Now, request.Status, kFAMWaitingForCustomerStatus, request.Accountable.Person.FullName, comments));

                SOFTTEK.SCMS.Entity.FA.TransferRequest updatedRequest = UpdateTransferRequest(request);
                return updatedRequest;
            }, string.Format("Assign  Fixed Asset Transfer Request from employe:{0} to area:{1}", request.Accountable.Identifier, request.Responsible.Area));
        }

        private SOFTTEK.SCMS.Entity.FA.TransferRequest UpdateTransferRequest(SOFTTEK.SCMS.Entity.FA.TransferRequest request)
        {
            return context.Execute(() =>
            {

                SOFTTEK.SCMS.Entity.FA.NoveltyRequest updatedRequestNovelty = RetrieveFixedAssetNoveltyRequests(request.Novelty).FirstOrDefault();

                if (updatedRequestNovelty.Equals(default(SOFTTEK.SCMS.Entity.FA.Request)))
                {
                    throw new Exception("It was no possible to retrieve the novelty's request information");
                }

                SOFTTEK.SCMS.Entity.FA.TransferRequest updatedRequest;
                using (dataSource = new Data.FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    updatedRequest = dataSource.UpdateTransferRequest(request, new SCMS.Entity.FA.TransferRequest { Identifier = request.Identifier });
                }

                updatedRequestNovelty.Responsible = request.Accountable;
                updatedRequestNovelty = AssignNoveltyRequest(updatedRequestNovelty, string.Format("The novelty request {0} with for your request has been updated with status {1}", updatedRequest.Identifier, updatedRequest.Status));

                return updatedRequest;

            }, string.Format("Update Transfer Request {0} from employee: {1}", request.Identifier, request.Accountable.Identifier));
        }

        public List<SOFTTEK.SCMS.Entity.FA.TransferRequest> RetrieveFixedAssetTransferRequests(SOFTTEK.SCMS.Entity.FA.TransferRequest filter)
        {
            return context.Execute(() =>
            {
                List<SOFTTEK.SCMS.Entity.FA.TransferRequest> requests;
                using (dataSource = new Data.FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    requests = dataSource.GetTransferRequests(filter);
                }
                
                return requests;
            }, "Retrieve Fixed Asset Transfer Requests by filter");
        }

        public List<SOFTTEK.SCMS.Entity.FA.TransferRequest> RetrieveFixedAssetTransferRequestsInPeriod(SOFTTEK.SCMS.Entity.FA.TransferRequest filter, DateTime from, DateTime to)
        {
            return context.Execute(() =>
            {
                List<SOFTTEK.SCMS.Entity.FA.TransferRequest> requests = RetrieveFixedAssetTransferRequests(filter).Where(r => r.UpdatedAt.Date.CompareTo(from.Date) >= 0 && r.UpdatedAt.Date.CompareTo(to) < 0).ToList();
                return requests;
            }, string.Format("Retrieve Fixed Asset Transfer Requests by filter between {0:dd/MM/yyyy} and {1:dd/MM/yyyy}", from.Date, to.Date));
        }


        #endregion

        #region Retirement Request


        public SOFTTEK.SCMS.Entity.FA.RetirementRequest CreateRetirementRequest(SOFTTEK.SCMS.Entity.FA.RetirementRequest request)
        {
            return context.Execute(() =>
            {
                SRA.EmployeeBO employeeBO = new SRA.EmployeeBO(context);
                request.Accountable = employeeBO.GetEmployeeInfoById(request.Accountable.Identifier);

                SOFTTEK.SCMS.Entity.FA.RetirementRequest submittedRequest;
                using (dataSource = new Data.FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    submittedRequest = dataSource.InsertRetirementRequest(request);
                }

                SOFTTEK.SCMS.Entity.FA.NoveltyRequest updatedRequestNovelty = RetrieveFixedAssetNoveltyRequests(request.Novelty).FirstOrDefault();

                if (updatedRequestNovelty.Equals(default(SOFTTEK.SCMS.Entity.FA.NoveltyRequest)))
                {
                    throw new Exception("It was no possible to retrieve the requsest's novelty information");
                }

                updatedRequestNovelty.Responsible = request.Accountable;
                updatedRequestNovelty = AssignNoveltyRequest(updatedRequestNovelty, string.Format("The Retirement request {0} with your novelty information has been generated", submittedRequest.Identifier));

                return submittedRequest;
            }, string.Format("Create Retirement Request for Novelty {0}", request.Novelty.Identifier));
        }

        public SOFTTEK.SCMS.Entity.FA.RetirementRequest AssignRetirementRequest(SOFTTEK.SCMS.Entity.FA.RetirementRequest request, string comments)
        {
            return context.Execute(() =>
            {
                request.Status = kFAMAssignedStatus;
                request.Comments = string.Concat(request.Comments, string.Format("\n[0:dd/MM/yyyy HH:mm:ss]:{1}=>{2}|{3}-{4}", DateTime.Now, request.Status, kFAMAssignedStatus, request.Responsible.Area, comments));

                SOFTTEK.SCMS.Entity.FA.RetirementRequest updatedRequest = UpdateRetirementRequest(request);
                return updatedRequest;
            }, string.Format("Assign Fixed Asset Retirement Request from employe:{0} to area:{1}", request.Accountable.Identifier, request.Responsible.Area));
        }

        public SOFTTEK.SCMS.Entity.FA.RetirementRequest ProcessRetirementRequest(SOFTTEK.SCMS.Entity.FA.RetirementRequest request)
        {
            return context.Execute(() =>
            {

                request.Status = kFAMInProcessStatus;
                request.Comments = string.Concat(request.Comments, string.Format("\n[0:dd/MM/yyyy HH:mm:ss]:{1}=>{2}|{3}", DateTime.Now, request.Status, kFAMInProcessStatus, request.Responsible.Area));

                SOFTTEK.SCMS.Entity.FA.RetirementRequest updatedRequest = UpdateRetirementRequest(request);
                return updatedRequest;
            }, string.Format("Process Fixed Asset Retirement Request from employe:{0}", request.Accountable.Identifier));
        }

        public SOFTTEK.SCMS.Entity.FA.RetirementRequest FulfillRetirementRequest(SOFTTEK.SCMS.Entity.FA.RetirementRequest request, string comments)
        {
            return context.Execute(() =>
            {
                SRA.EmployeeBO employeeBO = new SRA.EmployeeBO(context);

                request.Accountable = employeeBO.GetEmployeeInfoById(request.Accountable.Identifier);

                request.Status = kFAMWaitingForCustomerStatus;
                request.Comments = string.Concat(request.Comments, string.Format("\n[0:dd/MM/yyyy HH:mm:ss]:{1}=>{2}|{3}-{4}", DateTime.Now, request.Status, kFAMWaitingForCustomerStatus, request.Accountable.Person.FullName, comments));

                SOFTTEK.SCMS.Entity.FA.RetirementRequest updatedRequest = UpdateRetirementRequest(request);
                return updatedRequest;
            }, string.Format("Fulfill Fixed Asset Retirement Request from employe:{0}", request.Accountable.Identifier));
        }

        public SOFTTEK.SCMS.Entity.FA.RetirementRequest CloseRetirementRequest(SOFTTEK.SCMS.Entity.FA.RetirementRequest request, string comments)
        {
            return context.Execute(() =>
            {
                SRA.EmployeeBO employeeBO = new SRA.EmployeeBO(context);

                request.Accountable = employeeBO.GetEmployeeInfoById(request.Accountable.Identifier);

                if (!request.Accountable.User.Identifier.Equals(context.SecurityContext.ClientID)
                    && request.UpdatedAt.AddDays(kFAMDaysBetweenInteractionLimit).CompareTo(DateTime.Now) < 0)
                {
                    throw new Exception("The Retirement Request cannot be closed until the Accountable confirmation or the number of days to close the request has been reached.");
                }

                request.Status = kFAMClosedStatus;
                request.Comments = string.Concat(request.Comments, string.Format("\n[0:dd/MM/yyyy HH:mm:ss]:{1}=>{2}|{3}-{4}", DateTime.Now, request.Status, kFAMClosedStatus, request.Accountable.Person.FullName, comments));

                SOFTTEK.SCMS.Entity.FA.RetirementRequest updatedRequest = UpdateRetirementRequest(request);
                return updatedRequest;
            }, string.Format("Close Fixed Asset Retirement Request from employe:{0}", request.Accountable.Identifier));
        }

        public SOFTTEK.SCMS.Entity.FA.RetirementRequest RequestCustomerInformationFromRetirementRequest(SOFTTEK.SCMS.Entity.FA.RetirementRequest request, string comments)
        {
            return context.Execute(() =>
            {
                SRA.EmployeeBO employeeBO = new SRA.EmployeeBO(context);

                request.Accountable = employeeBO.GetEmployeeInfoById(request.Accountable.Identifier);
                request.Status = kFAMWaitingForCustomerStatus;
                request.Comments = string.Concat(request.Comments, string.Format("\n[0:dd/MM/yyyy HH:mm:ss]:{1}=>{2}|{3}-{4}", DateTime.Now, request.Status, kFAMWaitingForCustomerStatus, request.Accountable.Person.FullName, comments));

                SOFTTEK.SCMS.Entity.FA.RetirementRequest updatedRequest = UpdateRetirementRequest(request);
                return updatedRequest;
            }, string.Format("Assign  Fixed Asset Retirement Request from employe:{0} to area:{1}", request.Accountable.Identifier, request.Responsible.Area));
        }

        private SOFTTEK.SCMS.Entity.FA.RetirementRequest UpdateRetirementRequest(SOFTTEK.SCMS.Entity.FA.RetirementRequest request)
        {
            return context.Execute(() =>
            {

                SOFTTEK.SCMS.Entity.FA.NoveltyRequest updatedRequestNovelty = RetrieveFixedAssetNoveltyRequests(request.Novelty).FirstOrDefault();

                if (updatedRequestNovelty.Equals(default(SOFTTEK.SCMS.Entity.FA.Request)))
                {
                    throw new Exception("It was no possible to retrieve the novelty's request information");
                }

                SOFTTEK.SCMS.Entity.FA.RetirementRequest updatedRequest;
                using (dataSource = new Data.FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    updatedRequest = dataSource.UpdateRetirementRequest(request, new SCMS.Entity.FA.RetirementRequest { Identifier = request.Identifier });
                }

                updatedRequestNovelty.Responsible = request.Accountable;
                updatedRequestNovelty = AssignNoveltyRequest(updatedRequestNovelty, string.Format("The novelty request {0} with for your request has been updated with status {1}", updatedRequest.Identifier, updatedRequest.Status));

                return updatedRequest;

            }, string.Format("Update Retirement Request {0} from employee: {1}", request.Identifier, request.Accountable.Identifier));
        }

        public List<SOFTTEK.SCMS.Entity.FA.RetirementRequest> RetrieveFixedAssetRetirementRequests(SOFTTEK.SCMS.Entity.FA.RetirementRequest filter)
        {
            return context.Execute(() =>
            {
                List<SOFTTEK.SCMS.Entity.FA.RetirementRequest> requests;
                using (dataSource = new Data.FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    requests = dataSource.GetRetirementRequests(filter);
                }

                return requests;
            }, "Retrieve Fixed Asset Retirement Requests by filter");
        }

        public List<SOFTTEK.SCMS.Entity.FA.RetirementRequest> RetrieveFixedAssetRetirementRequestsInPeriod(SOFTTEK.SCMS.Entity.FA.RetirementRequest filter, DateTime from, DateTime to)
        {
            return context.Execute(() =>
            {
                List<SOFTTEK.SCMS.Entity.FA.RetirementRequest> requests = RetrieveFixedAssetRetirementRequests(filter).Where(r => r.UpdatedAt.Date.CompareTo(from.Date) >= 0 && r.UpdatedAt.Date.CompareTo(to) < 0).ToList();
                return requests;
            }, string.Format("Retrieve Fixed Asset Retirement Requests by filter between {0:dd/MM/yyyy} and {1:dd/MM/yyyy}", from.Date, to.Date));
        }

        #endregion

        #region Technical Evaluation Request


        public SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest CreateTechnicalEvaluationRequest(SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest request)
        {
            return context.Execute(() =>
            {
                SRA.EmployeeBO employeeBO = new SRA.EmployeeBO(context);
                request.Accountable = employeeBO.GetEmployeeInfoById(request.Accountable.Identifier);

                SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest submittedRequest;
                using (dataSource = new Data.FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    submittedRequest = dataSource.InsertTechnicalEvaluationRequest(request);
                }

                SOFTTEK.SCMS.Entity.FA.NoveltyRequest updatedRequestNovelty = RetrieveFixedAssetNoveltyRequests(request.Novelty).FirstOrDefault();

                if (updatedRequestNovelty.Equals(default(SOFTTEK.SCMS.Entity.FA.NoveltyRequest)))
                {
                    throw new Exception("It was no possible to retrieve the requsest's novelty information");
                }

                updatedRequestNovelty.Responsible = request.Accountable;
                updatedRequestNovelty = AssignNoveltyRequest(updatedRequestNovelty, string.Format("The TechnicalEvaluation request {0} with your novelty information has been generated", submittedRequest.Identifier));

                return submittedRequest;
            }, string.Format("Create TechnicalEvaluation Request for Novelty {0}", request.Novelty.Identifier));
        }

        public SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest AssignTechnicalEvaluationRequest(SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest request, string comments)
        {
            return context.Execute(() =>
            {
                request.Status = kFAMAssignedStatus;
                request.Comments = string.Concat(request.Comments, string.Format("\n[0:dd/MM/yyyy HH:mm:ss]:{1}=>{2}|{3}-{4}", DateTime.Now, request.Status, kFAMAssignedStatus, request.Responsible.Area, comments));

                SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest updatedRequest = UpdateTechnicalEvaluationRequest(request);
                return updatedRequest;
            }, string.Format("Assign Fixed Asset TechnicalEvaluation Request from employe:{0} to area:{1}", request.Accountable.Identifier, request.Responsible.Area));
        }

        public SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest ProcessTechnicalEvaluationRequest(SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest request)
        {
            return context.Execute(() =>
            {

                request.Status = kFAMInProcessStatus;
                request.Comments = string.Concat(request.Comments, string.Format("\n[0:dd/MM/yyyy HH:mm:ss]:{1}=>{2}|{3}", DateTime.Now, request.Status, kFAMInProcessStatus, request.Responsible.Area));

                SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest updatedRequest = UpdateTechnicalEvaluationRequest(request);
                return updatedRequest;
            }, string.Format("Process Fixed Asset TechnicalEvaluation Request from employe:{0}", request.Accountable.Identifier));
        }

        public SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest FulfillTechnicalEvaluationRequest(SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest request, string comments)
        {
            return context.Execute(() =>
            {
                SRA.EmployeeBO employeeBO = new SRA.EmployeeBO(context);

                request.Accountable = employeeBO.GetEmployeeInfoById(request.Accountable.Identifier);

                request.Status = kFAMWaitingForCustomerStatus;
                request.Comments = string.Concat(request.Comments, string.Format("\n[0:dd/MM/yyyy HH:mm:ss]:{1}=>{2}|{3}-{4}", DateTime.Now, request.Status, kFAMWaitingForCustomerStatus, request.Accountable.Person.FullName, comments));

                SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest updatedRequest = UpdateTechnicalEvaluationRequest(request);
                return updatedRequest;
            }, string.Format("Fulfill Fixed Asset TechnicalEvaluation Request from employe:{0}", request.Accountable.Identifier));
        }

        public SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest CloseTechnicalEvaluationRequest(SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest request, string comments)
        {
            return context.Execute(() =>
            {
                SRA.EmployeeBO employeeBO = new SRA.EmployeeBO(context);

                request.Accountable = employeeBO.GetEmployeeInfoById(request.Accountable.Identifier);

                if (!request.Accountable.User.Identifier.Equals(context.SecurityContext.ClientID)
                    && request.UpdatedAt.AddDays(kFAMDaysBetweenInteractionLimit).CompareTo(DateTime.Now) < 0)
                {
                    throw new Exception("The TechnicalEvaluation Request cannot be closed until the Accountable confirmation or the number of days to close the request has been reached.");
                }

                request.Status = kFAMClosedStatus;
                request.Comments = string.Concat(request.Comments, string.Format("\n[0:dd/MM/yyyy HH:mm:ss]:{1}=>{2}|{3}-{4}", DateTime.Now, request.Status, kFAMClosedStatus, request.Accountable.Person.FullName, comments));

                SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest updatedRequest = UpdateTechnicalEvaluationRequest(request);
                return updatedRequest;
            }, string.Format("Close Fixed Asset TechnicalEvaluation Request from employe:{0}", request.Accountable.Identifier));
        }

        public SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest RequestCustomerInformationFromTechnicalEvaluationRequest(SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest request, string comments)
        {
            return context.Execute(() =>
            {
                SRA.EmployeeBO employeeBO = new SRA.EmployeeBO(context);

                request.Accountable = employeeBO.GetEmployeeInfoById(request.Accountable.Identifier);
                request.Status = kFAMWaitingForCustomerStatus;
                request.Comments = string.Concat(request.Comments, string.Format("\n[0:dd/MM/yyyy HH:mm:ss]:{1}=>{2}|{3}-{4}", DateTime.Now, request.Status, kFAMWaitingForCustomerStatus, request.Accountable.Person.FullName, comments));

                SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest updatedRequest = UpdateTechnicalEvaluationRequest(request);
                return updatedRequest;
            }, string.Format("Assign  Fixed Asset TechnicalEvaluation Request from employe:{0} to area:{1}", request.Accountable.Identifier, request.Responsible.Area));
        }

        private SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest UpdateTechnicalEvaluationRequest(SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest request)
        {
            return context.Execute(() =>
            {

                SOFTTEK.SCMS.Entity.FA.NoveltyRequest updatedRequestNovelty = RetrieveFixedAssetNoveltyRequests(request.Novelty).FirstOrDefault();

                if (updatedRequestNovelty.Equals(default(SOFTTEK.SCMS.Entity.FA.Request)))
                {
                    throw new Exception("It was no possible to retrieve the novelty's request information");
                }

                SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest updatedRequest;
                using (dataSource = new Data.FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    updatedRequest = dataSource.UpdateTechnicalEvaluationRequest(request, new SCMS.Entity.FA.TechnicalEvaluationRequest { Identifier = request.Identifier });
                }

                updatedRequestNovelty.Responsible = request.Accountable;
                updatedRequestNovelty = AssignNoveltyRequest(updatedRequestNovelty, string.Format("The novelty request {0} with for your request has been updated with status {1}", updatedRequest.Identifier, updatedRequest.Status));

                return updatedRequest;

            }, string.Format("Update TechnicalEvaluation Request {0} from employee: {1}", request.Identifier, request.Accountable.Identifier));
        }

        public List<SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest> RetrieveFixedAssetTechnicalEvaluationRequests(SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest filter)
        {
            return context.Execute(() =>
            {
                List<SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest> requests;
                using (dataSource = new Data.FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    requests = dataSource.GetTechnicalEvaluationRequests(filter);
                }

                return requests;
            }, "Retrieve Fixed Asset TechnicalEvaluation Requests by filter");
        }

        public List<SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest> RetrieveFixedAssetTechnicalEvaluationRequestsInPeriod(SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest filter, DateTime from, DateTime to)
        {
            return context.Execute(() =>
            {
                List<SOFTTEK.SCMS.Entity.FA.TechnicalEvaluationRequest> requests = RetrieveFixedAssetTechnicalEvaluationRequests(filter).Where(r => r.UpdatedAt.Date.CompareTo(from.Date) >= 0 && r.UpdatedAt.Date.CompareTo(to) < 0).ToList();
                return requests;
            }, string.Format("Retrieve Fixed Asset TechnicalEvaluation Requests by filter between {0:dd/MM/yyyy} and {1:dd/MM/yyyy}", from.Date, to.Date));
        }

        #endregion

        #endregion

        #region Availability Forecast

        public SCMS.Entity.FA.AvailabilityForecast GenerateAvailabilityForecast(SCMS.Entity.FA.AvailabilityForecast availabilityForeCast) { 
            return context.Execute(()=>{
                SCMS.Entity.FA.Request requestToUpdate = RetrieveFixedAssetCustomersRequests(availabilityForeCast.Request).FirstOrDefault();
                if (requestToUpdate.Equals(default(SOFTTEK.SCMS.Entity.FA.Request)))
                {
                    throw new Exception("It was no possible to retrieve the Availability Forecast's request information");
                }

                SCMS.Entity.FA.AvailabilityForecast generatedAvailabilityForecast = new SCMS.Entity.FA.AvailabilityForecast();
                
                requestToUpdate = FulfillCustomerRequest(requestToUpdate, string.Format("The Availability Forecast {0} for your request information has been generated", generatedAvailabilityForecast.Identifier));
                
                return generatedAvailabilityForecast;
                
            }, string.Format("Generate Availability Forecast for request {0}", availabilityForeCast.Request.Identifier));
        }

        public SCMS.Entity.FA.AvailabilityForecast RetrieveAvailabilityForecast(SCMS.Entity.FA.Request request)
        {
            return context.Execute(() =>
            {
                SCMS.Entity.FA.Request retrievedRequest = RetrieveFixedAssetCustomersRequests(request).FirstOrDefault();
                if (retrievedRequest.Equals(default(SOFTTEK.SCMS.Entity.FA.Request)))
                {
                    throw new Exception("It was no possible to retrieve the Availability Forecast's request information");
                }

                SCMS.Entity.FA.AvailabilityForecast retrievedAvailabilityForecast;
                using (dataSource = new Data.FAMDataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    retrievedAvailabilityForecast = dataSource.GetAvailabilityForecasts(new SOFTTEK.SCMS.Entity.FA.AvailabilityForecast { Request = request }).FirstOrDefault();
                    retrievedAvailabilityForecast.Items = dataSource.GetAvailabilityForecastItems(new SOFTTEK.SCMS.Entity.FA.AvailabilityForecastItem { AvailabilityForecast = retrievedAvailabilityForecast }).ToList();
                }

                return retrievedAvailabilityForecast;

            }, string.Format("Retrieve Availability Forecast for request {0}", request.Identifier));
        }

        #endregion

    }
}
