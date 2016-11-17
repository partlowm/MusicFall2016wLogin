﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicFall2016.Models
{
    public class PlayListAlbums
    {
        public int PlaylistID { get; set; }
        public Playlist Playlist { get; set; }

        public int AlbumID { get; set; }
        public Album Album { get; set; }
    }
}
