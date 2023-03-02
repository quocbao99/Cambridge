using Interface.Services;
using Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static Utilities.CatalogueEnums;

namespace Extensions
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AppAuthorize : AuthorizeAttribute, IAuthorizationFilter
    {
        public AppAuthorize()
        {
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (UserLoginModel)context.HttpContext.Items["User"];//.User;
            string controllerName = string.Empty;
            string actionName = string.Empty;
            if (context.ActionDescriptor is ControllerActionDescriptor descriptor)
            {
                controllerName = descriptor.ControllerName;
                actionName = descriptor.ActionName;
            }

            if (user == null)
            {
                context.Result = new JsonResult(new AppDomainResult()
                {
                    ResultCode = (int)HttpStatusCode.Unauthorized,
                    ResultMessage = "Unauthorized"
                });
                return;
            }
            

            IUserService userService = (IUserService)context.HttpContext.RequestServices.GetService(typeof(IUserService));
            IRoleService roleService = (IRoleService)context.HttpContext.RequestServices.GetService(typeof(IRoleService));
            bool hasPermit = false;
            if (user.isAdmin)
            {
                hasPermit = true;
            }
            else
            {
                // lấy permission từ token
                //var userCheckResult = userService.HasPermission(LoginContext.Instance.CurrentUser.permission, controllerName, actionName);
                
                // lấy permission từ service
                var currentUser = userService.GetById(LoginContext.Instance.CurrentUser.userId);
                var roleIds = currentUser.Roles;
                string[] roles = roleIds.Split(',');
                if (roles.Length > 0) {
                    for (int i = 0; i < roles.Length; i++) { 
                        var roleOfUser = roleService.GetById(new Guid(roleIds));
                        var userCheckResult = userService.HasPermission(roleOfUser.Permissions, controllerName, actionName);
                        hasPermit = userCheckResult.Result;
                        if (hasPermit == true) break;
                    }
                }

            }

            if (!hasPermit)
            {
                context.Result = new JsonResult(new AppDomainResult()
                {
                    ResultCode = (int)HttpStatusCode.Unauthorized,
                    ResultMessage = "Unauthorized"
                });
                throw new UnauthorizedAccessException();
            }

        }
    }
}
