using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SF.DataModel;

namespace SOFTTEK.SCMS.Entity.FA
{
    public class Request
    {
        [PrimaryKeyField, OutputField, FilterField]
        public long Identifier { get; set; }

        [InputField, OutputField, FilterField]
        public string Number { get; set; }

        [ForeignKeyField(typeof(Shared.Parameter<string>), "Identifier,Value"), InputField, OutputField, FilterField]
        public Shared.Parameter<string> Type { get; set; }

        [ForeignKeyField(typeof(Shared.Employee), "Identifier"), InputField, OutputField, FilterField]
        public Shared.Employee Informed { get; set; }
        
        [ForeignKeyField(typeof(Shared.Employee), "Identifier"), InputField, OutputField, FilterField]
        public Shared.Employee Responsible { get; set; }
        
        [ForeignKeyField(typeof(Shared.Employee), "Identifier"), InputField, OutputField, FilterField]
        public Shared.Employee Accountable { get; set; }

        [InputField, OutputField, FilterField]
        public string Details { get; set; }

        [InputField, OutputField, FilterField]
        public string Comments { get; set; }

        [InputField, OutputField, FilterField]
        public string Topic { get; set; }

        [InputField, OutputField, FilterField]
        public string Status { get; set; }

        [CalculatedField, OutputField, FilterField]
        public DateTime UpdatedAt { get; set; }

        [CalculatedField, OutputField, FilterField]
        public DateTime RegisteredAt { get; set; }
        
    }
}
