using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace AIM.Base
{
    public abstract class UserControlBase : System.Web.UI.UserControl
    {
        private string _userControlVirtualPath = string.Empty;

        /// <summary>
        /// Get configuration for this web application
        /// </summary>
        protected AIM.Base.Classes.Config config
        {
            get;
            private set;

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
        /// Preference for customer
        /// </summary>
        protected AIM.Base.Classes.Preference Preference
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
        /// Virtual path to UserControl, must be set in constructor
        /// </summary>
        protected string UserControlVirtualPath
        {
            set
            {
                _userControlVirtualPath = value;
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
            baseInit();
        }

        /// <summary>
        /// To be used if Page_Load is not fired
        /// </summary>
        public void baseInit()
        {
            config = new AIM.Base.Classes.Config();
            config.GetConfig();

            Language = Classes.LanguageHandler.getLanguage(config.adminuserid, _userControlVirtualPath);
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
