using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIM.Base.Modules.Page.AimBusiness.ModulSystem.Website
{
    public abstract class AimBusiness : PageBase
    {
        protected AIM.Business.AimBusiness.ModulSystem.Website.DataObjectsDataContext db = new Business.AimBusiness.ModulSystem.Website.DataObjectsDataContext(System.Web.Configuration.WebConfigurationManager.AppSettings["ModulSystemConnectionString"]);

        /// <summary>
        /// Not a module, just an object type
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
