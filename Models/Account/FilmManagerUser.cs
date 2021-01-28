using Microsoft.AspNetCore.Identity;

namespace FilmManager.Models.Account
{
    public class FilmManagerUser : IdentityUser
    {
        public string FavouriteFilm { get; set; }
    }
}
