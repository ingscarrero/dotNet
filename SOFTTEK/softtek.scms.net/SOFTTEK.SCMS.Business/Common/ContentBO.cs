using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SCMS.Data;

namespace SOFTTEK.SCMS.Business.Common
{

    public class ContentBO : SOFTTEK.SCMS.Foundation.Business.BusinessObject
    {
        private SRADataContext dataSource;

        public ContentBO(SOFTTEK.SCMS.Foundation.Business.BusinessContext ctx)
            : base(ctx)
        {
        }

        public List<SOFTTEK.SCMS.Entity.SRA.Mobile.HomeItem> GetMenuItems() {
            List<SOFTTEK.SCMS.Entity.SRA.Mobile.HomeItem> menuItems = new List<SOFTTEK.SCMS.Entity.SRA.Mobile.HomeItem>();
            return context.Execute(() => {



                using (dataSource = new SRADataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SRA";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();

                    menuItems = dataSource.GetMobileHomeItems<SOFTTEK.SCMS.Entity.SRA.Mobile.HomeItem>();
                }
                return menuItems;
            }, "Retrieve menu items for the provided APP Identifier.");
        }

    }
}
