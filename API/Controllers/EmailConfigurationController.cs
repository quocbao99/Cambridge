using Entities;
using Entities.Configuration;
using Entities.Search;
using Extensions;
using Interface.Services;
using Interface.Services.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Models;
using Models.Configuration;
using Newtonsoft.Json;
using Request.Configuration;
using Request.RequestCreate;
using Request.RequestUpdate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Utilities;
using static Utilities.CatalogueEnums;
using static Utilities.CoreContants;

namespace BaseAPI.Controllers
{
    /// <summary>
    /// Quản lý cấu hình mail
    /// </summary>
    [Route("api/email-configuration")]
    [ApiController]
    [Description("Quản lý cấu hình mail")]
    [Authorize]
    public class EmailConfigurationController : BaseController<EmailConfigurations, EmailConfigurationModel, EmailConfigurationRequestCreate, EmailConfigurationRequestUpdate, Entities.DomainEntities.BaseSearch>
    {
        private IEmailConfigurationService emailConfigurationService;
        public EmailConfigurationController(IServiceProvider serviceProvider, ILogger<BaseController<EmailConfigurations, EmailConfigurationModel, EmailConfigurationRequestCreate, EmailConfigurationRequestUpdate, Entities.DomainEntities.BaseSearch>> logger
            , IWebHostEnvironment env) : base(serviceProvider, logger, env)
        {
            this.domainService = serviceProvider.GetRequiredService<IEmailConfigurationService>();
        }
    }
}