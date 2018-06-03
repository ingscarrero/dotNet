using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFTTEK.SCMS.Entity.Shared
{
    public class Contact
    {
        public Person Person { get; set; }
        public string Country { get; set; }
        public string Subdivision { get; set; }
        public string City { get; set; }
        public string Address{ get; set; }
        public string ZIP { get; set; }
        public List<string> Phones { get; set; }
    }
}
