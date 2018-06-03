using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SF.DataModel;

namespace SOFTTEK.SCMS.Entity.PM
{
    public class Activity
    {
        [PrimaryKeyField, OutputField, FilterField]
        public long Identifier { get; set; }

        [InputField, OutputField, FilterField]
        public string ExternalIdentifier { get; set; }

        [InputField, OutputField, FilterField]
        public string Name { get; set; }

        [InputField, OutputField, FilterField]
        public string Description { get; set; }

        [InputField, OutputField]
        public DateTime ExecutionStartAt { get; set; }

        [InputField, OutputField]
        public DateTime ExecutionFinishedAt { get; set; }

        [InputField, OutputField, FilterField]
        public int TotalDuration { get; set; }

        [InputField, OutputField, FilterField]
        public string Status { get; set; }

        [InputField, OutputField, FilterField]
        public string Comments { get; set; }

        [ForeignKeyField(typeof(MaintenancePlan), "Identifier"), InputField, OutputField, FilterField]
        public long MaintenancePlan { get; set; }
    }
}
