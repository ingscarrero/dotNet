using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFTTEK.SCMS.Business.PM
{
    public class MasterDataBO : Foundation.Business.BusinessObject
    {
        private SOFTTEK.SCMS.Data.PMDataContext pmDataSource;
        private SOFTTEK.SCMS.Data.SRADataContext sraDataSource;
        public MasterDataBO(Foundation.Business.BusinessContext ctx)
            : base (ctx)
        {
        }

        public List<SOFTTEK.SCMS.Entity.Shared.Parameter<string>> GetMasterDataForCategory(string categoryName = null) {
            return context.Execute(() => {
                DateTime lastUpdateSAPPMMasterData;
                List<SOFTTEK.SCMS.Entity.Shared.Parameter<string>> masterDataItems = new List<SCMS.Entity.Shared.Parameter<string>>();
                bool querySAP = true;

                lastUpdateSAPPMMasterData = GetSAPPMMasterDataExtractionDate();
                querySAP = DateTime.Now.Subtract(lastUpdateSAPPMMasterData) > new TimeSpan(1, 0, 0);

                // Query SAP is information expired(1 hour)
                if (querySAP)
                {
                    // Retrieve SAP PM Master Data
                    masterDataItems = RetrieveSapPMMasterData();
                    masterDataItems = string.IsNullOrEmpty(categoryName) ? masterDataItems : masterDataItems.Where(p=>p.Description == categoryName).ToList();

                } else {
                    using (sraDataSource = new Data.SRADataContext(context.SecurityContext))
                    {
                        sraDataSource.ConnectionString = "SRA";
                        sraDataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                        sraDataSource.Initialize();

                        masterDataItems = sraDataSource.GetParametersForCategory<string>("SAP_PM_MASTER_DATA");
                        masterDataItems = string.IsNullOrEmpty(categoryName) ? masterDataItems : masterDataItems.Where(p=>p.Description == categoryName).ToList();
                    }
                }
                return masterDataItems;
            }, "Query to SAP for PM master data and registration/update of applicable items");
        }

        private List<SCMS.Entity.Shared.Parameter<string>> RetrieveSapPMMasterData()
        {

            List<SOFTTEK.SCMS.Entity.Shared.Parameter<string>> masterDataItems;

            using (pmDataSource = new Data.PMDataContext(context.SecurityContext))
            {
                /**pmDataSource.ConnectionString = "SRA";
                pmDataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                pmDataSource.Initialize();*/
                masterDataItems = pmDataSource.GetMasterData();
            }

            using (sraDataSource = new Data.SRADataContext(context.SecurityContext))
            {
                sraDataSource.ConnectionString = "SRA";
                sraDataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                sraDataSource.Initialize();
                List<SOFTTEK.SCMS.Entity.Shared.Parameter<string>> registeredMasterDataItems = sraDataSource.GetParametersForCategory<string>("SAP_PM_MASTER_DATA");

                // Match sap master data items with registered items by external identifier
                masterDataItems = masterDataItems.Select((mi)=>{
                    SOFTTEK.SCMS.Entity.Shared.Parameter<string> ri = registeredMasterDataItems.Where(r1 => r1.ExternalIdentifier == mi.ExternalIdentifier).FirstOrDefault();

                    if(ri != null){
                        mi.Category = ri.Category;
                        mi.Identifier = ri.Identifier;
                    }

                    return mi;
                }).ToList();

                // Register/Update SAP records
                registeredMasterDataItems = masterDataItems.Select((mi) =>
                {
                    return mi.Identifier > 0 && mi.Category != null ?
                        sraDataSource.UpdateParameterForCategory<string>(mi.Category.Name, mi) :
                        sraDataSource.RegisterParameterForCategory<string>(mi.Category != null && !string.IsNullOrEmpty(mi.Category.Name) ?
                                mi.Category.Name :
                                "SAP_PM_MASTER_DATA", mi);
                }).ToList();

                // Set current date as last extraction date
                SOFTTEK.SCMS.Entity.Shared.Parameter<DateTime> systemDate = sraDataSource.GetParametersForCategory<DateTime>("SYSTEM_DATES").Where(p => p.Description == "SAP_PM_LAST_EXTRACTION").FirstOrDefault();
                if (systemDate != null)
                {
                    systemDate.Value = DateTime.Now;
                }
                else
                {
                    systemDate = new SOFTTEK.SCMS.Entity.Shared.Parameter<DateTime>
                    {
                        Description = "SAP_PM_LAST_EXTRACTION",
                        Comments = "Last date of PM Master Data Extraction from SAP",
                        IsActive = true,
                        Value = DateTime.Now
                    };
                }

                SOFTTEK.SCMS.Entity.Shared.Parameter<DateTime> pmSapExtractionParameter = systemDate != null && systemDate.Identifier > 0 ?
                    sraDataSource.UpdateParameterForCategory(systemDate.Category.Name, systemDate) :
                    sraDataSource.RegisterParameterForCategory("SYSTEM_DATES", systemDate);
            }
            return masterDataItems;
        }

        private DateTime GetSAPPMMasterDataExtractionDate()
        {
            DateTime lastUpdateSAPPMMasterData;
            // Get Last SAP Extraction Date
            using (sraDataSource = new Data.SRADataContext(context.SecurityContext))
            {
                sraDataSource.ConnectionString = "SRA";
                sraDataSource.DefaultUser = System.Configuration.ConfigurationManager.AppSettings["S_APP_UID"];
                sraDataSource.Initialize();
                SOFTTEK.SCMS.Entity.Shared.Parameter<DateTime> pmSapExtractionParameter = sraDataSource.GetParametersForCategory<DateTime>("SYSTEM_DATES").
                    Where(p => p.Description == "SAP_PM_LAST_EXTRACTION").FirstOrDefault();
                lastUpdateSAPPMMasterData = pmSapExtractionParameter != null ? pmSapExtractionParameter.Value : default(DateTime);
            }
            return lastUpdateSAPPMMasterData;
        }
    }
}
