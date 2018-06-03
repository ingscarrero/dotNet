using SOFTTEK.SF.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFTTEK.SCMS.Entity.PM
{
    public class Advice
    {
        [PrimaryKeyField, OutputField, FilterField]
        public long Identifier { get; set; }

        [InputField, OutputField, FilterField]
        public string ExternalIdentifier { get; set; }

        [InputField, OutputField, FilterField]
        public string Priority { get; set; }

        [InputField, OutputField, FilterField]
        public string Type { get; set; }

        [ForeignKeyField(typeof(Task), "Identifier"), InputField, OutputField, FilterField]
        public Task Task { get; set; }

        [InputField, OutputField, FilterField]
        public string DeviceType { get; set; }

        [ForeignKeyField(typeof(PM.TechnicalObject), "Identifier"), InputField, OutputField, FilterField]
        public PM.TechnicalObject TechnicalObject { get; set; }

        [InputField, OutputField, FilterField]
        public string Comments { get; set; }

        [InputField, OutputField]
        public DateTime ScheduledTo { get; set; }

        [InputField, OutputField]
        public DateTime ExecutionStartAt { get; set; }

        [InputField, OutputField]
        public DateTime ExecutionFinishedAt { get; set; }

        [InputField, OutputField]
        public string ExecutionHourStartAt { get; set; }

        [InputField, OutputField]
        public string ExecutionHourFinishedAt { get; set; }
    }
}