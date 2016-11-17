using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicFall2016.Models
{
    public class Genre
    {

        public int GenreID { get; set; }
        [Required(ErrorMessage = "Genre is required.")]
        [StringLength(20, ErrorMessage = "Can only be 20 Chars")]
        public string Name { get; set; }
    }
}
