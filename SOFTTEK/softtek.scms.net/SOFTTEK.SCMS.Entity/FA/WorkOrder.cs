using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SF.DataModel;

namespace SOFTTEK.SCMS.Entity.FA
{
    public class WorkOrder
    {
        [PrimaryKeyField, OutputField, FilterField]
        public long Identifier { get; set; }
        
        [InputField, OutputField, FilterField]
        public string Number { get; set; }

        [InputField, OutputField, FilterField]
        public string Status { get; set; }

        [CalculatedField, OutputField, FilterField]
        public DateTime IssuedAt { get; set; }

        [InputField, OutputField, FilterField]
        public DateTime ScheduledTo { get; set; }

        [ForeignKeyField(typeof(Provider), "Identifier"), InputField, OutputField, FilterField]
        public Provider Provider { get; set; }

        [ForeignKeyField(typeof(PhysicalInventoryTaking), "Identifier"), InputField, OutputField, FilterField]
        public PhysicalInventoryTaking PhysicalInventoryTaking { get; set; }

        [InputField, OutputField, FilterField]
        public string Description { get; set; }
    }
}
