using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Application.Dtos.Users;
using TaskManager.Core.Application.Interfaces.Common;

namespace TaskManager.Core.Application.Interfaces.Service
{
    public interface IUserService : IRead<UserDto, int>
    {
        public Task<UserDto> Update(int id, UserDto entity);
        public Task<bool> Delete(int id);
    }
}
