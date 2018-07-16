using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Linq;
using System.Data.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using Telerik.Web.UI;

namespace AimAdministrator
{
    public partial class Administrators : AIM.Base.Modules.Page.AimAdministrator
    {
        protected override void Page_Load()
        {            
            if (!IsPostBack)
                initAdminRoles();

            setLanguage();
        }

        private void initAdminRoles()
        {
            rblGroups.DataSource = (from o in db.Roles
                                     where !o.SiteID.HasValue ||
                                     o.SiteID.Value == config.siteID
                                     select o);
            rblGroups.DataTextField = "RoleName";
            rblGroups.DataValueField = "ID";
            rblGroups.DataBind();

            tr_roles.Visible = config.useAdminRoles;

            ddAdminLanguage.DataSource = db.AdminLanguages;
            ddAdminLanguage.DataValueField = "ID";
            ddAdminLanguage.DataBind();            
        }

        private void setLanguage()
        {
            lblFormUserName.Text = Language.getValue("lblFormUserName");
            lblFormPassword.Text = Language.getValue("lblFormPassword");
            lblFormNameFirst.Text = Language.getValue("lblFormNameFirst");
            lblFormNamelast.Text = Language.getValue("lblFormNamelast");
            lblFormEmail.Text = Language.getValue("lblFormEmail");

            reqUserName.ErrorMessage = Language.getValue("reqUserName");
            reqFirstName.ErrorMessage = Language.getValue("reqFirstName");
            reqLastName.ErrorMessage = Language.getValue("reqLastName");
            reqPassword.ErrorMessage = Language.getValue("reqPassword");

            btSave.Text = Language.getValue("btSave");

            rgAdministrators.Columns[0].HeaderText = Language.getValue("rgAdministrators_username");
            rgAdministrators.Columns[1].HeaderText = Language.getValue("rgAdministrators_name");
            rgAdministrators.Columns[2].HeaderText = Language.getValue("rgAdministrators_email");

            lblAdminLanguage.Text = Language.getValue("lblAdminLanguage");

            lblUserGroup.Text = Language.getValue("lblUserGroup");

            lblMailNotifications.Text = Language.getValue("lblMailNotifications");
            lblCbxMailNotifications.Text = Language.getValue("lblCbxMailNotifications");

            foreach (ListItem li in ddAdminLanguage.Items)
                li.Text = Language.getValue("language" + li.Value);
        }

        protected void rgAdministrators_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            List<AIM.Business.AimAdministrator.Admin> admin = (from linq_admin in db.Admins
                                                               where linq_admin.siteID == config.siteID
                                                               select linq_admin).ToList<AIM.Business.AimAdministrator.Admin>();

            rgAdministrators.DataSource = admin;            
        }

        protected void rgAdministrators_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridCommandItem)
            {
                try
                {
                    GridCommandItem cmditm = (GridCommandItem)e.Item;
                    LinkButton lbNewAdmin = (LinkButton)cmditm.Controls[0].FindControl("lbNewAdmin");

                    lbNewAdmin.Text = "<img class=\"textimage\" src=\"/Images/newobject.gif\" alt=\"New administrator\" />&nbsp;"
                        + Language.getValue("lbNewAdmin");
                }
                catch { }
            }
            else if (e.Item is GridDataItem)
            {
                try
                {
                    GridDataItem rgDataItem = e.Item as GridDataItem;
                    ImageButton delButton = rgDataItem["DeleteButton"].Controls[0] as ImageButton;
                    delButton.Attributes["onclick"] = "return confirm('" + Language.getValue("DelAdminConfirm") + "')";
                }
                catch
                { 
                }
            }
         
        }

        protected void rgAdministrators_ItemCommand(object sender, GridCommandEventArgs e)
        {            
            switch (e.CommandName)
            {
                case "new":
                    viewForm(true);
                    reqPassword.Enabled = true;
                    ddAdminLanguage.SelectedIndex = 0;
                    rblGroups.SelectedValue = "1";
                    cbxMailNotifications.Checked = true;
                    lblFormTitle.Text = Language.getValue("lblFormTitle_new");
                    break;
                case "editadmin":
                    initAdminEdit(int.Parse(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString()));
                    viewForm(true);
                    reqPassword.Enabled = false;
                    lblFormTitle.Text = Language.getValue("lblFormTitle_edit");
                    break;
                case "removeadmin":
                    AdminDelete(int.Parse(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString()));
                    break;
            }            
        }

        /// <summary>
        /// Should edit panel be viewed
        /// </summary>
        /// <param name="view"></param>
        private void viewForm(bool view)
        {
            rgAdministrators.Visible = !view;
            pnlEdit.Visible = view;

            if (!view)
            {
                hfID.Value = "-1";
                tbUserName.Text = String.Empty;
                tbPassword.Text = String.Empty;
                tbFirstName.Text = String.Empty;
                tbLastName.Text = String.Empty;
                tbEmail.Text = String.Empty;
                rgAdministrators.MasterTableView.ClearSelectedItems();
            }
        }

        private void initAdminEdit(int adminId)
        {
            AIM.Business.AimAdministrator.usp_GetAdministratorResult adminUser = db.usp_GetAdministrator(adminId).First<AIM.Business.AimAdministrator.usp_GetAdministratorResult>();

            tbUserName.Text = adminUser.username;
            tbFirstName.Text = adminUser.firstname;
            tbLastName.Text = adminUser.lastname;
            tbEmail.Text = adminUser.email;
            ddAdminLanguage.SelectedValue = adminUser.AdminLanguage_FK.ToString();
            hfID.Value = adminId.ToString();

            var adminRole = (from o in db.AdminRoles
                             where o.AdminID == adminId
                             select o).First();

            rblGroups.SelectedValue = adminRole.RoleID.ToString();
            cbxMailNotifications.Checked = adminUser.mailNotifications != null && adminUser.mailNotifications == 1 ? true : false;
        }

        private void AdminDelete(int adminUserId)
        {            
            db.usp_DeleteAdministrator(adminUserId);
            rgAdministrators.Rebind();
        }

        protected void ibBack_OnClick(object sender, EventArgs e)
        {
            viewForm(false);
        }

        protected void btSave_OnClick(object sender, EventArgs e)
        {
            AIM.Business.AimAdministrator.Admin adminUser;

            //old user
            if (hfID.Value != "-1")
            {
                adminUser = (from linq_adminuser in db.Admins where linq_adminuser.ID == int.Parse(hfID.Value) select linq_adminuser).First<AIM.Business.AimAdministrator.Admin>();
                
                if (tbPassword.Text.Length > 0)
                    adminUser.password = tbPassword.Text;
                
            }
            else //new user
            {                
                adminUser = new AIM.Business.AimAdministrator.Admin();
                db.Admins.InsertOnSubmit(adminUser);
                adminUser.siteID = config.siteID;

                if (tbPassword.Text.Length > 0)
                    adminUser.password = tbPassword.Text;
            }
            
            adminUser.username = tbUserName.Text;
            adminUser.firstname = tbFirstName.Text;
            adminUser.lastname = tbLastName.Text;
            adminUser.email = tbEmail.Text;
            adminUser.AdminLanguage_FK = int.Parse(ddAdminLanguage.SelectedValue);
            adminUser.mailNotifications = cbxMailNotifications.Checked ? 1 : 0;
            db.SubmitChanges();

            foreach (ListItem li in rblGroups.Items)
                db.usp_SaveAdministratorInGroup(adminUser.ID, int.Parse(li.Value), Convert.ToInt32(li.Selected));

            viewForm(false);
            rgAdministrators.Rebind();
        }
    }
}
