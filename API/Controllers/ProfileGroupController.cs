using Entities;
using Entities.Search;
using Extensions;
using Interface.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Models;
using Newtonsoft.Json;
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

namespace BaseAPI.Controllers
{

    /// <summary>
    /// Quản lý nhóm hồ sơ
    /// </summary>
    [Route("api/profile-group")]
    [ApiController]
    [Description("Quản lý nhóm hồ sơ")]
    [Authorize]
    public class ProfileGroupController : BaseController<ProfileGroup, ProfileGroupModel, ProfileGroupCreate, ProfileGroupUpdate, ProfileGroupSearch>
    {
        public ProfileGroupController(IServiceProvider serviceProvider, ILogger<BaseController<ProfileGroup, ProfileGroupModel, ProfileGroupCreate, ProfileGroupUpdate, ProfileGroupSearch>> logger
            , IWebHostEnvironment env) : base(serviceProvider, logger, env)
        {
            this.domainService = serviceProvider.GetRequiredService<IProfileGroupService>();
        }
    }
}