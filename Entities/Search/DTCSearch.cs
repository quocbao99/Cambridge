using Entities.DomainEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using static Utilities.CatalogueEnums;

namespace Entities.Search
{
    public class DTCSearch : BaseSearch
    {
        public Guid? EModelID { get; set; }
        public Guid? BrandID { get; set; }
        public string DTCCode { get; set; }
    }
}
