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
    public partial class UserDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null || Request.QueryString["id"] == null)
            {
                Response.Redirect("Default.aspx");
            }

            PrepareUser();
        }

        private void PrepareUser()
        {
            int id;
            if (!int.TryParse(Request.QueryString["id"], out id))
            {
                Response.Redirect("Users.aspx");
            }

            User userToDisplay = ((IRepository)Application["database"]).GetUser(id);

            ShowUser(userToDisplay);
        }

        private void ShowUser(User userToDisplay)
        {
            if (userToDisplay == null)
            {
                Response.Redirect("Users.aspx");
            }

            lblUsername.Text = userToDisplay.UserName;
            lblAddress.Text = userToDisplay.Address;
            lblEmail.Text = userToDisplay.Email;
            lblPhone.Text = userToDisplay.PhoneNumber;
            lblCreatedAt.Text = userToDisplay.CreatedAt.ToShortDateString();
            CheckIfUserDeleted(userToDisplay);

            AddIcon(userToDisplay.EmailConfirmed, lblConfirmedEmail, lblNotConfirmedEmail);
            AddIcon(userToDisplay.PhoneNumberConfirmed, lblConfirmedPhone, lblNotConfirmedPhone);
        }

        private void AddIcon(bool confirmed, Label positive, Label negative)
        {
            if (confirmed)
            {
                positive.Visible = true;
            }
            else
            {
                negative.Visible = true;
            }

        }

        private void CheckIfUserDeleted(User userToDisplay)
        {
            if (userToDisplay.DeletedAt.HasValue)
            {
                lblDeletedAt.Text = userToDisplay.DeletedAt.Value.ToShortDateString();
            }
            else
            {
                lblDeletedAt.Text = " - ";
            }
        }


    }
}