using Attributes;
using BaseAPI.Controllers;
using Entities;
using Entities.DomainEntities;
using Extensions;
using Interface.Services;
using Interface.Services.Catalogue;
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

namespace API.Controllers
{
    /// <summary>
    /// Quản lý phân quyền
    /// </summary>
    [Route("api/role")]
    [ApiController]
    [Description("Quản lý phân quyền")]
    [Authorize]
    
    public class RoleController : BaseCatalogueController<Role, RoleModel, RoleCreate, RoleUpdate, BaseSearch>
    {
        private IUserService userService;
        private IMenuService _menuService;
        public RoleController(IServiceProvider serviceProvider, ILogger<BaseController<Role, RoleModel, RoleCreate, RoleUpdate, BaseSearch>> logger
          , IWebHostEnvironment env) : base(serviceProvider, logger, env)
        {
            this.catalogueService = serviceProvider.GetRequiredService<IRoleService>();
            this.userService = serviceProvider.GetRequiredService<IUserService>();
            _menuService = serviceProvider.GetService<IMenuService>();
        }

        /// <summary>
        /// Thêm mới quyền
        /// </summary>
        /// <param name="itemModel"></param>
        /// <returns></returns>
        [HttpPost]
        [AppAuthorize]
        public override async Task<AppDomainResult> AddItem([FromBody] RoleCreate itemModel)
        {
            if (!LoginContext.Instance.CurrentUser.isAdmin)
                throw new UnauthorizedAccessException("Không có quyền thực hiện!");
            if (ModelState.IsValid)
            {
                Role item = mapper.Map<Role>(itemModel);
                item.Code = AppUtilities.RemoveUnicode(item.Name.Replace(" ", ""));
                var validate = await this.catalogueService.GetAsync(x => x.Name.ToUpper() == item.Name.ToUpper() || x.Code == item.Code);
                if (validate.Any())
                    throw new AppException("Đã tồn tại quyền này!");
                foreach (var itemMenu in itemModel.Menus)
                {
                    var existMenu = await _menuService.GetByIdAsync(itemMenu);
                    if (existMenu == null)
                    {
                        throw new AppException("menu không tồn tại");
                    }
                }
                if (item != null)
                {
                    item.Permissions = JsonConvert.SerializeObject(itemModel.Permissions);
                    item.MenuList = JsonConvert.SerializeObject(itemModel.Menus);
                    bool success = await this.catalogueService.CreateAsync(item);
                    if (!success)
                        throw new Exception("Lỗi trong quá trình xử lý");
                    return new AppDomainResult() { ResultCode = (int)HttpStatusCode.OK, ResultMessage = "Thêm mới thành công!", Success = true };
                }
                else
                    throw new AppException("Vui lòng nhập đầy đủ thông tin!");
            }
            throw new AppException(ModelState.GetErrorMessage());
        }

        /// <summary>
        /// Cập nhật quyền
        /// </summary>
        /// <param name="itemModel"></param>
        /// <returns></returns>
        [HttpPut]
        [AppAuthorize]
        public override async Task<AppDomainResult> UpdateItem([FromBody] RoleUpdate itemModel)
        {
            if (!LoginContext.Instance.CurrentUser.isAdmin)
                throw new UnauthorizedAccessException("Không có quyền thực hiện");
            if (ModelState.IsValid)
            {
                Role item = await this.catalogueService.GetByIdAsync(itemModel.Id);
                if (item == null)
                    throw new KeyNotFoundException("Item không tồn tại");
                foreach (var itemMenu in itemModel.Menus)
                {
                    var existMenu = await _menuService.GetByIdAsync(itemMenu);
                    if (existMenu == null)
                    {
                        throw new AppException("menu không tồn tại");
                    }
                }
                //if (item.Code == "Khachhang" && item.Name != item.Name)
                //    throw new Exception("Không thể chỉnh sửa tên phân quyền mặc định trong hệ thống!");

                item.Name = itemModel.Name ?? item.Name;
                item.MenuList = JsonConvert.SerializeObject(itemModel.Menus);
                if (itemModel.Permissions != null && itemModel.Permissions.Any())
                    item.Permissions = Newtonsoft.Json.JsonConvert.SerializeObject(itemModel.Permissions);

                bool success = await this.catalogueService.UpdateAsync(item);
                if (!success)
                    throw new Exception("Lỗi trong quá trình xử lý");
                return new AppDomainResult() { ResultCode = (int)HttpStatusCode.OK, ResultMessage = "Cập nhật thành công!", Success = true };
            }
            throw new AppException(ModelState.GetErrorMessage());
        }

