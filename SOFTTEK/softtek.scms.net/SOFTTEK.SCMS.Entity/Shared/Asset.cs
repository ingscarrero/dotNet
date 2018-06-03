using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SF.DataModel;

namespace SOFTTEK.SCMS.Entity.Shared
{
    public class Asset
    {
        [PrimaryKeyField, OutputField, FilterField]
        public long Identifier { get; set; }

        [InputField, OutputField, FilterField]
        public string ExternalIdentifier { get; set; }

        [InputField, OutputField, FilterField]
        public string Description { get; set; }

        [InputField, OutputField, FilterField]
        public string Placement { get; set; }

        [InputField, OutputField, FilterField]
        public string PlannificationCenter { get; set; }

        [InputField, OutputField, FilterField]
        public string Area { get; set; }

        [InputField, OutputField, FilterField]
        public string CostCenter { get; set; }

    }
}
