using AutoMapper;
using TaskManager.Core.Application.Mappings.Base;
using TaskManager.Core.Domain.Entities;

namespace TaskManager.Core.Application.Dtos.Users
{
    public class UserDto : IMapFrom<User>
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? UserName { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string FullName => $@"{FirstName} {LastName}";

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
