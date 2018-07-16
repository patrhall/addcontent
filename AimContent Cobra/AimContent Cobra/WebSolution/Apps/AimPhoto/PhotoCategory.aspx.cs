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
using System.Data.Linq;
using System.Collections.Generic;

namespace AimPhoto
{
    public partial class PhotoCategory : AIM.Base.Modules.Page.AimPhoto
    {
        public string albumcover;

        private int objectID
        {
            get
            {
                return Convert.ToInt32(Session["objectid"]);
            }

            set
            {
                Session["objectid"] = value;
            }
        }
        

        private int? categoryID
        {
            get
            {
                if (Session["CategoryId"] == null)
                    return null;
                else
                    return Convert.ToInt32(Session["CategoryId"]);
            }
            set
            {
                Session["CategoryId"] = value;
            }
        }

        protected override void Page_Load()
        {

            objectID = Convert.ToInt32(Request["oID"].ToString());
            categoryID = Convert.ToInt32(Request["CategoryId"]);

            if (!IsPostBack)
            {
                populateItemList();
                populateRoleList();

                if (Convert.ToBoolean(Preference.getValue("photoalbum", "multiupload", "used")))
                    hpUploadPics.NavigateUrl = "PhotoMultipleAdmin.aspx?CategoryId=" + categoryID.Value.ToString() + "&oID=" + objectID;
                else
                    hpUploadPics.NavigateUrl = "PhotoUpload.aspx?CategoryId=" + categoryID.Value.ToString() + "&oID=" + objectID;

                hpBack.NavigateUrl = "CategoryList.aspx?oID=" + objectID;
            }

            setLanguage();
        }

        private void setLanguage()
        {
            lblCategoryName.Text = Language.getValue("lblCategoryName");
            lblCategoryComment.Text = Language.getValue("lblCategoryComment");
            hpBack.Text = Language.getValue("hpBack");
            btnCategoryName.Text = Language.getValue("btnCategoryName");
            lblRoleTitle.Text = Language.getValue("lblRoleTitle");
            btRoles.Text = Language.getValue("btRoles");
            lblPicTitle.Text = Language.getValue("lblPicTitle");
            lblPicDescription.Text = Language.getValue("lblPicDescription");
            btnSavePicCom.Text = Language.getValue("btnSavePicCom");
            btnSavePic.Text = Language.getValue("btnSavePic");
            hpUploadPics.Text = "<img src='/Images/upload.gif' alt='" + Language.getValue("hpUploadPics") + "' /> " + Language.getValue("hpUploadPics");
        }

        private void populateRoleList()
        {            
            List<AIM.Business.AimPhoto.usp_AlbumPhoto_GetRolesResult> albumRoles = db.usp_AlbumPhoto_GetRoles(categoryID).ToList<AIM.Business.AimPhoto.usp_AlbumPhoto_GetRolesResult>();

            cblRoles.DataSource = Roles.GetAllRoles();            
            cblRoles.DataBind();

            for (int i = 0; i < cblRoles.Items.Count; i++)
            {
                foreach (AIM.Business.AimPhoto.usp_AlbumPhoto_GetRolesResult roleResult in albumRoles)
                {
                    if (roleResult.RoleName == cblRoles.Items[i].Value)
                    {
                        cblRoles.Items[i].Selected = true;
                    }
                }
            }
        }

        protected void populateItemList()
        {
            List<AIM.Business.AimPhoto.AlbumPhoto> albumPhotos =
            (
                from linq_o in db.AlbumPhotos
                where linq_o.PhotoCategoryID == categoryID.Value && !linq_o.AlbumPhotoCategory.Deleted && !linq_o.Deleted
                orderby linq_o.Sort.Value
                select linq_o).ToList<AIM.Business.AimPhoto.AlbumPhoto>();

            gvItems.EmptyDataText = "Det finns inga bilder i denna kategori.";

            AIM.Business.AimPhoto.usp_AlbumPhoto_GetCategoryInfoResult categoryInfo = db.usp_AlbumPhoto_GetCategoryInfo(categoryID).First<AIM.Business.AimPhoto.usp_AlbumPhoto_GetCategoryInfoResult>();
            tbCategoryName.Text = categoryInfo.CategoryName;
            tbCategoryComment.Text = categoryInfo.CategoryComment;

            if (albumPhotos.Count != 0)
            {
                //Set filepath to image
                foreach (AIM.Business.AimPhoto.AlbumPhoto image in albumPhotos)
                {
                    image.Filename = "PhotoView.aspx?size=th&img=" + image.ID;
                }

                if (categoryInfo.AlbumCover.HasValue)
                    imgAlbumCover.ImageUrl = "PhotoView.aspx?size=th&img=" + categoryInfo.AlbumCover;
                else
                    imgAlbumCover.ImageUrl = "~/images/NA.jpg";

                //Spara ID för AlbumCover
                if (categoryInfo.AlbumCover.HasValue)
                    lblAlbumCover.Text = categoryInfo.AlbumCover.Value.ToString();

                gvItems.DataSource = albumPhotos;
                gvItems.DataBind();
            }
            else
            {
                lblCategory.Text = "Inga bilder i kategorin";
                imgAlbumCover.ImageUrl = "~/images/NA.jpg";
            }
        }

