using RwaUtilities;
using RwaUtilities.DAL;
using RwaUtilities.Models;
using RwaUtilities.Models.Tags;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdministrationPart
{
    public partial class ApartmentDetails : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null || Request.QueryString["id"] == null)
            {
                Response.Redirect("Default.aspx");
            }

            PrepareApartment();           
        }

        private void PrepareApartment()
        {
            int id;
            if (!int.TryParse(Request.QueryString["id"], out id))
            {
                Response.Redirect("Apartments.aspx");
            }

            Apartment apartment = ((IRepository)Application["database"]).GetApartment(id);
            
            ShowApartment(apartment);
        }

        private void ShowApartment(Apartment apartment)
        {
            if (apartment == null)
            {
                Response.Redirect("Apartments.aspx");
            }

            txtName.Text = apartment.NameEng;
            txtCreatedAt.Text = apartment.CreatedAt.ToShortDateString();
            txtOwner.Text = apartment.Owner.OwnerName;
            txtMaxAdults.Text = apartment.MaxAdults.ToString();
            txtMaxChildren.Text = apartment.MaxChildren.ToString();
            txtTotalRooms.Text = apartment.TotalRooms.ToString();
            txtBeachDistance.Text = apartment.BeachDistance.ToString();
            txtCity.Text = apartment.City.NameCity;
            txtPrice.Text = apartment.Price.ToString("F");

            apartment.Images = ((IRepository)Application["database"]).GetApartmentImages(apartment.Id);


            HandleApartmentStatus(apartment);
            HandleApartmentImages(apartment);
            AddApartmentTags(apartment.Id);
        }

        private void HandleApartmentImages(Apartment apartment)
        {
            //imgActive.Src = $"{Constants.PicturePath}{apartment.Images.ElementAt(0).Guid}.jpg";
            imgActive.Src = apartment.Images.ElementAt(0).Path;
            lblDescription.Text = $"{apartment.Images.ElementAt(0).Name}";


            apartment.Images.Skip(1).ToList().ForEach(image =>
            {
                AddImageToCarousel(image);
            });
        }

        private void AddImageToCarousel(ApartmentImage image)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<div class=\"carousel-item\">");
            //sb.AppendLine($"<img src=\"Images\\{image.Guid}.jpg\" class=\"d-block w-100\" alt=\"Picture\" style=\"height: 25rem\">");
            sb.AppendLine($"<img src=\"{image.Path}\" class=\"d-block w-100\" alt=\"Picture\" style=\"height: 25rem\">");
            sb.AppendLine("<div class=\"carousel-caption d-none d-md-block\">");
            sb.AppendLine($"<label>{image.Name}</label>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");

            literalPictures.Text += sb.ToString();
        }

        private void AddApartmentTags(int id)
        {
            IList<Tag> tags = ((IRepository)Application["database"]).GetApartmentTags(id);
            blTags.DataSource = tags;
            blTags.DataBind();
            
        }

        private void HandleApartmentStatus(Apartment apartment)
        {
            txtStatus.Text = apartment.Status.ToString();
            if (apartment.Status != ApartmentStatus.Vacant)
            {
                txtReservedBy.Text = ((IRepository)Application["database"]).GetReservationUsername(apartment.Id);
            }
        }
    }
}