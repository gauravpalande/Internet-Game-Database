using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetGameDatabase.Models
{
    public class Platform
    {
        public Platform()
        {
            this.Games = new List<Game>();
        }

        [ScaffoldColumn(false)]
        public int PlatformID { get; set; }

        [Required, StringLength(1000), Display(Name = "Genre Name")]
        public string PlatformName { get; set; }

        [Display(Name = "Genre Description")]
        public String PlatformDescription { get; set; }

        public virtual ICollection<Game> Games { get; set; }

    }
}