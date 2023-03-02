using Entities.DomainEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using static Utilities.CatalogueEnums;

namespace Entities.Search
{
    public class LineOffSearch : BaseSearch
    {
        public Guid? EModelID { get; set; }

        public int? FromYear { get; set; }
        public int? ToYear { get; set; }
        public int? LineOffType { get; set; }
    }
}
