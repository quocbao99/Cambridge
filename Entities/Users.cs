using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace Entities
{
    public class Users : DomainEntities.DomainEntities
    {
        /// <summary>
        /// UserID trên mạng xã hội
        /// </summary>
        public string SocialId { get; set; }

        /// <summary>
        /// loại mạng xã hội
        /// </summary>
        public int SocialType { get; set; }

        /// <summary>
        /// Mã người dùng
        /// </summary>
        [Description("Mã người dùng")]
        public string Code { get; set; }
        /// <summary>
        /// UserName
        /// </summary>
        [Required]
        [StringLength(50)]
        [Description("Tên đăng nhập")]
        public string Username { get; set; }

        /// <summary>
        /// Họ
        /// </summary>
        [Description("Tên")]
        [StringLength(200, ErrorMessage = "Tên phải nhỏ hơn 200 kí tự")]
        public string FirstName { get; set; }

        /// <summary>
        /// Tên
        /// </summary>
        [Description("Họ")]
        [StringLength(300, ErrorMessage = "Tên phải nhỏ hơn 300 kí tự")]
        public string LastName { get; set; }
        /// <summary>
        /// Họ và tên
        /// </summary>
        [Description("Tên đầy đủ")]
        [StringLength(500)]
        public string FullName { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        [Description("Số điện thoại")]
        [StringLength(20)]
        public string Phone { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Description("Email")]
        [StringLength(50)]
        public string Email { get; set; }

        public string EmailTmp { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        [Description("Địa chỉ")]
        [StringLength(1000)]
        public string Address { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        [Description("Trạng thái")]
        public int? Status { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        [Description("Ngày sinh")]
        public double? Birthday { get; set; }
        /// <summary>
        /// Chứng minh nhân dân
        /// </summary>
        [Description("Chứng minh nhân dân")]
        [StringLength(50)]
        public string IdentityCard { get; set; }

        /// <summary>
        /// Ngày cấp chứng minh nhân dân
        /// </summary>
        [Description("Ngày cấp chứng minh nhân dân")]
        public double? IdentityCardDate { get; set; }

        /// <summary>
        /// Nơi cấp chứng minh nhân dân
        /// </summary>
        [Description("Nơi cấp chứng minh nhân dân")]
        [StringLength(1000, ErrorMessage = "Tên phải nhỏ hơn 300 kí tự")]
        public string IdentityCardAddress { get; set; }

        /// <summary>
        /// Mật khẩu người dùng
        /// </summary>
        [StringLength(4000)]
        public string Password { get; set; }
        /// <summary>
        /// Giới tính
        /// 0 => Khác
        /// 1 => Nam
        /// 2 => Nữ
        /// </summary>
        [Description("Giới tính")]
        public int? Gender { get; set; }
        /// <summary>
        /// Cờ cho biết đây là admin
        /// </summary>
        public bool? IsAdmin { get; set; }
        /// <summary>
        /// Danh sách chức vụ của người dùng
        /// </summary>
        [Description("Danh sách chức vụ của người dùng")]
        public string Roles { get; set; }
        public string Thumbnail { get; set; }
        //public Guid? DistrictId { get; set; }
        //public Guid? CityId { get; set; }
        //public Guid? WardId { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        [Description("Ghi chú")]
        public string Note { get; set; }
        public int? TimeZone { get; set; }

        public string RoleCode { get; set; }

        /// <summary>
        /// key để phân loại tài khoản cần gửi mà OTP để đăng nhập 
        /// </summary>
        public bool? IsSendOTP { get; set; }
        
        /// <summary>
        /// Khóa tài khoản
        /// </summary>
        public bool? IsLock { get; set; }
        /// <summary>
        /// Xác minh tài khoản
        /// </summary>
        public bool? IsVerification { get; set; }

        /// <summary>
        /// Dùng thử
        /// </summary>
        public bool? IsTrial { get; set; }

        public double DateDownLoad { get; set; }
        public int CoutDownLoad { get; set; }
        public int CoutDownLoadMonth { get; set; }
        public bool OpenCar { get; set; }
        public bool OpenTruct { get; set; }
        public string OneSignalID { get; set; }

    }
}
