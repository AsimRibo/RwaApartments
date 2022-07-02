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
    public partial class Apartments : System.Web.UI.Page
    {
        private IList<Apartment> allApartments;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Default.aspx");
            }

            allApartments = ((IRepository)Application["database"]).GetAllApartments();
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            rptApartments.DataSource = allApartments;
            rptApartments.DataBind();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(((LinkButton)sender).CommandArgument);
            allApartments.Remove(allApartments.FirstOrDefault(a => a.Id == id));
            ((IRepository)Application["database"]).DeleteApartment(id);
        }

        protected void btnDetails_Click(object sender, EventArgs e)
        {
            int apartmentId = int.Parse(((LinkButton)sender).CommandArgument);

            Response.Redirect($"ApartmentDetails.aspx?id={apartmentId}");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int apartmentId = int.Parse(((LinkButton)sender).CommandArgument);

            Response.Redirect($"ApartmentEdit.aspx?id={apartmentId}");
        }

        protected void btnAddApartment_Click(object sender, EventArgs e)
        {
            Response.Redirect($"ApartmentAdd.aspx");
        }
    }
}