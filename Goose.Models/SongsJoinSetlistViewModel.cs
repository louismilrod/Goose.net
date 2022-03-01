using Goose.Data;
using Goose.Models.Song_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using static Goose.Data.Data.Setlist;

namespace Goose.Models
{
    public class SongsJoinSetlistViewModel
    {
        [Required]
        public int SongsJoinSetlistId { get; set; }
        [Required]
        public int SongId { get; set; }
        [Required]
        public int SetlistId { get; set; }
        [Required]
        [Range(1, 25)]
        public int PositionInSet { get; set; }        
        public SelectList SelectListSong { get; set; }
        public SelectList SelectPositionInSet { get; set; }
        public string Title { get; set; }
        public DateTime DateOfPerformance { get; set; }

        public SetType SetType { get; set; }
    }
}
