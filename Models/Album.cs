using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_Practice.Models
{
    public class Album
    {
        [Key]
        public int AlbumID { get; set; }

        [Required]
        public string Title { get; set; }

        private int year;
        public int Year
        {
            get { return year; }
            set
            {
                if (year >= 1900 || year == DateTime.Now.Year)
                {
                    year = value;
                }
                else 
                {
                    year = 1900;
                }

            }
        }

        public int MyGenreID { get; set; }
        public Genre MyGenre { get; set; }

        public int MyArtistID { get; set; }
        public Artist MyArtist { get; set; }
    }
}
