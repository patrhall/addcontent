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

namespace AimStats
{
    public partial class LiveStats : AIM.Base.Modules.Page.AimStats
    {
        protected override void Page_Load()
        {
            if (!IsPostBack)
            {
                //Set linktext.
                litLinktext.Text = Language.getValue("linkText");

                //Get config object.
                AIM.Business.AimStats.Config configObject = GetConfigObject();
                //Get livestats.
                string liveStats = configObject.liveStats;
                //If livestats contains data.
                if (!String.IsNullOrEmpty(liveStats))
                {
                    //Set url.
                    statsLink.NavigateUrl = liveStats;
                    //Display link.
                    statsLink.Visible = true;
                }
            }
        }

        private AIM.Business.AimStats.Config GetConfigObject()
        {
            //Returns current site config object.
            return (from s in db.Sites where s.SiteID == config.siteID select s).Single<AIM.Business.AimStats.Site>().Config;
        }
    }
}
