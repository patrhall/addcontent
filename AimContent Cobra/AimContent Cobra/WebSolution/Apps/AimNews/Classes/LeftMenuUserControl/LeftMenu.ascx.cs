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

namespace AimNews.LeftMenuUserControl
{
    public partial class LeftMenu : AIM.Base.Modules.UserControl.AimNews
    {
        public LeftMenu()
        {
            UserControlVirtualPath = "/Apps/AimNews/LeftMenuUserControl/LeftMenu.ascx";
        }
        protected override void Page_Load()
        {
            GetLinkText();
        }

        private void GetLinkText()
        {
            MailListLink.InnerText = Language.getValue("MailListLinkText");
            GroupListLink.InnerText = Language.getValue("GroupListLinkText");
            UnRegisteredUsers.InnerText = Language.getValue("UnRegisteredUsers");
           // UserListLink.InnerText = Language.getValue("UserListLinkText");
        }
    }
}