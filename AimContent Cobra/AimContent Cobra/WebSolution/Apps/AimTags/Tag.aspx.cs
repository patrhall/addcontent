using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AIM.Business;
using Telerik.Web.UI;
using AIM.Business.AimTags;

namespace AimTags
{
    public partial class Tag : AIM.Base.Modules.Page.AimTags
    {
        protected override void Page_Load()
        {
            if (!IsPostBack)
            {
                setLanguage();
                initTypeDropDown();
                int TagID = 0;
                if (!String.IsNullOrEmpty(Request["ID"]) && int.TryParse(Request["ID"].ToString(), out TagID))
                {
                    initEditMode(TagID);
                }
            }
        }

        private void initEditMode(int TagID)
        {
            pnlTagType.Visible = false;
            var theTag = (from t in db.Tags
                         where t.ID == TagID
                         select t).Single();

            tbTagName.Text = theTag.Name;
            tbTagSlug.Text = theTag.Slug;
            initTagProperties(theTag.TagType_FK);
            
        }

        private void initTagProperties(int TagTypeID)
        {
            var Props = from o in db.TagTypeProperties
                        where o.TagType_FK == TagTypeID
                        orderby o.Sort
                        select o;
            if (Props.Count() > 0)
            {
                rpTagProperties.DataSource = Props;
            }

            rpTagProperties.DataBind();
        }
        protected void rptTagProperties_Bound(object sender, RepeaterItemEventArgs e)
        {
            TagTypeProperty item = (TagTypeProperty)e.Item.DataItem;
            HiddenField hfTagPropertyTypeId = (HiddenField)e.Item.FindControl("hfTagPropertyTypeId");
            HiddenField hfTagPropertyValueId = (HiddenField)e.Item.FindControl("hfTagPropertyValueId");
            Label lblTitle = (Label)e.Item.FindControl("lblTitle");
            TextBox tbText = (TextBox)e.Item.FindControl("tbText");
            TextBox tbGeoPosition = (TextBox)e.Item.FindControl("tbGeoPosition");
            RadEditor radRichText = (RadEditor)e.Item.FindControl("radRichText");
            RadDatePicker rdpDate = (RadDatePicker)e.Item.FindControl("rdpDate");
            RequiredFieldValidator reqItem = (RequiredFieldValidator)e.Item.FindControl("reqItem");
            RegularExpressionValidator regItem = (RegularExpressionValidator)e.Item.FindControl("regItem");

            int TagID = 0;
            string tagpropertyvalue = "";
            bool hasvalue = false;
            if (!String.IsNullOrEmpty(Request["ID"]) && int.TryParse(Request["ID"].ToString(), out TagID))
            {
                var itemvalue = (from o in db.TagTypePropertyValues
                                 where o.TagTypeProperty_FK == item.ID && o.Tag_FK == TagID
                                 select new
                                 {
                                     o.ID,
                                     o.TagTypeProperty.Name,
                                     o.Value,
                                     TypeShortName = o.TagTypeProperty.Slug
                                 }).SingleOrDefault();
                if (itemvalue != null)
                {
                    hasvalue = true;
                    tagpropertyvalue = itemvalue.Value;
                    hfTagPropertyValueId.Value = itemvalue.ID.ToString();
                }
            }
            
            lblTitle.Text = item.Name;            
            hfTagPropertyTypeId.Value = item.ID.ToString();

            switch (item.TagPropertyType.Slug)
            {
                case "TEXT":
                    tbText.Visible = true;
                    reqItem.ControlToValidate = "tbText";
                    tbText.TextMode = TextBoxMode.SingleLine;
                    if(hasvalue)
                        tbText.Text = tagpropertyvalue;
                    break;
                case "HTML":
                    radRichText.Visible = true;
                    reqItem.ControlToValidate = "radRichText";
                    if (hasvalue)
                        radRichText.Content = tagpropertyvalue;
                    AIM.Base.Classes.RadHandler.RadEditor_LoadEditorPreference(ref radRichText, config);
                    break;                
                case "NUMBER":
                    tbText.Visible = true;
                    reqItem.ControlToValidate = "tbText";
                    tbText.TextMode = TextBoxMode.SingleLine;
                    if (hasvalue)
                        tbText.Text = tagpropertyvalue;
                    regItem.Enabled = true;
                    regItem.ValidationExpression = "[0-9]+";
                    regItem.ControlToValidate = "tbText";
                    break;
                case "MULTILINE":
                    tbText.Visible = true;
                    reqItem.ControlToValidate = "tbText";
                    tbText.TextMode = TextBoxMode.MultiLine;
                    if (hasvalue)
                        tbText.Text  = tagpropertyvalue;
                    break;
                case "GOOGLEGEOPOSITION":
                    tbText.Visible = true;
                    tbText.Attributes.Add("name", "lnglat");
                    tbText.Attributes.Add("class", "lnglat");
                    reqItem.ControlToValidate = "tbText";
                    tbText.TextMode = TextBoxMode.SingleLine;
                    if (hasvalue)
                        tbText.Text = tagpropertyvalue;
                    Page.RegisterClientScriptBlock("", "<script>jQuery('input.lnglat').locationPicker();</script>");
                    break;
            }
        }
        private void setLanguage()
        {
            lblTagName.Text = Language.getValue("lblTagName");
            lblTagSlug.Text = Language.getValue("lblTagSlug");
            lblTagType.Text = Language.getValue("lblTagType");
            btnSave.Text = Language.getValue("btnSave");
        }
        private void initTypeDropDown()
        {
            var Types = from t in db.TagTypes
                        select t;
            ddlTagTypes.DataSource = Types;
            ddlTagTypes.DataTextField = "Name";
            ddlTagTypes.DataValueField = "ID";
            ddlTagTypes.DataBind();

            ListItem li = new ListItem("Välj typ...");
            ddlTagTypes.Items.Insert(0, li);
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int TagID = 0;
            if (!String.IsNullOrEmpty(Request["ID"]) && int.TryParse(Request["ID"].ToString(), out TagID))
            {
                var theTag = (from t in db.Tags
                              where t.ID == TagID
                              select t).Single();

                theTag.Name = tbTagName.Text;
                theTag.Slug = tbTagSlug.Text;

                db.SubmitChanges();

                saveTagProperties(theTag, true);
            }
            else
            {
                AIM.Business.AimTags.Tag theTag = new AIM.Business.AimTags.Tag();
                theTag.Name = tbTagName.Text;
                theTag.Slug = tbTagSlug.Text;
                theTag.TagType_FK = int.Parse(ddlTagTypes.SelectedValue);

                db.Tags.InsertOnSubmit(theTag);
                db.SubmitChanges();

                saveTagProperties(theTag, false);

            }
            Response.Redirect("TagList.aspx");
            
        }
        protected void ddlTagTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlTagTypes.SelectedIndex!=0)
            {
                int selecteditem = int.Parse(ddlTagTypes.SelectedValue);
                initTagProperties(selecteditem);
            }
            
        }
        private void saveTagProperties(AIM.Business.AimTags.Tag theTag, bool isEdit)
        {
            foreach (RepeaterItem ri in rpTagProperties.Items)
            {
                var hfTagPropertyTypeId = (HiddenField)ri.FindControl("hfTagPropertyTypeId");
                var hfTagPropertyValueId = (HiddenField)ri.FindControl("hfTagPropertyValueId");
                Label lblTitle = (Label)ri.FindControl("lblTitle");
                TextBox tbText = (TextBox)ri.FindControl("tbText");
                RadEditor radRichText = (RadEditor)ri.FindControl("radRichText");
                RadDatePicker rdpDate = (RadDatePicker)ri.FindControl("rdpDate");

                TagTypePropertyValue item = new TagTypePropertyValue();
                if (isEdit)
                {
                    item = db.TagTypePropertyValues.Single(p => p.ID == Convert.ToInt32(hfTagPropertyValueId.Value));
                }
                else
                {
                    item.Tag_FK = theTag.ID;
                    item.TagTypeProperty_FK = int.Parse(hfTagPropertyTypeId.Value);
                    db.TagTypePropertyValues.InsertOnSubmit(item);
                    item.Value = "";
                    db.SubmitChanges();
                }

                switch (item.TagTypeProperty.TagPropertyType.Slug)
                {
                    case "TEXT":
                    case "NUMBER":
                    case "MULTILINE":
                    case "GOOGLEGEOPOSITION":
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
        }
    }
}