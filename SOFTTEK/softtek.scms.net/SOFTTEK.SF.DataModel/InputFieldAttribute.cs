using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFTTEK.SF.DataModel
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=true)]
    public class InputFieldAttribute : System.Attribute 
    {
        private const string displayText = "Input Field";
        public override string ToString()
        {
            return displayText;
        }
    }
}
