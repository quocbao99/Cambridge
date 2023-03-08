using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ExaminationRoomModel : DomainModels.AppDomainModel
    {
        /// <summary>
        /// Mã phòng thi để admin quản lý
        /// </summary>
        public string ExamRoomCode { get; set; }
        /// <summary>
        /// ID nhóm Hồ sơ tham gia phòng thi
        /// </summary>
        public Guid? ProfileGroupID { get; set; }
        /// <summary>
        /// ID Đề Thi
        /// </summary>
        public Guid? ExamContentID { get; set; }
        /// <summary>
        /// ID giám thị
        /// </summary>
        public Guid? SupervisorID { get; set; }

        /// <summary>
        /// Ngày thi
        /// </summary>
        public double Date { get; set; }
        /// <summary>
        /// Giờ bắt đầu phòng thi, Zoom có hiệu lực
        /// </summary>
        public double Start { get; set; }
        /// <summary>
        /// Giờ phòng thi hết hiệu lực
        /// </summary>
        public double Finish { get; set; }
        /// <summary>
        /// Link phòng thi zoom
        /// </summary>
        public string LinkZoom { get; set; }
    }
}
