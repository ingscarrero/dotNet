using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFTTEK.SF.DataModel
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class OutputFieldAttribute : System.Attribute
    {
        private const string displayText = "Output Field";
        public override string ToString()
        {
            return displayText;
        }
    }
}
