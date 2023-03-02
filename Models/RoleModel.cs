using Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Utilities;

namespace Models
{
    public class RoleModel : AppDomainCatalogueModel
    {
        /// <summary>
        /// Danh sách chức năng được truy cập
        /// </summary>
        public List<Permission> PermissionsJson { get; set; }
        [JsonIgnore]
        public string Permissions { get; set; }
        /// <summary>
        /// Danh sách menu được truy cập
        /// </summary>
        public List<Guid> MenusJson { get; set; }
        [JsonIgnore]                               
        public string MenuList { get; set; }
    }
}
