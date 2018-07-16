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
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace WebSolution
{
    public partial class Testemails : System.Web.UI.Page
    {   
        

        protected void Page_Load(object sender, EventArgs e)
        {
            AIM.Business.AimNews.DataObjectsDataContext db = new AIM.Business.AimNews.DataObjectsDataContext(AIM.Business.DataManager.ConnectionString);



            int userID = 33548;
            //AIM.Business.AimNews.AimNews_User theUser = (from linq_user in db.AimNews_Users
            //                                              where linq_user.ID == userID
            //                                              select linq_user).First<AIM.Business.AimNews.AimNews_User>();

            List<AIM.Business.AimNews.AimNews_User> groupReceivers = (from linq_users in db.AimNews_Users
                                                                      where linq_users.ID == userID
                                                                      select linq_users).ToList<AIM.Business.AimNews.AimNews_User>();



            

            List<AIM.Business.AimNews.AimNews_User> receivers = new List<AIM.Business.AimNews.AimNews_User>();

            receivers.AddRange(groupReceivers);

            foreach (AIM.Business.AimNews.AimNews_User u in receivers)
            {
                tbEmail2.Text = u.Email + "**";
                u.Email = Regex.Replace(u.Email, @"\u00A0", " ").Trim();
            }

            foreach (AIM.Business.AimNews.AimNews_User uu in receivers)
            {
                tbEmail.Text = uu.Email + "*";
            }

            //db.SubmitChanges();
            
            //tbEmail2.Text = theUser.Email.Trim() + "*";

            //theUser.Email = Regex.Replace(theUser.Email, @"\u00A0", " ").Trim();
            

            //tbEmail.Text = theUser.Email + "*"; 


            //***

            
            //int temp = Convert.ToInt32(theUser.Email[theUser.Email.Length - 1].ToString());

            //int m = Convert.ToInt32(theUser.Email[theUser.Email.Length - 1].ToString());

            //tbEmail.Text += " *" + theUser.Email[theUser.Email.Length - 1].ToString() + "*";

            
            //tbEmail.Text = Regex.Replace(tbEmail.Text, @"\u00A0", " ");

            //tbEmail.Text = tbEmail.Text.Trim() + "**";  
            

            //foreach (char c in tbEmail.Text)
            //    Response.Write((int)c + "<br>k<br>");
        }
    }
}
