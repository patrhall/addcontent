using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIM.Base.Modules.Page
{
    public abstract class AimSiteNews : PageBase
    {
        protected AIM.Business.AimSiteNews.DataObjectsDataContext db = new AIM.Business.AimSiteNews.DataObjectsDataContext(AIM.Business.DataManager.ConnectionString);

        /// <summary>
        /// 
        /// </summary>
        protected override int SiteModuleId
        {
            get
            {
                return 1;
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
        }
    }
}
