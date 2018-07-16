using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AIM.Base;

namespace AIM.Base.Modules.Page
{
    /// <summary>
    /// Base Class for AimProduct
    /// </summary>
    public abstract class AimProduct : PageBase
    {
        private int _productModuleID;

        /// <summary>
        /// ID for the product register
        /// </summary>
        protected int ProductModuleID
        {
            get
            {
                return _productModuleID;
            }
        }

        protected override int SiteModuleId
        {
            get
            {
                return 29;
            }
        }

        /// <summary>
        /// Web Form startup function, please notice zero parameters
        /// </summary>
        protected abstract override void Page_Load();

        /// <summary>
        /// Init AimProduct specific properties
        /// </summary>
        protected override void InitModule()
        {
            //_productModuleID = AIM.DBcom.ComTrans.Product_GetModuleID(config.siteID);
        }
    }
}
