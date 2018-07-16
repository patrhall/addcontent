using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AIM.Base;

namespace AIM.Base.Modules.Page
{
    public abstract class AimNews : PageBase
    {
        protected AIM.Business.AimNews.DataObjectsDataContext db = new AIM.Business.AimNews.DataObjectsDataContext(AIM.Business.DataManager.ConnectionString);

        private int _AimNewsID;

        /// <summary>
        /// ID for the news module
        /// </summary>
        protected int AimNewsID
        {
            get
            {
                return _AimNewsID;
            }
        }

        protected override int SiteModuleId
        {
            get
            {
                return 4;
            }
        }

        /// <summary>
        /// Web Form startup function, please notice zero parameters
        /// </summary>
        protected abstract override void Page_Load();

        /// <summary>
        /// Init specific properties
        /// </summary>
        protected override void InitModule()
        {
            _AimNewsID = (from linq_o in db.AimNews_Sites where linq_o.SiteID == config.siteID select linq_o.AimNewsID).First<int>();
            
        }
    }
}
