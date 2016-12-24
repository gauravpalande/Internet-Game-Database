using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InternetGameDatabase.Models;
using InternetGameDatabase.Logic;
using System.Web.ModelBinding;

namespace InternetGameDatabase.User
{
    public partial class UserReview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            getRatings();
        }
        public void getRatings()
        {
            foreach(int i in Enumerable.Range(0, 101))
                DropDownRating.Items.Add(new ListItem(i.ToString(),i.ToString()));
        }

        public IQueryable<Game> getGame([QueryString("GameID")] int? gameId)
        {
            var db = new GameContext();
            IQueryable<Game> query = db.Games;
            if (gameId.HasValue && gameId > 0)
            {
                query = query.Where(g => g.GameID == gameId);
            }
            else
            {
                query = null;
            }
            return query;
        }

        protected void SubmitReview_Click(object sender, EventArgs e)
        {
            string userName = HttpContext.Current.User.Identity.Name;
            string quickDescription = QuickText.Text;
            string numericalRating = DropDownRating.SelectedValue;
            string detailedDescription = DetailedText.Text;
            string gameID = Request.QueryString["GameID"];
            AddUserRatingController.AddUserRating(userName, quickDescription, numericalRating, detailedDescription, gameID);
            GameContext db = new GameContext();
            int integerGameID = Int32.Parse(gameID);
            Game targetGame = db.Games.Where(g => g.GameID == integerGameID).FirstOrDefault();
            var userRatings = db.UserRatings.Where(u => u.game.GameID == integerGameID);
            int? sumScore = 0;
            foreach (UserRating userRating in userRatings) sumScore += userRating.NumericalRating;
            targetGame.AverageUserRating = sumScore/userRatings.Count(); 
            db.SaveChanges();
            Response.Redirect("../GameDetails.aspx?id=" + gameID);
        }
    }
}