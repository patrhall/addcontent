﻿using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net.Mail;
using System.Web.SessionState;
using System.Threading;
using System.Collections.Generic;
using AIM.Base.Classes;
using System.Collections;
using System.Web.Configuration;
using System.Text.RegularExpressions;
using AIM.Business.AimNews;

namespace AimNews.Classes
{
    public class ThreadedMail
    {
        public MailMessage Mailer;                 

        public List<AIM.Business.AimNews.AimNews_User> receivers {set; get;}         
        public bool chkRemoveLink {set; get;} 
        public Config config {set; get;} 
        public string HTMLOriginal {set; get;} 
        public int mailID {set; get;} 
        public string mailKey {set; get;} 
        public string topic {set; get;} 
        public bool CampaignUsed {set; get;} 
        public string UnregisterText {set; get;} 
        public string UnregisterLinkText {set; get;} 
        public string WebLinkText {set; get;} 
        public string HistoryID {set; get;} 
        public string Sender {set; get;} 
        public string SenderName {set; get;}         
        public bool Debug {set; get;}        

        private AIM.Business.AimNews.DataObjectsDataContext db = new AIM.Business.AimNews.DataObjectsDataContext(AIM.Business.DataManager.ConnectionString);
        public AIM.Business.AimNews.AimNews_History newHistory = new AIM.Business.AimNews.AimNews_History();

        private Dictionary<string, string> sendedMail = new Dictionary<string, string>();



