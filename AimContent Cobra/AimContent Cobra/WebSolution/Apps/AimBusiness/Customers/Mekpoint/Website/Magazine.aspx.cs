using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.IO;

namespace AimBusiness.Customers.Mekpoint.Website
{
    public partial class Magazine : AIM.Base.Modules.Page.AimBusiness.Mekpoint.Website.AimBusiness
    {
        private int _magazineID;

        protected override void Page_Load()
        {
            _magazineID = int.Parse(Request["oID"]);
            AIM.Base.Classes.RadHandler.RadEditor_LoadEditorPreference(ref reArticle, config);

            if (!IsPostBack)
                init();
        }

        protected void showOverview()
        {
            pnlOverview.Visible = true;
            pnlDetail.Visible = false;
            pnlArticleList.Visible = false;
            pnlArticle.Visible = false;
        }

        protected void showDetail()
        {
            pnlOverview.Visible = false;
            pnlDetail.Visible = true;
            pnlArticleList.Visible = false;
            pnlArticle.Visible = false;
        }

        protected void showArticleList(int id)
        {
            pnlOverview.Visible = false;
            pnlDetail.Visible = false;
            pnlArticleList.Visible = true;
            pnlArticle.Visible = false;

            hfMagazineId.Value = id.ToString();

            rgArticleList.Rebind();
        }

        protected void showArticle(int id)
        {
            pnlOverview.Visible = false;
            pnlDetail.Visible = false;
            pnlArticleList.Visible = false;
            pnlArticle.Visible = true;

            hfArticleId.Value = id.ToString();

            fillArticleFields(id);
        }

        private void init()
        {
            showOverview();            
        }

        private void clearFields()
        {
            tbName.Text = String.Empty;
            tbHeadline.Text = String.Empty;
            tbDescription.Text = String.Empty;
            rdpTime.Clear();
            tbArticleHeadline.Text = String.Empty;
            reArticle.Content = String.Empty;
            tbArticleIntroText.Text = String.Empty;
        }

        private void showEditNew()
        {
            showDetail();
            hfID.Value = String.Empty;

            hideAllFileUploads();

            clearFields();
        }

        private void hideAllFileUploads()
        {
            pnlImage.Visible = false;
            pnlPdfPart1.Visible = false;
            pnlPdfPart2.Visible = false;
            pnlPdfPart3.Visible = false;
            pnlHtml.Visible = false;
        }

        private void showEditObject(int id)
        {
            showDetail();
            hfID.Value = id.ToString();
            fillFields(id);
        }

        private void fillArticleFields(int id)
        {
            var article = (from o in db.Objects
                           where o.SiteID == config.siteID &&
                           o.ObjectID == id
                           select o).Single();

            tbArticleHeadline.Text = article.Title;
            tbArticleIntroText.Text = article.IntroText;
            reArticle.Content = article.HTMLContent;

            pnlArticleImage.Visible = false;
            if (!String.IsNullOrEmpty(article.Picture))
            {
                pnlArticleImage.Visible = true;
                hlArticleImage.NavigateUrl = config.customerpath + "magazines/images/" + article.Picture;
            }

            cbUsePublicationDate.Checked = article.PublishFrom.HasValue || article.PublishTo.HasValue;

            if (article.PublishFrom.HasValue)
            {
                calFrom.SelectedDate = article.PublishFrom.Value;
                calFrom.FocusedDate = article.PublishFrom.Value;
            }

            if (article.PublishTo.HasValue)
            {
                calTo.SelectedDate = article.PublishTo.Value;
                calTo.FocusedDate = article.PublishTo.Value;
            }

            initRoles(article.ObjectID);
        }

        private void initRoles(int oID)
        {
            if (config.applicationname != "")
            {
                string[] siteRoles = (from o in db.aspnet_Roles
                                      where o.aspnet_Application.ApplicationName == config.applicationname
                                      orderby o.RoleName
                                      select o.RoleName).ToArray();

                if (siteRoles.Length > 0)
                {
                    var objectRoles = db.usp_GetObjectRoles(oID).ToList();
                    cblRoleProperty.Items.Clear();

                    for (int i = 0; i < siteRoles.Length; i++)
                    {
                        ListItem li = new ListItem(siteRoles[i], "list" + i);

                        cblRoleProperty.Items.Add(li);

                        foreach (var objectRole in objectRoles)
                        {
                            if (siteRoles[i] == objectRole.RoleName)
                                cblRoleProperty.Items[i].Selected = true;
                        }
                    }
                }
            }
        }

