using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Application.Dtos;
using TaskManager.Core.Application.Dtos.Auths;
using TaskManager.Core.Application.Dtos.Users;
using TaskManager.Core.Domain.Entities;

namespace TaskManager.Core.Application.Interfaces.Service
{
    public interface IAuthService
    {
        public Task<TokenDto<UserDto>> ClientLogin(LoginDto login);
        public Task<TokenDto<UserDto>> UserRegister(RegisterDto register);
    }
}
