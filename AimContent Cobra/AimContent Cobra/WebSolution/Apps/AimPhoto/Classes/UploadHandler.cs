using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using Telerik.Web.UI;

namespace AimPhoto.Classes
{
    public class UploadHandler
    {
        private AIM.Base.Classes.Config config = new AIM.Base.Classes.Config();
        private AIM.Business.AimPhoto.DataObjectsDataContext db = new AIM.Business.AimPhoto.DataObjectsDataContext(AIM.Business.DataManager.ConnectionString);
        private AIM.Base.Classes.Preference Preference;

        private string imgPath;
        private string path_thumb;
        private string path_middle;
        private string path_slideshow;

        public UploadHandler()
        {
            config.GetConfig();
            imgPath = config.aimphotopath;
            path_thumb = imgPath + "thumbs/";
            path_middle = imgPath + "middle/";
            path_slideshow = imgPath + "slideshow/";

            Preference = AIM.Base.Classes.Preference.getPreference(config.customerpath);
        }

        /// <summary>
        /// Save file from TelerikControll
        /// </summary>
        /// <param name="file"></param>
        /// <param name="categoryId"></param>
        public void saveFile(UploadedFile file, int categoryId)
        {
            string imagesname = getImageName(file.FileName, categoryId);

            //Save orginal size picture
            file.SaveAs(imgPath + imagesname, true);

            saveImages(imagesname, categoryId);
        }

        /// <summary>
        /// Save file from Request stream
        /// </summary>
        /// <param name="file"></param>
        /// <param name="categoryId"></param>
        public void saveFile(HttpPostedFile file, int categoryId)
        {
            string imagesname = getImageName(file.FileName, categoryId);

            //Save orginal size picture
            file.SaveAs(imgPath + imagesname);

            saveImages(imagesname, categoryId);
        }

        /// <summary>
        /// Resize images and save to database
        /// </summary>
        /// <param name="imagesname"></param>
        /// <param name="categoryId"></param>
        private void saveImages(string imagesname, int categoryId)
        {
            //Save thumb and a smaller picture and a wonderfull slideshow version
            AimResources.ImageHandler.ResizeImageBox(imgPath, path_thumb, imagesname, imagesname, int.Parse(Preference.getValue("photoalbum", "imagesize_thumb", "width")), int.Parse(Preference.getValue("photoalbum", "imagesize_thumb", "height")), false);
            AimResources.ImageHandler.ResizeImageBox(imgPath, path_middle, imagesname, imagesname, int.Parse(Preference.getValue("photoalbum", "imagesize_middle", "width")), int.Parse(Preference.getValue("photoalbum", "imagesize_middle", "height")), false);
            AimResources.ImageHandler.ResizeImageBox(imgPath, path_slideshow, imagesname, imagesname, int.Parse(Preference.getValue("photoalbum", "imagesize_slideshow", "width")), int.Parse(Preference.getValue("photoalbum", "imagesize_slideshow", "height")), false);

            //Remove original, it is to big to save
            File.Delete(imgPath + imagesname);

            db.usp_AlbumPhoto_SaveItem(imagesname, categoryId);  
        }

        private string getImageName(string filename, int categoryId)
        {
            string imgExtension = Path.GetExtension(filename).ToLower();            

            if (imgExtension == ".jpeg" || imgExtension == ".gif" || imgExtension == ".jpg" || imgExtension == ".png")
            {
                return categoryId + "_" + DateTime.Now.ToString("yyMMddhhmm") + AimResources.StringFunctions.GenerateRandomLowerCaseString(6) + imgExtension;
            }
            else
                throw new Exception("Ej giltigt bildformat!");

        }
    }
}
