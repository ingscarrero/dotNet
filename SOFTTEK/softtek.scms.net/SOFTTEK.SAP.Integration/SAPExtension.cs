using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAP.Middleware.Connector;

namespace SOFTTEK.SAP.Integration
{
    public static class SAPExtension
    {


        public static List<Dictionary<string, object>> ToListOfDictionaries(this IRfcTable table)
        {

            List<Dictionary<string, object>> results = new List<Dictionary<string, object>>();

            foreach (IRfcStructure row in table)
            {
                Dictionary<string, object> sapTableRecord = new Dictionary<string, object>();
                for (int liElement = 0; liElement < row.ElementCount; liElement++)
                {
                    RfcElementMetadata metadata = table.GetElementMetadata(liElement);
                    switch (metadata.DataType)
                    {
                        case RfcDataType.DATE:
                            sapTableRecord[metadata.Name] = row.GetString(metadata.Name).Substring(0, 4) + row.GetString(metadata.Name).Substring(5, 2) + row.GetString(metadata.Name).Substring(8, 2);
                            break;
                        case RfcDataType.BCD:
                            sapTableRecord[metadata.Name] = row.GetDecimal(metadata.Name);
                            break;
                        case RfcDataType.CHAR:
                            sapTableRecord[metadata.Name] = row.GetString(metadata.Name);
                            break;
                        case RfcDataType.STRING:
                            sapTableRecord[metadata.Name] = row.GetString(metadata.Name);
                            break;
                        case RfcDataType.INT2:
                            sapTableRecord[metadata.Name] = row.GetInt(metadata.Name);
                            break;
                        case RfcDataType.INT4:
                            sapTableRecord[metadata.Name] = row.GetInt(metadata.Name);
                            break;
                        case RfcDataType.FLOAT:
                            sapTableRecord[metadata.Name] = row.GetDouble(metadata.Name);
                            break;
                        default:
                            sapTableRecord[metadata.Name] = row.GetString(metadata.Name);
                            break;
                    }
                }

                results.Add(sapTableRecord);
            }

            return results;
        }
    }
}
