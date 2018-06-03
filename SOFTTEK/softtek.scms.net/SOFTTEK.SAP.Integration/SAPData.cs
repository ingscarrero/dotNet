using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFTTEK.SAP.Integration
{
    public class SAPData
    {
        private const string kSAPDateFormat = "dd.MM.yyyy";
        private const string kSAPTimeFormat = "HH:mm:ss";
        private const string kSAPStructureDateFormat = "yyyyMMdd";
        private const string kSAPStructureTimeFormat = "{0:00}:{1:00}:{2:00}";
        private const string kSAPStructureRepresentationSeparator = "|";

        //Obtiene el tipo de dato del campo equivalente de SAP a .NET
        public static Type GetDataType(RfcDataType rfcDataType)
        {
            switch (rfcDataType)
            {
                case RfcDataType.DATE:
                    return typeof(DateTime);

                case RfcDataType.TIME:
                    return typeof(TimeSpan);

                case RfcDataType.CHAR:
                    return typeof(string);

                case RfcDataType.STRING:
                    return typeof(string);

                case RfcDataType.BCD:
                    return typeof(decimal);

                case RfcDataType.INT2:
                    return typeof(int);

                case RfcDataType.INT4:
                    return typeof(int);

                case RfcDataType.FLOAT:
                    return typeof(double);

                default:
                    return typeof(string);
            }
        }

        /// <summary>
        /// Convierte una fecha inscrita en formato Fecha SAP
        /// </summary>
        /// <param name="dateValue">Fecha enviada</param>
        /// <returns>Fecha en formato dd.mm.rrrr</returns>
        public static string DateString(string dateString)
        {
            string sapDateString = null;
            if (!string.IsNullOrEmpty(dateString))
            {
                DateTime date = new DateTime();
                if (DateTime.TryParse(dateString, out date))
                {
                    sapDateString = SAPData.DateString(date);
                }
                else
                {
                    throw new Exception("Invalid dateString. It does not match with a valid date string.");
                }
            }
            return sapDateString;
        }

        public static string DateString(DateTime date)
        {
            string sapDateString = date.ToString(kSAPDateFormat);
            return sapDateString;
        }

        public static string TimeString(DateTime time)
        {
            string sapTimeString = time.ToString(kSAPTimeFormat);
            return sapTimeString;
        }

        public static List<T> RFCTableToGenericList<T>(IRfcTable rfcTable, Func<Dictionary<string, object>, T> mapper)
        {
            List<T> results = new List<T>();
            foreach (var item in rfcTable.ToListOfDictionaries())
            {
                results.Add(mapper(item));
            }
            return results;
        }

        public static IRfcStructure CreateRFCStructure(Dictionary<string, object> item, RfcDestination destination, string structureName)
        {
            RfcStructureMetadata metaData = destination.Repository.GetStructureMetadata(structureName);
            IRfcStructure structure = metaData.CreateStructure();

            foreach (var key in item.Keys)
            {
                if (GetDataType(structure[key].Metadata.DataType) == typeof(DateTime))
                {
                    structure.SetValue(key, ((DateTime)item[key]).ToString(kSAPStructureDateFormat));
                }
                else if (GetDataType(structure[key].Metadata.DataType) == typeof(TimeSpan))
                {
                    TimeSpan t = (TimeSpan)item[key];
                    structure.SetValue(key, string.Format(kSAPStructureTimeFormat, t.Hours, t.Minutes, t.Seconds));
                }
                else
                {
                    structure.SetValue(key, item[key]);
                }
            }

            return structure;
        }

        public static IRfcTable CreateRFCStructure(List<Dictionary<string, object>> dictionaryList, RfcDestination destination, IRfcFunction rfc, string structureName, string tableName)
        {
            IRfcTable table = rfc.GetTable(tableName);

            foreach (var item in dictionaryList)
            {
                IRfcStructure structure = CreateRFCStructure(item, destination, structureName);
                table.Insert(structure);
            }

            return table;
        }

        public static Dictionary<string, object> GetDictionaryForMetadataExport(IRfcFunction rfc, string tableName)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();

            for (int i = 0; i < rfc.Metadata.ParameterCount; i++)
            {
                if (rfc.Metadata[i].Name.ToString().ToUpper() == tableName)
                {
                    switch (rfc.Metadata[i].DataType.ToString())
                    {
                        case "STRUCTURE":
                            for (int j = 0;
                                j < rfc.Metadata[i].ValueMetadataAsStructureMetadata.FieldCount;
                                j++)
                            {
                                dictionary[rfc.Metadata[i].ValueMetadataAsStructureMetadata[j].Name] = Activator.CreateInstance(SAPData.GetDataType(rfc.Metadata[i].ValueMetadataAsStructureMetadata[j].DataType));
                            }
                            break;

                        case "TABLE":
                            IRfcTable detail = rfc[tableName].GetTable();
                            /*for (int j = 0; j < detail.Count(); j++)
                            {
                                var valueObj = detail[j].GetValue(rfc.Metadata[i].ValueMetadataAsTableMetadata[i].Name);
                                dictionary[rfc.Metadata[i].ValueMetadataAsTableMetadata[i].Name] = valueObj;
                            }*/
                            for (int j = 0;
                                j < rfc.Metadata[i].ValueMetadataAsTableMetadata.LineType.FieldCount;
                                j++)
                            {
                                RfcFieldMetadata jd = rfc.Metadata[i].ValueMetadataAsTableMetadata[j];

                                Type t = SAPData.GetDataType(rfc.Metadata[i].ValueMetadataAsTableMetadata[j].DataType);
                                var valueObject = detail.GetValue(rfc.Metadata[i].ValueMetadataAsTableMetadata[j].Name);
                                object value = new object();
                                System.Reflection.PropertyInfo propInfo = t.GetType().GetProperty("UnderlyingSystemType");
                                if (t.Name != "String")
                                {
                                    Type type;

                                    switch (t.Name)
                                    {
                                        case "Decimal":
                                            type = (Type)propInfo.GetValue(typeof(System.Decimal), null);
                                            value = Activator.CreateInstance(type, valueObject);
                                            break;

                                        case "int":
                                            type = (Type)propInfo.GetValue(typeof(System.Int64), null);
                                            value = Activator.CreateInstance(type, valueObject);
                                            break;

                                        case "double":
                                            type = (Type)propInfo.GetValue(typeof(System.Double), null);
                                            value = Activator.CreateInstance(type, valueObject);
                                            break;

                                        default:
                                            type = (Type)propInfo.GetValue(t, null);
                                            value = Activator.CreateInstance(typeof(System.String), valueObject);
                                            break;
                                    }
                                }
                                else
                                {
                                    //Type type = (Type)propInfo.GetValue(t, null);
                                    value = valueObject.ToString();
                                }

                                dictionary[rfc.Metadata[i].ValueMetadataAsTableMetadata[j].Name] = value;
                            }
                            break;
                    }
                }
            }
            return dictionary;
        }

        public static List<Dictionary<string, object>> GetListDictionaryForMetadataExport(IRfcFunction rfc, string tableName)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            List<Dictionary<string, object>> listDictionary = new List<Dictionary<string, object>>();

            for (int i = 0; i < rfc.Metadata.ParameterCount; i++)
            {
                if (rfc.Metadata[i].Name.ToString().ToUpper() == tableName)
                {
                    switch (rfc.Metadata[i].DataType.ToString())
                    {
                        case "STRUCTURE":
                            IRfcStructure detailStructure = rfc[tableName].GetStructure();
                            dictionary = new Dictionary<string, object>();
                            for (int j = 0; j < detailStructure.Count(); j++)
                            {
                                //dictionary[rfc.Metadata[i].ValueMetadataAsStructureMetadata[j].Name] = Activator.CreateInstance(SAPData.GetDataType(rfc.Metadata[i].ValueMetadataAsStructureMetadata[j].DataType));

                                var valueObj = detailStructure.GetValue(rfc.Metadata[i].ValueMetadataAsStructureMetadata[j].Name);
                                dictionary[rfc.Metadata[i].ValueMetadataAsStructureMetadata[j].Name] = valueObj;
                            }
                            listDictionary.Add(dictionary);
                            break;

                        case "TABLE":
                            IRfcTable detail = rfc[tableName].GetTable();
                            for (int j = 0; j < detail.Count(); j++)
                            {
                                //foreach (IRfcStructure column in detail[j])
                                dictionary = new Dictionary<string, object>();
                                for (int x = 0; x < detail[j].Count(); x++)
                                {
                                    var valueObj = detail[j].GetValue(rfc.Metadata[i].ValueMetadataAsTableMetadata[x].Name);
                                    dictionary[rfc.Metadata[i].ValueMetadataAsTableMetadata[x].Name] = valueObj;
                                }
                                listDictionary.Add(dictionary);
                            }

                            break;
                    }
                }
            }
            return listDictionary;
        }

        public static string GetStringForMetadataExport(IRfcFunction rfc, string tableName)
        {
            string dataTableStringRepresentation = String.Empty;

            for (int i = 0; i < rfc.Metadata.ParameterCount; i++)
            {
                if (rfc.Metadata[i].Direction.ToString().ToUpper() == "EXPORT" || rfc.Metadata[i].Direction.ToString().ToUpper() == "TABLES")
                {
                    if (rfc.Metadata[i].Name.ToString().ToUpper() == tableName.ToUpper())
                    {
                        switch (rfc.Metadata[i].DataType.ToString())
                        {
                            case "STRUCTURE":
                                for (int j = 0;
                                    j < rfc.Metadata[i].ValueMetadataAsStructureMetadata.FieldCount;
                                    j++)
                                {
                                    string.Concat(dataTableStringRepresentation, rfc.Metadata[i].ValueMetadataAsStructureMetadata[j].Name, kSAPStructureRepresentationSeparator);
                                }
                                break;

                            case "TABLE":
                                for (int j = 0;
                                    j < rfc.Metadata[i].ValueMetadataAsTableMetadata.LineType.FieldCount;
                                    j++)
                                {
                                    string.Concat(dataTableStringRepresentation, rfc.Metadata[i].ValueMetadataAsTableMetadata[j].Name, kSAPStructureRepresentationSeparator);
                                }
                                break;
                        }
                    }
                }
            }
            return dataTableStringRepresentation.Substring(0, dataTableStringRepresentation.Length - kSAPStructureRepresentationSeparator.Length);
        }
    }
}