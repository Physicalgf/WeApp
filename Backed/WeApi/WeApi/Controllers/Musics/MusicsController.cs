using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using WeApi.Controllers.Login;
using WeApi.Entity.Login;
using WeApi.Entity.Musics;
using WeApi.Help;
using WeApi.Model;
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
            data.Create_Time=DateTime.Now;

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
        [HttpGet]
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
        [HttpGet]
        [ActionName("GetMusicAlbumList")]
        public ActionResult GetMusicAlbumList(string KeyWord)
        {
            var list = SugarHelp.Db.Queryable<Sys_MusicsAlbum>()
                .WhereIF(!string.IsNullOrEmpty(KeyWord),t=>t.Album_Name.Contains(KeyWord))
                .Select(t=>new AddMusocsAlbum()
                { 
                    Album_Name=t.Album_Name,
                    Album_Info=t.Album_Info,
                    Album_Url=t.Album_Url,
                    Create_Time=t.Create_Time,
                    Id=t.Id,
                    Musics_Id=t.Musics_Id
                }).ToList();

            list.ForEach(t=>{
                if (!string.IsNullOrEmpty(t.Musics_Id))
                {
                    List<string> sonlist = new List<string>();
                    if (t.Musics_Id.Contains(","))
                    {
                        sonlist.AddRange(t.Musics_Id.Split(",").ToList());
                    }
                    else
                    {
                        sonlist.Add(t.Musics_Id);
                    }
                    t.SonsList = sonlist;
                }
            });

            return Json(new { code = "200", data = list, messge = "" });
        }
        #endregion

        #region 音乐专辑详情
        [HttpGet]
        [ActionName("GetMusicAlbum")]
        public ActionResult GetMusicAlbum(string Id)
        {
            var entity = SugarHelp.GetEntity<Sys_MusicsAlbum>(t => t.Id == Id);

            AddMusocsAlbum addMusocsAlbum = new AddMusocsAlbum();
            addMusocsAlbum.Album_Name = entity.Album_Name;
            addMusocsAlbum.Album_Info = entity.Album_Info;
            addMusocsAlbum.Album_Url = entity.Album_Url;
            addMusocsAlbum.Create_Time = entity.Create_Time;
            addMusocsAlbum.Id = entity.Id;
            addMusocsAlbum.Musics_Id = entity.Musics_Id;

            var musics = SugarHelp.GetList<Sys_Musics>(t=> addMusocsAlbum.Musics_Id.Contains(t.Id))
                .Select(t=>new MusicList() 
                { 
                    Id=t.Id,
                    music_Name=t.Music_Name
                
                }).ToList();

            addMusocsAlbum.SonsInfoList= musics;

            return Json(new { code = "200", data = addMusocsAlbum, messge = "" });
        }
        #endregion

        #region 添加音乐专辑
        [HttpPost]
        [ActionName("AddMusicAlbum")]
        public ActionResult AddMusicAlbum([FromBody] AddMusocsAlbum data)
        {
            data.Id = Guid.NewGuid().ToString();
            data.Create_Time = DateTime.Now;
            if (data.SonsList!=null)
            {
                if (data.SonsList.Count>0)
                {
                    data.Musics_Id = string.Join(",", data.SonsList);
                }
            }
            
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
        [HttpGet]
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
