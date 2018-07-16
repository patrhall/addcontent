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
using Telerik.Web.UI;
using AIM.Business.WebSolution;
using AIM.Base.Classes;

namespace WebSolution
{
    public partial class Site : System.Web.UI.MasterPage
    {
        private AIM.Base.Classes.Config config;
        AIM.Business.WebSolution.DataObjectsDataContext db = new DataObjectsDataContext(AIM.Business.DataManager.ConnectionString);
        private AIM.Base.Classes.LanguageHandler _language;

        protected void Page_Load(object sender, EventArgs e)
        {
            config = new AIM.Base.Classes.Config();            
            config.GetConfig();

            _language = AIM.Base.Classes.LanguageHandler.getLanguage(config.adminuserid, "/Site.Master");

            loadInfo();

            if (!IsPostBack)
                loadMenu();
            else
                loadLeftContent();    
        }

        private void loadInfo()
        {
            lblAdminUserName.Text = _language.getValue("lblAdminUserName") + " " + (from linq_admin in db.Admins where linq_admin.ID == config.adminuserid select linq_admin.firstname + " " + linq_admin.lastname).ToList<string>()[0];
            lblSiteName.Text = _language.getValue("lblSiteName") + " " + (from linq_site in db.Sites where linq_site.SiteID == config.siteID select linq_site.SiteName).ToList<string>()[0];

            //Shows or hides edit Styles
            AIM.Base.Classes.Preference Preference;
            Preference = AIM.Base.Classes.Preference.getPreference(config.customerpath);

            if (!Convert.ToBoolean(Convert.ToBoolean(Preference.getValue("sitestylesetting", "style_visible", "used"))))
            {
                tdSiteStyle.Style.Add("display", "false");
                tdSiteStyle.Visible = false;                
            }
            else
            {
                tdSiteStyle.Style.Remove("display");
                tdSiteStyle.Visible = true;                
            }
        }

        private void loadMenu()
        {
            string moduleimagespath = "/Images/moduleicons/";

            ddModules.Items.Clear();

            RadComboBoxItem moduleitem;            

            List<Module> sitemodules = (from linq_module in db.Modules 
                                        from linq_sitemodule in linq_module.SiteModules
                                        where linq_sitemodule.SiteID == config.siteID
                                        select linq_module).ToList<Module>();

            if (config.useAdminRoles)
            {
                List<Module> modulesToRemove = new List<Module>();

                AdministratorAuthentication auth = new AdministratorAuthentication(config);
                foreach (var module in sitemodules)
                    if (!auth.HasModuleAccessPermission(module.ModuleID))
                        modulesToRemove.Add(module);

                foreach (var module in modulesToRemove)
                    sitemodules.Remove(module);
            }

            //Set height on dropdown, if to many modules, use default height
            if (sitemodules.Count >= 4)
                ddModules.Height = Unit.Pixel(525);
            else
                ddModules.Height = Unit.Pixel((sitemodules.Count * 55) + 55);

            //Add a start icon
            moduleitem = new RadComboBoxItem();
            moduleitem.Value = "0";
            moduleitem.Text = _language.getValue("module0");
            moduleitem.Attributes.Add("ImagePath", moduleimagespath + "overview.png");            
            ddModules.Items.Add(moduleitem);
            
            //Add all modules
            foreach (Module module in sitemodules)
            {
                moduleitem = new RadComboBoxItem();
                moduleitem.Value = module.ModuleID.ToString();
                moduleitem.Text = _language.getValue("module" + module.ModuleID.ToString());

                //Should we use a default module picture?
                if (System.IO.File.Exists(Server.MapPath(moduleimagespath + module.ModuleID.ToString() + ".png")))
                    moduleitem.Attributes.Add("ImagePath", moduleimagespath + module.ModuleID.ToString() + ".png");
                else
                    moduleitem.Attributes.Add("ImagePath", moduleimagespath + "default.png");

                ddModules.Items.Add(moduleitem);
            }

            ddModules.DataBind();                        
        }

        private void loadLeftContent()
        {
            if (ddModules.SelectedValue != "0")            
            {
                Module selectedModule = (from m in db.Modules where m.ModuleID == int.Parse(ddModules.SelectedValue) select m).ToList()[0];

                if (selectedModule.UseLeftMenu)
                {
                    Control cModule = LoadControl(selectedModule.UserControlName);
                    phMenu.Controls.Clear();
                    phMenu.Controls.Add(cModule);
                }                
            }
        }

        protected void ddModules_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {            
            if (ddModules.SelectedValue == "0")
                Response.Redirect("/");                
            else
            {
                try
                {
                    Module selectedModule = (from m in db.Modules where m.ModuleID == int.Parse(ddModules.SelectedValue) select m).ToList()[0];

                    if (!selectedModule.UseLeftMenu)
                        Response.Redirect(selectedModule.UserControlName);
                }
                catch
                {
                    throw new Exception("AimContent Setup error: dbo.Module UserControl parameters has to be set.");
                }

            }
        }

        /// <summary>
        /// Log out from aimcontent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ibLogout_OnClick(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            AIM.Base.Classes.CookieHandler.ResetCookie();
            Response.Redirect("/Login.aspx");
        }
    }
}
