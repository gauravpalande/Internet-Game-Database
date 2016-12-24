using InternetGameDatabase.Models;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using InternetGameDatabase.Amazon.PAAPI;

namespace InternetGameDatabase
{
    public partial class GameDetails : System.Web.UI.Page
    {
        Game searchGame;
        GameContext db;
        protected void Page_Load(object sender, EventArgs e)
        {
            db = new GameContext();
            if (Request.QueryString["id"] == null || !(Request.QueryString["id"].Length > 0)) Response.Redirect("games.aspx");
            else
            {
                int gameId = Int32.Parse(Request.QueryString["id"]);
                searchGame = (Game)db.Games.Where(g => g.GameID == gameId).FirstOrDefault();
                int stringLengthforColan = searchGame.GameName.Contains(':') ? searchGame.GameName.IndexOf(':') : searchGame.GameName.Length;
                int stringLengthforHyphen = searchGame.GameName.Contains('-') ? searchGame.GameName.IndexOf('-') : searchGame.GameName.Length;
                int stringLength = stringLengthforColan <= stringLengthforHyphen ? stringLengthforColan : stringLengthforHyphen;
                String clearString = searchGame.GameName.Substring(0, stringLength);
                String[] GameSearchName = clearString.Split(' ');
                String concatinated = "";
                foreach (String item in GameSearchName)
                {
                    concatinated += item;
                    concatinated += " ";
                }
                TextBox1.Text = concatinated.Trim();
            }
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                ReviewGame.PostBackUrl = "/User/UserReview.aspx?GameID=" + Request.QueryString["id"];
                ReviewGame.Visible = true;
            }

            if (HttpContext.Current.User.IsInRole("Administrator"))
            {

                RemoveGame.PostBackUrl = "/Admin/RemoveGameConfirm.aspx?GameID=" + Request.QueryString["id"];
                RemoveGame.Visible = true;
                UpdateGame.PostBackUrl = "/Admin/AddGame.aspx?GameID=" + Request.QueryString["id"];
                UpdateGame.Visible = true;
            }

            


        }

        public IQueryable<Game> getGame([QueryString("id")] int? gameId)
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

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable UserRatingList_GetData()
        {
            int i = Int32.Parse(Request.QueryString["id"]);
            return new GameContext().UserRatings.Where(u => u.game.GameID == i);
            
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            Label2.Text = "";
            // Instantiate Amazon ProductAdvertisingAPI client
            AWSECommerceServicePortTypeClient amazonClient = new AWSECommerceServicePortTypeClient();

            // prepare an ItemSearch request
            ItemSearchRequest request = new ItemSearchRequest();
            request.SearchIndex = "VideoGames";
            request.Title = TextBox1.Text;
            request.ResponseGroup = new string[] { "Small" };

            ItemSearch itemSearch = new ItemSearch();
            itemSearch.Request = new ItemSearchRequest[] { request };
            itemSearch.AWSAccessKeyId = "AKIAJ2RWGPJUYWJDC2NQ";
            itemSearch.AssociateTag = "gauravpalande-20";

            try
            {
                // send the ItemSearch request
                ItemSearchResponse response = amazonClient.ItemSearch(itemSearch);

                // write out the results from the ItemSearch request
                foreach (var item in response.Items[0].Item)
                {
                    Label2.Text += "Game Title: ";
                    Label2.Text += String.Format("<a href='{0}'>{1}</a>", item.DetailPageURL, item.ItemAttributes.Title + "<br />");
                }
            }
            catch (Exception ex)

            {

                Label2.Text = "<br />Games not found <br />";

            }

            AWSECommerceServicePortTypeClient amazonClient2 = new AWSECommerceServicePortTypeClient();

            // prepare an ItemSearch request
            ItemSearchRequest request2 = new ItemSearchRequest();
            request2.SearchIndex = "Toys";
            request2.Title = TextBox1.Text;
            request2.ResponseGroup = new string[] { "Small" };

            ItemSearch itemSearch2 = new ItemSearch();
            itemSearch2.Request = new ItemSearchRequest[] { request2 };
            itemSearch2.AWSAccessKeyId = "AKIAJ2RWGPJUYWJDC2NQ";
            itemSearch2.AssociateTag = "gauravpalande-20";

            try
            {
                // send the ItemSearch request
                ItemSearchResponse response2 = amazonClient.ItemSearch(itemSearch2);

                // write out the results from the ItemSearch request
                foreach (var item in response2.Items[0].Item)
                {
                    Label2.Text += "Toy Title: ";
                    Label2.Text += String.Format("<a href='{0}'>{1}</a>", item.DetailPageURL, item.ItemAttributes.Title + "<br />");
                }
            }
            catch (Exception ex)

            {

                Label2.Text = "<br />Games not found <br />";
            }

            AWSECommerceServicePortTypeClient amazonClient3 = new AWSECommerceServicePortTypeClient();

            // prepare an ItemSearch request
            ItemSearchRequest request3 = new ItemSearchRequest();
            request3.SearchIndex = "MobileApps";
            request3.Title = TextBox1.Text;
            request3.ResponseGroup = new string[] { "Small" };

            ItemSearch itemSearch3 = new ItemSearch();
            itemSearch3.Request = new ItemSearchRequest[] { request3 };
            itemSearch3.AWSAccessKeyId = "AKIAJ2RWGPJUYWJDC2NQ";
            itemSearch3.AssociateTag = "gauravpalande-20";

            try
            {
                // send the ItemSearch request
                ItemSearchResponse response3 = amazonClient.ItemSearch(itemSearch3);

                // write out the results from the ItemSearch request
                foreach (var item in response3.Items[0].Item)
                {
                    Label2.Text += "MobilApp Title: ";
                    Label2.Text += String.Format("<a href='{0}'>{1}</a>", item.DetailPageURL, item.ItemAttributes.Title + "<br />");
                }
            }
            catch (Exception ex)

            {

                Label2.Text = "<br />Games not found <br />";
            }
        }
    }
}