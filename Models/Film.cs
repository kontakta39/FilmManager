using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace FilmManager.Models
{
    public class Film
    {

        public int FilmId { get; set; }

        [Required(ErrorMessage = "Title name is required")]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Genre name is required")]
        [Display(Name = "Genre")]
        //[MaxLength(50)]
        //public string Genre { get; set; }
        public int GenreId { get; set; }

        [Required(ErrorMessage = "Director name is required")]
        [MaxLength(50)]
        public string Director { get; set; }

        [Required(ErrorMessage = "Music author is required")]
        [Display(Name = "Music Author")]
        [MaxLength(50)]
        public string Music { get; set; }

        [Required(ErrorMessage = "Release year is required")]
        [Display(Name = "Release Year")]
        [Range(1900, 2021, ErrorMessage = "Release year must be between 1900 and 2021")]
        public int ReleaseYear { get; set; }

        [NotMapped]
        //public virtual Genre genre { get; set; }
        public List<Genre> Genres { get; set; } = new List<Genre>();

    }
}
