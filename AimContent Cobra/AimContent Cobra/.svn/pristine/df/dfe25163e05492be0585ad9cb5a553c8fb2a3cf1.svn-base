using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.IO;

namespace AimTags
{
    public partial class EditContent : AIM.Base.Modules.Page.AimTags
    {
        private int ContentID=0;

        protected override void Page_Load()
        {
            if (Request["ID"] != null)
                ContentID = int.Parse(Request["ID"].ToString());
            setLanguage();
            if (!IsPostBack)
            {
                initTags();
                initContentTypes();

                if (ContentID != 0)
                    initEditContent();
            }
           

            
        }

        private void initContentTypes()
        {
            var ContentTypes = from c in db.ContentTypes
                               where c.SiteID == config.siteID
                               select c;

            ddlContentType.DataTextField = "Name";
            ddlContentType.DataValueField = "ID";

            ddlContentType.DataSource = ContentTypes;
            ddlContentType.DataBind();

            ListItem li = new ListItem("Välj typ...","0");
            ddlContentType.Items.Insert(0,li);

        }

        private void setLanguage()
        {
        }

        private void initEditContent()
        {
            initTags();
            pnlContentType.Visible = false;
            pnlForm.Visible = true;
            var Content = (from c in db.Contents
                          where c.SiteID == config.siteID && c.ID == ContentID
                          select c).First();

            tbName.Text = Content.Name;
            tbSearchWords.Text = Content.SearchWords;
            tbDescription.Text = Content.Description;
            ddlContentType.SelectedValue = Content.ContentType_FK.ToString();
            switch (Content.ContentType.Name)
            {
                case "Vanlig text":
                    pnlHtml.Visible = true;
                    pnlFile.Visible = false;
                    editor1.Content = Content.HtmlContent;
                    break;
                default:
                    pnlFile.Visible = true;
                    pnlCurrentFile.Visible = true;
                    lblCurrentFile.Text = "<a href=\"" + config.customerpath + "Content/" + Content.FileName + "\" target=\"_blank\">" + Content.FileName + "</a>";
                    break;
            }

            SetSelectedTags(ContentID);
        }

