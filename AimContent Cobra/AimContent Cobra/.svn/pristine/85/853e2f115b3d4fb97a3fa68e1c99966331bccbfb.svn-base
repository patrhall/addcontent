using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using AIM.Business.AimTags;
using System.IO;

namespace AimTags
{
    public partial class Content : AIM.Base.Modules.Page.AimTags
    {
        protected override void Page_Load()
        {
            setLanguage();
            if (!IsPostBack)
            {
                initContentList();                
            }
        }
        private void initContentList()
        {
            var Content = from c in db.Contents
                          where c.SiteID == config.siteID
                          select c;

            rgContent.DataSource = Content;
            rgContent.DataBind();
       
        }

        private void setLanguage()
        {
            rgContent.Columns[0].HeaderText = Language.getValue("rgContent_name");
            rgContent.Columns[1].HeaderText = Language.getValue("rgContent_tags");
            rgContent.Columns[2].HeaderText = Language.getValue("rgContent_dateadded");
        }
        protected void rgContent_ItemCreated(object sender, GridItemEventArgs e)
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
        protected void rgContent_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                int contentID = 0;
                if (int.TryParse(rgContent.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString(), out contentID))
                {
                    Repeater rptTags = (Repeater)e.Item.FindControl("rptTags");
                    var Tags = from t in db.Tags_X_Contents
                               where t.Content_FK == contentID && !t.Tag.Deleted
                               select new
                               {
                                   TagName = t.Tag.Name,
                                   t.ID
                               };

                    rptTags.DataSource = Tags;
                    rptTags.DataBind();
                }
            }
            catch { }
        }
        protected void rgContent_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "new":
                    Response.Redirect("/Apps/AimTags/EditContent.aspx");
                    break;
                case "editadmin":
                    Response.Redirect("/Apps/AimTags/EditContent.aspx?ID=" + rgContent.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString());
                    break;
                case "removeadmin":
                    DeleteContent(int.Parse(rgContent.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString()));                    
                    break;
            }
        }

        private void DeleteContent(int ContentID)
        {
            var delContent = (from c in db.Contents
                             where c.ID == ContentID
                             select c).Single();

            var delTags = from t in db.Tags_X_Contents
                          where t.Content_FK == ContentID
                          select t;

            string path = config.customerpath + "Content/";
            if(delContent.FileName!="")
                if(File.Exists(Server.MapPath(path + delContent.FileName)))
                    File.Delete(Server.MapPath(path + delContent.FileName));

            db.Tags_X_Contents.DeleteAllOnSubmit(delTags);
            db.SubmitChanges();

            db.Contents.DeleteOnSubmit(delContent);
            db.SubmitChanges();
            initContentList();
        }
        protected void rptTags_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                int ContentTagID = 0;
                if(int.TryParse(e.CommandArgument.ToString(), out ContentTagID))
                {
                    Tags_X_Content tXc = (from t in db.Tags_X_Contents
                                         where t.ID == ContentTagID
                                         select t).First();

                    db.Tags_X_Contents.DeleteOnSubmit(tXc);
                    db.SubmitChanges();
                    initContentList();
                }

            }
        }
        
    }
}