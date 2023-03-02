using Entities.DomainEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using static Utilities.CatalogueEnums;

namespace Entities.Search
{
    public class UserNotificationSearch : BaseSearch
    {
        public string Type { get; set; }
        public Guid? UserID { get; set; }
        public bool? IsRead { get; set; }
    }
}
