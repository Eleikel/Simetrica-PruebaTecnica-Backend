using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Application.Dtos.Task;
using TaskManager.Core.Application.Interfaces.Common;

namespace TaskManager.Core.Application.Interfaces.Service
{
    public interface ITasksService  : IRead<UpdateTaskDto, int>
    {
        public Task<CreateTaskDto> Create(CreateTaskDto entity);
        public Task<UpdateTaskDto> Update(int id, UpdateTaskDto entity);
        public Task<bool> Delete(int id);
    }
}
