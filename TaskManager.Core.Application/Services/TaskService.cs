using AutoMapper;
using TaskManager.Core.Application.Interfaces.Service;
using TaskManager.Infrastructure.Persistence.Interfaces.Repository;
using TaskManager.Common.Exceptions;
using TaskManager.Core.Application.Dtos.Task;
using TaskManager.Core.Domain.Entities;


namespace TaskManager.Core.Application.Services
{
    public class TaskService : ITasksService
    {

        private readonly ITasksRepository _taskRepository;
        private readonly IMapper _mapper;


        public TaskService(ITasksRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<CreateTaskDto> Create(CreateTaskDto entity)
        {
            Tasks TaskEntity = _mapper.Map<Tasks>(entity);
            TaskEntity = await _taskRepository.CreateAsync(TaskEntity);
            return _mapper.Map<CreateTaskDto>(TaskEntity);
        }

        public async Task<bool> Delete(int id)
        {
            var taskEntity = await _taskRepository.GetByIdAsync(id) ?? throw new NotFoundException("Esta tarea");
            var taskEntityToDelete = await _taskRepository.DeleteAsync(taskEntity);
            return taskEntityToDelete;
        }

        public async Task<IEnumerable<UpdateTaskDto>> Get()
        {
            var taskEntities = await _taskRepository.GetAllAsync();
            var taskEntitiesDto = _mapper.Map<IEnumerable<UpdateTaskDto>>(taskEntities);
            if (!taskEntitiesDto.Any())
                throw new NotFoundException("Las tareas");

            return taskEntitiesDto;
        }

        public async Task<UpdateTaskDto> GetById(int id)
        {
            var taskEntity = await _taskRepository.GetByIdAsync(id) ?? throw new NotFoundException("Esta tarea");
            return _mapper.Map<UpdateTaskDto>(taskEntity);
        }

        public async Task<UpdateTaskDto> Update(int id, UpdateTaskDto entity)
        {
            var taskEntity = await _taskRepository.GetByIdAsync(id) ?? throw new NotFoundException("Esta tarea");
            await _taskRepository.UpdateAsync(_mapper.Map<Tasks>(entity), id);
            return await GetById(id);
        }
    }
}
