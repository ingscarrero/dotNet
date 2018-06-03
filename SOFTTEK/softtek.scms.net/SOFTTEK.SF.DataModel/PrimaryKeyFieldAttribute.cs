using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFTTEK.SF.DataModel
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class PrimaryKeyFieldAttribute : System.Attribute
    {
        private const string displayText = "Primary Key Field";
        private const string fieldAlias = "id";
        private const string fieldSuffix = "Pk";

        public string Alias { get { return fieldAlias; } }
        public string Suffix { get { return fieldSuffix; } }

        public override string ToString()
        {
            return displayText;
        }
    }
}
