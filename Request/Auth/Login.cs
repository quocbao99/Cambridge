using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Request.Auth
{
    public class Login
    {
        /// <summary>
        /// Tên đăng nhập
        /// </summary>
        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc nhập")]
        public string Username { set; get; }
        /// <summary>
        /// Mật khẩu
        /// </summary>

        [Required(ErrorMessage = "Mật khẩu là bắt buộc nhập")]
        public string Password { set; get; }
        /// <summary>
        /// Ghi nhớ mật khẩu
        /// </summary>
        public bool? RememberPassword { get; set; }

        /// <summary>
        /// Mã OTP
        /// </summary>
        public string OTPValue { get; set; }
    }
    public class ResendOTP {
        /// <summary>
        /// Email
        /// </summary>
        [StringLength(50, ErrorMessage = "Số kí tự của email phải nhỏ hơn 50!")]
        [Required(ErrorMessage = "Vui lòng nhập Email!")]
        [EmailAddress(ErrorMessage = "Email có định dạng không hợp lệ!")]
        public string? Email { get; set; }
    }
}
