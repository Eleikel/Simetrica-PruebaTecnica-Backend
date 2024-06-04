using AutoMapper;
using FluentAssertions;
using Moq;
using TaskManager.Core.Application.Dtos.Task;
using TaskManager.Core.Application.Interfaces.Service;
using TaskManager.Core.Application.Services;
using TaskManager.Core.Domain.Entities;
using TaskManager.Infrastructure.Persistence.Interfaces.Repository;

namespace TaskManager.Tests.Service
{
    public class TaskServiceTests
    {
        private readonly Mock<ITasksRepository> _mockRepository;
        private readonly ITasksService _taskService;
        private readonly IMapper _mapper;

        public TaskServiceTests()
        {
            _mockRepository = new Mock<ITasksRepository>();

            // Configure Mappings
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.CreateMap<CreateTaskDto, Tasks>().ReverseMap();
                mc.CreateMap<UpdateTaskDto, Tasks>().ReverseMap();
            });

            _mapper = mappingConfig.CreateMapper();
            _taskService = new TaskService(_mockRepository.Object, _mapper);
        }


        [Fact]
        public async Task AddAsync_CallsServiceAddAsync()
        {
            // Arrange
            var entity = new Tasks { Id = 111, Title = "Test Service Method Add Title", Description = "Test Service Method Add Description" };

            // Act
            await _taskService.Create(_mapper.Map<CreateTaskDto>(entity));

            // Assert
            _mockRepository.Verify(repo => repo.CreateAsync(It.Is<Tasks>(t => t.Id == entity.Id && t.Title == entity.Title && t.Description == entity.Description)), Times.Once);
        }


        [Fact]
        public async Task GetByIdAsync_ReturnsTask_WhenTaskExists()
        {
            // Arrange
             
            var task = new Tasks { Id = 20, Title = "Test Service GetById Title", Description = "Test Service GetById Description" };

            _mockRepository.Setup(repo => repo.GetByIdAsync(task.Id)).ReturnsAsync(task);

            // Act
            var result = await _taskService.GetById(task.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(task.Id, result.Id);
            Assert.Equal(task.Title, result.Title);
            result.Should().NotBeNull();
            result.Should().BeOfType<UpdateTaskDto>();
        }


        [Fact]
        public async Task UpdateAsync_CallsUpdateAsyncMethodService()
        {
            // Arrange
            var task = new Tasks { Id = 1, Title = "Test Service Update Title", Description = "Test Service Update Description" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(task.Id)).ReturnsAsync(task);

            var updateTaskDto = new UpdateTaskDto { Id = task.Id, Title = task.Title, Description = task.Description };

            // Act
            await _taskService.Update(task.Id, updateTaskDto);

            // Assert
            //_mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Tasks>(), task.Id), Times.Once);
            _mockRepository.Verify(repo => repo.UpdateAsync(It.Is<Tasks>(t => t.Id == task.Id && t.Title == task.Title && t.Description == task.Description), task.Id), Times.Once);

        }

        [Fact]
        public async Task DeleteAsync_CallsDeleteAsyncMethodService()
        {
            //Arrange
            var task = new Tasks { Id = 1, Title = "Test Service Delete Title", Description = "Test Service Delete Description" };

            _mockRepository.Setup(repo => repo.GetByIdAsync(task.Id)).ReturnsAsync(task);

            // Act
            await _taskService.Delete(task.Id);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(task), Times.Once);

        }

    }
}
