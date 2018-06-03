using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SF.DataModel;

namespace SOFTTEK.SCMS.Entity.PM
{
    public class Task
    {
        [PrimaryKeyField, OutputField, FilterField]
        public long Identifier { get; set; }

        [InputField, OutputField, FilterField]
        public string ExternalIdentifier { get; set; }

        [InputField, OutputField, FilterField]
        public string Name { get; set; }

        [InputField, OutputField, FilterField]
        public string Description { get; set; }

        [InputField, OutputField, FilterField]
        public string Performer { get; set; }
        
        [InputField, OutputField]
        public DateTime StartedAt { get; set; }

        [InputField, OutputField]
        public DateTime FinishedAt { get; set; }

        [InputField, OutputField, FilterField]
        public string Status { get; set; }

        [InputField, OutputField, FilterField]
        public string Comments { get; set; }

        [InputField, OutputField, FilterField]
        public string QuantityCapacity { get; set; }

        [InputField, OutputField, FilterField]
        public string DurationOperation { get; set; }

        [ForeignKeyField(typeof(MaintenancePlan), "Identifier"), InputField, OutputField, FilterField]
        public MaintenancePlan Plan { get; set; }

        [ForeignKeyField(typeof(WorkOrder), "Identifier"), InputField, OutputField, FilterField]
        public WorkOrder WorkOrder { get; set; }
    }
}
