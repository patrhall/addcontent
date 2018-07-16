using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace AimBusiness.Customers.Mekpoint.Website
{
    public partial class Agency : AIM.Base.Modules.Page.AimBusiness.Mekpoint.Website.AimBusiness
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
        }

        private void clearFields()
        {
            tbName.Text = String.Empty;
            tbCountry.Text = String.Empty;
            tbUrl.Text = String.Empty;
            tbUrlText.Text = String.Empty;
            tbDescription.Text = String.Empty;
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
            var agency = dbMekpoint.Agencies.Single(p => p.Id == id);

            // Fill fields with data
            tbName.Text = agency.Name;
            tbCountry.Text = agency.Country;
            tbUrl.Text = agency.Url;
            tbUrlText.Text = agency.UrlText;
            tbDescription.Text = agency.Description;
        }

        protected void rgAgency_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("30"));
                PageSizeCombo.FindItemByText("30").Attributes.Add("ownerTableViewId", rgAgency.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("60"));
                PageSizeCombo.FindItemByText("60").Attributes.Add("ownerTableViewId", rgAgency.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("90"));
                PageSizeCombo.FindItemByText("90").Attributes.Add("ownerTableViewId", rgAgency.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("120"));
                PageSizeCombo.FindItemByText("120").Attributes.Add("ownerTableViewId", rgAgency.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void rgAgency_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var agencies = (from o in dbMekpoint.Agencies
                            orderby o.Id descending
                            select new
                            {
                                Id = o.Id,
                                Name = o.Name,
                                Country = o.Country,
                                Company = "<a target=blank href=\"" + o.Url + "\">" + o.UrlText + "</a>"
                            });

            if (!String.IsNullOrEmpty(hfLatestFilter.Value))
            {
                agencies = (from o in agencies
                            orderby o.Id descending
                            select o);
            }

            else if (!String.IsNullOrEmpty(hfLetterFilter.Value))
            {
                agencies = (from o in agencies
                            where o.Name.ToLower().Substring(0, 1).Equals(hfLetterFilter.Value.ToLower())
                            select o);
            }

            else if (!String.IsNullOrEmpty(hfSearchFilter.Value))
            {
                string searchString = hfSearchFilter.Value.Trim().ToLower();

                agencies = (from o in agencies
                            where o.Name.ToLower().Contains(searchString) ||
                            o.Country.ToLower().Contains(searchString) ||
                            o.Company.ToLower().Contains(searchString)
                            select o);
            }

            rgAgency.DataSource = agencies;
        }

        protected void lbLetter_Click(object sender, EventArgs e)
        {
            LinkButton senderButton = (LinkButton)sender;

            enableAllLetters();

            senderButton.Enabled = false;

            hfLetterFilter.Value = senderButton.Text;
            hfSearchFilter.Value = String.Empty;
            tbSearch.Text = String.Empty;
            hfLatestFilter.Value = String.Empty;

            rgAgency.MasterTableView.Rebind();
        }

        protected void btnSearchSubmit_Click(object sender, EventArgs e)
        {
            hfSearchFilter.Value = tbSearch.Text;
            hfLetterFilter.Value = String.Empty;
            hfLatestFilter.Value = String.Empty;

            rgAgency.MasterTableView.Rebind();
        }

        protected void lbLatestFilter_Click(object sender, EventArgs e)
        {
            hfSearchFilter.Value = String.Empty;
            tbSearch.Text = String.Empty;
            hfLetterFilter.Value = String.Empty;
            enableAllLetters();

            hfLatestFilter.Value = "1";

            rgAgency.MasterTableView.Rebind();
        }

        protected void enableAllLetters()
        {
            foreach (var child in pnlLetters.Controls)
            {
                if (child.GetType() == typeof(LinkButton))
                {
                    ((LinkButton)child).Enabled = true;
                }
            }
        }

        protected void rgAgency_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridCommandItem)
            {
                try
                {
                    GridCommandItem cmditm = (GridCommandItem)e.Item;
                    LinkButton lbNew = (LinkButton)cmditm.Controls[0].FindControl("lbNew");

                    lbNew.Text = "<img class=\"textimage\" src=\"/Images/newobject.gif\" alt=\"Ny agentur/produkt\" />&nbsp;Ny agentur/produkt";
                }
                catch { }
            }
        }

        protected void rgAgency_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "newitem":
                    showEditNew();
                    break;
                case "edititem":
                    showEditObject(int.Parse(rgAgency.MasterTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString()));
                    break;
                case "deleteitem":
                    var agencies = (from o in dbMekpoint.Agencies
                                  where o.Id == int.Parse(rgAgency.MasterTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString())
                                  select o);
                    dbMekpoint.Agencies.DeleteAllOnSubmit(agencies);
                    dbMekpoint.SubmitChanges();

                    rgAgency.Rebind();

                    break;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(hfID.Value))
            {
                int id = int.Parse(hfID.Value);

                if (dbMekpoint.Agencies.Any(p => p.Id == id))
                {
                    var agency = dbMekpoint.Agencies.Single(p => p.Id == id);

                    agency.Name = tbName.Text;
                    agency.Country = tbCountry.Text;
                    agency.Url = tbUrl.Text;
                    agency.UrlText = tbUrlText.Text;
                    agency.Description = tbDescription.Text;

                    dbMekpoint.SubmitChanges();

                    rgAgency.Rebind();

                    showOverview(true);
                }
            }
            else
            {
                var agency = new AIM.Business.AimBusiness.Mekpoint.Website.Mekpoint.Agency();

                agency.Name = tbName.Text;
                agency.Country = tbCountry.Text;
                agency.Url = tbUrl.Text;
                agency.UrlText = tbUrlText.Text;
                agency.Description = tbDescription.Text;

                dbMekpoint.Agencies.InsertOnSubmit(agency);
                dbMekpoint.SubmitChanges();

                rgAgency.Rebind();

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