using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Profile;
using System.Data.Linq;
using System.Xml.Linq;
using System.Linq;
using Telerik.Web.UI;
using System.Collections.Generic;
using AIM.General.UserControls;

namespace AimEdit
{
    public partial class EditWidgets : AIM.Base.Modules.Page.AimEdit
    {
        private int ObjectID
        {
            get
            {
                return Convert.ToInt32(Request["oID"]);
            }
        }

        protected override void Page_Load()
        {
            setLanguage();

            if (!IsPostBack)
            {
                init();
            }
        }

        private void init()
        {
            ddSiteWidgets.DataSource = (from o in db.ObjectWidgets
                                        where o.Site_FK == config.siteID
                                        orderby o.Name
                                        select o);

            ddSiteWidgets.DataValueField = "ID";
            ddSiteWidgets.DataTextField = "Name";
            ddSiteWidgets.DataBind();

            ddWidgetPlaceHolders.DataSource = (from o in db.ObjectWidgetPlaceHolders
                                               where o.Site_FK == config.siteID
                                               orderby o.Name
                                               select o);
            ddWidgetPlaceHolders.DataValueField = "ID";
            ddWidgetPlaceHolders.DataTextField = "Name";
            ddWidgetPlaceHolders.DataBind();

            try
            {
                ddWidgetPlaceHolders.SelectedIndex = 0;
            }
            catch { }

            if (ddWidgetPlaceHolders.Items.Count == 0)
            {
                pnlEditWidget.Visible = false;
                pnlNoWidgetsUsed.Visible = true;
                pnlOverview.Visible = false;
            }
        }

        protected void rgObjectWidgets_Source(object sender, GridNeedDataSourceEventArgs e)
        {
            rgObjectWidgets.DataSource = (from o in db.Object_X_ObjectWidgets
                                          where o.ObjectWidgetPlaceHolder.Site_FK == config.siteID &&
                                          (!o.Object_FK.HasValue || o.Object_FK == ObjectID) &&
                                          o.ObjectWidgetPlaceHolder_FK == int.Parse(ddWidgetPlaceHolders.SelectedValue) &&
                                          !o.Deleted                                          
                                          orderby o.Object_FK.HasValue, o.SortOrder
                                          select new
                                          {
                                              o.ID,
                                              o.Name,
                                              WidgetName = o.ObjectWidget.Name,
                                              DefaultWidget = !o.Object_FK.HasValue,
                                              o.Published,
                                              MoveDownText = (o.Object_FK.HasValue ? "Flytta ner" : "-"),
                                              MoveUpText =(o.Object_FK.HasValue ? "Flytta upp" : "-"),
                                              PublishText = (o.Published ? "Avpublicera" : "Publicera")
                                          });

            rptObjectWidgetProperties.DataSource = null;
            rptObjectWidgetProperties.DataBind();

            pnlOverview.Visible = true;
            pnlEditWidget.Visible = false;
        }        

        private void setLanguage()
        {
            lblEditDefaultDescription.Text = Language.getValue("lblEditDefaultDescription_Text");
            cbEditDefault.Text = Language.getValue("cbEditDefault_Text");
            lblEditLocalName.Text = Language.getValue("lblEditLocalName_Text");
            btAdd.Text = Language.getValue("btAdd_Text");
            lblNew.Text = Language.getValue("lblNew_Text");
            lblArea.Text = Language.getValue("Area_Text");
            lblName.Text = Language.getValue("lblEditLocalName_Text");
        }

