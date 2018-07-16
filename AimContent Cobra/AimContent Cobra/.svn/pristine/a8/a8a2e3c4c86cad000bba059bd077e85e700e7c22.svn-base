using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.OleDb;
using AIM.Base.Modules.Page.AimBusiness.ModulSystem.Website;
using AIM.Business.AimBusiness.ModulSystem.Website;


namespace AimBusiness.Customers.ModulSystem.Website
{
    public partial class Upload : AIM.Base.Modules.Page.AimBusiness.ModulSystem.Website.AimBusiness
    {
        public class ArticleGroupContainer
        {
            public int    ArticleGroupId { get; set; }
            public string ArticleGroupName { get; set; }
            public int    ArticleSubGroupId { get; set; }
            public string ArticleSubGroupName { get; set; }
        }

        private const string ExcelSheetName = "Blad1";

        private const string ColumnProductGroup = "Product group";
        private const string ColumnHeadLine = "Product";
        private const string ColumnProductName = "Product name";
        private const string ColumnDescription = "Description";
        private const string ColumnArtNr = "ArtNo";
        private const string ColumnRelation = "Relation ";
        private const string ColumnAccesories = "Rec accessories ";
        private const string ColumnImage = "Image";
        private const string ColumnWidth = "Width";
        private const string ColumnDepth = "Depth";
        private const string ColumnHeight = "Height";
        private const string ColumnWeight = "Weight";
        private const string ColumnIsPublc = "Show";
        private const string ColumnSort = "Sorting";
        private const string ColumnInstallationInstructionName = "Assembly instruction";
        private const string ColumnDimension = "Dimensions";
        private const string ColumnPreviewImageName = "Image";
        private const string ColumnProductGroupUrl = "Product Group Url";

        private string ImportFileName { get; set; }
        private DataTable Data { get; set; }
        private List<ArticleGroupContainer> ArticleGroups { get; set; }                
        private Dictionary<string, int> Dimensions { get; set; }
        private Dictionary<string, int> LanguagesIds { get; set; }
        private Dictionary<string, int> Articles { get; set; }
        private List<string> Log { get; set; }
        private string language;
        private int languageID;
        

        protected override void Page_Load()
        {

            lblMsg.Text = String.Empty;
            btnImport.Text = Language.getValue("btnImport");
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            if (!fuExcel.HasFile || Path.GetExtension(fuExcel.FileName) != ".xlsx")
            {
                lblMsg.Text = "The file uploaded needs to be a xlsx-file";
                return;
            }

            ArticleGroups = new List<ArticleGroupContainer>();            
            Dimensions    = new Dictionary<string, int>();
            Articles      = new Dictionary<string, int>();
            Log           = new List<string>();

            setCurrentLanguage();
            saveFile();
            getData();
            clearDb();
            importData();

            if (Log.Count == 0)
                lblMsg.Text = "The import has finished with success";
            else
            {
                lblMsg.Text = "The import has finished, but with errors:<br />";

                foreach (var logItem in Log.Distinct())
                    lblMsg.Text += logItem + "<br />";
            }
        }
        private void setCurrentLanguage()
        {
            string fileName = Path.GetFileNameWithoutExtension(fuExcel.FileName);
            language = fileName.Substring(fileName.Length - 2);
            if(fileName.Substring(fileName.Length - 4)=="Tevo")
                languageID = (from l in db.Languages where l.Language1 == "Tevo" select l.Id).Single();
            else if (fileName.Substring(fileName.Length - 4) == "BEFR")
                languageID = (from l in db.Languages where l.Language1 == "BEFR" select l.Id).Single();
            else
                languageID = (from l in db.Languages where l.Language1 == language select l.Id).Single();
        }

        private void clearDb()
        {
            //Dont clear db.Languages!!
            db.ArticleRelations.DeleteAllOnSubmit(db.ArticleRelations.Where(p => p.Language_FK==languageID));
            db.SubmitChanges();

            db.Articles.DeleteAllOnSubmit(db.Articles.Where(p => p.Language_FK == languageID));
            db.ArticleGroups.DeleteAllOnSubmit(db.ArticleGroups.Where(p => p.Language_FK == languageID));
            db.Dimensions.DeleteAllOnSubmit(db.Dimensions.Where(p => p.Language_FK == languageID));
            db.SubmitChanges();
        }

