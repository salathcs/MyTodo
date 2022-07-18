﻿using Microsoft.AspNetCore.Mvc;
using MyAuth_lib.Auth_Server.Models;
using MyAuth_lib.Interfaces;
using MyLogger.Interfaces;

namespace MyAuth.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMyLogger myLogger;
        private readonly IAuthService authService;

        public AuthController(IMyLogger myLogger, 
                              IAuthService authService)
        {
            this.myLogger = myLogger;
            this.authService = authService;
        }

        [HttpPost("LogIn")]
        public IActionResult LogIn(AuthRequest authRequest)
        {
            myLogger.Debug("LogIn request.");

            var result = authService.Authenticate(authRequest);

            return Ok(result);
        }
    }
}
