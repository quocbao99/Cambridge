using Models.DomainModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using static Utilities.CatalogueEnums;

namespace Models
{

    public class UserModel : AppDomainModel
    {
        /// <summary>
        /// Mã người dùng
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// UserName
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Họ
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Tên
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Họ và tên
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public double? Birthday { get; set; }

        /// <summary>
        /// Chứng minh nhân dân
        /// </summary>
        public string IdentityCard { get; set; }

        /// <summary>
        /// Mật khẩu người dùng
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Giới tính
        /// 0 => Khác
        /// 1 => Nam
        /// 2 => Nữ
        /// </summary>
        public int Gender { get; set; }
        public string Roles { get; set; }
        public ObjectJsonCustom UserRole { get; set; }
        public string Thumbnail { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Note { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsTrial { get; set; }

        // thông tin hợp đồng
        public bool CarContract { get; set; }
        public int? CarMonthExp { get; set; }
        public bool TruckContract { get; set; }
        public int? TructMonthExp { get; set; }


        public Guid? ContractCarID { get; set; }
        public Guid? ContractTructID { get; set; }
        public double DateDownLoad { get; set; }
        public int CoutDownLoad { get; set; }
        public bool OpenCar { get; set; }
        public bool OpenTruct { get; set; }
        public string OneSignalID { get; set; }
    }

}
