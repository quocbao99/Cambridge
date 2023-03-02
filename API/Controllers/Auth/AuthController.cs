using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.Auth
{
    /// <summary>
    /// Quản lý tài khoản
    /// </summary>
    [Route("api/authenticate")]
    [ApiController]
    [Description("Authenticate")]
    public class AuthController : BaseAPI.Controllers.Auth.AuthController
    {
        public AuthController(IServiceProvider serviceProvider, IConfiguration configuration, IMapper mapper, ILogger<AuthController> logger) : base(serviceProvider, configuration, mapper, logger)
        {
        }
    }
}