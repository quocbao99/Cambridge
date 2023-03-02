using Entities.DomainEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities
{
    /// <summary>
    /// Đơn hàng
    /// </summary>
    public class Order : DomainEntities.DomainEntities
    {
        /// <summary>
        /// Mã người dùng
        /// </summary>
        public Guid UserID { get; set; }
        public Guid PackageID { get; set; }

        public string OrderCode { get; set; }
        /// <summary>
        /// thông tin gói khi mua đơn hàng
        /// </summary>
        public string PackageInfo { get; set; }
    }
}
