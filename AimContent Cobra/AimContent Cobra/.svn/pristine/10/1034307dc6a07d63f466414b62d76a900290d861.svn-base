using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.Linq;
using System.Collections.Generic;

namespace AimEdit
{
    public partial class EditDirectLink : AIM.Base.Modules.Page.AimEdit
    {
        AIM.General.UserControls.PagePreference pref;
        int objectID;

        protected override void Page_Load()
        {
            objectID = int.Parse(Request["oID"]);
            
            if (!IsPostBack)
            {
                setLanguage();
             
                AIM.Business.AimEdit.usp_GetObjectResult theObject = db.usp_GetObject(config.siteID, objectID).First<AIM.Business.AimEdit.usp_GetObjectResult>();

                tbLink2.Text = theObject.HTMLContent;

                if (!String.IsNullOrEmpty(theObject.LinkTarget))
                    rblListType.SelectedValue = theObject.LinkTarget;
                else
                    rblListType.SelectedIndex = 0;
                
                ddLinkType.SelectedValue = theObject.SecureObject.ToString();
                //Set correct text on relative link listitem.
                ddLinkType.Items[2].Text = Language.getValue("dropDownRelativeLinkText");
                //Set correct text on savebutton.
                btnSave.Text = Language.getValue("saveButtonText");
                //Hide relative link option for production.
                ddLinkType.Items.RemoveAt(2);//*/

                if (theObject.Category == 1)
                {
                    rblListType.SelectedValue = "_Rel";
                }

                if (config.useAdminRoles && !db.AdminRoles.Any(p => p.AdminID == config.adminuserid && p.Role.RoleName.ToLower() == AIM.Base.Globals.Configuration.SuperAdminRole))
                    btnSave.Enabled = false;
            }            

            loadPagePreference();
            
        }

        private void setLanguage()
        {
            lbPreference.Text = "<img src='/Images/edit.gif' alt='" + Language.getValue("lbPreference") + "' /> " + Language.getValue("lbPreference");

            if (!IsPostBack)
            {
                rblListType.Items.Clear();
                rblListType.Items.Add(new ListItem(Language.getValue("rblListType_Extern_Text"), "_blank"));
                rblListType.Items.Add(new ListItem(Language.getValue("rblListType_Internal_Text"), "_top"));
                
                //090708
                AIM.Base.Classes.Preference Preference;
                Preference = AIM.Base.Classes.Preference.getPreference(config.customerpath);

                if (Convert.ToBoolean(Convert.ToBoolean(Preference.getValue("relativelinks", "relative_visible", "used"))))
                {
                    //090707
                    rblListType.Items.Add(new ListItem(Language.getValue("rblListType_Relative_Text"), "_Rel"));
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            AIM.Business.AimEdit.Object theObject = (from linq_obj in db.Objects where linq_obj.ObjectID == objectID select linq_obj).First<AIM.Business.AimEdit.Object>();

            //New feature. Relative link.

            //If user selected relative link.
            if (ddLinkType.SelectedValue == "2")
                //If linkurl is not empty string.
                if (!String.IsNullOrEmpty(tbLink2.Text))
                    //If the linktext is missing '/' at the start.
                    if (tbLink2.Text[0] != '/')
                        //Add '/'.
                        tbLink2.Text = "/" + tbLink2.Text;

            theObject.HTMLContent = tbLink2.Text;
            theObject.LinkTarget = rblListType.SelectedValue;
            theObject.SecureObject = Convert.ToByte(ddLinkType.SelectedValue);

            //090707
            if (rblListType.SelectedItem.Text.ToString() == Language.getValue("rblListType_Relative_Text"))
            {
                theObject.Category = 1;
                theObject.LinkTarget = "_top";
            }
            else
            {
                theObject.Category = null;
            }

            db.SubmitChanges();
        }

        private void loadPagePreference()
        {
            Control controlPagePreference = LoadControl("/General/UserControls/PagePreference.ascx");
            pref = (AIM.General.UserControls.PagePreference)controlPagePreference;
            pref.setObjectID(objectID);
            phPagePreference.Controls.Add(pref);
        }

        protected void lbPreference_OnClick(object sender, EventArgs e)
        {
            phPagePreference.Visible = true;
            pnlContent.Visible = false;
            initPagePreference();
        }

        /// <summary>
        /// Init pagepreference
        /// </summary>
        private void initPagePreference()
        {
            pref.viewUrl = false;
            pref.viewMeta = false;                        

            //Load preference
            pref.init();

        }
    }
}
