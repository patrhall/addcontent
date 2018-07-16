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
    public partial class PhotoMultipleUpload : AIM.Base.Modules.Page.AimPhoto
    {
        protected override void Page_Load()
        {
            Classes.UploadHandler uploadHandler = new AimPhoto.Classes.UploadHandler();

            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFile myFile = Request.Files[i];

                if (myFile != null && myFile.FileName != "")
                    uploadHandler.saveFile(myFile, int.Parse(Request["CategoryID"]));
            }             
        }
    }
}
