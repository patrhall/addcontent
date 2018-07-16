using System;
using System.Collections;
using System.Collections.Generic;
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
using System.Data.Linq;
using Telerik.Web.UI;
using AIM.General.UserControls;

namespace AimSiteNews
{
    public partial class Overview : AIM.Base.Modules.Page.AimSiteNews
    {
        private int _newsListID;
        AIM.General.UserControls.PagePreference pref;

        protected override void Page_Load()
        {
            _newsListID = int.Parse(Request["oID"]);       
                     
            setLanguage();

            loadPagePreference();

            lblMessage.Visible = !String.IsNullOrEmpty(Request["finsihed"]);                
        }

        private void setLanguage()
        {
            lbPreference.Text = Language.getValue("lbPreference");

            rgNews.Columns[0].HeaderText = Language.getValue("rgNews_created"); 
            rgNews.Columns[1].HeaderText = Language.getValue("rgNews_title"); 
            rgNews.Columns[2].HeaderText = Language.getValue("rgNews_email"); 
            rgNews.Columns[3].HeaderText = Language.getValue("rgNews_entries");

            lblMessage.Text = Language.getValue("lblMessage");
        }

        private int getFormID(int objectID)
        {
            var reg = db.usp_Registration_GetObjectForm(objectID);

            if (reg.Count<AIM.Business.AimSiteNews.usp_Registration_GetObjectFormResult>() != 0)
                return reg.First<AIM.Business.AimSiteNews.usp_Registration_GetObjectFormResult>().FormID;

            return 0;
        }

        private void loadPagePreference()
        {
            if (!AdministratorAuthentication.IsUserAdmin)
            {
                phPagePreference.Visible = false;
                lbPreference.Visible = false;
            }

            Control controlPagePreference = LoadControl("/General/UserControls/PagePreference.ascx");
            pref = (AIM.General.UserControls.PagePreference)controlPagePreference;
            pref.setObjectID(_newsListID);
            phPagePreference.Controls.Add(pref);
        }

        /// <summary>
        /// Init pagepreference
        /// </summary>
        private void initPagePreference()
        {
            pref.viewUrl = Boolean.Parse(Preference.getValue("content", "directurl", "used"));

            switch (Preference.getValue("content", "directurl", "urlrewrite"))
            {
                case "NOTUSED":
                    pref.viewUrlRewrite = PagePreference.UrlRewriteOptions.NOTUSED;
                    break;
                case "NOMASTER":
                    pref.viewUrlRewrite = PagePreference.UrlRewriteOptions.NOMASTER;
                    break;
                case "MASTER":
                    pref.viewUrlRewrite = PagePreference.UrlRewriteOptions.MASTER;
                    break;
            }

            //Load preference
            pref.init();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbPreference_OnClick(object sender, EventArgs e)
        {
            phPagePreference.Visible = true;
            pnlContent.Visible = false;
            initPagePreference();
        }

        protected void rgNews_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }

        protected void rgNews_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgNews.DataSource = (from o in db.Objects
                                 where o.ListID == _newsListID
                                 orderby o.Created descending
                                 select o).ToList();
        }

        protected void rgNews_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridCommandItem)
            {
                try
                {
                    GridCommandItem cmditm = (GridCommandItem)e.Item;
                    LinkButton lbNewAdmin = (LinkButton)cmditm.Controls[0].FindControl("lbNew");

                    lbNewAdmin.Text = "<img class=\"textimage\" src=\"/Images/newobject.gif\" alt=\"" + Language.getValue("lbNew") + "\" />&nbsp;"
                        + Language.getValue("lbNew");
                }
                catch { }
            }
        }

        protected void rgNews_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "new":
                    Response.Redirect("/Apps/AimSiteNews/CreateSiteNews.aspx?ID=0&ListID=" + _newsListID);
                    break;
                case "editadmin":
                    Response.Redirect("/Apps/AimSiteNews/CreateSiteNews.aspx?ID=" + rgNews.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ObjectID"].ToString() + "&ListID=" + _newsListID);
                    break;
                case "removeadmin":
                    var adminRoles = (from o in db.Object_X_AdminRoles
                                      where o.ObjectId == int.Parse(rgNews.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ObjectID"].ToString())
                                      select o);
                    db.Object_X_AdminRoles.DeleteAllOnSubmit(adminRoles);
                    db.SubmitChanges();

                    db.usp_DeleteObjectRoles(int.Parse(rgNews.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ObjectID"].ToString()));
                    db.usp_DeleteNews(int.Parse(rgNews.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ObjectID"].ToString()));
                    rgNews.Rebind();                                    
                    break;
            }
            
        }
    }
}
