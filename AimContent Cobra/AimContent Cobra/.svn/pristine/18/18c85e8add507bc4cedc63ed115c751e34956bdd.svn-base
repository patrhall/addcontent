using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Profile;
using System.Data.Linq;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using AIM.General.UserControls;
using Telerik.Web.UI;
using System.Threading;
using System.Web.Configuration;
using System.Text.RegularExpressions;

namespace AimNews
{
    public partial class EditMail: AIM.Base.Modules.Page.AimNews
    {   
        int mailID;
        string mailKey;            

        protected override void Page_Load()
        {

            AIM.Base.Classes.RadHandler.RadEditor_LoadEditorPreference(ref editor1, config);            
          
            initLanguage();
            initStatus();

            if (!IsPostBack)
            {
                if (Request["ID"] != null && Request["ID"] != "0")
                    getMailData();
                else
                    cbWebLink.Checked = true;
            }
        }        
        
        private void initLanguage()
        {
            lblHeader.Text = Language.getValue("headerLabelText");
            rfvHeader.Text = Language.getValue("headerValidatorText");

            lblFromName.Text = Language.getValue("fromNameLabelText");
            lblFromMail.Text = Language.getValue("fromMailLabelText");

            lblUnregisterHeader.Text = Language.getValue("unregisterHeaderText");
            lblUnregisterText.Text = Language.getValue("unregisterText");
            lblUnregisterChoice.Text = Language.getValue("unregisterChoiceText");

            lblWebLinkHeader.Text = Language.getValue("weblinkHeaderText");
            lblWebLink.Text = Language.getValue("weblinkLabelText");

            lblTestmail.Text = Language.getValue("testmailLabelText");
            lblTestmailAdress.Text = Language.getValue("testmailAdressText");
            lblErrorTestMail.Text = Language.getValue("fromMailValidatorText");

            rfvFromMail.Text = Language.getValue("fromMailValidatorText");
            revFromMail.Text = Language.getValue("fromMailExpressionText");
            rfvTestMail.Text = Language.getValue("testMailValidatorText");
            revTestMail.Text = Language.getValue("testMailExpressionText");

            lblEditor.Text = Language.getValue("editorLabelText");
            lblGroup.Text = Language.getValue("groupLabelText");

            //lblDate.Text = Language.getValue("dateLabelText");
            //lblSchedule.Text = Language.getValue("scheduleLabelText"); 

            btnSave.Text = Language.getValue("savebuttonText");
            btnSend.Text = Language.getValue("sendbuttonText");
            btnSendTest.Text = Language.getValue("sendtestbuttonText");

        }

        private void initStatus()
        {
            if (Request["S"] != null)
            {
                switch (Request["S"].ToString())
                {
                    case "0":
                        lblStatus.Text = Language.getValue("mailCreatedStatus");
                        lblStatus.Visible = true;
                        break;
                    case "1":
                        lblStatus.Text = Language.getValue("mailSavedStatus");
                        lblStatus.Visible = true;
                        break;
                 /*   case "2":
                        lblStatus.Text = Language.getValue("mailSendStatus");
                        lblStatus.Visible = true;
                        break;*/
                    case "3":
                        lblStatus.Text = Language.getValue("testmailSendStatus");
                        lblStatus.Visible = true;
                        break;
                }
            }
        }

        private void getMailData()
        {
            mailID = Convert.ToInt32(Request["ID"].ToString());
            AIM.Business.AimNews.AimNews_Mail theMail = (from linq_mailnews in db.AimNews_Mails
                                                         where linq_mailnews.ID == mailID
                                                         select linq_mailnews).First<AIM.Business.AimNews.AimNews_Mail>();

            tbHeader.Text = theMail.Header;
            tbFromMail.Text = theMail.FromMail;
            tbFromName.Text = theMail.FromName;
            editor1.Content = theMail.MailContent;
            cbUnregister.Checked = theMail.hasUnregisterLink;
            tbUnregister.Text = theMail.UnregisterText;
            cbWebLink.Checked = theMail.hasWebLink;


            //rdpSendTime.SelectedDate = theMail.SendDate;
            //cbSchedule.Checked = Convert.ToBoolean(theMail.isScheduled);
            //tbTestmailAdress.Text = theMail.TestMailRecepient;   
        }

