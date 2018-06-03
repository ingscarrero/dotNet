using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFTTEK.SCMS.Business.Entity
{
    class Category : SOFTTEK.SCMS.Entity.Shared.Category, SOFTTEK.SCMS.Foundation.Auditory.IAuditable<SOFTTEK.SCMS.Entity.Shared.Category>
    {

        public Category()
        {

        }

        public Category(SOFTTEK.SCMS.Entity.Shared.Category category)
        {
            Identifier = category.Identifier;
            Name = category.Name;
            Description = category.Description;
            Type = category.Type;
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

        public void AddTrace(SCMS.Entity.Shared.Category trace)
        {
            throw new NotImplementedException();
        }
    }
}
