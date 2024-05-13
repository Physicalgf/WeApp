using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WeApi.Entity.Sys
{
    public class BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
        ///// <summary>
        ///// 是否删除
        ///// </summary>
        //public bool Is_Deleted { get; set; }
        ///// <summary>
        ///// 创建时间
        ///// </summary>
        //public DateTime Create_Time { get; set; }
    }
}
