
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InternetGameDatabase.Models
{
    public class Game
    {
        public Game()
        {
            this.Platforms = new List<Platform>();
        }
        [ScaffoldColumn(false)]
        public int GameID { get; set; }
        
        [Required, StringLength(100), Display(Name ="Name"), Index(IsUnique = true)]
        public String GameName { get; set; }

        [Required, StringLength(10000), Display(Name ="Description"), DataType(DataType.MultilineText)]
        public String GameDescription { get; set; }

        public String ImagePath { get; set; }

        public byte[] Image { get; set; }

        public double? GameRating { get; set; }

        public double? AverageUserRating { get; set; }

        public int? GenreID { get; set; }

        public virtual Genre Genre { get; set; }

        public virtual ICollection<UserRating> UserRatings { get; set; }

        public virtual ICollection<Platform> Platforms{ get; set; }
    }
}