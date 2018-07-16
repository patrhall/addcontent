using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Profile;
using System.Data.Linq;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Author: Andreas Ivarsson
/// Create date: 2008-05-12
/// 
/// Description:
/// All page preference
/// 
/// UPDATES:
/// -
/// </summary>
namespace AIM.General.UserControls
{
    public partial class PagePreference : AIM.Base.Modules.UserControl.General
    {
        public PagePreference()
        {
            UserControlVirtualPath = "/General/UserControls/PagePreference.ascx";
        }

        private int oID = int.MinValue;
        #region views
        private bool _viewUrl = true;
        private bool _viewVisibleInMenu = true;
        private UrlRewriteOptions _viewUrlRewrite = UrlRewriteOptions.NOTUSED;

        private bool _viewRoles = true;

        private bool _viewPublish = true;
        private bool _viewPublishFrom = true;
        private DateTime _setPublishFrom = DateTime.MinValue;
        private bool _viewPublishTo = true;
        private DateTime _setPublishTo = DateTime.MinValue;

        private bool _viewMeta = true;
        private bool _viewSlug = true;

        private bool _viewSaveButton = true;

        private bool _viewCreatedDatePanel = true;
        #endregion
        #region public
        /// <summary>
        /// Set if Roles should be shown
        /// </summary>
        public bool viewRoles
        {
            set { _viewRoles = value; }
        }

        /// <summary>
        /// Set if Url should be shown
        /// </summary>
        public bool viewUrl
        {
            set { _viewUrl = value; }
        }

        /// <summary>
        /// Set which type of url link to be viewed
        /// </summary>
        public UrlRewriteOptions viewUrlRewrite
        {
            set { _viewUrlRewrite = value; }
        }

        /// <summary>
        /// Set if publish should be shown
        /// </summary>
        public bool viewPublish
        {
            set { _viewPublish = value; }
        }

        /// <summary>
        /// Set if PublishFrom should be shown
        /// </summary>
        public bool viewPublishFrom
        {
            set { _viewPublishFrom = value; }
        }

        /// <summary>
        /// Set PublishTo should be shown
        /// </summary>
        public bool viewPublishTo
        {
            set { _viewPublishTo = value; }
        }

        /// <summary>
        /// Set if Meta should be shown
        /// </summary>
        public bool viewMeta
        {
            set { _viewMeta = value; }
        }

        /// <summary>
        /// Set if save button should be shown
        /// </summary>
        public bool viewSaveButton
        {
            set { _viewSaveButton = value; }
        }

        /// <summary>
        /// Set publish date
        /// </summary>
        public DateTime setPublishFrom
        {
            set { _setPublishFrom = value; }
        }

        /// <summary>
        /// Set publish end date
        /// </summary>
        public DateTime setPublishTo
        {
            set { _setPublishTo = value; }
        }
        /// <summary>
        /// Set if Url should be shown
        /// </summary>
        public bool viewCreatedDatePanel
        {
            set { _viewCreatedDatePanel = value; }
        }
        public enum UrlRewriteOptions { NOTUSED, NOMASTER, MASTER, DYNAMICANDMASTER };

        /// <summary>
        /// init site. View param and ObjectID has to be set before init
        /// </summary>
        public void init()
        {
            baseInit();

            if (_viewRoles)
                initRoles();
            else
                pnlRoles.Visible = false;

            if (_viewMeta)
                initMeta();
            else
                pnlMeta.Visible = false;

            if (_viewSlug)
                initSlug();


            if (_viewPublish)
                initPublish();
            else
                pnlPublish.Visible = false;

            if (_viewUrl)
                initUrl();
            else
                pnlUrl.Visible = false;

            if (_viewVisibleInMenu)
                initVisibleInMenu();
            else
                pnlShowInMenu.Visible = false;

            if (_viewCreatedDatePanel)
                initCreatedDate();
            else
                pnlCreatedDate.Visible = false;

            if (config.useAdminRoles && AdministratorAuthentication.IsUserAdmin && oID != int.MinValue)
                initUserAccessRights();
            else
                pnlAccessRights.Visible = false;

            btSave.Visible = _viewSaveButton;
        }

