using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFTTEK.SCMS.Business.Entity
{
    class User : SOFTTEK.SCMS.Entity.Security.User, SOFTTEK.SCMS.Foundation.Auditory.IAuditable<User>, SOFTTEK.SCMS.Foundation.Security.IEncryptable
    {
        public User()
        {

        }

        public User(SOFTTEK.SCMS.Entity.Security.User user, SOFTTEK.SCMS.Foundation.Security.EncryptionStatus encryptionStatus = SCMS.Foundation.Security.EncryptionStatus.EncryptionStatusNone, SOFTTEK.SCMS.Foundation.Security.SymmetricCipherProvider symmetricCipherProvider = null)
        {
            DeviceIdentifier = user.DeviceIdentifier;
            NetworkAccount = user.NetworkAccount;
            Password = user.Password;
            Identifier = user.Identifier;

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

        #region IAuditable
        public string CreatedBy
        {
            get;
            set;
        }

        public DateTime CreatedAt
        {
            get;
            set;
        }

        public string ModifiedBy
        {
            get;
            set;
        }

        public DateTime ModifiedAt
        {
            get;
            set;
        }

        public List<SOFTTEK.SCMS.Foundation.Auditory.AuditableTrace> GetAuditoryTrace()
        {
            throw new NotImplementedException();
        }

        public void AddTrace(User trace)
        {
            Func<User, List<SOFTTEK.SCMS.Foundation.Auditory.AuditableTrace>> auditor = (u) =>
            {
                List<SOFTTEK.SCMS.Foundation.Auditory.AuditableTrace> auditory = new List<SOFTTEK.SCMS.Foundation.Auditory.AuditableTrace>();

                SOFTTEK.SCMS.Foundation.Auditory.AuditableTrace f1 = new SOFTTEK.SCMS.Foundation.Auditory.AuditableTrace
                {
                    Field = "NetworkAccount",
                    OldValue = u.NetworkAccount,
                    NewValue = this.NetworkAccount,
                    ModifiedAt = DateTime.Now
                };

                SOFTTEK.SCMS.Foundation.Auditory.AuditableTrace f2 = new SOFTTEK.SCMS.Foundation.Auditory.AuditableTrace
                {
                    Field = "Password",
                    OldValue = u.Password,
                    NewValue = this.Password,
                    ModifiedAt = DateTime.Now
                };

                SOFTTEK.SCMS.Foundation.Auditory.AuditableTrace f3 = new SOFTTEK.SCMS.Foundation.Auditory.AuditableTrace
                {
                    Field = "DeviceIdentifier",
                    OldValue = u.DeviceIdentifier,
                    NewValue = this.DeviceIdentifier,
                    ModifiedAt = DateTime.Now
                };

                SOFTTEK.SCMS.Foundation.Auditory.AuditableTrace f4 = new SOFTTEK.SCMS.Foundation.Auditory.AuditableTrace
                {
                    Field = "Identifier",
                    OldValue = u.Identifier,
                    NewValue = this.Identifier,
                    ModifiedAt = DateTime.Now
                };

                auditory.Add(f1);
                auditory.Add(f2);
                auditory.Add(f3);
                auditory.Add(f4);

                return auditory;
            };

        }
        #endregion

        public void Encrypt(SOFTTEK.SCMS.Foundation.Security.SymmetricCipherProvider symmetricCipherProvider)
        {
            if (symmetricCipherProvider == null)
            {
                throw new Exception("The provided symmetric cipher provider is not instantiated.", new ArgumentNullException("symmetricCipherProvider"));
            }

            DeviceIdentifier = string.IsNullOrEmpty(DeviceIdentifier) ? string.Empty : symmetricCipherProvider.EncryptData(DeviceIdentifier);
            Identifier = string.IsNullOrEmpty(Identifier) ? string.Empty : symmetricCipherProvider.EncryptData(Identifier);
            NetworkAccount = string.IsNullOrEmpty(NetworkAccount) ? string.Empty : symmetricCipherProvider.EncryptData(NetworkAccount);
            Password = string.IsNullOrEmpty(Password) ? string.Empty : symmetricCipherProvider.EncryptData(Password);
        }

        public void Decrypt(SOFTTEK.SCMS.Foundation.Security.SymmetricCipherProvider symmetricCipherProvider)
        {
            if (symmetricCipherProvider == null)
            {
                throw new Exception("The provided symmetric cipher provider is not instantiated.", new ArgumentNullException("symmetricCipherProvider"));
            }

            DeviceIdentifier = string.IsNullOrEmpty(DeviceIdentifier) ? string.Empty : symmetricCipherProvider.DecryptData(DeviceIdentifier);
            Identifier = string.IsNullOrEmpty(Identifier) ? string.Empty : symmetricCipherProvider.DecryptData(Identifier);
            NetworkAccount = string.IsNullOrEmpty(NetworkAccount) ? string.Empty : symmetricCipherProvider.DecryptData(NetworkAccount);
            Password = string.IsNullOrEmpty(Password) ? string.Empty : symmetricCipherProvider.DecryptData(Password);
        }
    }
}
