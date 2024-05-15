using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeApi.Entity.Musics;

namespace WeApi.Model
{
    public class AddMusocsAlbum: Sys_MusicsAlbum
    {
        public List<string> SonsList { get; set; }
        public List<MusicList> SonsInfoList { get; set; }
    }

    public class MusicList
    {
        public string Id { get; set; }
        public string music_Name { get; set; }
    }
}
