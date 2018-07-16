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
using Telerik.Web.UI;
using System.Collections.Generic;

namespace AimNews
{
    public partial class MailList : AIM.Base.Modules.Page.AimNews   
    {
        protected override void Page_Load()
        {
            initStatus();
            initGridHeaders();
        }

        private void initStatus()
        {
            if (Request["S"] != null)
            {
                switch (Request["S"].ToString())
                {
                    case "1":
                        lblStatus.Text = Language.getValue("mailSendStatus");
                        lblStatus.Visible = true;
                        break;
                }
            }
        }

        private void initGridHeaders()
        {
            foreach (GridColumn column in rgNewsLetter.Columns)
            {
                switch (column.UniqueName)
                {
                    case "LastDateColumn":
                        (column as GridTemplateColumn).HeaderText = Language.getValue("LastDateColumnHeader");
                        break;
                    case "StatisticsColumn":
                        if (Boolean.Parse(Preference.getValue("aimnews", "maillist_StatisticsColummn", "used")))
                            (column as GridButtonColumn).Visible = true;
                        break;
                }
            }
        }

        protected void rgNewsLetter_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            List<AIM.Business.AimNews.AimNews_Mail> mailList = (from linq_mailnews in db.AimNews_Mails
                                                                where linq_mailnews.AimNewsID == AimNewsID
                                                                select linq_mailnews).ToList<AIM.Business.AimNews.AimNews_Mail>();

            GridSortExpression expression = new GridSortExpression();
            expression.FieldName = "LastSendDate";
            expression.SortOrder = GridSortOrder.Descending;

            rgNewsLetter.DataSource = mailList;
            rgNewsLetter.MasterTableView.SortExpressions.AddSortExpression(expression);

            rgNewsLetter.MasterTableView.NoMasterRecordsText = Language.getValue("noRecordText");
        }

        protected void rgNewsLetter_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridCommandItem)
            {
                try
                {
                    GridCommandItem cmditm = (GridCommandItem)e.Item;
                    LinkButton newButton = (LinkButton)cmditm.Controls[0].FindControl("lbNewMail");

                    newButton.Text = "<img class=\"textimage\" src=\"/Images/newobject.gif\" alt=\"" + Language.getValue("newMailTooltip") + "\" />&nbsp;"
                         + Language.getValue("lbNewMail");
                }
                catch { }
            }
            else if (e.Item is GridDataItem)
           {
               try
               {
                   GridDataItem rgDataItem = e.Item as GridDataItem;
                   ImageButton delButton = rgDataItem["DeleteButton"].Controls[0] as ImageButton;
                   delButton.Attributes["onclick"] = "return confirm('" + Language.getValue("DelMailConfirm") + "')";

               }
               catch { }
            }
        }
 
        protected string formatDate(DateTime? sendDate)
        {
            if (sendDate.HasValue)
                return sendDate.ToString().Substring(0,10);
           
            return Language.getValue("notSendText");
        }

        protected void rgNewsLetter_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                int mailID = int.Parse(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString());

                switch (e.CommandName)
                {
                    case "viewStat":
                        Response.Redirect("Statistics.aspx?ID=" + mailID);
                        break;
                    case "editMail":
                        Response.Redirect("EditMail.aspx?ID=" + mailID);
                        break;
                    case "removeMail":
                        deleteLinkClick(mailID);
                        deleteLink(mailID);
                        deleteLog(mailID);
                        deleteHistory(mailID);
                        deleteGroupMail(mailID);
                        deleteMail(mailID);
                        rgNewsLetter.Rebind();
                        break;
                }
            }
            catch
            {
            }
        }

        private void deleteLinkClick(int MailID)
        {
            List<AIM.Business.AimNews.AimNews_LinkedClicked> linkclicklist = (from linq_linkclick in db.AimNews_LinkedClickeds
                                                                          where linq_linkclick.AimNews_Link.AimNews_History.MailID == MailID
                                                                              select linq_linkclick).ToList<AIM.Business.AimNews.AimNews_LinkedClicked>();

            foreach (AIM.Business.AimNews.AimNews_LinkedClicked linkclick in linkclicklist)
            {
                db.AimNews_LinkedClickeds.DeleteOnSubmit(linkclick);

                try
                {
                    db.SubmitChanges();
                }
                catch (Exception err)
                {
                    Response.Write(err);
                }
            }
        
        }

        private void deleteLink(int MailID)
        {
            

            List<AIM.Business.AimNews.AimNews_Link> linklist = (from linq_links in db.AimNews_Links
                                                            where linq_links.AimNews_History.MailID == MailID
                                                      select linq_links).ToList<AIM.Business.AimNews.AimNews_Link>();

            foreach (AIM.Business.AimNews.AimNews_Link link in linklist)
            {
                db.AimNews_Links.DeleteOnSubmit(link);

                try
                {
                    db.SubmitChanges();
                }
                catch (Exception err)
                {
                    Response.Write(err);
                }
            
            }
            
        }

        private void deleteLog(int MailID)
        {
            List<AIM.Business.AimNews.AimNews_Log> loglist = (from linq_log in db.AimNews_Logs
                                                              where linq_log.AimNews_History.MailID == MailID
                                                              select linq_log).ToList<AIM.Business.AimNews.AimNews_Log>();

            foreach (AIM.Business.AimNews.AimNews_Log log in loglist)
            {
                db.AimNews_Logs.DeleteOnSubmit(log);

                try
                {
                    db.SubmitChanges();
                }
                catch (Exception err)
                {
                    Response.Write(err);
                }
            }
        }

        private void deleteHistory(int MailID)
        {
           List<AIM.Business.AimNews.AimNews_History> historylist  = (from linq_history in db.AimNews_Histories
                                                         where linq_history.MailID == MailID
                                                         select linq_history).ToList<AIM.Business.AimNews.AimNews_History>();
           
           foreach (AIM.Business.AimNews.AimNews_History history in historylist)
           {
               db.AimNews_Histories.DeleteOnSubmit(history);

               try
               {
                   db.SubmitChanges();
               }
               catch (Exception err)
               {
                   Response.Write(err);
               }
           }   
        }

        private void deleteGroupMail(int MailID)
        {
            List<AIM.Business.AimNews.AimNews_MailGroup> groupmaillist = (from linq_groupuser in db.AimNews_MailGroups
                                                                          where linq_groupuser.MailID == MailID
                                                                          select linq_groupuser).ToList<AIM.Business.AimNews.AimNews_MailGroup>();

            foreach (AIM.Business.AimNews.AimNews_MailGroup groupmail in groupmaillist)
            {
                db.AimNews_MailGroups.DeleteOnSubmit(groupmail);

                try
                {
                    db.SubmitChanges();
                }
                catch (Exception err)
                {
                   Response.Write(err);
                }
            }
        }

        private void deleteMail(int MailID)
        {
            try
            {

                AIM.Business.AimNews.AimNews_Mail theMail = (from linq_mailnews in db.AimNews_Mails
                                                             where linq_mailnews.ID == MailID
                                                             select linq_mailnews).First<AIM.Business.AimNews.AimNews_Mail>();
                db.AimNews_Mails.DeleteOnSubmit(theMail);
                db.SubmitChanges();
            }
            catch (Exception err)
            {
                Response.Write(err);
            }

        }
     
    }
}
