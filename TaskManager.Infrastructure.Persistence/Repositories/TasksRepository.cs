using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Domain.Entities;
using TaskManager.Infrastructure.Persistence.Context;
using TaskManager.Infrastructure.Persistence.Interfaces.Repository;

namespace TaskManager.Infrastructure.Persistence.Repositories
{
    public class TasksRepository : GenericRepository<Tasks>, ITasksRepository
    {
        public ApplicationDbContext _applicationContext;
        public TasksRepository(ApplicationDbContext applicationContext) : base(applicationContext)
        {
            _applicationContext = applicationContext;
        }
    }
}
