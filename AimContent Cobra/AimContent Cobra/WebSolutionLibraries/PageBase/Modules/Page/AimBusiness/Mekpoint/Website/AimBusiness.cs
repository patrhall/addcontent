using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIM.Base.Modules.Page.AimBusiness.Mekpoint.Website
{
    public abstract class AimBusiness : PageBase
    {
        protected AIM.Business.AimBusiness.Mekpoint.Website.AimContent.DataObjectsDataContext db = new Business.AimBusiness.Mekpoint.Website.AimContent.DataObjectsDataContext(AIM.Business.DataManager.ConnectionString);

        protected AIM.Business.AimBusiness.Mekpoint.Website.Mekpoint.DataObjectsDataContext dbMekpoint = new Business.AimBusiness.Mekpoint.Website.Mekpoint.DataObjectsDataContext("workstation id=aimit1;packet size=4096;user id=mekpoint;data source=aimit1;persist security info=True;initial catalog=Mekpoint;password=maskop2012;Min Pool Size=10;Max Pool Size=100");

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
