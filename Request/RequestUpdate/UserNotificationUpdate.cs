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
    public class UserNotificationUpdate : DomainUpdate
    {
        public string Type { get; set; }
        public Guid? UserID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string LinkWeb { get; set; }
        public string LinkApp { get; set; }
        public bool? IsRead { get; set; }
        public bool? IsSendMail { get; set; }
    }
}
