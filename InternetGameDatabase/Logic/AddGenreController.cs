using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InternetGameDatabase.Models;

namespace InternetGameDatabase.Logic
{
    internal static class AddGenreController
    {
        private static Genre genre;
        public static void AddGenre(String genreName, String genreDescription)
        {
            genre = new Genre();
            genre.GenreName = genreName;
            genre.GenreDescription = genreDescription;

            using (GameContext db = new GameContext())
            {
                db.Genres.Add(genre);
                db.SaveChanges();
            }

        }
    }
}