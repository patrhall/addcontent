using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.IO;

namespace AimBusiness.Customers.Runo.EventsWebsite
{
    public partial class Overview : AIM.Base.Modules.Page.AimBusiness.Runo.EventWebsite.AimBusiness
    {
        private Logic.EventHandler EventHandler { get; set; }
        private Logic.Containers.Event Item
        {
            get
            {
                return EventHandler.Events.Single(p => p.ObjectId == Convert.ToInt32(Request["oID"]));
            }
        }

        protected override void Page_Load()
        {
            EventHandler = new Logic.EventHandler(config.customerpath);

            if (!IsPostBack)
                init();
        }

        private void init()
        {
            if (!EventHandler.Events.Any(p => p.ObjectId == Convert.ToInt32(Request["oID"])))
            {
                Logic.Containers.Event item = new Logic.Containers.Event();
                item.Id = Guid.NewGuid();
                item.ObjectId = Convert.ToInt32(Request["oID"]);
                item.Title = String.Empty;
                item.SubTitle = String.Empty;
                item.Description = String.Empty;
                item.ImagePath = String.Empty;
                item.Price = String.Empty;
                item.ActivityPrice = String.Empty;
                item.EventList = new List<string>();
                item.EventItems = new List<Logic.Containers.EventItem>();
                item.LastUpdated = DateTime.Now;
                item.Created = DateTime.Now;
                item.IsDeleted = false;
                EventHandler.Events.Add(item);
                EventHandler.Save();
            }
            else
            {
                tbTitle.Text = Item.Title;
                tbSubTitle.Text = Item.SubTitle;
                tbDescription.Text = Item.Description;

                if (Item.ImagePath != "")
                {
                    imgImagePath.ImageUrl = config.customerpath + "articles/images/" + Item.ImagePath;
                    imgImagePath.Visible = true;
                }

                tbPrice.Text = Item.Price;
                tbActivityPrice.Text = Item.ActivityPrice;
            }

            bindEventList();
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            Item.Title = tbTitle.Text;
            Item.SubTitle = tbSubTitle.Text;
            Item.Description = tbDescription.Text;

            if (fuImagePath.HasFile && (Path.GetExtension(fuImagePath.FileName) == ".jpg" || Path.GetExtension(fuImagePath.FileName) == ".png"))
            {
                string filename = Guid.NewGuid() + Path.GetExtension(fuImagePath.FileName);
                fuImagePath.SaveAs(Server.MapPath(config.customerpath + "articles/images/" + filename));
                Item.ImagePath = filename;
            }

            Item.Price = tbPrice.Text;
            Item.ActivityPrice = tbActivityPrice.Text;
            Item.LastUpdated = DateTime.Now;

            Item.EventList = new List<string>();

            foreach (RepeaterItem ri in rptEventList.Items)
                if (((TextBox)ri.FindControl("tbEventItem")).Text != "")
                    Item.EventList.Add(((TextBox)ri.FindControl("tbEventItem")).Text);

            EventHandler.Save();

            Response.Redirect(Request.Url.ToString());
        }

        protected void lbNewEventList_Click(object sender, EventArgs e)
        {
            Item.EventList.Add("");

            bindEventList();

            EventHandler.Save();
        }

        private void bindEventList()
        {
            List<string> oldValues = new List<string>();

            if (IsPostBack)
            {
                foreach (RepeaterItem ri in rptEventList.Items)
                    oldValues.Add(((TextBox)ri.FindControl("tbEventItem")).Text);
            }
            else
                oldValues = Item.EventList;

            if (Item.EventList.Count == 0)
                Item.EventList.Add("");

            rptEventList.DataSource = Item.EventList;
            rptEventList.DataBind();


            for (int i = 0; i < rptEventList.Items.Count; i++)
            {
                if (i >= oldValues.Count)
                    break;

                ((TextBox)rptEventList.Items[i].FindControl("tbEventItem")).Text = oldValues[i];

            }
        }

        protected void rgEventItems_Source(object sender, GridNeedDataSourceEventArgs e)
        {
            rgEventItems.DataSource = Item.EventItems.Where(p => !p.IsDeleted);

            viewEventItemForm(false);
        }

        protected void rgEventItems_Command(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "NEW":
                    editEventItem(null);
                    break;
                case "CHANGE":
                    editEventItem(new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString()));
                    break;
                case "REMOVE":
                    var itemToDelete = Item.EventItems.Single(p => p.Id == new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString()));
                    itemToDelete.IsDeleted = true;
                    EventHandler.Save();
                    rgEventItems.Rebind();
                    break;
            }
        }

        private void viewEventItemForm(bool isEdit)
        {
            hfEventItemId.Value = Guid.Empty.ToString();
            tbEventItemTitle.Text = "";
            imgEventItemImagePath.Visible = false;
            tbEventItemDescription.Text = "";

            pnlEventItemForm.Visible = isEdit;
            rgEventItems.Visible = !isEdit;
        }

        private void editEventItem(Guid? Id)
        {
            viewEventItemForm(true);

            if (Id.HasValue)
            {
                var eventItem = Item.EventItems.Single(p => p.Id == Id.Value);

                hfEventItemId.Value = eventItem.Id.ToString();
                tbEventItemTitle.Text = eventItem.Title;
                tbEventItemDescription.Text = eventItem.Description;

                if (eventItem.ImagePath != "")
                {
                    imgEventItemImagePath.Visible = true;
                    imgEventItemImagePath.ImageUrl = config.customerpath + "articles/images/" + eventItem.ImagePath;
                }
            }            
        }

        protected void btEventItemSave_Click(object sender, EventArgs e)
        {            
            Guid Id = new Guid(hfEventItemId.Value);

            Logic.Containers.EventItem eItem;

            if (Id == Guid.Empty)
            {
                eItem = new Logic.Containers.EventItem()
                {
                    Id = Guid.NewGuid(),
                    Created = DateTime.Now,
                    IsDeleted = false,
                };

                Item.EventItems.Add(eItem);
            }
            else
                eItem = Item.EventItems.Single(p => p.Id == Id);

            eItem.LastUpdated = DateTime.Now;
            eItem.Title = tbEventItemTitle.Text;
            eItem.Description = tbEventItemDescription.Text;

            if (fuEventItemImagePath.HasFile && (Path.GetExtension(fuEventItemImagePath.FileName) == ".jpg" || Path.GetExtension(fuEventItemImagePath.FileName) == ".png"))
            {
                string filename = Guid.NewGuid() + Path.GetExtension(fuEventItemImagePath.FileName);
                fuEventItemImagePath.SaveAs(Server.MapPath(config.customerpath + "articles/images/" + filename));
                eItem.ImagePath = filename;
            }

            EventHandler.Save();

            rgEventItems.Rebind();
        }

        protected void lbEventItemBack_Click(object sender, EventArgs e)
        {
            viewEventItemForm(false);
        }
    }
}