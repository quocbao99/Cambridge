﻿using Entities.DomainEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities
{
    public class Role : AppDomainCatalogue
    {
        /// <summary>
        /// Danh sách chức năng được truy cập
        /// </summary>
        public string Permissions { get; set; }
        /// <summary>
        /// Danh sách menu được truy cập
        /// </summary>
        public string MenuList { get; set; }
    }
}
