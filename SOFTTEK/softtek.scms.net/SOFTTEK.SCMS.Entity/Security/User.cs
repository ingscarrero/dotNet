using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFTTEK.SCMS.Entity.Security
{
    public class User
    {
        public string DeviceIdentifier { get; set; }
        public string Identifier { get; set; }
        public string NetworkAccount { get; set; }
        public string Password { get; set; }
    }
}
