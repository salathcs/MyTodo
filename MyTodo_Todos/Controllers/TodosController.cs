using DataTransfer.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTodo_Todos.Interfaces;

namespace MyTodo_Todos.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodosService todosService;

        public TodosController(ITodosService todosService)
        {
            this.todosService = todosService;
        }

        // GET: api/<TodosController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TodoDto>))]
        public IEnumerable<TodoDto> Get()
        {
            return todosService.GetAll();
        }

        // GET api/<TodosController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TodoDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(long id)
        {
            var user = todosService.GetById(id);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST api/<TodosController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TodoDto))]
        public IActionResult Post([FromBody] TodoDto todoDto)
        {
            todosService.Create(todoDto);

            return CreatedAtAction(nameof(Post), new { id = todoDto.Id }, todoDto);
        }

        // PUT api/<TodosController>/5
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put([FromBody] TodoDto todoDto)
        {
            if (!todosService.Update(todoDto))
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE api/<TodosController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(long id)
        {
            if (!todosService.Delete(id))
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
