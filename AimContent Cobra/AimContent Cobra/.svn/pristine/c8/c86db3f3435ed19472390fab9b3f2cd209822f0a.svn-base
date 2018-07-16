using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using System.Web.Configuration;
using System.Linq;
using System.Data.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace AIM
{
    /// <summary>
    /// Author: Jan Erlandsson
    /// Create date: -
    /// 
    /// Description:
    /// Login handling for aimcontent
    /// 
    /// UPDATES:
    /// 2008-05-20 (aniv): Added https redirect in page_load
    /// 2008-10-30 (aniv): Added to AimContent Cobra
    /// </summary>
	public partial class Login : System.Web.UI.Page
	{
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Page.Theme = "Aimit";
        }

		protected void Page_Load(object sender, System.EventArgs e)
		{
            //Redirect for https on aimcontent
            switch (Request.Url.Host)
            {
                case "aim.aimit.se":
                    Response.Redirect("https://aim.aimcontent.se", true);
                    break;
                case "a1.aimcontent.se":
                case "ois.aimcontent.se":
                case "rr.aimcontent.se":
                //case "congrex.aimcontent.se":
                    if (Request.Url.ToString().Contains("http://"))
                        Response.Redirect(Request.Url.ToString().Replace("http://", "https://"), true);
                    break;                
            }
                                   
            tbUserName.Focus();

            setLanguage();
		}

        private void setLanguage()
        {
            AIM.Base.Classes.LanguageHandler language;

            switch(ddLanguage.SelectedValue)
            {
                case "English":
                    language = new AIM.Base.Classes.LanguageHandler(AIM.Base.Classes.LanguageHandler.Language.ENGLISH);
                    rwAbout.NavigateUrl = "/Public/About.aspx?L=ENGLISH";
                    rwPassword.NavigateUrl = "/Public/Recovery.aspx?L=ENGLISH";
                    break;
                default:
                    language = new AIM.Base.Classes.LanguageHandler(AIM.Base.Classes.LanguageHandler.Language.SWEDISH);
                    rwAbout.NavigateUrl = "/Public/About.aspx";
                    rwPassword.NavigateUrl = "/Public/Recovery.aspx";
                    break;
            }                

            lblUserName.Text = language.getValue("username");
            lblPassWord.Text = language.getValue("password");
            lblForgetPassword.Text = language.getValue("forgotpassword");
            lblReadMore.Text = language.getValue("about");
            btnLogin.Text = language.getValue("loginbutton");
        }

		protected void btnLogin_Click(object sender, System.EventArgs e)
        {  
            AIM.Business.WebSolution.DataObjectsDataContext db = new AIM.Business.WebSolution.DataObjectsDataContext(AIM.Business.DataManager.ConnectionString);            

           List<AIM.Business.WebSolution.Admin> userColl = (from u in db.Admins where u.username == tbUserName.Text && u.password == tbPassword.Text select u).ToList<AIM.Business.WebSolution.Admin>();
            
            if (userColl.Count == 1)
            {
                AIM.Business.WebSolution.Admin adminUser = userColl[0];                

                List<AIM.Business.WebSolution.usp_GetRolesResult> roles = db.usp_GetRoles(adminUser.ID).ToList<AIM.Business.WebSolution.usp_GetRolesResult>();

                string myRoles = String.Empty;
                
                foreach (AIM.Business.WebSolution.usp_GetRolesResult role in roles)
                    myRoles += role.RoleName + "|";                                

                AIM.Base.Classes.CookieHandler.SetCookie(adminUser.siteID.Value, adminUser.ID);                
                
                // Create the authentication ticket
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, adminUser.username, DateTime.Now, DateTime.MaxValue, false, myRoles);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                Response.Cookies.Add(authCookie);                

                Response.Redirect(FormsAuthentication.GetRedirectUrl(adminUser.username, true));
            }            
		}
	}
}
