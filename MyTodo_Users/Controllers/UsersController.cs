using DataTransfer.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTodo_Users.Interfaces;

namespace MyTodo_Users.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        // GET: api/<UsersController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TodoDto>))]
        public IEnumerable<UserDto> Get()
        {
            return usersService.GetAll();
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TodoDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(long id)
        {
            var user = usersService.GetById(id);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST api/<UsersController>
        [HttpPost]
        public IActionResult Post([FromBody] UserDto userDto)
        {
            usersService.Create(userDto);

            return CreatedAtAction(nameof(Post), new { id = userDto.Id }, userDto);
        }

        // PUT api/<UsersController>/5
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put([FromBody] UserDto userDto)
        {
            if (!usersService.Update(userDto))
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(long id)
        {
            if (!usersService.Delete(id))
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
