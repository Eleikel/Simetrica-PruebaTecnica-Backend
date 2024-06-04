using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TaskManager.Core.Domain.Entities;
using TaskManager.Infrastructure.Persistence.Context;
using TaskManager.Infrastructure.Persistence.Repositories;

namespace TaskManager.Tests.Repository
{
    public class TaskRepositoryTests
    {

        private DbContextOptions<ApplicationDbContext> _dbContextOptions;

        public TaskRepositoryTests()
        {

            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection");


            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseOracle(connectionString)
               .Options;


            using (var databaseContext = new ApplicationDbContext(_dbContextOptions))
            {
                databaseContext.Database.OpenConnection();
                databaseContext.Database.EnsureCreated();
            }
        }


        [Fact]
        public async Task UpdateAsync_Should_Update_Task()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var repository = new TasksRepository(context);
                var entity = new Tasks { Id = 255, Title = "Update", Description = "Testing Update", Done = 0 };
                await repository.CreateAsync(entity);

                // Act
                var taskToUpdate = new Tasks
                {
                    Id = entity.Id,
                    Title = "Title should be modified",
                    Description = "Description should be modified",
                    Done = 1
                };

                await repository.UpdateAsync(taskToUpdate, entity.Id);

                // Assert
                var updatedEntity = await context.Task.FindAsync(entity.Id);
                Assert.NotNull(updatedEntity);
                Assert.Equal(taskToUpdate.Title, updatedEntity.Title);
                Assert.Equal(taskToUpdate.Description, updatedEntity.Description);
                Assert.Equal(taskToUpdate.Done.ToString(), updatedEntity.Done.ToString());
                entity.Should().NotBeNull();
                entity.Should().BeOfType<Tasks>();
            }
        }



        [Fact]
        public async Task AddTaskAsync_Should_Add_Task()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var repository = new TasksRepository(context);
                var newTask = new Tasks { Id = 301, Title = "Interfaz global", Description = "Construir la Interfaz Global", Done = 0 };

                // Act
                await repository.CreateAsync(newTask);

                // Assert
                var addedEntity = await context.Task.FindAsync(newTask.Id);
                Assert.NotNull(addedEntity);
                Assert.Equal("Interfaz global", addedEntity.Title);
                Assert.Equal("Construir la Interfaz Global", addedEntity.Description);
                Assert.Equal("0", newTask.Done.ToString());
                newTask.Should().NotBeNull();
                newTask.Should().BeOfType<Tasks>();
            }
        }


        [Fact]
        public async Task GetByIdAsync_Should_Return_Task()
        {
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                // Arrange
                Tasks newTask = new Tasks { Id = 200, Title = "Funcionalidad Pago", Description = "Implementar La Funcionalidad Pago", Done = 0 };

                context.Task.Add(newTask);
                await context.SaveChangesAsync();

                var repository = new TasksRepository(context);

                // Act
                var getTaskById = await repository.GetByIdAsync(newTask.Id);

                // Assert
                Assert.NotNull(getTaskById);
                Assert.Equal("Funcionalidad Pago", getTaskById.Title);
                Assert.Equal("Implementar La Funcionalidad Pago", getTaskById.Description);
                Assert.Equal("0", getTaskById.Done.ToString());
                getTaskById.Should().NotBeNull();
                getTaskById.Should().BeOfType<Tasks>();
            }
        }



        [Fact]
        public async Task DeleteAsync_Should_Remove_Task()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var repository = new TasksRepository(context);
                var taskToCreate = new Tasks { Id = 250, Title = "Delete Function", Description = "Make this delete function to work", Done = 0 };

                var TaskCreated = await repository.CreateAsync(taskToCreate);

                // Act
                await repository.DeleteAsync(TaskCreated);

                // Assert
                var deletedTask = await context.Task.FindAsync(1);
                Assert.Null(deletedTask);
            }
        }
    }
}
