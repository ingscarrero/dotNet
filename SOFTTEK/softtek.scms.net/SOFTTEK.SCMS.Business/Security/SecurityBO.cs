using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SCMS.Data;

namespace SOFTTEK.SCMS.Business.Security
{
    public class SecurityBO : SOFTTEK.SCMS.Foundation.Business.BusinessObject
    {
        private SRADataContext dataSource;

        public SecurityBO(SOFTTEK.SCMS.Foundation.Business.BusinessContext ctx)
            : base(ctx)
        { 
        }

        public SOFTTEK.SCMS.Entity.Security.Token Authorize(SOFTTEK.SCMS.Entity.Security.User user)
        {
            SOFTTEK.SCMS.Entity.Security.Token authorizationToken = null;
            return context.Execute(() => {

                using (dataSource = new SRADataContext(context.SecurityContext))
                {
                    Entity.User decryptedUser = new Entity.User(user, SCMS.Foundation.Security.EncryptionStatus.EncryptionStatusDecrypted, symmetricCipherProvider);
                    
                    dataSource.ConnectionString = "SCMS";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();
                    
                    authorizationToken = dataSource.AuthenticateUser(decryptedUser);
                }
                if (authorizationToken != null)
                {
                    Business.Entity.Token encryptedToken = new Entity.Token(authorizationToken, SCMS.Foundation.Security.EncryptionStatus.EncryptionStatusEncrypted, symmetricCipherProvider);
                    return encryptedToken;
                }
                return null;
            }, "Authenticate the provided user credentials with the security provider, and identify authenticated user authorization profile.");  
        }

        public SOFTTEK.SCMS.Entity.Security.Token Register(SOFTTEK.SCMS.Entity.Security.User user)
        {
            SOFTTEK.SCMS.Entity.Security.Token authorizationToken = null;

            return context.Execute(()=> {
                Entity.User decryptedUser = new Entity.User(user, SCMS.Foundation.Security.EncryptionStatus.EncryptionStatusDecrypted, symmetricCipherProvider);



                using (dataSource = new SRADataContext(context.SecurityContext))
                {
                    dataSource.ConnectionString = "SCMS";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();

                    authorizationToken = dataSource.CreateUser(decryptedUser);
                }
                if (authorizationToken != null)
                {
                    Business.Entity.Token encryptedToken = new Entity.Token(authorizationToken, SCMS.Foundation.Security.EncryptionStatus.EncryptionStatusEncrypted, symmetricCipherProvider);
                    return encryptedToken;
                }
                return null;
            }, "Register an user for the provided user credentials, and retrieve the default authorization profile.");  
        }

        public SOFTTEK.SCMS.Entity.Security.Token GetToken()
        {
            SOFTTEK.SCMS.Entity.Security.Token authorizationToken = null;
            return context.Execute(() =>
            {

                using (dataSource = new SRADataContext(context.SecurityContext))
                {

                    dataSource.ConnectionString = "SCMS";
                    dataSource.DefaultUser = new System.Configuration.AppSettingsReader().GetValue("S_APP_UID", typeof(string)).ToString();
                    dataSource.Initialize();

                    authorizationToken = dataSource.GetToken();
                }

                if (authorizationToken != null)
                {
                    Business.Entity.Token encryptedToken = new Entity.Token(authorizationToken, SCMS.Foundation.Security.EncryptionStatus.EncryptionStatusEncrypted, symmetricCipherProvider);
                    return encryptedToken;
                }

                return null;

            }, "Retrieve the authorization token information for the provided token identifier and device identifier." );
        }
    }
}
