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

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public AddressData Address { get; set; }

        public List<PostData> Posts { get; set; }
    }
}
