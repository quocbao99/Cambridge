using Entities.DomainEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using static Utilities.CatalogueEnums;

namespace Entities.Search
{
    public class EModelSearch : BaseSearch
    {
        public Guid? BrandID { get; set; }
        public int? EmodelType { get; set; }
    }
}
