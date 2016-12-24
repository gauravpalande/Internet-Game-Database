using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InternetGameDatabase.Models;

namespace InternetGameDatabase.Logic
{
    internal static class AddUserRatingController
    {
        private static UserRating userRating;
        public static void AddUserRating(String userName, String quickDescription, String numericalRating, String detailedDescription, String gameID)
        {
            userRating = new UserRating();
            userRating.UserName = userName;
            userRating.QuickDescription = quickDescription;
            userRating.NumericalRating = Int32.Parse(numericalRating);
            userRating.DetailedDescription = detailedDescription;
            int i = Int32.Parse(gameID);
            GameContext db = new GameContext();
            userRating.game = db.Games.Where(g => g.GameID == i).FirstOrDefault();
            
            db.UserRatings.Add(userRating);
            db.SaveChanges();
            
        }

    }
}