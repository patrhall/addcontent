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

namespace AimPhoto
{
    public partial class PhotoCategoryCreate : AIM.Base.Modules.Page.AimPhoto
    {       
        private int objectID;

        protected override void Page_Load()
        {
            lblMsg.Text = "";

            objectID = Convert.ToInt32(Request["oID"]);
            hpBack.NavigateUrl = "CategoryList.aspx?oID=" + objectID.ToString();

            setLanguage();
        }

        private void setLanguage()
        {
            lblDate.Text = Language.getValue("lblDate");
            lblCategory.Text = Language.getValue("lblCategory");
            lblComment.Text = Language.getValue("lblComment");
            btCategoryName.Text = Language.getValue("btCategoryName");
            hpBack.Text = Language.getValue("hpBack");
        }

        //Har lagt till CategoryComment
        private string saveCategory(string CategoryName, string categoryDate, string CategoryCommment)
        {
            AIM.Business.AimPhoto.AlbumPhotoCategory cat = new AIM.Business.AimPhoto.AlbumPhotoCategory();
            cat.CategoryName = CategoryName;

            if (categoryDate != "")
                cat.CategoryDate = Convert.ToDateTime(categoryDate);
            else
                cat.CategoryDate = DateTime.Now;

            cat.ObjectID = objectID;
            cat.CategoryComment = CategoryCommment;

            db.AlbumPhotoCategories.InsertOnSubmit(cat);
            db.SubmitChanges();

            return cat.ID.ToString();
        }

        protected void btCategoryName_Click(object sender, EventArgs e)
        {
            if (tbCategoryName.Text != "")
            {
                string categoryid = saveCategory(tbCategoryName.Text, tbCategoryDate.Text, tbCComment.Text);

                if (Convert.ToBoolean(Preference.getValue("photoalbum", "multiupload", "used")))
                    Response.Redirect("PhotoMultipleAdmin.aspx" + Request.Url.Query + "&CategoryId=" + categoryid);
                else
                    Response.Redirect("PhotoUpload.aspx" + Request.Url.Query + "&CategoryId=" + categoryid);
            }
            else
                lblMsg.Text = Language.getValue("lblMsg");
        }
    }
}
