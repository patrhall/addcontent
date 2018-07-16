using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.Linq;
using System.Collections.Generic;
using Telerik.Web.UI;

namespace AimAds
{
    public partial class Ads : AIM.Base.Modules.Page.AimAds
    {
        public string imgPath
        {
            get
            {
                return config.customerpath + "ads/";
            }
        }

        protected override void Page_Load()
        {            
            if (!IsPostBack)            
                init();                            
        }

        private void init()
        {
            int groupID;

            rcbAdGroups.DataSource = (from o in db.AimAds_Groups
                                     where o.Site_FK == config.siteID
                                     orderby o.Name
                                     select o);
            rcbAdGroups.DataBind();

            rcbAdGroups.SelectedIndex = 0;

            if (int.TryParse(Request["GroupID"], out groupID))
                rcbAdGroups.SelectedValue = groupID.ToString();            
            
            fillGroupAds();
        }

        protected void rcbAdGroups_Changed(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            fillGroupAds();
        }

        protected void fillGroupAds()
        {
            AIM.Business.AimAds.AimAds_Group theGroup = db.AimAds_Groups.Single(p => p.GroupID == Convert.ToInt32(rcbAdGroups.SelectedValue));

            repHorizontal.DataSource = (from o in db.AimAds_Items
                                        where o.AimAds_Group_FK == theGroup.GroupID
                                            && !o.Deleted
                                        orderby o.Sort ascending
                                        select new
                                        {
                                            o.ID,
                                            o.AimAds_Group_FK,
                                            ImageFilename = ((o.ImageFilename != null && o.ImageFilename != "") ? o.ImageFilename : "noimageorscript.jpg"),
                                            o.Title,
                                            o.Href
                                        });
            repHorizontal.DataBind();

            hlNewAd.NavigateUrl = "EditAd.aspx?GroupID=" + rcbAdGroups.SelectedValue + "&ADID=0";            
        }
        
        protected void repHorizontal_OnItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.CommandName == "up")
                {
                    sortAdsUp(Convert.ToInt32(e.CommandArgument));
                }
                else if (e.CommandName == "down")
                {
                    sortAdsDown(Convert.ToInt32(e.CommandArgument));//btn.CommandArgument));
                }
                else if (e.CommandName == "delete")
                {
                    DeleteAd(Convert.ToInt32(e.CommandArgument));
                }                
            }
            
        }        

        protected void DeleteAd(int aID)
        {
            var theItem = db.AimAds_Items.Single(p => p.ID == aID);
            theItem.Deleted = true;
            theItem.DeletedDate = DateTime.Now;

            db.SubmitChanges();
            fillGroupAds();
        }
        
        protected void sortAdsUp(int adID)
        {
            List<AIM.Business.AimAds.AimAds_Item> adsList =
                (from o in db.AimAds_Items
                 where o.AimAds_Group_FK == Convert.ToInt32(rcbAdGroups.SelectedValue) && o.Deleted != true
                 orderby o.Sort ascending
                 select o).ToList<AIM.Business.AimAds.AimAds_Item>();

            int previousSort = 1;
            int previousID = adID;
            bool gate = true;

            foreach(AIM.Business.AimAds.AimAds_Item ad in adsList)
            {
                if (ad.ID == adID)
                {
                    int oldSort = ad.Sort.Value;
                    ad.Sort = previousSort;
                    previousSort = oldSort;
                    gate = false;
                }
                else
                {
                    if (gate)
                    {
                        previousSort = ad.Sort.Value;
                        previousID = ad.ID;
                    }
                }
            }
            
            db.SubmitChanges();
            
            AIM.Business.AimAds.AimAds_Item theItem = (from linq_itm in db.AimAds_Items where linq_itm.ID == previousID select linq_itm).First<AIM.Business.AimAds.AimAds_Item>();

            theItem.Sort = previousSort;
            
            db.SubmitChanges();

            fillGroupAds();
        }

        protected void sortAdsDown(int adID)
        {

            List<AIM.Business.AimAds.AimAds_Item> adsList =
                (from o in db.AimAds_Items
                 where o.AimAds_Group_FK == Convert.ToInt32(rcbAdGroups.SelectedValue) && o.Deleted != true
                 orderby o.Sort ascending
                 select o).ToList<AIM.Business.AimAds.AimAds_Item>();

            int previousSort = 1;
            int previousID = adID;
            bool gate = false;

            foreach (AIM.Business.AimAds.AimAds_Item ad in adsList)
            {
                if (ad.ID == adID)
                {
                    previousSort = ad.Sort.Value;
                    gate = true;
                }
                else
                {
                    if (gate)
                    {
                        int oldSort = ad.Sort.Value;
                        ad.Sort = previousSort;
                        previousSort = oldSort;

                        gate = false;
                    }
                }
            }

            db.SubmitChanges();

            AIM.Business.AimAds.AimAds_Item theItem = (from linq_itm in db.AimAds_Items where linq_itm.ID == adID select linq_itm).First<AIM.Business.AimAds.AimAds_Item>();

            theItem.Sort = previousSort;

            db.SubmitChanges();

            fillGroupAds();
        }
    }
}
