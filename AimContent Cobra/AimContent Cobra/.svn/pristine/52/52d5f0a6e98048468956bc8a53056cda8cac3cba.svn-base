using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace AimBusiness.Customers.ModulSystem.Website
{
    public partial class Overview : AIM.Base.Modules.Page.AimBusiness.ModulSystem.Website.AimBusiness
    {
        private int SelectedArticleGroupId
        {
            get
            {
                if (ddArticleGroups.SelectedValue != "")
                    return Convert.ToInt32(ddArticleGroups.SelectedValue);
                else
                    return 0;
            }
        }

        protected override void Page_Load()
        {
            if (!IsPostBack)
                init();

            hlUploadProductExcel.Text = Language.getValue("hlUploadProductExcel");
        }

        private void init()
        {
            AIM.Base.Classes.RadHandler.RadEditor_LoadEditorPreference(ref reDescription, config); 
            
            int languageId = db.Languages.Single(p => p.AimContentLanguageId==2).Id;

            var maingroups = (from ag in db.ArticleGroups
                              where ag.Language_FK == languageId &&
                              !ag.ArticleGroup_FK.HasValue
                              select new
                              {
                                  Id = ag.Id,
                                  Name = ag.GroupName
                              });

            var items = (from ag in db.ArticleGroups
                         where ag.Language_FK == languageId &&
                         ag.ArticleGroup_FK.HasValue
                         orderby ag.GroupName
                         select new
                         {
                             ParentGroup = ag.ArticleGroup_FK,
                             Name = ag.GroupName,
                             Id = ag.Id
                         });

            List<ArticleGroup> datasource = new List<ArticleGroup>();

            foreach (var item in items)
            {
                datasource.Add(new ArticleGroup() { Id = (int)item.Id, Name = maingroups.Single(p => p.Id == item.ParentGroup).Name + ": " + item.Name });
            }

            ddArticleGroups.DataSource = datasource.OrderBy(p => p.Name);
            ddArticleGroups.DataBind();

            AIM.Business.DataObjectsDataContext dbObject = new AIM.Business.DataObjectsDataContext(AIM.Business.DataManager.ConnectionString);

            var theObject = dbObject.Objects.Single(p => p.ObjectID == ObjectId.Value);

            txtUrl.Text = theObject.Structure;
            reDescription.Content = theObject.HTMLContent;    
        }

        protected void rgArticle_Source(object sender, GridNeedDataSourceEventArgs e)
        {
            int defaultLanguageId = db.Languages.Single(p => p.IsDefault).Id;

            rgArticle.DataSource = (from o in db.Articles
                                    where o.ArticleGroup_FK == SelectedArticleGroupId &&
                                    o.Language_FK == defaultLanguageId
                                    orderby o.SortOrder
                                    select new
                                    {
                                        o.Id,
                                        o.ArticleName,
                                        o.ArticleNumber,                                        
                                        o.IsPublic                                        
                                    });
        }

        protected void ddArticleGroups_Changed(object sender, EventArgs e)
        {
            rgArticle.Rebind();
        }

        protected void lbExportAll_Click(object sender, EventArgs e)
        {
            rgExportAll.DataSource = db.Articles;
            rgExportAll.DataBind();

            rgExportAll.ExportSettings.Excel.Format = GridExcelExportFormat.Html;
            rgExportAll.ExportSettings.FileName = "Articles";
            rgExportAll.ExportSettings.ExportOnlyData = true;
            rgExportAll.MasterTableView.ExportToExcel();
        }

        public class ArticleGroup
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        private int menuLanguageId
        {
            get
            {
                AIM.Business.AimEdit.DataObjectsDataContext dbMenu = new AIM.Business.AimEdit.DataObjectsDataContext(AIM.Business.DataManager.ConnectionString);

                if (Session["WebSolution_LeftMenuControl_ContentTreeView"] != null)
                    return int.Parse(Session["WebSolution_LeftMenuControl_ContentTreeView"].ToString());
                else
                {
                    Session["WebSolution_LeftMenuControl_ContentTreeView"] = dbMenu.usp_GetObject(config.siteID, config.startpageID).First<AIM.Business.AimEdit.usp_GetObjectResult>().LangID;
                    return int.Parse(Session["WebSolution_LeftMenuControl_ContentTreeView"].ToString());
                }
            }

            set
            {
                Session["WebSolution_LeftMenuControl_ContentTreeView"] = value;
            }
        }

        protected void btnSaveUrl_Click(object sender, EventArgs e)
        {

            AIM.Business.DataObjectsDataContext dbObject = new AIM.Business.DataObjectsDataContext(AIM.Business.DataManager.ConnectionString);

            var theObject = dbObject.Objects.Single(p => p.ObjectID == ObjectId.Value);

            theObject.Structure = txtUrl.Text;
            theObject.HTMLContent = reDescription.Content;
            dbObject.SubmitChanges();
        }
    }
}