using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFTTEK.SCMS.Business.Entity
{
    class Person : SCMS.Entity.Shared.Person, SOFTTEK.SCMS.Foundation.Security.IEncryptable, SOFTTEK.SCMS.Foundation.Auditory.IAuditable<SOFTTEK.SCMS.Entity.Shared.Person>
    {
        public Person()
        {

        }

        public Person(SCMS.Entity.Shared.Person person, SOFTTEK.SCMS.Foundation.Security.EncryptionStatus encryptionStatus = SOFTTEK.SCMS.Foundation.Security.EncryptionStatus.EncryptionStatusNone, SOFTTEK.SCMS.Foundation.Security.SymmetricCipherProvider symmetricCipherProvider = null)
        {
            Identifier = person.Identifier;
            Identification = person.Identification;
            Name = person.Name;
            MiddleName = person.MiddleName;
            LastName = person.LastName;
            Gender = person.Gender;
            From = person.From;

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

            Identification = string.IsNullOrEmpty(Identification) ? string.Empty : symmetricCipherProvider.EncryptData(Identification);
            Name = string.IsNullOrEmpty(Name) ? string.Empty : symmetricCipherProvider.EncryptData(Name);
            MiddleName = string.IsNullOrEmpty(MiddleName) ? string.Empty : symmetricCipherProvider.EncryptData(MiddleName);
            LastName = string.IsNullOrEmpty(LastName) ? string.Empty : symmetricCipherProvider.EncryptData(LastName);
            Gender = string.IsNullOrEmpty(Gender) ? string.Empty : symmetricCipherProvider.EncryptData(Gender);
            From = string.IsNullOrEmpty(From) ? string.Empty : symmetricCipherProvider.EncryptData(From);
        }

        public void Decrypt(SCMS.Foundation.Security.SymmetricCipherProvider symmetricCipherProvider)
        {
            if (symmetricCipherProvider == null)
            {
                throw new Exception("The provided symmetric cipher provider is not instantiated.", new ArgumentNullException("symmetricCipherProvider"));
            }

            Identification = string.IsNullOrEmpty(Identification) ? string.Empty : symmetricCipherProvider.DecryptData(Identification);
            Name = string.IsNullOrEmpty(Name) ? string.Empty : symmetricCipherProvider.DecryptData(Name);
            MiddleName = string.IsNullOrEmpty(MiddleName) ? string.Empty : symmetricCipherProvider.DecryptData(MiddleName);
            LastName = string.IsNullOrEmpty(LastName) ? string.Empty : symmetricCipherProvider.DecryptData(LastName);
            Gender = string.IsNullOrEmpty(Gender) ? string.Empty : symmetricCipherProvider.DecryptData(Gender);
            From = string.IsNullOrEmpty(Gender) ? string.Empty : symmetricCipherProvider.DecryptData(From);
        }

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

        public void AddTrace(SCMS.Entity.Shared.Person trace)
        {
            throw new NotImplementedException();
        }
    }
}
