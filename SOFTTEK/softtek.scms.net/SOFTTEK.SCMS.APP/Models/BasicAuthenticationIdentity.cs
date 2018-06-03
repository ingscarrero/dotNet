using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOFTTEK.SCMS.SRA.Models
{
    public class BasicAuthenticationIdentity : System.Security.Principal.GenericIdentity
    {
        public BasicAuthenticationIdentity(string networkAccount, string password,  string deviceIdentifier)
            : base(networkAccount, "Basic")
        {
            DeviceIdentifier = deviceIdentifier;
            User = new SOFTTEK.SCMS.Entity.Security.User
            {
                DeviceIdentifier = deviceIdentifier,
                NetworkAccount = networkAccount,
                Password = password
            };
        }

        public BasicAuthenticationIdentity(SOFTTEK.SCMS.Entity.Security.Token token, string deviceIdentifier)
            : base(token.Identifier, "Basic")
        {
            DeviceIdentifier = deviceIdentifier;
            Token = token;
        }

        public string DeviceIdentifier { get; set; }
        public SOFTTEK.SCMS.Entity.Security.User User { get; set; }
        public SOFTTEK.SCMS.Entity.Security.Token Token { get; set; }
    }
}