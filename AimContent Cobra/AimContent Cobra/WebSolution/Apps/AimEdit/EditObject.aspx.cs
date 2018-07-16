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
using AIM.General.UserControls;

namespace AimEdit
{
    public partial class EditObject : AIM.Base.Modules.Page.AimEdit
    {
        private int objectID;
        AIM.General.UserControls.PagePreference pref;

        protected override void Page_Load()
        {
            objectID = Convert.ToInt32(Request["oID"]);

            if (!IsPostBack)
            {
                AIM.Business.AimEdit.usp_GetObjectResult theObject = db.usp_GetObject(config.siteID, objectID).First<AIM.Business.AimEdit.usp_GetObjectResult>();

                List<AIM.Business.AimEdit.usp_GetSiteObjectTypeResult> siteObjectTypes = db.usp_GetSiteObjectType(config.siteID).ToList<AIM.Business.AimEdit.usp_GetSiteObjectTypeResult>();

                rblNodeProperty.DataSource = siteObjectTypes;                
                rblNodeProperty.DataValueField = "ObjectTypeID";
                rblNodeProperty.DataBind();

                rblNodeProperty.SelectedValue = theObject.ObjectTypeID.Value.ToString();                
            }

            loadPagePreference();
            setLanguage();
        }

        private void setLanguage()
        {
            lbPreference.Text = "<img src='/Images/edit.gif' alt='" + Language.getValue("lbPreference") + "' /> " + Language.getValue("lbPreference");

            if (!IsPostBack)
                foreach (ListItem li in rblNodeProperty.Items)
                    li.Text = Language.getValue("objecttype" + li.Value);
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
            pref.viewUrl = Boolean.Parse(Preference.getValue("content", "directurl", "used"));

            int sitecreateyear = db.Sites.Single(p => p.SiteID == config.siteID).Created.Year;

            PagePreference.UrlRewriteOptions selectedDirectUrl = PagePreference.StringToUrlRewriteOptions(Preference.getValue("content", "directurl", "urlrewrite"));

            if (sitecreateyear < 2010 && selectedDirectUrl == PagePreference.UrlRewriteOptions.DYNAMICANDMASTER)
                selectedDirectUrl = PagePreference.StringToUrlRewriteOptions(Preference.getValue("content", "directurl_before2010", "urlrewrite"));

            pref.viewUrlRewrite = selectedDirectUrl;

            //Load preference
            pref.init();

        }

        protected void bNodeProperty_Clicked(object sender, System.EventArgs e)
        {
            db.usp_SaveObjectType(int.Parse(rblNodeProperty.SelectedValue), objectID);

            List<AIM.Business.AimEdit.usp_GetObjectResult> listObject = db.usp_GetObject(config.siteID, objectID).ToList<AIM.Business.AimEdit.usp_GetObjectResult>();

            DataRow drObject = AimResources.Collections.List2DataTable<AIM.Business.AimEdit.usp_GetObjectResult>(listObject).Rows[0];

            //Redirect to right objecttype
            Response.Redirect(getAimContentUrl(drObject));
        }

        /// <summary>
        /// THIS IS DUPLICATED FUNCTION. ADD THIS ALSO IN CONTENTTREEVIEW.ASCX
        /// </summary>
        /// <param name="objectRow"></param>
        /// <returns></returns>
        private string getAimContentUrl(DataRow objectRow)
        {
            // THIS IS DUPLICATED FUNCTION. ADD THIS ALSO IN CONTENTTREEVIEW.ASCX

            string url = String.Empty;

            try
            {
                //Get objecttype for object
                var objectType = (from linq_objecttype in db.ObjectTypes
                                  where linq_objecttype.ObjectTypeID == int.Parse(objectRow["ObjectTypeID"].ToString())
                                  select linq_objecttype).Single();

                url = objectType.AimContentURL;

                //Add $-replacements here
                url = url.Replace("$ObjectID$", objectRow["ObjectID"].ToString());
            }
            catch
            {
                try
                {
                    // should not go here but to ease the error message
                    url = "/Apps/AimEdit/EditObject.aspx?oID=" + objectRow["ObjectID"].ToString();
                }
                catch { }
            }

            return url;
        }
    }
}
