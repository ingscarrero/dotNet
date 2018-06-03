using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SF.DataModel;

namespace SOFTTEK.SCMS.Entity.FA
{
    public class PhysicalInventoryTaking
    {
        [PrimaryKeyField, OutputField, FilterField]
        public long Identifier { get; set; }

        [ForeignKeyField(typeof(Shared.Employee), "Identifier"), InputField, OutputField, FilterField]
        public Shared.Employee Accountable { get; set; }

        [ForeignKeyField(typeof(Shared.Employee), "Identifier"), InputField, OutputField, FilterField]
        public Shared.Employee Responsible { get; set; }

        [ForeignKeyField(typeof(Shared.Employee), "Identifier"), InputField, OutputField, FilterField]
        public Shared.Employee Informed { get; set; }

        [ForeignKeyField(typeof(WorkOrder), "Identifier"), InputField, OutputField, FilterField]
        public WorkOrder WorkOrder { get; set; }

        [InputField, OutputField, FilterField]
        public string Location { get; set; }

        [InputField, OutputField, FilterField]
        public string Status { get; set; }

        [InputField, OutputField, FilterField]
        public string Comments { get; set; }

        [CalculatedField, OutputField, FilterField]
        public DateTime UpdatedAt { get; set; }

        [CalculatedField, OutputField, FilterField]
        public DateTime RegisteredAt { get; set; }

        public List<PhysicalInventoryTakingItem> Items { get; set; }
        
    }
}
