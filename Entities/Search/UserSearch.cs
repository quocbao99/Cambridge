using Entities.DomainEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using static Utilities.CatalogueEnums;

namespace Entities.Search
{
    public class UserSearch : BaseSearch
    {
        /// <summary>
        /// Lọc theo chức vụ ID
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// cờ  active
        /// </summary>
        public bool? Active { get; set; }
        /// <summary>
        /// lọc theo chức cụ code
        /// </summary>
        public string RoleCode { get; set; }
    }
}
