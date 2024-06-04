using TaskManager.Core.Domain.Entities;

namespace TaskManager.Infrastructure.Persistence.Interfaces.Repository
{
    public interface ITasksRepository : IGenericRepository<Tasks>
    {
    }
}
