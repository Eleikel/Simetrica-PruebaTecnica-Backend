using AutoMapper;
using TaskManager.Core.Application.Mappings.Base;
using TaskManager.Core.Domain.Entities;

namespace TaskManager.Core.Application.Dtos.Auths;

public class RegisterDto : IMapFrom<User>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }
    public string Password { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<RegisterDto, User>().ReverseMap();
    }
}