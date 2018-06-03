using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SF.DataModel;

namespace SOFTTEK.SCMS.Entity.FA
{
    public class TechnicalEvaluationRequest : Request
    {
        [InputField, OutputField, FilterField]
        public string Concept { get; set; }

        [ForeignKeyField(typeof(NoveltyRequest), "Identifier"), InputField, OutputField, FilterField]
        public NoveltyRequest Novelty { get; set; }
    }
}
