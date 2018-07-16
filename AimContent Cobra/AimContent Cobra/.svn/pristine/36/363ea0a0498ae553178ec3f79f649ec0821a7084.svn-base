using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIM.Base;

namespace AIM.Base.Modules.Page
{
    public abstract class AimAdministrator : PageBase
    {
        protected AIM.Business.AimAdministrator.DataObjectsDataContext db = new AIM.Business.AimAdministrator.DataObjectsDataContext(AIM.Business.DataManager.ConnectionString);

        /// <summary>
        /// 
        /// </summary>
        protected override int SiteModuleId
        {
            get
            {
                return 2;
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
