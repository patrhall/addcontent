using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using AIM.Business.AimTags;

namespace AimTags
{
    public partial class EditTagTreeObject : AIM.Base.Modules.Page.AimTags
    {
        protected override void Page_Load()
        {
            if(!IsPostBack)
                initTags();
        }

        private void initTags()
        {
            int oID = int.Parse(Request["oID"].ToString());
            var AvailableTags = from t in db.Tags
                       where !t.Deleted
                       orderby t.Name
                       select new
                       {
                           Name = t.Name + " [" + t.TagType.Name + "]",
                           t.ID
                       };

            var SelectedTags = from t in db.Object_X_Tags
                                where t.Object_FK == oID && !t.Tag.Deleted
                                orderby t.Tag.Name
                                select new
                                {
                                    Name = t.Tag.Name + " [" + t.Tag.TagType.Name + "]",
                                    t.Tag.ID
                                };



            rlbAvailableTags.DataSource = AvailableTags;
            rlbAvailableTags.DataTextField = "Name";
            rlbAvailableTags.DataValueField = "ID";
            rlbAvailableTags.DataBind();

            rlbSelectedTags.DataSource = SelectedTags;
            rlbSelectedTags.DataTextField = "Name";
            rlbSelectedTags.DataValueField = "ID";
            rlbSelectedTags.DataBind();

            List<RadListBoxItem> itemstodelete = new List<RadListBoxItem>();
            foreach (RadListBoxItem li_chosen in rlbSelectedTags.Items)
            {
                foreach (RadListBoxItem li_available in rlbAvailableTags.Items)
                    if (li_available.Value == li_chosen.Value)
                        itemstodelete.Add(li_available);
            }
            foreach (RadListBoxItem li in itemstodelete)
                li.Remove();
        }
        protected void btnSaveTags_Click(object sender, EventArgs e)
        {            
            int oID = int.Parse(Request["oID"].ToString());
            RemoveAllTags(oID);
            foreach (RadListBoxItem li in rlbSelectedTags.Items)
            {
                Object_X_Tag oXt = new Object_X_Tag();
                oXt.Object_FK = oID;
                oXt.Tag_FK = int.Parse(li.Value);

                db.Object_X_Tags.InsertOnSubmit(oXt);                
            }
            db.SubmitChanges();
            initTags();
        }

        private void RemoveAllTags(int oID)
        {
            var Items = from i in db.Object_X_Tags
                        where i.Object_FK == oID
                        select i;
            db.Object_X_Tags.DeleteAllOnSubmit(Items);
            db.SubmitChanges();
        }
    }
}