using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AimBusiness.Customers.Boletus.Website
{
    public partial class EditProduct : AIM.Base.Modules.Page.AimBusiness.Boletus.Website.AimBusiness
    {
        private Common.DataHandler _dataHandler;

        private int? ProductId
        {
            get
            {
                if (String.IsNullOrEmpty(Request["ID"]))
                    return null;

                return Convert.ToInt32(Request["ID"]);
            }
        }        

        protected override void Page_Load()
        {
            _dataHandler = new Common.DataHandler(config.customerpath);

            if (!IsPostBack)
                init();

            imgProductImage.Attributes.Add("style", "max-width:300px;");
        }

        private void init()
        {
            if (ProductId.HasValue)                
                fillEdit();

            rptProductItems.DataSource = Containers.Language.AllLanguage;            
            rptProductItems.DataBind();

            rptCategoryGroups.DataSource = new List<Containers.ProductCategoryGroup.GroupName>() { Containers.ProductCategoryGroup.GroupName.GRUPPER, Containers.ProductCategoryGroup.GroupName.GUIDER };
            rptCategoryGroups.DataBind();
        }

        private void fillEdit()
        {            
            var product = _dataHandler.GetProduct(ProductId.Value);

            imgProductImage.Visible = (product.ImagePath.Trim().Length > 0);

            imgProductImage.ImageUrl = config.customerpath + "/articles/images/" + product.ImagePath;

            tbName.Text = product.Name;
        }

        protected void rptProductItems_Bound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblLanguageTitle = (Label)e.Item.FindControl("lblLanguageTitle");

                TextBox tbName = (TextBox)e.Item.FindControl("tbName");
                TextBox tbGroupName = (TextBox)e.Item.FindControl("tbGroupName");
                TextBox tbDescription = (TextBox)e.Item.FindControl("tbDescription");
                HiddenField hfLanguage = (HiddenField)e.Item.FindControl("hfLanguage");

                hfLanguage.Value = e.Item.DataItem.ToString();
                lblLanguageTitle.Text = hfLanguage.Value;

                if (ProductId.HasValue)
                {
                    var product = _dataHandler.GetProduct(ProductId.Value);

                    if (product.ProductItems.Any(p => p.Language == hfLanguage.Value))
                    {
                        var item = product.ProductItems.Single(p => p.Language == hfLanguage.Value);
                        tbName.Text = item.Name;
                        tbGroupName.Text = item.GroupName;
                        tbDescription.Text = item.Description;
                    }
                }
            }
        }

        protected void rptCategoryGroups_Bound(object sender, RepeaterItemEventArgs e)
        {
            var groupType = (Containers.ProductCategoryGroup.GroupName)e.Item.DataItem;

            Label lblCategoryGroupTitle = (Label)e.Item.FindControl("lblCategoryGroupTitle");
            CheckBoxList cblCategories = (CheckBoxList)e.Item.FindControl("cblCategories");

            lblCategoryGroupTitle.Text = groupType.ToString();
            cblCategories.DataSource = _dataHandler.GetCategories(groupType);
            cblCategories.DataValueField = "ID";
            cblCategories.DataTextField = "Name";
            cblCategories.DataBind();

            if (ProductId.HasValue)
            {
                var product = _dataHandler.GetProduct(ProductId.Value);

                foreach (ListItem li in cblCategories.Items)
                {
                    if (product.ProductCategories.Any(p => p == Convert.ToInt32(li.Value)))
                        li.Selected = true;
                }
            }
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            Containers.Product product = new Containers.Product();
            
            product.Name = tbName.Text;

            if (fuProductImage.HasFile)
            {
                var imagename = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(fuProductImage.FileName);
                fuProductImage.SaveAs(Server.MapPath(config.customerpath + "/articles/images/" + imagename));
                product.ImagePath = imagename;
            }
            else
                product.ImagePath = String.Empty;

            product.ProductItems = new List<Containers.ProductItem>();

            foreach (RepeaterItem rptItem in rptProductItems.Items)
            {                
                TextBox tbItemName = (TextBox)rptItem.FindControl("tbName");
                TextBox tbItemGroupName = (TextBox)rptItem.FindControl("tbGroupName");
                TextBox tbItemDescription = (TextBox)rptItem.FindControl("tbDescription");
                HiddenField hfItemLanguage = (HiddenField)rptItem.FindControl("hfLanguage");

                product.ProductItems.Add(new Containers.ProductItem()
                {
                    Name = tbItemName.Text,
                    GroupName = tbItemGroupName.Text,
                    Description = tbItemDescription.Text,
                    Language = hfItemLanguage.Value
                });
            }

            product.ProductCategories = new List<int>();

            foreach (RepeaterItem rptCat in rptCategoryGroups.Items)
            {
                CheckBoxList cblCategories = (CheckBoxList)rptCat.FindControl("cblCategories");

                foreach (ListItem li in cblCategories.Items)
                    if (li.Selected)
                        product.ProductCategories.Add(Convert.ToInt32(li.Value));
            }            

            _dataHandler.AddProduct(ProductId, product);

            Response.Redirect("Overview.aspx?S=Y");
        }
    }
}