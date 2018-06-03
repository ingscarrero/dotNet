using SOFTTEK.SCMS.Foundation.Data;
using SOFTTEK.SCMS.Foundation.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFTTEK.SCMS.Data
{
    public class SAPDataContext : DataContext, IDisposable
    {
        public SAPDataContext(SecurityContext securityContext)
           : base(securityContext)
        {
        }

        /// <summary>
        /// Release the instance's resources
        /// </summary>
        public void Dispose()
        {
            //dataProvider.Dispose();
            Dispose(false);
        }

        /// <summary>
        /// Implementation of the Dispose Pattern for the disposable instance of the Data Context by releasing all its disposable resources.
        /// <param name="disposing">Flag that indicates the current disposing status for the Data Context instance</param>
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Dispose();
            }
            //dataProvider = null;
        }

        public List<SOFTTEK.SCMS.Entity.Shared.Parameter<string>> GetPMMasterData()
        {
            SOFTTEK.SCMS.Data.External.SAPDataProvider sapDataProvider = new Data.External.SAPDataProvider(this);
            List<SOFTTEK.SCMS.Entity.Shared.Parameter<string>> parameters = new List<Entity.Shared.Parameter<string>>();

            Dictionary<string, object> rfcResponseParameters = sapDataProvider.GetMasterDataFromRFC<Entity.Shared.Parameter<string>>((s) =>
            {
                if ("ZPMT_DATOS_MAESTROS".Equals(s))
                {
                    return (a) => new Entity.Shared.Parameter<string>
                    {
                        Value = a["LLAVE"].ToString(),
                        Description = a["CATEGORIA"].ToString(),
                        Comments = a["DESCRIPCION"].ToString(),
                        ExternalIdentifier = string.Format("{0}-{1}", a["CATEGORIA"].ToString(), a["LLAVE"].ToString()),
                    };
                }
                else
                {
                    return null;
                }
            });

            parameters = (List<SOFTTEK.SCMS.Entity.Shared.Parameter<string>>)((dynamic)rfcResponseParameters["ZPMT_DATOS_MAESTROS"]).Value;
            return parameters;
        }
    }
}