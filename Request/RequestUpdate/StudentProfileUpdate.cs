using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json;
using Request.DomainRequests;
using Utilities;
using static Utilities.CatalogueEnums;

namespace Request.RequestUpdate
{
    public class StudentProfileUpdate : DomainUpdate
    {
        /// <summary>
        /// ID nhóm hồ sơ
        /// </summary>
        public Guid ProfileGroupID { get; set; }
        /// <summary>
        /// Họ tên thí sinh
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Năm sinh
        /// </summary>
        public double? Dob { get; set; }
        /// <summary>
        /// nơi sinh
        /// </summary>
        public string Pob { get; set; }
        /// <summary>
        /// Giới tính
        /// 0 => Khác
        /// 1 => Nam
        /// 2 => Nữ
        /// </summary>
        public int? Gender { get; set; }

        /// <summary>
        /// Loại giấy tờ dùng để đăng ký thi
        /// </summary>
        public int TypeOfIdentification { get; set; }
        /// <summary>
        /// Tùy vào loại giấy tờ đã chọn ở ô 4. Điền vào số CCCD/CMT/Hộ chiếu/Giấy khai sinh (Citizen identification/ID card/ Passport/Birth Certificate)
        /// </summary>
        public string IdentificationNumber { get; set; }
        /// <summary>
        /// Ngày cấp giấy tờ
        /// </summary>
        public double DateOfIssue { get; set; }
        /// <summary>
        /// Nơi cấp giấy tờ
        /// </summary>
        public string PlaceOfIssue { get; set; }
        /// <summary>
        /// Địa chỉ để liên hệ
        /// </summary>
        public string TypeOfAddress { get; set; }
        /// <summary>
        /// Địa chỉ cụ thể gồm số nhà, đường
        /// </summary>
        public string SpecificAddress { get; set; }
        /// <summary>
        /// Số điện thoại của thí sinh
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Số điện thoại dự phòng
        /// </summary>
        public string PhoneBackupNumber { get; set; }
        /// <summary>
        /// email của thí sinh
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// nghề nghiệp hiện tại của thí sinh
        /// </summary>
        public string Occupation { get; set; }
        /// <summary>
        /// Nơi thí sinh công tác, nếu là học sinh thì điền tên trường đang học
        /// </summary>
        public string PlaceOfWork { get; set; }
        /// <summary>
        /// Ảnh của thí sinh, kích thước thường là 3x4
        /// </summary>
        public string PhotoOfCandidate { get; set; }
        /// <summary>
        /// Có thể là ảnh CCCD,CMT, Họ chiếu
        /// </summary>
        public string PhotoOfIDCardOrPassport { get; set; }
        /// <summary>
        /// Mục đích dự thi của thí sinh, có thể là:
        ///- đánh giá trình độ(qualification assessment)
        ///- Du học(study abroad)
        ///- Xin việc hoặc nâng ngạch(Apply for a job or raise your quota)
        ///- Xét tốt nghiệp(Graduation consideration)
        /// </summary>
        public string PurposeOfTakingExam { get; set; }
        /// <summary>
        /// Cấp độ kiểm tra
        /// 0 => A1
        /// 1 => A2
        /// 2 => B1
        /// 3 => B2
        /// 4 => C1
        /// 5 => C2
        /// </summary>
        public string? ExamLevel { get; set; }
        /// <summary>
        /// Mục tiêu điểm số của thí sinh
        /// </summary>
        public int PointTarget { get; set; }

        /// <summary>
        /// Thời gian tiếp nhận hồ sơ từ điểm thu hồ sơ
        /// </summary>
        public double TimeOfSubmitingCandidateProfile { get; set; }
        /// <summary>
        /// Thí sinh muốn nhận chứng chỉ bản cứng
        /// </summary>
        public bool? NeedsHardCopyCertification { get; set; }
    }
}
