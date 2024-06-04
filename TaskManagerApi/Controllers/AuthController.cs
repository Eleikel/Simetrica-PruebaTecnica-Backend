
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Application.Interfaces.Service;
using TaskManager.Common;
using TaskManager.Core.Application.Dtos.Users;
using TaskManager.Core.Application.Services;
using TaskManager.Core.Domain.Entities;
using TaskManager.Core.Application.Dtos;
using TaskManager.Core.Application.Dtos.Auths;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;


namespace TaskManager.WebApi.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class AuthController : ControllerBase
    {
        private IAuthService _authServices;

        public AuthController(IAuthService authServices)
        {
            _authServices = authServices;
        }


        /// <summary>
        /// Iniciar sesión.
        /// </summary>
        /// <remarks>
        /// Ejemplo de solicitud:
        ///
        ///     POST /api/v1/LogIn
        ///
        /// </remarks>
        /// <returns>Todas las categorías</returns>
        /// <response code="200">Inicio de sesión exitoso</response>
        /// <response code="400">La solicitud es inválida</response>
        [HttpPost("LogIn")]
        public async Task<ActionResult<ApiResponse<TokenDto<UserDto>>>> Post([FromBody] LoginDto loginDto)
        {
            return Ok(new ApiResponse<TokenDto<UserDto>>(await _authServices.ClientLogin(loginDto)));
        }


        /// <summary>
        /// Registrar un nuevo Usuario.
        /// </summary>
        /// <remarks>
        /// Ejemplo de solicitud:
        ///
        ///     POST /api/v1/Register
        ///
        /// </remarks>
        /// <returns>Todas las categorías</returns>
        /// <response code="200">Se ha registrado el usuario exitosamente</response>
        /// <response code="401">No autorizado</response>
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<ApiResponse>> Register([FromBody] RegisterDto registerDto)
        {
            return Ok(new ApiResponse<TokenDto<UserDto>>(await _authServices.UserRegister(registerDto)));

        }

    }
}
