using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFTTEK.SCMS.Business.Entity.SRA
{
    public class Week<T>
        where T: SOFTTEK.SCMS.Entity.SRA.Activity, new()

    {
        public int Number { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public List<DateTime> Holydays { get; set; }
        public List<T> ReportedActivities { get; set; }
    }
}
