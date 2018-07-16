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
using AIM.Business.AimBusiness.AimFor2022.Website;
using System.IO;
using Telerik.Web.UI;
using System.Drawing;
using Image = System.Drawing.Image;

namespace AimBusiness.Customers.AimFor2022.Website
{
    public partial class Overview : AIM.Base.Modules.Page.AimBusiness.AimFor2022.Website.AimBusiness
    {
        private Int32 lastPosition;
        private Int32 firstPosition;

        protected override void Page_Load()
        {
            if (!IsPostBack)
            {
                this.FillGrid();
                this.FillSettings();
            }
        }

        private void FillGrid()
        {
            if (db.Articles_AimFor2022s.Count<Articles_AimFor2022>() > 0)
            {
                lastPosition = (from a in db.Articles_AimFor2022s
                                select a.Position).Max();
                firstPosition = (from a in db.Articles_AimFor2022s
                                 select a.Position).Min();
            }

            rgCases.DataSource = from _case in db.Articles_AimFor2022s
                                 orderby _case.Position
                                 select _case;
            rgCases.DataBind();
        }

        private void FillSettings()
        {
            XDocument doc = XDocument.Load(Server.MapPath(config.customerpath) + "xml/settings.xml");
            XElement elem = doc.Element("settings");
            tbColumns.Text = elem.Attribute("rows").Value;
            tbRows.Text = elem.Attribute("columns").Value;
        }

        protected void btnUpload_OnClick(object sender, EventArgs e)
        {
            string ext = Path.GetExtension(fuOriginalImage.FileName);
            Size imgSize;
            using (MemoryStream stream = new MemoryStream(fuOriginalImage.FileBytes))
            {
                Image img = Image.FromStream(stream);
                imgSize = new Size(img.Width, img.Height);
            }
            if (!fuOriginalImage.HasFile && hfEdit.Value == "-1")
            {
                lblErrorNoFile.Visible = true;
            }
            else if (ext != ".jpg" && ext != ".jpeg" && ext != ".gif" && ext != ".png")
            {
                lblErrorWrongExtension.Visible = true;
            }
            else if (imgSize.Width < 320 || imgSize.Height < 320)
            {
                lblErrorWrongSize.Visible = true;
            }
            else
            {
                int position = 1;

                if (db.Articles_AimFor2022s.Count<Articles_AimFor2022>() > 0)
                {
                    position = (from a in db.Articles_AimFor2022s
                                select a.Position).Max();
                    position++;
                }

                Articles_AimFor2022 article = hfEdit.Value == "-1" ?
                    new Articles_AimFor2022()
                    :
                    (from a in db.Articles_AimFor2022s where a.Id == Convert.ToInt32(hfId.Value) select a).Single<Articles_AimFor2022>();
                article.CaseName = tbCaseName.Text;
                article.DescriptionText = tbDescription.Text;
                if (fuOriginalImage.HasFile)
                    article.Image = this.SaveImage(fuOriginalImage);
                article.Position = position;

                if (hfEdit.Value == "-1")
                    db.Articles_AimFor2022s.InsertOnSubmit(article);
                db.SubmitChanges();

                this.FillGrid();
                this.SwitchView(true);
                this.SaveXml();
            }
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

            Bitmap bmPhoto =  new Bitmap(Width, Height,
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

        protected void rgCases_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "new":
                    this.SwitchView(false);
                    hfEdit.Value = "-1";
                    break;
                case "up":
                    this.MoveCase(rgCases.MasterTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString(), true);
                    break;
                case "down":
                    this.MoveCase(rgCases.MasterTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString(), false);
                    break;
                case "editCase":
                    this.SwitchView(false);
                    this.FillEditForm(rgCases.MasterTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());
                    break;
                case "deleteCase":
                    this.DeleteCase(rgCases.MasterTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());
                    break;
                default:
                    break;
            }
        }

        private void SwitchView(bool original)
        {
            pnlShowCases.Visible = original;
            pnlEditCase.Visible = !original;

            if (original)
            {
                tbCaseName.Text = String.Empty;
                tbDescription.Text = String.Empty;
            }
        }


        private void MoveCase(string Id, bool up)
        {
            Articles_AimFor2022 article = (from art in db.Articles_AimFor2022s
                                        where art.Id == Convert.ToInt32(Id)
                                        select art).Single<Articles_AimFor2022>();
            Articles_AimFor2022 articleToSwitchPlace = up ?
                (from a in db.Articles_AimFor2022s
                 where a.Position ==
                 (from aBefore in db.Articles_AimFor2022s
                  where aBefore.Position < article.Position
                  select aBefore.Position).Max()
                 select a).Single<Articles_AimFor2022>()
                :
                (from a in db.Articles_AimFor2022s
                 where a.Position ==
                 (from aAfter in db.Articles_AimFor2022s
                  where aAfter.Position > article.Position
                  select aAfter.Position).Min()
                 select a).Single<Articles_AimFor2022>();

            Int32 tempPos = articleToSwitchPlace.Position;

            articleToSwitchPlace.Position = article.Position;
            article.Position = tempPos;
            db.SubmitChanges();

            this.FillGrid();
            this.SaveXml();
        }

        protected bool ButtonVisible(string position, bool upButton)
        {
            return !(upButton ? Convert.ToInt32(position) == firstPosition : Convert.ToInt32(position) == lastPosition);
        }

        private void DeleteCase(string Id)
        {
            Articles_AimFor2022 article = (from a in db.Articles_AimFor2022s
                                        where a.Id == Convert.ToInt32(Id)
                                        select a).Single<Articles_AimFor2022>();
            db.Articles_AimFor2022s.DeleteOnSubmit(article);
            db.SubmitChanges();
            this.FillGrid();
            this.SaveXml();
        }

        private void FillEditForm(string Id)
        {
            Articles_AimFor2022 article = (from a in db.Articles_AimFor2022s
                                        where a.Id == Convert.ToInt32(Id)
                                        select a).Single<Articles_AimFor2022>();
            tbCaseName.Text = article.CaseName;
            tbDescription.Text = article.DescriptionText;
            hfEdit.Value = "1";
            hfId.Value = Id;
        }

        protected void imgBtnCancel_OnClick(object sender, EventArgs e)
        {
            this.SwitchView(true);
        }

        private void SaveXml()
        {
            var articles = from a in db.Articles_AimFor2022s
                          orderby a.Position ascending
                          select a;
            XDocument doc = new XDocument(
                new XDeclaration("1.0", "utf-8", "no"),
                new XElement("tiltviewergallery",
                    new XElement("photos")
                )
            );

            foreach (Articles_AimFor2022 a in articles)
            {
                XElement elem = new XElement("photo",
                    new XAttribute("imageurl", config.customerpath + "articlepictures/" + a.Image),
                    new XElement("title", a.CaseName),
                    new XElement("description", new XCData(a.DescriptionText))
                );
                doc.Root.Element("photos").Add(elem);
            }
            doc.Save(Server.MapPath(config.customerpath) + "xml/gallery.xml");
        }

        private void SaveSettings(string rows, string columns)
        {
            XDocument doc = new XDocument(
                new XDeclaration("1.0", "utf-8", "no"),
                new XElement("settings",
                    new XAttribute("rows", rows),
                    new XAttribute("columns", columns)
                )
            );
            doc.Save(Server.MapPath(config.customerpath) + "xml/settings.xml");
        }

        protected void btnSaveSettings_OnClick(object sender, EventArgs e)
        {
            this.SaveSettings(tbRows.Text, tbColumns.Text);
        }
    }
}
