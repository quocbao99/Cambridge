﻿using Entities.DomainEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using static Utilities.CatalogueEnums;

namespace Entities.Search
{
    public class BrandSearch : BaseSearch
    {
        public Guid? MarketID { get; set; }
    }
}