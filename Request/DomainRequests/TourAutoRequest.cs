using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Request.DomainRequests
{
    public class TourAutoRequest
    {
        /// <summary>
        /// 1 - Recurring, 2 - Schedule
        /// </summary>
        public int? AutoType { get; set; }
        /// <summary>
        /// định kỳ theo ngày (1)
        /// </summary>
        public int? Recurring { get; set; }
        /// <summary>
        /// ngày bắt đầu recurring (1)
        /// </summary>
        public double? StartDateRecurring { get; set; }
        /// <summary>
        /// chọn ngày (2)
        /// </summary>
        public List<TourSchedule> Schedule { get; set; }
    }
    public class TourSchedule
    {
        public double? DepartDate { get; set; }
        public double? Created { get; set; }
    }

    //public class IntervalType
    //{
    //    /// <summary>
    //    /// khoảng thời gian theo ngày, theo giờ, ...
    //    /// </summary>
    //    public int Type { get; set; }
    //    /// <summary>
    //    /// khoảng thời gian / vd: 3 tiếng 1 lần, 2 ngày 1 lần
    //    /// </summary>
    //    public int Interval { get; set; }
    //}
    //public class TimeType
    //{
    //    /// <summary>
    //    /// ngày trong tuần, tuần trong tháng, tháng trong năm
    //    /// </summary>
    //    public int Type { get; set; }
    //    /// <summary>
    //    /// chỉ định vào lúc nào của type
    //    /// </summary>
    //    public int AtTime { get; set; }
    //}
}