        public void setObjectID(int objectID)
        {
            oID = objectID;
        }

        /// <summary>
        /// Save preference, used if not savebutton is used
        /// </summary>
        /// <param name="objectID"></param>
        public void savePreference(int objectID, bool reloadAfterSave)
        {
            oID = objectID;
            save(reloadAfterSave);
        }

        public void savePreference(int objectID)
        {
            savePreference(objectID, true);
        }
        #endregion

        protected override void Page_Load()
        {
            try
            {
                Membership.ApplicationName = config.applicationname;
                ProfileManager.ApplicationName = config.applicationname;
                Roles.ApplicationName = config.applicationname;
            }
            catch { }

            setLanguage();
        }

        private void setLanguage()
        {
            lblHeadTitle.Text = Language.getValue("lblHeadTitle");
            lblHeadDescription.Text = Language.getValue("lblHeadDescription");
            lblUrlTitle.Text = Language.getValue("lblUrlTitle");
            lblUrlDescription.Text = Language.getValue("lblUrlDescription");
            lblSlugTitle.Text = Language.getValue("lblSlugTitle");
            lblRolesTitle.Text = Language.getValue("lblRolesTitle");
            lblRolesDescription.Text = Language.getValue("lblRolesDescription");
            lblPublishTitle.Text = Language.getValue("lblPublishTitle");
            lblPublishDescription.Text = Language.getValue("lblPublishDescription");
            lblMetaHeadTitle.Text = Language.getValue("lblMetaHeadTitle");
            lblMetaHeadDescription.Text = Language.getValue("lblMetaHeadDescription");
            lblMetaTitle.Text = Language.getValue("lblMetaTitle");
            lblMetaKeywords.Text = Language.getValue("lblMetaKeywords");
            lblMetaDescription.Text = Language.getValue("lblMetaDescription");
            btSave.Text = Language.getValue("btSave");
            cbUsePublicationDate.Text = Language.getValue("cbUsePublicationDate");
            lblPublishFrom.Text = Language.getValue("lblPublishFrom");
            lblPublishTo.Text = Language.getValue("lblPublishTo");
            lblShowInMenuTitle.Text = Language.getValue("lblShowInMenuTitle");
            lblShowInMenuDescription.Text = Language.getValue("lblShowInMenuDescription");
            cbShowInMenu.Text = Language.getValue("cbShowInMenu");
            lblCreatedDateTitle.Text = Language.getValue("lblCreatedDateTitle");
            lblCreatedDateDescription.Text = Language.getValue("lblCreatedDateDescription");
            lblCreatedDate.Text = Language.getValue("lblCreatedDate");
            lblAccessRightTitle.Text = Language.getValue("lblAccessRightTitle");
            lblAccessRightDescription.Text = Language.getValue("lblAccessRightDescription");
        }

