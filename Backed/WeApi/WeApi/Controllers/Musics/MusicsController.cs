using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using WeApi.Controllers.Login;
using WeApi.Entity.Login;
using WeApi.Entity.Musics;
using WeApi.Help;
using WeApi.Model.Login;

namespace WeApi.Controllers.Musics
{
    /// <summary>
    /// 音乐
    /// </summary>
    [Route("[controller]/[Action]")]
    [ApiController]
    public class MusicsController : Controller
    {
        private readonly ILogger<LoginController> _logger;

        public MusicsController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }


        #region 后台
        #region 音乐
        #region 音乐列表
        [HttpGet]
        [ActionName("GetMusicList")]
        public ActionResult GetMusicList(string KeyWord)
        {
            var list=SugarHelp.Db.Queryable<Sys_Musics>()
                .WhereIF(!string.IsNullOrEmpty(KeyWord),t=>t.Music_Name.Contains(KeyWord)||t.Music_Person.Contains(KeyWord))
                .ToList();

            return Json(new { code = "200", data = list, messge = "" });
        }
        #endregion

        #region 音乐详情
        [HttpPost]
        [ActionName("GetMusic")]
        public ActionResult GetMusic(string Id)
        {
            var entity = SugarHelp.GetEntity<Sys_Musics>(t=>t.Id==Id);

            return Json(new { code = "200", data = entity, messge = "" });
        }
        #endregion

        #region 添加音乐
        [HttpPost]
        [ActionName("AddMusic")]
        public ActionResult AddMusic([FromBody] Sys_Musics data)
        {
            data.Id = Guid.NewGuid().ToString();

            SugarHelp.Insert<Sys_Musics>(data);

            return Json(new { code = "200", data = "", messge = "新增成功" });
        }
        #endregion

        #region 修改音乐
        [HttpPost]
        [ActionName("EditeMusic")]
        public ActionResult EditeMusic([FromBody] Sys_Musics data)
        {
            SugarHelp.Update<Sys_Musics>(t=>new Sys_Musics() {
                Music_Name=data.Music_Name,
                Music_Person=data.Music_Person,
                Music_Info=data.Music_Info,
                Music_Lyric=data.Music_Lyric,
                Music_Time=data.Music_Time
            
            },t=>t.Id==data.Id);

            return Json(new { code = "200", data = "", messge = "修改成功" });
        }
        #endregion

        #region 删除音乐
        [HttpPost]
        [ActionName("DeleteMusic")]
        public ActionResult DeleteMusic(string Id)
        {
            SugarHelp.Delete<Sys_Musics>(t => t.Id == Id);

            return Json(new { code = "200", data = "", messge = "删除成功" });
        }
        #endregion
        #endregion

        #region 音乐专辑
        #region 音乐专辑列表
        [HttpPost]
        [ActionName("GetMusicAlbumList")]
        public ActionResult GetMusicAlbumList()
        {
            var list = SugarHelp.GetList<Sys_MusicsAlbum>(null);

            return Json(new { code = "200", data = list, messge = "" });
        }
        #endregion

        #region 音乐专辑详情
        [HttpPost]
        [ActionName("GetMusicAlbum")]
        public ActionResult GetMusicAlbum(string Id)
        {
            var entity = SugarHelp.GetEntity<Sys_MusicsAlbum>(t => t.Id == Id);

            return Json(new { code = "200", data = entity, messge = "" });
        }
        #endregion

        #region 添加音乐专辑
        [HttpPost]
        [ActionName("AddMusicAlbum")]
        public ActionResult AddMusicAlbum([FromBody] Sys_MusicsAlbum data)
        {
            data.Id = Guid.NewGuid().ToString();

            SugarHelp.Insert<Sys_MusicsAlbum>(data);

            return Json(new { code = "200", data = "", messge = "新增成功" });
        }
        #endregion

        #region 修改音乐专辑
        [HttpPost]
        [ActionName("EditeMusicAlbum")]
        public ActionResult EditeMusicAlbum([FromBody] Sys_MusicsAlbum data)
        {
            SugarHelp.Update<Sys_MusicsAlbum>(t => new Sys_MusicsAlbum()
            {
                Album_Name = data.Album_Name,
                Album_Info = data.Album_Info,
                Album_Url = data.Album_Url,
                Musics_Id = data.Musics_Id

            }, t => t.Id == data.Id);

            return Json(new { code = "200", data = "", messge = "修改成功" });
        }
        #endregion

        #region 删除音乐专辑
        [HttpPost]
        [ActionName("DeleteMusicAlbum")]
        public ActionResult DeleteMusicAlbum(string Id)
        {
            SugarHelp.Delete<Sys_MusicsAlbum>(t => t.Id == Id);

            return Json(new { code = "200", data = "", messge = "删除成功" });
        }
        #endregion

        #endregion

        #endregion

    }
}
