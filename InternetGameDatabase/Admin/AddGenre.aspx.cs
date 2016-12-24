using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InternetGameDatabase.Logic;
using InternetGameDatabase.Models;

namespace InternetGameDatabase.Admin
{
    public partial class AddGenre : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Request.QueryString["add"] == "true")
                    SuccessLabel.Visible = true;

                if (Request.QueryString["removeError"] == "true")
                    RemoveWarning.Visible = true;
            }
        }

        public IQueryable<Genre> getGenres()
        {
            var db = new GameContext();
            IQueryable<Genre> genreQuery = db.Genres;
            return genreQuery;
        }

        protected void AddGenreButton_Click(object sender, EventArgs e)
        {
            AddGenreController.AddGenre(AddGenreName.Text, AddGenreDescription.Text);
            Response.Redirect("AddGenre.aspx?add=true");
        }

        protected void ConfirmRemove_Click(object sender, EventArgs e)
        {
            int targetGenreID = Int32.Parse(DropDownGenre.SelectedValue);
            GameContext db = new GameContext();
            using (db)
            {
                Genre targetGenre = db.Genres.Where(g => g.GenreID == targetGenreID).FirstOrDefault();
                db.Genres.Remove(targetGenre);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    Response.Redirect("AddGenre.aspx?removeError=true");
                }

            }
            Response.Redirect("../Games.aspx");
        }
    }
}