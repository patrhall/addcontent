using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace AimBusiness
{
    public partial class AimBusinessRedirect : AIM.Base.PageBase
    {
        protected override void Page_Load()
        {
            if (Request["oId"] != null)
            {
                Response.Redirect(config.aimbusinesspath + "?oId=" + Request["oId"]);
            }
        }
    }
}
