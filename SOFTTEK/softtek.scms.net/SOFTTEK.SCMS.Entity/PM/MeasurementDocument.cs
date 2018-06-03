using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SF.DataModel;

namespace SOFTTEK.SCMS.Entity.PM
{
    public class MeasurementDocument
    {
        [PrimaryKeyField, OutputField, FilterField]
        public long Identifier { get; set; }

        [InputField, OutputField, FilterField]
        public string ExternalIdentifier { get; set; }

        [ForeignKeyField(typeof(Task), "Identifier"), InputField, OutputField, FilterField]
        public Task Task { get; set; }

        [InputField, OutputField, FilterField]
        public string DeviceType { get; set; }

        [ForeignKeyField(typeof(PM.TechnicalObject), "Identifier"), InputField, OutputField, FilterField]
        public PM.TechnicalObject TechnicalObject { get; set; }
        
        public List<Measure> Measures { get; set; }
    }
}