        private void saveRoles(int oID)
        {
            db.usp_DeleteObjectRoles(oID);

            for (int i = 0; i < cblRoleProperty.Items.Count; i++)
            {
                if (cblRoleProperty.Items[i].Selected == true)
                {
                    Guid siteRoleId = db.usp_GetRoleID(cblRoleProperty.Items[i].Text, config.applicationname).First().RoleId;
                    db.usp_SaveObjectRoles(oID, siteRoleId.ToString());
                }
            }
        }

        private void fillFields(int id)
        {
            var magazine = dbMekpoint.Magazines.Single(p => p.Id == id);

            // Fill fields with data
            tbName.Text = magazine.Name;
            tbHeadline.Text = magazine.Headline;
            tbDescription.Text = magazine.Description;
            rdpTime.SelectedDate = magazine.Date;

            hideAllFileUploads();

            if (!String.IsNullOrEmpty(magazine.Image))
            {
                pnlImage.Visible = true;
                hlImage.NavigateUrl = config.customerpath + "magazines/images/" + magazine.Image;
            }

            if (!String.IsNullOrEmpty(magazine.Pdf))
            {
                pnlPdfPart1.Visible = true;
                hlPdfPart1.NavigateUrl = config.customerpath + "magazines/pdfs/" + magazine.Pdf;
            }

            if (!String.IsNullOrEmpty(magazine.Pdf2))
            {
                pnlPdfPart2.Visible = true;
                hlPdfPart2.NavigateUrl = config.customerpath + "magazines/pdfs/" + magazine.Pdf2;
            }

            if (!String.IsNullOrEmpty(magazine.Pdf3))
            {
                pnlPdfPart3.Visible = true;
                hlPdfPart3.NavigateUrl = config.customerpath + "magazines/pdfs/" + magazine.Pdf3;
            }

            if (!String.IsNullOrEmpty(magazine.Html))
            {
                pnlHtml.Visible = true;
                hlHtml.NavigateUrl = config.customerpath + "magazines/htmls/" + magazine.Html;
            }
        }

        protected void rgMagazine_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("30"));
                PageSizeCombo.FindItemByText("30").Attributes.Add("ownerTableViewId", rgMagazine.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("60"));
                PageSizeCombo.FindItemByText("60").Attributes.Add("ownerTableViewId", rgMagazine.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("90"));
                PageSizeCombo.FindItemByText("90").Attributes.Add("ownerTableViewId", rgMagazine.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("120"));
                PageSizeCombo.FindItemByText("120").Attributes.Add("ownerTableViewId", rgMagazine.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void rgMagazine_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var magazines = (from o in dbMekpoint.Magazines
                             orderby o.Id descending
                             select o);

            rgMagazine.DataSource = magazines;
        }

