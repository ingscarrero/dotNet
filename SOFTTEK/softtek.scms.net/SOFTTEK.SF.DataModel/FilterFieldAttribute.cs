using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFTTEK.SF.DataModel
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class FilterFieldAttribute : System.Attribute
    {
        private const string displayText = "Filter Field";
        public override string ToString()
        {
            return displayText;
        }
    }
}
