using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFTTEK.SCMS.Entity.SRA
{
    public class PermitsAndAbsences
    {
        public long perabs_id { get; set; }
        public int perabs_activity_code { get; set; }
        public DateTime perabs_start_at { get; set; }
        public DateTime perabs_end_at { get; set; }
        public double perabs_total_hours { get; set; }
        public string perabs_validated_by { get; set; }
        public string perabs_description { get; set; }
        public DateTime perabs_created_at { get; set; }
        public string perabs_created_by { get; set; }
        public DateTime perabs_modified_at { get; set; }
        public string perabs_modified_by { get; set; }
        public string perabs_validated_comments { get; set; }
        public Entity.Shared.Employee perabs_employee { get; set; }
        public DateTime perabs_validated_at { get; set; }
        public string perabs_Status { get; set; }
    }
}