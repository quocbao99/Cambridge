using Entities.DomainEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using static Utilities.CatalogueEnums;

namespace Entities.Search
{
    public class PackageSearch : BaseSearch
    {
        public decimal? FromPrice { get; set; }
        public decimal? ToPrice { get; set; }
        public int? MonthExp { get; set; }
        public int? PackageType { get; set; }
    }
}
