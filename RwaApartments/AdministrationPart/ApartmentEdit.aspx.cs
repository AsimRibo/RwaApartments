using RwaUtilities.DAL;
using RwaUtilities.Models;
using RwaUtilities.Models.Tags;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdministrationPart
{
    public partial class ApartmentEdit : System.Web.UI.Page
    {
        IList<ApartmentImage> images = new List<ApartmentImage>();
        IList<Tag> tags;
        IList<Tag> selectedTags = new List<Tag>();
        Apartment apartment;

        private readonly string _imgPath = "Images\\";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null || Request.QueryString["id"] == null || !int.TryParse(Request.QueryString["id"], out _))
            {
                Response.Redirect("Default.aspx");
            }

            tags = ((IRepository)Application["database"]).GetAllTags();
            PrepareApartment();

            if (!IsPostBack)
            {
                PrepareDropDownLists();
                PrepareCheckBoxTags();
                PrepareImages();
                LoadGridView();
                SetCorrectValuesToDropDownLists();
            }


            if (IsPostBack)
            {
                if (ViewState["imgList"] != null)
                {
                    images = (IList<ApartmentImage>)ViewState["imgList"];

                }
            }
            


        }

        private void SetCorrectValuesToDropDownLists()
        {
            ddlCity.SelectedValue = apartment.City.IdCity.ToString();
            ddlOwners.SelectedValue = apartment.Owner.OwnerId.ToString();
        }

        private void PrepareApartment()
        {
            apartment = ((IRepository)Application["database"]).GetApartment(int.Parse(Request.QueryString["id"]));

            ShowApartment(apartment);
        }

        private void ShowApartment(Apartment apartment)
        {
            txtName.Text = apartment.Name;
            txtNameEng.Text = apartment.NameEng;
            txtBeachDistance.Text = apartment.BeachDistance.ToString();
            txtMaxAdults.Text = apartment.MaxAdults.ToString();
            txtMaxChildren.Text = apartment.MaxChildren.ToString();
            txtPrice.Text = apartment.Price.ToString("F");
            txtTotalRooms.Text = apartment.TotalRooms.ToString();
        }

        private void PrepareImages()
        {
            images = ((IRepository)Application["database"]).GetApartmentImages(int.Parse(Request.QueryString["id"]));
            ViewState["imgList"] = images;
        }

        private void PrepareCheckBoxTags()
        {
            chbListTags.DataSource = tags;
            chbListTags.DataTextField = nameof(Tag.NameEng);
            chbListTags.DataValueField = nameof(Tag.Id);
            chbListTags.DataBind();
            CheckCheckBoxes();
        }

        private void CheckCheckBoxes()
        {
            IList<Tag> apartmentTags = ((IRepository)Application["database"]).GetApartmentTags(int.Parse(Request.QueryString["id"]));
            foreach(Tag tag in apartmentTags)
            {
                chbListTags.Items.FindByText(tag.NameEng).Selected = true;
            }
        }

        private void PrepareDropDownLists()
        {
            FillDdl(ddlCity, ((IRepository)Application["database"]).GetAllCities(), nameof(City.IdCity), nameof(City.NameCity));
            FillDdl(ddlOwners, ((IRepository)Application["database"]).GetAllOwners(), nameof(ApartmentOwner.OwnerId), nameof(ApartmentOwner.OwnerName));

            FillDdl(ddlUsers, ((IRepository)Application["database"]).GetAllUsers(), nameof(RwaUtilities.Models.User.Id), nameof(RwaUtilities.Models.User.UserName));

            FillStatusDdl();
        }

        private void FillStatusDdl()
        {
            ddlStatus.DataSource = Enum.GetValues(typeof(ApartmentStatus));
            ddlStatus.DataBind();
            ddlStatus.SelectedIndex = 0;
        }

        protected void btnAddImage_Click(object sender, EventArgs e)
        {
            if (fuImage.HasFile)
            {
                try
                {
                    string ext = Path.GetExtension(fuImage.FileName);
                    ApartmentImage image = new ApartmentImage
                    {
                        Guid = Guid.NewGuid(),
                        CreatedAt = DateTime.Now,
                    };

                    string path = _imgPath + image.Guid + ext;
                    fuImage.SaveAs(Server.MapPath(path));

                    image.Path = path;

                    images.Add(image);
                    ViewState["imgList"] = images;
                    LoadGridView();
                }
                catch { }

            }
        }

        private void FillDdl<T>(DropDownList ddl, IList<T> data, string value, string display)
        {
            ddl.DataSource = data;
            ddl.DataValueField = value;
            ddl.DataTextField = display;
            ddl.DataBind();
            ddl.SelectedIndex = 0;
        }

        protected void btnDeleteImage_Click(object sender, EventArgs e)
        {
            string guid = (((LinkButton)sender).CommandArgument).ToString();
            ApartmentImage imageToRemove = images.First(image => image.Guid.ToString() == guid);
            if (imageToRemove.Id == 0)
            {
                images.Remove(imageToRemove);
            }
            else
            {
                imageToRemove.DoDelete = true;
            }
            
        }

        protected void btnSetRepresentative_Click(object sender, EventArgs e)
        {
            RemovePreviousRepresentativePictureFlag();
            string guid = (((LinkButton)sender).CommandArgument).ToString();
            images.ToList().ForEach(image =>
            {
                if (image.Guid.ToString() == guid)
                {
                    image.IsRepresentative = true;
                }
            });
        }

        private void RemovePreviousRepresentativePictureFlag()
        =>
            images.ToList().ForEach(image => image.IsRepresentative = false);

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void LoadGridView()
        {
            gvImages.DataSource = images;
            gvImages.DataBind();
        }
    }
}