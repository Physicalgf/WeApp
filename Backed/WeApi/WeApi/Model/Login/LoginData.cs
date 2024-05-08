using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeApi.Model.Login
{
    public class LoginData
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
        /// <summary>
        /// 旧密码
        /// </summary>
        public string OldPassWord { get; set; }
    }
}
