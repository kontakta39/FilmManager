using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FilmManager.Models
{
    public class Director
    {
        public int DirectorId { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [MaxLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Film name is required")]
        [Display(Name = "Film")]
        public int FilmId { get; set; }

        [Required(ErrorMessage = "Nationality is required")]
        [MaxLength(50)]
        public string Nationality { get; set; }

        [Required(ErrorMessage = "Birth year is required")]
        [Display(Name = "Birth Year")]
        [Range(1900, 1990, ErrorMessage = "Release year must be between 1900 and 1990")]
        public int BirthYear { get; set; }

        [NotMapped]
        public List<Film> Films { get; set; } = new List<Film>();

    }
}
