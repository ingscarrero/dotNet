using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SF.DataModel;

namespace SOFTTEK.SCMS.Entity.PM
{
    public class MaintenancePlan
    {
        [PrimaryKeyField, OutputField, FilterField]
        public long Identifier { get; set; }

        public List<Task> Activities { get; set; }

        [InputField, OutputField, FilterField]
        public string ExternalIdentifier { get; set; }

        [InputField, OutputField, FilterField]
        public string Description { get; set; }

        [InputField, OutputField, FilterField]
        public string Comments { get; set; }

        [InputField, OutputField, FilterField]
        public string DeviceType { get; set; }

        [ForeignKeyField(typeof(PM.WorkOrder), "Identifier"), InputField, OutputField, FilterField]
        public long WorkOrder { get; set; }
    }
}
