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
using ProfileBuilder;
using System.Data.Linq;

namespace AimUsers
{
    public partial class Users : AIM.Base.Modules.Page.AimUsers
    {
        protected override void Page_Load()
        {
            setLanguage();
        }

        private void setLanguage()
        {
            lblUserName.Text = Language.getValue("lblUserName");
            reqValUsername.Text = Language.getValue("reqValUsername");
            lblPassword.Text = Language.getValue("lblPassword");
            reqValPassword.Text = Language.getValue("reqValPassword");
            lblEditEmail.Text = Language.getValue("lblEditEmail");
            lblFirstname.Text = Language.getValue("lblFirstname");
            lblLastname.Text = Language.getValue("lblLastname");
            lblEditVerify.Text = Language.getValue("lblEditVerify");
            lblRoles.Text = Language.getValue("lblRoles");
            btnSave.Text = Language.getValue("btnSave");
        }

        protected void gvList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            rgUsers.DataSource = getDataSource();

            if (!Boolean.Parse(Preference.getValue("users", "verify", "used")))
                rgUsers.Columns[2].Visible = false;

            if (!Boolean.Parse(Preference.getValue("users", "name", "used")))
            {
                rgUsers.Columns[1].Visible = false;
                rgUsers.Width = Unit.Pixel(700);
            }
        }

        /// <summary>
        /// Get the data for all users
        /// </summary>
        /// <returns></returns>
        private DataTable getDataSource()
        {
            List<UserSort> listuser = new List<UserSort>();

            MembershipUserCollection muc = Membership.GetAllUsers();            

            foreach (MembershipUser m in muc)
            {
                WebProfile p = WebProfile.GetProfile(m.UserName);
                listuser.Add(new UserSort(p.UserName, m.IsApproved, m.CreationDate, p.AimUsers.FirstName, p.AimUsers.LastName));                
            }

            listuser.Sort(UserSort.sortUsers);

            DataTable dt = new DataTable();
            dt.Columns.Add("UserName");
            dt.Columns.Add("isApproved");
            dt.Columns.Add("CreationDate");
            dt.Columns.Add("NameFirst");
            dt.Columns.Add("NameLast");

            foreach (UserSort us in listuser)
                dt.Rows.Add(us.getValues());

            return dt;
        }

