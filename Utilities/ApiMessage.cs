using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class ApiMessage
    {
        public const string Unauthorized = "Chưa đăng nhập";
        public const string WrongUserNameOrPass = "Sai tên tài khoản hoặc mật khẩu";
        public const string Success = "Thành công";
        public const string Fail = "Thất bại";
        public const string Forbidden = "Bạn không có quyền truy cập";
        public const string ExpiredToken = "Phiên đăng nhập hết hạn";
        public const string NotFoundInformation = "Không tìm thấy thông tin";
        public const string WrongPassword = "Sai mật khẩu";
        public const string InvalidFile = "File không đúng định dạng";
        public const string RequiredInformation = "Vui lòng điền đầy đủ thông tin";
       
        // message chung
        public const string HandlingError = "Lỗi trong quá trình xử lí";
        public const string ItemNotFound = "Dữ liệu không tồn tại";
        public const string SuccessfulGet = "Lấy dữ liệu thành công";
        public const string SuccessfulPost = "Thêm dữ liệu thành công";
        public const string SuccessfulPut = "Cập nhật dữ liệu thành công";
        public const string SuccessfulDelete = "Xóa dữ liệu thành công";
        public const string FailedGet = "Lấy dữ liệu thất bại";
        public const string FailedPost = "Thêm dữ liệu thất bại";
        public const string FailedPut = "Cập nhật dữ liệu thất bại";
        public const string FailedDelete = "Xóa dữ liệu thất bại";
        public const string DenyDelete = "Không được phép xóa";
        //file
        public const string SuccessfulUpload = "Tệp tải lên thành công";
    }
}
