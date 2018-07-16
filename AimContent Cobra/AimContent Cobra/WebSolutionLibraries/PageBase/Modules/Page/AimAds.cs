using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIM.Base.Modules.Page
{
    public abstract class AimAds : PageBase    
    {
        protected AIM.Business.AimAds.DataObjectsDataContext db = new AIM.Business.AimAds.DataObjectsDataContext(AIM.Business.DataManager.ConnectionString);        
        
        protected override int SiteModuleId
        {
            get
            {
                return 18;
            }
        }

        /// <summary>
        /// Web Form startup function, please notice zero parameters
        /// </summary>
        protected abstract override void Page_Load();
    }
}
