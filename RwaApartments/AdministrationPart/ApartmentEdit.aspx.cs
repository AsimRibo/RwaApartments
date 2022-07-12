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
            apartment = ((IRepository)Application["database"]).GetApartment(int.Parse(Request.QueryString["id"]));

            if (!IsPostBack)
            {
                PrepareDropDownLists();
                PrepareCheckBoxTags();
                PrepareImages();
                LoadGridView();
                SetCorrectValuesToDropDownLists();
                ShowApartment(apartment);
            }


            if (IsPostBack)
            {
                panelMessage.Visible = false;
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
            LoadGridView();
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
            if (chbListTags.Items.Cast<ListItem>().Any(i => i.Selected))
            {
                HandleTagsChange();

                CollectImagesData();

                HandleImagesChange();

                UpdateApartment();

                HandleReservation();

                Response.Redirect("Apartments.aspx");
            }
            else
            {
                panelMessage.Visible = true;
            }
        }

        private void HandleReservation()
        {
            if (ddlStatus.SelectedItem.ToString() != ApartmentStatus.Vacant.ToString())
            {
                ApartmentReservation reservation = new ApartmentReservation();

                reservation.Details = txtDetails.Text.Trim();
                reservation.ApartmentId = apartment.Id;

                if (chbTypeOfUser.Checked)
                {
                    ((IRepository)Application["database"]).AddReservationById(reservation, int.Parse(ddlUsers.SelectedValue));
                }
                else
                {
                    reservation.UserPhone = txtPhone.Text.Trim();
                    reservation.Username = txtUsername.Text.Trim();
                    reservation.UserEmail = txtEmail.Text.Trim();
                    reservation.UserAddress = txtAddress.Text.Trim();

                    ((IRepository)Application["database"]).AddReservation(reservation);
                }
            }
            else
            {
                ((IRepository)Application["database"]).FreeApartmentFromReservation(apartment.Id);
            }
        }

        private void UpdateApartment()
        {
            apartment.Name = txtName.Text.Trim();
            apartment.NameEng = txtNameEng.Text.Trim();
            apartment.TotalRooms = int.Parse(txtTotalRooms.Text.Trim());
            apartment.BeachDistance = int.Parse(txtBeachDistance.Text.Trim());
            apartment.MaxAdults = int.Parse(txtMaxAdults.Text.Trim());
            apartment.MaxChildren = int.Parse(txtMaxChildren.Text.Trim());
            apartment.Price = Decimal.Parse(txtPrice.Text.Trim());

            ((IRepository)Application["database"]).UpdateApartment(apartment, int.Parse(ddlCity.SelectedValue), int.Parse(ddlOwners.SelectedValue));
            
        }

        private void CollectImagesData()
        {
            foreach (GridViewRow row in gvImages.Rows)
            {
                ApartmentImage image = images.FirstOrDefault(x => x.Guid.ToString() == (row.FindControl("lblGuid") as Label).Text);
                image.Name = (row.FindControl("txtName") as TextBox).Text;
            }
            SetDefaultRepresentativePicture();
        }

        private void SetDefaultRepresentativePicture()
        {
            ApartmentImage image = images.FirstOrDefault(i => i.IsRepresentative && !i.DoDelete);
            if (image is null)
            {
                images.ElementAt(0).IsRepresentative = true;
            }
        }

        private void HandleImagesChange()
        {
            images.ToList().ForEach(i =>
            {
                if (i.DoDelete)
                {
                    ((IRepository)Application["database"]).DeleteApartmentImage(i.Id);
                }
                else if(i.Id == 0)
                {
                    ((IRepository)Application["database"]).AddApartmentImage(i, apartment.Id);
                }
                else
                {
                    ((IRepository)Application["database"]).UpdateApartmentImage(i.Id, i.Name, i.IsRepresentative);
                }
            });
        }

        private void HandleTagsChange()
        {
            foreach (ListItem item in this.chbListTags.Items)
            {
                if (item.Selected)
                    selectedTags.Add(new Tag { Id = int.Parse(item.Value) });
            }
            IList<Tag> originalTags = ((IRepository)Application["database"]).GetApartmentTags(apartment.Id);

            DeleteTagsFromDb(originalTags);

            AddNewTagsToDb(originalTags);
        }

        private void AddNewTagsToDb(IList<Tag> originalTags)
        {
            foreach (Tag tag in selectedTags)
            {
                if (!originalTags.Contains(tag))
                {
                    ((IRepository)Application["database"]).AddTagForApartment(tag.Id, apartment.Id);
                }
            }
        }

        private void DeleteTagsFromDb(IList<Tag> originalTags)
        {
            foreach (Tag tag in originalTags)
            {
                if (!selectedTags.Contains(tag))
                {
                    ((IRepository)Application["database"]).DeleteTagForApartment(tag.Id, apartment.Id);
                }
            }
        }

        private void LoadGridView()
        {
            gvImages.DataSource = images.Where(i => !i.DoDelete);
            gvImages.DataBind();
        }
    }
}