        private void saveMail(bool isNewMail)
        {
            AIM.Business.AimNews.AimNews_Mail theMail;

            if (isNewMail)
            {
                theMail = new AIM.Business.AimNews.AimNews_Mail();
                theMail.AimNewsID = AimNewsID;
            }
            else
            {

                mailID = Convert.ToInt32(Request["ID"].ToString());

                theMail = (from linq_mailnews in db.AimNews_Mails
                           where linq_mailnews.ID == mailID
                           select linq_mailnews).First<AIM.Business.AimNews.AimNews_Mail>();
            }

            theMail.Header = tbHeader.Text;
            theMail.FromMail = tbFromMail.Text;
            theMail.FromName = tbFromName.Text;
            theMail.MailContent = editor1.Content;
            theMail.hasUnregisterLink = cbUnregister.Checked;
            theMail.UnregisterText = tbUnregister.Text;
            theMail.hasWebLink = cbWebLink.Checked;
            //theMail.SendDate = rdpSendTime.SelectedDate;
            //theMail.isScheduled = cbSchedule.Checked;
            //theMail.TestMailRecepient = tbTestmailAdress.Text;
            
            if(isNewMail)
                db.AimNews_Mails.InsertOnSubmit(theMail);
            
            db.SubmitChanges();

            if (isNewMail)
            {
                //An infinite loop is not what we want
                for (int i = 0; i < 10000; i++)
                {
                    string randomString = AimResources.StringFunctions.GenerateRandomLowerCaseString(8);

                    if ((from linq_o in db.AimNews_Mails where linq_o.MailKey == randomString select linq_o).Count<AIM.Business.AimNews.AimNews_Mail>() == 0)
                    {
                        theMail.MailKey = randomString;
                        db.SubmitChanges();
                        break;
                    }
                }

                mailID = theMail.ID;
            }

            
            mailKey = theMail.MailKey;
        }

        private void sendMail()
        {
            AIM.Business.AimNews.AimNews_Mail theMail = (from linq_mailnews in db.AimNews_Mails
                                                         where linq_mailnews.ID == mailID
                                                         select linq_mailnews).First<AIM.Business.AimNews.AimNews_Mail>();

            Classes.ThreadedMail oET = new Classes.ThreadedMail();
            //TODO fixa anmälningslänk oET.chkRemoveLink = chkRemovelink.Checked;
            oET.config = config;
            oET.HTMLOriginal = theMail.MailContent;
            oET.mailID = theMail.ID;
            oET.mailKey = theMail.MailKey;
            oET.topic = theMail.Header;

            if (cbUnregister.Checked)
            {
                oET.UnregisterText = tbUnregister.Text;
                oET.UnregisterLinkText = Language.getValue("unregisterLinkText");
            }

            if (cbWebLink.Checked)
                oET.WebLinkText = Language.getValue("weblinkText");

            oET.Debug = Convert.ToBoolean(WebConfigurationManager.AppSettings["AimNews_Debug"].ToString());
            oET.Sender = tbFromMail.Text;
            oET.SenderName = tbFromName.Text;

            //oET.CampaignUsed = Convert.ToBoolean(config_pref.getValue("newsletter", "campaign", "used"));

            oET.receivers = getReceivers();

                      

            ThreadStart thStart = new ThreadStart(oET.sendAllNewsLetters);
            Thread oT = new Thread(thStart);
            //oT = new ThreadStart(oET.sendNewsletter(s,chkRemovelink.Checked,config,config_pref,HTMLOriginal,newsletterID, topic)));

            oT.Start();
        }

