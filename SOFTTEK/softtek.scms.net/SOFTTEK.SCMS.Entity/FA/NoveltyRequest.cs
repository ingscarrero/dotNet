using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SF.DataModel;

namespace SOFTTEK.SCMS.Entity.FA
{
    public class NoveltyRequest : Request
    {
        [ForeignKeyField(typeof(Request), "Identifier"), InputField, OutputField, FilterField]
        public Request Request { get; set; }
        [InputField, OutputField, FilterField]
        public string ExternalIdentifier { get; set; }
    }
}
