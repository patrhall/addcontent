using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AIM.Business.AimBusiness.ModulSystem.Website;

namespace AimBusiness.Customers.ModulSystem.Website
{
    public partial class ArticleGroups : AIM.Base.Modules.Page.AimBusiness.ModulSystem.Website.AimBusiness
    {
        private const string ARTICLEGROUP = "ARTICLEGROUP";
        private const string DIMENSION = "DIMENSION";

        private int SelectedDimensionId
        {
            get
            {
                return Convert.ToInt32(ddDimensions.SelectedValue);
            }
        }

        private int SelectedArticleGroupId
        {
            get
            {
                return Convert.ToInt32(ddArticleGroups.SelectedValue);
            }
        }

        protected override void Page_Load()
        {
            if (!IsPostBack)
                init();
        }

        private void init()
        {
            ddArticleGroups.DataSource = (from o in db.ArticleGroup_X_Languages
                                          where o.Language.IsDefault
                                          orderby o.GroupName
                                          select new
                                          {
                                              Id = o.ArticleGroup_FK,
                                              o.GroupName
                                          });
            ddArticleGroups.DataValueField = "Id";
            ddArticleGroups.DataTextField = "GroupName";
            ddArticleGroups.DataBind();

            ddDimensions.DataSource = (from o in db.Dimension_X_Languages
                                       where o.Language.IsDefault
                                       orderby o.Name
                                       select new { 
                                           Id = o.Dimension_FK,
                                           o.Name
                                       });
            ddDimensions.DataValueField = "Id";
            ddDimensions.DataTextField = "Name";
            ddDimensions.DataBind();

            //ArticleLanguages = db.Article_X_Languages.Where(p => p.Article_FK == ArticleId.Value).ToList();
        }

        protected void ibEditDimension_Click(object sender, EventArgs e)
        {
            List<DataSource> source = new List<DataSource>();

            foreach (var lang in db.Languages)
            {
                var item = db.Dimension_X_Languages.SingleOrDefault(p => p.Language_FK == lang.Id && p.Dimension_FK == SelectedDimensionId);

                source.Add(new DataSource()
                    {
                        LanguageId = lang.Id,
                        Name = (item != null ? item.Name : String.Empty)
                    });
            }

            rptLanguage.DataSource = source;
            rptLanguage.DataBind();

            hfWhat.Value = DIMENSION;
            lblEditTitle.Text = "Redigering av dimension";
            IsEdit = true;
        }

        protected void ibEditArticleGroup_Click(object sender, EventArgs e)
        {
            List<DataSource> source = new List<DataSource>();

            foreach (var lang in db.Languages)
            {
                var item = db.ArticleGroup_X_Languages.SingleOrDefault(p => p.Language_FK == lang.Id && p.ArticleGroup_FK == SelectedArticleGroupId);

                source.Add(new DataSource()
                {
                    LanguageId = lang.Id,
                    LanguageName = lang.Language1,
                    Name = (item != null ? item.GroupName : String.Empty)
                });
            }

            rptLanguage.DataSource = source;
            rptLanguage.DataBind();

            hfWhat.Value = ARTICLEGROUP;
            lblEditTitle.Text = "Redigering av artikelgrupp";
            IsEdit = true;
        }

        protected void rptLanguage_Bound(object sender, RepeaterItemEventArgs e)
        {
            var dataItem = (DataSource)e.Item.DataItem;            

            ((Label)e.Item.FindControl("lblLanguage")).Text = dataItem.LanguageName;
            ((HiddenField)e.Item.FindControl("hfLanguageId")).Value = dataItem.LanguageId.ToString();
            ((TextBox)e.Item.FindControl("tbName")).Text = dataItem.Name;            
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem ri in rptLanguage.Items)
            {
                int languageId = Convert.ToInt32(((HiddenField)ri.FindControl("hfLanguageId")).Value);
                string name = ((TextBox)ri.FindControl("tbName")).Text;

                if (hfWhat.Value == ARTICLEGROUP)
                    saveArticleGroup(languageId, name);
                else
                    saveDimension(languageId, name);
            }

            Response.Redirect("ArticleGroups.aspx");
        }

        private void saveArticleGroup(int languageId, string groupName)
        {
            ArticleGroup_X_Language item;

            if (db.ArticleGroup_X_Languages.Any(p => p.ArticleGroup_FK == SelectedArticleGroupId && p.Language_FK == languageId))
                item = db.ArticleGroup_X_Languages.Single(p => p.ArticleGroup_FK == SelectedArticleGroupId && p.Language_FK == languageId);
            else
            {
                item = new ArticleGroup_X_Language();
                db.ArticleGroup_X_Languages.InsertOnSubmit(item);
                item.ArticleGroup_FK = SelectedArticleGroupId;
            }

            item.Language_FK = languageId;
            item.GroupName = groupName;

            db.SubmitChanges();
        }

        private void saveDimension(int languageId, string name)
        {
            Dimension_X_Language item;

            if (db.Dimension_X_Languages.Any(p => p.Dimension_FK == SelectedDimensionId && p.Language_FK == languageId))
                item = db.Dimension_X_Languages.Single(p => p.Dimension_FK == SelectedDimensionId && p.Language_FK == languageId);
            else
            {
                item = new Dimension_X_Language();
                db.Dimension_X_Languages.InsertOnSubmit(item);
                item.Dimension_FK = SelectedDimensionId;
            }

            item.Language_FK = languageId;
            item.Name = name;

            db.SubmitChanges();
        }

        private bool IsEdit
        {
            set
            {
                pnlEdit.Visible = value;
                pnlOverview.Visible = !value;
            }
            get
            {
                return pnlEdit.Visible;
            }
        }

        public class DataSource
        {
            public int LanguageId { get; set; }
            public string LanguageName { get; set; }
            public string Name { get; set; }
        }
    }
}