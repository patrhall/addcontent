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
    public partial class Sponsors : AIM.Base.Modules.Page.AimBusiness.CongrexAPP.Website.AimBusiness
    {

        DataObjectsDataContext dbCongrex = new DataObjectsDataContext();
        int sId;

        protected override void Page_Load()
        {

        }

        protected void SaveSponsor(object sender, System.EventArgs e)
        {
            Sponsor newSponsor = new Sponsor();
            newSponsor.Name = txtName.Text;
            newSponsor.Link = txtLink.Text;
            newSponsor.Site_FK = 116;
            newSponsor.SortOrder = 0;
            newSponsor.LastUpdated = DateTime.Now;
            dbCongrex.Sponsors.InsertOnSubmit(newSponsor);
            dbCongrex.SubmitChanges();

            SaveImage(FileUpload1, newSponsor.Id.ToString());
        }

        private string SaveImage(FileUpload fileUploader, string id)
        {
            string origfileName = fileUploader.FileName;
            string fileName = id;
            string path = Server.MapPath(config.customerpath + "/sponsors/");
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