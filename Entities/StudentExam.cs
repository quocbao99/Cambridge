using Entities.DomainEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities
{
    public class StudentExam : DomainEntities.DomainEntities
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