        private void sendTestMail()
        {

            string MailMsg = "";
            string htmlContent = editor1.Content.Replace("`", "'");
            string link = config.publicurl + "/ShowNewsLetter.aspx?M=" + mailKey;
            string unregisterlink = "<a href=\"" + config.publicurl + "/Unregister.aspx?M=" + mailKey + "\" >" + Language.getValue("unregisterLinkText") + "</a>";

            //Fulhack för att få unika kampanjer. jool tar på sig den.
            /*if (CampaignUsed)
            {
                string searchstring = config.siteurl_config + "/";
                if (MailMsg.Contains(searchstring))
                {
                    //int newsletterID = Convert.ToInt32(Request["ID"].ToString());
                    MailMsg = MailMsg.Replace(searchstring, config.publicurl + "/?cID=" + mailID);
                }
            }*/

            htmlContent = htmlContent.Replace("src=\"/", "src=\"" + config.publicurl + "/");
            htmlContent = htmlContent.Replace("background=", "background=" + config.publicurl);
            //MailMsg = MailMsg.Replace("href=\"/", "href=\"" + config.publicurl + "/");
            MailMsg = "<html><head><link href=\"" + config.publicurl + config.customerpath + config.cssfile_editor_config + "\" type=\"text/css\" rel=\"stylesheet\"></head><body>";

            if (cbWebLink.Checked)
                MailMsg += "<span class=normal>" + Language.getValue("weblinkText") + "<br>" + link + "</span><br /><br />";

            MailMsg += htmlContent;

            if (cbUnregister.Checked)
                MailMsg += "<br /><br /><span class='normal'>" + tbUnregister.Text + "<br />" + unregisterlink + "</span>";


            MailMsg += "</body></html>";

            if (tbFromName.Text != "")
                AimResources.Mail.sendMail(tbTestmailAdress.Text.Trim(), tbFromMail.Text, tbFromName.Text, true, tbHeader.Text, MailMsg);
            else
                AimResources.Mail.sendMail(tbTestmailAdress.Text.Trim(), tbFromMail.Text, true, tbHeader.Text, MailMsg);

        }

        private List<AIM.Business.AimNews.AimNews_User> getReceivers()
        {
            List<int> mailgroup = (from linq_mailgroups in db.AimNews_MailGroups
                                   where linq_mailgroups.MailID == mailID
                                   select linq_mailgroups.GroupID).ToList<int>();

            List<AIM.Business.AimNews.AimNews_User> receivers = new List<AIM.Business.AimNews.AimNews_User>();

            foreach (int theGroupmail in mailgroup)
            {
                List<AIM.Business.AimNews.AimNews_User> groupReceivers = (from linq_users in db.AimNews_Users
                                                                          where linq_users.AimNewsID == AimNewsID
                                                                          from linq_groupusers in db.AimNews_GroupUsers
                                                                          where linq_groupusers.GroupID == theGroupmail && linq_users.ID == linq_groupusers.UserID
                                                                          select linq_users).ToList<AIM.Business.AimNews.AimNews_User>();

                receivers.AddRange(groupReceivers);
            }


            foreach (AIM.Business.AimNews.AimNews_User u in receivers)
            {                
                u.Email = Regex.Replace(u.Email, @"\u00A0", " ").Trim();
            }

            db.SubmitChanges();

            return receivers;

        }

