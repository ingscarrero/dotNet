using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFTTEK.SCMS.Entity.SRA
{
    public class Activity
    {
        public long Identifier { get; set; }
        public double Effort { get; set; }

        public Shared.Employee Employee { get; set; }
        public Shared.Employee ApprovedBy { get; set; }
        
        public DateTime ExecutedAt { get; set; }
        public DateTime ReportedAt { get; set; }
        public DateTime ApprovedAt { get; set; }

        public string Project { get; set; }
        public string ActivityCode { get; set; }
        public virtual string Details { get; set; }
        public string State { get; set; }




        public string ApprovedComments { get; set; }
        public string Jornade { get; set; }
    }
}
