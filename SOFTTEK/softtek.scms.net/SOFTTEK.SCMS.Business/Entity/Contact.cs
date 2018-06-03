using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFTTEK.SCMS.Business.Entity
{
    class Contact : SOFTTEK.SCMS.Entity.Shared.Contact, SOFTTEK.SCMS.Foundation.Security.IEncryptable, SOFTTEK.SCMS.Foundation.Auditory.IAuditable<SOFTTEK.SCMS.Entity.Shared.Contact>
    {
        public Contact()
        {

        }

        public Contact(SOFTTEK.SCMS.Entity.Shared.Contact contact, SOFTTEK.SCMS.Foundation.Security.EncryptionStatus encryptionStatus = SCMS.Foundation.Security.EncryptionStatus.EncryptionStatusNone, SOFTTEK.SCMS.Foundation.Security.SymmetricCipherProvider symmetricCipherProvider = null)
        {
            Person = contact.Person;
            Country = contact.Country;
            Subdivision = contact.Subdivision;
            City = contact.City;
            Address = contact.Address;
            ZIP = contact.ZIP;
            Phones = contact.Phones;

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

            Person = new Entity.Person(Person, SCMS.Foundation.Security.EncryptionStatus.EncryptionStatusEncrypted, symmetricCipherProvider);
            Country = string.IsNullOrEmpty(Country) ? string.Empty : symmetricCipherProvider.EncryptData(Country);
            Subdivision = string.IsNullOrEmpty(Subdivision) ? string.Empty : symmetricCipherProvider.EncryptData(Subdivision);
            City = string.IsNullOrEmpty(City) ? string.Empty : symmetricCipherProvider.EncryptData(City);
            Address = string.IsNullOrEmpty(Address) ? string.Empty : symmetricCipherProvider.EncryptData(Address);
            ZIP = string.IsNullOrEmpty(ZIP) ? string.Empty : symmetricCipherProvider.EncryptData(ZIP);
            Phones = Phones == null ? null : Phones.Select(p => string.IsNullOrEmpty(p) ? string.Empty : symmetricCipherProvider.EncryptData(p)).ToList();
        }

        public void Decrypt(SCMS.Foundation.Security.SymmetricCipherProvider symmetricCipherProvider)
        {
            if (symmetricCipherProvider == null)
            {
                throw new Exception("The provided symmetric cipher provider is not instantiated.", new ArgumentNullException("symmetricCipherProvider"));
            }

            Person = new Entity.Person(Person, SCMS.Foundation.Security.EncryptionStatus.EncryptionStatusDecrypted, symmetricCipherProvider);
            Country = string.IsNullOrEmpty(Country) ? string.Empty : symmetricCipherProvider.DecryptData(Country);
            Subdivision = string.IsNullOrEmpty(Subdivision) ? string.Empty : symmetricCipherProvider.DecryptData(Subdivision);
            City = string.IsNullOrEmpty(City) ? string.Empty : symmetricCipherProvider.DecryptData(City);
            Address = string.IsNullOrEmpty(Address) ? string.Empty : symmetricCipherProvider.DecryptData(Address);
            ZIP = string.IsNullOrEmpty(ZIP) ? string.Empty : symmetricCipherProvider.DecryptData(ZIP);
            Phones = Phones == null ? null : Phones.Select(p => string.IsNullOrEmpty(p) ? string.Empty : symmetricCipherProvider.DecryptData(p)).ToList();
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

        public void AddTrace(SCMS.Entity.Shared.Contact trace)
        {
            throw new NotImplementedException();
        }
    }
}
