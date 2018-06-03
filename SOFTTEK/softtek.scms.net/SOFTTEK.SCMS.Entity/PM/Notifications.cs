using SOFTTEK.SF.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFTTEK.SCMS.Entity.PM
{
    public class Notifications
    {
        [PrimaryKeyField, OutputField, FilterField]
        public long Identifier { get; set; }

        [InputField, OutputField, FilterField]
        public string ExternalIdentifier { get; set; }

        [InputField, OutputField, FilterField]
        public string Priority { get; set; }

        [InputField, OutputField, FilterField]
        public string Comments { get; set; }

        [InputField, OutputField, FilterField]
        public string ActualWork { get; set; }

        [InputField, OutputField, FilterField]
        public string ExecutionStartAt { get; set; }
    }
}
