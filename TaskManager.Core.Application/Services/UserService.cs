using AutoMapper;
using TaskManager.Common.Exceptions;
using TaskManager.Core.Application.Dtos.Users;
using TaskManager.Core.Application.Interfaces.Service;
using TaskManager.Infrastructure.Persistence.Context;
using TaskManager.Infrastructure.Persistence.Interfaces.Repository;

namespace TaskManager.Core.Application.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private ApplicationDbContext _dbContext;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> Get()
        {
            var userEntity = await _userRepository.GetAllAsync();
            var userDto = _mapper.Map<IEnumerable<UserDto>>(userEntity);
            return userDto;
        }

        public async Task<UserDto> GetById(int id)
        {
            var userEntity = await _userRepository.GetByIdAsync(id) ?? throw new NotFoundException("El usuario");
            var userDto = _mapper.Map<UserDto>(userEntity);
            return userDto;
        }

        public async Task<UserDto> Update(int id, UserDto entity)
        {
            var userEntity = await _userRepository.GetByIdAsync(id) ?? throw new NotFoundException("El usuario");
            var userDto = _mapper.Map<UserDto>(userEntity);
            return userDto;
        }

        public async Task<bool> Delete(int id)
        {
            var userEntity = await _userRepository.GetByIdAsync(id) ?? throw new NotFoundException("El usuario");
            var userToDelete = await _userRepository.DeleteAsync(userEntity);
            return userToDelete;
        }
    }

}
