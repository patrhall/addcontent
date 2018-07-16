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
using System.Data.Linq;
using AIM.General.UserControls;

namespace AimPhoto
{
    public partial class CategoryList : AIM.Base.Modules.Page.AimPhoto
    {
        private int objectID;
        AIM.General.UserControls.PagePreference pref;

        public int typeID;
        public string topic;

        protected override void Page_Load()
        {
            objectID = Convert.ToInt32(Request["oID"]);
                    
            loadPagePreference();

            setLanguage();
        }

        private void setLanguage()
        {
            rgCategories.MasterTableView.NoMasterRecordsText = Language.getValue("rgCategories_NoMasterRecordsText");
            rgCategories.Columns[0].HeaderText = Language.getValue("rgCategories_c1_headertext");
            rgCategories.Columns[1].HeaderText = Language.getValue("rgCategories_c2_headertext");
            lbPreference.Text = Language.getValue("lbPreference");
        }

        private void loadPagePreference()
        {            
            Control controlPagePreference = LoadControl("/General/UserControls/PagePreference.ascx");
            pref = (PagePreference)controlPagePreference;
            pref.setObjectID(objectID);
            phPagePreference.Controls.Add(pref);
        }

        protected void rgCategories_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "open")
            {
                Response.Redirect("PhotoCategory.aspx?oID=" + objectID.ToString() + "&CategoryId=" + e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString());
            }
            else if (e.CommandName == "del")
            {
                db.usp_AlbumPhoto_DeleteCategories(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString()));

                rgCategories.Rebind();
            }
        }

        protected void rgCategories_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridCommandItem)
            {
                try
                {
                    GridCommandItem cmditm = (GridCommandItem)e.Item;
                    LinkButton newButton = (LinkButton)cmditm.Controls[0].FindControl("lbCreateCategory");

                    newButton.Text = Language.getValue("lbCreateCategory");
                }
                catch { }
            } 
            else if (e.Item is GridDataItem)
            {
                e.Item.Cells[2].Attributes.Add("onmouseover", "this.style.cursor = 'pointer';");
                e.Item.Cells[3].Attributes.Add("onmouseover", "this.style.cursor = 'pointer';");

                try
                {
                    GridDataItem rgDataItem = e.Item as GridDataItem;
                    ImageButton delButton = rgDataItem["Action2"].Controls[0] as ImageButton;
                    delButton.Attributes["onclick"] = "return confirm('" + Language.getValue("delCategoryConfirm") + "')";
                }
                catch
                { }
            }
        }

        protected void rgCategories_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(AimResources.Collections.List2DataTable<AIM.Business.AimPhoto.usp_AlbumPhoto_GetCategoriesResult>(db.usp_AlbumPhoto_GetCategories(objectID, null).ToList<AIM.Business.AimPhoto.usp_AlbumPhoto_GetCategoriesResult>()));
            
            ds.Tables[0].Columns.Add("img", System.Type.GetType("System.String"));

            if (ds.Tables[0].Rows.Count != 0)
            {
                //Set filepath to image
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dr["img"] = "PhotoView.aspx?size=th&img=" + dr["AlbumCover"].ToString();
                }

                rgCategories.DataSource = ds;
            }
            else
                rgCategories.DataSource = ds;
        }

        protected void lbCreateCategory_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("PhotoCategoryCreate.aspx?oID=" + objectID.ToString());
        }

        protected void rgCategories_ItemDataBound1(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem gridDataItem = (GridDataItem)e.Item;
                string temp = gridDataItem["AlbumCover"].Text.ToString();//.Text.ToString();
                temp = temp.Remove(0, temp.IndexOf('>') + 1);
                temp = temp.Replace("<", "");
                temp = temp.Replace("n", "");
                temp = temp.Replace("o", "");
                temp = temp.Replace("b", "");
                temp = temp.Replace("r", "");
                temp = temp.Replace(">", "");
                temp = temp.Replace("/", "");

                temp = temp.Replace("&", "");
                temp = temp.Replace("b/", "");
                temp = temp.Replace("s", "");
                temp = temp.Replace("p", "");
                temp = temp.Replace(";", "");

                Image image = (Image)gridDataItem["Image"].FindControl("AlbumImg");
                ImageButton imageB = (ImageButton)gridDataItem["Image"].FindControl("AlbumImgB");

                if (temp != "")
                {
                    image.ImageUrl = "PhotoView.aspx?size=th&img=" + temp;
                    imageB.ImageUrl = "PhotoView.aspx?size=th&img=" + temp;
                }
                else
                    image.Visible = false;
            }
        }
        
        protected void lbPreference_OnClick(object sender, EventArgs e)
        {
            phPagePreference.Visible = true;
            pnlContent.Visible = false;
            initPagePreference();
        }

        /// <summary>
        /// Init pagepreference
        /// </summary>
        private void initPagePreference()
        {           
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
    }
}
