using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities
{
    public class Districts : DomainEntities.DomainEntities
    {
        public Guid? cityId { get; set; }
        [NotMapped]
        public string cityName { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string description { get; set; }

    }
}
