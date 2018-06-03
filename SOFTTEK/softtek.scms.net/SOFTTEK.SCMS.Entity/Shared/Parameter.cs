using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFTTEK.SCMS.Entity.Shared
{
    public class Parameter<T>
    {
        public int Identifier { get; set; }
        public  T Value { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; }
        public string ExternalIdentifier { get; set; }
        public string Comments { get; set; }

        public Parameter()
        {
            Category = new Category();
        }
    }
}
