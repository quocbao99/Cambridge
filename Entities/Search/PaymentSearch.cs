using Entities.DomainEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using static Utilities.CatalogueEnums;

namespace Entities.Search
{
    public class PaymentSearch : BaseSearch
    {
        public Guid? OrderID { get; set; }
        public Guid? UserID { get; set; }
        public Guid? PaymentMethodConfigurationID { get; set; }

        public int? Status { get; set; }
        public int? StatusForSubScription { get; set; }
        public double? FromDateCreated { get; set; }
        public double? ToDateCreated { get; set; }
        public decimal? FromAmountMoney { get; set; }
        public decimal? ToAmountMoney { get; set; }

    }
}
