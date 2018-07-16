using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Telerik.Web.UI;

namespace AimBusiness.Customers.Boletus.Website
{
    public partial class Overview : AIM.Base.Modules.Page.AimBusiness.Boletus.Website.AimBusiness
    {
        private Common.DataHandler _dataHandler;

        protected override void Page_Load()
        {
            _dataHandler = new Common.DataHandler(config.customerpath);

            if (!String.IsNullOrEmpty(Request["S"]) && Request["S"] == "Y" && !IsPostBack)
                lblMsg.Text = "Produkten är sparad";
            else
                lblMsg.Visible = false;
        }
        
        protected void rgProducts_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "CHANGE" || e.CommandName == "REMOVE")
            {
                int id = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"]);

                switch (e.CommandName)
                {
                    case "CHANGE":
                        Response.Redirect("EditProduct.aspx?ID=" + id);
                        break;
                    case "REMOVE":
                        _dataHandler.DeleteProduct(id);
                        rgProducts.Rebind();
                        break;
                }
            }
        }        

        protected void rgProducts_Source(object sender, GridNeedDataSourceEventArgs e)
        {
            rgProducts.DataSource = _dataHandler.GetProducts();
        }
    }
}

//THIS CODE GENERATES XML FOR CATEGORIES

//Common.XmlHandler temp = new Common.XmlHandler();
//temp.FilePath = Server.MapPath(config.customerpath + "/articles/" + Common.DataHandler.ObjectName.categories.ToString() + ".xml");

//List<Containers.ProductCategoryGroup> bah = new List<Containers.ProductCategoryGroup>();
//Containers.ProductCategoryGroup g1 = new Containers.ProductCategoryGroup();
//g1.ShortName = Containers.ProductCategoryGroup.GroupName.GUIDES;            
//g1.Categories = new List<Containers.ProductCategory>();
//g1.Categories.Add(new Containers.ProductCategory() { ID = 1, Name = "Svampar", Created = DateTime.Now, LastUpdated = DateTime.Now });
//g1.Categories.Add(new Containers.ProductCategory() { ID = 2, Name = "Blommor", Created = DateTime.Now, LastUpdated = DateTime.Now });
//g1.Categories.Add(new Containers.ProductCategory() { ID = 3, Name = "Frukter", Created = DateTime.Now, LastUpdated = DateTime.Now });
//g1.Categories.Add(new Containers.ProductCategory() { ID = 4, Name = "Örter", Created = DateTime.Now, LastUpdated = DateTime.Now });
//g1.Categories.Add(new Containers.ProductCategory() { ID = 5, Name = "Rötter", Created = DateTime.Now, LastUpdated = DateTime.Now });
//g1.Categories.Add(new Containers.ProductCategory() { ID = 6, Name = "Löv", Created = DateTime.Now, LastUpdated = DateTime.Now });
//g1.Categories.Add(new Containers.ProductCategory() { ID = 7, Name = "Frön", Created = DateTime.Now, LastUpdated = DateTime.Now });
//g1.Categories.Add(new Containers.ProductCategory() { ID = 8, Name = "Bark & Lavar", Created = DateTime.Now, LastUpdated = DateTime.Now });
//bah.Add(g1);

//Containers.ProductCategoryGroup g2 = new Containers.ProductCategoryGroup();
//g2.ShortName = Containers.ProductCategoryGroup.GroupName.GROUPS;
//g2.Categories = new List<Containers.ProductCategory>();
//g2.Categories.Add(new Containers.ProductCategory() { ID = 9, Name = "Läkemedel", Created = DateTime.Now, LastUpdated = DateTime.Now });
//g2.Categories.Add(new Containers.ProductCategory() { ID = 10, Name = "Hälsokost", Created = DateTime.Now, LastUpdated = DateTime.Now });
//g2.Categories.Add(new Containers.ProductCategory() { ID = 11, Name = "Livsmedel", Created = DateTime.Now, LastUpdated = DateTime.Now });
//g2.Categories.Add(new Containers.ProductCategory() { ID = 12, Name = "Kosmetika", Created = DateTime.Now, LastUpdated = DateTime.Now });

//bah.Add(g2);

//temp.WriteDataItems(bah);