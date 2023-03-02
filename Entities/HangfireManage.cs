using Entities.DomainEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities
{
    public class HangfireManage : DomainEntities.DomainEntities
    {
        public string JobID { get; set; }
        public int HangfireManageType { get; set; }
        public Guid? OTPHistoryID { get; set; }
        public Guid? UserID { get; set; }
        public Guid? ContractID { get; set; }
        public Guid? PaymentID { get; set; }
    }
}
