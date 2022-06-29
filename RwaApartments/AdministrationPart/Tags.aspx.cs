using RwaUtilities.DAL;
using RwaUtilities.Models.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdministrationPart
{
    public partial class Tags : System.Web.UI.Page
    {
        private IList<Tag> allTags;
        private IList<TagType> allTagTypes;

        private AddTag ctrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Default.aspx");
            }


            allTags = ((IRepository)Application["database"]).GetAllTags();
            allTagTypes = ((IRepository)Application["database"]).GetAllTagTypes();

            LoadAddTagUC();

            if (!IsPostBack)
            {
                panelMessage.Visible = false;
                panelAdd.Visible = false;
                LoadData();
            }

        }

        private void Ctrl_OnBtnAddClick(object sender, EventArgs e)
        {
            panelMessage.Visible = false;

            TagType type = allTagTypes.First(t => t.NameEng == ctrl.TagType);
            Tag tag = new Tag
            {
                Name = ctrl.TagName,
                NameEng = ctrl.TagNameEng,
                CreatedAt = DateTime.Now,
                TagType = type
            };

            int id = ((IRepository)Application["database"]).AddTag(tag);

            tag.Id = id;
            
            allTags.Add(tag);
            LoadData();

            HandlePanelAdd();
        }

        private void HandlePanelAdd()
        {
            ctrl.ResetUC();
            panelAdd.Visible = false;

        }

        private void LoadAddTagUC()
        {
            ctrl = LoadControl("AddTag.ascx") as AddTag;
            phAddTag.Controls.Add(ctrl);
            ctrl.OnBtnAddClick += Ctrl_OnBtnAddClick;
            FillAddTagDdl();
        }

        private void FillAddTagDdl()
        {
            ctrl.InitDdl(allTagTypes);
        }

        private void LoadData()
        {
            rptTags.DataSource = allTags;
            rptTags.DataBind();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            panelAdd.Visible = false;
            int id = int.Parse(((LinkButton)sender).CommandArgument);
            Tag tag = allTags.FirstOrDefault(t => t.Id == id);
            if (tag != null && tag.TagCount == 0)
            {
                allTags.Remove(tag);
                ((IRepository)Application["database"]).DeleteTag(id);
                LoadData();
                panelMessage.Visible = false;
            }
            else
            {
                panelMessage.Visible = true;
            }
        }

        protected void btnAddTag_Click(object sender, EventArgs e)
        {
            panelAdd.Visible = true;

        }
    }
}