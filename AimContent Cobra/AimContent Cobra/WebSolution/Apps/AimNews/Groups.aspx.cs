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
    public partial class Groups : AIM.Base.Modules.Page.AimNews
    {
        protected override void Page_Load()
        {

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
                catch { }
            }
            else if (e.Item is GridDataItem)
            {
                try
                {
                    GridDataItem rgDataItem = e.Item as GridDataItem;
                    ImageButton delButton = rgDataItem["DeleteButton"].Controls[0] as ImageButton;
                    delButton.Attributes["onclick"] = "return confirm('" + Language.getValue("delGroupConfirm") + "')";
                }
                catch
                { }
            }
        }

        protected void rgGroups_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {

                case "editGroup":
                    Response.Redirect("EditGroup.aspx?ID=" + e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString());
                    break;
                case "deleteGroup":
                    deleteGroupMail(int.Parse(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString()));
                    deleteGroupUser(int.Parse(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString()));
                    deleteGroup(int.Parse(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString()));
                    rgGroups.Rebind();
                    break;
            }
        }

        private void deleteGroupUser(int GroupID)
        {
            List<AIM.Business.AimNews.AimNews_GroupUser> groupusers = (from linq_groupuser in db.AimNews_GroupUsers
                                                                       where linq_groupuser.GroupID == GroupID
                                                                       select linq_groupuser).ToList<AIM.Business.AimNews.AimNews_GroupUser>();

            foreach (AIM.Business.AimNews.AimNews_GroupUser groupuser in groupusers)
            {
                db.AimNews_GroupUsers.DeleteOnSubmit(groupuser);

                try
                {
                    db.SubmitChanges();
                }
                catch { }

                checkGroupUserExistence(groupuser.UserID);

            }

        }

        private void deleteGroupMail(int GroupID)
        {
            List<AIM.Business.AimNews.AimNews_MailGroup> groupmaillist = (from linq_groupuser in db.AimNews_MailGroups
                                                                          where linq_groupuser.GroupID == GroupID
                                                                          select linq_groupuser).ToList<AIM.Business.AimNews.AimNews_MailGroup>();

            foreach (AIM.Business.AimNews.AimNews_MailGroup groupmail in groupmaillist)
            {
                db.AimNews_MailGroups.DeleteOnSubmit(groupmail);

                try
                {
                    db.SubmitChanges();
                }
                catch { }
            }

        }

        private void checkGroupUserExistence(int UserID)
        {
            List<AIM.Business.AimNews.AimNews_GroupUser> usergruopconnections = (from linq_groupuser in db.AimNews_GroupUsers
                                                                                 where linq_groupuser.UserID == UserID
                                                                                 select linq_groupuser).ToList<AIM.Business.AimNews.AimNews_GroupUser>();
            if (usergruopconnections.Count == 0)
                deleteUser(UserID);
        }

        private void deleteUser(int UserID)
        {
            AIM.Business.AimNews.AimNews_User theUser = (from linq_user in db.AimNews_Users
                                                         where linq_user.ID == UserID
                                                         select linq_user).First<AIM.Business.AimNews.AimNews_User>();
            db.AimNews_Users.DeleteOnSubmit(theUser);


            db.SubmitChanges();


        }

        private void deleteGroup(int GroupID)
        {

            AIM.Business.AimNews.AimNews_Group theGroup = (from linq_mailgroup in db.AimNews_Groups
                                                           where linq_mailgroup.ID == GroupID
                                                           select linq_mailgroup).First<AIM.Business.AimNews.AimNews_Group>();
            db.AimNews_Groups.DeleteOnSubmit(theGroup);


            db.SubmitChanges();


        }
    }
}
