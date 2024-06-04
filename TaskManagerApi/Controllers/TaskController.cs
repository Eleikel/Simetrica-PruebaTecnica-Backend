
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Application.Interfaces.Service;
using TaskManager.Common;
using TaskManager.Core.Application.Dtos.Task;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Swashbuckle.AspNetCore.Annotations;

namespace TaskManager.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class TaskController : ControllerBase
    {
        private readonly ITasksService _tasksService;

        public TaskController(ITasksService tasksService)
        {
            _tasksService = tasksService;
        }

        /// <summary>
        /// Obtiene todas las tareas registradas.
        /// </summary>
        /// <remarks>
        /// Ejemplo de solicitud:
        ///
        ///     GET /api/v1/Task
        ///
        /// </remarks>
        /// <returns>Todas las categorías</returns>
        /// <response code="200">Devuelve la lista de las tareas</response>
        [HttpGet]
        public async Task<ActionResult<ApiResponse>> Get()
        {
            return Ok(new ApiResponse<IEnumerable<UpdateTaskDto>>(await _tasksService.Get()));
        }


        /// <summary>
        /// Obtiene una Tarea por su ID.
        /// </summary>
        /// <param name="id">ID de la Tarea.</param>
        /// <remarks>
        /// Ejemplo de solicitud:
        ///
        ///     GET /api/v1/Task/1
        ///
        /// </remarks>
        /// <returns>La Tarea correspondiente al ID proporcionado.</returns>
        /// <response code="200">Devuelve la Tarea encontrada</response>
        /// <response code="404">Si no se encuentra la tarea solicitada</response>        
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<ApiResponse>> GetById(int id)
        {
            return Ok(new ApiResponse<UpdateTaskDto>(await _tasksService.GetById(id)));
        }

        /// <summary>
        /// Crea una nueva Tarea.
        /// </summary>
        /// <param name="task">Datos de la nueva Tarea a Crear.</param>
        /// <remarks>
        /// Ejemplo de solicitud:
        ///
        ///     POST /api/v1/Task
        ///     {
        ///        "name": "Nueva Tarea",
        ///        "title": "Titulo de la tarea",
        ///        "description": "Descripción de la tarea"
        ///     }
        ///
        /// </remarks>
        /// <returns>La Tarea creada.</returns>
        /// <response code="200">Devuelve la Tarea creada</response>
        /// <response code="400">Si los datos de la Tarea son inválidos</response>   
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> Create([FromBody] CreateTaskDto task)
        {
            return Ok(new ApiResponse<CreateTaskDto>(await _tasksService.Create(task)));
        }


        /// <summary>
        /// Actualiza una Tarea existente.
        /// </summary>
        /// <param name="id">ID de la Tarea a actualizar.</param>
        /// <param name="task">Datos actualizados de la Tarea.</param>
        /// <remarks>
        /// Ejemplo de solicitud:
        ///
        ///     PUT /api/v1/Task/1
        ///     {
        ///        "name": "Tarea Actualizada",
        ///        "title": "Titulo de la tarea a actualizar",
        ///        "description": "Descripción de la tarea a actualizar"
        ///     }
        ///
        /// </remarks>    
        /// <returns>La Tarea actualizada.</returns>
        /// <response code="200">Devuelve la Tarea actualizada</response>
        /// <response code="400">Si los datos de la tarea son inválidos</response>        
        /// <response code="404">Si no se encuentra la tarea que deseas Actualizar</response>  
        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<ApiResponse>> Update(int id, [FromBody] UpdateTaskDto task)
        {
            return Ok(new ApiResponse<UpdateTaskDto>(await _tasksService.Update(id, task)));
        }

        /// <summary>
        /// Elimina una Tarea existente.
        /// </summary>
        /// <param name="id">ID de la Tarea a eliminar.</param>
        /// <remarks>
        /// Ejemplo de solicitud:
        ///
        ///     DELETE /api/v1/Task/1
        ///
        /// </remarks>               
        /// <returns>Respuesta de confirmación.</returns>
        /// <response code="404">Si no se encuentra la tarea que deseas Eliminar</response>  
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<ApiResponse>> Delete(int id)
        {
            return Ok(new ApiResponse<bool>(await _tasksService.Delete(id)));
        }

    }
}
