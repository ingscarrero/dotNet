using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFTTEK.SCMS.Business.Entity.SRA
{
    public class ARSDetailedActivity:SOFTTEK.SCMS.Entity.SRA.DetailedActivity
    {

        protected override string ExtractFieldFromDetails(string fieldName)
        {
            string result = string.Empty;

            if (String.IsNullOrEmpty(details)) {
                return result;
            }

            var detailTextParts = details.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            var detailItems = detailTextParts.Select((s) =>
            {
                var pair = s.Split(new char[] { ':' });

                KeyValuePair<string, string> kv = new KeyValuePair<string, string>();
                if (pair != null)
                {
                    if (pair.Length == 2)
                    {
                        kv = new KeyValuePair<string, string>(pair[0], pair[1]);
                    }
                }
                else
                {
                    kv = new KeyValuePair<string, string>("undefined", "");
                }
                
                return kv;
            });

            return detailItems.Where(i => i.Key == fieldName).FirstOrDefault().Value;
        }
    }
}
