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
    public partial class UnRegisteredUsers : AIM.Base.Modules.Page.AimNews  
    {
        protected override void Page_Load()
        {
            initGridHeaders();
        }

        protected void rgUsers_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            List<AIM.Business.AimNews.AimNews_User> userList = (from linq_users in db.AimNews_Users
                                                                where linq_users.AimNewsID == AimNewsID && linq_users.Unregistered == true
                                                                select linq_users).ToList<AIM.Business.AimNews.AimNews_User>();
            rgUsers.DataSource = userList;
            rgUsers.MasterTableView.NoMasterRecordsText = Language.getValue("noRecordText");
        }

        protected void rgUsers_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridCommandItem)
            {
                try
                {
                    GridCommandItem cmditm = (GridCommandItem)e.Item;
                    LinkButton newButton = (LinkButton)cmditm.Controls[0].FindControl("lbNewUser");

                    newButton.Text = "<img class=\"textimage\" src=\"/Images/newobject.gif\" alt=\"" + Language.getValue("newUserTooltip") + "\" />&nbsp;"
                         + Language.getValue("lbNewUser");
                }
                catch { }
            }
            else if (e.Item is GridDataItem)
            {

                try
                {
                    GridDataItem rgDataItem = e.Item as GridDataItem;
                    ImageButton delButton = rgDataItem["DeleteButton"].Controls[0] as ImageButton;
                    delButton.Attributes["onclick"] = "return confirm('" + Language.getValue("DelUserConfirm") + "')";

                }
                catch
                { }
            }

        }

        protected void rgUsers_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "editUser":
                    Response.Redirect("EditUser.aspx?ID=" + e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString());
                    break;
                case "deleteUser":
                    deleteUser(int.Parse(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString()));
                    rgUsers.Rebind();
                    break;
            }
        }

        private void initGridHeaders()
        {
            foreach (GridColumn column in rgUsers.Columns)
            {
                switch (column.UniqueName)
                {
                    case "FirstName":
                        (column as GridTemplateColumn).HeaderText = Language.getValue("FirstnameHeader");
                        if (Boolean.Parse(Preference.getValue("aimnews", "user_FirstnameColummn", "used")))
                            (column as GridTemplateColumn).Visible = true;
                        break;
                    case "LastName":
                        (column as GridTemplateColumn).HeaderText = Language.getValue("LastnameHeader");
                        if (Boolean.Parse(Preference.getValue("aimnews", "user_LastnameColummn", "used")))
                            (column as GridTemplateColumn).Visible = true;
                        break;
                    case "Email":
                        (column as GridTemplateColumn).HeaderText = Language.getValue("EmailHeader");
                        if (Boolean.Parse(Preference.getValue("aimnews", "user_EmailColummn", "used")))
                            (column as GridTemplateColumn).Visible = true;
                        break;
                    case "Company":
                        (column as GridTemplateColumn).HeaderText = Language.getValue("CompanyHeader");
                        if (Boolean.Parse(Preference.getValue("aimnews", "user_CompanyColummn", "used")))
                            (column as GridTemplateColumn).Visible = true;
                        break;
                }
            }
        }

        private void deleteUser(int UserID)
        {
            AIM.Business.AimNews.AimNews_User theUser = (from linq_user in db.AimNews_Users
                                                         where linq_user.ID == UserID
                                                         select linq_user).First<AIM.Business.AimNews.AimNews_User>();
            db.AimNews_Users.DeleteOnSubmit(theUser);

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
}