using Project.Model;
using System;
using System.Data.SQLite;
using System.Collections.Generic;

namespace Project.DAO
{
    class FilmDBDao : IFilmDAO
    {
        private static readonly string conn_string = @"Data Source=..\..\..\..\DB\films.db;";

        // Uj filmet adunk az adatbázishoz
        public bool AddFilm(Film film)
        {
            using SQLiteConnection connection = new SQLiteConnection(conn_string);
            connection.Open();

            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Films "
                + "(Title, Category, PYear, FLength, Priority) VALUES "
                + "(@title, @category, @pyear, @flength, @priority); ";

            command.Parameters.Add("title", System.Data.DbType.String).Value = film.Title;
            command.Parameters.Add("category", System.Data.DbType.String).Value = film.Category;
            command.Parameters.Add("pyear", System.Data.DbType.Int32).Value = film.Year;
            command.Parameters.Add("flength", System.Data.DbType.Int32).Value = film.Length;
            command.Parameters.Add("priority", System.Data.DbType.Int32).Value = film.Priority;

            bool ret = true;
            try
            {
                command.ExecuteNonQuery();
            }
            catch(Exception)
            {
                ret = false;
            }

            connection.Close();
            return ret;
        }

        // Filmre kattintás esetén szerkesztés ablakhoz ez hívódik meg
        public Film GetFilm(int filmID)
        {
            using SQLiteConnection connection = new SQLiteConnection(conn_string);
            connection.Open();

            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Films "
                + "WHERE ID = @id "
                + "LIMIT 1; ";

            command.Parameters.Add("id", System.Data.DbType.Int32).Value = filmID;

            using var reader = command.ExecuteReader();

            connection.Close();
            // Mivel nem felhasználó adja meg elvileg nem lehetséges, hogy nem létező ID-ra keressünk rá, de biztonság kedvéért
            if (!reader.Read())
            {
                return null;
            }
            else
            {
                return new Film
                {
                    ID = reader.GetInt32(reader.GetOrdinal("ID")),
                    Title = reader.GetString(reader.GetOrdinal("Title")),
                    Category = reader.GetString(reader.GetOrdinal("Category")),
                    Year = reader.GetInt32(reader.GetOrdinal("PYear")),
                    Length = reader.GetInt32(reader.GetOrdinal("FLength")),
                    Priority = reader.GetInt32(reader.GetOrdinal("Priority"))
                };
            }
            
        }

        // Filmek kilistáza
        public IEnumerable<Film> GetFilms()
        {
            List<Film> films = new List<Film>();

            using SQLiteConnection connection = new SQLiteConnection(conn_string);
            connection.Open();

            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Films "
                +"ORDER BY Priority DESC ";

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                films.Add(new Film
                {
                    ID = reader.GetInt32(reader.GetOrdinal("ID")),
                    Title = reader.GetString(reader.GetOrdinal("Title")),
                    Category = reader.GetString(reader.GetOrdinal("Category")),
                    Year = reader.GetInt32(reader.GetOrdinal("PYear")),
                    Length = reader.GetInt32(reader.GetOrdinal("FLength")),
                    Priority = reader.GetInt32(reader.GetOrdinal("Priority"))
                });
            }

            connection.Close();
            return films;
        }

        // Egy film frissítése ahol ID megegyezik, csak prioritást frissítünk
        public bool ModifyFilm(int ID, int priority)
        {
            using SQLiteConnection connection = new SQLiteConnection(conn_string);
            connection.Open();

            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE Films SET "
                + "Priority=@priority "
                + "WHERE ID=@id; ";

            command.Parameters.Add("priority", System.Data.DbType.Int32).Value = priority;
            command.Parameters.Add("id", System.Data.DbType.Int32).Value = ID;

            bool ret = true;
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                ret = false;
            }

            connection.Close();
            return ret;
        }

        // Létezik-e ez a film
        public bool IsInDatabase(string title)
        {
            using SQLiteConnection connection = new SQLiteConnection(conn_string);
            connection.Open();

            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Films "
                +"WHERE Title=@title; ";

            command.Parameters.Add("title", System.Data.DbType.String).Value = title;

            using var reader = command.ExecuteReader();

            bool ret = false;
            if (reader.Read())
            {
                ret = true;
            }

            connection.Close();
            return ret;
        }
    }

}
