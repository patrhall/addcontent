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

namespace WebSolution.Public
{
    public partial class About : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Page.Theme = "Aimit";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            setLanguage();
        }

        private void setLanguage()
        {
            AIM.Base.Classes.LanguageHandler language;

            if (!String.IsNullOrEmpty(Request["L"]) && Request["L"].ToUpper() == "ENGLISH")
                language = new AIM.Base.Classes.LanguageHandler(AIM.Base.Classes.LanguageHandler.Language.ENGLISH);
            else
                language = new AIM.Base.Classes.LanguageHandler(AIM.Base.Classes.LanguageHandler.Language.SWEDISH);

            lblTitle.Text = language.getValue("lblTitle");
            lblContent.Text = language.getValue("lblContent");

            //<title> has to be in <head> for xhtml Transitional, but has to be removed before adding meta
            Page.Header.Controls.Remove(Page.Header.FindControl("theTitle"));

            HtmlTitle title = new HtmlTitle();
            title.Text = language.getValue("pageTitle");
            Page.Header.Controls.Add(title);
        }
    }
}