        protected void rgObjectWidgets_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "MoveUp" || e.CommandName == "MoveDown" || e.CommandName == "PUBLISH" || e.CommandName == "Remove" || e.CommandName == "Change")
            {
                var itemId = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"]);

                switch (e.CommandName)
                {
                    case "MoveUp":
                        moveWidget(itemId, false);
                        break;
                    case "MoveDown":
                        moveWidget(itemId, true);
                        break;
                    case "PUBLISH":
                        publishWidget(itemId);
                        break;
                    case "Change":
                        fillEdit(itemId);
                        break;
                    case "Remove":
                        deleteWidgetFromPlaceHolder(itemId);
                        break;
                }
            }
        }

        protected void dd_Changed(object sender, EventArgs e)
        {
            rgObjectWidgets.Rebind();
        }

        private void fillEdit(int itemId)
        {
            pnlEditWidget.Visible = true;
            pnlOverview.Visible = false;

            var item = db.Object_X_ObjectWidgets.Single(p => p.ID == itemId);

            hfEditWidgetId.Value = itemId.ToString();
            cbEditDefault.Checked = !item.Object_FK.HasValue;
            tbEditLocalName.Text = item.Name;

            foreach (var aType in item.ObjectWidget.ObjectWidgetProperties)
            {
                if (!aType.ObjectWidgetPropertyValues.Any(p => p.ObjectXObjectWidget_FK == itemId))
                {
                    var aValue = new AIM.Business.AimEdit.ObjectWidgetPropertyValue();
                    aValue.ObjectWidgetProperty = aType;
                    aValue.ObjectXObjectWidget_FK = itemId;
                    aValue.Value = String.Empty;
                    db.ObjectWidgetPropertyValues.InsertOnSubmit(aValue);
                }
            }

            db.SubmitChanges();

            rptObjectWidgetProperties.DataSource = (from o in item.ObjectWidgetPropertyValues
                                                    select o.ID);
            rptObjectWidgetProperties.DataBind();
        }

        private void deleteWidgetFromPlaceHolder(int itemId)
        {
            var item = db.Object_X_ObjectWidgets.Single(p => p.ID == itemId);
            item.Deleted = true;
            db.SubmitChanges();
            rgObjectWidgets.Rebind();
        }

        private void publishWidget(int itemId)
        {
            var item = db.Object_X_ObjectWidgets.Single(p => p.ID == itemId);
            item.Published = !item.Published;
            db.SubmitChanges();
            rgObjectWidgets.Rebind();
        }

        private void moveWidget(int itemId, bool moveDown)
        {
            var item = db.Object_X_ObjectWidgets.Single(p => p.ID == itemId);
            var items = (from o in db.Object_X_ObjectWidgets
                         where o.Object_FK.HasValue &&
                         o.Object_FK.Value == ObjectID &&
                         o.ObjectWidgetPlaceHolder_FK == Convert.ToInt32(ddWidgetPlaceHolders.SelectedValue) &&
                         !o.Deleted
                         orderby o.SortOrder
                         select o).ToList();

            var itemIndex = items.IndexOf(item);

            if((itemIndex == items.Count - 1 && moveDown) || (itemIndex == 0 && !moveDown))
                return;

            for (int i = 0; i < items.Count; i++)
            {
                if (!moveDown)
                {
                    if (i == itemIndex - 1)
                        items[i].SortOrder = itemIndex;
                    else if (i == itemIndex)
                        items[i].SortOrder = (itemIndex - 1);
                    else
                        items[i].SortOrder = i;
                }
                else
                {
                    if (i == itemIndex + 1)
                        items[i].SortOrder = itemIndex;
                    else if (i == itemIndex)
                        items[i].SortOrder = (itemIndex + 1);
                    else
                        items[i].SortOrder = i;
                }
            }

            db.SubmitChanges();

            rgObjectWidgets.Rebind();
        }

        protected void btAdd_Click(object sender, EventArgs e)
        {
            var item = new AIM.Business.AimEdit.Object_X_ObjectWidget();
            item.Object_FK = ObjectID;
            item.ObjectWidget_FK = Convert.ToInt32(ddSiteWidgets.SelectedValue);
            item.ObjectWidgetPlaceHolder_FK = Convert.ToInt32(ddWidgetPlaceHolders.SelectedValue);
            item.SortOrder = 999999;
            item.Name = tbName.Text;
            item.Published = false;
            item.Deleted = false;
            db.Object_X_ObjectWidgets.InsertOnSubmit(item);
            db.SubmitChanges();

            rgObjectWidgets.Rebind();
        }

        protected void rptObjectWidgetProperties_Bound(object sender, RepeaterItemEventArgs e)
        {
            int valueId = Convert.ToInt32(e.Item.DataItem);
            var hfObjectWidgetPropertyId = (HiddenField)e.Item.FindControl("hfObjectWidgetPropertyId");
            Label lblTitle = (Label)e.Item.FindControl("lblTitle");
            TextBox tbText = (TextBox)e.Item.FindControl("tbText");
            RadEditor radRichText = (RadEditor)e.Item.FindControl("radRichText");
            RadDatePicker rdpDate = (RadDatePicker)e.Item.FindControl("rdpDate");
            RequiredFieldValidator reqItem = (RequiredFieldValidator)e.Item.FindControl("reqItem");
            RegularExpressionValidator regItem = (RegularExpressionValidator)e.Item.FindControl("regItem");

            var item = (from o in db.ObjectWidgetPropertyValues
                        where o.ID == valueId
                        select new
                        {
                            o.ID,
                            o.ObjectWidgetProperty.PropertyName,
                            o.Value,
                            TypeShortName = o.ObjectWidgetProperty.ObjectWidgetPropertyType.ShortName
                        }).Single();


            lblTitle.Text = item.PropertyName;
            hfObjectWidgetPropertyId.Value = item.ID.ToString();

            switch (item.TypeShortName)
            {
                case "TEXT":
                    tbText.Visible = true;
                    reqItem.ControlToValidate = "tbText";
                    tbText.TextMode = TextBoxMode.SingleLine;
                    tbText.Text = item.Value;
                    break;
                case "HTML":
                    radRichText.Visible = true;
                    reqItem.ControlToValidate = "radRichText";
                    radRichText.Content = item.Value;
                    AIM.Base.Classes.RadHandler.RadEditor_LoadEditorPreference(ref radRichText, config);
                    break;
                case "DATE":
                    rdpDate.Visible = true;
                    reqItem.ControlToValidate = "rdpDate";

                    DateTime selectedDate = DateTime.Now;
                    DateTime.TryParse(item.Value, out selectedDate);
                    rdpDate.SelectedDate = selectedDate;
                    break;
                case "NUMBER":
                    tbText.Visible = true;
                    reqItem.ControlToValidate = "tbText";
                    tbText.TextMode = TextBoxMode.SingleLine;
                    tbText.Text = item.Value;
                    regItem.Enabled = true;
                    regItem.ValidationExpression = "[0-9]+";
                    regItem.ControlToValidate = "tbText";
                    break;
                case "MULTILINE":
                    tbText.Visible = true;
                    reqItem.ControlToValidate = "tbText";
                    tbText.TextMode = TextBoxMode.MultiLine;
                    tbText.Text = item.Value;
                    break;
            }
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem ri in rptObjectWidgetProperties.Items)
            {
                var hfObjectWidgetPropertyId = (HiddenField)ri.FindControl("hfObjectWidgetPropertyId");
                Label lblTitle = (Label)ri.FindControl("lblTitle");
                TextBox tbText = (TextBox)ri.FindControl("tbText");
                RadEditor radRichText = (RadEditor)ri.FindControl("radRichText");
                RadDatePicker rdpDate = (RadDatePicker)ri.FindControl("rdpDate");

                var item = db.ObjectWidgetPropertyValues.Single(p => p.ID == Convert.ToInt32(hfObjectWidgetPropertyId.Value));

                switch (item.ObjectWidgetProperty.ObjectWidgetPropertyType.ShortName)
                {
                    case "TEXT":
                    case "NUMBER":
                    case "MULTILINE":
                        item.Value = tbText.Text;
                        break;
                    case "HTML":
                        item.Value = radRichText.Content;                        
                        break;
                    case "DATE":
                        item.Value = rdpDate.SelectedDate.Value.ToString();                        
                        break;                                            
                }

                db.SubmitChanges();
            }

            var itemX = db.Object_X_ObjectWidgets.Single(p => p.ID == Convert.ToInt32(Convert.ToInt32(hfEditWidgetId.Value)));
            itemX.Name = tbEditLocalName.Text;

            if (cbEditDefault.Checked)
                itemX.Object_FK = null;
            else
                itemX.Object_FK = ObjectID;

            db.SubmitChanges();

            rgObjectWidgets.Rebind();
        }

        protected void btBack_Click(object sender, EventArgs e)
        {
            rgObjectWidgets.Rebind();
        }
    }
}
