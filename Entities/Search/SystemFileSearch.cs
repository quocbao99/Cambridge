using Entities.DomainEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using static Utilities.CatalogueEnums;

namespace Entities.Search
{
    public class SystemFileSearch : BaseSearch
    {
        public Guid? BrandID { get; set; }
        public Guid? EModelID { get; set; }
        public Guid? LineOffID { get; set; }
        public Guid? ParentID { get; set; }
        public string SystemFileCategory { get; set; }

    }
}
