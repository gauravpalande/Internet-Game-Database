using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InternetGameDatabase.Models;
using System.IO;

namespace InternetGameDatabase.Admin
{
    public partial class AdminTools : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack)
            {
                if (gameImageConfirm.HasFile) imagePathLabel.Text = Path.GetFileName(gameImageConfirm.FileName);
            }
            else
            {
                if (Request.QueryString["GameID"] != null)
                {
                    Jumbo.InnerText = "Update Game";
                    AddGame.Visible = false;
                    UpdateGame.Visible = true;
                }
            }
        }

        protected void Page_PreRenderComplete(object sender, EventArgs e)
        {

            if (Request.QueryString["GameID"] != null)
            {//get game data
                int id = Int32.Parse(Request.QueryString["GameID"]);
                Game currentGame = new GameContext().Games.Where(g => g.GameID == id).FirstOrDefault();
                AddGameName.Text = currentGame.GameName;
                DropDownGenre.SelectedIndex = (int)currentGame.GenreID;
                AddGameDescription.Text = currentGame.GameDescription;

                foreach (Platform p in currentGame.Platforms)
                {

                    ListItem currentCheck = PlatformCheck.Items.FindByValue(p.PlatformID.ToString());
                    if (currentCheck != null) currentCheck.Selected = true;
                }
            }
        }



        public IQueryable<Genre> getGenres()
        {
            var db = new GameContext();
            IQueryable<Genre> genreQuery = db.Genres;
            return genreQuery;
        }

        public IQueryable<Platform> getPlatforms()
        {
            var db = new GameContext();
            IQueryable<Platform> platformQuery = db.Platforms;
            return platformQuery;
        }

        protected string BuildQueryString()
        {
            string gameName = AddGameName.Text;
            string gameGenre = DropDownGenre.SelectedItem.Text;
            string genreID = DropDownGenre.SelectedValue;
            string platformsChecked = "";
            string platformValues = "";
            int platformCount = 0;
            string gameDescription = AddGameDescription.Text;
            string imagePath = gameImageConfirm.FileName;

            string queryString = "?";

            queryString += "GameName=" + gameName + "&";
            queryString += "GameGenre=" + gameGenre + "&";
            queryString += "GenreID=" + genreID + "&";
            foreach (ListItem l in PlatformCheck.Items)
            {
                if (l.Selected == true)
                {
                    platformsChecked += l.Text + ", ";
                    platformValues += l.Value + ",";
                    platformCount++;
                }
            }
            if (platformCount > 0)
            {
                platformsChecked = platformsChecked.Substring(0, platformsChecked.Length - 2);
                platformValues = platformValues.Substring(0, platformValues.Length - 1);
            }
            queryString += "PlatformValues=" + platformValues + "&";
            queryString += "GameDescription=" + gameDescription + "&";
            queryString += "ImagePath=" + imagePath;

            return queryString;
        }

        protected void AddGame_Click(object sender, EventArgs e)
        {
            String s = "~/Admin/AddGameConfirm.aspx" + BuildQueryString();
            s = s.Replace("\n\r", "");
            Response.Redirect(s);
        }

        protected void GameIcon_Click(object sender, ImageClickEventArgs e)
        {
            GameDetailLabel.Text = gameImageConfirm.FileName;

            //adding post logic here
            if (gameImageConfirm.PostedFile != null)
            {
                string fileExt =
                   System.IO.Path.GetExtension(gameImageConfirm.FileName);

                if (fileExt == ".jpeg" || fileExt == ".jpg" || fileExt == ".png")
                {

                    string FileNameSave = Path.GetFileName(gameImageConfirm.PostedFile.FileName);

                    //Save files to disk
                    gameImageConfirm.PostedFile.SaveAs(Server.MapPath("~/Images/Temporary/") + gameImageConfirm.FileName);
                    GameIcon.ImageUrl = "~/Images/Temporary/" + FileNameSave;
                }
                else
                {
                    imagePathLabel.Text = "Upload Images Only!!";
                }
            }
            string FileName = Path.GetFileName(gameImageConfirm.PostedFile.FileName);
            GameIcon.ImageUrl = "~/Images/Temporary/" + FileName;
        }

        protected void UpdateGame_Click(object sender, EventArgs e)
        {
            String s = "~/Admin/AddGameConfirm.aspx" + BuildQueryString() + "&GameID=" + Request.QueryString["GameID"] + "&update=true";
            s = s.Replace("\n", "");
            s = s.Replace("\r", "");
            Response.Redirect(s);
        }
    }
}