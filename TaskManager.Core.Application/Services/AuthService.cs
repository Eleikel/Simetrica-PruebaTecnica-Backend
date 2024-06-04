using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManager.Common;
using TaskManager.Core.Application.Dtos;
using TaskManager.Core.Application.Dtos.Auths;
using TaskManager.Core.Application.Dtos.Users;
using TaskManager.Core.Application.Interfaces.Service;
using TaskManager.Core.Domain.Entities;
using TaskManager.Infrastructure.Persistence.Context;
using TaskManager.Common.Exceptions;
using TaskManager.Common.Extensions;
using System.Net;

namespace TaskManager.Core.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _dbContext;
        public IUserService _userService;

        public AuthService(ApplicationDbContext dbContext, IMapper mapper, IUserService userService, AppSettings appSettings)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userService = userService;
            _appSettings = appSettings;
        }

        public async Task<TokenDto<UserDto>> ClientLogin(LoginDto loginDto)
        {

            var userEntity = await _dbContext.User
                                   .FirstOrDefaultAsync(x => x.Email.Trim() == loginDto.User.Trim() ||
                                                        x.UserName.Trim() == loginDto.User.Trim());

            if (userEntity != null)
            {
                string hash = Algorithm.HashPassword(userEntity.Salt, loginDto.Password);
                if (userEntity.Hash == hash)
                {

                    return await GenerateUserToken(_mapper.Map<UserDto>(await _userService.GetById(userEntity.Id)));
                }
                throw new UnauthorizedException($"Debe especificar una clave válida");

            }

            throw new NotFoundException($"El usuario '{loginDto.User}'");
        }

        public async Task<TokenDto<UserDto>> UserRegister(RegisterDto registerDto)
        {
            var dbObject = _mapper.Map<User>(registerDto);
            List<string> error = CheckIfUser(registerDto);
            if (error.Count > 0)
            {
                string erromsg = string.Empty;
                foreach (var e in error)
                {
                    erromsg = erromsg + e + " ";
                }
                throw new WebException(erromsg);
            }

            dbObject.Salt = Common.Algorithm.GenerateSalt();
            dbObject.Hash = Common.Algorithm.HashPassword(dbObject.Salt, registerDto.Password);
            dbObject.UserName = dbObject.Email.Split("@")[0];

            await _dbContext.User.AddAsync(dbObject);
            await _dbContext.SaveChangesAsync();

            return await GenerateUserToken(_mapper.Map<UserDto>(await _userService.GetById(dbObject.Id)));
        }


        private List<string> CheckIfUser(RegisterDto registerDto)
        {
            List<string> error = new List<string>();
            string username = registerDto.Email.Split("@")[0];

            var userEntity = _dbContext.User.FirstOrDefault(x =>
                x.Email!.Trim() == registerDto.Email.Trim() || x.UserName!.Trim() == username.Trim() );

            if (userEntity != null) throw new UserAlreadyExistsException(username + " - " + registerDto.Email);

            if (!registerDto.Email.ValidateEmail()) throw new EmailIsNotValid(registerDto.Email);


            var existPhoneNumber = _dbContext.User.FirstOrDefault(x => x.Phone == registerDto.Phone);
            if (existPhoneNumber != null) throw new PhoneNumberAlreadyExistsException(registerDto.Phone);

            return error;
        }


        public async Task<TokenDto<T>> GenerateUserToken<T>(T userDto) where T : class
        {
            var claims = new List<Claim>
        {
            new Claim("FullName",  userDto.GetType().GetProperty("FullName")?.GetValue(userDto)?.ToString()),
            new Claim(JwtRegisteredClaimNames.Sid, userDto.GetType().GetProperty("Id")?.GetValue(userDto)?.ToString())

        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.TokenKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = Server.GetDate().AddHours(_appSettings.TokenTime);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _appSettings.Domain,
                audience: _appSettings.Domain,
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            return new TokenDto<T>
            {
                Value = new JwtSecurityTokenHandler().WriteToken(token),
                Expire = expiration.Year + "-" + expiration.Month + "-" + expiration.Day + " " + expiration.ToString("HH") + ":" + expiration.Minute + ":" + expiration.Second,
                Info = userDto
            };
        }
    }
}
