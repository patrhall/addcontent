using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIM.Base.Modules.Page
{
    public abstract class AimCalendar : PageBase
    {
        protected AIM.Business.AimCalendar.DataObjectsDataContext db = new AIM.Business.AimCalendar.DataObjectsDataContext(AIM.Business.DataManager.ConnectionString);

        protected override int SiteModuleId
        {
            get
            {
                return 33;
            }
        }

        /// <summary>
        /// Web Form startup function, please notice zero parameters
        /// </summary>
        protected abstract override void Page_Load();
    }

}