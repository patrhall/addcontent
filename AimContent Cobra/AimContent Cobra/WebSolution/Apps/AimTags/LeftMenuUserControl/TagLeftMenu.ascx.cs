using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AimTags.LeftMenuUserControl
{
    public partial class TagLeftMenu : AIM.Base.Modules.UserControl.AimTags
    {
        public TagLeftMenu()
        {
            UserControlVirtualPath = "/Apps/AimTags/LeftMenuUserControl/TagLeftMenu.ascx";
        }
        protected override void Page_Load()
        {
            GetLinkText();
        }

        private void GetLinkText()
        {
            ContentListLink.InnerText = Language.getValue("ContentListLink");
            TagListLink.InnerText = Language.getValue("TagListLink");
        }
    }
}