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
using AIM.Business.AimBusiness.ProceduralRacing.Website;
using System.IO;
using Telerik.Web.UI;
using System.Drawing;
using Image = System.Drawing.Image;

namespace AimBusiness.Customers.ProceduralRacing.Website
{
    public partial class Overview : AIM.Base.Modules.Page.AimBusiness.ProceduralRacing.Website.AimBusiness
    {
        private int objectId;

        protected override void Page_Load()
        {
            if (Request["oId"] != null)
                objectId = Convert.ToInt32(Request["oId"]);

            if (!IsPostBack)
            {
                FillGrid();
            }
        }

        private void FillGrid()
        {
            var articles = from a in db.Articles_Procedurals
                           where a.ObjectID == objectId
                           orderby a.Datum descending
                           select new { 
                            ArticleID = a.ArticleID,
                            Datum = a.Datum,
                            Rubrik = a.Rubrik
                           };

            rgCases.DataSource = articles;
            rgCases.DataBind();
            
        }

        protected void rgCases_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "new":
                    table1.Visible = true;
                    txtRubrik.Text = "";
                    txtGrupp.Text = "";
                    txtPaket.Text = "";
                    txtPris.Text = "";
                    txtLagersaldo.Text = "";
                    txtId.Text = "";
                    txtSortOrder.Text = "";
                    break;
                case "editCase":
                    this.FillEditForm(rgCases.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ArticleID"].ToString());
                    break;
                case "deleteCase":
                    this.DeleteCase(rgCases.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ArticleID"].ToString());
                    break;
                default:
                    break;
            }
        }

        private void FillEditForm(string Id)
        {
            Articles_Procedural article = (from a in db.Articles_Procedurals
                                        where a.ArticleID == Convert.ToInt32(Id)
                                        select a).Single<Articles_Procedural>();
                txtRubrik.Text = article.Rubrik;
                txtGrupp.Text = article.Grupp;
                txtPaket.Text = article.Paket;

                string fileName = article.Bild;
                string path = "/proceduralracing_data/articlepictures/";
                Image1.ImageUrl = path + fileName;
                txtPris.Text = article.Pris.ToString();
                txtLagersaldo.Text = article.LagerSaldo.ToString();
                RadDatePicker1.SelectedDate = article.Datum;
                RadTimePicker1.SelectedDate = article.Tid;
                txtId.Text = article.ArticleID.ToString();
                txtSortOrder.Text = article.SortOrder.ToString();
        }

        private void DeleteCase(string Id)
        {
            Articles_Procedural article = (from a in db.Articles_Procedurals
                                        where a.ArticleID == Convert.ToInt32(Id)
                                        select a).Single<Articles_Procedural>();
            db.Articles_Procedurals.DeleteOnSubmit(article);
            db.SubmitChanges();
            this.FillGrid();
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            saveCase();
        }

        private void saveCase()
        {
            
            if (txtId.Text == "")
            {
                Articles_Procedural article = new Articles_Procedural();
                article.Rubrik = txtRubrik.Text;
                article.Grupp = txtGrupp.Text;
                article.Paket = txtPaket.Text;
                article.Pris = Convert.ToInt32(txtPris.Text);
                article.LagerSaldo = Convert.ToInt32(txtLagersaldo.Text);
                article.Datum = RadDatePicker1.SelectedDate;
                article.Tid = RadTimePicker1.SelectedDate;
                article.SortOrder = Convert.ToInt32(txtSortOrder.Text);
                if (fuOriginalImage.HasFile)
                    article.Bild = this.SaveImage(fuOriginalImage);
                else
                    article.Bild = "noimage.jpg";

                article.ObjectID = objectId;

                db.Articles_Procedurals.InsertOnSubmit(article);
            }
            else
            {
                Articles_Procedural article = (from a in db.Articles_Procedurals where a.ArticleID == Convert.ToInt32(txtId.Text) select a).Single<Articles_Procedural>();
                 article.Rubrik = txtRubrik.Text;
                 article.Grupp = txtGrupp.Text;
                 article.Paket = txtPaket.Text;
                 article.Pris = Convert.ToInt32(txtPris.Text);
                 article.LagerSaldo = Convert.ToInt32(txtLagersaldo.Text);
                 article.Datum = RadDatePicker1.SelectedDate;
                 article.Tid = RadTimePicker1.SelectedDate;
                 article.SortOrder = Convert.ToInt32(txtSortOrder.Text);
                if (fuOriginalImage.HasFile)
                    article.Bild = this.SaveImage(fuOriginalImage);
            }

            db.SubmitChanges();

            this.FillGrid();
        }

        private string SaveImage(FileUpload fileUploader)
        {
            string fileName = fileUploader.FileName;
            string path = Server.MapPath(config.customerpath + "/articlepictures/");
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
            string extension = Path.GetExtension(fileName);
            if (File.Exists(path + fileName))
            {
                Int32 counter = 0;
                fileName = fileNameWithoutExt + counter.ToString() + extension;
                while (File.Exists(path + fileName))
                {
                    counter++;
                    fileName = fileNameWithoutExt + counter.ToString() + extension;
                }
            }
            Image origImage;
            using (MemoryStream stream = new MemoryStream(fileUploader.FileBytes))
            {
                origImage = Image.FromStream(fileUploader.FileContent);
            }
            Image saveImage;
            if (origImage.Height <= origImage.Width)
            {
                double d = (double)origImage.Height / (double)origImage.Width;
                saveImage = FixedSize(origImage, 320, (int)(320 * d));
            }
            else
            {
                double d = (double)origImage.Width / (double)origImage.Height;
                saveImage = FixedSize(origImage, (int)(320 * d), 320);
            }
            fileUploader.SaveAs(path + fileName);
            saveImage.Save(path + fileName);
            return fileName;
        }

        static Image FixedSize(Image imgPhoto, int Width, int Height)
        {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)Width / (float)sourceWidth);
            nPercentH = ((float)Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = System.Convert.ToInt16((Width -
                              (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((Height -
                              (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(Width, Height,
                              System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                             imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.White);
            grPhoto.InterpolationMode =
                    System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }

    }
}