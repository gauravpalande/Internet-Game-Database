using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InternetGameDatabase.Models;

namespace InternetGameDatabase.Admin
{
    public partial class RemoveGameConfirm1 : System.Web.UI.Page
    {
        private int GameID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {


            if (Request.QueryString["GameID"] != null)
            {
                GameID = Int32.Parse(Request.QueryString["GameID"]);
            }
            else
            {
                Response.Redirect("../games.aspx");
            }

        }

        public Game getRemoveGame()
        {
            return new GameContext().Games.Where(g => g.GameID == GameID).FirstOrDefault();
        }

        protected void ConfirmButton_Click(object sender, EventArgs e)
        {

            //todo!! Remove image from storage

            //femove game from database
            GameContext db = new GameContext();

            Game targetGame = db.Games.Where(g => g.GameID == GameID).FirstOrDefault();
            foreach (UserRating userRating in db.UserRatings.Where(g => g.game.GameID == targetGame.GameID))
            {
                db.UserRatings.Remove(userRating);
            }


            db.Games.Remove(targetGame);
            db.SaveChanges();


            //redirect to games list
            Response.Redirect("../Games.aspx");
        }
    }
}