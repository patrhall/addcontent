using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIM.Base.Modules.Page
{
    public abstract class AimApp : PageBase
    {
        protected AIM.Business.AimEdit.DataObjectsDataContext db = new AIM.Business.AimEdit.DataObjectsDataContext(AIM.Business.DataManager.ConnectionString);

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
            addModuleCss("/Apps/AimEdit/css/Module.css");
        }
    }
}
