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
    public class CrudController : ControllerBase
    {
        private readonly ICrudService crudService;
        private readonly IAuthorizationService authorizationService;

        public CrudController(ICrudService todosService, IAuthorizationService authorizationService)
        {
            this.crudService = todosService;
            this.authorizationService = authorizationService;
        }

        // GET: api/<TodosController>
        [Authorize(ADMIN_POLICY)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TodoDto>))]
        public IEnumerable<TodoDto> Get()
        {
            return crudService.GetAll();
        }

        // GET api/<TodosController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TodoDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(long id)
        {
            var todo = crudService.GetById(id);

            if (todo is null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        // POST api/<TodosController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Post([FromBody] TodoDto todoDto)
        {
            crudService.Create(todoDto);

            return CreatedAtAction(nameof(Get), new { id = todoDto.Id }, todoDto);
        }

        // PUT api/<TodosController>/5
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put([FromBody] TodoDto todoDto)
        {
            var existingTodo = crudService.GetById(todoDto.Id);

            if (existingTodo is null)
            {
                return NotFound();
            }

            var authorizationResult = await authorizationService.AuthorizeAsync(User, existingTodo, RESOURCE_BASED_TODO_POLICY);

            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            crudService.Update(todoDto);
            return NoContent();
        }

        // DELETE api/<TodosController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(long id)
        {
            var existingTodo = crudService.GetById(id);

            if (existingTodo is null)
            {
                return NotFound();
            }

            var authorizationResult = await authorizationService.AuthorizeAsync(User, existingTodo, RESOURCE_BASED_TODO_POLICY);

            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            crudService.Delete(id);
            return NoContent();
        }
    }
}
