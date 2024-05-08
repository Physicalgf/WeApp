using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeApi.Entity.Sys;

namespace WeApi.Entity.Login
{
    public class Sys_User : BaseEntity
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
    }
}
