using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json;
using Request.DomainRequests;
using Utilities;
using static Utilities.CatalogueEnums;

namespace Request.RequestUpdate
{
    public class BrandUpdate : DomainUpdate
    {
        public Guid? MarketID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
    }
}
