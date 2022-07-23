using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static MyAuth_lib.Constants.PolicyConstants;

namespace MyTodo_Users.Controllers
{
    [Authorize]
    [Route("api/users/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        // GET: api/<UsersController>
        [Authorize(ADMIN_POLICY)]
        [HttpGet("HasAdminRight")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult HasAdminRight()
        {
            return NoContent();
        }
    }
}
