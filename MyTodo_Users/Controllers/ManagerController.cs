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
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService managerService;

        public ManagerController(IManagerService managerService)
        {
            this.managerService = managerService;
        }

        [Authorize(ADMIN_POLICY)]
        [HttpHead("HasAdminRight")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult HasAdminRight()
        {
            return NoContent();
        }

        [Authorize(ADMIN_POLICY)]
        [HttpPut("UpdateUserAndAddAdminRight")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateUserAndAddAdminRight([FromBody] UserDto user)
        {
            if (!managerService.UpdateUserAndAddAdminRight(user))
            {
                return NotFound();
            }

            return NoContent();
        }

        [Authorize(ADMIN_POLICY)]
        [HttpPut("UpdateUserAndRemoveAdminRight")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateUserAndRemoveAdminRight([FromBody] UserDto user)
        {
            if (!managerService.UpdateUserAndRemoveAdminRight(user))
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