        #region init
        /// <summary>
        /// Init the creation date for allowing editing for sorting purposes. Setting in preferences
        /// </summary>
        private void initCreatedDate()
        {
            if (oID != int.MinValue)
            {
                AIM.Business.General.Object theobject = (from linq_obj in db.Objects where linq_obj.ObjectID == oID select linq_obj).First<AIM.Business.General.Object>();
                calCreateDate.SelectedDate = theobject.Created;
            }
        }
        /// <summary>
        /// load roles
        /// </summary>
        private void initRoles()
        {
            if (config.applicationname != "")
            {                               
                string[] siteRoles = (from o in db.aspnet_Roles
                                      where o.aspnet_Application.ApplicationName == config.applicationname
                                      orderby o.RoleName
                                      select o.RoleName).ToArray();                                
             
                if (siteRoles.Length > 0)
                {
                    pnlRoles.Visible = true;

                    List<AIM.Business.General.usp_GetObjectRolesResult> objectRoles = db.usp_GetObjectRoles(oID).ToList<AIM.Business.General.usp_GetObjectRolesResult>();

                    for (int i = 0; i < siteRoles.Length; i++)
                    {
                        ListItem li = new ListItem(siteRoles[i], "list" + i);

                        cblRoleProperty.Items.Add(li);

                        foreach (AIM.Business.General.usp_GetObjectRolesResult objectRole in objectRoles)
                        {
                            if (siteRoles[i] == objectRole.RoleName)
                                cblRoleProperty.Items[i].Selected = true;
                        }
                    }
                }
            }            
        }
        private void initVisibleInMenu()
        {
            try
            {
                int temp = Convert.ToInt32(oID);

                AIM.Business.General.usp_GetObjectResult theObject = db.usp_GetObject(config.siteID, temp).SingleOrDefault<AIM.Business.General.usp_GetObjectResult>();
                //AIM.Business.General.usp_GetObjectResult theObject = db.usp_GetObject(config.siteID, oID).First<AIM.Business.General.usp_GetObjectResult>();
                cbShowInMenu.Checked = theObject.VisibleInMenu;
                pnlShowInMenu.Visible = true;
            }
            catch
            {
                pnlShowInMenu.Visible = false;
                cbShowInMenu.Checked = true;
            }
        }

        /// <summary>
        /// load meta
        /// </summary>
        private void initMeta()
        {
            if (oID != int.MinValue)
            {
                AIM.Business.General.usp_GetObjectResult theObject = db.usp_GetObject(config.siteID, oID).First<AIM.Business.General.usp_GetObjectResult>();
                tbMetaTitle.Text = theObject.MetaTitle;
                tbMetaKeywords.Text = theObject.MetaKeywords;
                tbMetaDescription.Text = theObject.MetaDescription;                
            }
        }
        /// <summary>
        /// load slug title
        /// </summary>
        private void initSlug()
        {
            if (oID != int.MinValue)
            {
                AIM.Business.General.Object theobject = (from linq_obj in db.Objects where linq_obj.ObjectID == oID select linq_obj).First<AIM.Business.General.Object>();
                tbSlug.Text = theobject.Slug;
            }
        }

        /// <summary>
        /// load publish properties
        /// </summary>
        private void initPublish()
        {
            Session.LCID = config.lcid_config;

            if (oID != int.MinValue)
            {

                AIM.Business.General.usp_GetObjectResult theObject = db.usp_GetObject(null, oID).First<AIM.Business.General.usp_GetObjectResult>();

                if (theObject.PublishFrom.HasValue || theObject.PublishTo.HasValue)
                {
                    if (theObject.PublishFrom.HasValue)
                    {
                        calFrom.SelectedDate = theObject.PublishFrom.Value;
                        calFrom.FocusedDate = theObject.PublishFrom.Value;
                    }

                    if (theObject.PublishTo.HasValue)
                    {
                        calTo.SelectedDate = theObject.PublishTo.Value;
                        calTo.FocusedDate = theObject.PublishTo.Value;
                    }

                    cbUsePublicationDate.Checked = true;
                }
                else
                    cbUsePublicationDate.Checked = false;
            }
            else
            {
                if (_setPublishFrom != DateTime.MinValue)
                    calFrom.SelectedDate = _setPublishFrom;

                if (_setPublishTo != DateTime.MinValue)
                    calTo.SelectedDate = _setPublishTo;

                //cbUsePublicationDate.Checked = true;
            }

            calFrom.Visible = _viewPublishFrom;
            lblPublishFrom.Visible = _viewPublishFrom;
            calTo.Visible = _viewPublishTo;
            lblPublishTo.Visible = _viewPublishTo;
        }

