using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities
{
    public class CoreContants
    {
        //public const string Get = "Get";
        //public const string Post = "Post";
        //public const string Put = "Put";
        //public const string Delete = "Delete";

        public const string ViewAll = "ViewAll";
        public const string Delete = "Delete";
        public const string AddNew = "AddNew";
        public const string View = "View";
        public const string FullControl = "FullControl";
        public const string Update = "Update";
        public const string Approve = "Approve";
        public const string Import = "Import";
        public const string Upload = "Upload";
        public const string Download = "Download";
        public const string DeleteFile = "DeleteFile";
        public const string Export = "Export";

        //public const string FullControl = "FullControl";
        //public const string Approve = "Approve";
        //public const string DeleteFile = "DeleteFile";

        public const string UPLOAD_FOLDER_NAME = "Upload";
        public const string UPLOAD_IMPORTJAR_FOLDER_NAME = "Import";
        public const string UPLOAD_DATAEXTRACT_FOLDER_NAME = "DataExtract";
        public const string UPLOAD_MATERIAL_FOLDER_NAME = "MaterialUpload";
        public const string UPLOAD_MATERIALSUB_FOLDER_NAME = "MaterialSubUpload";
        public const string TEMP_FOLDER_NAME = "Temp";
        public const string TEMPLATE_FOLDER_NAME = "Template";
        public const string CATALOGUE_TEMPLATE_NAME = "CatalogueTemplate.xlsx";
        public const string USER_FOLDER_NAME = "User";
        public const string QR_CODE_FOLDER_NAME = "QRCode";

        public const string GET_TOTAL_NOTIFICATION = "get-total-notification";


        #region signal message
        public const string SIGNAL_FINISHED_BOOKING = "FINISHED_BOOKING";
        #endregion

        public const string HANGFIRE_ID = "11111111-1111-1111-1111-111111111111" ;
        public const string ADMIN_ID = "0debff1d-ac80-4e2d-be24-3151b26f2176";
        public const string ROLE_CUSTOMER= "2d58e454-ee92-4851-3269-08db1ba26ac4";
        public const string ROLECODE_CUSTOMER= "ThiSinh";

        public const string ROLE_MANAGER= "2dc979de-08c8-4fb2-20f2-08dae1aa05d9";
        public const string ROLECODE_MANAGER = "Quanly";

        public const string SubjectMailLoginWithOTP = "CamBridge OTP for login ";

        public const string MessageBuyPackageForDetail = "Đăng kí gói để xem đầy đủ";

        
        public enum NotificationType
        {
            USER = 1
            ,USERs = 2
            ,ROLES = 3
        }

        public enum OTPStatus
        {
            UnExpired = 0,
            Expired = 1
        }
        public enum ContractStatus
        {
            Using = 0,
            Ended = 1,
            Expired = 2,
        }
        public enum TimeExpiredOTPMinute
        {
            Time = 60, // 60 (Phút) (số nguyên)
        }
        public enum TimeTrial
        {
            Time = 1, // 1 (Ngày) (số nguyên)
        }

        public enum HangfireManageType { 
            HangFireOTP = 1, // hangfire otp
            HangFireTrial = 2, // hangfire trial
            HangFireExprideConTract = 3, // hangfire contract
            HangFireMomoPayUsingToken = 4 // hangfire momo
        }

        public enum SocialType {
            FaceBook =1,
            Google =2,
        }

        public const string hangfireRole = "hangfire";


        /// <summary>
        /// Danh mục quyền
        /// </summary>
        //public enum PermissionContants
        //{
        //    Get = 1,
        //    Post = 2,
        //    Put = 3,
        //    Delete = 4
        //}
        //public enum userStatus
        //{
        //    notActivated,
        //    active,
        //    locked,

        //}
        #region Catalogue Name
        /// <summary>
        /// Phường
        /// </summary>
        public const string WARD_CATALOGUE_NAME = "Ward";

        /// <summary>
        /// Quốc gia
        /// </summary>
        public const string COUNTRY_CATALOGUE_NAME = "Country";

        /// <summary>
        /// Quận
        /// </summary>
        public const string DISTRICT_CATALOGUE_NAME = "District";

        /// <summary>
        /// Thành phố
        /// </summary>
        public const string CITY_CATALOGUE_NAME = "City";

        /// <summary>
        /// Dân tộc
        /// </summary>
        public const string NATION_CATALOGUE_NAME = "Nation";

        /// <summary>
        /// Loại thông báo
        /// </summary>
        public const string NOTIFICATION_TYPE_CATALOGUE_NAME = "NotificationType";
        #endregion

        #region SMS Template
        /// <summary>
        /// Xác nhận OTP SMS
        /// </summary>
        public const string SMS_XNOTP = "XNOTP";
        #endregion

        #region Email Template
        #endregion
    }
}