        public void sendAllNewsLetters()
        {
            createHistoryItem();
            saveSendDate();  

            try
            {
                foreach (AIM.Business.AimNews.AimNews_User rec in receivers)
                {
                    if (!sendedMail.ContainsKey(rec.Email))
                    {
                        sendNewsletter(rec);
                        sendedMail.Add(rec.Email, String.Empty);
                    }
                }
            }
            catch (Exception err)
            {
                newHistory.ErrorMessage = err.ToString();
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// Send _one_ newsletter
        /// </summary>
        /// <param name="s"></param>
        /// <returns>errorstring if exists</returns>
        
        private void createHistoryItem()
        {
            newHistory.MailID = mailID;
            newHistory.NbrRecipients = receivers.Count;
            newHistory.NbrOpens = 0;
            newHistory.SendTime = DateTime.Now;
            newHistory.hasFinished = false;
            db.AimNews_Histories.InsertOnSubmit(newHistory);
            db.SubmitChanges();

            var groups = (from mg in db.AimNews_MailGroups where mg.MailID == mailID select mg);

            foreach (var group in groups)
            {
                var association = new AimNews_History_X_Group();
                association.GroupID = group.GroupID;
                association.HistoryID = newHistory.ID;
                db.AimNews_History_X_Groups.InsertOnSubmit(association);

            }
        }

        private void saveSendDate()
        {
            AIM.Business.AimNews.AimNews_Mail theMail = (from linq_mailnews in db.AimNews_Mails
                                                         where linq_mailnews.ID == mailID
                                                         select linq_mailnews).First<AIM.Business.AimNews.AimNews_Mail>();
            theMail.LastSendDate = DateTime.Today;
            db.SubmitChanges();
        }

        public void sendNewsletter(AIM.Business.AimNews.AimNews_User user)
        {

            string MailMsg;
            string HTML = HTMLOriginal;

            Hashtable hashkey = new Hashtable();           

            //Change ´ against ' to allow urls with ' in them, initially an informtrycket problem
            HTML = HTML.Replace("`", "'");
            HTML = replaceHref(HTML, user.ID);
            HTML = HTML.Replace("src=\"/", "src=\"" + config.publicurl + "/");
            HTML = HTML.Replace("background=", "background=" + config.publicurl);           

            string weblink = config.publicurl + "/ShowNewsLetter.aspx?M=" + mailKey + "&U=" + user.ID + "&H=" + newHistory.ID;
            string unregisterlink = "<a href=\"" + config.publicurl + "/Unregister.aspx?M=" + mailKey + "&U=" + user.ID + "\" >" + UnregisterLinkText + "</a>";

            MailMsg = "<html><head><link href=\"" + config.publicurl + config.customerpath + config.cssfile_editor_config + "\" type=\"text/css\" rel=\"stylesheet\"></head><body>";
            MailMsg += "<br /><img alt='' src=\"" + config.publicurl + "/Image.aspx?M=" + mailKey + "&U=" + user.ID + "&H=" + newHistory.ID + "\" />";

            if (!String.IsNullOrEmpty(WebLinkText))
                MailMsg += "<span class='normal'>" + WebLinkText + "<br />" + weblink + "</span><br /><br />";
           
            MailMsg += HTML;

             if (!String.IsNullOrEmpty(UnregisterText))
                 MailMsg += "<br /><br /><span class='normal'>" + UnregisterText + "<br />" + unregisterlink + "</span>";
            
            MailMsg += "</body></html>";
            
            string log_receiver = user.Email;
            
            Classes.LogHandler.NewsLetterLogHandler newsletterlogHandler = new Classes.LogHandler.NewsLetterLogHandler();
            newsletterlogHandler.userID_FK = user.ID;
            newsletterlogHandler.MailAddress = user.Email;
            newsletterlogHandler.NewsLetterHistory_FK = newHistory.ID;

            try
            {
                if (!Debug)
                {
                    Label temp = new Label();
                    
                    temp.Text = Regex.Replace(user.Email, @"\u00A0", " ");

                    temp.Text = temp.Text.Trim();


                    if (SenderName != "")
                        AimResources.Mail.sendMail(user.Email.Trim(), Sender, SenderName, true, topic, MailMsg);
                    else
                        AimResources.Mail.sendMail(user.Email.Trim(), Sender, true, topic, MailMsg);
                }

                //Log the address
                newsletterlogHandler.Write();
            }
            catch (Exception err)
            {
                //If error, log
                newsletterlogHandler.Message = err.ToString();
                newsletterlogHandler.Write();
            }

        }

        public string replaceHref(string mailMessage, int UserID)
        {
            string mailKey = getMailKey();         
            string regexexpr = "(<a )([^>]*)(href=\")([^>\"]+)(\")([^>]*)(>)";
            
            MatchCollection matches = Regex.Matches(mailMessage, regexexpr, RegexOptions.IgnoreCase);            
     
            foreach(Match m in matches)
            {
                int mailLinks = (from mailLink in db.AimNews_Links
                                       where mailLink.Url == m.Groups[4].Value
                                       && mailLink.HistoryID_FK == newHistory.ID
                                       select mailLink).Count<AIM.Business.AimNews.AimNews_Link>();

                if (mailLinks == 0 && !m.Groups[4].Value.Contains("mailto:"))
                {

                    AIM.Business.AimNews.AimNews_Link newLink = new AIM.Business.AimNews.AimNews_Link();
                    newLink.HistoryID_FK = newHistory.ID;
                    newLink.Url = m.Groups[4].Value;
                    db.AimNews_Links.InsertOnSubmit(newLink);
                    db.SubmitChanges();                    
                }                
            }

            List<AIM.Business.AimNews.AimNews_Link> linksInMail = (from linq_o in db.AimNews_Links 
                                                                   where linq_o.HistoryID_FK == newHistory.ID 
                                                                   select linq_o).ToList();

            string regexexpr_start = "(<a)([^>]*)(href=\")(";
            string regexexpr_end = ")(\")([^>]*)(>)";

            foreach (AIM.Business.AimNews.AimNews_Link linkInMail in linksInMail)
            {
                if (!linkInMail.Url.Contains("mailto:"))
                    mailMessage = Regex.Replace(mailMessage, regexexpr_start + removeEvilRegexChars(linkInMail.Url) + regexexpr_end, "$1$2$3" + config.publicurl + "/Link.aspx?K=" + mailKey + "&L=" + linkInMail.ID.ToString() + "&H=" + linkInMail.HistoryID_FK.ToString() + "&U=" + UserID + "$5$6$7", RegexOptions.IgnoreCase);
            }

            return mailMessage;           
        }

        private string removeEvilRegexChars(string str)
        {
            str = str.Replace(".", "\\.");
            str = str.Replace("$", "\\$");
            str = str.Replace("+", "\\+");
            str = str.Replace("?", "\\?");

            return str;
        }

        public string getMailKey()
        {
             return (from theMail in db.AimNews_Mails
                    where theMail.ID == mailID
                    select theMail.MailKey).First<string>().ToString();
        }       
    }
}
