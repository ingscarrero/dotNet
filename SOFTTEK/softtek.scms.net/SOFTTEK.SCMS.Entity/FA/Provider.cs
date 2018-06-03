using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SF.DataModel;

namespace SOFTTEK.SCMS.Entity.FA
{
    public class Provider
    {
        [PrimaryKeyField, OutputField, FilterField]
        public long Identifier { get; set; }

        [InputField, OutputField, FilterField]
        public string ExternalIdentifier { get; set; }


        [InputField, OutputField, FilterField]
        public string Name { get; set; }


        [InputField, OutputField, FilterField]
        public string Contract { get; set; }

        [InputField, OutputField, FilterField]
        public string Document { get; set; }

        [InputField, OutputField, FilterField]
        public string State { get; set; }
    }
}
