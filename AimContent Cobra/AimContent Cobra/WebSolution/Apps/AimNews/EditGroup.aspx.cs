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

namespace AimNews
{
    public partial class EditGroup: AIM.Base.Modules.Page.AimNews
    {   
        int groupID;
        string createStatus;
        string saveStatus;
        string importStatus;
        string importErrorStatus;

        protected override void Page_Load()
        {

            //  AIM.Base.Classes.RadHandler.RadEditor_LoadEditorPreference(ref editor1, config);

            initLanguage();
            initGridHeaders();
            initStatus();

            if (!IsPostBack)
            {
                if (Request["ID"] != null && Request["ID"] != "0")
                    getGroupData();
                else
                    btnImport.Enabled = false;
            }
        }

        /// <summary>
        /// Init pagepreference
        /// </summary>
        /// 
       /* private void initPagePreference()
        {
            pref.viewUrl = Boolean.Parse(Preference.getValue("content", "directurl", "used"));
            pref.viewUrlRewrite = PagePreference.StringToUrlRewriteOptions(Preference.getValue("content", "directurl", "urlrewrite"));
            
            //Load preference
            pref.init();
        }*/

        protected void rgUsers_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            groupID = 0;

            if (Request["ID"] != null)
            {
                groupID = int.Parse(Request["ID"].ToString());

                List<AIM.Business.AimNews.AimNews_User> userList = (from linq_users in db.AimNews_Users
                                                                    where linq_users.AimNewsID == AimNewsID
                                                                    from linq_groupusers in db.AimNews_GroupUsers
                                                                    where linq_groupusers.GroupID == groupID && linq_users.ID == linq_groupusers.UserID
                                                                    select linq_users).ToList<AIM.Business.AimNews.AimNews_User>();

                rgUsers.DataSource = userList;
                rgUsers.MasterTableView.NoMasterRecordsText = Language.getValue("noRecordText");
            }

         
       
        }

        protected void rgUsers_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridCommandItem)
            {
                try
                {
                    GridCommandItem cmditm = (GridCommandItem)e.Item;

                    if (cmditm.NamingContainer is GridTHead)
                    {
                        LinkButton newButton = (LinkButton)cmditm.Controls[0].FindControl("lbNewUser");
                        newButton.Text = "<img class=\"textimage\" src=\"/Images/newobject.gif\" alt=\"" + Language.getValue("newuserTooltip") + "\" />&nbsp;";
                        newButton.Attributes.Add("href", "EditUser.aspx?ID=0&gID=" + groupID.ToString());

                        if (groupID == 0)
                            newButton.Visible = false;
                    }
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
            groupID = 0;

            if (Request["ID"] != null)
                groupID = int.Parse(Request["ID"].ToString());

            switch (e.CommandName)
            {
                case "editUser":
                    Response.Redirect("EditUser.aspx?ID=" + e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString() + "&gID=" + groupID);
                    break;
                case "deleteUser":
                    deleteGroupUser(int.Parse(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString()));
                    checkGroupUserExistence(int.Parse(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString()));
                    rgUsers.Rebind();
                    break;

            }
        }

        private void initLanguage()
        {
            lblHeader.Text = Language.getValue("headerlabelText");
            lblDescription.Text = Language.getValue("descriptionLabelText");
            rfvHeader.Text = Language.getValue("validatorText");
            lblUsers.Text = Language.getValue("userlabelText");
            btnImport.Text = Language.getValue("importbuttonText");
            btnSave.Text = Language.getValue("savebuttonText");
            createStatus = Language.getValue("groupCreatedStatus");
            saveStatus = Language.getValue("groupSavedStatus");
            importStatus = Language.getValue("importedFileStatus");
            importErrorStatus = Language.getValue("importedFileErrorStatus");
 
        }

        private void initStatus()
        {
            if (Request["S"] != null)
            {
                switch (Request["S"].ToString())
                {
                    case "0":
                        lblStatus.Text = createStatus;
                        lblStatus.Visible = true;
                        break;
                    case "1":
                        lblStatus.Text = saveStatus;
                        lblStatus.Visible = true;
                        break;
                    case "2":
                        lblStatus.Text = importStatus;
                        lblStatus.Visible = true;
                        break;
                    case "3":
                        lblStatus.Text = importErrorStatus;
                        lblStatus.CssClass = "error";
                        lblStatus.Visible = true;
                        break;
                }
            }
        }

        private void getGroupData()
        {
            if (Request["ID"] != null && Request["ID"].ToString() != "0")
            {
                groupID = Convert.ToInt32(Request["ID"].ToString());
                AIM.Business.AimNews.AimNews_Group theGroup = (from linq_group in db.AimNews_Groups
                                                               where linq_group.ID == groupID
                                                               select linq_group).First<AIM.Business.AimNews.AimNews_Group>();

                tbHeader.Text = theGroup.GroupName;
                tbDescription.Text = theGroup.Description;
            }
            
        }

        private void createNewGroup()
        {
            AIM.Business.AimNews.AimNews_Group newGroup = new AIM.Business.AimNews.AimNews_Group();

            newGroup.AimNewsID = AimNewsID;
            newGroup.GroupName = tbHeader.Text;
            newGroup.Description = tbDescription.Text;
            db.AimNews_Groups.InsertOnSubmit(newGroup);
            db.SubmitChanges();

            groupID = newGroup.ID;
        }

        private void deleteGroupUser(int UserID)
        {
            AIM.Business.AimNews.AimNews_GroupUser groupuser = (from linq_groupuser in db.AimNews_GroupUsers
                                                                where linq_groupuser.UserID == UserID &&
                                                                linq_groupuser.GroupID == groupID
                                                                select linq_groupuser).First<AIM.Business.AimNews.AimNews_GroupUser>();
            
            db.AimNews_GroupUsers.DeleteOnSubmit(groupuser);

            try
            {
                db.SubmitChanges();
            }
            catch (Exception err)
            {
                Response.Write(err);
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

        private void checkGroupUserExistence(int UserID)
        {
            List<AIM.Business.AimNews.AimNews_GroupUser> usergruopconnections = (from linq_groupuser in db.AimNews_GroupUsers
                                                                                 where linq_groupuser.UserID == UserID
                                                                                 select linq_groupuser).ToList<AIM.Business.AimNews.AimNews_GroupUser>();
            if (usergruopconnections.Count == 0)
                deleteUser(UserID);
        }

        private void saveGroup()
        {
            groupID = Convert.ToInt32(Request["ID"].ToString());

            AIM.Business.AimNews.AimNews_Group theGroup = (from linq_group in db.AimNews_Groups
                                                           where linq_group.ID == groupID
                                                           select linq_group).First<AIM.Business.AimNews.AimNews_Group>();
            theGroup.GroupName = tbHeader.Text;
            theGroup.Description = tbDescription.Text;
            db.SubmitChanges();
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

        protected void btnSave_Click(object sender, System.EventArgs e)
        {
            if (Request["ID"] != null && Request["ID"].ToString() != "0")
            {
                saveGroup();
                Response.Redirect("EditGroup.aspx?ID=" + groupID + "&S=1");
            }
            else
            {
                createNewGroup();
                Response.Redirect("EditGroup.aspx?ID=" + groupID + "&S=0");
            }
        }

        protected void btnImport_Click(object sender, System.EventArgs e)
        {
             if (Request["ID"] != null  && Request["ID"].ToString() != "0")
                 Response.Redirect("ImportGroup.aspx?ID=" + Request["ID"]);
        }

        protected void ibBack_OnClick(object sender, System.EventArgs e)
        {
            Response.Redirect("Groups.aspx");
        }
   
    }
}
