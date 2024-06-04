using System.Linq.Expressions;


namespace TaskManager.Infrastructure.Persistence.Interfaces.Repository
{
    public interface IGenericRepository<Entity> where Entity : class
    {
        Task<Entity> CreateAsync(Entity entity);
        Task<bool> UpdateAsync(Entity entity, int id);
        Task<bool> DeleteAsync(Entity entity);
        Task<IEnumerable<Entity>> GetAllAsync();
        Task<Entity> GetByIdAsync(int id);
        Task<bool> Save();
    }
}
