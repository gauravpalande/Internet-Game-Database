using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InternetGameDatabase.Models;

namespace InternetGameDatabase.Logic
{
    
    internal static class AddGameController
    {
        private static Game game;

        public static void AddGame(string gameName, string gameDescription, string imagePath, int genre)
        {
            
            game = new Game();
            game.GameName = null;
            game.GameDescription = null;
            game.ImagePath = null;
            game.GenreID = null;
            //foreach for platforms
            ICollection<Platform> Platforms = new List<Platform>();
            game.Platforms.Add(null);
            
            using (GameContext db = new GameContext())
            {
                db.Games.Add(game);
                db.SaveChanges();
            }
        }
    }
    /*
        REFERENCE FOR FIELDS
        [Required, StringLength(100), Display(Name ="Name")]
        public String GameName { get; set; }

        [Required, StringLength(1000), Display(Name ="Description"), DataType(DataType.MultilineText)]
        public String GameDescription { get; set; }

        public String ImagePath { get; set; }

        [Display(Name ="Average Rating")]
        public double? GameRating { get; set; }

        public int? GenreID { get; set; }

        public virtual Genre Genre { get; set; }
        
        public virtual ICollection<Platform> Platforms{ get; set; }
        */
}