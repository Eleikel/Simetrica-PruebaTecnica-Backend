using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.Domain.Common
{
    public interface IAuditableBaseEntity
    {
       public int Id { get; set; }
        public int CreatedById { get; set; }
        public DateTime Created { get; set; }
        public int LastModifiedById { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
