using SOFTTEK.SCMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFTTEK.SCMS.Business.Common
{
    public class ConfigurationBO : SOFTTEK.SCMS.Foundation.Business.BusinessObject
    {
        private SRADataContext dataSource;

        public ConfigurationBO(SOFTTEK.SCMS.Foundation.Business.BusinessContext ctx)
            : base(ctx)
        {

        }

        public List<SOFTTEK.SCMS.Entity.Shared.Parameter<string>> GetParametersForCategory(string category)
        {
            List<SOFTTEK.SCMS.Entity.Shared.Parameter<string>> parameters = new List<SOFTTEK.SCMS.Entity.Shared.Parameter<string>>();
            
            return context.Execute(() =>
            {

                using (dataSource = new SRADataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();

                    parameters = dataSource.GetParametersForCategory<string>(category);
                }
                return parameters;
            }, "Get parameters for the provided category identifier.");
        }


        public SOFTTEK.SCMS.Entity.Shared.Parameter<string> RegisterParameter(SCMS.Entity.Shared.Parameter<string> parameter)
        {
            throw new NotImplementedException();
        }

        internal SOFTTEK.SCMS.Entity.Shared.Category RegisterCategory(SCMS.Entity.Shared.Category category)
        {
            throw new NotImplementedException();
        }
    }
}
