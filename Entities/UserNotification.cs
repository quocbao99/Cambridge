using Entities.DomainEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities
{
    public class UserNotification : DomainEntities.DomainEntities
    {
        public Guid? UserID { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Content{ get; set; }
        public string LinkWeb { get; set; }
        public string LinkApp { get; set; }
        public bool? IsSendMail { get; set; }
        public bool? IsRead { get; set; }
    }
}
