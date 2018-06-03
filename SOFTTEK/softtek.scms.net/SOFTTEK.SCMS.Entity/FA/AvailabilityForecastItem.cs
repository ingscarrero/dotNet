using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SF.DataModel;

namespace SOFTTEK.SCMS.Entity.FA
{
    public class AvailabilityForecastItem
    {
        [PrimaryKeyField, OutputField, FilterField]
        public long Identifier { get; set; }

        [ForeignKeyField(typeof(FixedAsset), "Identifier"), InputField, OutputField]
        public FixedAsset FixedAsset { get; set; }
        
        [InputField, OutputField]
        public int Stock { get; set; }

        [InputField, OutputField, FilterField]
        public string Status { get; set; }

        [InputField, OutputField]
        public DateTime From { get; set; }
        
        [InputField, OutputField]
        public DateTime To { get; set; }

        [ForeignKeyField(typeof(FixedAsset), "Identifier"), InputField, OutputField, FilterField]
        public AvailabilityForecast AvailabilityForecast { get; set; }
    }
}
