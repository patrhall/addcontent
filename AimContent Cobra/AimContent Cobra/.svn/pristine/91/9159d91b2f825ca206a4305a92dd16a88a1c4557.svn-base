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
using System.Collections.Generic;

namespace AimEdit
{
    public partial class PickLanguage : AIM.Base.Modules.Page.AimEdit
    {
        private int objectID;
        AIM.General.UserControls.PagePreference pref;

        protected override void Page_Load()
        {
            objectID = Convert.ToInt32(Request["oID"]);

            if (!IsPostBack)
            {
                AIM.Business.AimEdit.usp_GetObjectResult theObject = db.usp_GetObject(config.siteID, objectID).First<AIM.Business.AimEdit.usp_GetObjectResult>();

                List<AIM.Business.AimEdit.Language> siteObjectTypes = (from o in db.SiteLanguages where o.SiteID == config.siteID select o.Language).ToList<AIM.Business.AimEdit.Language>();

                rblLanguage.DataSource = siteObjectTypes;
                rblLanguage.DataValueField = "LangID";
                rblLanguage.DataBind();

                rblLanguage.SelectedValue = theObject.HTMLContent;
            }

            loadPagePreference();
            setLanguage();
        }

        private void setLanguage()
        {
            lbPreference.Text = "<img src='/Images/edit.gif' alt='" + Language.getValue("lbPreference") + "' /> " + Language.getValue("lbPreference");

            if (!IsPostBack)
                foreach (ListItem li in rblLanguage.Items)
                    li.Text = Language.getValue("language" + li.Value);
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

        protected void bLanguage_Clicked(object sender, System.EventArgs e)
        {
            AIM.Business.AimEdit.Object theObject = (from o in db.Objects where o.ObjectID == int.Parse(Request["oID"]) select o).First<AIM.Business.AimEdit.Object>();
            theObject.HTMLContent = rblLanguage.SelectedValue;
            db.SubmitChanges();
        }
    }
}
