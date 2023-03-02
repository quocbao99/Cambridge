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
        public const string ROLE_CUSTOMER= "bb478b78-99d7-418c-0295-08dae257a40d";
        public const string ROLECODE_CUSTOMER= "Khachhang";

        public const string ROLE_MANAGER= "2dc979de-08c8-4fb2-20f2-08dae1aa05d9";
        public const string ROLECODE_MANAGER = "Quanly";

        public const string SubjectMailLoginWithOTP = "Auto_ISM OTP for login ";

        public const string MessageBuyPackageForDetail = "Đăng kí gói để xem đầy đủ";

        public enum LimitedDownload
        {
            LimitedDownload = 2, // 2 lượt download 1 ngày
            LimitedDownloadMonth = 30 // 2 lượt download 1 ngày
        }

        public enum NotificationType
        {
            USER = 1
            ,USERs = 2
            ,ROLES = 3
        }

        public enum ExchangeType {
            USD = 1
        }

        public enum SystemFileCategory
        {
            WrittingDiagram = 1,
            Specifications = 2,
            TimingBeltChain = 3,
            TroubleShootingGuide = 4,
            TransmissionManual = 5,
        }

        public enum PackageContractType
        {
            Car = 1,
            Truck = 2
        }

        public enum EModelType
        {
            Car = 1,
            Truck = 2
        }

        public enum AccessLineOff
        {
            All = 0,
            Type = 1,
            IDs = 2 ,
            Time = 3 // Year
        }
        
        public enum PaymentMethodType
        {
            MomoSubscription = 0,
            Momo = 1,
            Paypal = 2,
            PaypalSubscription = 3,
            VNPay = 4,
            Stripe = 5,
            StripeSubscription = 6

        }
        public enum MaterialStatus
        {
            Waiting = 1,
            Success = 2,
            Fail = 3
        }

        public enum PaymentStatus
        {
            Waiting = 1,
            Success = 2,
            Fail = 3
        }

        public enum PaymentStatusForSubScription
        {
            Waiting = 1,
            Success = 2,
            Fail = 3
        }

        public enum PaymentSessionStatus
        {
            Waiting = 1,
            Success = 2,
            Fail = 3
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
            Time = 3, // 10 (Phút) (số nguyên)
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

        public enum SystemFileType
        {
            SystemFile = 1,
            MaterialFile  = 2,
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
