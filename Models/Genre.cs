using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_Practice.Models
{
    public class Genre
    {
        [Key]
        public int GenreID { get; set; }
        
        [Required]
        public string Name { get; set; }

        //referece to Albums
        public List<Album> MyAlbums { get; set; }
    }
}
