﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeApi.Entity.Login;
using WeApi.Help;
using WeApi.Model.Login;

namespace WeApi.Controllers.Login
{
    /// <summary>
    /// 登陆
    /// </summary>
    [Route("[controller]/[Action]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }


        #region 后台登录
        #region 登录效验
        [HttpPost]
        [ActionName("GetData")]
        public ActionResult GetData([FromBody] LoginData data)
        {
            var count = SugarHelp.GetList<Sys_User>(t => t.Name == data.UserName && t.PassWord == data.PassWord);
            if (count.Count == 0)
            {
                return Json(new { code = "0", data = "", messge = "登录失败，请检查账号密码" });
            }

            return Json(new { code = "200", data = "", messge = "登录成功" });
        }
        #endregion

        #region 创建账号
        [HttpPost]
        [ActionName("CreateAcc")]
        public ActionResult CreateAcc([FromBody] LoginData data)
        {
            //判断用户名是否存在
            var sysuserentity = SugarHelp.GetEntity<Sys_User>(t => t.Name == data.UserName);
            if (sysuserentity==null)
            {
                Sys_User sys_User = new Sys_User();
                sys_User.Id = Guid.NewGuid().ToString();
                sys_User.Name = data.UserName;//账号
                sys_User.PassWord = data.PassWord;//密码

                SugarHelp.Insert(sys_User);

                return Json(new { code = "200", data = "", messge = "创建成功，请登录" });
            }
            else
            {
                return Json(new { code = "0", data = "", messge = "创建失败，账号已存在" });
            }
        }
        #endregion

        #region 修改密码
        [HttpPost]
        [ActionName("UpdataAcc")]
        public ActionResult UpdataAcc([FromBody] LoginData data)
        {
            var counts = SugarHelp.GetEntity<Sys_User>(t => t.Name == data.UserName && t.PassWord == data.PassWord);
            if (counts == null)
            {
                return Json(new { code = "1", data = "账号或密码错误", messge = "" });
            }

            SugarHelp.Update<Sys_User>(t => new Sys_User() { PassWord = data.OldPassWord }, t => t.Id == counts.Id);

            return Json(new { code = "200", data = "", messge = "修改密码成功" });
        }
        #endregion

        #region 忘记密码
        [HttpPost]
        [ActionName("ForgetPass")]
        public ActionResult ForgetPass([FromBody] LoginData data)
        {
            var counts = SugarHelp.GetEntity<Sys_User>(t => t.Name == data.UserName);
            if (counts == null)
            {
                return Json(new { code = "1", data = "", messge = "该账号不存在" });
            }

            return Json(new { code = "200", data ="密码是: "+ counts.PassWord, messge = "" });
        }
        #endregion
        #endregion

        #region 后台管理
        #region 登录效验
        [HttpGet]
        [ActionName("GetUserList")]
        public ActionResult GetUserList(string KeyWord)
        {
            var list = SugarHelp.Db.Queryable<Sys_User>()
                .WhereIF(!string.IsNullOrEmpty(KeyWord),t=>t.Name.Contains(KeyWord))
                .Select(t=> new { 
                   t.Id,
                   User_Name = t.Name,
                   Create_Time=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                }).ToList();

            return Json(new { code = "200", data = list, messge = "" });
        }
        #endregion
        #endregion
    }
}
