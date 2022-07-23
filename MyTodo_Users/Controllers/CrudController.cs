using DataTransfer.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTodo_Users.Interfaces;
using static MyAuth_lib.Constants.PolicyConstants;

namespace MyTodo_Users.Controllers
{
    [Authorize]
    [Route("api/users/[controller]")]
    [ApiController]
    public class CrudController : ControllerBase
    {
        private readonly ICrudService crudService;
        private readonly IAuthorizationService authorizationService;

        public CrudController(ICrudService crudService, IAuthorizationService authorizationService)
        {
            this.crudService = crudService;
            this.authorizationService = authorizationService;
        }

        // GET: api/<UsersController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserDto>))]
        public IEnumerable<UserDto> Get()
        {
            return crudService.GetAll();
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(long id)
        {
            var user = crudService.GetById(id);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST api/<UsersController>
        [Authorize(ADMIN_POLICY)]
        [HttpPost]
        public IActionResult Post([FromBody] UserWithIdentityDto userDto)
        {
            crudService.Create(userDto);

            return CreatedAtAction(nameof(Get), new { id = userDto.Id }, userDto);
        }

        // PUT api/<UsersController>/5
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put([FromBody] UserDto userDto)
        {
            var existingUser = crudService.GetById(userDto.Id);

            if (existingUser is null)
            {
                return NotFound();
            }

            var authorizationResult = await authorizationService.AuthorizeAsync(User, existingUser, RESOURCE_BASED_USER_POLICY);

            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            crudService.Update(userDto);
            return NoContent();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(long id)
        {
            var existingUser = crudService.GetById(id);

            if (existingUser is null)
            {
                return NotFound();
            }

            var authorizationResult = await authorizationService.AuthorizeAsync(User, existingUser, RESOURCE_BASED_USER_POLICY);

            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            crudService.Delete(id);
            return NoContent();
        }
    }
}
