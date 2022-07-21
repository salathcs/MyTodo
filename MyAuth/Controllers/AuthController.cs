using AutoMapper;
using DataTransfer.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAuth_lib.Auth_Server.Models;
using MyAuth_lib.Exceptions;
using MyAuth_lib.Interfaces;
using MyLogger.Interfaces;
using static MyAuth_lib.Constants.AuthConstants;

namespace MyAuth.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMyLogger myLogger;
        private readonly IAuthService authService;
        private readonly IMapper mapper;

        public AuthController(IMyLogger myLogger,
                              IAuthService authService,
                              IMapper mapper)
        {
            this.myLogger = myLogger;
            this.authService = authService;
            this.mapper = mapper;
        }

        [HttpPost("LogIn")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult LogIn(AuthRequest authRequest)
        {
            try
            {
                myLogger.Debug("LogIn request.");

                var result = authService.Authenticate(authRequest);

                return Ok(result);
            }
            catch (LoginFailedException e)
            {
                myLogger.Info("Login failed!", e);
                return Unauthorized();
            }
        }

        [Authorize(VALIDATION_POLICY)]
        [HttpGet("Validate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClaimDto>))]
        public IActionResult Validate()
        {
            var claimDtos = mapper.Map<IEnumerable<ClaimDto>>(ControllerContext.HttpContext.User.Claims);

            return Ok(claimDtos);
        }
    }
}
