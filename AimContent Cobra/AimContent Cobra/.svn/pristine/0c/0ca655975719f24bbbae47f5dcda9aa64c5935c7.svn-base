using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIM.Base.Modules.UserControl
{
    public abstract class AimNews : UserControlBase
    {
        protected AIM.Business.AimNews.DataObjectsDataContext db = new AIM.Business.AimNews.DataObjectsDataContext(AIM.Business.DataManager.ConnectionString);

        protected override int SiteModuleId
        {
            get
            {
                return 4;
            }
        }

        /// <summary>
        /// Usercontrol startup function, please notice zero parameters
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

