using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Data.Linq;
using Telerik.Web.UI;

namespace AimTags
{
    public partial class TagList : AIM.Base.Modules.Page.AimTags
    {
        protected override void Page_Load()
        {
            setLanguage();
            if (!IsPostBack)
            {
                initTagList(0);                
                initTypeDropDown();
            }
           
        }

        private void initTagList(int TagTypeID)
        {
            if (TagTypeID == 0)
            {
                var Tags = from t in db.Tags
                           where !t.Deleted
                           select new
                           {
                               t.Name,
                               t.ID,
                               t.Slug,
                               TagType = t.TagType.Name
                           };

                rgTags.DataSource = Tags;
                rgTags.DataBind();
            }
            else
            {
                var Tags = from t in db.Tags
                           where !t.Deleted && t.TagType_FK == TagTypeID
                           select new
                           {
                               t.Name,
                               t.ID,
                               t.Slug,
                               TagType = t.TagType.Name
                           };

                rgTags.DataSource = Tags;
                rgTags.DataBind();
            }

            
                       
        }

        private void setLanguage()
        {
            rgTags.Columns[0].HeaderText = Language.getValue("rgTags_name");
            rgTags.Columns[1].HeaderText = Language.getValue("rgTags_slug");
            rgTags.Columns[2].HeaderText = Language.getValue("rgTags_tagtype");
            lblChooseTagType.Text = Language.getValue("lblChooseTagType");
        }
        protected void rgTags_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridCommandItem)
            {
                try
                {
                    GridCommandItem cmditm = (GridCommandItem)e.Item;
                    LinkButton lbNewAdmin = (LinkButton)cmditm.Controls[0].FindControl("lbNew");

                    lbNewAdmin.Text = "<img class=\"textimage\" src=\"/Images/newobject.gif\" alt=\"" + Language.getValue("lbNew") + "\" />&nbsp;"
                        + Language.getValue("lbNew");
                }
                catch { }
            }
        }
        protected void rgTags_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "new":
                    Response.Redirect("/Apps/AimTags/Tag.aspx");
                    break;
                case "editadmin":
                    Response.Redirect("/Apps/AimTags/Tag.aspx?ID=" + rgTags.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString());
                    break;
                case "removeadmin":
                    AIM.Business.AimTags.Tag selectedTag = db.Tags.Single(t=>t.ID==int.Parse(rgTags.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString()));
                    selectedTag.Deleted = true;
                    db.SubmitChanges();
                    initTagList(int.Parse(ddlTagTypes.SelectedValue));
                    break;                
            }

        }
        private void initTypeDropDown()
        {
            var Types = from t in db.TagTypes
                        select t;
            ddlTagTypes.DataSource = Types;
            ddlTagTypes.DataTextField = "Name";
            ddlTagTypes.DataValueField = "ID";
            ddlTagTypes.DataBind();

            ListItem li = new ListItem("Visa alla taggar","0");
            ddlTagTypes.Items.Insert(0, li);
        }
        protected void ddlTagTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            initTagList(int.Parse(ddlTagTypes.SelectedValue));
        }
    }
}