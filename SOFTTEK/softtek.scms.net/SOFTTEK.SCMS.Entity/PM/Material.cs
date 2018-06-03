using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SF.DataModel;

namespace SOFTTEK.SCMS.Entity.PM
{
    public class Material
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
        public string Class { get; set; }

        [InputField, OutputField]
        public double Stock { get; set; }

        [ForeignKeyField(typeof(Task), "Identifier"), InputField, OutputField, FilterField]
        public Task Task { get; set; }

        [InputField, OutputField, FilterField]
        public long MaterialParameter { get; set; }

        [InputField,OutputField]
        public string Observations { get; set; }
    }
}
