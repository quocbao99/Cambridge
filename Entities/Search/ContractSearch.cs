using Entities.DomainEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using static Utilities.CatalogueEnums;

namespace Entities.Search
{
    public class ContractSearch : BaseSearch
    {
        public Guid? UserID { get; set; }
        public Guid? PaymenntID { get; set; }
        public int? ContractType { get; set; }
        public int? Status { get; set; }

        public double? FromDateStartTime { get; set; }
        public double? ToDateStartTime { get; set; }
        public double? FromDateEndTime { get; set; }
        public double? ToDateEndTime { get; set; }

    }
}
