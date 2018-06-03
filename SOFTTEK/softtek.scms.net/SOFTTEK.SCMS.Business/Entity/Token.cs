using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFTTEK.SCMS.Business.Entity
{
   

    class Token : SOFTTEK.SCMS.Entity.Security.Token, SOFTTEK.SCMS.Foundation.Security.IEncryptable
    {

        public Token()
        {

        }

        public Token(SOFTTEK.SCMS.Entity.Security.Token token, SOFTTEK.SCMS.Foundation.Security.EncryptionStatus encryptionStatus = SCMS.Foundation.Security.EncryptionStatus.EncryptionStatusNone, SOFTTEK.SCMS.Foundation.Security.SymmetricCipherProvider symmetricCipherProvider = null)
        {
            Identifier = token.Identifier;
            UserIS = token.UserIS;
            CreatedAt = token.CreatedAt;
            ExpiresAt = token.ExpiresAt;

            switch (encryptionStatus)
            {
                case SOFTTEK.SCMS.Foundation.Security.EncryptionStatus.EncryptionStatusEncrypted:

                    Encrypt(symmetricCipherProvider);

                    break;
                case SOFTTEK.SCMS.Foundation.Security.EncryptionStatus.EncryptionStatusDecrypted:

                    Decrypt(symmetricCipherProvider);

                    break;
                default:
                    break;
            }
        }

        public void Encrypt(SCMS.Foundation.Security.SymmetricCipherProvider symmetricCipherProvider)
        {
            if (symmetricCipherProvider == null)
            {
                throw new Exception("The provided symmetric cipher provider is not instantiated.", new ArgumentNullException("symmetricCipherProvider"));
            }

            UserIS = string.IsNullOrEmpty(UserIS) ? string.Empty : symmetricCipherProvider.EncryptData(UserIS);
            Identifier = string.IsNullOrEmpty(Identifier) ? string.Empty : symmetricCipherProvider.EncryptData(Identifier);
        }

        public void Decrypt(SCMS.Foundation.Security.SymmetricCipherProvider symmetricCipherProvider)
        {
            if (symmetricCipherProvider == null)
            {
                throw new Exception("The provided symmetric cipher provider is not instantiated.", new ArgumentNullException("symmetricCipherProvider"));
            }

            UserIS = string.IsNullOrEmpty(UserIS) ? string.Empty : symmetricCipherProvider.DecryptData(UserIS);
            Identifier = string.IsNullOrEmpty(Identifier) ? string.Empty : symmetricCipherProvider.DecryptData(Identifier);
        }
    }
}
