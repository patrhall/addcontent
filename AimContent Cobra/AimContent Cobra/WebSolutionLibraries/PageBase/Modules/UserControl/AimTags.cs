using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIM.Base.Modules.UserControl
{
    public abstract class AimTags : UserControlBase
    {
        protected AIM.Business.AimTags.DataObjectsDataContext db = new AIM.Business.AimTags.DataObjectsDataContext(AIM.Business.DataManager.ConnectionString);

        protected override int SiteModuleId
        {
            get
            {
                return 34;
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

