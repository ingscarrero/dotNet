using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFTTEK.SCMS.Business.Entity
{
    class Parameter<T>:SOFTTEK.SCMS.Entity.Shared.Parameter<T>, SOFTTEK.SCMS.Foundation.Auditory.IAuditable<SOFTTEK.SCMS.Business.Entity.Parameter<T>>
    {

        public Parameter()
        {

        }

        public Parameter(SOFTTEK.SCMS.Entity.Shared.Parameter<T> parameter)
        {
            Identifier = parameter.Identifier;
            ExternalIdentifier = parameter.ExternalIdentifier;
            Value = parameter.Value;
            Description = parameter.Description;
            Category = parameter.Category;
            Order = parameter.Order;
            IsActive = parameter.IsActive;
            Comments = parameter.Comments;
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

        public void AddTrace(Parameter<T> trace)
        {
            throw new NotImplementedException();
        }
    }
}
