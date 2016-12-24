using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InternetGameDatabase.Models;
using LinqKit;
using System.Threading.Tasks;

namespace InternetGameDatabase
{
    public partial class Games : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //RegisterAsyncTask(new PageAsyncTask(presetFields));
        }

        protected async Task presetFields()
        {

                var db = new GameContext();

            // dirty way to preset fields
            await Task.Delay(1);

                // need to prefill checkboxes and dropdown / search with request information here
                if (Request.QueryString["GameName"] != null)
                {
                    //IQueryable<Game> selected
                    FilterGameName.Text = Request.QueryString["GameName"];
                }

                if (Request.QueryString["GenreID"] != null)
                {
                    int genreID = Int32.Parse(Request.QueryString["GenreID"]);

                    IQueryable<Genre> selectedGenre = db.Genres.Where(g => g.GenreID == genreID);
                    var list = selectedGenre.Select(g => new { ID = g.GenreID, Type = "Genre" }).ToList();
                    DropDownGenre.Items.FindByValue(list.ElementAt(0).ID.ToString()).Selected = true;
                }

                if (Request.QueryString["PlatformIDs"] != null)
                {
                    List<int> platIDs = Request.QueryString["PlatformIDs"].Split(',').Select(int.Parse).ToList();

                    IQueryable<Platform> selectedPlatforms = db.Platforms.Where(p => platIDs.Contains(p.PlatformID));
                    var list = selectedPlatforms.Select(p => new { ID = p.PlatformID, Type = "Platform" }).ToList();

                    for (int i = 0; i < selectedPlatforms.Count(); i++)
                        chklstStates.Items.FindByValue(list.ElementAt(i).ID.ToString()).Selected = true;
                }
        }

        protected List<object> GetParameters()
        {
            List<object> filterObjs = new List<object>();
            var db = new GameContext();


            if (Request.QueryString["GameName"] != null)
            {
                //IQueryable<Game> selected
                filterObjs.Add(Request.QueryString["GameName"]);
            }

            if (Request.QueryString["GenreID"] != null)
            {
                int genreID = Int32.Parse(Request.QueryString["GenreID"]);
                
                IQueryable<Genre> selectedGenre = db.Genres.Where(g => g.GenreID == genreID);
                var list = selectedGenre.Select(g => new { ID = g.GenreID, Type = "Genre" }).ToList();
                filterObjs.Add(list);
            }

            if (Request.QueryString["PlatformIDs"] != null)
            {
                List<int> platIDs = Request.QueryString["PlatformIDs"].Split(',').Select(int.Parse).ToList();

                IQueryable<Platform> selectedPlatforms = db.Platforms.Where(p => platIDs.Contains(p.PlatformID));
                var list = selectedPlatforms.Select(p => new { ID = p.PlatformID, Type = "Platform" }).ToList();

                filterObjs.Add(list);
            }
            return filterObjs;
        }

        public IQueryable<Game> GetGamesWithParams(List<object> filterObjs)
        {
            var _db = new InternetGameDatabase.Models.GameContext();
            Expression<Func<Game, bool>> whereClause = g => g.GameID >= 0;
            

            foreach(object filter in filterObjs)
            {
                if (filter.GetType() == typeof(String))
                {
                    if (!String.IsNullOrEmpty(filter.ToString()))
                    {
                        whereClause = PredicateBuilder.And(whereClause, g => g.GameName.ToLower().Contains(filter.ToString().ToLower()));
                    }
                }
                else
                {
                    bool genre = false;
                    IEnumerable<object> list = (IEnumerable<object>)filter;
                    Type elementType = list.GetType().GetGenericArguments()[0];
                    PropertyInfo idproperty = elementType.GetProperty("ID");
                    PropertyInfo property = elementType.GetProperty("Type");

                    List<object> displayTypes = list.Cast<object>().Select(type => property.GetValue(type, null)).ToList();
                    if (displayTypes.ElementAt(0).Equals("Genre"))
                        genre = true;

                    List<object> displayValues = list.Cast<object>().Select(id => idproperty.GetValue(id, null)).ToList();
                    List<int> IDs = new List<int>();
                    for(int i = 0; i < displayValues.Count(); i++)
                    {
                        IDs.Add((int)displayValues.ElementAt(i));
                    }
                    whereClause = (genre) ? PredicateBuilder.And(whereClause, g => IDs.Contains(g.Genre.GenreID)) : 
                        PredicateBuilder.And(whereClause, g => g.Platforms.Any(p => IDs.Contains(p.PlatformID)));
                }

            }
            RegisterAsyncTask(new PageAsyncTask(presetFields));
            IQueryable < Game > gameQuery = _db.Games.AsExpandable().Where(whereClause);
            return gameQuery;
        }

        public IQueryable<Game> GetGames()
        {
            return GetGamesWithParams(GetParameters());
        }
        //filter methods
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

        protected void FilterButton_Click(object sender, EventArgs e)
        {
            string searchStr = FilterGameName.Text;
            string genreID = DropDownGenre.SelectedValue;
            string paramStr = null;
            Boolean first = false;

            if (!String.IsNullOrWhiteSpace(searchStr))
            {
                paramStr += "?GameName=" + searchStr;
                first = true;
            }

            if (Int32.Parse(genreID) > 1)
            {
                paramStr += (first) ? "&GenreID=" + genreID : "?GenreID=" + genreID;
                first = true;
            }


            string selectedPlatforms = null;
            Boolean firstPlatform = false;
            for(int i = 0; i < chklstStates.Items.Count; i++)
            {
                if(chklstStates.Items[i].Selected)
                {
                    selectedPlatforms += (firstPlatform) ? "," + chklstStates.Items[i].Value : chklstStates.Items[i].Value;
                    firstPlatform = true;
                }
            }

            if(!String.IsNullOrWhiteSpace(selectedPlatforms))
            {
                paramStr += (first) ? "&PlatformIDs=" + selectedPlatforms : "?PlatformIDs=" + selectedPlatforms;
                first = true;
            }

            Response.Redirect("Games.aspx" + paramStr);
        }

        protected void GamesListView_Sorting(object sender, ListViewSortEventArgs e)
        {
            ImageButton imGameName = GamesListView.FindControl("imGameName") as ImageButton;
            ImageButton imGenre = GamesListView.FindControl("imGenre") as ImageButton;
            ImageButton imPlatform = GamesListView.FindControl("imPlatform") as ImageButton;
            ImageButton imCriticRating = GamesListView.FindControl("imCriticRating") as ImageButton;
            ImageButton imUserRating = GamesListView.FindControl("imUserRating") as ImageButton;

            string DefaultSortIMG = "/Images/Icons/desc.png";
            string imgUrl = "/Images/Icons/asc.png";

            if (ViewState["SortExpression"] != null)
            {
                if (ViewState["SortExpression"].ToString() == e.SortExpression)
                {
                    ViewState["SortExpression"] = null;
                    imgUrl = DefaultSortIMG;
                }
                else
                {
                    ViewState["SortExpression"] = e.SortExpression;
                }
            }
            else
            {
                    ViewState["SortExpression"] = e.SortExpression;
            }

            switch (e.SortExpression)
            {
                case "GAMENAME":
                    if (imGameName != null)
                        imGameName.ImageUrl = imgUrl;
                    if (imGenre != null)
                        imGenre.ImageUrl = DefaultSortIMG;
                    if (imPlatform != null)
                        imPlatform.ImageUrl = DefaultSortIMG;
                    if (imUserRating != null)
                        imUserRating.ImageUrl = DefaultSortIMG;
                    if (imCriticRating != null)
                        imCriticRating.ImageUrl = DefaultSortIMG;
                    break;
                case "GENRE":
                    if (imGenre != null)
                        imGenre.ImageUrl = imgUrl;
                    if (imGameName != null)
                        imGameName.ImageUrl = DefaultSortIMG;
                    if (imPlatform != null)
                        imPlatform.ImageUrl = DefaultSortIMG;
                    if (imUserRating != null)
                        imUserRating.ImageUrl = DefaultSortIMG;
                    if (imCriticRating != null)
                        imCriticRating.ImageUrl = DefaultSortIMG;
                    break;
                case "PLATFORMS":
                    if (imPlatform != null)
                        imPlatform.ImageUrl = imgUrl;
                    if (imGenre != null)
                        imGenre.ImageUrl = DefaultSortIMG;
                    if (imGameName != null)
                        imGameName.ImageUrl = DefaultSortIMG;
                    if (imUserRating != null)
                        imUserRating.ImageUrl = DefaultSortIMG;
                    if (imCriticRating != null)
                        imCriticRating.ImageUrl = DefaultSortIMG;
                    break;
                case "GAMERATING":
                    if (imCriticRating != null)
                        imCriticRating.ImageUrl = imgUrl;
                    if (imGameName != null)
                        imGameName.ImageUrl = DefaultSortIMG;
                    if (imGenre != null)
                        imGenre.ImageUrl = DefaultSortIMG;
                    if (imPlatform != null)
                        imPlatform.ImageUrl = DefaultSortIMG;
                    if (imUserRating != null)
                        imUserRating.ImageUrl = DefaultSortIMG;
                    break;
                case "AVERAGEUSERRATING":
                    if (imUserRating != null)
                        imUserRating.ImageUrl = imgUrl;
                    if (imGameName != null)
                        imGameName.ImageUrl = DefaultSortIMG;
                    if (imGenre != null)
                        imGenre.ImageUrl = DefaultSortIMG;
                    if (imPlatform != null)
                        imPlatform.ImageUrl = DefaultSortIMG;
                    if (imCriticRating != null)
                        imCriticRating.ImageUrl = DefaultSortIMG;
                    break;
            }
        }


    }
}