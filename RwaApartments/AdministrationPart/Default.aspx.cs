using RwaUtilities.Crypto;
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
    public partial class Default : System.Web.UI.Page
    {
        private const string Admin = "Admin";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                panelForm.Visible = true;
                panelMessage.Visible = false;
            }

            if (Session["user"] != null)
            {
                Response.Redirect("Users.aspx");
            }
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                User user = ((IRepository)Application["database"]).AuthenticateUser(txtUsername.Text.Trim(), Cryptography.HashPassword(txtPassword.Text.Trim()));

                if(user != null && ((IRepository)Application["database"]).GetUserRoles(user.Id).ToList().Contains(Admin))
                {
                    Session["user"] = user;
                    Response.Redirect("Users.aspx");
                }
                else
                {
                    panelForm.Visible = true;
                    panelMessage.Visible = true;

                    txtUsername.Text = "";
                    txtPassword.Text = "";
                }
            }
        }
    }
}