using RwaUtilities;
using RwaUtilities.DAL;
using RwaUtilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdministrationPart
{
    public partial class Users : System.Web.UI.Page
    {
        private IList<User> allUsers;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Default.aspx");
            }


            allUsers = ((IRepository)Application["database"]).GetAllUsers();
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            rptUsers.DataSource = allUsers;
            rptUsers.DataBind();
        }

        protected void btnDetails_Click(object sender, EventArgs e)
        {
            int userId = int.Parse(((LinkButton)sender).CommandArgument);

            Response.Redirect($"UserDetails.aspx?id={userId}");
            
        }
    }
}