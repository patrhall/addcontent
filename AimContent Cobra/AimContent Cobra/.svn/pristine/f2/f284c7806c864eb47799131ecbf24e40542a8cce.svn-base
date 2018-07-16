using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AIM.Business.AimBusiness.ModulSystem.Website;

namespace AimBusiness.Customers.ModulSystem.Website
{
    public partial class Article : AIM.Base.Modules.Page.AimBusiness.ModulSystem.Website.AimBusiness
    {
        private List<Article_X_Language> ArticleLanguages { get; set; }

        private int? ArticleId
        {
            get
            {
                int tmp;

                if (int.TryParse(Request["Id"], out tmp))
                    return tmp;

                return null;
            }
        }

        protected override void Page_Load()
        {
            if (!IsPostBack)
                init();
        }

        private void init()
        {
            int defaultLanguageId = db.Languages.Single(p => p.IsDefault).Id;

            var maingroups = (from o in db.ArticleGroup_X_Languages
                              where o.Language_FK == defaultLanguageId &&
                              !o.ArticleGroup.ArticleGroup_FK.HasValue
                              select new
                              {
                                  Id = o.ArticleGroup_FK,
                                  Name = o.GroupName
                              }).ToList();

            var items = (from o in db.ArticleGroup_X_Languages
                         where o.Language_FK == defaultLanguageId &&
                         o.ArticleGroup.ArticleGroup_FK.HasValue
                         orderby o.GroupName
                         select new
                         {
                             ParentId = o.ArticleGroup.ArticleGroup_FK,
                             Name = o.GroupName,
                             Id = o.ArticleGroup_FK
                         });

            List<ArticleGroup> datasource = new List<ArticleGroup>();

            foreach (var item in items)
            {
                datasource.Add(new ArticleGroup() { Id = item.Id, Name = maingroups.Single(p => p.Id == item.ParentId).Name + ": " + item.Name });
            }

            ddArticleGroup.DataSource = datasource.OrderBy(p => p.Name);
            ddArticleGroup.DataBind();

            ddDimension.DataSource = (from o in db.Dimension_X_Languages
                                      where o.Language.IsDefault 
                                      orderby o.Name
                                      select new
                                      {
                                          Id = o.Dimension_FK,
                                          o.Name
                                      });
            ddDimension.DataBind();

            if (ArticleId.HasValue)
            {
                var article = (from o in db.Articles
                               where o.Id == ArticleId.Value
                               select o).Single();

                ArticleLanguages = db.Article_X_Languages.Where(p => p.Article_FK == ArticleId.Value).ToList();

                tbArticleNumber.Text = article.ArticleNumber;
                tbImageName.Text = article.ImageName;
                tbWidth.Text = article.Width;
                tbDepth.Text = article.Depth;
                tbHeight.Text = article.Height;
                tbWeight.Text = article.Weight;
                cbIsPublic.Checked = article.IsPublic;
                tbInstallationInstructionName.Text = article.InstallationInstructionName;
                tbPreviewImageName.Text = article.PreviewImageName;
                ddArticleGroup.SelectedValue = article.ArticleGroup_FK.ToString();
                ddDimension.SelectedValue = article.Dimension_FK.ToString();

                string relations = "";

                foreach(var x in article.ArticleRelations.Where(p => p.IsRelation))                
                    relations += x.Article.ArticleNumber + ",";

                tbRelatedArticles.Text = relations;

                string acc = "";

                foreach (var x in article.ArticleRelations.Where(p => !p.IsRelation))
                    acc += x.Article.ArticleNumber + ",";

                tbRelatedArticlesAcc.Text = acc;

                string exckludedLanguages = "";

                foreach (var x in article.Article_X_LanguageExcludes)
                    exckludedLanguages += x.Language.Language1;

                tbLanguageExcluded.Text = exckludedLanguages;
                    
            }
            else
                ArticleLanguages = new List<Article_X_Language>();

            rptLanguage.DataSource = (from o in db.Languages
                                      orderby !o.IsDefault, o.Language1
                                      select o);
            rptLanguage.DataBind();
        }

        public class ArticleGroup
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        protected void rptLanguage_Bound(object sender, RepeaterItemEventArgs e)
        {
            var dataItem = (Language)e.Item.DataItem;
            var x = ArticleLanguages.SingleOrDefault(p => p.Language_FK == dataItem.Id);

            ((Label)e.Item.FindControl("lblLanguage")).Text = dataItem.Language1;
            ((HiddenField)e.Item.FindControl("hfLanguageId")).Value = dataItem.Id.ToString();

            if (x != null)
            {
                ((TextBox)e.Item.FindControl("tbArticleName")).Text = x.ArticleName;
                ((TextBox)e.Item.FindControl("tbArticleDescription")).Text = x.ArticleDescription;
            }
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            AIM.Business.AimBusiness.ModulSystem.Website.Article article;

            if (!ArticleId.HasValue)
            {
                article = new AIM.Business.AimBusiness.ModulSystem.Website.Article();
                db.Articles.InsertOnSubmit(article);
            }
            else
                article = db.Articles.Single(p => p.Id == ArticleId.Value);

            article.ArticleNumber = tbArticleNumber.Text;
            article.ImageName = tbImageName.Text;
            article.Width = tbWidth.Text;
            article.Depth = tbDepth.Text;
            article.Height = tbHeight.Text;
            article.Weight = tbWeight.Text;
            article.IsPublic = cbIsPublic.Checked;

            article.InstallationInstructionName = tbInstallationInstructionName.Text;
            article.PreviewImageName = tbPreviewImageName.Text;
            article.ArticleGroup_FK = Convert.ToInt32(ddArticleGroup.SelectedValue);
            article.ArticleGroup_FK = Convert.ToInt32(ddDimension.SelectedValue);            

            foreach (RepeaterItem ri in rptLanguage.Items)
            {
                int languageId = Convert.ToInt32(((HiddenField)ri.FindControl("hfLanguageId")).Value);

                Article_X_Language x;

                if (ArticleId.HasValue && db.Article_X_Languages.Any(p => p.Article_FK == ArticleId.Value && p.Language_FK == languageId))
                    x = db.Article_X_Languages.Single(p => p.Article_FK == ArticleId.Value && p.Language_FK == languageId);
                else
                {
                    x = new Article_X_Language();
                    db.Article_X_Languages.InsertOnSubmit(x);
                    x.Article = article;
                    x.Language_FK = languageId;
                }

                x.ArticleName = ((TextBox)ri.FindControl("tbArticleName")).Text;
                x.ArticleDescription = ((TextBox)ri.FindControl("tbArticleDescription")).Text;
            }            

            db.SubmitChanges();



            //if (tbRelatedArticles.Text.Trim() != "")
            //{
            //    try
            //    {
            //        foreach (var item in tbRelatedArticles.Text.Split(new char[] { ',' }))
            //        {
            //            if (db.Articles.Any(p => p.ArticleNumber == item))
            //            {
            //                 //article. db.Articles.Single(p => p.ArticleNumber == item).Id
            //            }
            //        }
            //    }
            //    catch { }
            //}
        }
    }
}