        private void selectGroups()
        {
            foreach (GridDataItem gridItem in rgGroups.Items)
            {
                int groupID = int.Parse(gridItem.OwnerTableView.DataKeyValues[gridItem.ItemIndex]["ID"].ToString());
                int mailgroupCount = (from linq_groupmail in db.AimNews_MailGroups
                                      where linq_groupmail.MailID == mailID && linq_groupmail.GroupID == groupID
                                      select linq_groupmail).Count();

                CheckBox chkSelect = (CheckBox)gridItem.FindControl("cbChoice");

                if (chkSelect.Checked && mailgroupCount == 0)
                {
                    AIM.Business.AimNews.AimNews_MailGroup newMailGroup = new AIM.Business.AimNews.AimNews_MailGroup();
                    newMailGroup.GroupID = groupID;
                    newMailGroup.MailID = mailID;
                    db.AimNews_MailGroups.InsertOnSubmit(newMailGroup);
                    db.SubmitChanges();
                }
                else if (!chkSelect.Checked && mailgroupCount != 0)
                {
                    AIM.Business.AimNews.AimNews_MailGroup mailGroup = (from linq_groupmail in db.AimNews_MailGroups
                                                                        where linq_groupmail.MailID == mailID && linq_groupmail.GroupID == groupID
                                                                        select linq_groupmail).First<AIM.Business.AimNews.AimNews_MailGroup>();

                    db.AimNews_MailGroups.DeleteOnSubmit(mailGroup);

                    try
                    {
                        db.SubmitChanges();
                    }
                    catch { }
                }
            }
        }

        protected void btnSave_Click(object sender, System.EventArgs e)
        {
            if (Request["ID"] != null && Request["ID"].ToString() != "0")
            {
                saveMail(false);
                Response.Redirect("EditMail.aspx?ID=" + mailID + "&S=1");
            }
            else
            {
                saveMail(true);
                Response.Redirect("EditMail.aspx?ID=" + mailID + "&S=0");
            }
        }

        protected void btnSend_PreRender(object sender, System.EventArgs e)
        {
            btnSend.Attributes.Add("onclick", "return confirm ('" + Language.getValue("sendConfirmMessage") + "');");
        }

        protected void btnSendTest_PreRender(object sender, System.EventArgs e)
        {
            btnSendTest.Attributes.Add("onclick", "alert('" + Language.getValue("sendTestMessage") + "');");
        }

        protected void btnSend_Click(object sender, System.EventArgs e)
        {

            if (Request["ID"] != null && Request["ID"].ToString() != "0")
                saveMail(false);
            else
                saveMail(true);

            selectGroups();
            sendMail();

            Response.Redirect("MailList.aspx?ID=" + mailID + "&S=1");
        }

        protected void btnSendTest_Click(object sender, System.EventArgs e)
        {
            //Validates that from mail is not empty.
            rfvFromMail.Validate();
            if (!rfvFromMail.IsValid)
            {
                //Display error.
                lblErrorTestMail.Visible = true;
                //Return from method.
                return;
            }

            if (Request["ID"] != null && Request["ID"].ToString() != "0")
                saveMail(false);
            else
                saveMail(true);

           // replaceHref(editor1.Content);
            sendTestMail();
            Response.Redirect("EditMail.aspx?ID=" + mailID + "&S=3");
        }

        protected void ibBack_OnClick(object sender, System.EventArgs e)
        {
            Response.Redirect("MailList.aspx");
        }

        protected void rgGroups_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            List<AIM.Business.AimNews.AimNews_Group> groupList = (from linq_mailgroups in db.AimNews_Groups
                                                                  where linq_mailgroups.AimNewsID == AimNewsID
                                                                  select linq_mailgroups).ToList<AIM.Business.AimNews.AimNews_Group>();

            rgGroups.DataSource = groupList;
            rgGroups.MasterTableView.NoMasterRecordsText = Language.getValue("noRecordText");
        }

        protected void rgGroups_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridCommandItem)
            {
                try
                {
                    GridCommandItem cmditm = (GridCommandItem)e.Item;
                    LinkButton newButton = (LinkButton)cmditm.Controls[0].FindControl("lbNewGroup");

                    newButton.Text = "<img class=\"textimage\" src=\"/Images/newobject.gif\" alt=\"" + Language.getValue("newGroupTooltip") + "\" />&nbsp;"
                         + Language.getValue("lbNewGroup");
                }
                catch  { }
            }         
        }        
    }
}
