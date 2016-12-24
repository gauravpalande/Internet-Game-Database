using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace InternetGameDatabase.Models
{
    public class GameContext : DbContext
    {
        public GameContext() : base("InternetGameDatabase")
        {

        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<UserRating> UserRatings{ get; set;}

    }
}