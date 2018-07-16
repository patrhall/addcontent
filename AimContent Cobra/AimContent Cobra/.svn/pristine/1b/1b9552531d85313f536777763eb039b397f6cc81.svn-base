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
    public abstract class AimEdit : PageBase
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
