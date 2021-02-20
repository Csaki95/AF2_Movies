using Project.Model;
using System.Collections.Generic;

namespace Project.DAO
{
    public interface IFilmDAO
    {
        bool AddFilm(Film film);
        bool ModifyFilm(int ID, int priority);
        IEnumerable<Film> GetFilms();
        Film GetFilm(int filmID);
        bool IsInDatabase(string title);
    }
}