        protected void gvItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                AIM.Business.AimPhoto.AlbumPhoto dv = (AIM.Business.AimPhoto.AlbumPhoto)e.Row.DataItem;

                TextBox _myTextBox = (TextBox)e.Row.FindControl("gvTbPicDesc");
                if (_myTextBox != null)
                    _myTextBox.Text = dv.Comment;

                LinkButton lbUp = (LinkButton)e.Row.Cells[3].Controls[0];
                lbUp.Text = Language.getValue("gvItems_up");

                LinkButton lbDown = (LinkButton)e.Row.Cells[4].Controls[0];
                lbDown.Text = Language.getValue("gvItems_down");

                LinkButton lbCover = (LinkButton)e.Row.Cells[5].Controls[0];
                lbCover.Text = Language.getValue("gvItems_cover");

                LinkButton lbDelete = (LinkButton)e.Row.Cells[6].Controls[0];
                lbDelete.Text = Language.getValue("gvItems_del");
            }
        }

        protected void btRoles_OnClick(object sender, EventArgs e)
        {
            for (int i = 0; i < cblRoles.Items.Count; i++)
            {
                if (cblRoles.Items[i].Selected)
                    saveUserInGroup(cblRoles.Items[i].Text);
                else
                    deleteUserInGroup(cblRoles.Items[i].Text);
            }
        }

        protected void gvCategoryListRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "UP")
                    db.usp_AlbumPhoto_SwitchPicture(int.Parse(gvItems.DataKeys[Convert.ToInt32(e.CommandArgument.ToString())].Value.ToString()), int.Parse(gvItems.DataKeys[Convert.ToInt32(e.CommandArgument.ToString()) - 1].Value.ToString()));
                else if (e.CommandName == "DOWN")
                    db.usp_AlbumPhoto_SwitchPicture(int.Parse(gvItems.DataKeys[Convert.ToInt32(e.CommandArgument.ToString()) + 1].Value.ToString()), int.Parse(gvItems.DataKeys[Convert.ToInt32(e.CommandArgument.ToString())].Value.ToString()));
                else if (e.CommandName == "del")
                    db.usp_AlbumPhoto_DeleteItem(int.Parse(gvItems.DataKeys[Convert.ToInt32(e.CommandArgument.ToString())].Value.ToString()));
                else if (e.CommandName == "setAlbumCover")
                {
                    db.usp_AlbumPhoto_UpdateCategoryComment(categoryID, tbCategoryName.Text, tbCategoryComment.Text, int.Parse(gvItems.DataKeys[Convert.ToInt32(e.CommandArgument.ToString())].Value.ToString()));

                    lblUpdated.Text = "Kommentarerna har sparats";
                    lblUpdated.Visible = true;

                    imgAlbumCover.ImageUrl = "PhotoView.aspx?size=th&img=" + gvItems.DataKeys[Convert.ToInt32(e.CommandArgument.ToString())].Value.ToString();
                }
            }
            catch
            {
            }

            populateItemList();
        }

        protected void gvItemsDescTextChanged(object sender, EventArgs e)
        {
        }

        private void saveUserInGroup(string roleName)
        {
            db.usp_AlbumPhoto_SetRoles(categoryID, roleName, config.applicationname);            
        }
        private void deleteUserInGroup(string roleName)
        {
            db.usp_AlbumPhoto_DeleteRoles(categoryID, roleName, config.applicationname);            
        }
        protected void btnCategoryName_Click(object sender, EventArgs e)
        {
            if (tbCategoryName.Text != "")
            {
                if (lblAlbumCover.Text != "")
                    db.usp_AlbumPhoto_UpdateCategoryComment(categoryID, tbCategoryName.Text, tbCategoryComment.Text, Convert.ToInt32(lblAlbumCover.Text));
                else
                    db.usp_AlbumPhoto_UpdateCategoryComment(categoryID, tbCategoryName.Text, tbCategoryComment.Text, null);

                for (int i = 0; i < cblRoles.Items.Count; i++)
                {
                    if (cblRoles.Items[i].Selected)
                        saveUserInGroup(cblRoles.Items[i].Value);
                    else
                        deleteUserInGroup(cblRoles.Items[i].Value);
                }

                lblUpdated.Text = "Uppdaterad";
                lblUpdated.ForeColor = System.Drawing.Color.Green;
                lblUpdated.Visible = true;
            }
            else
            {
                lblUpdated.Text = "Du måste fylla i ett namn på kategorin";
                lblUpdated.ForeColor = System.Drawing.Color.Red;
                lblUpdated.Visible = true;
            }
        }

        protected void btnSavePicCom_Click(object sender, EventArgs e)
        {
            Button btnB = (Button)sender;
            GridViewRow row = ((Button)sender).Parent.Parent as GridViewRow;
            TextBox txtBox = (TextBox)gvItems.Rows[row.RowIndex].FindControl("gvTbPicDesc");

            db.usp_AlbumPhoto_UpdateComment(Convert.ToInt32(gvItems.DataKeys[row.RowIndex].Value), txtBox.Text);
        }

        protected void btnSavePic_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in gvItems.Rows)
            {
                string currentText = ((TextBox)gvr.FindControl("gvTbPicDesc")).Text;
                db.usp_AlbumPhoto_UpdateComment(Convert.ToInt32(gvItems.DataKeys[gvr.RowIndex].Value), currentText);
            }
        }
    }
}
