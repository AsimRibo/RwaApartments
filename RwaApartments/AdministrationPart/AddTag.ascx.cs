using RwaUtilities.Models.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdministrationPart
{
    public partial class AddTag : System.Web.UI.UserControl
    {
        public event EventHandler OnBtnAddClick;

        public string TagName { get => txtTagName.Value; }

        public string TagNameEng { get => txtTagNameEng.Value; }

        public string TagType { get => ddlTagType.SelectedItem.ToString(); }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void InitDdl(IList<TagType> tagTypes)
        {
            ddlTagType.DataSource = tagTypes;
            ddlTagType.DataBind();
            ddlTagType.SelectedIndex = 0;
        }

        public void ResetUC()
        {
            txtTagName.Value = string.Empty;
            txtTagNameEng.Value = string.Empty;
            ddlTagType.SelectedIndex = 0;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid) OnBtnAddClick(sender, e);
        }
    }
}