        protected void rgMagazine_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridCommandItem)
            {
                try
                {
                    GridCommandItem cmditm = (GridCommandItem)e.Item;
                    LinkButton lbNew = (LinkButton)cmditm.Controls[0].FindControl("lbNew");

                    lbNew.Text = "<img class=\"textimage\" src=\"/Images/newobject.gif\" alt=\"Ny tidning\" />&nbsp;Ny tidning";
                }
                catch { }
            }
        }

        protected void rgMagazine_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "newitem":
                    showEditNew();
                    break;
                case "edititem":
                    showEditObject(int.Parse(rgMagazine.MasterTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString()));
                    break;
                case "viewarticles":
                    showArticleList(int.Parse(rgMagazine.MasterTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString()));
                    break;
                case "deleteitem":
                    var magazines = (from o in dbMekpoint.Magazines
                                     where o.Id == int.Parse(rgMagazine.MasterTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString())
                                     select o).ToList();
                    dbMekpoint.Magazines.DeleteAllOnSubmit(magazines);

                    foreach (var magazine in magazines)
                    {
                        var articles = (from o in dbMekpoint.MagazineArticles
                                        where o.Magazine == magazine
                                        select o).ToList();

                        foreach (var article in articles)
                        {
                            var objects = (from o in db.Objects
                                           where o.ObjectID == article.ObjectId
                                           select o).ToList();

                            foreach (var obj in objects)
                            {
                                obj.TrashedDate = DateTime.Now;
                            }
                        }

                        dbMekpoint.MagazineArticles.DeleteAllOnSubmit(articles);
                    }                     

                    dbMekpoint.SubmitChanges();
                    db.SubmitChanges();

                    rgMagazine.Rebind();

                    break;
            }
        }

        protected void rgArticleList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (!String.IsNullOrEmpty(hfMagazineId.Value))
            {
                List<int> articles = (from o in dbMekpoint.MagazineArticles
                                where o.Magazine_FK == int.Parse(hfMagazineId.Value)
                                select o.ObjectId).ToList<int>();

                var objects = (from o in db.Objects
                               where o.SiteID == config.siteID &&
                               articles.Contains(o.ObjectID) &&
                               !o.TrashedDate.HasValue
                               orderby o.Created descending
                               select o);

                rgArticleList.DataSource = objects;
            }
        }

        

        protected void rgArticleList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            
        }

        protected void rgArticleList_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridCommandItem)
            {
                try
                {
                    GridCommandItem cmditm = (GridCommandItem)e.Item;
                    LinkButton lbNew = (LinkButton)cmditm.Controls[0].FindControl("lbNew");

                    lbNew.Text = "<img class=\"textimage\" src=\"/Images/newobject.gif\" alt=\"Ny artikel\" />&nbsp;Ny artikel";
                }
                catch { }
            }
        }

        protected void rgArticleList_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "newitem":
                    createNewArticle(int.Parse(hfMagazineId.Value));
                    break;
                case "edititem":
                    showArticle(int.Parse(rgArticleList.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ObjectID"].ToString()));
                    break;
                case "deleteitem":
                    var articles = (from o in dbMekpoint.MagazineArticles
                                    where o.ObjectId == int.Parse(rgArticleList.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ObjectID"].ToString())
                                    select o).ToList();
                    dbMekpoint.MagazineArticles.DeleteAllOnSubmit(articles);

                    foreach (var article in articles)
                    {
                        var objs = (from o in db.Objects
                                    where o.ObjectID == article.ObjectId && 
                                    o.SiteID == config.siteID
                                    select o).ToList();

                        foreach (var obj in objs)
                        {
                            obj.TrashedDate = DateTime.Now;
                        }
                    }

                    dbMekpoint.SubmitChanges();
                    db.SubmitChanges();

                    showArticleList(int.Parse(hfMagazineId.Value));

                    break;
            }
        }

        protected void createNewArticle(int id)
        {
            AIM.Business.AimBusiness.Mekpoint.Website.AimContent.Object newObject = new AIM.Business.AimBusiness.Mekpoint.Website.AimContent.Object();

            newObject.SiteID = config.siteID;
            newObject.ObjectTypeID = 81;
            newObject.LangID = 1;
            newObject.ListID = _magazineID;
            newObject.VisibleInMenu = false;
            newObject.Created = DateTime.Now;
            newObject.LastChanged = DateTime.Now;
            newObject.SecureObject = 0;

            if (cbUsePublicationDate.Checked)
            {
                if (!calFrom.SelectedDate.HasValue)
                {
                    calFrom.SelectedDate = DateTime.Now;
                }

                newObject.PublishFrom = calFrom.SelectedDate;
                newObject.PublishTo = calTo.SelectedDate;
            }
            else
            {
                newObject.PublishFrom = null;
                newObject.PublishTo = null;
            }

            db.Objects.InsertOnSubmit(newObject);
            db.SubmitChanges();

            AIM.Business.AimBusiness.Mekpoint.Website.Mekpoint.MagazineArticle newArticle = new AIM.Business.AimBusiness.Mekpoint.Website.Mekpoint.MagazineArticle();

            newArticle.Magazine_FK = id;
            newArticle.ObjectId = newObject.ObjectID;
            newArticle.MagazineArticleType_FK = 1; // TODO: This might not be in the spec

            dbMekpoint.MagazineArticles.InsertOnSubmit(newArticle);
            dbMekpoint.SubmitChanges();

            showArticle(newObject.ObjectID);            
        }

        protected void fillMagazineFromFields(AIM.Business.AimBusiness.Mekpoint.Website.Mekpoint.Magazine magazine)
        {
            magazine.Name = tbName.Text;
            magazine.Headline = tbHeadline.Text;
            magazine.Description = tbDescription.Text;
            magazine.Date = rdpTime.SelectedDate.Value;

            if (fuImage.HasFile)
            {
                List<string> allowedTypes = new List<string>() { ".png", ".jpg", ".gif", ".jpeg" };
                string folder = HttpContext.Current.Server.MapPath(config.customerpath + "magazines/images/");
                string savedFilename = saveFile(fuImage, allowedTypes, folder);
                
                if (!String.IsNullOrEmpty(savedFilename))
                    magazine.Image = savedFilename;
            }

            if (fuPdfPart1.HasFile)
            {
                List<string> allowedTypes = new List<string>() { ".pdf" };
                string folder = HttpContext.Current.Server.MapPath(config.customerpath + "magazines/pdfs/");
                string savedFilename = saveFile(fuPdfPart1, allowedTypes, folder);

                if (!String.IsNullOrEmpty(savedFilename))
                    magazine.Pdf = savedFilename;
            }

            if (fuPdfPart2.HasFile)
            {
                List<string> allowedTypes = new List<string>() { ".pdf" };
                string folder = HttpContext.Current.Server.MapPath(config.customerpath + "magazines/pdfs/");
                string savedFilename = saveFile(fuPdfPart2, allowedTypes, folder);

                if (!String.IsNullOrEmpty(savedFilename))
                    magazine.Pdf2 = savedFilename;
            }

            if (fuPdfPart3.HasFile)
            {
                List<string> allowedTypes = new List<string>() { ".pdf" };
                string folder = HttpContext.Current.Server.MapPath(config.customerpath + "magazines/pdfs/");
                string savedFilename = saveFile(fuPdfPart3, allowedTypes, folder);

                if (!String.IsNullOrEmpty(savedFilename))
                    magazine.Pdf3 = savedFilename;
            }

            if (fuHtml.HasFile)
            {
                List<string> allowedTypes = new List<string>() { ".htm", ".html" };
                string folder = HttpContext.Current.Server.MapPath(config.customerpath + "magazines/htmls/");
                string savedFilename = saveFile(fuHtml, allowedTypes, folder);

                if (!String.IsNullOrEmpty(savedFilename))
                    magazine.Html = savedFilename;
            }
        }

        private string saveFile(FileUpload fileUpload, List<string> allowedTypes, string folder)
        {
            string filenamewithoutextention = Path.GetFileNameWithoutExtension(fileUpload.FileName);
            string extension = Path.GetExtension(fileUpload.FileName).ToLower();

            if (allowedTypes.Any(o => o.Equals(extension)))
            {
                if (File.Exists(folder + fileUpload.FileName))
                {
                    int i = 1;
                    while (File.Exists(folder + filenamewithoutextention + "-" + i.ToString() + extension))
                        i++;

                    filenamewithoutextention += "-" + i.ToString();
                }

                fileUpload.SaveAs(folder + filenamewithoutextention + extension);

                return filenamewithoutextention + extension;
            }

            return String.Empty;
        }

        /* ------------- */
        /* BUTTON CLICKS */
        /* ------------- */
        protected void lbArticleImageDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(hfArticleId.Value);

            if (db.Objects.Any(p => p.SiteID == config.siteID && p.ObjectID == id))
            {
                var article = db.Objects.Single(p => p.SiteID == config.siteID && p.ObjectID == id);

                article.Picture = String.Empty;

                db.SubmitChanges();

                pnlArticleImage.Visible = false;
            }
        }

        protected void btnArticleSave_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(hfArticleId.Value))
            {
                var article = (from o in db.Objects
                               where o.SiteID == config.siteID &&
                               o.ObjectID == int.Parse(hfArticleId.Value)
                               select o).Single();

                article.Title = tbArticleHeadline.Text;
                article.HTMLContent = reArticle.Content;
                article.IntroText = tbArticleIntroText.Text;
                article.LastChanged = DateTime.Now;

                if (fuArticleImage.HasFile)
                {
                    List<string> allowedTypes = new List<string>() { ".png", ".jpg", ".gif", ".jpeg" };
                    string folder = HttpContext.Current.Server.MapPath(config.customerpath + "magazines/images/");
                    string savedFilename = saveFile(fuArticleImage, allowedTypes, folder);

                    if (!String.IsNullOrEmpty(savedFilename))
                        article.Picture = savedFilename;
                }

                if (cbUsePublicationDate.Checked)
                {
                    if (!calFrom.SelectedDate.HasValue)
                    {
                        calFrom.SelectedDate = DateTime.Now;
                    }

                    article.PublishFrom = calFrom.SelectedDate;
                    article.PublishTo = calTo.SelectedDate;
                }
                else
                {
                    article.PublishFrom = null;
                    article.PublishTo = null;
                }

                db.SubmitChanges();

                saveRoles(article.ObjectID);

                showArticle(article.ObjectID);
            }
        }

        protected void btnArticleBack_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(hfMagazineId.Value))
            {
                showArticleList(int.Parse(hfMagazineId.Value));
            }
            else
            {
                showOverview();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(hfID.Value))
            {
                int id = int.Parse(hfID.Value);

                if (dbMekpoint.Magazines.Any(p => p.Id == id))
                {
                    var magazine = dbMekpoint.Magazines.Single(p => p.Id == id);

                    fillMagazineFromFields(magazine);

                    dbMekpoint.SubmitChanges();

                    rgMagazine.Rebind();

                    showEditObject(magazine.Id);
                }
            }
            else
            {
                var magazine = new AIM.Business.AimBusiness.Mekpoint.Website.Mekpoint.Magazine();

                fillMagazineFromFields(magazine);

                dbMekpoint.Magazines.InsertOnSubmit(magazine);
                dbMekpoint.SubmitChanges();

                rgMagazine.Rebind();

                showEditObject(magazine.Id);
            }
        }

        protected void lbImageDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(hfID.Value);

            if (dbMekpoint.Magazines.Any(p => p.Id == id))
            {
                var magazine = dbMekpoint.Magazines.Single(p => p.Id == id);

                magazine.Image = String.Empty;

                dbMekpoint.SubmitChanges();

                pnlImage.Visible = false;
            }
        }

        protected void lbPdfPart1Delete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(hfID.Value);

            if (dbMekpoint.Magazines.Any(p => p.Id == id))
            {
                var magazine = dbMekpoint.Magazines.Single(p => p.Id == id);

                magazine.Pdf = String.Empty;

                dbMekpoint.SubmitChanges();

                pnlPdfPart1.Visible = false;
            }
        }

        protected void lbPdfPart2Delete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(hfID.Value);

            if (dbMekpoint.Magazines.Any(p => p.Id == id))
            {
                var magazine = dbMekpoint.Magazines.Single(p => p.Id == id);

                magazine.Pdf2 = String.Empty;

                dbMekpoint.SubmitChanges();

                pnlPdfPart2.Visible = false;
            }
        }

        protected void lbPdfPart3Delete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(hfID.Value);

            if (dbMekpoint.Magazines.Any(p => p.Id == id))
            {
                var magazine = dbMekpoint.Magazines.Single(p => p.Id == id);

                magazine.Pdf3 = String.Empty;

                dbMekpoint.SubmitChanges();

                pnlPdfPart3.Visible = false;
            }
        }

        protected void lbHtmlDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(hfID.Value);

            if (dbMekpoint.Magazines.Any(p => p.Id == id))
            {
                var magazine = dbMekpoint.Magazines.Single(p => p.Id == id);

                magazine.Html = String.Empty;

                dbMekpoint.SubmitChanges();

                pnlHtml.Visible = false;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            hfID.Value = String.Empty;
            clearFields();
            showOverview();
        }
    }
}