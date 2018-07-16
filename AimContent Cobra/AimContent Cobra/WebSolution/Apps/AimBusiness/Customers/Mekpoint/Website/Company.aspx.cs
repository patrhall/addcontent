using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace AimBusiness.Customers.Mekpoint.Website
{
    public partial class Company : AIM.Base.Modules.Page.AimBusiness.Mekpoint.Website.AimBusiness
    {
        protected override void Page_Load()
        {
            if (!IsPostBack)
                init();
        }

        protected void showOverview(bool show)
        {
            pnlOverview.Visible = show;
            pnlDetail.Visible = !show;
        }

        private void init()
        {
            showOverview(true);

            var categories = (from o in dbMekpoint.CompanyCategories
                              orderby o.Name
                              select o);

            ddlCategoryFilter.DataTextField = "Name";
            ddlCategoryFilter.DataValueField = "Id";
            ddlCategoryFilter.DataSource = categories;
            ddlCategoryFilter.DataBind();
            ddlCategoryFilter.Items.Insert(0, new ListItem("Visa alla", "0"));

            ddlCategories.DataTextField = "Name";
            ddlCategories.DataValueField = "Id";
            ddlCategories.DataSource = categories;
            ddlCategories.DataBind();
        }

        private void clearFields()
        {
            tbName.Text = String.Empty;
            tbUrl.Text = String.Empty;
            tbDescription.Text = String.Empty;
            ddlCategories.SelectedIndex = 0;
            cbLK.Checked = false;
            cbEP.Checked = false;
            cbCC.Checked = false;
        }

        private void showEditNew()
        {
            showOverview(false);
            hfID.Value = String.Empty;

            clearFields();
        }

        private void showEditObject(int id)
        {
            showOverview(false);
            hfID.Value = id.ToString();
            fillFields(id);
        }

        private void fillFields(int id)
        {
            var company = dbMekpoint.Companies.Single(p => p.Id == id);

            // Fill fields with data
            tbName.Text = company.Name;
            tbUrl.Text = company.Url;
            tbDescription.Text = company.Description;
            ddlCategories.SelectedValue = company.CompanyCategory_FK.ToString();
            cbLK.Checked = company.LK == 1;
            cbEP.Checked = company.EP == 1;
            cbCC.Checked = company.CC == 1;
        }

        protected void rgCompany_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("30"));
                PageSizeCombo.FindItemByText("30").Attributes.Add("ownerTableViewId", rgCompany.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("60"));
                PageSizeCombo.FindItemByText("60").Attributes.Add("ownerTableViewId", rgCompany.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("90"));
                PageSizeCombo.FindItemByText("90").Attributes.Add("ownerTableViewId", rgCompany.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("120"));
                PageSizeCombo.FindItemByText("120").Attributes.Add("ownerTableViewId", rgCompany.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            } 
        }

        protected void rgCompany_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var companies = (from o in dbMekpoint.Companies
                             orderby o.Id descending
                             select new
                             {
                                 Id = o.Id,
                                 Name = o.Name + 
                                 (o.LK == 1 ? "&nbsp; <img alt=\"Ledig Kapacitet\" src=\"images/LK.gif\">" : "") +
                                 (o.EP == 1 ? "&nbsp; <img alt=\"Egna Produkter\" src=\"images/EP.gif\">" : "") +
                                 (o.CC == 1 ? "&nbsp; <img alt=\"CAD/CAM\" src=\"images/CC.gif\">" : ""),
                                 Url = o.Url,
                                 Description = o.Description,
                                 CompanyCategory_FK = o.CompanyCategory_FK
                             });

            if (!String.IsNullOrEmpty(hfLatestFilter.Value))
            {
                companies = (from o in companies
                             orderby o.Id descending
                             select o);
            }
            else
            {
                if (!String.IsNullOrEmpty(hfSearchFilter.Value))
                {
                    string searchString = hfSearchFilter.Value.Trim().ToLower();

                    companies = (from o in companies
                                 where o.Name.ToLower().Contains(searchString) ||
                                 o.Url.ToLower().Contains(searchString) ||
                                 o.Description.ToLower().Contains(searchString)
                                 select o);
                }

                if (ddlCategoryFilter.SelectedIndex != 0)
                {
                    companies = (from o in companies
                                 where o.CompanyCategory_FK == Convert.ToInt32(ddlCategoryFilter.SelectedValue)
                                 select o);
                }
            }

            rgCompany.DataSource = companies;
        }

        protected void btnSearchSubmit_Click(object sender, EventArgs e)
        {
            hfSearchFilter.Value = tbSearch.Text;
            hfLatestFilter.Value = String.Empty;

            rgCompany.MasterTableView.Rebind();
        }

        protected void ddlCategoryFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            hfLatestFilter.Value = String.Empty;
            rgCompany.MasterTableView.Rebind();
        }

        protected void lbLatestFilter_Click(object sender, EventArgs e)
        {
            hfSearchFilter.Value = String.Empty;
            tbSearch.Text = String.Empty;
            ddlCategoryFilter.SelectedIndex = 0;

            hfLatestFilter.Value = "1";

            rgCompany.MasterTableView.Rebind();
        }

        protected void rgCompany_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridCommandItem)
            {
                try
                {
                    GridCommandItem cmditm = (GridCommandItem)e.Item;
                    LinkButton lbNew = (LinkButton)cmditm.Controls[0].FindControl("lbNew");

                    lbNew.Text = "<img class=\"textimage\" src=\"/Images/newobject.gif\" alt=\"Nytt företag\" />&nbsp;Nytt företag";
                }
                catch { }
            }
        }

        protected void rgCompany_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "newitem":
                    showEditNew();
                    break;
                case "edititem":
                    showEditObject(int.Parse(rgCompany.MasterTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString()));
                    break;
                case "deleteitem":
                    var companies = (from o in dbMekpoint.Companies
                                    where o.Id == int.Parse(rgCompany.MasterTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString())
                                    select o);
                    dbMekpoint.Companies.DeleteAllOnSubmit(companies);
                    dbMekpoint.SubmitChanges();

                    rgCompany.Rebind();

                    break;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(hfID.Value))
            {
                int id = int.Parse(hfID.Value);

                if (dbMekpoint.Companies.Any(p => p.Id == id))
                {
                    var company = dbMekpoint.Companies.Single(p => p.Id == id);

                    company.Name = tbName.Text;
                    company.Url = tbUrl.Text;
                    company.Description = tbDescription.Text;
                    company.CompanyCategory_FK = Convert.ToInt32(ddlCategories.SelectedValue);
                    company.LK = cbLK.Checked ? 1 : 0;
                    company.EP = cbEP.Checked ? 1 : 0;
                    company.CC = cbCC.Checked ? 1 : 0;

                    dbMekpoint.SubmitChanges();

                    rgCompany.Rebind();

                    showOverview(true);
                }
            }
            else
            {
                var company = new AIM.Business.AimBusiness.Mekpoint.Website.Mekpoint.Company();

                company.Name = tbName.Text;
                company.Url = tbUrl.Text;
                company.Description = tbDescription.Text;
                company.CompanyCategory_FK = Convert.ToInt32(ddlCategories.SelectedValue);
                company.LK = cbLK.Checked ? 1 : 0;
                company.EP = cbEP.Checked ? 1 : 0;
                company.CC = cbCC.Checked ? 1 : 0;

                dbMekpoint.Companies.InsertOnSubmit(company);
                dbMekpoint.SubmitChanges();

                rgCompany.Rebind();

                showOverview(true);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            hfID.Value = String.Empty;
            clearFields();
            showOverview(true);
        }
    }
}