
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskManager.Infrastructure.Persistence.Context;
using TaskManager.Common.Exceptions;
using TaskManager.Infrastructure.Persistence.Interfaces.Repository;



namespace TaskManager.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : class
    {

        public ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Entity> CreateAsync(Entity entity)
        {
            await _dbContext.Set<Entity>().AddAsync(entity);
            await Save();

            return entity;
        }

        public virtual async Task<bool> DeleteAsync(Entity entity)
        {
            _dbContext.Set<Entity>().Remove(entity);
            return await Save();
        }

        public virtual async Task<bool> UpdateAsync(Entity entity, int id)
        {
            if (entity != null)
            {
                Entity EntityDB = await _dbContext.Set<Entity>().FindAsync(id);
                if (EntityDB == null)
                {
                    throw new NotFoundException("The entity couldn't be found");
                }
                _dbContext.Entry(EntityDB).CurrentValues.SetValues(entity);
            }

            return await Save();
        }
      
        public virtual async Task<IEnumerable<Entity>> GetAllAsync()
        {
            return await _dbContext.Set<Entity>().ToListAsync();
        }

       
        public async virtual Task<Entity> GetByIdAsync(int id)
        {
            return await _dbContext.Set<Entity>().FindAsync(id);
        }
       
        public async Task<bool> Save()
        {
            return await _dbContext.SaveChangesAsync() >= 0 ? true : false;
        }
    }
}
