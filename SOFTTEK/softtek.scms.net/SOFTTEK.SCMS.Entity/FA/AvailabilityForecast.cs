using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SF.DataModel;

namespace SOFTTEK.SCMS.Entity.FA
{
    public class AvailabilityForecast
    {
        [PrimaryKeyField, OutputField, FilterField]
        public long Identifier { get; set; }

        [ForeignKeyField(typeof(Request), "Identifier"), InputField, OutputField, FilterField]
        public Request Request { get; set; }

        [CalculatedField, OutputField]
        public DateTime GeneratedAt { get; set; }

        [InputField, OutputField]
        public DateTime ValidUntil { get; set; }

        [ForeignKeyField(typeof(Shared.Employee), "Identifier"), InputField, OutputField, FilterField]
        public Shared.Employee ValidatedBy { get; set; }

        public List<AvailabilityForecastItem> Items { get; set; }
    }
}
