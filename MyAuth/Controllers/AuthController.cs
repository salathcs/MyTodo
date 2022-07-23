using AutoMapper;
using DataTransfer.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAuth.Interfaces;
using MyAuth.Models;
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
        private readonly ILoginService loginService;
        private readonly IRegistrationService registrationService;
        private readonly IMapper mapper;

        public AuthController(IMyLogger myLogger,
                              IAuthService authService,
                              ILoginService loginService,
                              IRegistrationService registrationService,
                              IMapper mapper)
        {
            this.myLogger = myLogger;
            this.authService = authService;
            this.loginService = loginService;
            this.registrationService = registrationService;
            this.mapper = mapper;
        }

        [HttpPost("LogIn")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExtendedAuthResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult LogIn(AuthRequest authRequest)
        {
            try
            {
                myLogger.Debug("LogIn request.");

                var authResult = authService.Authenticate(authRequest);

                var extendedAuthResult = loginService.CreateExtendedAuthResult(authResult);

                return Ok(extendedAuthResult);
            }
            catch (LoginFailedException e)
            {
                myLogger.Info("Login failed!", e);
                return Unauthorized();
            }
        }

        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult Register(UserWithIdentityDto userDto)
        {
            try
            {
                myLogger.Debug("LogIn request.");

                registrationService.Register(userDto);

                return NoContent();
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
