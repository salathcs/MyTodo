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
        public IEnumerable<UserDto> Get()
        {
            return usersService.GetAll();
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public UserDto Get(long id)
        {
            return usersService.GetById(id);
        }

        // POST api/<UsersController>
        [HttpPost]
        public void Post([FromBody] UserDto userDto)
        {
            usersService.Create(userDto);
        }

        // PUT api/<UsersController>/5
        [HttpPut]
        public void Put([FromBody] UserDto userDto)
        {
            usersService.Update(userDto);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            usersService.Delete(id);
        }
    }
}
