using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SF.DataModel;

namespace SOFTTEK.SCMS.Entity.PM
{
    public class Measure
    {
        [PrimaryKeyField, OutputField, FilterField]
        public long Identifier { get; set; }

        [InputField, OutputField, FilterField]
        public string UnityOfMeasurement { get; set; }
        
        [InputField, OutputField]
        public string Value { get; set; }
        
        [InputField, OutputField, FilterField]
        public string ExternalIdentifier { get; set; }
        
        [InputField, OutputField, FilterField]
        public string Comments { get; set; }

        [ForeignKeyField(typeof(MeasurementDocument), "Identifier"), InputField, OutputField, FilterField]
        public MeasurementDocument Document { get; set; }
        
        [InputField, OutputField, FilterField]
        public string DeviceType { get; set; }

        [ForeignKeyField(typeof(PM.TechnicalObject), "Identifier"), InputField, OutputField, FilterField]
        public PM.TechnicalObject TechnicalObject { get; set; }

    }
}
