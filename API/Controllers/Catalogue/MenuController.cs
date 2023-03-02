using BaseAPI.Controllers;
using Entities.Catalogue;
using Entities.DomainEntities;
using Interface.Services.Catalogue;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Models.Catalogue;
using Request.Catalogue.CatalogueCreate;
using Request.Catalogue.CatalogueUpdate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Utilities;

namespace API.Controllers.Catalogue
{
    /// <summary>
    /// Quản lý Menu
    /// </summary>
    [Route("api/menu")]
    [ApiController]
    [Description("Quản lý Menu")]
    public class MenuController : BaseCatalogueController<Menu, MenuModel, RequestMenuCreateModel, RequestMenuUpdateModel, BaseSearch>
    {
        public MenuController(IServiceProvider serviceProvider, ILogger<BaseController<Menu, MenuModel, RequestMenuCreateModel, RequestMenuUpdateModel, BaseSearch>> logger
            , IWebHostEnvironment env) : base(serviceProvider, logger, env)
        {
            this.catalogueService = serviceProvider.GetRequiredService<IMenuService>();
        }
        /// <summary>
        /// thêm mới item
        /// </summary>
        /// <param name="itemModel"></param>
        /// <returns></returns>
        [HttpPost]
        public override Task<AppDomainResult> AddItem([FromBody] RequestMenuCreateModel itemModel)
        {
            itemModel.Code = AppUtilities.RemoveUnicode(itemModel.Name.Replace(" ", ""));
            return base.AddItem(itemModel);
        }
        /// <summary>
        /// cập nhật item
        /// </summary>
        /// <param name="itemModel"></param>
        /// <returns></returns>
        [HttpPut]
        public override Task<AppDomainResult> UpdateItem([FromBody] RequestMenuUpdateModel itemModel)
        {
            itemModel.Code = AppUtilities.RemoveUnicode(itemModel.Name.Replace(" ", ""));
            return base.UpdateItem(itemModel);
        }
    }
}
