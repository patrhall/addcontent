using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Linq;
using System.Xml.Linq;
using AIM.Business.General;

namespace AIM.Base
{
    /// <summary>
    /// Used as base for all webforms
    /// </summary>
    public abstract class PageBase : System.Web.UI.Page
    {  
        protected int? ObjectId
        {
            get
            {
                int tmp;

                if (int.TryParse(Request["oID"], out tmp))
                    return tmp;

                return null;
            }
        }
      
        private AIM.Business.General.DataObjectsDataContext db = new AIM.Business.General.DataObjectsDataContext(AIM.Business.DataManager.ConnectionString);

        /// <summary>
        /// Get configuration for this web application
        /// </summary>
        protected AIM.Base.Classes.Config config { get; private set; }

        /// <summary>
        /// Title for the content part of page
        /// </summary>
        protected string ContentTitle
        {
            get
            {
                string title = Language.getValue("contenttitle");

                if (title == String.Empty)
                    throw new Exception("AIM.Base.PageBase Initial error: ContentTitle is required in language xml");

                return title;
            }
        }

        /// <summary>
        /// Description for the content part of page
        /// </summary>
        protected string ContentDescription
        {
            get
            {
                string description = Language.getValue("contentdescription");

                if (description == String.Empty)
                    throw new Exception("AIM.Base.PageBase Initial error: ContentDescription is required in language xml");

                return description;
            }
        }

        /// <summary>
        /// Language handler for Page
        /// </summary>
        protected AIM.Base.Classes.LanguageHandler Language
        {
            get;
            private set;
        }

        /// <summary>
        /// Check if user has access to page
        /// </summary>
        protected AIM.Base.Classes.AdministratorAuthentication AdministratorAuthentication
        {
            get;
            private set;
        }

        /// <summary>
        /// Preference for customer
        /// </summary>
        protected AIM.Base.Classes.Preference Preference
        {
            get;
            private set;
        }

        /// <summary>
        /// Get module id, can be overridden by modulebase
        /// </summary>
        protected virtual int SiteModuleId
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Base Class Page_PreInit, which calls virtual Page_PreInit in subclass
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Page.Theme = AIM.Base.Globals.Configuration.Theme;

            this.Page_PreInit();
        }

        /// <summary>
        /// Base class Page_Load, which calls abstract Page_Load in subclass
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {                       
            config = new AIM.Base.Classes.Config();
            config.GetConfig();
            Session.LCID = config.lcid_config;

            checkObjectRights();

            Language = Classes.LanguageHandler.getLanguage(config.adminuserid);
            Preference = AIM.Base.Classes.Preference.getPreference(config.customerpath);
            AdministratorAuthentication = new AIM.Base.Classes.AdministratorAuthentication(config);

            try
            {
                Membership.ApplicationName = config.applicationname;
                Roles.ApplicationName = config.applicationname;
                System.Web.Profile.ProfileManager.ApplicationName = config.applicationname;

            }
            catch { } //Cathing sites without asp.net-application

            InitModule();

            this.Page_Load();

            if (ObjectId.HasValue && !AdministratorAuthentication.HasAccessPermission(ObjectId.Value))
                throw new Exception("AIM.Base.PageBase.Page_Load() error: User has no access to page");

            setContentTitle();

            if (!IsPostBack)
                selectMenu();
        }

        /// <summary>
        /// Check if the administrators has access to this pageObject
        /// </summary>
        private void checkObjectRights()
        {            
            //If oID is set, check if administrator has access to this object
            if (ObjectId.HasValue)
                if ((from o in db.Objects where o.SiteID == config.siteID && o.ObjectID == ObjectId.Value select o).Count() == 0)
                    throw new Exception("AIM.Base.Pagebase.checkobjectRights() error: User has no access to this pageObject");
        }

        /// <summary>
        /// Adds module specific css to page
        /// </summary>
        /// <param name="virtualPath"></param>
        protected void addModuleCss(string virtualPath)
        {
            ContentPlaceHolder cphHead = (ContentPlaceHolder)Page.Master.FindControl("cphHead");

            HtmlLink cssLinkControl = new HtmlLink();
            cssLinkControl.Href = virtualPath;
            cssLinkControl.Attributes.Add("rel", "stylesheet");
            cssLinkControl.Attributes.Add("type", "text/css");

            cphHead.Controls.Add(cssLinkControl);
        }

        /// <summary>
        /// Select right module in leftmenu
        /// </summary>
        private void selectMenu()
        {
            Telerik.Web.UI.RadComboBox rcb = (Telerik.Web.UI.RadComboBox)Page.Master.FindControl("ddModules");
            rcb.SelectedValue = SiteModuleId.ToString();

            var selectedModule = (from m in db.Modules where m.ModuleID == SiteModuleId select m);

            if (selectedModule.Count<Module>() == 1 && selectedModule.First<Module>().UseLeftMenu)
            {
                PlaceHolder phMenu = (PlaceHolder)Page.Master.FindControl("phMenu");
                Control cModule = LoadControl(selectedModule.First<Module>().UserControlName);
                phMenu.Controls.Clear();
                phMenu.Controls.Add(cModule);
            }
        }

        /// <summary>
        /// init content title and description
        /// </summary>
        private void setContentTitle()
        {
            Label labelContentTitle = (Label)Page.Master.FindControl("lblContentTitle");
            labelContentTitle.Text = ContentTitle;

            Label labelContentDescription = (Label)Page.Master.FindControl("lblContentDescription");
            labelContentDescription.Text = ContentDescription;
        }

        /// <summary>
        /// Can be overriden by child
        /// </summary>
        protected virtual void Page_PreInit() { }

        /// <summary>
        /// Web Form startup function, please notice zero parameters
        /// </summary>
        protected abstract void Page_Load();

        /// <summary>
        /// To be override in subbaseclasses
        /// </summary>
        protected virtual void InitModule()
        {
            /*
             * This function has no action as default
             * To be overriden in SubPageClasses, i.e. base for modules
             * Should not be used in actual forms
             */
        }
    }
}