        protected void gvList_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "CHANGE")
            {
                viewForm(true);
                fillEdit(rgUsers.MasterTableView.DataKeyValues[e.Item.ItemIndex]["UserName"].ToString());
            }
            else if (e.CommandName == "REMOVE")
            {                
                Membership.DeleteUser(rgUsers.MasterTableView.DataKeyValues[e.Item.ItemIndex]["UserName"].ToString(), true);
                rgUsers.Rebind();
            }
        }

        protected void gvList_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                e.Item.Cells[6].Attributes.Add("onmouseover", "this.style.cursor = 'pointer';");
                e.Item.Cells[7].Attributes.Add("onmouseover", "this.style.cursor = 'pointer';");
            }
        }

        protected void lbNewUser_OnClick(object sender, EventArgs e)
        {
            viewForm(true);            
        }

        protected void ibBack_Click(object sender, EventArgs e)
        {
            viewForm(false);
        }

        private void fillEdit(string username)
        {
            hfEditId.Value = username;

            WebProfile p = WebProfile.GetProfile(username);
            MembershipUser mu = Membership.GetUser(username);

            listGroups.DataSource = Roles.GetAllRoles();
            listGroups.DataBind();

            //Select roles for user
            foreach (string role in Roles.GetRolesForUser(username))
                foreach (ListItem li in listGroups.Items)
                    if (role == li.Value)
                        li.Selected = true;
            
            tbUserName.Text = username;
            tbFirstname.Text = p.AimUsers.FirstName;
            tbLastname.Text = p.AimUsers.LastName;
            tbEmail.Text = mu.Email;
            reqValPassword.Enabled = false;

            if (Boolean.Parse(Preference.getValue("users", "verify", "used")))
            {
                cbVerify.Checked = mu.IsApproved;
                lblEditVerify.Visible = true;
                cbVerify.Visible = true;
            }
            else
            {
                lblEditVerify.Visible = false;
                cbVerify.Visible = false;
            }

            tbEmail.Visible = Boolean.Parse(Preference.getValue("users", "email", "used"));
            lblEditEmail.Visible = Boolean.Parse(Preference.getValue("users", "email", "used"));
        }

        private void viewForm(bool view)
        {
            tbUserName.Text = String.Empty;
            tbEmail.Text = String.Empty;
            tbFirstname.Text = String.Empty;
            tbLastname.Text = String.Empty;
            tbPassword.Text = String.Empty;
            hfEditId.Value = "-1";
            listGroups.ClearSelection();

            rgUsers.Visible = !view;
            pnlEdit.Visible = view;

            if (!view)
                rgUsers.Rebind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            MembershipUser userToSave;

            if (hfEditId.Value == "-1")
                userToSave = Membership.CreateUser(tbUserName.Text, tbPassword.Text, tbEmail.Text);
            else
            {
                if (hfEditId.Value != tbUserName.Text)
                {
                    //update username
                    AIM.Business.AimUsers.aspnet_User user = (from o in db.aspnet_Users
                                                              where o.UserName == hfEditId.Value &&
                                                              o.aspnet_Application.ApplicationName == config.applicationname
                                                              select o).First<AIM.Business.AimUsers.aspnet_User>();

                    user.UserName = tbUserName.Text;
                    user.LoweredUserName = tbUserName.Text.ToLower();
                    db.SubmitChanges();
                }

                userToSave = Membership.GetUser(tbUserName.Text);

                if (tbPassword.Text.Trim() != "")
                    userToSave.ChangePassword(userToSave.ResetPassword(), tbPassword.Text);
            }

            WebProfile p = WebProfile.GetProfile(userToSave.UserName);

            p.AimUsers.FirstName = tbFirstname.Text;
            p.AimUsers.LastName = tbLastname.Text;            
            userToSave.Email = tbEmail.Text;

            //Remove or add user to role
            foreach (ListItem li in listGroups.Items)
                if (li.Selected && !Roles.IsUserInRole(userToSave.UserName, li.Value))
                    Roles.AddUserToRole(userToSave.UserName, li.Value);
                else if (!li.Selected && Roles.IsUserInRole(userToSave.UserName, li.Value))
                    Roles.RemoveUserFromRole(userToSave.UserName, li.Value);
            
            if (Boolean.Parse(Preference.getValue("users", "verify", "used")))
                userToSave.IsApproved = cbVerify.Checked;

            p.Save();
            Membership.UpdateUser(userToSave);

            viewForm(false);
        }

        /// <summary>
        /// Used for a list to sort users by different parameters
        /// </summary>
        private class UserSort
        {
            public enum SortBy { NAME, USERNAME };

            public static SortBy sortby = SortBy.USERNAME;

            public UserSort(string username, bool isApproved, DateTime CreationDate, string firstname, string lastname)
            {
                this.username = username;
                this.isApproved = isApproved;
                this.CreationDate = CreationDate;
                this.firstname = firstname;
                this.lastname = lastname;
            }

            /// <summary>
            /// Used in List to sort
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>
            public static int sortUsers(UserSort x, UserSort y)
            {
                if (sortby == SortBy.NAME)
                {
                    if (x.lastname.CompareTo(y.lastname) != 0)
                        return x.lastname.CompareTo(y.lastname);
                    else
                        return x.firstname.CompareTo(y.firstname);
                }
                else if (sortby == SortBy.USERNAME)
                {
                    if (x.username.CompareTo(y.username) != 0)
                        return x.username.CompareTo(y.username);
                    else
                    {
                        if (x.lastname.CompareTo(y.lastname) != 0)
                            return x.lastname.CompareTo(y.lastname);
                        else
                            return x.firstname.CompareTo(y.firstname);
                    }
                }


                return -1;
            }

            private string username;
            private bool isApproved;
            private DateTime CreationDate;
            private string firstname;
            private string lastname;

            /// <summary>
            /// Return all values as a string array
            /// </summary>
            /// <returns></returns>
            public string[] getValues()
            {
                string[] str = new string[5];
                str[0] = username;
                str[1] = isApproved.ToString();
                str[2] = CreationDate.ToShortDateString();
                str[3] = firstname;
                str[4] = lastname;

                return str;
            }
        }
    }
}
