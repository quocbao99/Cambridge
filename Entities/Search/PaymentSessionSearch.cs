using Entities.DomainEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using static Utilities.CatalogueEnums;

namespace Entities.Search
{
    public class PaymentSessionSearch : BaseSearch
    {
        public Guid? PaymentID { get; set; }

        public Guid? Status { get; set; }

        public double? FromDateCreated { get; set; }
        public double? ToDateCreated { get; set; }
        public decimal? FromAmount { get; set; }
        public decimal? ToAmount { get; set; }
    }
}