        private void saveFile()
        {
            ImportFileName = Server.MapPath("~/import.xlsx");

            if (File.Exists(ImportFileName))
                File.Delete(ImportFileName);

            fuExcel.PostedFile.SaveAs(ImportFileName);
        }
        private void getData()
        {
            Excel excel = new Excel();
            DataSet ds = excel.getDataSet(ImportFileName, ExcelSheetName);

            Data = ds.Tables[0];
        }

        private void importData()
        {
            foreach (DataRow d in Data.Rows)
            {
                if(rowIsEmpty(d)) break;
                try
                {
                    int articleSubGroupId, dimensionId;

                    if (ArticleGroups.Any(p => p.ArticleSubGroupName == d[ColumnHeadLine].ToString() && p.ArticleGroupName == d[ColumnProductGroup].ToString()))
                    {
                        articleSubGroupId = ArticleGroups.Single(p => p.ArticleSubGroupName == d[ColumnHeadLine].ToString() && p.ArticleGroupName == d[ColumnProductGroup].ToString()).ArticleSubGroupId;
                    }
                    else
                    {
                        ArticleGroupContainer articleGroupContainer = new ArticleGroupContainer();

                        if (ArticleGroups.Any(p => p.ArticleGroupName == d[ColumnProductGroup].ToString()))
                        {
                            var oldItem = ArticleGroups.First(p => p.ArticleGroupName == d[ColumnProductGroup].ToString());

                            articleGroupContainer.ArticleGroupId = oldItem.ArticleGroupId;
                            articleGroupContainer.ArticleGroupName = oldItem.ArticleGroupName;
                        }                            
                        else
                        {
                            AIM.Business.AimBusiness.ModulSystem.Website.ArticleGroup newArticleGroup = new AIM.Business.AimBusiness.ModulSystem.Website.ArticleGroup();
                            newArticleGroup.Language_FK     = this.languageID;
                            newArticleGroup.ArticleGroup_FK = null;
                            newArticleGroup.Url             = d[ColumnProductGroupUrl].ToString().ToLower();
                            newArticleGroup.GroupName       = d[ColumnProductGroup].ToString();
                            db.ArticleGroups.InsertOnSubmit(newArticleGroup);
                            db.SubmitChanges();                            

                            articleGroupContainer.ArticleGroupId = newArticleGroup.Id;
                            articleGroupContainer.ArticleGroupName = d[ColumnProductGroup].ToString();                            
                        }

                        AIM.Business.AimBusiness.ModulSystem.Website.ArticleGroup subGroup = new AIM.Business.AimBusiness.ModulSystem.Website.ArticleGroup();
                        subGroup.Language_FK = languageID;
                        subGroup.ArticleGroup_FK = articleGroupContainer.ArticleGroupId;
                        subGroup.GroupName = d[ColumnHeadLine].ToString();
                        db.ArticleGroups.InsertOnSubmit(subGroup);
                        db.SubmitChanges();

                        articleGroupContainer.ArticleSubGroupId = subGroup.Id;
                        articleGroupContainer.ArticleSubGroupName = d[ColumnHeadLine].ToString();                        
                        ArticleGroups.Add(articleGroupContainer);
                        articleSubGroupId = subGroup.Id;
                    }

                    if (Dimensions.ContainsKey(d[ColumnDimension].ToString()))
                        dimensionId = Dimensions[d[ColumnDimension].ToString()];
                    else
                    {
                        AIM.Business.AimBusiness.ModulSystem.Website.Dimension newDimension = new AIM.Business.AimBusiness.ModulSystem.Website.Dimension();
                        newDimension.Language_FK = languageID;
                        newDimension.Name = d[ColumnDimension].ToString();
                        db.Dimensions.InsertOnSubmit(newDimension);
                        db.SubmitChanges();

                        dimensionId = newDimension.Id;
                        Dimensions.Add(d[ColumnDimension].ToString(), dimensionId);
                    }

                    int articleId = createArticle(d, articleSubGroupId, dimensionId, this.languageID);
                }
                catch (Exception ex)
                {
                    log("Article " + d[ColumnArtNr].ToString() + ": The article was not added!");
                    log("Exception: " + ex.Message + ex.StackTrace + "<br /> <br />");
                }
            }

            //Relations can only be created after all articles
            foreach (DataRow d in Data.Rows)
            {
                try
                {
                    //for (int i = 1; i <= 10; i++)
                        //createRelation(d[ColumnArtNr].ToString(), true, i, d[ColumnRelation + i].ToString());

                    for (int i = 1; i <= 9; i++)
                        createRelation(d[ColumnArtNr].ToString(), false, i, d[ColumnAccesories + i].ToString());
                }
                catch (Exception e)
                {
                    log("Article " + d[ColumnArtNr].ToString() + ": Adding relations failed");
                }
            }
        }

