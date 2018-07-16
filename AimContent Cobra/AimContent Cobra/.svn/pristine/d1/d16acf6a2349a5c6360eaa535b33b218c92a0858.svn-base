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
using System.Collections.Generic;

namespace WebSolution.Public
{
    public partial class Recovery : System.Web.UI.Page
    {
        private AIM.Base.Classes.LanguageHandler language;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Page.Theme = "Aimit";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMsg.Text = String.Empty;

            tbMail.Attributes.Add("onKeyPress", "if(event.keyCode==13) {document.getElementById('" + btSend.UniqueID + "').click(); return false}");

            setLanguage();
        }

        private void setLanguage()
        {            
            if (!String.IsNullOrEmpty(Request["L"]) && Request["L"].ToUpper() == "ENGLISH")
                language = new AIM.Base.Classes.LanguageHandler(AIM.Base.Classes.LanguageHandler.Language.ENGLISH);
            else
                language = new AIM.Base.Classes.LanguageHandler(AIM.Base.Classes.LanguageHandler.Language.SWEDISH);

            lblTitle.Text = language.getValue("lblTitle");
            lblContent.Text = language.getValue("lblContent");
            btSend.Text = language.getValue("btSend");

            //<title> has to be in <head> for xhtml Transitional, but has to be removed before adding meta
            Page.Header.Controls.Remove(Page.Header.FindControl("theTitle"));

            HtmlTitle title = new HtmlTitle();
            title.Text = language.getValue("pageTitle");
            Page.Header.Controls.Add(title);
        }

        protected void btSend_Click(object sender, EventArgs e)
        {
            if (tbMail.Text != "")
            {
                AIM.Business.WebSolution.DataObjectsDataContext db = new AIM.Business.WebSolution.DataObjectsDataContext(AIM.Business.DataManager.ConnectionString);

                List<AIM.Business.WebSolution.Admin> admins = (from linq_admin in db.Admins where linq_admin.email.ToLower() == tbMail.Text.ToLower() select linq_admin).ToList<AIM.Business.WebSolution.Admin>();

                if (admins.Count != 0)
                {
                    string mailContent = language.getValue("mailContent") + "<br /><br />" + Environment.NewLine + Environment.NewLine;

                    foreach (AIM.Business.WebSolution.Admin admin in admins)
                    {
                        mailContent += language.getValue("site") + " " + admin.Site.SiteName + " <br />" + Environment.NewLine +
                            language.getValue("username") + " " + admin.username + " <br />" + Environment.NewLine +
                            language.getValue("password") + " " + admin.password + " <br /><br />" + Environment.NewLine + Environment.NewLine;
                    }

                    AimResources.Mail.sendMail(tbMail.Text, "info@aimit.se", true, language.getValue("mailTitle"), mailContent);

                    lblMsg.Text = language.getValue("lblMsg");
                }                
            }

            tbMail.Text = "";
        }
    }
}
