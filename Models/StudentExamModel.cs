using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class StudentExamModel : DomainModels.AppDomainModel
    {
        /// <summary>
        /// Xác minh bởi giám thị
        /// </summary>
        public bool? isVertified { get; set; }
        /// <summary>
        /// Đang làm bài thi?
        /// </summary>
        public bool? isOnline { get; set; }

    }
}
