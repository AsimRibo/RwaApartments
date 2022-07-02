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
    public partial class ApartmentAdd : System.Web.UI.Page
    {
        IList<ApartmentImage> images = new List<ApartmentImage>();
        IList<Tag> tags;
        IList<Tag> selectedTags = new List<Tag>();

        private readonly string _imgPath = "Images\\";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Default.aspx");
            }

            tags = ((IRepository)Application["database"]).GetAllTags();

            if (!IsPostBack)
            {
                PrepareDropDownLists();
                PrepareCheckBoxTags();
            }

            if (IsPostBack)
            {
                if (ViewState["imgList"] != null)
                {
                    images = (IList<ApartmentImage>)ViewState["imgList"];
                }
            }
        }

        private void PrepareCheckBoxTags()
        {
            chbListTags.DataSource = tags;
            chbListTags.DataTextField = nameof(Tag.NameEng);
            chbListTags.DataValueField = nameof(Tag.Id);
            chbListTags.DataBind();
        }

        private void PrepareDropDownLists()
        {
            FillDdl(ddlCity, ((IRepository)Application["database"]).GetAllCities(), nameof(City.IdCity), nameof(City.NameCity));
            FillDdl(ddlOwners, ((IRepository)Application["database"]).GetAllOwners(), nameof(ApartmentOwner.OwnerId), nameof(ApartmentOwner.OwnerName));
        }

        private void FillDdl<T>(DropDownList ddl, IList<T> data, string value, string display)
        {
            ddl.DataSource = data;
            ddl.DataValueField = value;
            ddl.DataTextField = display;
            ddl.DataBind();
            ddl.SelectedIndex = 0;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            panelMessage.Visible = false;
            if (chbListTags.Items.Cast<ListItem>().Any(i => i.Selected) && Page.IsValid)
            {
                CollectImageData();
                CollectTagsData();
                Apartment apartment = new Apartment
                {
                    Name = txtName.Text.Trim(),
                    NameEng = txtNameEng.Text.Trim(),
                    TotalRooms = int.Parse(txtTotalRooms.Text.Trim()),
                    BeachDistance = int.Parse(txtBeachDistance.Text.Trim()),
                    MaxAdults = int.Parse(txtMaxAdults.Text.Trim()),
                    MaxChildren = int.Parse(txtMaxChildren.Text.Trim()),
                    CreatedAt = DateTime.Now,
                    Price = Decimal.Parse(txtPrice.Text.Trim()),
                };
                int apartmentId = ((IRepository)Application["database"]).AddApartment(apartment, int.Parse(ddlCity.SelectedValue), int.Parse(ddlOwners.SelectedValue));
                AddTagsToDB(apartmentId);
                AddPicturesToDB(apartmentId);
                Response.Redirect("Apartments.aspx");
            }
            else
            {
                panelMessage.Visible = true;
            }
        }

        private void AddPicturesToDB(int apartmentId)
        {
            images.ToList().ForEach(i => ((IRepository)Application["database"]).AddApartmentImage(i, apartmentId));
        }

        private void AddTagsToDB(int apartmentId)
        {
            selectedTags.ToList().ForEach(tag => ((IRepository)Application["database"]).AddTagForApartment(tag.Id, apartmentId));
        }

        private void CollectTagsData()
        {
            foreach (ListItem item in this.chbListTags.Items)
            {
                if (item.Selected)
                    selectedTags.Add(new Tag { Id = int.Parse(item.Value) });
            }
        }

        private void CollectImageData()
        {
            foreach(GridViewRow row in gvImages.Rows)
            {
                ApartmentImage image = images.FirstOrDefault(x => x.Guid.ToString() == (row.FindControl("lblGuid") as Label).Text);
                image.Name = (row.FindControl("txtName") as TextBox).Text;
            }
            SetDefaultRepresentativePicture();
        }

        private void SetDefaultRepresentativePicture()
        {
            ApartmentImage image = images.FirstOrDefault(i => i.IsRepresentative);
            if (image is null)
            {
                images.ElementAt(0).IsRepresentative = true;
            }
        }

        protected void btnDeleteImage_Click(object sender, EventArgs e)
        {
            string guid = (((LinkButton)sender).CommandArgument).ToString();
            images.Remove(images.First(image => image.Guid.ToString() == guid));
            LoadGridView();
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

        private void LoadGridView()
        {
            gvImages.DataSource = images;
            gvImages.DataBind();
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
        
    }
}