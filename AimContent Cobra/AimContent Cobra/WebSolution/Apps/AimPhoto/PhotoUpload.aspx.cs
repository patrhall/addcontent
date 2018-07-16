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

namespace AimPhoto
{
    public partial class PhotoUpload : AIM.Base.Modules.Page.AimPhoto
    {
        protected override void Page_Load()
        {       
            if((from o in db.AlbumPhotoCategories where o.ID == int.Parse(Request["CategoryId"]) && o.ObjectID == int.Parse(Request["oID"]) select o).Count<AIM.Business.AimPhoto.AlbumPhotoCategory>() == 0)
                throw new Exception("Not authorized to this category");

            initLanguage();
        }

        private void initLanguage()
        {
            ruUpload.AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".bmp", ".png", ".gif" };
            ruUpload.ControlObjectsVisibility = ControlObjectsVisibility.AddButton | ControlObjectsVisibility.RemoveButtons;            
            ruUpload.InputSize = 100;
            ruUpload.MaxFileInputsCount = 30;

            btnSubmit.Text = Language.getValue("btnSubmit");
            hpBack.Text = Language.getValue("hpBack");            
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Classes.UploadHandler uploadHandler = new AimPhoto.Classes.UploadHandler();
            int categoryId = int.Parse(Request["CategoryId"]);

            foreach (UploadedFile file in ruUpload.UploadedFiles)
                uploadHandler.saveFile(file, categoryId);

            Response.Redirect("PhotoCategory.aspx?CategoryId=" + Request["CategoryId"] + "&oID=" + Request["oID"]);
        }
    }
}
