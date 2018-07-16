using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIM.Base.Modules.Page
{
    public abstract class AimTags : PageBase
    {
        protected AIM.Business.AimTags.DataObjectsDataContext db = new AIM.Business.AimTags.DataObjectsDataContext(AIM.Business.DataManager.ConnectionString);

        /// <summary>
        /// 
        /// </summary>
        protected override int SiteModuleId
        {
            get
            {
                return 34;
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
