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
using System.Drawing.Imaging;

namespace AimPhoto
{
    public partial class PhotoView : System.Web.UI.Page
    {
        private AIM.Business.AimPhoto.DataObjectsDataContext db;

        private AIM.Base.Classes.Config config;        

        /// <summary>
        /// Querystrings param:
        /// img = id image
        /// size = fs (fullsize), th (thumb), mi (middle)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            config = new AIM.Base.Classes.Config();
            config.GetConfig();

            try
            {
                string imageURL = getPicture(Request["img"]);

                string imageType = imageURL.Substring(imageURL.IndexOf(".") + 1);

                //Make sure the image url doesn´t contains any back or frontslash
                if (imageURL.IndexOf("/") >= 0 || imageURL.IndexOf("\\") >= 0)
                    throw new Exception("Illegal name");

                switch (Request["size"])
                {
                    case "fs":
                        imageURL = config.aimphotopath + imageURL;
                        break;
                    case "th":
                        imageURL = config.aimphotopath + "/thumbs/" + imageURL;
                        break;
                    case "mi":
                        imageURL = config.aimphotopath + "/middle/" + imageURL;
                        break;
                    default:
                        throw new Exception("Illegal size");
                }

                System.Drawing.Image fullSizeImg;

                //Set contentType
                switch (imageType)
                {
                    case "gif":
                        fullSizeImg = System.Drawing.Image.FromFile(imageURL);
                        Response.ContentType = "image/gif";
                        fullSizeImg.Save(Response.OutputStream, ImageFormat.Gif);
                        fullSizeImg.Dispose();
                        break;
                    case "jpg":
                        fullSizeImg = System.Drawing.Image.FromFile(imageURL);
                        Response.ContentType = "image/jpeg";
                        fullSizeImg.Save(Response.OutputStream, ImageFormat.Jpeg);
                        fullSizeImg.Dispose();
                        break;
                    case "bmp":
                        fullSizeImg = System.Drawing.Image.FromFile(imageURL);
                        Response.ContentType = "image/bmp";
                        fullSizeImg.Save(Response.OutputStream, ImageFormat.Bmp);
                        fullSizeImg.Dispose();
                        break;
                    case "png":
                        fullSizeImg = System.Drawing.Image.FromFile(imageURL);
                        Response.ContentType = "image/png";
                        fullSizeImg.Save(Response.OutputStream, ImageFormat.Png);
                        fullSizeImg.Dispose();
                        break;
                    default:
                        throw new Exception("Illegal imageType");
                }
            }
            catch
            {
                Response.End();
            }
        }

        /// <summary>
        /// Get picture name. throws an exception if user has no access
        /// </summary>
        /// <param name="imageId"></param>
        private string getPicture(string imageId)
        {
            db = new AIM.Business.AimPhoto.DataObjectsDataContext(AIM.Business.DataManager.ConnectionString);
            return db.usp_AlbumPhoto_GetItem(int.Parse(imageId), null).First<AIM.Business.AimPhoto.usp_AlbumPhoto_GetItemResult>().Filename;
        }
    }
}
