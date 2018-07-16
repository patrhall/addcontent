using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace WebSolution
{
    public partial class _04 : AIM.Base.Modules.Page.WebSolution
    {
        protected override void Page_Load()
        {
            lblDate.Text = Language.getValue("lblDate") + DateTime.Now.ToString();
        }
    }
}
