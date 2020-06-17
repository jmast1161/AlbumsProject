using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Albums.Models
{
    public class AlbumEntries
    {
        public PagedList<AlbumEntryData> AlbumEntires { get; set; }
    }
}
