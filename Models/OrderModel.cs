using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class OrderModel: DomainModels.AppDomainModel
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

        public UserModel UserModel{ get; set; }

    }
}
