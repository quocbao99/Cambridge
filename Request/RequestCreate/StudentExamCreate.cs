using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Request.DomainRequests;
using Utilities;
using static Utilities.CatalogueEnums;

namespace Request.RequestCreate
{
    public class StudentExamCreate : DomainCreate
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