        private void initUrl()
        {            
            var theObject = db.Objects.Single(p => p.ObjectID == oID && p.SiteID == config.siteID);

            //Populate Link Tab
            string type = theObject.ObjectTypeID.Value.ToString();
            string key = theObject.ObjectKey;
            string lang = theObject.LangID.ToString();

            string objecttypeurl = db.SiteObjectTypes.Single(p => p.ObjectTypeID == theObject.ObjectTypeID && p.SiteID == config.siteID).ObjectTypeURL;

            string url = String.Empty;

            if (config.siteurl_config.EndsWith("/"))
                url += config.siteurl_config;
            else
                url += config.siteurl_config + "/";

            switch (_viewUrlRewrite)
            {
                case UrlRewriteOptions.NOTUSED:
                    url += "Default.aspx?oID=" + oID + "&lID=" + lang + "&tID=" + type + "&kID=" + key;
                    break;
                case UrlRewriteOptions.NOMASTER:
                    url += oID + "/Default.aspx?lID=" + lang + "&tID=" + type;
                    break;
                case UrlRewriteOptions.MASTER:
                    if (objecttypeurl != String.Empty)
                        url += oID + "/" + objecttypeurl + "?lID=" + lang + "&tID=" + type;
                    else
                        url += oID + "/Default.aspx?lID=" + lang + "&tID=" + type;
                    break;
                case UrlRewriteOptions.DYNAMICANDMASTER:
                    if (!String.IsNullOrEmpty(objecttypeurl))
                    {
                        if (!String.IsNullOrEmpty(theObject.Slug))
                            url += theObject.Slug;
                        else
                            url += oID + "/" + objecttypeurl;
                    }
                    break;
            }

            hpURL.Text = url;
            hpURL.NavigateUrl = url;
        }

        private void initUserAccessRights()
        {            
            cblObjectUserAccessRight.DataSource = (from o in db.Roles
                                                   where !o.SiteID.HasValue ||
                                                   o.SiteID == config.siteID
                                                   select o);
            cblObjectUserAccessRight.DataValueField = "ID";
            cblObjectUserAccessRight.DataTextField = "RoleName";
            cblObjectUserAccessRight.DataBind();

            foreach(var item in db.Object_X_AdminRoles.Where(p => p.ObjectId == oID))
                foreach (ListItem li in cblObjectUserAccessRight.Items)
                {
                    if (item.RoleId.ToString() == li.Value)
                        li.Selected = true;
                }

            //superuser has always access
            cblObjectUserAccessRight.Items[0].Selected = true;
            cblObjectUserAccessRight.Items[0].Enabled = false;
        }

        #endregion
        #region events
        protected void btSave_Clicked(object sender, EventArgs e)
        {
            save(true);
        }
        #endregion
        #region save
        /// <summary>
        /// Save everything
        /// </summary>
        private void save(bool reloadAfterSave)
        {
            if (_viewRoles)
                saveRoles();

            if (_viewPublish)
                savePublishDates();

            if (_viewMeta)
                saveMeta();

            if (_viewVisibleInMenu)
                saveVisibleInMenu();
            
            if(_viewSlug)
                saveSlug();

            if (_viewCreatedDatePanel)
                saveCreatedDate();

            if (config.useAdminRoles && AdministratorAuthentication.IsUserAdmin && pnlAccessRights.Visible)
                saveUserAccessRoles();            

            if (reloadAfterSave)
                Response.Redirect(Request.Url.AbsoluteUri);            
        }

        private void saveCreatedDate()
        {
            if (calCreateDate.SelectedDate.HasValue && oID != int.MinValue)
            {
                AIM.Business.General.Object thebject = (from linq_obj in db.Objects where linq_obj.ObjectID == oID select linq_obj).First<AIM.Business.General.Object>();
                thebject.Created = calCreateDate.SelectedDate.Value;
                db.SubmitChanges();
            }
        }

