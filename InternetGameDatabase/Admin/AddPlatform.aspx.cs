using InternetGameDatabase.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InternetGameDatabase.Models;

namespace InternetGameDatabase.Admin
{
    public partial class AddPlatform : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SuccessLabel.Visible = false;
            if (!IsPostBack)
            {
                if (Request.QueryString["add"] == "true")
                    SuccessLabel.Visible = true;
            }
        }

        protected void AddPlatformButton_Click(object sender, EventArgs e)
        {
            AddPlatformController.AddPlatform(AddPlatformName.Text,AddPlatformDescription.Text);
            Response.Redirect("AddPlatform.aspx?add=true");
        }

        public IQueryable<Platform> getPlatforms()
        {
            var db = new GameContext();
            IQueryable<Platform> platformQuery = db.Platforms;
            return platformQuery;
        }

        protected void ConfirmRemove_Click(object sender, EventArgs e)
        {
            List<int> checkedValues = new List<int>();
            foreach (ListItem l in PlatformCheck.Items)
            {
                if (l.Selected == true)
                {
                    checkedValues.Add(Int32.Parse(l.Value));
                }
            }

            GameContext db = new GameContext();
            using (db)
            {
                IQueryable<Platform> targetPlatforms = db.Platforms.Where(p => checkedValues.Contains(p.PlatformID));
                foreach(Platform p in targetPlatforms)db.Platforms.Remove(p);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    Response.Redirect("AddPlatform.aspx?removeError=true");
                }

            }
            Response.Redirect("../Games.aspx");
        }
    }
}