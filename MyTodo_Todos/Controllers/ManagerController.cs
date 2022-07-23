using DataTransfer.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTodo_Todos.Interfaces;
using static MyAuth_lib.Constants.PolicyConstants;

namespace MyTodo_Todos.Controllers
{
    [Authorize]
    [Route("api/todos/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService managerService;
        private readonly IAuthorizationService authorizationService;

        public ManagerController(IManagerService managerService, IAuthorizationService authorizationService)
        {
            this.managerService = managerService;
            this.authorizationService = authorizationService;
        }

        // GET api/<TodosController>/5
        [HttpGet("GetByUserId/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TodoDto>))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetByUserId(long userId)
        {
            var todos = managerService.GetByUserId(userId);

            if (!todos.Any())
            {
                return Ok(todos);
            }

            var authorizationResult = await authorizationService.AuthorizeAsync(User, todos.First(), RESOURCE_BASED_TODO_POLICY);

            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            return Ok(todos);
        }
    }
}
