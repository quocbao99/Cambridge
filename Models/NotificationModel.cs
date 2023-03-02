using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class NotificationModel : DomainModels.AppDomainModel
    {
        public string Type { get; set; }
        public string Roles { get; set; }
        public string UserIDs { get; set; }
        public Guid? UserID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string LinkWeb { get; set; }
        public string LinkApp { get; set; }
        public bool? IsSendMail { get; set; }
        public bool? IsRead { get; set; }
    }
}
