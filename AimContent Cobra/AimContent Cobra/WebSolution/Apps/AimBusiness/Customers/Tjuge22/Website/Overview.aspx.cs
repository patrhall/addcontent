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
using AIM.Business.AimBusiness.Tjuge22.Website;
using System.IO;
using Telerik.Web.UI;
using System.Drawing;
using Image = System.Drawing.Image;

namespace AimBusiness.Customers.Tjuge22.Website
{
    public partial class Overview : AIM.Base.Modules.Page.AimBusiness.Tjuge22.Website.AimBusiness
    {
        private Int32 lastPosition;
        private Int32 firstPosition;
        private Int32 objectId;

        protected override void Page_Load()
        {
            if (Request["oId"] != null)
                objectId = Convert.ToInt32(Request["oId"]);

            if (!IsPostBack)
            {
                this.InitXmlFiles();
                this.FillGrid();
                this.FillSettings();
            }
        }

        private void InitXmlFiles()
        {
            string gallerypath = Server.MapPath(config.customerpath) + "xml/gallery_" + objectId.ToString() + ".xml";
            string settingspath = Server.MapPath(config.customerpath) + "xml/settings_" + objectId.ToString() + ".xml";

            if (!File.Exists(gallerypath))
            {
                XDocument doc = new XDocument(
                       new XDeclaration("1.0", "utf-8", "no"),
                       new XElement("tiltviewergallery",
                           new XElement("photos")
                       )
                   );
                doc.Save(gallerypath);
            }
            if (!File.Exists(settingspath))
                this.SaveSettings("1", "1", "Arial", "90", "32", "0x000000", "1400", "6000", "true");
                    //, "40", "0xFFFFFF", "0xFFFFFF");

        }

        private void FillGrid()
        {
            var articles = from a in db.Articles_Tjuge22s
                           where a.ObjectId == objectId
                           select a;
            if (articles.Count<Articles_Tjuge22>() > 0)
            {
                lastPosition = (from a in db.Articles_Tjuge22s
                                where a.ObjectId == objectId
                                select a.Position).Max();
                firstPosition = (from a in db.Articles_Tjuge22s
                                 where a.ObjectId == objectId
                                 select a.Position).Min();
            }

            rgCases.DataSource = from _case in db.Articles_Tjuge22s
                                 where _case.ObjectId == objectId
                                 orderby _case.Position
                                 select _case;
            rgCases.DataBind();
        }

        private void FillSettings()
        {
            string path = Server.MapPath(config.customerpath) + "xml/settings_" + objectId.ToString() + ".xml";
            XDocument doc = XDocument.Load(path);
            XElement elem = doc.Element("settings");
            tbRows.Text = elem.Attribute("rows").Value;
            tbColumns.Text = elem.Attribute("columns").Value;
            tbFontName.Text = elem.Attribute("fontname").Value;
            tbTitleSize.Text = elem.Attribute("titlesize").Value;
            tbDescSize.Text = elem.Attribute("descsize").Value;
            tbFontColor.Text = elem.Attribute("fontcolor").Value;
            tbZoomedInDistance.Text = elem.Attribute("zoomedin").Value;
            tbZoomedOutDistance.Text = elem.Attribute("zoomedout").Value;
            rblSound.ClearSelection();
            rblSound.Items.FindByValue(elem.Attribute("enablesound").Value).Selected = true;
            //tbFrameWidth.Text = elem.Attribute("framewidth").Value;
            //tbFlipButtonColor.Text = elem.Attribute("flipbuttoncolor").Value;
            //tbNavButtonColor.Text = elem.Attribute("navbuttoncolor").Value;
        }

        protected void btnUpload_OnClick(object sender, EventArgs e)
        {
            string ext = Path.GetExtension(fuOriginalImage.FileName);
            Size imgSize = Size.Empty;

            if (fuOriginalImage.HasFile)
            {
                bool passed = true;
                using (MemoryStream stream = new MemoryStream(fuOriginalImage.FileBytes))
                {
                    Image img = Image.FromStream(stream);
                    imgSize = new Size(img.Width, img.Height);
                }

                if (ext != ".jpg" && ext != ".jpeg" && ext != ".gif" && ext != ".png")
                {
                    lblErrorWrongExtension.Visible = true;
                    passed = false;
                    Response.Write("notpassedext");
                }

                if (imgSize.Width < 320 || imgSize.Height < 320)
                {
                    lblErrorWrongSize.Visible = true;
                    passed = false;
                    Response.Write("notpassedsize");
                }

                if (passed)
                {
                    //Save information
                    if (hfEdit.Value == "-1")
                        saveCase(true);
                    else
                        saveCase(false);

                    //Response.Redirect("Overview.aspx?oId=" + Request["oId"].ToString());
                    this.SwitchView(true);
                }
                else
                {
                    
                }
            }
            else
            {
                if (hfEdit.Value == "-1")
                    saveCase(true);
                else
                    saveCase(false);

                this.SwitchView(true);
            }


            
        }

