using SOFTTEK.SF.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFTTEK.SCMS.Entity.PM
{
    public class WorkOrder
    {
        public WorkOrder()
        {
            Activities = new List<Task>();
        }

        [PrimaryKeyField, OutputField, FilterField]
        public long Identifier { get; set; }

        [InputField, OutputField, FilterField]
        public string ExternalIdentifier { get; set; }

        [InputField, OutputField, FilterField]
        public string Type { get; set; }

        [InputField, OutputField, FilterField]
        public string Company { get; set; }

        [InputField, OutputField, FilterField]
        public string Priority { get; set; }

        [InputField, OutputField, FilterField]
        public string Performer { get; set; }

        [InputField, OutputField, FilterField]
        public string State { get; set; }

        [ForeignKeyField(typeof(PM.TechnicalObject), "Identifier"), InputField, OutputField, FilterField]
        public PM.TechnicalObject TechnicalObject { get; set; }

        [InputField, OutputField]
        public DateTime ScheduledTo { get; set; }

        [InputField, OutputField]
        public DateTime ExecutionStartAt { get; set; }

        [InputField, OutputField]
        public DateTime ExecutionFinishedAt { get; set; }

        [InputField, OutputField, FilterField]
        public DateTime ReleaseDate { get; set; }

        [InputField, OutputField, FilterField]
        public string PlanningGroup { get; set; }

        [InputField, OutputField, FilterField]
        public string Workstation { get; set; }

        public List<Task> Activities { get; set; }

        [InputField, OutputField, FilterField]
        public string Activity { get; set; }
    }
}