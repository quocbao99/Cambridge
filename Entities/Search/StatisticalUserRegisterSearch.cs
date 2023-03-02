using Entities.DomainEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using static Utilities.CatalogueEnums;

namespace Entities.Search
{
    public class StatisticalUserRegisterSearch 
    {
       
        public double? FromDate { get; set; }
        public double? ToDate { get; set; }

    }
    public class StatisticalRevenueSearch
    {

        public double? FromDate { get; set; }
        public double? ToDate { get; set; }

    }

    public class StatisticalMonthSearch
    {

        public int? Month { get; set; }
        public int? Year{ get; set; }

    }

    public class StatisticalYearRevenueSearch
    {
        public int? Year{ get; set; }

    }
    public class StatisticalYearUserRegisterSearch
    {
        public int? Year { get; set; }

    }
}
