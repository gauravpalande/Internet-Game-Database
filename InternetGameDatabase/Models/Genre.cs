using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetGameDatabase.Models
{
    public class Genre
    {
        [ScaffoldColumn(false)]
        public int GenreID { get; set; }

        [Required, StringLength(100), Display(Name ="Genre Name"), Index(IsUnique = true)]
        public string GenreName { get; set; }

        [Display(Name ="Genre Description")]
        public String GenreDescription { get; set; }
        

    }
}