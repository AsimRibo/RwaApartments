using RwaUtilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdministrationPart
{
    public partial class ApartmentAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IList<ApartmentImage> list = new List<ApartmentImage>
            {
                new ApartmentImage
                {
                    Guid = Guid.Parse("1F99FA36-8865-49D1-8EDC-9A8C4A7EBE68"),
                    Path = "Images\\1F99FA36-8865-49D1-8EDC-9A8C4A7EBE68.jpg"
                },
                new ApartmentImage
                {
                    Guid = Guid.Parse("2D50BC81-CEB1-4255-84FD-7AAFD6349CE9"),
                    Path = "Images\\2D50BC81-CEB1-4255-84FD-7AAFD6349CE9.jpg"
                }
            };

            gwImages.DataSource = list;
            gwImages.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                lblTemp.Text = "Success";
            }
        }

        protected void btnDeleteImage_Click(object sender, EventArgs e)
        {

        }

        protected void btnAddImage_Click(object sender, EventArgs e)
        {

        }
    }
}