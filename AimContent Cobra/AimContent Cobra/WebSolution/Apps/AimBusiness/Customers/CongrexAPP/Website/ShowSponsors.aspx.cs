using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AIM.Business.AimBusiness.CongrexAPP.Website;
using Telerik.Web.UI;

namespace AimBusiness.Customers.CongrexAPP.Website
{
    public partial class ShowSponsors : System.Web.UI.Page
    {
        DataObjectsDataContext dbCongrex = new DataObjectsDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void rgSponsors_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            var Sponsors = from s in dbCongrex.Sponsors
                           where s.Site_FK == 116
                           orderby s.SortOrder
                           select s;

            rgSponsors.DataSource = Sponsors;
        }

        protected void moveDown_Click(object sender, EventArgs e)
        {
            Button item = (Button)sender;
            string id = item.CommandArgument;

                var Sponsor = (from d in dbCongrex.Sponsors
                             where d.Id == int.Parse(id)
                             select d).Single();
                if (Sponsor.SortOrder >= 1)
                {
                    Sponsor.SortOrder = Sponsor.SortOrder - 1;
                    dbCongrex.SubmitChanges();
                    rgSponsors.Rebind();
                }
        }

        protected void moveUp_Click(object sender, EventArgs e)
        {
            Button item = (Button)sender;
            string id = item.CommandArgument;

            var Sponsor = (from d in dbCongrex.Sponsors
                             where d.Id == int.Parse(id)
                             select d).Single();
            if (Sponsor.SortOrder <= rgSponsors.MasterTableView.Items.Count - 1)
                {
                    Sponsor.SortOrder = Sponsor.SortOrder + 1;
                    dbCongrex.SubmitChanges();
                    rgSponsors.Rebind();
                }
            
        }

        protected void DelTask_Click(object sender, EventArgs e)
        {
            Button item = (Button)sender;
            string id = item.CommandArgument;

            var Sponsor = (from d in dbCongrex.Sponsors
                           where d.Id == int.Parse(id)
                           select d).Single();

            dbCongrex.Sponsors.DeleteOnSubmit(Sponsor);
            dbCongrex.SubmitChanges();
            rgSponsors.Rebind();
        }

        protected void RadGrid_ItemDataBound(object aSender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                GridTableCell linkCell = (GridTableCell)item["Name"];
                LinkButton reportLink = (LinkButton)linkCell.FindControl("lbName");

                reportLink.Text = item.GetDataKeyValue("Name").ToString();

                reportLink.PostBackUrl = "Sponsors.aspx?sId=" + item.GetDataKeyValue("Id").ToString();
            }
        }
    }
}