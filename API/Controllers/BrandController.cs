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
    /// Quản lý Brand
    /// </summary>
    [Route("api/brand")]
    [ApiController]
    [Description("Quản lý thương hiệu")]
    [Authorize]
    public class BrandController : BaseController<Brand, BrandModel, BrandCreate, BrandUpdate, BrandSearch>
    {
        public BrandController(IServiceProvider serviceProvider, ILogger<BaseController<Brand, BrandModel, BrandCreate, BrandUpdate, BrandSearch>> logger
            , IWebHostEnvironment env) : base(serviceProvider, logger, env)
        {
            this.domainService = serviceProvider.GetRequiredService<IBrandService>();
        }
    }
}