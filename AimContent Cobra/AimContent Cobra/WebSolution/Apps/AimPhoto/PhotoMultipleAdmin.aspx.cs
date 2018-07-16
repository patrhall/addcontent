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

namespace AimPhoto
{
    public partial class PhotoMultipleAdmin : AIM.Base.Modules.Page.AimPhoto
    {
        protected override void Page_Load()
        {
            int catid = 0;

            if (!int.TryParse(Request["CategoryID"], out catid))
            {
                lblError.Visible = true;
                lblError.Text = Language.getValue("lblError");
                return;
            }

            Literal ObjectHTML = new Literal();
            ObjectHTML.Text = "<object height='400' width='900' id='ActiveXPowUpload' name='ActiveXPowUpload' codebase='/components/ActiveXPowUpload.cab' classid='CLSID:FB98CEED-9DE1-4517-B30C-CDA19C6D150B'>\n"
                + "<param name='Script' value='/Modules/AimPhoto/PhotoMultipleUpload.aspx?oID=" + Request["oID"] + "&CategoryId=" + catid.ToString() + "' />\n"
                + "<param name='Server' value='" + Request.Url.Host + "' />\n"
                + "<param name='SerialKey' value='17113527801131692480170172' />\n"
                + "</object>\n";

            ObjectHTML.Text += "<br /><br />\n<input name='submitbtn' onclick='mysubmit();' type='button' value='" + Language.getValue("submitbtn") + "' />\n";

            phUploader.Controls.Add(ObjectHTML);

            hpBack.NavigateUrl = "PhotoCategory.aspx?oID=" + Request["oID"] + "&CategoryId=" + Request["CategoryId"];

            setLanguage();
        }

        private void setLanguage()
        {
            hpBack.Text = Language.getValue("hpBack");
        }
    }
}
