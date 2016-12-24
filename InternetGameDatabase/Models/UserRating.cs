using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetGameDatabase.Models
{
    public class UserRating
    {
        [ScaffoldColumn(false)]
        public int UserRatingID { get; set; }

        [StringLength(100), Display(Name = "User Name")]
        public String UserName { get; set; }

        [StringLength(10000), Display(Name = "Quick Description"), DataType(DataType.MultilineText)]
        public String QuickDescription { get; set; }

        [StringLength(10000), Display(Name = "Detailed Description"), DataType(DataType.MultilineText)]
        public String DetailedDescription { get; set; }

        public int? NumericalRating { get; set; }

        public virtual Game game { get; set; }
        

    }
}