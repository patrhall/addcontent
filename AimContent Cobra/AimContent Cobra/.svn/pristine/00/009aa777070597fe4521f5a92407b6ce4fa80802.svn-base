using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AIM.Business.AimBusiness.CongrexAPP.Website;
using System.Drawing;
using Image = System.Drawing.Image;
using System.IO;

namespace AimBusiness.Customers.CongrexAPP.Website
{
    public partial class Exhibitors : AIM.Base.Modules.Page.AimBusiness.CongrexAPP.Website.AimBusiness
    {
        DataObjectsDataContext dbCongrex = new DataObjectsDataContext();

        protected override void Page_Load()
        {

        }

        protected void SaveExhibitor(object sender, System.EventArgs e)
        {
            Exhibitor newExhibitor = new Exhibitor();
            newExhibitor.Name = txtName.Text;
            newExhibitor.Email = txtMail.Text;
            newExhibitor.Monter = txtMonter.Text;
            newExhibitor.Place = txtPlace.Text;
            newExhibitor.Site_FK = 116;
            newExhibitor.SortOrder = 0;
            newExhibitor.LastUpdated = DateTime.Now;
            dbCongrex.Exhibitors.InsertOnSubmit(newExhibitor);
            dbCongrex.SubmitChanges();

            SaveImage(FileUpload1, newExhibitor.Id.ToString());
        }

        private string SaveImage(FileUpload fileUploader, string id)
        {
            string origfileName = fileUploader.FileName;
            string fileName = id;
            string path = Server.MapPath(config.customerpath + "/exhibitors/");
            string extension = Path.GetExtension(origfileName);

            if (extension == ".png" || extension == ".jpg")
            {
                Image origImage;
                using (MemoryStream stream = new MemoryStream(fileUploader.FileBytes))
                {
                    origImage = Image.FromStream(fileUploader.FileContent);
                }
                fileName += extension;
                fileUploader.SaveAs(path + fileName);
            }
            return fileName;
        }
    }
}