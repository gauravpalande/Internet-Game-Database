using InternetGameDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternetGameDatabase.Admin
{
    public partial class AddGameConfirm : System.Web.UI.Page
    {
        private Game newGame;
        private GameContext db;
        protected void Page_Load(object sender, EventArgs e)
        {
            newGame = new Game();
            db = new GameContext();
            newGame.GameName = Request.QueryString["GameName"];
            newGame.GameDescription = Request.QueryString["GameDescription"];
            newGame.ImagePath = Request.QueryString["ImagePath"];
            int genreID = Int32.Parse(Request.QueryString["GenreID"]);
            newGame.Genre = db.Genres.Where(g => g.GenreID == genreID).FirstOrDefault();
            string[] platformValues = Request.QueryString["PlatformValues"].Split(',');
            int[] platformValuesAsInt = Array.ConvertAll(platformValues, Int32.Parse);
            var platforms = db.Platforms.Where(p => platformValuesAsInt.Contains(p.PlatformID));
            newGame.Platforms = platforms.ToList();
            if(Request.QueryString["update"] == "true")
            {
                ConfirmButton.Visible = false;
                UpdateButton.Visible = true;
                newGame.GameID = Int32.Parse(Request.QueryString["GameID"]);
            }
        }

        public Game getNewGame()
        {
            return newGame;
        }

        protected void ConfirmButton_Click(object sender, EventArgs e)
        {
            //move image to permanent directory
            System.IO.File.Copy(Server.MapPath("../Images/Temporary/" + newGame.ImagePath), Server.MapPath("../Images/Games/" + newGame.ImagePath), true);
            System.IO.File.Delete(Server.MapPath("../Images/Temporary/" + newGame.ImagePath));
            //add game to database
            using (db)
            {
                    db.Games.Add(newGame);
                    db.SaveChanges();
            }
            //redirect to games list
            Response.Redirect("../Games.aspx");
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            using (db)
            {
                Game previousVersion = db.Games.Where(g => g.GameID == newGame.GameID).FirstOrDefault();
                previousVersion.GameName = newGame.GameName;
                db.SaveChanges();
            }
            //redirect to games list
            Response.Redirect("../Games.aspx");
        }
    }
}