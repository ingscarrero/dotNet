using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFTTEK.SCMS.Entity.Shared
{
    public class Employee
    {
        public int Identifier { get; set; }
        public Security.User User { get; set; }
        public Contact Contact { get; set; }
        public Person Person { get; set; }
        public string Role { get; set; }
        public DateTime HiredAt { get; set; }
        public string Area { get; set; }
        public Employee Supervisor { get; set; }
        public string Comments { get; set; }
        public string ImageURL { get; set; }
    }
}
