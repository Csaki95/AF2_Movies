using Project.Model;
using Project.DAO;
using System.Collections.Generic;

namespace Project.Controller
{
    public class FilmController
    {
        private readonly IFilmDAO dao;

        public FilmController(IFilmDAO filmDAO)
        {
            dao = filmDAO;
        }

        public bool AddFilm(Film film)
        {
            return dao.AddFilm(film);
        }

        public bool ModifyFilm(int ID, int Priority)
        {
            return dao.ModifyFilm(ID, Priority);
        }
        public IEnumerable<Film> GetFilms()
        {
            return dao.GetFilms();
        }

        public Film GetFilm(int FilmID)
        {
            return dao.GetFilm(FilmID);
        }

        public bool IsInDatabase(string title)
        {
            return dao.IsInDatabase(title);
        }
    }
}
