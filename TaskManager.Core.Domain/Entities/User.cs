using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Domain.Common;

namespace TaskManager.Core.Domain.Entities
{
    public class User: IAuditableBaseEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? UserName { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string Hash { get; set; }
        public byte[] Salt { get; set; }
        public int CreatedById { get; set; }
        public DateTime Created { get; set; }
        public int LastModifiedById { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
