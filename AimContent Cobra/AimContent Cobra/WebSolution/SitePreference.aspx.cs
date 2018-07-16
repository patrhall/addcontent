using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Globalization;

namespace WebSolution
{
    public partial class SitePreference : AIM.Base.Modules.Page.WebSolution
    {
        protected override void Page_Load()
        {
            lblMetaMessage.Visible = false;

            setLanguage();
            initPrePreference();

            if (!IsPostBack)
            {
                initMeta();
                initAdminRoles();
            }
        }

        private void setLanguage()
        {
            lblMetaHeadTitle.Text = Language.getValue("lblMetaHeadTitle");
            lblMetaHeadDescription.Text = Language.getValue("lblMetaHeadDescription");
            lblMetaTitle.Text = Language.getValue("lblMetaTitle");
            lblMetaKeywords.Text = Language.getValue("lblMetaKeywords");
            lblMetaDescription.Text = Language.getValue("lblMetaDescription");

            btMetaSave.Text = Language.getValue("btMetaSave");
            lblMetaMessage.Text = Language.getValue("lblMetaMessage");

            lblPrePreferenceTitle.Text = Language.getValue("lblPrePreferenceTitle");
            lblPrePreferenceDescription.Text = Language.getValue("lblPrePreferenceDescription");
            lblPrePreferenceSiteUrl.Text = Language.getValue("lblPrePreferenceSiteUrl");
            lblPrePreferenceMaxFilesize.Text = Language.getValue("lblPrePreferenceMaxFilesize");
            lblPrePreferenceLcid.Text = Language.getValue("lblPrePreferenceLcid");
            lblPrePreferenceUseAdminRoles.Text = Language.getValue("lblPrePreferenceUseAdminRoles");
            lblPrePreferenceSecureEditing.Text = Language.getValue("lblPrePreferenceSecureEditing");
            lblPrePreferenceLiveStat.Text = Language.getValue("lblPrePreferenceLiveStat");
            lblPrePreferenceID.Text = Language.getValue("lblPrePreferenceID");

            lblAdminRolesTitle.Text = Language.getValue("lblAdminRolesTitle");
            lblAdminRolesDescription.Text = Language.getValue("lblAdminRolesDescription");

            btSaveAdminRoles.Text = Language.getValue("btSaveAdminRoles");
        }

        private void initMeta()
        {
            AIM.Business.WebSolution.usp_GetObjectResult theObject = db.usp_GetObject(config.siteID, config.startpageID).First<AIM.Business.WebSolution.usp_GetObjectResult>();

            tbMetaTitle.Text = theObject.MetaTitle;
            tbMetaKeywords.Text = theObject.MetaKeywords;
            tbMetaDescription.Text = theObject.MetaDescription;
        }

        private void initAdminRoles()
        {
            //TODO: temp hiding
            //pnlAdminRoles.Visible = false;
            //return;

            pnlAdminRoles.Visible = (config.useAdminRoles && AdministratorAuthentication.IsUserAdmin);

            var siteModules = (from o in db.SiteModules
                               where o.SiteID == config.siteID
                               orderby o.ModuleID
                               select o);

            rptModules.DataSource = siteModules;
            rptModules.DataBind();
        }

        private void initPrePreference()
        {
            lblPrePreferenceSiteUrlValue.Text = config.siteurl_config;

            lblPrePreferenceMaxFilesizeValue.Text = Convert.ToString(config.maxuploadbytesize / (1024 * 1024)) + " Mb";

            CultureInfo culture = new CultureInfo(config.lcid_config);
            lblPrePreferenceLcidValue.Text = culture.NativeName;

            lblPrePreferenceUseAdminRolesValue.Text = (config.useAdminRoles ? Language.getValue("yes") : Language.getValue("no"));
            lblPrePreferenceSecureEditingValue.Text = (!String.IsNullOrEmpty(config.secureEditing) ? Language.getValue("yes") : Language.getValue("no"));            

            if (!String.IsNullOrEmpty(config.liveStats))
                lblPrePreferenceLiveStatValue.Text = "<a href='" + config.liveStats + "' target='_blank'>" + Language.getValue("lblPrePreferenceLiveStatValue_true") + "</a>";
            else
                lblPrePreferenceLiveStatValue.Text = Language.getValue("lblPrePreferenceLiveStatValue_false");

            lblPrePreferenceIDValue.Text = config.siteID.ToString();
        }

        protected void btMetaSave_OnClick(object sender, EventArgs e)
        {
            var theObject = db.Objects.Single(p => p.ObjectID == config.startpageID);

            theObject.MetaTitle = tbMetaTitle.Text;
            theObject.MetaKeywords = tbMetaKeywords.Text;
            theObject.MetaDescription = tbMetaDescription.Text;

            db.SubmitChanges();

            lblMetaMessage.Visible = true;
        }

        protected void rptModules_Bound(object sender, RepeaterItemEventArgs e)
        {
            var data = (AIM.Business.WebSolution.SiteModule)e.Item.DataItem;

            ((Label)e.Item.FindControl("lblModule")).Text = Language.getValue("module" + data.ModuleID);

            ((HiddenField)e.Item.FindControl("hfModuleID")).Value = data.ModuleID.ToString();

            var cblAdminRoles = (CheckBoxList)e.Item.FindControl("cblAdminRoles");
            cblAdminRoles.DataSource = (from o in db.Roles
                                        where !o.SiteID.HasValue ||
                                        o.SiteID == config.siteID
                                        select o);
            cblAdminRoles.DataValueField = "ID";
            cblAdminRoles.DataTextField = "RoleName";
            cblAdminRoles.DataBind();

            // set access
            foreach (ListItem item in cblAdminRoles.Items)
            {
                item.Selected = (from o in db.Roles_X_Modules
                                 where o.SiteID == config.siteID &&
                                 o.Module_FK == data.ModuleID &&
                                 o.Roles_FK == Convert.ToInt32(item.Value)
                                 select o).Any();
            }

            //superuser has always access
            cblAdminRoles.Items[0].Selected = true;
            cblAdminRoles.Items[0].Enabled = false;
        }

        protected void btSaveAdminRoles_Click(object sender, EventArgs e)
        {
            // Remove all previous roles assignments for site
            var allObjects = (from o in db.Roles_X_Modules
                                    where o.SiteID == config.siteID
                                    select o);

                db.Roles_X_Modules.DeleteAllOnSubmit(allObjects);
                db.SubmitChanges();

            foreach (RepeaterItem module in rptModules.Items)
            {
                var cblAdminRoles = (CheckBoxList)module.FindControl("cblAdminRoles");
                var moduleID = Convert.ToInt32(((HiddenField)module.FindControl("hfModuleID")).Value);
                
                foreach (ListItem item in cblAdminRoles.Items)
                {
                    if (item.Selected && item.Text != "Superuser")
                    {
                        var x = new AIM.Business.WebSolution.Roles_X_Module();
                        db.Roles_X_Modules.InsertOnSubmit(x);
                        x.Roles_FK = Convert.ToInt32(item.Value);
                        x.SiteID = config.siteID;
                        x.Module_FK = moduleID;
                        db.SubmitChanges();
                    }
                }
            }  
        }
    }
}