        private int createArticle(DataRow d, int articleSubGroupId, int dimensionId, int language)
        {
            AIM.Business.AimBusiness.ModulSystem.Website.Article article = new AIM.Business.AimBusiness.ModulSystem.Website.Article();
            article.Language_FK = language;
            article.ArticleGroup_FK = articleSubGroupId;
            article.Dimension_FK = dimensionId;
            article.ArticleNumber = d[ColumnArtNr].ToString();
            article.ImageName = d[ColumnImage].ToString();
            article.Width = d[ColumnWidth].ToString();
            article.Depth = d[ColumnDepth].ToString();
            article.Height = d[ColumnHeight].ToString();
            article.Weight = d[ColumnWeight].ToString();
            article.IsPublic = (d[ColumnIsPublc].ToString().ToLower() == "x");
            article.SortOrder = Convert.ToInt32(d[ColumnSort]);
            article.InstallationInstructionName = d[ColumnInstallationInstructionName].ToString();
            article.PreviewImageName = d[ColumnPreviewImageName].ToString();
            article.ArticleName = d[ColumnProductName].ToString();
            article.ArticleDescription = d[ColumnDescription].ToString();

            db.Articles.InsertOnSubmit(article);

            db.SubmitChanges();

            Articles.Add(article.ArticleNumber, article.Id);

            return article.Id;
        }

        private void createRelation(string parentArticleNumber, bool isRelation, int placeholderId, string value)
        {
            int? articleId = null;
            string text = null;

            if (value == "-")
            {
                text = value;
            }
            else if (value.Contains("/"))
            {
                var articles = value.Split(new char[] { '/' });

                foreach (var article in articles)
                    createRelation(parentArticleNumber, isRelation, placeholderId, article.Trim());

                return;
            }
            else if (value.Contains("+"))
            {
                var articles = value.Split(new char[] { '+' });

                foreach (var article in articles)
                    createRelation(parentArticleNumber, isRelation, placeholderId, article.Trim());

                return;
            }
            else if (value.Trim().Length > 0)
            {
                if (Articles.ContainsKey(value.Trim()))
                    articleId = Articles[value.Trim()];
                else
                {
                    log(value + " not in articlelist");
                    return;
                }
            }
            else
                return;

            if (!Articles.ContainsKey(parentArticleNumber))
            {
                log(value + " not in articlelist");
                return;
            }

            AIM.Business.AimBusiness.ModulSystem.Website.ArticleRelation newRelation = new AIM.Business.AimBusiness.ModulSystem.Website.ArticleRelation();
            newRelation.Language_FK      = languageID;
            newRelation.ParentArticle_FK = Articles[parentArticleNumber];
            newRelation.Article_FK       = articleId;
            newRelation.IsRelation       = isRelation;
            newRelation.PlaceHolderId    = placeholderId;
            newRelation.Text             = text;

            db.ArticleRelations.InsertOnSubmit(newRelation);
            db.SubmitChanges();
        }

        private void log(string str)
        {
            Log.Add(str);
        }

        private bool rowIsEmpty(DataRow current){
            int result;
            return !Int32.TryParse(current[ColumnSort].ToString(), out result);
        }
    }
}