using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace WebSolution.Public
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btTengil_Katla(object sender, EventArgs e)
        {
            if (tbTengil.Text != "siktaitXAdnEq7B")
                return;

            pnlLoggedIn.Visible = true;

            AIM.Business.WebSolution.DataObjectsDataContext db = new AIM.Business.WebSolution.DataObjectsDataContext(AIM.Business.DataManager.ConnectionString);


            var admins = db.Admins.ToList();
            var sites = db.Sites.ToList();

            ddUsers.DataSource = (from a in admins
                                  from s in sites
                                  where a.siteID.Value == s.SiteID                                  
                                  orderby s.SiteName, a.username
                                  select new
                                  {
                                      Id = a.ID,                                      
                                      Name = s.SiteName + ": " + a.username
                                  });
            ddUsers.DataValueField = "Id";
            ddUsers.DataTextField = "Name";
            ddUsers.DataBind();
        }

        protected void btLogin_Click(object sender, EventArgs e)
        {
            AIM.Business.WebSolution.DataObjectsDataContext db = new AIM.Business.WebSolution.DataObjectsDataContext(AIM.Business.DataManager.ConnectionString);

            var adminUser = db.Admins.Single(p => p.ID == Convert.ToInt32(ddUsers.SelectedValue));

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