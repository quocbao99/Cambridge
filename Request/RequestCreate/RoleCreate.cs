using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Entities.Catalogue;
using Newtonsoft.Json;
using Request.DomainRequests;
using Utilities;

namespace Request.RequestCreate
{
    public class RoleCreate : RequestCatalogueCreateModel
    {
        /// <summary>
        /// danh sách phân quyền
        /// </summary>
        public List<Permission> Permissions { get; set; }
        /// <summary>
        /// danh sách menu
        /// </summary>
        public List<Guid> Menus { get; set; }
    }
}
