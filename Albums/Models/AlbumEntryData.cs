using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Albums.Models
{
    public class AlbumEntryData
    {
        public List<AlbumThumbnailData> AlbumThumbnails { get; set; }
        
        public string AlbumTitle { get; set; }

        public UserData UserInfo { get; set; }

        public List<PostData> Posts { get; set; }
    }
}
