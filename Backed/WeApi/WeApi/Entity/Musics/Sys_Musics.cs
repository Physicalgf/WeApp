using WeApi.Entity.Sys;

namespace WeApi.Entity.Musics
{
    /// <summary>
    /// 音乐
    /// </summary>
    public class Sys_Musics : BaseEntity
    {
        /// <summary>
        /// 音乐名称
        /// </summary>
        public string Music_Name { get; set; }
        /// <summary>
        /// 音乐人
        /// </summary>
        public string Music_Person { get; set; }
        /// <summary>
        /// 音乐url信息
        /// </summary>
        public string Music_Info { get; set; }
        /// <summary>
        /// 音乐时间
        /// </summary>
        public string Music_Time { get; set; }
        /// <summary>
        /// 歌词
        /// </summary>
        public string Music_Lyric { get; set; }
        /// <summary>
        /// 歌曲封面
        /// </summary>
        public string Music_Url { get; set; }
    }
}
