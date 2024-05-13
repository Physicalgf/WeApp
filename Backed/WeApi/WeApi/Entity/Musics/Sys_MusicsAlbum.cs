using WeApi.Entity.Sys;

namespace WeApi.Entity.Musics
{
    /// <summary>
    /// 音乐专辑
    /// </summary>
    public class Sys_MusicsAlbum:BaseEntity
    {
        /// <summary>
        /// 专辑名称
        /// </summary>
        public string Album_Name { get; set; }
        /// <summary>
        /// 专辑信息
        /// </summary>
        public string Album_Info { get; set; }
        /// <summary>
        /// 专辑封面地址
        /// </summary>
        public string Album_Url { get; set; }
        /// <summary>
        /// 歌曲Id 多个，逗号隔开
        /// </summary>
        public string Musics_Id { get; set; }
    }
}