        private void saveCase(bool isNewCase)
        {
            int position = 1;
            var articles = from a in db.Articles_Tjuge22s
                           where a.ObjectId == objectId
                           select a;
            if (isNewCase)
            {
                if (articles.Count<Articles_Tjuge22>() > 0)
                {
                    position = (from a in db.Articles_Tjuge22s
                                where a.ObjectId == objectId
                                select a.Position).Max();
                    position++;
                }
                Articles_Tjuge22 article = new Articles_Tjuge22();
                article.Name = tbCaseName.Text;
                article.DescriptionText = tbDescription.Text;

                if (fuOriginalImage.HasFile)
                    article.Image = this.SaveImage(fuOriginalImage);
                else
                    article.Image = "noimage.jpg";

                article.ObjectId = objectId;
                article.Position = position;

                db.Articles_Tjuge22s.InsertOnSubmit(article);
            }
            else
            {
                Articles_Tjuge22 article = (from a in db.Articles_Tjuge22s where a.Id == Convert.ToInt32(hfId.Value) select a).Single<Articles_Tjuge22>();
                article.Name = tbCaseName.Text;
                article.DescriptionText = tbDescription.Text;
                if (fuOriginalImage.HasFile)
                    article.Image = this.SaveImage(fuOriginalImage);
            }      
            
            db.SubmitChanges();
            
            this.FillGrid();
            this.SaveXml();
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
            Articles_Tjuge22 article = (from art in db.Articles_Tjuge22s
                                        where art.Id == Convert.ToInt32(Id)
                                        select art).Single<Articles_Tjuge22>();
            Articles_Tjuge22 articleToSwitchPlace = up ?
                (from a in db.Articles_Tjuge22s
                 where a.Position ==
                 (from aBefore in db.Articles_Tjuge22s
                  where aBefore.Position < article.Position
                  && aBefore.ObjectId == objectId
                  select aBefore.Position).Max()
                 select a).Single<Articles_Tjuge22>()
                :
                (from a in db.Articles_Tjuge22s
                 where a.Position ==
                 (from aAfter in db.Articles_Tjuge22s
                  where aAfter.Position > article.Position
                  && aAfter.ObjectId == objectId
                  select aAfter.Position).Min()
                 select a).Single<Articles_Tjuge22>();

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
            Articles_Tjuge22 article = (from a in db.Articles_Tjuge22s
                                        where a.Id == Convert.ToInt32(Id)
                                        select a).Single<Articles_Tjuge22>();
            db.Articles_Tjuge22s.DeleteOnSubmit(article);
            db.SubmitChanges();
            this.FillGrid();
            this.SaveXml();
        }

        private void FillEditForm(string Id)
        {
            Articles_Tjuge22 article = (from a in db.Articles_Tjuge22s
                                        where a.Id == Convert.ToInt32(Id)
                                        select a).Single<Articles_Tjuge22>();
            tbCaseName.Text = article.Name;
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
            var articles = from a in db.Articles_Tjuge22s
                           where a.ObjectId == objectId
                          orderby a.Position ascending
                          select a;
            XDocument doc = new XDocument(
                new XDeclaration("1.0", "utf-8", "no"),
                new XElement("tiltviewergallery",
                    new XElement("photos")
                )
            );

            foreach (Articles_Tjuge22 a in articles)
            {
                XElement elem = new XElement("photo",
                    new XAttribute("imageurl", config.customerpath + "articlepictures/" + a.Image),
                    new XElement("title", a.Name),
                    new XElement("description", new XCData(a.DescriptionText))
                );
                doc.Root.Element("photos").Add(elem);
            }
            doc.Save(Server.MapPath(config.customerpath) + "xml/gallery_" + objectId.ToString() + ".xml");
        }

        private void SaveSettings(string rows, string columns, string fontName,
            string titleSize, string descSize, string fontColor, string zoomedIn,
            string zoomedOut, string enableSound)
            //, string frameWidth, string flipButton, string navButton)
        {
            XDocument doc = new XDocument(
                new XDeclaration("1.0", "utf-8", "no"),
                new XElement("settings",
                    new XAttribute("rows", rows),
                    new XAttribute("columns", columns),
                    new XAttribute("fontname", fontName),
                    new XAttribute("titlesize", titleSize),
                    new XAttribute("descsize", descSize),
                    new XAttribute("fontcolor", fontColor),
                    new XAttribute("zoomedin", zoomedIn),
                    new XAttribute("zoomedout", zoomedOut),
                    new XAttribute("enablesound", enableSound)
                    //These attributes doesn't work, no need to save them.
                    //new XAttribute("framewidth", frameWidth),
                    //new XAttribute("flipbuttoncolor", flipButton),
                    //new XAttribute("navbuttoncolor", navButton)
                )
            );
            doc.Save(Server.MapPath(config.customerpath) + "xml/settings_" + objectId.ToString() + ".xml");
        }

        protected void btnSaveSettings_OnClick(object sender, EventArgs e)
        {
            this.SaveSettings(tbRows.Text, tbColumns.Text, tbFontName.Text, tbTitleSize.Text, tbDescSize.Text, tbFontColor.Text,
                tbZoomedInDistance.Text, tbZoomedOutDistance.Text, rblSound.SelectedValue);
                //, tbFrameWidth.Text, tbFlipButtonColor.Text, tbNavButtonColor.Text);
        }
    }
}
