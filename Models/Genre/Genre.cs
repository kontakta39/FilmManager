using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmManager.Models.Genre
{
    public class Genre
    {
        public int GenreId { get; set; }

        [Required(ErrorMessage = "Genre name is required")]
        [MinLength(3)]
        [Display(Name = "Genre")]
        public string GenreName { get; set; }
    }
}