        /// <summary>
        /// Xoá phân quyền
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Description("Xoá phân quyền")]
        [AppAuthorize]
        public override async Task<AppDomainResult> DeleteItem(Guid id)
        {
            if (!LoginContext.Instance.CurrentUser.isAdmin)
                throw new UnauthorizedAccessException("Không có quyền thực hiện");
            AppDomainResult appDomainResult = new AppDomainResult();
            Role item = await this.catalogueService.GetByIdAsync(id);
            if (item == null)
                throw new KeyNotFoundException("Item không tồn tại");
            //if (item.Code == "Khachhang")
            //    throw new Exception("Không thể xóa phân quyền mặc định trong hệ thống!");
            

            Expression<Func<Users, Users>> includeProperties = e => new Users() { Roles = e.Roles };
            IList<Users> users = await this.userService.GetAsync(new Expression<Func<Users, bool>>[] { }, includeProperties);
            users = users.Where(x => !string.IsNullOrEmpty(x.Roles)).ToList();
            if (users.Any(x => x.Roles.Contains(item.Id.ToString())))
                throw new AppException("Không thể xóa phân quyền đã có người dùng!");
            bool success = await this.catalogueService.DeleteAsync(id);
            if (success)
            {
                appDomainResult.ResultCode = (int)HttpStatusCode.OK;
                appDomainResult.Success = success;
            }
            else
                throw new Exception("Lỗi trong quá trình xử lý");
            return appDomainResult;
        }

        /// <summary>
        /// Lấy thông tin theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Description("Xem thông tin chi tiết")]
        [AppAuthorize]
        public override async Task<AppDomainResult> GetById(Guid id)
        {
            Role item = await this.catalogueService.GetByIdAsync(id);
            if (item == null)
                throw new KeyNotFoundException("Item không tồn tại");

            RoleModel itemModel = mapper.Map<RoleModel>(item);
            itemModel = GetPermission(itemModel);
            return new AppDomainResult()
            {
                Success = true,
                Data = itemModel,
                ResultCode = (int)HttpStatusCode.OK
            };
        }

        /// <summary>
        /// Danh sách phân quyền
        /// </summary>
        /// <param name="baseSearch"></param>
        /// <returns></returns>
        [HttpGet]
        [AppAuthorize]
        public override async Task<AppDomainResult> Get([FromQuery] BaseSearch baseSearch)
        {
            if (ModelState.IsValid)
            {
                PagedList<Role> pagedData = await this.catalogueService.GetPagedListData(baseSearch);
                PagedList<RoleModel> pagedDataModel = mapper.Map<PagedList<RoleModel>>(pagedData);
                pagedDataModel = GetPermissions(pagedDataModel);
                return new AppDomainResult
                {
                    Data = pagedDataModel,
                    Success = true,
                    ResultCode = (int)HttpStatusCode.OK
                };
            }
            else
                throw new AppException(ModelState.GetErrorMessage());
        }

        /// <summary>
        /// danh sách chức năng phân quyền trong hệ thống
        /// </summary>
        /// <returns></returns>
        [HttpGet("getlistpermission")]
        public AppDomainResult GetListPermission()
        {
            var temp = LoginContext.Instance.CurrentUser;
            return new AppDomainResult()
            {
                Data = base.ListPermission(),
                Success = true,
                ResultCode = (int)HttpStatusCode.OK
            };
        }

        [NonAction]
        public PagedList<RoleModel> GetPermissions(PagedList<RoleModel> pagedDataModel)
        {
            List<Permission> permissions = ListPermission();
            //kiểm tra phân quyền này đã có thêm chức năng nào chưa, nếu có => so sánh và gán lại Allowed
            foreach (var item in pagedDataModel.Items)
            {
                List<Permission> permissionsJson = JsonConvert.DeserializeObject<List<Permission>>(item.Permissions);
                List<Permission> permission = permissions;
                foreach (var jtem in permission)
                {
                    foreach (var per in jtem.PermissionActions)
                    {
                        if (permissionsJson != null && permissionsJson.Any())
                        {
                            if (permissionsJson.Any(x => x.Controller == jtem.Controller && x.PermissionActions.Any(x => x.Action == per.Action && x.Allowed == true)))
                            {
                                per.Allowed = true;
                            }
                        }
                    }
                }
                item.Permissions = JsonConvert.SerializeObject(permission);
                item.PermissionsJson = permissions;
                item.MenusJson = JsonConvert.DeserializeObject<List<Guid>>(item.MenuList);
            }
            return pagedDataModel;
        }

        [NonAction]
        public RoleModel GetPermission(RoleModel roleModel)
        {
            //kiểm tra phân quyền này đã có thêm chức năng nào chưa, nếu có => so sánh và gán lại Allowed
            List<Permission> permissionsJson = JsonConvert.DeserializeObject<List<Permission>>(roleModel.Permissions);
            List<Permission> permissions = ListPermission();
            if (permissionsJson != null && permissionsJson.Any())
            {
                //lặp qua per hệ thống
                foreach (var item in permissions)
                {
                    //per db = per hệ thống
                    if (permissionsJson.Any(x => x.Controller == item.Controller))
                    {
                        foreach (var jtem in item.PermissionActions)
                        {
                            if (permissionsJson.Any(x => x.Controller == item.Controller && x.PermissionActions.Any(x => x.Action == jtem.Action && x.Allowed == true)))
                                jtem.Allowed = true;
                        }
                    }
                }
            }
            roleModel.Permissions = JsonConvert.SerializeObject(permissions);
            roleModel.PermissionsJson = permissions;
            roleModel.MenusJson = JsonConvert.DeserializeObject<List<Guid>>(roleModel.MenuList);
            return roleModel;
        }
    }
}