        private void SetSelectedTags(int ContentID)
        {
            var Tags = from t in db.Tags_X_Contents
                       where t.Content_FK == ContentID && !t.Tag.Deleted
                       select new
                       {
                           Name = t.Tag.Name + " [" + t.Tag.TagType.Name + "]",
                           t.Tag.ID
                       };

            rlbSelectedTags.DataSource = Tags;
            rlbSelectedTags.DataTextField = "Name";
            rlbSelectedTags.DataValueField = "ID";
            rlbSelectedTags.DataBind();

            List<RadListBoxItem> itemstodelete = new List<RadListBoxItem>();
            foreach (RadListBoxItem li_chosen in rlbSelectedTags.Items)
            {
                foreach (RadListBoxItem li_available in rlbAvailableTags.Items)
                {
                    if (li_available.Value == li_chosen.Value)
                    {
                        itemstodelete.Add(li_available);
                    }
                }
            }
            foreach (RadListBoxItem li in itemstodelete)
                rlbAvailableTags.Items.Remove(li);
        }
        private void initTags()
        {
            var AvailableTags = from t in db.Tags
                                where !t.Deleted
                                orderby t.Name
                                select new
                                {
                                    Name = t.Name + " [" + t.TagType.Name + "]",
                                    t.ID
                                };

            rlbAvailableTags.DataSource = AvailableTags;
            rlbAvailableTags.DataTextField = "Name";
            rlbAvailableTags.DataValueField = "ID";
            rlbAvailableTags.DataBind();
        }
        protected void ddlContentType_SelectedIndexChanged(Object sender, EventArgs e)
        {
            if (ddlContentType.SelectedIndex != 0)
            {
                pnlForm.Visible = true;
                switch (ddlContentType.SelectedItem.Text)
                {
                    case "Vanlig text":
                        pnlHtml.Visible = true;
                        pnlFile.Visible = false;
                        break;
                    case "Länk":
                    case "Film":
                        pnlFile.Visible = false;
                        break;
                    default:
                        pnlFile.Visible = true;
                        break;
                }
            }
        }
        protected void btnSave_Click(Object sender, EventArgs e)
        {

            int contentID;


            if (ContentID != 0)
            {
                AIM.Business.AimTags.Content content = (from c in db.Contents
                           where c.SiteID == config.siteID && c.ID == ContentID
                           select c).First();
                ClearSelectedTags(ContentID);

                string filename = CheckAndUploadFiles();

                if (filename != "")
                {
                    DeleteFile(content.FileName);
                    content.FileName = filename;
                }
                

                content.Name = tbName.Text;
                content.SearchWords = tbSearchWords.Text;
                content.Description = tbDescription.Text;
                content.HtmlContent = editor1.Content;
                content.DateAdded = DateTime.Now;
                content.SiteID = config.siteID;

                db.SubmitChanges();

                contentID = content.ID;

            }
            else
            {
                AIM.Business.AimTags.Content content = new AIM.Business.AimTags.Content();
                string filename = CheckAndUploadFiles();
                content.Name = tbName.Text;
                content.SearchWords = tbSearchWords.Text;
                content.FileName = filename;
                content.Description = tbDescription.Text;
                content.HtmlContent = editor1.Content;
                content.DateAdded = DateTime.Now;
                content.SiteID = config.siteID;
                content.ContentType_FK = int.Parse(ddlContentType.SelectedValue);
                db.Contents.InsertOnSubmit(content);

                db.SubmitChanges();
                contentID = content.ID;
            }

            foreach (RadListBoxItem li in rlbSelectedTags.Items)
            {
                AIM.Business.AimTags.Tags_X_Content tXc = new AIM.Business.AimTags.Tags_X_Content();
                tXc.Content_FK = contentID;
                tXc.Tag_FK = int.Parse(li.Value);

                db.Tags_X_Contents.InsertOnSubmit(tXc);
                db.SubmitChanges();
            }

            Response.Redirect("Content.aspx");
            
            
        }

        private void DeleteFile(string filename)
        {
            string path = config.customerpath + "Content/";
            if(File.Exists(Server.MapPath(path + filename)))
                File.Delete(Server.MapPath(path + filename));
        }

        private string CheckAndUploadFiles()
        {
            int selectedValue = 0;
            int.TryParse(ddlContentType.SelectedValue, out selectedValue);

            var ContentType = (from ct in db.ContentTypes
                              where ct.ID == selectedValue
                              select ct).Single();

            if (ContentType.HasFile)
            {
                if (fuFile.HasFile)
                {
                    string filename = fuFile.FileName;
                    string path = config.customerpath + "Content/";
                    if (!File.Exists(Server.MapPath(path + filename)))
                        fuFile.SaveAs(Server.MapPath(path + filename));
                    else
                    {
                        int i = 1;
                        while(File.Exists(Server.MapPath(path + filename)))
                        {
                            string extension = System.IO.Path.GetExtension(filename);
                            string filenamewithoutextension = System.IO.Path.GetFileNameWithoutExtension(filename);
                            filename = filenamewithoutextension + i + extension;
                            i++;
                        }
                        fuFile.SaveAs(Server.MapPath(path + filename));
                    }
                    return filename;
                }
                return "";
            }
            else
                return "";
        }

        private void ClearSelectedTags(int ContentID)
        {
            var SelectedTags = from t in db.Tags_X_Contents
                               where t.Content_FK == ContentID
                               select t;
            foreach (AIM.Business.AimTags.Tags_X_Content tag in SelectedTags)
            {
                db.Tags_X_Contents.DeleteOnSubmit(tag);
            }
            db.SubmitChanges();
        }
    }
}