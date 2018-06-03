using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFTTEK.SCMS.Business.Entity
{
    class Employee : SOFTTEK.SCMS.Entity.Shared.Employee, SOFTTEK.SCMS.Foundation.Security.IEncryptable, SOFTTEK.SCMS.Foundation.Auditory.IAuditable<SOFTTEK.SCMS.Entity.Shared.Employee>
    {
        public Employee()
        {

        }

        public Employee(SOFTTEK.SCMS.Entity.Shared.Employee employee, SOFTTEK.SCMS.Foundation.Security.EncryptionStatus encryptionStatus = SCMS.Foundation.Security.EncryptionStatus.EncryptionStatusNone, SOFTTEK.SCMS.Foundation.Security.SymmetricCipherProvider symmetricCipherProvider = null)
        {
            Identifier = employee.Identifier;
            User = employee.User;
            Contact = employee.Contact;
            Person = employee.Person;
            Role = employee.Role;
            HiredAt = employee.HiredAt;
            Area = employee.Area;
            Supervisor = employee.Supervisor;
            Comments = employee.Comments;
            ImageURL = employee.ImageURL;

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

        public void AddTrace(SCMS.Entity.Shared.Employee trace)
        {
            throw new NotImplementedException();
        }
        #endregion

        public void Encrypt(SCMS.Foundation.Security.SymmetricCipherProvider symmetricCipherProvider)
        {
            if (symmetricCipherProvider == null)
            {
                throw new Exception("The provided symmetric cipher provider is not instantiated.", new ArgumentNullException("symmetricCipherProvider"));
            }

            Supervisor.Person = new Entity.Person(Supervisor.Person, SCMS.Foundation.Security.EncryptionStatus.EncryptionStatusEncrypted, symmetricCipherProvider);
            Person = new Entity.Person(Person, SCMS.Foundation.Security.EncryptionStatus.EncryptionStatusEncrypted, symmetricCipherProvider);
        }

        public void Decrypt(SCMS.Foundation.Security.SymmetricCipherProvider symmetricCipherProvider)
        {
            if (symmetricCipherProvider == null)
            {
                throw new Exception("The provided symmetric cipher provider is not instantiated.", new ArgumentNullException("symmetricCipherProvider"));
            }

            Supervisor.Person = new Entity.Person(Supervisor.Person, SCMS.Foundation.Security.EncryptionStatus.EncryptionStatusDecrypted, symmetricCipherProvider);
            Person = new Entity.Person(Person, SCMS.Foundation.Security.EncryptionStatus.EncryptionStatusDecrypted, symmetricCipherProvider);

        }
    }
}
