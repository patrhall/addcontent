using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using AIM.General.UserControls;
using System.Xml.Linq;
using System.Data.Linq;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace AimApp
{
    public partial class EditPage : AIM.Base.Modules.Page.AimApp
    {

        private int oID;
        AIM.General.UserControls.PagePreference pref;

        protected override void Page_Load()
        {
            oID = int.Parse(Request["oID"]);

            if (config.useAdminRoles && AdministratorAuthentication.IsUserAdmin)
                editor1.ToolsFile = "/App_Data/RadControls/Editor/ToolsFile_Admin.xml";

            AIM.Base.Classes.RadHandler.RadEditor_LoadEditorPreference(ref editor1, config);

            //Används objectimages
            if (Convert.ToBoolean(Preference.getValue("content", "objectimage", "used")))
                preferenceViewObjectImage();

            if (!IsPostBack)
                fillObjectData();

            loadPagePreference();
            setLanguage();
        }

        private void setLanguage()
        {
            lbPreference.Text = "<img src='/Images/edit.gif' alt='" + Language.getValue("lbPreference") + "' /> " + Language.getValue("lbPreference");
            bRoleConfirm.Text = Language.getValue("bRoleConfirm");
        }

        /// <summary>
        /// Load old data
        /// </summary>
        private void fillObjectData()
        {
            AIM.Business.AimEdit.Object theObject = (from linq_object in db.Objects where linq_object.ObjectID == oID && linq_object.SiteID == config.siteID select linq_object).First<AIM.Business.AimEdit.Object>();

            if (!IsPostBack && config.secureEditing != null && config.secureEditing != "")
                editor1.Content = theObject.HTMLContent.Replace(config.secureEditing, config.customerpath + config.secureEditing);
            else
                editor1.Content = theObject.HTMLContent;
        }

        private void loadPagePreference()
        {
            if (!AdministratorAuthentication.IsUserAdmin)
            {
                phPagePreference.Visible = false;
                lbPreference.Visible = false;
            }

            Control controlPagePreference = LoadControl("/General/UserControls/PagePreference.ascx");
            pref = (AIM.General.UserControls.PagePreference)controlPagePreference;
            pref.setObjectID(oID);
            phPagePreference.Controls.Add(pref);
        }

        /// <summary>
        /// Init pagepreference
        /// </summary>
        private void initPagePreference()
        {
            int sitecreateyear = db.Sites.Single(p => p.SiteID == config.siteID).Created.Year;

            pref.viewUrl = Boolean.Parse(Preference.getValue("content", "directurl", "used"));

            PagePreference.UrlRewriteOptions selectedDirectUrl = PagePreference.StringToUrlRewriteOptions(Preference.getValue("content", "directurl", "urlrewrite"));

            if (sitecreateyear < 2010 && selectedDirectUrl == PagePreference.UrlRewriteOptions.DYNAMICANDMASTER)
                selectedDirectUrl = PagePreference.StringToUrlRewriteOptions(Preference.getValue("content", "directurl_before2010", "urlrewrite"));

            pref.viewUrlRewrite = selectedDirectUrl;

            //Load preference
            pref.init();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbPreference_OnClick(object sender, EventArgs e)
        {
            phPagePreference.Visible = true;
            pnlContent.Visible = false;
            initPagePreference();
        }

        private void preferenceViewObjectImage()
        {
            pnlObjectImage.Visible = true;

            //Find file on the current objectID
            string sourceDir = Server.MapPath(config.customerpath + "objectimages/");
            string[] fileEntries = Directory.GetFiles(sourceDir);
            foreach (string fileName in fileEntries)
            {
                string strExt = fileName.Substring(fileName.LastIndexOf(".")).ToLower();
                string fileNameWithoutExt = fileName.Substring(0, fileName.LastIndexOf(".")).ToLower();
                fileNameWithoutExt = fileNameWithoutExt.Substring(fileName.LastIndexOf("\\"));
                fileNameWithoutExt = fileNameWithoutExt.Replace("\\", "");
                if (fileNameWithoutExt == oID.ToString())
                {
                    imgObjectImage.Visible = true;
                    imgObjectImage.ImageUrl = config.customerpath + "objectimages/" + fileNameWithoutExt + strExt;
                }
            }
        }

        protected void editor1_SubmitClicked(object sender, System.EventArgs e)
        {
            string text = editor1.Content;

            //SecureEditing needs a new hyperlink
            if (config.secureEditing != null && config.secureEditing != "")
                text = text.Replace(config.customerpath + config.secureEditing, config.secureEditing);

            AIM.Business.AimEdit.Object theObject = (from linq_object in db.Objects where linq_object.ObjectID == oID && linq_object.SiteID == config.siteID select linq_object).First<AIM.Business.AimEdit.Object>();

            theObject.LastChanged = DateTime.Now;

            string oldText = theObject.HTMLContent;

            theObject.HTMLContent = text;

            db.SubmitChanges();

            // Send a mail if the content has been changed
            if (!oldText.Equals(text))
            {
                //sendMail();             
            }

            //If we should upload object image
            if (Convert.ToBoolean(Preference.getValue("content", "objectimage", "used")) && fuObjectImage.HasFile)
            {
                string strExt = fuObjectImage.PostedFile.FileName.Substring(fuObjectImage.PostedFile.FileName.LastIndexOf(".")).ToLower();
                fuObjectImage.SaveAs(Server.MapPath(config.customerpath + "objectimages/") + ID + strExt);
            }

            //If we should update the template
            if (Convert.ToBoolean(Preference.getValue("content", "templates", "used")))
            {
                int templateID = Convert.ToInt32(rblTemplates.SelectedValue);
                db.usp_Object_SaveTemplate(oID, templateID);
            }

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditText.aspx?ID=" + Request["ID"] + "&ObjectKey=" + Request["ObjectKey"]);
        }

        private void sendMail()
        {
            string from_email = "noreply@aimit.se";

            bool isBodyHtml = false;

            AIM.Business.AimEdit.Site theSite = (from linq_site in db.Sites
                                                 where linq_site.SiteID == config.siteID
                                                 select linq_site).First<AIM.Business.AimEdit.Site>();

            string subject = Language.getValue("MailNotificationSubject") + theSite.SiteName;

            AIM.Business.AimEdit.Admin theAdmin = (from linq_admin in db.Admins
                                                   where linq_admin.ID == config.adminuserid &&
                                                   linq_admin.siteID == config.siteID
                                                   select linq_admin).First<AIM.Business.AimEdit.Admin>();

            string from_name = theAdmin.firstname + " " + theAdmin.lastname;

            String body = Language.getValue("MailNotificationUser") + from_name;

            AIM.Business.AimEdit.Object theObject = (from linq_object in db.Objects
                                                     where linq_object.ObjectID == oID &&
                                                     linq_object.SiteID == config.siteID
                                                     select linq_object).First<AIM.Business.AimEdit.Object>();

            body += Language.getValue("MailNotificationHasChangedPage") + theObject.Title + ".";

            IEnumerable<string> emails = (from linq_emails in db.Admins
                                          where linq_emails.siteID == config.siteID &&
                                                linq_emails.mailNotifications == 1 &&
                                                linq_emails.email != null &&
                                                linq_emails.email != ""
                                          select linq_emails.email).AsEnumerable<string>();

            foreach (string to_email in emails)
            {
                AimResources.Mail.sendMail(to_email, from_email, isBodyHtml, subject, body);
            }
        }
    }
}