using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_Practice.Models
{
    public class Song
    {
        [Key]
        public int SongID { get; set; }
        [Required]
        public string Title { get; set; }

        //reference to Album
        public int MyAlbumID { get; set; }
        public Album MyAlbum { get; set; }
        public string SongUrl { get; set; }
    }
}
