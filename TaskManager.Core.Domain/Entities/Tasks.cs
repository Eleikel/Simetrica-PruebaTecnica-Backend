
using TaskManager.Core.Domain.Common;

namespace TaskManager.Core.Domain.Entities
{
    public class Tasks : IAuditableBaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Done { get; set; } = 0;
        public int CreatedById { get; set; }
        public DateTime Created { get; set; }
        public int LastModifiedById { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
