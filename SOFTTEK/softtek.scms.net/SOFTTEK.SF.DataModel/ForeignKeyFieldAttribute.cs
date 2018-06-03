using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace SOFTTEK.SF.DataModel
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ForeignKeyFieldAttribute:System.Attribute
    {
        private const string displayText = "Foreign Key Field";
        private const string fieldSuffix = "Fk";

        private readonly Type linkedEntity;
        private List<string> linkedEntityFields { get; set; }

        public string Suffix { get { return fieldSuffix; } }
        public Type LinkedEntity { get { return linkedEntity; } }
        public List<string> LinkedEntityFields { get { return linkedEntityFields; } }


        public ForeignKeyFieldAttribute(Type linkedEntity, string linkedEntityFields)
        {
            this.linkedEntity = linkedEntity;
            this.linkedEntityFields = string.IsNullOrEmpty(linkedEntityFields) ? GetEntityPrimaryKey(linkedEntity) : linkedEntityFields.Split(',').
                Select(s=> {
                    if (IsValidFieldForEntity(linkedEntity, s)) {
                        return s;
                    } else {
                        throw new ArgumentException(string.Format("The field [{0}] is not a valid field of Entity [{1}]. Please valdate and try again", s, linkedEntity.Name));
                    }
                }).ToList();
        }

        private bool IsValidFieldForEntity(Type linkedEntity, string s)
        {
            bool isValid = linkedEntity.GetProperties().Where(p => p.Name.Equals(s)).Count() > 0;
            return isValid;
        }

        private List<string> GetEntityPrimaryKey(Type linkedEntity)
        {
            List<string> primaryKey = new List<string>();
            foreach (PropertyInfo pI in linkedEntity.GetProperties()) {
                foreach (object attribute in pI.GetCustomAttributes(true)) {
                    if (attribute is PrimaryKeyFieldAttribute)
                    {
                        primaryKey.Add(pI.Name);
                    }
                }
            }
            return primaryKey;
        }

        public override string ToString()
        {
            return displayText;
        }
    }
}
