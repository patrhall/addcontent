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
using AIM.Business.AimForm;
using Telerik.Web.UI;

namespace AimForm
{
    public partial class ExternalConnectionValues : AIM.Base.Modules.Page.AimForm
    {
        public int FormID;
        protected override void Page_Load()
        {
            lblBack.Text = Language.getValue("backlink");
            FormID = int.Parse(Request["fID"]);
            int ResponderID = int.Parse(Request["rID"]);

            var Values = from v in db.Form_ExternalConnections_ReturnValues
                         where v.Responder_FK == ResponderID
                         select new
                         {
                             Property = v.Form_ExternalConnections_ReturnProperty.Name,
                             Value = v.Value
                         };

            string result = "";
            rgValues.DataSource = Values;
            rgValues.DataBind();           
        }
    }
}