        private void saveSlug()
        {

            AIM.Business.General.Object thebject = (from linq_obj in db.Objects where linq_obj.ObjectID == oID select linq_obj).First<AIM.Business.General.Object>();
            thebject.Slug = trimSlashes(tbSlug.Text);
            try
            {
                db.SubmitChanges();
            }
            catch
            {
            }
        }

        private string trimSlashes(string str)
        {
            while (str.StartsWith("/"))
            {
                str = str.Substring(1);
            }

            while (str.EndsWith("/"))
            {
                str = str.Substring(0, str.Length - 1);
            }
            return str;
        }

        private void savePublishDates()
        {
            if (!cbUsePublicationDate.Checked)
                db.usp_SavePublishDates(oID, null, null);
            else
            {
                if (!calFrom.SelectedDate.HasValue)
                {
                    calFrom.SelectedDate = DateTime.Now;
                }
                
                //Convert.ToDateTime("1999-12-04");
                //Convert.ToDateTime("2099-12-04");                

                AIM.Business.General.Object theNewsObject = (from linq_obj in db.Objects where linq_obj.ObjectID == oID select linq_obj).First<AIM.Business.General.Object>();
                            
                theNewsObject.PublishFrom = calFrom.SelectedDate;
                theNewsObject.PublishTo = calTo.SelectedDate;

                db.SubmitChanges();                
            }
        }
        private void saveRoles()
        {
            db.usp_DeleteObjectRoles(oID);

            for (int i = 0; i < cblRoleProperty.Items.Count; i++)
            {
                if (cblRoleProperty.Items[i].Selected == true)
                {
                    Guid siteRoleId = db.usp_GetRoleID(cblRoleProperty.Items[i].Text, config.applicationname).First<AIM.Business.General.usp_GetRoleIDResult>().RoleId;
                    db.usp_SaveObjectRoles(oID, siteRoleId.ToString());
                }
            }
        }
        private void saveMeta()
        {
            db.usp_SaveTextMeta(tbMetaTitle.Text, tbMetaKeywords.Text, tbMetaDescription.Text, oID);
        }
        private void saveVisibleInMenu()
        {
            db.usp_SaveObjectVisibleInMenu(oID, cbShowInMenu.Checked);
        }

        private void saveUserAccessRoles()
        {
            var allObjects = (from o in db.Object_X_AdminRoles
                              where o.ObjectId == oID
                              select o);

            db.Object_X_AdminRoles.DeleteAllOnSubmit(allObjects);
            db.SubmitChanges();

            foreach (ListItem li in cblObjectUserAccessRight.Items)
                if (li.Selected)
                {
                    var x = new Business.General.Object_X_AdminRole();
                    db.Object_X_AdminRoles.InsertOnSubmit(x);
                    x.ObjectId = oID;
                    x.RoleId = Convert.ToInt32(li.Value);
                }

            db.SubmitChanges();
        }
        #endregion
        #region help functions
        /// <summary>
        /// convert string to enum
        /// </summary>
        /// <param name="urlRewriteOption"></param>
        /// <returns></returns>
        public static UrlRewriteOptions StringToUrlRewriteOptions(string urlRewriteOption)
        {
            UrlRewriteOptions option;

            switch (urlRewriteOption)
            {
                case "NOTUSED":
                    option = PagePreference.UrlRewriteOptions.NOTUSED;
                    break;
                case "NOMASTER":
                    option = PagePreference.UrlRewriteOptions.NOMASTER;
                    break;
                case "MASTER":
                    option = PagePreference.UrlRewriteOptions.MASTER;
                    break;
                case "DYNAMICANDMASTER":
                    option = PagePreference.UrlRewriteOptions.DYNAMICANDMASTER;
                    break;
                default:
                    throw new Exception("AIM.General.UserControls.PagePreference Exception: StringToUrlRewriteOptions(string urlRewriteOption) converting exception");
            }

            return option;
        }
        #endregion
    }
}